<template>
  <div class="p-m-4">
    <h3>Detalhes do Livro</h3>
    <div v-if="livro">
      <p><strong>Título:</strong> {{ livro.nmTitulo }}</p>
      <p><strong>Autor:</strong> {{ livro.nmAutor }}</p>
      <p><strong>Ano:</strong> {{ livro.aaPublicacao }}</p>
      <p><strong>Disponível:</strong> {{ livro.qtDisponivel }}</p>
      <Button label="Voltar" class="p-mt-2" @click="$router.back()" />
    </div>
    <div v-else>
      Carregando...
    </div>
  </div>
</template>

<script>
import livrosService from '@/services/livros.services'
import Button from 'primevue/button'

export default {
  name: 'BookDetails',
  components: { Button },
  data() {
    return {
      livro: null
    }
  },
  async mounted() {
    const id = this.$route.params.id
    this.livro = await livrosService.getLivro(id)
  }
}
</script>