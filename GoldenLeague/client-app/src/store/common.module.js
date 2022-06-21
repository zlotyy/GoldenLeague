import CommonService from "@/services/CommonService.js";

const moduleState = {
  competitions: [],
};

const mutations = {
  SET_COMPETITIONS(state, competitions) {
    state.competitions = competitions;
  },
};

const getters = {
  getCompetitions: (state) => state.competitions,
};

const actions = {
  async setCompetitions({ commit, state }) {
    if (!state.competitions.length) {
      try {
        const response = await CommonService.GetCompetitions();
        if (response.status === 200 && !(response.data || {}).errors[0]) {
          const competitions = response.data.data;
          commit("SET_COMPETITIONS", competitions);
        } else {
          this.$vToastify.customError("Nie udało się pobrać rozgrywek");
        }
      } catch (err) {
        this.$vToastify.customError("Nie udało się pobrać rozgrywek");
      }
    }
  },
  async resetCompetitions({ commit }) {
    commit("SET_COMPETITIONS", []);
  },
};

export default {
  namespaced: true,
  state: moduleState,
  getters,
  mutations,
  actions,
};
