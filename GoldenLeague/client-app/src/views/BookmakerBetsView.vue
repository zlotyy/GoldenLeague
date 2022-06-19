<template>
  <v-row>
    <v-col cols="12">
      <MatchesBetTable />
    </v-col>
  </v-row>
</template>

<script>
import MatchesBetTable from "@/components/bookmaker/MatchesBetTable.vue";

export default {
  name: "BookmakerBetsView",
  components: {
    MatchesBetTable,
  },
};
</script>
