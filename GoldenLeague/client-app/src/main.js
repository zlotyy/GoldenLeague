import Vue from "vue";
// import "./plugins/axios";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import "roboto-fontface/css/roboto/roboto-fontface.css";
import "./assets/css/main.css";
import i18n from "./i18n";
import { VueMaskDirective } from "v-mask";

Vue.directive("mask", VueMaskDirective);
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  i18n,
  render: (h) => h(App),
}).$mount("#app");
