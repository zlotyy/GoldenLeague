<template>
  <div>
    <v-card class="flex">
      <v-card-title>{{ $t("common.currentGameweek") }}</v-card-title>
      <v-data-table
        :headers="headers"
        :items="items"
        :items-per-page="30"
        :loading="loading"
        hide-default-footer
        group-by="matchDate"
        group-desc
        sort-by="matchDateTime"
        sort-desc
        disable-sort
        class="elevation-1"
      >
        <template v-slot:[`group.header`]="{ items }">
          <th colspan="5">
            {{ items[0].matchDateHumanFriendly }}
          </th>
        </template>
        <template v-slot:[`item.matchTime`]="{ item }">
          {{ item.matchTime }}
        </template>
        <template v-slot:[`item.homeTeamName`]="{ item }">
          {{ item.matchResult.homeTeam.teamName }}
        </template>
        <template v-slot:[`item.awayTeamName`]="{ item }">
          {{ item.matchResult.awayTeam.teamName }}
        </template>
        <template v-slot:[`item.teamsSpacer`]> - </template>
        <template v-slot:[`item.result`]="{ item }">
          <span>
            {{ item.matchResult.homeTeam.teamScoreActual }} :
            {{ item.matchResult.awayTeam.teamScoreActual }}
          </span>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import MatchesService from "@/services/MatchService.js";
import dayjs from "@/plugins/dayjs.js";

export default {
  name: "GameweekTable",
  data() {
    return {
      headers: [
        { value: "matchTime" },
        { value: "homeTeamName", align: "end", width: "20%" },
        { value: "teamsSpacer", align: "center", width: "1%" },
        { value: "awayTeamName", align: "start", width: "20%" },
        {
          text: "Wynik",
          value: "result",
          align: "center",
          width: "10%",
        },
      ],
      items: [],
      loading: false,
    };
  },
  mounted() {
    this.$_setMatches();
  },
  methods: {
    $_setMatches() {
      // TODO - async await
      this.loading = true;
      MatchesService.GetCurrentGameweekMatches().then((response) => {
        const result = response.data;
        if (result.success) {
          this.items = result.data.map((x) => {
            return {
              ...x,
              matchDateHumanFriendly: this.$_getMatchDateHumanFriendly(
                x.matchDateTime
              ),
              matchDate: this.$_getMatchDate(x.matchDateTime),
              matchTime: this.$_getMatchTime(x.matchDateTime),
            };
          });
        }
        this.loading = false;
      });
    },
    $_getMatchDate(dateTime) {
      return dayjs(dateTime).format("YYYY-MM-DD");
    },
    $_getMatchDateHumanFriendly(dateTime) {
      return dayjs(dateTime).format("DD MMMM YYYY");
    },
    $_getMatchTime(dateTime) {
      return dayjs(dateTime).format("HH:mm");
    },
  },
};
</script>
