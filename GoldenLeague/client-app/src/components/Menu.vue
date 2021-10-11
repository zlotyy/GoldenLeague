<template>
  <v-app-bar app color="primary">
    <v-app-bar-nav-icon :to="{ name: 'Home' }">
      <v-icon>fas fa-futbol</v-icon>
    </v-app-bar-nav-icon>
    <v-toolbar-title :to="{ name: 'Home' }">Golden League</v-toolbar-title>
    <v-divider class="mx-4" vertical></v-divider>
    <v-btn text :to="{ name: 'Points' }">
      <v-icon>fas fa-poll</v-icon>
      <span class="ml-1">Punkty</span>
    </v-btn>
    <v-btn text :to="{ name: 'Ranking' }">
      <v-icon>fas fa-table</v-icon>
      <span class="ml-1">Ranking</span>
    </v-btn>
    <v-btn text :to="{ name: 'Info' }">
      <v-icon>fas fa-info-circle</v-icon>
      <span class="ml-1">Info</span>
    </v-btn>
  </v-app-bar>
</template>

<script>
export default {
  name: "Menu",
};
</script>
