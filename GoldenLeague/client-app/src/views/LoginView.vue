<template>
  <v-row justify="center" class="mt-16">
    <v-col cols="6">
      <div>
        <div class="mb-16 text-center">
          <span>Aby móc korzystać z aplikacji musisz się zalogować</span>
        </div>
        <v-card>
          <v-card-title>Logowanie</v-card-title>
          <v-card-text>
            <v-text-field v-model="login" label="Login"> </v-text-field>
            <v-text-field type="password" v-model="password" label="Hasło">
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
    async LogIn() {
      try {
        if (!this.isValid()) {
          console.log("LogIn isValid false");
          return;
        }

        const response = await UserService.LogIn(this.login, this.password);
        const { data } = response;
        console.log("LogIn response: " + response);

        if (response.status === 200) {
          await this.setUser({
            id: data.userId,
            login: data.login,
            token: data.token,
          });

          this.$router.push({
            name: "MatchBetting",
          });
        } else {
          console.log("Register UNKNOWN ERROR");
          alert("Wystąpił nieoczekiwany błąd");
          await this.resetUser();
        }
      } catch (err) {
        console.log("LogIn err: " + err);
        if (err.status === 500) {
          console.log("SERVER ERROR");
          alert("Wystąpił błąd serwera");
        } else if (err.status === 404) {
          alert("Nie znaleziono użytkownika");
        } else {
          alert("Wystąpił nieoczekiwany błąd");
        }
        await this.resetUser();
      }
    },
    isValid() {
      if (this.login.length < 1) {
        alert("Login nie może być pusty");
        return false;
      }
      if (this.password.length < 1) {
        alert("Hasło nie może być puste");
        return false;
      }
      return true;
    },
  },
};
</script>

<style>
.card-container {
  padding: 16px;
}
</style>
