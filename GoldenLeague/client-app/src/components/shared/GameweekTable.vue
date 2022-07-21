<template>
  <div>
    <v-card class="flex">
      <v-card-subtitle>Zbliżające się mecze</v-card-subtitle>
      <v-data-table
        :headers="headers"
        :items="items"
        :items-per-page="30"
        :loading="loading"
        hide-default-footer
        group-by="matchDate"
        disable-sort
        height="400"
        class="elevation-1"
        mobile-breakpoint="0"
      >
        <template v-slot:[`group.header`]="{ items }">
          <th colspan="5">
            {{ items[0].matchDateHumanFriendly }}
          </th>
        </template>
        <template v-slot:[`item.matchTime`]="{ item }">
          {{ item.matchTime }}
        </template>
        <template v-slot:[`item.teamsSpacer`]> - </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import UserService from "@/services/UserService.js";
import dayjs from "@/plugins/dayjs.js";

export default {
  name: "GameweekTable",
  data() {
    return {
      headers: [
        { value: "matchTime", width: "5%" },
        { value: "homeTeamName", align: "end", width: "37%" },
        { value: "teamsSpacer", align: "center", width: "1%" },
        { value: "awayTeamName", align: "start", width: "37%" },
      ],
      items: [],
      loading: false,
    };
  },
  async mounted() {
    await this.$_setMatches();
  },
  methods: {
    async $_setMatches() {
      this.loading = true;
      try {
        const response = await UserService.GetBookmakerIncomingMatches();

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.items = response.data.data
            .map((x) => {
              return {
                ...x,
                matchDateHumanFriendly: this.$_getMatchDateHumanFriendly(
                  x.matchDateTime
                ),
                matchDate: this.$_getMatchDate(x.matchDateTime),
                matchTime: this.$_getMatchTime(x.matchDateTime),
              };
            })
            .sort((a, b) => dayjs(a.matchDateTime) - dayjs(b.matchDateTime));
        } else {
          this.items = [];
        }
      } catch (err) {
        this.items = [];
        return;
      } finally {
        this.loading = false;
      }
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
