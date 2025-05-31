<template>
  <div>
    <Menubar :model="items" />
    <router-view />
  </div>
</template>

<script>
import Menubar from 'primevue/menubar'

export default {
  name: 'App',
  components: { Menubar },
  data() {
    return {
      items: []
    }
  },
  mounted() {
    // Simulando autenticação (você pode substituir pelo Vuex ou LocalStorage)
    const user = {
      nome: 'João',
      perfil: 'admin' // ou 'user'
    }

    this.configureMenu(user)
  },
  methods: {
    configureMenu(user) {
      if (user.perfil === 'admin') {
        this.items = [
          { label: 'Dashboard', icon: 'pi pi-home', to: '/admin' },
          { label: 'Livros', icon: 'pi pi-book', items: [
            { label: 'Ver Acervo', to: '/livros' },
            { label: 'Cadastrar Livro', to: '/admin/novo-livro' }
          ]},
          { label: 'Usuários', icon: 'pi pi-users', to: '/admin/usuarios' },
          { label: 'Sair', icon: 'pi pi-sign-out', command: this.logout }
        ]
      } else {
        this.items = [
          { label: 'Dashboard', icon: 'pi pi-home', to: '/user' },
          { label: 'Livros Disponíveis', icon: 'pi pi-book', to: '/livros' },
          { label: 'Sair', icon: 'pi pi-sign-out', command: this.logout }
        ]
      }
    },
    logout() {
      // Simular logout: redirecionar e limpar dados
      console.log('Logout realizado')
      this.$router.push('/login')
    }
  }
}
</script>

<style>
body {
  margin: 0;
}

.p-menubar {
  border-radius: 0;
  z-index: 1000;
  position: sticky;
  top: 0;
}
</style>