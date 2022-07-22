<template>
  <v-app-bar app color="primary">
    <v-app-bar-nav-icon :to="{ name: 'Home' }">
      <v-icon>fas fa-futbol</v-icon>
    </v-app-bar-nav-icon>
    <v-toolbar-title :to="{ name: 'Home' }" class="d-none d-md-flex">
      {{ toolbarTitle }}
    </v-toolbar-title>
    <v-divider class="mx-4" vertical></v-divider>
    <v-btn text v-if="isAuthorized()" :to="{ name: 'BookmakerLeagues' }">
      <v-icon>fas fa-table</v-icon>
      <span class="ml-1 d-none d-md-flex">{{
        $t("common.bookmakerLeagues")
      }}</span>
    </v-btn>
    <v-btn text v-if="isAuthorized()" :to="{ name: 'BookmakerBets' }">
      <v-icon>fas fa-hand-holding-usd</v-icon>
      <span class="ml-1 d-none d-md-flex">{{
        $t("common.bookmakerBets")
      }}</span>
    </v-btn>
    <v-btn text v-if="isAuthorized()" :to="{ name: 'Info' }">
      <v-icon>fas fa-info-circle</v-icon>
      <span class="ml-1 d-none d-md-flex">{{ $t("common.info") }}</span>
    </v-btn>
    <v-btn text v-if="!isAuthorized()" :to="{ name: 'Register' }">
      <v-icon>fas fa-user-plus</v-icon>
      <span class="ml-1 d-none d-md-flex">Rejestracja</span>
    </v-btn>
    <v-spacer></v-spacer>
    <v-btn text v-if="!isAuthorized()" :to="{ name: 'Login' }">
      <v-icon>fas fa-sign-in-alt</v-icon>
      <span class="ml-1">Zaloguj</span>
    </v-btn>
    <v-menu v-if="isAuthorized()" offset-y bottom rounded="lg">
      <template v-slot:activator="{ on, attrs }">
        <v-btn icon>
          <v-icon v-bind="attrs" v-on="on" large>fas fa-user-circle</v-icon>
        </v-btn>
      </template>
      <v-list>
        <v-list-item @click="ChangePassword()">
          <v-list-item-title>Zmień hasło</v-list-item-title>
        </v-list-item>
      </v-list>
      <v-list>
        <v-list-item @click="Logout()">
          <v-list-item-title>Wyloguj</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>
  </v-app-bar>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "TheMenu",
  computed: {
    toolbarTitle() {
      return this.isAuthorized() ? this.getLogin() : this.$t("common.appName");
    },
  },
  methods: {
    ...mapGetters("user", ["isAuthorized", "getLogin"]),
    ...mapActions("user", ["logout"]),
    async Logout() {
      await this.logout();

      this.$vToastify.customSuccess("Wylogowano z aplikacji");

      this.$router
        .push({
          name: "Login",
        })
        .catch(() => {});
    },
    ChangePassword() {
      this.$router
        .push({
          name: "ChangePassword",
        })
        .catch(() => {});
    },
  },
};
</script>

<style scoped>
.v-list {
  padding: 0;
}
</style>
