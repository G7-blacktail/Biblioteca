<template>
  <div class="p-m-4">
    <h2>Bem-vindo à Biblioteca</h2>
    <DataTable :value="locacoes" paginator rows="5" responsiveLayout="scroll">
    <Column field="livroTitulo" header="Livro" />
    <Column field="stLocacao" header="Status" />
    <Column field="dtDevolucaoPrevista" header="Devolução Prevista" />
    <Column field="vlMulta" header="Multa" />
    <Column field="pagamentoPendente" header="Pagamento Pendente" />
  </DataTable>
  </div>
</template>

<script>
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import locacaoService from '@/services/locacao.service'
import livrosService from '@/services/livros.services'

export default {
  components: { DataTable, Column },
  data() {
    return {
      locacoes: [],
      livrosDisponiveis: []
    }
  },
  async mounted() {
    // Carrega locações do usuário logado
    const idUsuario = this.$store.state.usuario.idUsuario
    this.locacoes = await locacaoService.getLocacoesUsuario(idUsuario) || []

    // Carrega livros disponíveis (qtDisponivel > 0)
    const todosLivros = await livrosService.getLivros()
    this.livrosDisponiveis = todosLivros.filter(l => l.qtDisponivel > 0)
  }
}
</script>
