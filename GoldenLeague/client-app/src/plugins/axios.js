import axios from "axios";
import store from "@/store";
import Vue from "vue";
import router from "@/router/index";

// Full config:  https://github.com/axios/axios#request-config
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';

axios.interceptors.request.use(
  (config) => {
    config.headers.common["Cache-Control"] = "no-cache";
    config.headers.get["If-Modified-Since"] = "0";

    if (store.state.user.token) {
      config.headers.Authorization = `Bearer ${store.state.user.token}`;
    }

    return config;
  },
  (error) => Promise.reject(error)
);

let isRefreshing = false;
let subscribers = [];

axios.interceptors.response.use(
  (response) => {
    return Promise.resolve(response);
  },
  (error) => {
    const {
      config,
      response,
      response: { status, headers },
    } = error;

    const originalRequest = config;

    if (status === 403) {
      Vue.$vToastify.authorizationError();
      store.dispatch("user/resetUser");
      router.push("/").catch(() => {});
      return;
    }
    if (status === 401 && !headers["token-expired"]) {
      if (((response.data || {}).errors || [])[0]) {
        response.data.errors.forEach((err) => Vue.$vToastify.customError(err));
        return Promise.resolve(response);
      }
      // Vue.$vToastify.authorizationError();
      store.dispatch("user/resetUser");
      router.push("/").catch(() => {});
      return;
    }
    if (status === 401 && headers["token-expired"] === "true") {
      if (!isRefreshing) {
        isRefreshing = true;
        store
          .dispatch("user/updateToken")
          .then(() => {
            onRrefreshed(store.state.user.token);
            isRefreshing = false;
            subscribers = [];
          })
          .catch(() => {
            return;
          });
      }
      const requestSubscribers = new Promise((resolve) => {
        subscribeTokenRefresh((token) => {
          originalRequest.headers.Authorization = `Bearer ${token}`;
          resolve(axios(originalRequest));
        });
      });
      return requestSubscribers;
    }

    if (((response.data || {}).errors || [])[0]) {
      response.data.errors.forEach((err) => Vue.$vToastify.customError(err));
      return Promise.resolve(response);
    }

    if (status === 401) {
      Vue.$vToastify.unauthorizedError();
      return Promise.resolve(response);
    }

    if (status === 404) {
      Vue.$vToastify.unexpectedError();
      return Promise.resolve(response);
    }

    if (status === 500) {
      Vue.$vToastify.unexpectedError();
      return Promise.resolve(response);
    }

    return Promise.reject(response);
  }
);

function subscribeTokenRefresh(cb) {
  subscribers.push(cb);
}

function onRrefreshed(token) {
  subscribers.map((cb) => cb(token));
}

export default axios;
