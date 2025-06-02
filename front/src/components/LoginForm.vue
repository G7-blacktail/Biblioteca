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
      <Button label="Entrar" icon="pi pi-sign-in" class="p-mt-3" @click="realizarLogin" />
    </div>
  </div>
</template>

<script>
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import Button from 'primevue/button'
import { mapActions } from 'vuex'

export default {
  components: { InputText, Password, Button },
  data() {
    return {
      email: '',
      senha: ''
    }
  },
  methods: {
    ...mapActions(['login']),
    realizarLogin() {
      const usuariosMockados = [
        {
          nome: 'Administrador',
          email: 'admin@teste.com',
          senha: 'admin',
          role: 'admin'
        },
        {
          nome: 'Usuário',
          email: 'user@teste.com',
          senha: 'user',
          role: 'user'
        }
      ]

    const usuario = usuariosMockados.find(u => u.email === this.email && u.senha === this.senha)

      if (usuario) {
        this.login(usuario)
        this.$router.push('/')
      } else {
        alert('Credenciais inválidas')
      }
    },
    created() {
      const user = this.$store.getters.user
      if (user) {
        this.$router.push(user.role === 'admin' ? '/admin' : '/user')
      }
    }
  }
}
</script>
