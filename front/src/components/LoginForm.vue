<template>
  <div class="p-d-flex p-jc-center p-ai-center p-min-h-screen" style="min-height: 100vh;">
    <div class="p-card p-shadow-4 p-p-4" style="width: 350px">
      <div class="p-d-flex p-jc-center p-mb-4">
        <img src="@/assets/logo.png" alt="Logo" style="max-width: 120px;" />
      </div>
      <h3 class="p-text-center p-mb-4">Login</h3>
      <div class="p-mb-4">
        <div>
          <label for="email" style="width: 70px;" class="p-mr-3">E-mail   </label>
          <InputText id="email" v-model="email" class="p-inputtext-sm" style="flex: 1" />
        </div>
        <div>
          <label for="senha" style="width: 70px;" class="p-mr-3">Senha</label>
          <Password id="senha" v-model="senha" toggleMask :feedback="false" class="p-inputtext-sm" style="flex: 1" />
        </div>
      </div>
      <Button label="Entrar" icon="pi pi-sign-in" class="p-mt-2 p-w-full" @click="realizarLogin" />
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
        this.$router.push('/home')
      } else {
        alert('Credenciais inválidas')
      }
    }
  }
}
</script>