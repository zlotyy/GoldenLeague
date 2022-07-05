<template>
  <div>
    <v-card>
      <v-row>
        <!-- TODO - dorobić filtry daty i kolejek - do wyboru -->
        <v-col class="d-flex flex-column" cols="6" sm="3" lg="2">
          <v-select
            dense
            outlined
            :label="$t('common.gameweek')"
            :items="gameweeks"
            v-model="gameweekNo"
          ></v-select>
        </v-col>
        <v-spacer></v-spacer>
        <div class="d-flex flex-column text-center pa-3">
          <v-chip label large>
            {{ competitions.competitionsName }}
          </v-chip>
        </div>
      </v-row>
      <v-divider class="mt-3"></v-divider>
      <v-data-table
        :headers="matchesTable.headers"
        :items="gameweekItems"
        :items-per-page="20"
        :loading="matchesTable.loading"
        hide-default-footer
        group-by="match.matchDate"
        group-desc
        sort-by="match.matchDateTime"
        sort-desc
        :item-class="RowClass()"
        class="elevation-1"
      >
        <template v-slot:[`group.header`]="{ items }">
          <th colspan="7">
            {{ items[0].match.matchDate }}
          </th>
        </template>
        <template v-slot:[`item.matchTime`]="{ item }">
          {{ item.match.matchTime }}
        </template>
        <template v-slot:[`item.homeTeamName`]="{ item }">
          {{ item.match.homeTeam.teamNameShort }}
        </template>
        <template v-slot:[`item.awayTeamName`]="{ item }">
          {{ item.match.awayTeam.teamNameShort }}
        </template>
        <template v-slot:[`item.teamsSpacer`]> - </template>
        <template v-slot:[`item.resultBet`]="{ item }">
          <div v-if="AllowBet(item.match.matchDateTime)">
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
                  v-model.number="item.matchResultBet.homeTeamScoreBet"
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
                  v-model.number="item.matchResultBet.awayTeamScoreBet"
                ></v-text-field>
              </div>
            </div>
          </div>
          <div v-else-if="BetEmpty(item)"><span> - </span></div>
          <div v-else>
            <span>
              {{ item.matchResultBet.homeTeamScoreBet }} :
              {{ item.matchResultBet.awayTeamScoreBet }}
            </span>
          </div>
        </template>
        <template v-slot:[`item.resultActual`]="{ item }">
          <span>
            {{ item.match.homeTeamScore }} :
            {{ item.match.awayTeamScore }}
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
                @click="SaveBookmakerBets"
                >{{ $t("bookmakerBets.saveYourBets") }}</v-btn
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
import dayjs from "@/plugins/dayjs.js";

export default {
  name: "MatchesBetTable",
  props: {
    competitions: {
      type: Object,
      require: true,
    },
    betRecords: {
      type: Array,
      default: () => [],
    },
  },
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
        loading: false,
      },
      gameweekNo: 1,
      saveLoading: false,
    };
  },
  computed: {
    gameweekItems() {
      return this.betRecords.filter(
        (x) => x.match.gameweekNo == this.gameweekNo
      );
    },
    gameweeks() {
      return [...new Set(this.betRecords.map((x) => x.match.gameweekNo))].sort(
        (a, b) => a - b
      );
    },
  },
  watch: {
    betRecords() {
      this.$_setCurrentGameweek();
    },
  },
  mounted() {
    // this.$_setCurrentGameweek();
  },
  methods: {
    AllowBet(matchDateTime) {
      // pozwól typować najpóźniej godzinę przed meczem
      return dayjs(matchDateTime) > dayjs().subtract(1, "hour");
    },
    BetEmpty(item) {
      return (
        (item.matchResultBet || {}).homeTeamScoreBet == null ||
        (item.matchResultBet || {}).awayTeamScoreBet == null
      );
    },
    RowClass() {
      // NOT WORKING COLOR
      return "betting-hit";
    },
    async SaveBookmakerBets() {
      try {
        this.saveLoading = true;
        const dto = this.$_getDto();
        if (!this.$_isValid(dto)) {
          return;
        }

        const response = await UserService.UpdateBookmakerBets(dto);
        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.$vToastify.customSuccess(
            `Zapisano typy dla rozgrywek: ${this.competitions.competitionsId}, kolejka: ${this.gameweekNo}`
          );
          this.$emit("bets-saved", this.competitions);
          // TODO przeładuj tabelę w parencie?
        }
      } catch (err) {
        //
      } finally {
        this.saveLoading = false;
      }
    },
    $_isValid() {
      // $_isValid(dto) {
      // const anyBetInserted = dto.some(
      //   (x) =>
      //     (x.matchResultBet.homeTeamScoreBet ||
      //       x.matchResultBet.homeTeamScoreBet == 0) &&
      //     (x.matchResultBet.awayTeamScoreBet ||
      //       x.matchResultBet.awayTeamScoreBet == 0)
      // );
      // if (!anyBetInserted) {
      //   this.$vToastify.validationError(
      //     "Aby zapisać zmiany musisz wprowadzić typ przynajmniej dla jednego meczu"
      //   );
      //   return false;
      // }

      return true;
    },
    $_setBookmakerBetsItems() {
      this.matchesTable.loading = true;
      // TODO async
      UserService.GetBookmakerBets().then((response) => {
        const result = response.data;
        if (result.success) {
          this.betRecords = result.data.map((x) => {
            x.match = {
              ...x.match,
              matchDate: this.$_getMatchDate(x.match.matchDateTime),
              matchTime: this.$_getMatchTime(x.match.matchDateTime),
            };
            return x;
          });
        }
        this.matchesTable.loading = false;
      });
    },
    $_getMatchDate(dateTime) {
      return dayjs(dateTime).format("DD MMMM YYYY");
    },
    $_getMatchTime(dateTime) {
      return dayjs(dateTime).format("HH:mm");
    },
    $_setCurrentGameweek() {
      const orderedRecords = (this.betRecords || [])
        .map((x) => x.match)
        .sort((a, b) => dayjs(a.matchDateTime) - dayjs(b.matchDateTime));

      const closestMatchToBet = orderedRecords.find(
        (x) =>
          dayjs(x.matchDateTime) >
          dayjs().set("hour", 23).set("minute", 59).set("second", 59)
      );

      if (closestMatchToBet) {
        this.gameweekNo = closestMatchToBet.gameweekNo;
      } else {
        this.gameweekNo = (
          orderedRecords[orderedRecords.length - 1] || {}
        ).gameweekNo;
      }
    },
    $_getDto() {
      return this.gameweekItems.map((x) => {
        if (x.matchResultBet.homeTeamScoreBet === "") {
          x.matchResultBet.homeTeamScoreBet = null;
        }
        if (x.matchResultBet.awayTeamScoreBet === "") {
          x.matchResultBet.awayTeamScoreBet = null;
        }
        return x;
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
