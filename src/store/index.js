import { createStore } from 'vuex';

const store = createStore({
  state: {
    user: null, 
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    logout(state) {
      state.user = null;
    }
  },
  actions: {
    login({ commit }, user) {

      commit('setUser', user);
    },
    logout({ commit }) {
      commit('logout');
    }
  },
  getters: {
    isAuthenticated(state) {
      return !!state.user;
    },
    isAdmin(state) {
      return state.user && state.user.role === 'admin';
    }
  }
});

export default store;
