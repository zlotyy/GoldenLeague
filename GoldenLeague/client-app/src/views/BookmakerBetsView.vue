<template>
  <v-row>
    <v-col cols="12">
      <div class="text-h5 mb-5">Twoje typy</div>
      <div>
        <h3 v-if="!competitions[0] && !competitionsLoading">
          Dołącz do ligi typerów lub utwórz własną ligę, aby móc typować mecze
        </h3>
        <v-select
          v-else
          label="Rozgrywki"
          dense
          outlined
          multiple
          v-model="competitionsSelected"
          :items="competitions"
          item-text="competitionsFullName"
          item-value="competitionsId"
          return-object
          chips
          deletable-chips
          small-chips
          clearable
          :loading="competitionsLoading"
        ></v-select>
      </div>
      <MatchesBetTable
        v-for="comps in competitionsSelected"
        :key="comps.competitionsId"
        class="mb-10"
        :competitions="comps"
        :betRecords="GetBetRecords(comps.competitionsId)"
      />
    </v-col>
  </v-row>
</template>

<script>
import UserService from "@/services/UserService.js";
import MatchesBetTable from "@/components/bookmaker/MatchesBetTable.vue";
import { mapGetters, mapActions } from "vuex";
import dayjs from "@/plugins/dayjs.js";

export default {
  name: "BookmakerBetsView",
  components: {
    MatchesBetTable,
  },
  data() {
    return {
      competitions: [],
      competitionsSelected: [],
      bookmakerMatches: [],
      competitionsLoading: false,
    };
  },
  async mounted() {
    await this.setCompetitions();
    await this.$_setBookmakerBetsItems();
    this.competitions = await this.getCompetitions();
    this.competitionsSelected = this.competitions;
  },
  methods: {
    ...mapGetters("user", ["getUserId"]),
    ...mapActions("common", ["setCompetitions"]),
    GetBetRecords(competitionsId) {
      return this.bookmakerMatches
        .filter((x) => x.competitionsId === competitionsId)
        .map((x) => x.userBets)
        .flat();
    },
    async $_setBookmakerBetsItems() {
      // TODO async await
      UserService.GetBookmakerBets().then((response) => {
        const result = response.data;
        if (result.success) {
          this.bookmakerMatches = result.data.map((x) => {
            x.userBets.map((b) => {
              b.match = {
                ...b.match,
                matchDate: this.$_getMatchDate(b.match.matchDateTime),
                matchTime: this.$_getMatchTime(b.match.matchDateTime),
              };
            });
            return x;
          });
        }
      });
    },
    async getCompetitions() {
      try {
        this.competitionsLoading = true;
        const response = await UserService.GetBookmakerCompetitions();

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          return response.data.data;
        }
        return [];
      } catch (err) {
        return [];
      } finally {
        this.competitionsLoading = false;
      }
    },
    $_getMatchDate(dateTime) {
      return dayjs(dateTime).format("DD MMMM YYYY");
    },
    $_getMatchTime(dateTime) {
      return dayjs(dateTime).format("HH:mm");
    },
  },
};
</script>
