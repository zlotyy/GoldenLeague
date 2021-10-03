import "bootstrap/dist/css/bootstrap.css";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

createApp(App).use(router).mount("#app");

// import "bootstrap/dist/css/bootstrap.css";
// import App from "./App.vue";
// import Vue from "vue";
// import vuetify from "vuetify";
// import router from "./router";

// const requireComponent = require.context(
//   "@/components/base",
//   false,
//   /Base[A-Z]\w+\.(vue|js)$/
// );

// requireComponent.keys().forEach((fileName) => {
//   const componentConfig = requireComponent(fileName);
//   const componentName = fileName.replace(/^\.\/(.*)\.\w+$/, "$1");
//   Vue.component(componentName, componentConfig.default || componentConfig);
// });

// new Vue({
//   router,
//   store,
//   vuetify,
//   i18n,
//   render: (h) => h(App)
// }).$mount("#app");
