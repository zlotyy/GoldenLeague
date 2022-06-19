<template>
  <v-row justify="center" class="mt-16">
    <v-col cols="6">
      <div>
        <div class="mb-16 text-center">
          <h1 v-if="isAuthorized()">Witaj {{ userLogin }}</h1>
          <span v-else>
            Jeśli znalazłeś się na tej stronie, zgłoś błąd do administratora
          </span>
        </div>
      </div>
    </v-col>
  </v-row>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "HomeView",
  computed: {
    userLogin() {
      return this.getLogin();
    },
  },
  methods: {
    ...mapGetters("user", ["isAuthorized", "getLogin"]),
  },
};
</script>
