<template>
  <div class="p-m-4">
    <h3>Detalhes do Livro</h3>
    <Card>
      <template #title>{{ livro.titulo }}</template>
      <template #content>
        <p><strong>Autor:</strong> {{ livro.autor }}</p>
        <p><strong>Editora:</strong> {{ livro.editora }}</p>
        <p><strong>Ano:</strong> {{ livro.ano }}</p>
        <p><strong>ISBN:</strong> {{ livro.isbn }}</p>
        <p><strong>Disponíveis:</strong> {{ livro.disponivel }}</p>
      </template>
    </Card>
  </div>
</template>

<script>
import Card from 'primevue/card'

export default {
  name: 'BookDetail',
  components: { Card },
  data() {
    return {
      livro: {
        id: this.$route.params.id,
        titulo: 'Dom Casmurro',
        autor: 'Machado de Assis',
        editora: 'Editora X',
        ano: 1899,
        isbn: '123-456-789',
        disponivel: 3
      }
    }
  }
}
</script>
