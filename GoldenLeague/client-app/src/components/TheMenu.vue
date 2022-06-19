<template>
  <v-app-bar app color="primary">
    <v-app-bar-nav-icon :to="{ name: 'Home' }">
      <v-icon>fas fa-futbol</v-icon>
    </v-app-bar-nav-icon>
    <v-toolbar-title :to="{ name: 'Home' }">{{
      $t("common.appName")
    }}</v-toolbar-title>
    <v-divider class="mx-4" vertical></v-divider>
    <v-btn text v-if="isAuthorized()" :to="{ name: 'Ranking' }">
      <v-icon>fas fa-table</v-icon>
      <span class="ml-1">{{ $t("common.ranking") }}</span>
    </v-btn>
    <!-- <v-btn text :to="{ name: 'MySquad' }">
      <v-icon>fas fa-running</v-icon>
      <span class="ml-1">{{ $t("common.squad") }}</span>
    </v-btn> -->
    <v-btn text v-if="isAuthorized()" :to="{ name: 'BookmakerBets' }">
      <v-icon>fas fa-hand-holding-usd</v-icon>
      <span class="ml-1">{{ $t("common.bookmakerBets") }}</span>
    </v-btn>
    <v-btn text v-if="isAuthorized()" :to="{ name: 'Info' }">
      <v-icon>fas fa-info-circle</v-icon>
      <span class="ml-1">{{ $t("common.info") }}</span>
    </v-btn>
    <v-btn text v-if="!isAuthorized()" :to="{ name: 'Register' }">
      <v-icon>fas fa-user-plus</v-icon>
      <span class="ml-1">Rejestracja</span>
    </v-btn>
    <v-spacer></v-spacer>
    <v-btn text v-if="!isAuthorized()" :to="{ name: 'Login' }">
      <v-icon>fas fa-sign-in-alt</v-icon>
      <span class="ml-1">Zaloguj</span>
    </v-btn>
    <v-btn text v-if="isAuthorized()" @click="Logout()">
      <v-icon>fas fa-sign-out-alt</v-icon>
      <span class="ml-1">Wyloguj</span>
    </v-btn>
  </v-app-bar>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "TheMenu",
  methods: {
    ...mapGetters("user", ["isAuthorized"]),
    ...mapActions("user", ["logout"]),
    async Logout() {
      await this.logout();

      this.$vToastify.customSuccess("Wylogowano z aplikacji");

      this.$router.push({
        name: "Login",
      });
    },
  },
};
</script>
