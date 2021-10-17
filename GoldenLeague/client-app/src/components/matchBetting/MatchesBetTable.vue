<template>
  <div>
    <v-card>
      <v-card-title>{{ $t("matchBetting.yourBets") }}</v-card-title>
      <v-data-table
        :headers="matchesTable.headers"
        :items="matchesTable.items"
        :items-per-page="20"
        hide-default-footer
        group-by="matchDate"
        group-desc
        sort-by="matchDateTime"
        sort-desc
        disable-sort
        class="elevation-1"
      >
        <template v-slot:[`group.header`]="{ items }">
          <th colspan="7">
            {{ items[0].matchDate }}
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
        <template v-slot:[`item.resultBet`]="{ item }">
          <div v-if="AllowBet(item.matchDateTime)">
            <div
              class="d-flex"
              style="align-items: baseline; justify-content: center"
            >
              <div class="flex">
                <v-text-field
                  dense
                  outlined
                  hide-details="true"
                  v-model="item.matchResult.homeTeam.teamScoreBet"
                ></v-text-field>
              </div>
              <div class="flex mx-2">
                <span>:</span>
              </div>
              <div class="flex">
                <v-text-field
                  dense
                  outlined
                  hide-details="true"
                  v-model="item.matchResult.awayTeam.teamScoreBet"
                ></v-text-field>
              </div>
            </div>
          </div>
          <div v-else>
            <span>
              {{ item.matchResult.homeTeam.teamScoreBet }} :
              {{ item.matchResult.awayTeam.teamScoreBet }}
            </span>
          </div>
        </template>
        <template v-slot:[`item.resultActual`]="{ item }">
          <span>
            {{ item.matchResult.homeTeam.teamScoreActual }} :
            {{ item.matchResult.awayTeam.teamScoreActual }}
          </span>
        </template>
        <template v-slot:[`body.append`]>
          <tr>
            <td colspan="12" class="text-right">
              <v-btn class="primary my-5" outlined>Zapisz typy</v-btn>
            </td>
          </tr>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import UserService from "@/services/UserService.js";
import dayjs from "@/plugins/dayjs.js";

export default {
  name: "MatchesBetTable",
  data() {
    return {
      matchesTable: {
        headers: [
          { value: "matchTime" },
          { value: "homeTeamName", align: "end", width: "20%" },
          { value: "teamsSpacer", align: "center", width: "1%" },
          { value: "awayTeamName", align: "start", width: "20%" },
          { text: "Typ", value: "resultBet", align: "center", width: "12%" },
          {
            text: "Wynik",
            value: "resultActual",
            align: "center",
            width: "10%",
          },
        ],
        items: [],
      },
    };
  },
  mounted() {
    const userId = "35CE5DF3-CBCD-4A43-9582-A51CBEC26B91";
    debugger;
    UserService.GetMatchBetting(userId).then((response) => {
      const result = response.data;
      if (result.success) {
        this.matchesTable.items = result.data.map((x) => {
          return {
            ...x,
            matchDate: this.GetMatchDate(x.matchDateTime),
            matchTime: this.GetMatchTime(x.matchDateTime),
          };
        });
      }
    });
  },
  methods: {
    AllowBet(matchDateTime) {
      return dayjs(matchDateTime) > dayjs();
    },
    GetMatchDate(dateTime) {
      return dayjs(dateTime).format("DD MMMM YYYY");
    },
    GetMatchTime(dateTime) {
      return dayjs(dateTime).format("HH:mm");
    },
  },
};
</script>
