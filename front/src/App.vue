<template>
  <div>
    <Menubar v-if="isAuthenticated" :model="items" class="p-mb-4" />
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
        { label: 'Home', icon: 'pi pi-home', command: () => this.$router.push('/home') },
        { label: 'Dashboard', icon: 'pi pi-chart-bar', command: () => this.$router.push('/admin') },
        { label: 'Livros', icon: 'pi pi-book', command: () => this.$router.push('/livros') },
        { label: 'Usuários', icon: 'pi pi-users', command: () => this.$router.push('/admin/usuarios') },
        { label: 'Sair', icon: 'pi pi-sign-out', command: this.logout }
      ]
    } else {
      this.items = [
        { label: 'Home', icon: 'pi pi-home', command: () => this.$router.push('/home') },
        { label: 'Dashboard', icon: 'pi pi-chart-bar', command: () => this.$router.push('/user') },
        { label: 'Livros Disponíveis', icon: 'pi pi-book', command: () => this.$router.push('/livros') },
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
