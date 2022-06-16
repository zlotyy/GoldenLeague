<template>
  <v-row justify="center" class="mt-16">
    <v-col cols="6">
      <div>
        <v-card>
          <v-card-title>Rejestracja</v-card-title>
          <v-card-text>
            <v-text-field v-model="login" label="Login"> </v-text-field>
            <v-text-field type="password" v-model="password" label="Hasło">
            </v-text-field>
            <v-text-field
              type="password"
              v-model="passwordConfirm"
              label="Potwierdź hasło"
            >
            </v-text-field>
          </v-card-text>
          <div class="text-right card-container">
            <v-btn class="primary" outlined @click="Register">
              Zarejestruj
            </v-btn>
          </div>
        </v-card>
      </div>
    </v-col>
  </v-row>
</template>

<script>
import UserService from "@/services/UserService";

export default {
  name: "RegisterView",
  data() {
    return {
      login: "",
      password: "",
      passwordConfirm: "",
    };
  },
  methods: {
    async Register() {
      try {
        if (!this.isValid()) {
          console.log("Register isValid false");
          return;
        }

        const response = await UserService.Register(this.login, this.password);
        console.log("Register response: " + response);

        if (response.status === 200) {
          this.$router.push({
            name: "Login",
          });
        } else {
          console.log("Register UNKNOWN ERROR");
          alert("Wystąpił nieoczekiwany błąd");
        }
      } catch (err) {
        console.log("Register err: " + err);
        if (err.status === 500) {
          console.log("SERVER ERROR");
          alert("Wystąpił błąd serwera");
        } else {
          alert("Wystąpił nieoczekiwany błąd");
        }
      }
    },
    isValid() {
      if (this.password !== this.passwordConfirm) {
        alert("Hasła muszą być takie same");
        return false;
      }
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
