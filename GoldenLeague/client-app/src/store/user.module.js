const moduleState = { id: null, login: "", token: "" };
const mutations = {
  SET_USER(state, { id, login, token }) {
    state.id = id;
    state.login = login;
    state.token = token;
  },
  UPDATE_TOKEN(state, token) {
    state.token = token;
  },
};
const getters = {
  isAuthorized: (state) => state.id && state.token,
  getLogin: (state) => state.login,
  getUserId: (state) => state.id,
};
const actions = {
  async setUser({ commit }, { id, login, token }) {
    commit("SET_USER", { id, login, token });
  },
  async resetUser({ commit }) {
    commit("SET_USER", {});
  },
  async logout({ commit }) {
    commit("SET_USER", {});
  },
  // TODO
  // updateToken({ commit, state }) {
  //   return LoginService.refreshToken(state.token).then(r => {
  //     const { token } = r.data;
  //     commit('UPDATE_TOKEN', token);
  //   });
  // }
};

export default {
  namespaced: true,
  state: moduleState,
  getters,
  mutations,
  actions,
};
