<template>
  <v-card>
    <v-card-subtitle>{{ header }}</v-card-subtitle>
    <v-simple-table dense fixed-header>
      <template>
        <thead>
          <tr>
            <th></th>
            <th>{{ $t("common.player") }}</th>
            <th>{{ $t("common.points") }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(user, index) in users" :key="user.id">
            <td>{{ index + 1 }}</td>
            <td>{{ user.login }}</td>
            <td>{{ user.points }}</td>
          </tr>
        </tbody>
      </template>
    </v-simple-table>
  </v-card>
</template>

<script>
export default {
  name: "StandingsTable",
  props: {
    header: {
      type: String,
      default: "Ranking",
    },
    users: [],
  },
};
</script>
