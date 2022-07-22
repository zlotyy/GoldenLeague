<template>
  <v-row justify="center">
    <v-col cols="12" lg="6">
      <div>
        <v-card>
          <v-card-title>Logowanie</v-card-title>
          <v-card-text>
            <v-text-field
              v-model="login"
              label="Login"
              @keyup.enter="LogIn"
            ></v-text-field>
            <v-text-field
              type="password"
              v-model="password"
              label="Hasło"
              @keyup.enter="LogIn"
            >
            </v-text-field>
          </v-card-text>
          <div class="text-right card-container">
            <v-btn class="primary" outlined @click="LogIn">Zaloguj</v-btn>
          </div>
        </v-card>
      </div>
    </v-col>
  </v-row>
</template>

<script>
import { mapActions } from "vuex";
import UserService from "@/services/UserService";

export default {
  name: "LoginView",
  data() {
    return {
      login: "",
      password: "",
    };
  },
  methods: {
    ...mapActions("user", ["setUser", "resetUser"]),
    ...mapActions("common", ["resetCompetitions"]),
    async LogIn() {
      try {
        if (!this.$_isValid()) {
          return;
        }

        const response = await UserService.LogIn(this.login, this.password);

        if (response.status === 200 && !(response.data || {}).errors[0]) {
          const userData = response.data.data;
          await this.setUser({
            id: userData.userId,
            login: userData.login,
            token: userData.token,
          });

          this.$vToastify.customSuccess(
            userData.login + " - zostałeś zalogowany"
          );

          await this.resetCompetitions();

          this.$router
            .push({
              name: "Home",
            })
            .catch(() => {});
        } else {
          await this.resetUser();
        }
      } catch (err) {
        if (err.status === 404) {
          this.$vToastify.customWarning("Nie znaleziono użytkownika");
        }
        await this.resetUser();
      }
    },
    $_isValid() {
      if (this.login.length < 1) {
        this.$vToastify.validationError("Login nie może być pusty");
        return false;
      }
      if (this.password.length < 1) {
        this.$vToastify.validationError("Hasło nie może być puste");
        return false;
      }
      return true;
    },
  },
};
</script>
