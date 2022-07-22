<template>
  <v-card class="d-flex flex-column flex grey lighten-5" outlined>
    <IncomingMatchesTable class="d-flex flex-column flex" />
  </v-card>
</template>

<script>
import IncomingMatchesTable from "@/components/shared/IncomingMatchesTable.vue";

export default {
  name: "SecondaryLayout",
  components: {
    IncomingMatchesTable,
  },
};
</script>
