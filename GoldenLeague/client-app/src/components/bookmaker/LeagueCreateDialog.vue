<template>
  <v-dialog
    persistent
    max-width="600px"
    v-model="dialog"
    @keydown.esc="CloseDialog()"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-col class="d-flex flex-column" cols="12" md="6">
        <v-btn class="primary" outlined v-bind="attrs" v-on="on">
          Utwórz ligę
        </v-btn>
      </v-col>
    </template>
    <v-card>
      <v-card-title>Nowa liga prywatna</v-card-title>
      <v-card-text>
        <v-text-field label="Nazwa" v-model="leagueName"> </v-text-field>
        <v-select
          dense
          outlined
          multiple
          label="Rozgrywki"
          :items="competitions"
          item-text="competitionsName"
          item-value="competitionsId"
          v-model="competitionsSelected"
        ></v-select>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="primary" outlined @click="CloseDialog()"> Anuluj </v-btn>
        <v-btn class="primary" outlined @click="SubmitLeagueCreate()">
          Utwórz
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import BookmakerService from "@/services/BookmakerService";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "LeagueCreateDialog",
  data: () => ({
    dialog: false,
    leagueName: "",
    competitions: [],
    competitionsSelected: [],
  }),
  async mounted() {
    await this.setCompetitions();
    this.competitions = this.getCompetitions();
  },
  methods: {
    ...mapGetters("user", ["getUserId"]),
    ...mapGetters("common", ["getCompetitions"]),
    ...mapActions("common", ["setCompetitions"]),
    async SubmitLeagueCreate() {
      try {
        if (!this.$_isValid()) {
          return;
        }

        const response = await BookmakerService.LeagueCreate({
          Name: this.leagueName,
          InsertUserId: this.getUserId(),
          CompetitionsIds: this.competitionsSelected,
        });

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          this.$vToastify.customSuccess("Liga została utworzona");
          this.CloseDialog();
          this.$emit("league-created");
        }
      } catch (err) {
        return;
      }
    },
    CloseDialog() {
      this.dialog = false;
      this.leagueName = "";
      this.competitionsSelected = [];
      this.$emit("input");
    },
    $_isValid() {
      if (!this.leagueName) {
        this.$vToastify.validationError("Wprowadź nazwę ligi");
        return false;
      }

      if (!this.competitionsSelected[0]) {
        this.$vToastify.validationError("Wybierz rozgrywki do typowania");
        return false;
      }

      return true;
    },
  },
};
</script>
