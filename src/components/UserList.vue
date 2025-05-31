<template>
  <div class="p-m-4">
    <h3>Usuários Cadastrados</h3>
    <DataTable :value="usuarios" paginator rows="5">
      <Column field="nome" header="Nome" />
      <Column field="email" header="E-mail" />
      <Column field="perfil" header="Perfil" />
      <Column header="Ações">
        <template #body>
          <Button icon="pi pi-trash" class="p-button-danger p-button-text" />
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'

export default {
  name: 'UserList',
  components: { DataTable, Column, Button },
  data() {
    return {
      usuarios: [
        { nome: 'João', email: 'joao@email.com', perfil: 'Usuário' },
        { nome: 'Maria', email: 'maria@email.com', perfil: 'Administrador' }
      ]
    }
  }
}
</script>
