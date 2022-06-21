import Vue from "vue";
import Vuex from "vuex";
import user from "./user.module";
import common from "./common.module";
import VuexPersistence from "vuex-persist";
import createMutationsSharer from "vuex-shared-mutations";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: { user, common },
  plugins: [
    new VuexPersistence().plugin, // dla trzymania store w localstorage
    createMutationsSharer({
      // dla przekazywania store pomiędzy tabami w przeglądarce
      predicate: [
        "user/SET_USER",
        "user/UPDATE_TOKEN",
        "common/SET_COMPETITIONS",
      ],
    }),
  ],
});
