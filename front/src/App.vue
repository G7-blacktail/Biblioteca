<template>
  <div>
    <Menubar v-if="isAuthenticated" :model="items" class="p-mb-4" :router="true" />
    <router-view />
  </div>
</template>

<script>
import Menubar from 'primevue/menubar'
import { mapGetters } from 'vuex'

export default {
  name: 'App',
  components: { Menubar },
  data() {
    return { items: [] }
  },
  computed: {
    ...mapGetters(['isAuthenticated', 'user'])
  },
  watch: {
    user: {
      immediate: true,
      handler(user) {
        if (user) this.configureMenu(user)
      }
    }
  },
  methods: {
    configureMenu(user) {
      if (user.role === 'admin') {
        this.items = [
          { label: 'Dashboard', icon: 'pi pi-home', routerLink: '/admin' },
          { label: 'Livros', icon: 'pi pi-book', routerLink: '/admin/livros' },
          { label: 'Usuários', icon: 'pi pi-users', routerLink: '/admin/usuarios' },
          { label: 'Sair', icon: 'pi pi-sign-out', command: this.logout }
        ]
      } else {
        this.items = [
          { label: 'Dashboard', icon: 'pi pi-home', routerLink: '/user' },
          { label: 'Livros Disponíveis', icon: 'pi pi-book', routerLink: '/livros' },
          { label: 'Sair', icon: 'pi pi-sign-out', command: this.logout }
        ]
      }
    },
    logout() {
      this.$store.dispatch('logout')
      this.$router.push('/login')
    }
  }
}
</script>
