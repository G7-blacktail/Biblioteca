<template>
  <div class="p-d-flex p-jc-center p-mt-6">
    <div class="p-card p-shadow-4 p-p-4" style="width: 350px">
      <h3>Login</h3>
      <div class="p-field">
        <label for="email">E-mail</label>
        <InputText id="email" v-model="email" class="p-inputtext-sm" />
      </div>
      <div class="p-field">
        <label for="senha">Senha</label>
        <Password id="senha" v-model="senha" toggleMask />
      </div>
      <Button label="Entrar" icon="pi pi-sign-in" class="p-mt-3" @click="login" />
    </div>
  </div>
</template>

<script>
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import Button from 'primevue/button'

export default {
  components: { InputText, Password, Button },
  data() {
    return {
      email: '',
      senha: ''
    }
  },
  methods: {
    login() {
      if (this.email === 'admin@teste.com' && this.senha === 'admin') {
        this.$router.push('/admin')
      } else if (this.email === 'user@teste.com' && this.senha === 'user') {
        this.$router.push('/user')
      } else {
        alert('Credenciais inválidas')
      }
    }
  }
}
</script>
