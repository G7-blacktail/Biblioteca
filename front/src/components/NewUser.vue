<template>
  <div class="p-m-4">
    <h3>Novo Usuário</h3>
    <form @submit.prevent="salvarUsuario" class="p-fluid">
      <div class="p-field">
        <label for="nome">Nome *</label>
        <InputText id="nome" v-model="usuario.nmUsuario" required :class="{'p-invalid': submitted && !usuario.nmUsuario}" />
      </div>
      <div class="p-field">
        <label for="email">E-mail *</label>
        <InputText id="email" v-model="usuario.nmEmail" required :class="{'p-invalid': submitted && !usuario.nmEmail}" />
      </div>
      <div class="p-field">
        <label for="telefone">Telefone</label>
        <InputText id="telefone" v-model="usuario.nrTelefone" />
      </div>
      <div class="p-field">
        <label for="senha">Senha *</label>
        <InputText id="senha" v-model="usuario.dsSenha" type="password" required :class="{'p-invalid': submitted && !usuario.dsSenha}" />
      </div>
      <div class="p-field">
        <label for="perfil">Perfil *</label>
        <Dropdown
          id="perfil"
          v-model="usuario.tpPerfil"
          :options="perfis"
          optionLabel="label"
          optionValue="value"
          placeholder="Selecione o perfil"
          required
          :class="{'p-invalid': submitted && !usuario.tpPerfil}"
        />
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
import Button from 'primevue/button'
import Dropdown from 'primevue/dropdown'
import usuariosService from '@/services/usuario.service'

export default {
  components: { InputText, Button, Dropdown  },
  data() {
    return {
      usuario: {
        nmUsuario: '',
        nmEmail: '',
        nrTelefone: '',
        dsSenha: '',
        tpPerfil: ''
      },perfis: [
        { label: 'Administrador', value: 'Administrador' },
        { label: 'Padrao', value: 'Padrao' }
      ],
      submitted: false
    }
  },
  computed: {
    formValido() {
      return (
        this.usuario.nmUsuario &&
        this.usuario.nmEmail &&
        this.usuario.dsSenha &&
        this.usuario.tpPerfil
      )
    }
  },
  methods: {
    async salvarUsuario() {
      this.submitted = true
      if (!this.formValido) return

      try {
        await usuariosService.criarUsuario(this.usuario)
        this.$router.push('/admin/usuarios')
      } catch (error) {
        alert(error.response?.data || error.message || 'Erro ao salvar usuário')
      }
    },
    cancelar() {
      this.$router.push('/admin/usuarios')
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