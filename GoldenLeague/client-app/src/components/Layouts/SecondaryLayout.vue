<template>
  <v-card class="d-flex flex-column flex grey lighten-5" outlined>
    <GameweekTable class="d-flex flex-column flex" />
    <PremierLeagueTable class="d-flex flex-column mt-2 flex" />
  </v-card>
</template>

<script>
import GameweekTable from "@/components/shared/GameweekTable.vue";
import PremierLeagueTable from "@/components/shared/PremierLeagueTable.vue";

export default {
  name: "SecondaryLayout",
  components: {
    GameweekTable,
    PremierLeagueTable,
  },
};
</script>
