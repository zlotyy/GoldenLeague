import axios from "axios";
import store from "../store";
import Vue from "vue";
// import router from '../router/index';
// import { compile } from "vue/types/umd";
// import { authToken } from "./auth-header";

// Full config:  https://github.com/axios/axios#request-config
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
// axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

axios.interceptors.request.use(
  (config) => {
    // Do something before request is sent

    // TODO - po zrobieniu logowania

    config.headers.common["Cache-Control"] = "no-cache";
    config.headers.get["If-Modified-Since"] = "0";
    // if (store.state.user.token) {
    //   config.headers.common.Authorization = `Bearer ${store.state.user.token}`;
    //   config.headers.common["Authorization"] = authToken();
    // }
    if (store.state.user.token) {
      config.headers.Authorization = `Bearer ${store.state.user.token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// let isRefreshing = false;
// let subscribers = [];
// function subscribeTokenRefresh(cb) {
//   subscribers.push(cb);
// }
// function onRrefreshed(token) {
//   subscribers.map((cb) => cb(token));
// }

// Add a response interceptor
axios.interceptors.response.use(
  (response) => {
    return Promise.resolve(response);
  },
  (error) => {
    // TODO - dokończyć
    // const {
    //   config,
    //   response: { status, headers },
    // } = error;
    // const originalRequest = config;
    // if (status === 401 && headers["token-expired"] === "true") {
    //   if (!isRefreshing) {
    //     isRefreshing = true;
    //     store.dispatch("user/updateToken").then(() => {
    //       onRrefreshed(store.state.user.token);
    //       isRefreshing = false;
    //       subscribers = [];
    //     });
    //   }
    //   const requestSubscribers = new Promise((resolve) => {
    //     subscribeTokenRefresh((token) => {
    //       originalRequest.headers.Authorization = `Bearer ${token}`;
    //       resolve(mAxios(originalRequest));
    //     });
    //   });
    //   return requestSubscribers;
    // }

    const { response } = error;

    if ((response.data || {}).errors[0]) {
      response.data.errors.forEach((err) => Vue.$vToastify.customError(err));
      return Promise.resolve(response);
    }

    if (response.status === 401) {
      Vue.$vToastify.unauthorizedError();
      return Promise.resolve(response);
    }

    if (response.status === 500) {
      Vue.$vToastify.unexpectedError();
      return Promise.resolve(response);
    }

    return Promise.reject(response);
  }
);

// /* eslint no-param-reassign: "error" */
// Plugin.install = (VueInstance) => {
//   VueInstance.axios = mAxios;
//   window.axios = mAxios;
//   Object.defineProperties(VueInstance.prototype, {
//     axios: {
//       get() {
//         return mAxios;
//       },
//     },
//     $axios: {
//       get() {
//         return mAxios;
//       },
//     },
//   });
// };

// Vue.use(Plugin);

export default axios;
