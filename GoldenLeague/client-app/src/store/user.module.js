import UserService from "@/services/UserService.js";

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
  async updateToken({ commit, state }) {
    try {
      const response = await UserService.RefreshToken(state.token);
      const token = response.data.data;
      commit("UPDATE_TOKEN", token);
      return Promise.resolve(response);
    } catch (error) {
      return Promise.reject(error);
    }
  },
};

export default {
  namespaced: true,
  state: moduleState,
  getters,
  mutations,
  actions,
};
