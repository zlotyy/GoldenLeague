<template>
  <v-row>
    <v-col cols="12">
      <div class="text-h5 mb-5">Ligi typerów</div>
      <v-card>
        <v-row>
          <LeagueCreateDialog
            @league-created="SetLeaguesData()"
          ></LeagueCreateDialog>
          <LeagueJoinDialog
            @league-joined="SetLeaguesData()"
          ></LeagueJoinDialog>
        </v-row>
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
          @click:row="ShowLeagueRank"
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
          @click:row="ShowLeagueRank"
        >
          <template v-slot:[`item.options`]="{ item }">
            <v-menu offset-y bottom rounded="lg">
              <template v-slot:activator="{ on, attrs }">
                <v-btn icon>
                  <v-icon v-bind="attrs" v-on="on">fas fa-ellipsis-v</v-icon>
                </v-btn>
              </template>
              <v-list>
                <v-list-item @click="CopyLeagueCode(item.leagueId)">
                  <v-list-item-title>Skopiuj kod ligi</v-list-item-title>
                </v-list-item>
                <base-confirm
                  :message="
                    'Czy jesteś pewien że chcesz opuścić ligę ' +
                    item.leagueName +
                    '?'
                  "
                  @confirm="LeaveLeague(item.leagueId)"
                >
                  <template v-slot:default="{ on }">
                    <v-list-item @click="() => {}">
                      <v-list-item-title v-on="on">
                        Opuść ligę
                      </v-list-item-title>
                    </v-list-item>
                  </template>
                </base-confirm>
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
import BaseConfirm from "@/components/base/BaseConfirm.vue";
import UserService from "@/services/UserService";

export default {
  name: "BookmakerLeaguesView",
  components: { LeagueCreateDialog, LeagueJoinDialog, BaseConfirm },
  data() {
    return {
      headers: [
        { text: "Liga", value: "leagueName", width: "50%" },
        { text: "Pozycja", value: "entryRank", width: "40%" },
        { value: "options", width: "10%" },
      ],
      leagues: [],
      loading: false,
      selectedItem: undefined,
      showDeleteConfirm: false,
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
    this.SetLeaguesData();
  },
  methods: {
    async SetLeaguesData() {
      try {
        const response = await UserService.GetBookmakerLeaguesJoined();

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.leagues = response.data.data;
        }
      } catch (err) {
        return;
      }
    },
    async CopyLeagueCode(code) {
      await window.navigator.clipboard.writeText(code);
      this.$vToastify.customSuccess("Kod ligi został skopiowany");
    },
    async LeaveLeague(leagueId) {
      try {
        const response = await UserService.BookmakerLeagueLeave(leagueId);

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.$vToastify.customSuccess("Opuściłeś ligę");
          this.SetLeaguesData();
        }
      } catch (err) {
        return;
      }
    },
    async ShowLeagueRank(row) {
      this.$router.push({
        name: "BookmakerLeague",
        params: { leagueId: row.leagueId },
      });
    },
  },
};
</script>
