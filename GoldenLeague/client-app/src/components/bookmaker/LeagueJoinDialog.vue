<template>
  <v-dialog
    v-model="dialog"
    persistent
    max-width="600px"
    @keydown.esc="CloseDialog()"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-col class="d-flex flex-column" cols="12" md="6">
        <v-btn class="primary" outlined v-bind="attrs" v-on="on">
          Dołącz do ligi
        </v-btn>
      </v-col>
    </template>
    <v-card>
      <v-card-title>
        Dołącz do prywatnej ligi
        <v-spacer></v-spacer>
        <v-tooltip bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-icon v-bind="attrs" v-on="on"> fas fa-info-circle </v-icon>
          </template>
          <span>
            Aby dołączyć do prywatnej ligi musisz poprosić jej założyciela o kod
          </span>
        </v-tooltip>
      </v-card-title>
      <v-card-text>
        <v-text-field
          label="Kod ligi"
          v-model="leagueCode"
          @keyup.enter="SubmitLeagueJoin"
        >
        </v-text-field>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="primary" outlined @click="CloseDialog()"> Anuluj </v-btn>
        <v-btn class="primary" outlined @click="SubmitLeagueJoin()">
          Dołącz
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import UserService from "@/services/UserService";
import { mapActions } from "vuex";

export default {
  name: "LeagueJoinDialog",
  data: () => ({
    dialog: false,
    leagueCode: "",
  }),
  methods: {
    ...mapActions("common", ["resetCompetitions"]),
    async SubmitLeagueJoin() {
      try {
        if (!this.$_isValid()) {
          return;
        }

        const response = await UserService.BookmakerLeagueJoin(this.leagueCode);

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.$vToastify.customSuccess("Dołączyłeś do nowej ligi");
          await this.resetCompetitions();
          this.CloseDialog();
          this.$emit("league-joined");
        }
      } catch (err) {
        if (err.status === 404) {
          this.$vToastify.customWarning("Nie znaleziono użytkownika");
        }
      }
    },
    CloseDialog() {
      this.dialog = false;
      this.leagueCode = "";
      this.$emit("input");
    },
    $_isValid() {
      const pattern =
        /^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$/i;

      if (!this.leagueCode) {
        this.$vToastify.validationError("Wprowadź kod ligi");
        return false;
      }

      if (!pattern.test(this.leagueCode)) {
        this.$vToastify.validationError(
          "Wprowadzony kod ligi jest nieprawidłowy"
        );
        return false;
      }

      return true;
    },
  },
};
</script>
