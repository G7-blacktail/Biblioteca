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
        <p><strong>Preço de locação:</strong> R$ {{ livro.preco.toFixed(2) }}</p>
        <p><strong>Status:</strong> {{ livro.disponivel > 0 ? 'Disponível' : 'Alugado' }}</p>
        <p v-if="livro.disponivel === 0"><strong>Com usuário:</strong> João da Silva</p>
      </template>
    </Card>
  </div>
</template>

<script>
export default {
  name: 'BookDetails',
  props: {
    livro: Object
  }
}
</script>