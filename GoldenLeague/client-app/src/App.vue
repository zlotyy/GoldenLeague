<template>
  <v-app>
    <Menu></Menu>
    <v-main>
      <Layout>
        <router-view />
      </Layout>
    </v-main>
  </v-app>
</template>

<script>
import Menu from "@/components/Menu.vue";
import Layout from "@/components/layouts/Layout.vue";

export default {
  name: "App",
  components: {
    Menu,
    Layout,
  },
  data: () => ({
    //
  }),
};
</script>
