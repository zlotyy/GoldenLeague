<template>
  <v-row>
    <v-col cols="12">
      <div class="text-h5 mb-5">Ligi typerów</div>
      <v-card>
        <v-row>
          <LeagueCreateDialog></LeagueCreateDialog>
          <LeagueJoinDialog></LeagueJoinDialog>
        </v-row>
        <!-- <v-divider class="mt-3"></v-divider> -->
        <v-card-title class="mt-5">Liga globalna</v-card-title>
        <v-data-table
          :headers="headers"
          :items="globalLeague"
          :loading="loading"
          hide-default-footer
          disable-sort
          class="elevation-1"
          :items-per-page="-1"
          mobile-breakpoint="0"
        >
        </v-data-table>
        <v-card-title class="mt-5">Ligi prywatne</v-card-title>
        <v-data-table
          :headers="headers"
          :items="privateLeagues"
          :loading="loading"
          hide-default-footer
          disable-sort
          class="elevation-1"
          :items-per-page="-1"
          mobile-breakpoint="0"
        >
          <template v-slot:item.options="{}">
            <v-menu offset-y rounded="lg">
              <template v-slot:activator="{ on, attrs }">
                <v-icon v-bind="attrs" v-on="on">fas fa-ellipsis-v</v-icon>
              </template>
              <v-list>
                <v-list-item>
                  <v-list-item-title>Skopiuj kod ligi</v-list-item-title>
                </v-list-item>
                <v-list-item>
                  <v-list-item-title>Opuść ligę</v-list-item-title>
                </v-list-item>
              </v-list>
            </v-menu>
          </template>
        </v-data-table>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import LeagueCreateDialog from "@/components/bookmaker/LeagueCreateDialog.vue";
import LeagueJoinDialog from "@/components/bookmaker/LeagueJoinDialog.vue";
import UserService from "@/services/UserService";
import { mapGetters } from "vuex";

export default {
  name: "BookmakerLeaguesView",
  components: { LeagueCreateDialog, LeagueJoinDialog },
  data() {
    return {
      headers: [
        { text: "Liga", value: "leagueName", width: "50%" },
        { text: "Pozycja", value: "ranking", width: "40%" },
        { value: "options", width: "10%" },
      ],
      leagues: [],
      loading: false,
    };
  },
  computed: {
    privateLeagues() {
      return this.leagues.filter((x) => x.isPrivate);
    },
    globalLeague() {
      return [this.leagues.find((x) => !x.isPrivate)];
    },
  },
  mounted() {
    this.$_setLeaguesData();
  },
  methods: {
    ...mapGetters("user", ["getUserId"]),
    async $_setLeaguesData() {
      try {
        const response = await UserService.GetBookmakerLeaguesJoined(
          this.getUserId()
        );
        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.leagues = response.data.data;
        } else {
          this.$vToastify.customError("Nie udało się pobrać lig użytkownika");
        }
      } catch (err) {
        this.$vToastify.customError("Nie udało się pobrać lig użytkownika");
      }
    },
  },
};
</script>
