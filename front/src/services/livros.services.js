import axios from 'axios'
import environment from '../../environments/environments'

const api = axios.create({
  baseURL: environment.apiBaseUrl
})

// Função auxiliar para tratar erros
function tratarErro(error) {
  if (error.response) {
    // Se for objeto, transforma em string legível
    const msg = typeof error.response.data === 'string'
      ? error.response.data
      : JSON.stringify(error.response.data)
    throw new Error(msg || 'Erro na API')
  } else if (error.request) {
    throw new Error('Sem resposta do servidor')
  } else {
    throw new Error(error.message)
  }
}

export default {
  async getLivros() {
    try {
      const response = await api.get('/Livros')
      return response.data
    } catch (error) {
      tratarErro(error)
    }
  },
  async getLivro(id) {
    try {
      const response = await api.get(`/Livros/${id}`)
      return response.data
    } catch (error) {
      tratarErro(error)
    }
  },
  async criarLivro(livro) {
    try {
      const response = await api.post('/Livros', livro)
      return response.data
    } catch (error) {
      tratarErro(error)
    }
  },
  async atualizarLivro(id, livro) {
    try {
      await api.put(`/Livros/${id}`, livro)
      return true
    } catch (error) {
      tratarErro(error)
    }
  },
  async deletarLivro(id) {
    try {
      await api.delete(`/Livros/${id}`)
      return true
    } catch (error) {
      tratarErro(error)
    }
  }
}

export { api }