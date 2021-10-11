import Vue from "vue";
import Vuetify from "vuetify/lib/framework";
import pl from "vuetify/lib/locale/pl";
import "@fortawesome/fontawesome-free/css/all.css";
import "roboto-fontface/css/roboto/roboto-fontface.css";

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    options: {
      customProperties: true,
    },
    themes: {
      light: {
        primary: "#FFC107",
        secondary: "#424242",
        accent: "#82B1FF",
        error: "#FF5252",
        info: "#2196F3",
        success: "#4CAF50",
        warning: "#FFC107",
      },
    },
  },
  lang: {
    locales: { pl },
    current: "pl",
  },
  icons: {
    iconfont: "fa",
  },
});
