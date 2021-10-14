<template>
  <v-row>
    <v-col cols="4">
      <StandingsTable
        :header="$t('ranking.matchBettingRanking')"
        :players="matchBettingStandings"
      />
    </v-col>
    <v-col cols="4">
      <StandingsTable
        :header="$t('ranking.goldenLeagueRanking')"
        :players="goldenLeagueStandings"
      />
    </v-col>
    <v-col cols="4">
      <StandingsTable
        :header="$t('ranking.summaryRanking')"
        :players="summaryStandings"
      />
    </v-col>
  </v-row>
</template>

<script>
import StandingsTable from "@/components/ranking/StandingsTable.vue";

export default {
  name: "RankingView",
  components: {
    StandingsTable,
  },
  data() {
    return {
      matchBettingStandings: [],
      goldenLeagueStandings: [],
      summaryStandings: [],
    };
  },
  mounted() {
    this.matchBettingStandings = [
      { id: 1, login: "zlotyy", points: 101 },
      { id: 2, login: "mrówa", points: 79 },
      { id: 3, login: "adi", points: 89 },
      { id: 4, login: "kamciasz", points: 78 },
      { id: 5, login: "kucyk", points: 66 },
    ].sort((x, y) => y.points - x.points);

    this.goldenLeagueStandings = [
      { id: 1, login: "zlotyy", points: 111 },
      { id: 2, login: "mrówa", points: 133 },
      { id: 3, login: "adi", points: 123 },
      { id: 4, login: "kamciasz", points: 134 },
      { id: 5, login: "kucyk", points: 146 },
    ].sort((x, y) => y.points - x.points);

    this.summaryStandings = this.matchBettingStandings
      .map((x) => ({
        ...x,
        points:
          x.points +
          this.goldenLeagueStandings.find((y) => y.id === x.id).points,
      }))
      .sort((x, y) =>
        x.points === y.points
          ? x.login.localeCompare(y.login)
          : y.points - x.points
      );
  },
};
</script>
