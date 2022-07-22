<template>
  <v-container fluid class="grey lighten-1 fill-height">
    <v-row v-if="userAuthorized" dense class="fill-height">
      <v-col class="d-flex flex-column" cols="12" md="8">
        <MainLayout>
          <slot />
        </MainLayout>
      </v-col>
      <v-col class="d-flex flex-column" cols="12" md="4">
        <SecondaryLayout />
      </v-col>
    </v-row>
    <v-row v-else dense class="fill-height">
      <v-col class="d-flex flex-column" cols="12" md="12">
        <MainLayout>
          <slot />
        </MainLayout>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import MainLayout from "./MainLayout.vue";
import SecondaryLayout from "./SecondaryLayout.vue";
import { mapGetters } from "vuex";

export default {
  name: "Layout",
  components: {
    MainLayout,
    SecondaryLayout,
  },
  computed: {
    userAuthorized() {
      return this.isAuthorized();
    },
  },
  methods: {
    ...mapGetters("user", ["isAuthorized"]),
  },
};
</script>
