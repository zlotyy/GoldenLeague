<template>
  <v-dialog v-model="dialog" persistent max-width="600px">
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
          item-text="competitionName"
          item-value="competitionId"
          v-model="competitionsSelected"
        ></v-select>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="primary" outlined @click="dialog = false"> Anuluj </v-btn>
        <v-btn class="primary" outlined @click="dialog = false"> Utwórz </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
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
    ...mapGetters("common", ["getCompetitions"]),
    ...mapActions("common", ["setCompetitions"]),
  },
};
</script>
