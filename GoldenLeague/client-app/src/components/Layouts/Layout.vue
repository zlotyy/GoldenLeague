<template>
  <v-container fluid class="grey lighten-1">
    <v-row dense>
      <v-col cols="8 d-flex flex-column">
        <MainLayout>
          <slot />
        </MainLayout>
      </v-col>
      <v-col cols="4 d-flex flex-column">
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
