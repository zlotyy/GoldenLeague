<template>
  <v-row>
    <v-col cols="4">
      <StandingsTable header="Ranking Typerów" :players="tipsterStandings" />
    </v-col>
    <v-col cols="4">
      <StandingsTable
        header="Ranking Golden League"
        :players="goldenLeagueStandings"
      />
    </v-col>
    <v-col cols="4">
      <StandingsTable
        header="Ranking Podsumowujący"
        :players="summaryStandings"
      />
    </v-col>
  </v-row>
</template>

<script>
import StandingsTable from "@/components/ranking/StandingsTable.vue";

export default {
  name: "Ranking",
  components: {
    StandingsTable,
  },
  data() {
    return {
      tipsterStandings: [],
      goldenLeagueStandings: [],
      summaryStandings: [],
    };
  },
  mounted() {
    this.tipsterStandings = [
      { id: 1, login: "zlotyy", points: 101 },
      { id: 2, login: "mrówa", points: 79 },
      { id: 3, login: "adi", points: 89 },
      { id: 4, login: "kamciasz", points: 78 },
      { id: 5, login: "kucyk", points: 65 },
    ].sort((x, y) => y.points - x.points);

    this.goldenLeagueStandings = [
      { id: 1, login: "zlotyy", points: 111 },
      { id: 2, login: "mrówa", points: 133 },
      { id: 3, login: "adi", points: 122 },
      { id: 4, login: "kamciasz", points: 134 },
      { id: 5, login: "kucyk", points: 146 },
    ].sort((x, y) => y.points - x.points);

    this.summaryStandings = this.tipsterStandings
      .map((x) => ({
        ...x,
        points:
          x.points +
          this.goldenLeagueStandings.find((y) => y.id === x.id).points,
      }))
      .sort((x, y) => y.points - x.points);
  },
};
</script>
