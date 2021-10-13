<template>
  <div>
    <v-card>
      <v-card-subtitle>{{ header }}</v-card-subtitle>
      <v-simple-table dense fixed-header>
        <template>
          <thead>
            <tr>
              <th></th>
              <th>{{ $t("common.player") }}</th>
              <th>Punkty</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(player, index) in players" :key="player.id">
              <td>{{ index + 1 }}</td>
              <td>{{ player.login }}</td>
              <td>{{ player.points }}</td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
    </v-card>
  </div>
</template>

<script>
export default {
  name: "StandingsTable",
  props: {
    header: {
      type: String,
      default: "Ranking",
    },
    players: [],
  },
};
</script>
