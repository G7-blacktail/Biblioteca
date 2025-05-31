<template>
  <div class="p-m-4">
    <h3>Cadastrar Novo Livro</h3>
    <div class="p-fluid p-formgrid p-grid">
      <div class="p-field p-col-12 p-md-6">
        <label for="titulo">Título</label>
        <InputText id="titulo" v-model="livro.titulo" />
      </div>
      <div class="p-field p-col-12 p-md-6">
        <label for="autor">Autor</label>
        <InputText id="autor" v-model="livro.autor" />
      </div>
      <div class="p-field p-col-12 p-md-6">
        <label for="editora">Editora</label>
        <InputText id="editora" v-model="livro.editora" />
      </div>
      <div class="p-field p-col-12 p-md-3">
        <label for="ano">Ano</label>
        <InputNumber id="ano" v-model="livro.ano" :min="1500" />
      </div>
      <div class="p-field p-col-12 p-md-3">
        <label for="quantidade">Quantidade</label>
        <InputNumber id="quantidade" v-model="livro.quantidade" />
      </div>
    </div>
    <Button label="Salvar" icon="pi pi-save" class="p-mt-2" @click="salvarLivro" />
  </div>
</template>

<script>
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import Button from 'primevue/button'

export default {
  name: 'AddBook',
  components: { InputText, InputNumber, Button },
  data() {
    return {
      livro: {
        titulo: '',
        autor: '',
        editora: '',
        ano: null,
        quantidade: null
      }
    }
  },
  methods: {
    salvarLivro() {
      console.log('Livro salvo:', this.livro)
      alert('Livro cadastrado com sucesso!')
      // Aqui você chamaria a API
    }
  }
}
</script>
