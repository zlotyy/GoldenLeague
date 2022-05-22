<template>
  <div>
    <v-card class="flex">
      <v-card-subtitle>{{ $t("common.premierLeagueTable") }}</v-card-subtitle>
      <v-data-table
        :headers="headers"
        :items="items"
        :loading="loading"
        hide-default-footer
        disable-sort
        class="elevation-1"
      >
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import MatchService from "@/services/MatchService.js";

export default {
  name: "PremierLeagueTable",
  data() {
    return {
      headers: [
        { value: "teamName", width: "50%" },
        { value: "matchesPlayed", width: "25%" },
        { value: "points", width: "25%" },
      ],
      items: [],
      loading: false,
    };
  },
  mounted() {
    this.$_setTableData();
  },
  methods: {
    $_setTableData() {
      // TODO - async await
      this.loading = true;
      MatchService.GetCurrentGameweekMatches().then((response) => {
        const result = response.data;
        if (result.success) {
          this.items = [{ teamName: "Arsenal", matchesPlayed: 20, points: 35 }];
        }
        this.loading = false;
      });
    },
  },
};
</script>
