<template>
  <div class="p-m-4">
    <h3>Acervo de Livros</h3>
    <DataTable :value="livros" paginator rows="5" responsiveLayout="scroll">
      <Column field="titulo" header="Título" />
      <Column field="autor" header="Autor" />
      <Column field="ano" header="Ano" />
      <Column field="disponivel" header="Disponível" />
      <Column header="Ações">
        <template #body="slotProps">
          <Button icon="pi pi-eye" label="Detalhes" class="p-button-text"
            @click="verDetalhes(slotProps.data)" />
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'

export default {
  name: 'BookList',
  components: { DataTable, Column, Button },
  data() {
    return {
      livros: [
        { id: 1, titulo: 'Dom Casmurro', autor: 'Machado de Assis', ano: 1899, disponivel: 3 },
        { id: 2, titulo: 'O Hobbit', autor: 'J.R.R. Tolkien', ano: 1937, disponivel: 0 }
      ]
    }
  },
  methods: {
    verDetalhes(livro) {
      this.$router.push({ name: 'BookDetail', params: { id: livro.id } })
    }
  }
}
</script>
