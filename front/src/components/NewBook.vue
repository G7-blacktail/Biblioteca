<template>
  <div class="p-m-4">
    <h3>{{ livro.idLivro ? 'Editar Livro' : 'Novo Livro' }}</h3>
    <form @submit.prevent="salvarLivro" class="p-fluid">
      <div class="p-field">
        <label for="titulo">Título *</label>
        <InputText id="titulo" v-model="livro.nmTitulo" required :class="{'p-invalid': submitted && !livro.nmTitulo}" />
      </div>
      <div class="p-field">
        <label for="autor">Autor *</label>
        <InputText id="autor" v-model="livro.nmAutor" required :class="{'p-invalid': submitted && !livro.nmAutor}" />
      </div>
      <div class="p-field">
        <label for="editora">Editora</label>
        <InputText id="editora" v-model="livro.nmEditora" />
      </div>
      <div class="p-field">
        <label for="ano">Ano</label>
        <InputNumber id="ano" v-model="livro.aaPublicacao" :min="1500" :max="2100" />
      </div>
      <div class="p-field">
        <label for="isbn">ISBN *</label>
        <InputText id="isbn" v-model="livro.isbnLivro" required :class="{'p-invalid': submitted && !livro.isbnLivro}" />
      </div>
      <div class="p-field">
        <label for="disponivel">Quantidade Disponível *</label>
        <InputNumber id="disponivel" v-model="livro.qtDisponivel" :min="0" required :class="{'p-invalid': submitted && (livro.qtDisponivel === null || livro.qtDisponivel === undefined)}" />
      </div>
      <div class="p-d-flex p-jc-end p-mt-3">
        <Button type="submit" label="Salvar" class="p-button-success p-mr-2" />
        <Button type="button" label="Cancelar" class="p-button-secondary" @click="cancelar" />
      </div>
      <div v-if="submitted && !formValido" class="p-error p-mt-2">
        Preencha todos os campos obrigatórios!
      </div>
    </form>
  </div>
</template>

<script>
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import Button from 'primevue/button'
import livrosService from '@/services/livros.services'

export default {
  components: { InputText, InputNumber, Button },
  data() {
    return {
      livro: {
        nmTitulo: '',
        nmAutor: '',
        nmEditora: '',
        aaPublicacao: null,
        isbnLivro: '',
        qtDisponivel: null
      },
      submitted: false
    }
  },
  computed: {
    formValido() {
      return (
        this.livro.nmTitulo &&
        this.livro.nmAutor &&
        this.livro.isbnLivro &&
        this.livro.qtDisponivel !== null &&
        this.livro.qtDisponivel !== undefined
      )
    }
  },
  created() {
    const id = this.$route.query.id
    if (id) {
      livrosService.getLivro(id).then(livro => {
        this.livro = livro
      })
    }
  },
  methods: {
    async salvarLivro() {
      this.submitted = true
      if (!this.formValido) return

      try {
        if (this.livro.idLivro) {
          await livrosService.atualizarLivro(this.livro.idLivro, this.livro)
        } else {
          await livrosService.criarLivro(this.livro)
        }
        this.$router.push('/livros')
      } catch (error) {
        alert(error.response?.data || error.message || 'Erro ao salvar livro')
      }
    },
    cancelar() {
      this.$router.push('/livros')
    }
  }
}
</script>

<style scoped>
.p-invalid {
  border-color: #f44336 !important;
}
.p-error {
  color: #f44336;
}
</style>