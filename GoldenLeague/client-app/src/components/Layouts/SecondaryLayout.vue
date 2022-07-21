<template>
  <v-card class="d-flex flex-column flex grey lighten-5" outlined>
    <GameweekTable class="d-flex flex-column flex" />
  </v-card>
</template>

<script>
import GameweekTable from "@/components/shared/GameweekTable.vue";

export default {
  name: "SecondaryLayout",
  components: {
    GameweekTable,
  },
};
</script>
