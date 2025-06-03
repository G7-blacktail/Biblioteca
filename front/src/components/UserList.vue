<template>
  <div class="p-m-4">
    <div class="p-d-flex p-jc-between p-ai-center p-mb-3">
      <h3>Usuários Cadastrados</h3>
      <Button
        label="Novo Usuário"
        icon="pi pi-plus"
        @click="novoUsuario"
      />
    </div>
    <DataTable :value="usuarios" paginator rows="5" responsiveLayout="scroll">
      <Column field="nmUsuario" header="Nome" />
      <Column field="nmEmail" header="E-mail" />
      <Column field="tpPerfil" header="Perfil" />
      <Column header="Ações">
        <template #body="slotProps">
          <Button icon="pi pi-eye" class="p-button-text p-mr-1"
            @click="verDetalhes(slotProps.data)" />
          <Button icon="pi pi-trash" class="p-button-danger p-button-text"
            @click="excluirUsuario(slotProps.data)" />
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import usuariosService from '@/services/usuario.service'

export default {
  name: 'UserList',
  components: { DataTable, Column, Button },
  data() {
    return {
      usuarios: []
    }
  },
  mounted() {
    this.carregarUsuarios()
  },
  methods: {
    async carregarUsuarios() {
      try {
        this.usuarios = await usuariosService.getUsuarios()
      } catch (error) {
        alert(error.message)
      }
    },
    verDetalhes(usuario) {
      this.$router.push({ path: `/admin/usuarios/${usuario.idUsuario}` })
    },
    async excluirUsuario(usuario) {
      if (confirm(`Deseja realmente excluir o usuário "${usuario.nmUsuario}"?`)) {
        try {
          await usuariosService.deletarUsuario(usuario.idUsuario)
          this.carregarUsuarios()
        } catch (error) {
          alert(error.message)
        }
      }
    },
    novoUsuario() {
      this.$router.push('/admin/novo-usuario')
    }
  }
}
</script>