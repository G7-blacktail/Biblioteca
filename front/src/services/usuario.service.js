import axios from 'axios'
import environment from '../../environments/environments'

const api = axios.create({
  baseURL: environment.apiBaseUrl
})

function tratarErro(error) {
  if (error.response) {
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
  async getUsuarios() {
    try {
      const response = await api.get('/Usuarios')
      return response.data
    } catch (error) {
      tratarErro(error)
    }
  },
  async getUsuario(id) {
    try {
        const response = await api.get(`/Usuarios/${id}`)
        return response.data
    } catch (error) {
        tratarErro(error)
    }
    },
      async criarUsuario(usuario) {
    try {
      const response = await api.post('/Usuarios', usuario)
      return response.data
    } catch (error) {
      tratarErro(error)
    }
  },
  async deletarUsuario(id) {
    try {
      await api.delete(`/Usuarios/${id}`)
      return true
    } catch (error) {
      tratarErro(error)
    }
  }
}

export { api }