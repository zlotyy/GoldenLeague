<template>
  <div>
    <v-card>
      <v-card-title>{{ $t("matchBetting.yourBets") }}</v-card-title>
      <v-data-table
        :headers="matchesTable.headers"
        :items="gameweekItems"
        :items-per-page="20"
        :loading="matchesTable.loading"
        hide-default-footer
        group-by="matchDate"
        group-desc
        sort-by="matchDateTime"
        sort-desc
        disable-sort
        :item-class="RowClass"
        class="elevation-1"
      >
        <template v-slot:[`top`]>
          <v-row justify="start">
            <v-col class="mx-3" cols="2">
              <v-select
                dense
                outlined
                :label="$t('common.gameweek')"
                :items="gameweeks"
                v-model="gameweekNo"
              ></v-select>
            </v-col>
          </v-row>
        </template>
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
                  hide-details
                  v-mask="'#'"
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
                  hide-details
                  v-mask="'#'"
                  v-model="item.matchResult.awayTeam.teamScoreBet"
                ></v-text-field>
              </div>
            </div>
          </div>
          <div v-else-if="BetEmpty(item)"><span> - </span></div>
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
              <v-btn
                class="primary my-5"
                outlined
                :loading="saveLoading"
                :disabled="saveLoading"
                @click="UpdateMatchBetting"
                >{{ $t("matchBetting.saveYourBets") }}</v-btn
              >
            </td>
          </tr>
        </template>
      </v-data-table>
    </v-card>
  </div>
</template>

<script>
import UserService from "@/services/UserService.js";
import MatchesService from "@/services/MatchService.js";
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
          { text: "Typ", value: "resultBet", align: "center", width: "13%" },
          {
            text: "Wynik",
            value: "resultActual",
            align: "center",
            width: "10%",
          },
        ],
        items: [],
        loading: false,
      },
      gameweekNo: 1,
      gameweeks: [],
      saveLoading: false,
    };
  },
  computed: {
    gameweekItems() {
      return this.matchesTable.items.filter(
        (x) => x.gameweekNo == this.gameweekNo
      );
    },
  },
  mounted() {
    this.$_setMatchBettingItems();
  },
  methods: {
    AllowBet(matchDateTime) {
      return dayjs(matchDateTime) > dayjs();
    },
    BetEmpty(item) {
      return (
        ((item.matchResult || {}).homeTeam || {}).teamScoreBet == null ||
        ((item.matchResult || {}).awayTeam || {}).teamScoreBet == null
      );
    },
    GetMatchDate(dateTime) {
      return dayjs(dateTime).format("DD MMMM YYYY");
    },
    GetMatchTime(dateTime) {
      return dayjs(dateTime).format("HH:mm");
    },
    RowClass() {
      // NOT WORKING COLOR
      return "betting-hit";
    },
    UpdateMatchBetting() {
      this.saveLoading = true;
      UserService.UpdateMatchBetting(this.gameweekItems).then((response) => {
        const result = response.data;
        if (result.success) {
          console.log("Updated");
        } else {
          console.log("Error - not updated");
        }
        this.$_setMatchBettingItems();
        this.saveLoading = false;
      });
    },
    $_setMatchBettingItems() {
      this.matchesTable.loading = true;
      UserService.GetMatchBetting().then((response) => {
        const result = response.data;
        if (result.success) {
          this.matchesTable.items = result.data.map((x) => {
            return {
              ...x,
              matchDate: this.GetMatchDate(x.matchDateTime),
              matchTime: this.GetMatchTime(x.matchDateTime),
            };
          });
          this.gameweeks = this.$_getGameweeks();
          this.$_setCurrentGameweek();
        }
        this.matchesTable.loading = false;
      });
    },
    $_getGameweeks() {
      return [
        ...new Set(this.matchesTable.items.map((x) => x.gameweekNo)),
      ].sort((a, b) => a - b);
    },
    $_setCurrentGameweek() {
      MatchesService.GetCurrentGameweek().then((response) => {
        const result = response.data;
        this.gameweekNo = result;
      });
    },
  },
};
</script>

<style scoped>
tr .betting-hit {
  background-color: chartreuse !important;
}
</style>
