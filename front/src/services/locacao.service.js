import axios from 'axios'
import environment from '../../environments/environments'

const api = axios.create({
  baseURL: environment.apiBaseUrl
})

function tratarErro(error) {
  if (error.response) {
    throw new Error(error.response.data || 'Erro na API')
  } else if (error.request) {
    throw new Error('Sem resposta do servidor')
  } else {
    throw new Error(error.message)
  }
}

export default {
  async getLocacoes() {
    try {
      const response = await api.get('/Locacoes')
      return response.data
    } catch (error) {
      tratarErro(error)
    }
  },
    async getLocacoesDashboard() {
        try {
            const response = await api.get('/Locacoes/dashboard')
            return response.data // já será um array simples
        } catch (error) {
            tratarErro(error)
            return []
        }
    },
    async getLocacoesUsuario(idUsuario) {
        try {
            const response = await api.get(`/Locacoes/usuario/${idUsuario}`)
            return response.data
        } catch (error) {
            tratarErro(error)
            return []
        }
    },
    async solicitarLocacao(idLivro, idUsuario, dtDevolucaoPrevista) {
        try {
        const response = await api.post('/Locacoes', {
            idLivro,
            idUsuario,
            dtDevolucaoPrevista
        })
        return response.data
        } catch (error) {
        tratarErro(error)
        }
    },
    async devolverLivro(idLocacao) {
        try {
        const response = await api.post(`/Locacoes/${idLocacao}/devolver`)
        return response.data
        } catch (error) {
        tratarErro(error)
        }
    }
}