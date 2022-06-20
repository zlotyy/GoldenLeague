<template>
  <v-container fluid class="grey lighten-1">
    <v-row dense>
      <v-col class="d-flex flex-column" cols="12" md="8">
        <MainLayout>
          <slot />
        </MainLayout>
      </v-col>
      <v-col class="d-flex flex-column" cols="12" md="4">
        <SecondaryLayout />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import MainLayout from "./MainLayout.vue";
import SecondaryLayout from "./SecondaryLayout.vue";

export default {
  name: "Layout",
  components: {
    MainLayout,
    SecondaryLayout,
  },
};
</script>
