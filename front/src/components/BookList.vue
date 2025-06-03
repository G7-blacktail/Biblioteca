<template>
  <div class="p-m-4">
    <div class="p-d-flex p-jc-between p-ai-center p-mb-3">
      <h3>Acervo de Livros</h3>
      <Button
        v-if="$store.getters.isAdmin"
        label="Novo Livro"
        icon="pi pi-plus"
        @click="novoLivro"
      />
    </div>

    <DataTable :value="livros" :paginator="true" rows="5" responsiveLayout="scroll">
      <Column field="nmTitulo" header="Título" />
      <Column field="nmAutor" header="Autor" />
      <Column field="aaPublicacao" header="Ano" />
      <Column field="qtDisponivel" header="Disponível" />
      <Column header="Ações">
        <template #body="slotProps">
          <Button icon="pi pi-eye" class="p-button-text p-mr-1"
            @click="verDetalhes(slotProps.data)" />
          <Button icon="pi pi-pencil" class="p-button-text p-mr-1"
            @click="editarLivro(slotProps.data)" v-if="$store.getters.isAdmin" />
          <!-- <Button icon="pi pi-trash" class="p-button-danger p-button-text"
            @click="excluirLivro(slotProps.data)" v-if="$store.getters.isAdmin" /> -->
        </template>
      </Column>
    </DataTable>

  </div>
</template>

<script>
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import livrosService from '@/services/livros.services'

export default {
  components: { DataTable, Column, Button },
  data() {
    return {
      livros: [],
      livroSelecionado: null,
      detalhesVisivel: false
    }
  },
  mounted() {
    this.carregarLivros()
  },
  methods: {
    async carregarLivros() {
      try {
        this.livros = await livrosService.getLivros()
      } catch (error) {
        alert(error.message)
      }
    },
    verDetalhes(livro) {
      this.$router.push({ path: `/livros/${livro.idLivro}` })
    },
    editarLivro(livro) {
      this.$router.push({ path: `/admin/novo-livro`, query: { id: livro.idLivro } })
    },
    async excluirLivro(livro) {
      if (confirm(`Deseja realmente excluir o livro "${livro.nmTitulo}"?`)) {
        try {
          await livrosService.deletarLivro(livro.idLivro)
          this.carregarLivros()
          } catch (error) {
            alert(error.response?.data || error.message || 'Não é possível excluir um livro que possui locações.')
          }
        }
    },
    novoLivro() {
      this.$router.push('/admin/novo-livro')
    }
  }
}
</script>