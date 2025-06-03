<template>
  <div class="p-m-4">
    <h2>Painel do Administrador</h2>
    <DataTable :value="locacoes" paginator rows="5" responsiveLayout="scroll">
      <Column field="livroTitulo" header="Livro" />
      <Column field="usuarioNome" header="Usuário" />
      <Column field="dtRetirada" header="Retirada" />
      <Column field="dtDevolucaoPrevista" header="Devolução Prevista" />
      <Column field="stLocacao" header="Status" />
      <Column field="vlMulta" header="Multa" />
      <Column header="Ações">
        <template #body="slotProps">
          <Button
            v-if="slotProps.data.stLocacao === 'Pendente'"
            label="Devolver"
            icon="pi pi-undo"
            class="p-button-success p-button-sm"
            @click="confirmarDevolucao(slotProps.data)"
          />
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import locacaoService from '@/services/locacao.service'

export default {
  name: 'AdministradorDashBoard',
  components: { DataTable, Column, Button },
  data() {
    return {
      locacoes: []
    }
  },
  async mounted() {
    this.locacoes = await locacaoService.getLocacoesDashboard() || []
  },
  methods: {
    async carregarLocacoes() {
      try {
        this.locacoes = await locacaoService.getLocacoes()
      } catch (error) {
        alert(error.message)
      }
    },
    async confirmarDevolucao(locacao) {
      if (confirm(`Confirmar devolução da locação #${locacao.idLocacao}?`)) {
        try {
          await locacaoService.devolverLivro(locacao.idLocacao)
          this.carregarLocacoes()
        } catch (error) {
          alert(error.message)
        }
      }
    }
  }
}
</script>