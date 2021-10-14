<template>
  <v-row>
    <v-col cols="12">
      <MatchesBetTable />
    </v-col>
  </v-row>
</template>

<script>
import MatchesBetTable from "@/components/matchBetting/MatchesBetTable.vue";

export default {
  name: "MatchBettingView",
  components: {
    MatchesBetTable,
  },
};
</script>
