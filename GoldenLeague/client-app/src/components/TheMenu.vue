<template>
  <v-app-bar app color="primary">
    <v-app-bar-nav-icon :to="{ name: 'Home' }">
      <v-icon>fas fa-futbol</v-icon>
    </v-app-bar-nav-icon>
    <v-toolbar-title :to="{ name: 'Home' }">{{
      $t("common.appName")
    }}</v-toolbar-title>
    <v-divider class="mx-4" vertical></v-divider>
    <v-btn text :to="{ name: 'Ranking' }">
      <v-icon>fas fa-table</v-icon>
      <span class="ml-1">{{ $t("common.ranking") }}</span>
    </v-btn>
    <v-btn text :to="{ name: 'MySquad' }">
      <v-icon>fas fa-running</v-icon>
      <span class="ml-1">{{ $t("common.squad") }}</span>
    </v-btn>
    <v-btn text :to="{ name: 'MatchBetting' }">
      <v-icon>fas fa-hand-holding-usd</v-icon>
      <span class="ml-1">{{ $t("common.matchBetting") }}</span>
    </v-btn>
    <v-btn text :to="{ name: 'Info' }">
      <v-icon>fas fa-info-circle</v-icon>
      <span class="ml-1">{{ $t("common.info") }}</span>
    </v-btn>
    <v-btn text @click="TestApi()">
      <v-icon>fas fa-info-circle</v-icon>
      <span class="ml-1">API Test</span>
    </v-btn>
  </v-app-bar>
</template>

<script>
import CommonService from "@/services/CommonService.js";

export default {
  name: "TheMenu",
  methods: {
    TestApi() {
      CommonService.ApiTest().then((response) => {
        const result = (response.data || {}).data;
        console.log("API TEST: " + result);
      });
    },
  },
};
</script>
