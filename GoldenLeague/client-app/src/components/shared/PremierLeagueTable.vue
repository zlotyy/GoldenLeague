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
        height="400"
        :items-per-page="-1"
        mobile-breakpoint="0"
      >
        <template v-slot:item.standing="{ item }">
          {{ $_teamStanding(item) }}
        </template>
        <template v-slot:item.goalDifference="{ item }">
          {{ $_goalDifference(item) }}
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import TeamService from "@/services/TeamService.js";

export default {
  name: "PremierLeagueTable",
  data() {
    return {
      headers: [
        { value: "standing", width: "5%" },
        { value: "teamName", width: "40%" },
        { text: "M", value: "matchesPlayed", width: "10%" },
        { text: "PKT", value: "points", width: "10%" },
        { text: "+/-", value: "goalDifference", width: "10%" },
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
      TeamService.GetTeamsRanking().then((response) => {
        const result = response.data;
        if (result.success) {
          this.items = result.data;
        }
        this.loading = false;
      });
    },
    $_teamStanding(item) {
      return this.items.indexOf(item) + 1;
    },
    $_goalDifference(item) {
      return item.goalsScored - item.goalsConceded;
    },
  },
};
</script>
