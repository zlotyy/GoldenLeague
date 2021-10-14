<template>
  <div>
    <v-card>
      <v-card-title>{{ $t("matchBetting.yourBets") }}</v-card-title>
      <v-data-table
        :headers="matchesTable.headers"
        :items="matchesTable.items"
        :items-per-page="10"
        hide-default-footer
        group-by="matchDate"
        group-desc
        sort-by="matchDate"
        sort-desc
        disable-sort
        class="elevation-1"
      >
        <template v-slot:[`group.header`]="{ items }">
          <th colspan="6">
            {{ items[0].matchDate.toLocaleString() }}
          </th>
        </template>
        <template v-slot:[`item.matchDate`]="{ item }">
          {{ item.matchDate }}
        </template>
        <template v-slot:[`item.teamsSpacer`]> - </template>
        <template v-slot:[`item.betResult`]="{ item }">
          <div v-if="item.matchDate > new Date()">
            <div
              class="d-flex"
              style="align-items: baseline; justify-content: center"
            >
              <div class="flex">
                <v-text-field
                  dense
                  outlined
                  hide-details="true"
                  v-model="item.homeTeamGoalsBet"
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
                  v-model="item.awayTeamGoalsBet"
                ></v-text-field>
              </div>
            </div>
          </div>
          <div v-else>
            <span>
              {{ item.homeTeamGoalsBet }} : {{ item.awayTeamGoalsBet }}
            </span>
          </div>
        </template>
        <template v-slot:[`item.matchResult`]="{ item }">
          <span>
            {{ item.homeTeamGoalsResult }} : {{ item.awayTeamGoalsResult }}
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
export default {
  name: "MatchesBetTable",
  data() {
    return {
      matchesTable: {
        headers: [
          { value: "matchDate", width: "20%" },
          { value: "homeTeamName", align: "end", width: "20%" },
          { value: "teamsSpacer", align: "center", width: "1%" },
          { value: "awayTeamName", align: "start", width: "20%" },
          { text: "Typ", value: "betResult", align: "center", width: "12%" },
          {
            text: "Wynik",
            value: "matchResult",
            align: "center",
            width: "10%",
          },
        ],
        items: [],
      },
    };
  },
  mounted() {
    var tmpDate = new Date("2021-10-03 15:00");

    this.matchesTable.items = [
      {
        matchId: 1,
        matchDate: new Date("2021-10-02 13:30"),
        homeTeamId: 5,
        homeTeamName: "Manchester United",
        homeTeamGoalsBet: 2,
        homeTeamGoalsResult: 1,
        awayTeamId: 6,
        awayTeamName: "Everton",
        awayTeamGoalsBet: 1,
        awayTeamGoalsResult: 1,
      },
      {
        matchId: 2,
        matchDate: new Date("2021-10-18 21:00"),
        homeTeamId: 1,
        homeTeamName: "Arsenal",
        homeTeamGoalsBet: null,
        homeTeamGoalsResult: null,
        awayTeamId: 2,
        awayTeamName: "Crystal Palace",
        awayTeamGoalsBet: null,
        awayTeamGoalsResult: null,
      },
      {
        matchId: 3,
        matchDate: new Date("2021-10-16 13:30"),
        homeTeamId: 3,
        homeTeamName: "Watford",
        homeTeamGoalsBet: 0,
        homeTeamGoalsResult: null,
        awayTeamId: 4,
        awayTeamName: "Liverpool",
        awayTeamGoalsBet: 2,
        awayTeamGoalsResult: null,
      },

      {
        matchId: 4,
        matchDate: tmpDate,
        homeTeamId: 5,
        homeTeamName: "Crystal Palace",
        homeTeamGoalsBet: 0,
        homeTeamGoalsResult: 2,
        awayTeamId: 6,
        awayTeamName: "Leicester",
        awayTeamGoalsBet: 1,
        awayTeamGoalsResult: 2,
      },
      {
        matchId: 5,
        matchDate: tmpDate,
        homeTeamId: 5,
        homeTeamName: "Tottenham",
        homeTeamGoalsBet: 1,
        homeTeamGoalsResult: 2,
        awayTeamId: 6,
        awayTeamName: "Aston Villa",
        awayTeamGoalsBet: 1,
        awayTeamGoalsResult: 1,
      },
      {
        matchId: 6,
        matchDate: tmpDate,
        homeTeamId: 5,
        homeTeamName: "West Ham",
        homeTeamGoalsBet: 1,
        homeTeamGoalsResult: 1,
        awayTeamId: 6,
        awayTeamName: "Brentford",
        awayTeamGoalsBet: 0,
        awayTeamGoalsResult: 2,
      },
      {
        matchId: 7,
        matchDate: new Date("2021-10-03 15:00"),
        homeTeamId: 5,
        homeTeamName: "Liverpool",
        homeTeamGoalsBet: 1,
        homeTeamGoalsResult: 2,
        awayTeamId: 6,
        awayTeamName: "Manchester City",
        awayTeamGoalsBet: 1,
        awayTeamGoalsResult: 2,
      },
      {
        matchId: 8,
        matchDate: new Date("2021-10-16 16:00"),
        homeTeamId: 5,
        homeTeamName: "Manchester City",
        homeTeamGoalsBet: 3,
        homeTeamGoalsResult: null,
        awayTeamId: 6,
        awayTeamName: "Burnley",
        awayTeamGoalsBet: 0,
        awayTeamGoalsResult: null,
      },
      {
        matchId: 9,
        matchDate: new Date("2021-10-16 16:00"),
        homeTeamId: 5,
        homeTeamName: "Arsenal",
        homeTeamGoalsBet: 1,
        homeTeamGoalsResult: null,
        awayTeamId: 6,
        awayTeamName: "Manchester United",
        awayTeamGoalsBet: 1,
        awayTeamGoalsResult: null,
      },
    ].sort((x, y) => y.matchDate - x.matchDate);
  },
};
</script>
