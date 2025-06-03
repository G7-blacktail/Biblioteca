<template>
  <div class="p-m-4">
    <h3>Detalhes do Usuário</h3>
    <div v-if="usuario">
      <p><strong>Nome:</strong> {{ usuario.nmUsuario }}</p>
      <p><strong>E-mail:</strong> {{ usuario.nmEmail }}</p>
      <p><strong>Telefone:</strong> {{ usuario.nrTelefone }}</p>
      <p><strong>Perfil:</strong> {{ usuario.tpPerfil }}</p>
      <Button label="Voltar" class="p-mt-2" @click="$router.back()" />
    </div>
    <div v-else>
      Carregando...
    </div>
  </div>
</template>

<script>
import usuariosService from '@/services/usuario.service'
import Button from 'primevue/button'

export default {
  components: { Button },
  data() {
    return {
      usuario: null
    }
  },
  async mounted() {
    const id = this.$route.params.id
    this.usuario = await usuariosService.getUsuario(id)
  }
}
</script>