<template>
  <v-row>
    <v-col cols="12">
      <div class="text-h5 mb-5">{{ leagueName }}</div>
      <v-data-table
        :headers="headers"
        :items="items"
        :loading="loading"
        hide-default-footer
        disable-sort
        class="elevation-1"
        :items-per-page="-1"
        mobile-breakpoint="0"
      >
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import BookmakerService from "@/services/BookmakerService";

export default {
  name: "BookmakerLeagueView",
  props: {
    leagueId: {
      type: String,
    },
  },
  data() {
    return {
      headers: [
        { text: "Pozycja", value: "userRanking", width: "15%" },
        { text: "Gracz", value: "userLogin", width: "40%" },
        { text: "Punkty", value: "userPoints", width: "15%" },
        { text: "Trafione typy", value: "userCorrectBets", width: "15%" },
        { text: "Trafione wyniki", value: "userCorrectResults", width: "15%" },
      ],
      leagueName: "",
      items: [],
      loading: false,
    };
  },
  mounted() {
    this.SetLeagueData();
  },
  methods: {
    async SetLeagueData() {
      try {
        const response = await BookmakerService.GetLeagueRank(this.leagueId);

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          const data = response.data.data;
          this.leagueName = data.leagueName;
          this.items = data.users;
        } else if (response.status === 404) {
          this.$router
            .push({
              name: "BookmakerLeagues",
            })
            .catch(() => {});
        }
      } catch (err) {
        return;
      }
    },
  },
};
</script>
