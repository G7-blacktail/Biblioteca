<template>
  <div class="p-m-4">
    <div class="p-d-flex p-jc-between p-ai-center p-mb-3">
      <h3>Acervo de Livros</h3>
      <Button label="Novo Livro" icon="pi pi-plus" @click="novoLivro" />
    </div>

    <DataTable :value="livros" :paginator="true" rows="5" responsiveLayout="scroll">
      <Column field="titulo" header="Título" />
      <Column field="autor" header="Autor" />
      <Column field="ano" header="Ano" />
      <Column field="disponivel" header="Disponível" />
      <Column header="Ações">
        <template #body="slotProps">
          <Button icon="pi pi-eye" class="p-button-text p-mr-1" @click="verDetalhes(slotProps.data)" />
          <Button icon="pi pi-pencil" class="p-button-text p-mr-1" @click="editarLivro(slotProps.data)" />
          <Button icon="pi pi-trash" class="p-button-danger p-button-text" @click="excluirLivro(slotProps.data)" />
        </template>
      </Column>
    </DataTable>

    <Dialog v-show="detalhesVisivel" header="Detalhes do Livro" :modal="true" :style="{ width: '500px' }">
      <BookDetails :livro="livroSelecionado" />
    </Dialog>
  </div>
</template>

<script>
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import BookDetails from './BookDetails.vue'

export default {
  components: { DataTable, Column, Button, Dialog, BookDetails },
  data() {
    return {
      livros: [
        { id: 1, titulo: 'Dom Casmurro', autor: 'Machado de Assis', ano: 1899, disponivel: 2, preco: 5.99 },
        { id: 2, titulo: 'O Hobbit', autor: 'Tolkien', ano: 1937, disponivel: 0, preco: 8.99 }
      ],
      livroSelecionado: null,
      detalhesVisivel: false
    }
  },
  methods: {
    verDetalhes(livro) {
      this.livroSelecionado = livro
      this.detalhesVisivel = true
    },
    editarLivro(livro) {
      this.$router.push({ path: `/admin/novo-livro`, query: { id: livro.id } })
    },
    excluirLivro(livro) {
      if (confirm(`Deseja realmente excluir o livro "${livro.titulo}"?`)) {
        this.livros = this.livros.filter(l => l.id !== livro.id)
      }
    },
    novoLivro() {
      this.$router.push('/admin/novo-livro')
    }
  }
}
</script>
