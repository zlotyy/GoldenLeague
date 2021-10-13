module.exports = {
  transpileDependencies: ["vuetify"],

  pluginOptions: {
    i18n: {
      locale: "pl",
      fallbackLocale: "en",
      localeDir: "locales",
      enableInSFC: true,
      enableBridge: false,
    },
  },
};
