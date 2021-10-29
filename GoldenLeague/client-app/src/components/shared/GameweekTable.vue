<template>
  <div>
    <v-card class="flex">
      <v-card-subtitle
        >{{ $t("common.currentGameweek") }} - {{ gameweekNo }}</v-card-subtitle
      >
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
          {{ item.homeTeam.teamNameShort }}
        </template>
        <template v-slot:[`item.awayTeamName`]="{ item }">
          {{ item.awayTeam.teamNameShort }}
        </template>
        <template v-slot:[`item.teamsSpacer`]> - </template>
        <template v-slot:[`item.result`]="{ item }">
          <span>
            {{ item.homeTeamScore }} :
            {{ item.awayTeamScore }}
          </span>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import MatchService from "@/services/MatchService.js";
import dayjs from "@/plugins/dayjs.js";

export default {
  name: "GameweekTable",
  data() {
    return {
      headers: [
        { value: "matchTime" },
        { value: "homeTeamName", align: "end", width: "40%" },
        { value: "teamsSpacer", align: "center", width: "1%" },
        { value: "awayTeamName", align: "start", width: "40%" },
        {
          value: "result",
          align: "center",
          width: "10%",
        },
      ],
      items: [],
      loading: false,
      gameweekNo: null,
    };
  },
  mounted() {
    this.$_setCurrentGameweek();
    this.$_setMatches();
  },
  methods: {
    $_setMatches() {
      // TODO - async await
      this.loading = true;
      MatchService.GetCurrentGameweekMatches().then((response) => {
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
    $_setCurrentGameweek() {
      MatchService.GetCurrentGameweekNo().then((response) => {
        const result = response.data;
        this.gameweekNo = result;
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
