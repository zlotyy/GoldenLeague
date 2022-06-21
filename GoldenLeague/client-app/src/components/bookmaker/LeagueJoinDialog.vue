<template>
  <v-dialog v-model="dialog" persistent max-width="600px">
    <template v-slot:activator="{ on, attrs }">
      <v-col class="d-flex flex-column" cols="12" md="6">
        <v-btn class="primary" outlined v-bind="attrs" v-on="on">
          Dołącz do ligi
        </v-btn>
      </v-col>
    </template>
    <v-card>
      <v-card-title>Dołącz do prywatnej ligi</v-card-title>
      <v-card-text>
        <v-text-field label="Kod ligi" v-model="leagueCode"> </v-text-field>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="primary" outlined @click="dialog = false"> Anuluj </v-btn>
        <v-btn class="primary" outlined @click="SubmitLeagueJoin()">
          Dołącz
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import UserService from "@/services/UserService";
import { mapGetters } from "vuex";

export default {
  name: "LeagueJoinDialog",
  data: () => ({
    dialog: false,
    leagueCode: "",
  }),
  methods: {
    ...mapGetters("user", ["getUserId"]),
    async SubmitLeagueJoin() {
      try {
        // TODO walidacja

        const response = await UserService.BookmakerLeagueJoin(
          this.getUserId(),
          this.leagueCode
        );

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.$vToastify.customSuccess("Dołączyłeś do nowej ligi");
          this.dialog = false;
          this.leagueCode = "";
          // TODO Emituj event żeby przeładować ligi
        } else {
          this.$vToastify.customError("Nie udało się dołączyć do ligi");
        }
      } catch (err) {
        this.$vToastify.customError("Nie udało się dołączyć do ligi");
      }
    },
  },
};
</script>
