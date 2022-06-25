import VueToastify from "vue-toastify";
import Vue from "vue";

Vue.use(VueToastify, {
  customNotifications: {
    unauthorizedError: {
      defaultTitle: false,
      body: "Aby móc korzystać z aplikacji, musisz się zalogować",
      type: "error",
      canTimeout: true,
      duration: "4000",
    },
    authorizationError: {
      defaultTitle: false,
      body: "Wystąpił błąd podczas autoryzacji, zaloguj się ponownie",
      type: "error",
      canTimeout: true,
      duration: "4000",
    },
    unexpectedError: {
      defaultTitle: false,
      body: "Wystąpił nieoczekiwany błąd",
      type: "error",
      canTimeout: true,
      duration: "4000",
    },
    validationError: {
      defaultTitle: false,
      body: "Wystąpił błąd walidacji, uzupełnij poprawnie wszystkie wymagane pola",
      type: "warning",
      canTimeout: true,
      duration: "4000",
    },
    customInfo: {
      defaultTitle: false,
      body: "",
      type: "info",
      canTimeout: true,
      duration: "4000",
    },
    customWarning: {
      defaultTitle: false,
      body: "",
      type: "warning",
      canTimeout: true,
      duration: "4000",
    },
    customSuccess: {
      defaultTitle: false,
      body: "",
      type: "success",
      canTimeout: true,
      duration: "4000",
    },
    customError: {
      defaultTitle: false,
      body: "",
      type: "error",
      canTimeout: true,
      duration: "4000",
    },
  },
  position: "top-right",
});
