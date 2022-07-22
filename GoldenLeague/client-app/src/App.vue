<template>
  <v-app>
    <TheMenu></TheMenu>
    <v-main>
      <Layout>
        <router-view />
      </Layout>
    </v-main>
  </v-app>
</template>

<script>
import TheMenu from "@/components/TheMenu.vue";
import Layout from "@/components/layouts/Layout.vue";

export default {
  name: "App",
  components: {
    TheMenu,
    Layout,
  },
  data: () => ({
    //
  }),
};
</script>

<style lang="scss">
@import "@/scss/global.scss";
.custom-height {
  min-height: 100%;
}
</style>
