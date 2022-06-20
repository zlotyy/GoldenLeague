<template>
  <v-row>
    <v-col cols="12">
      <div class="text-h5 mb-5">Ligi typerów</div>
      <v-card>
        <v-row>
          <v-col class="d-flex flex-column" cols="12" md="6">
            <v-btn class="primary" outlined>Utwórz ligę</v-btn>
          </v-col>
          <v-col class="d-flex flex-column" cols="12" md="6">
            <v-btn class="primary" outlined>Dołącz do ligi</v-btn>
          </v-col>
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
          <template v-slot:item.options="{ item }">
            <v-icon>fas fa-ellipsis-v</v-icon>
          </template>
        </v-data-table>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: "BookmakerLeaguesView",
  data() {
    return {
      headers: [
        { text: "Liga", value: "name", width: "50%" },
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
    $_setLeaguesData() {
      this.leagues = [
        {
          id: "DA1F3389-EC5E-464C-8945-A83AF3EAD7CB",
          name: "GoldenLeague",
          isPrivate: false,
          ranking: 2,
        },
        {
          id: "44466491-8B6B-4F83-9E6A-6CC85CA9C14A",
          name: "Liga tymczasowa",
          isPrivate: true,
          ranking: 1,
        },
      ];
    },
  },
};
</script>
