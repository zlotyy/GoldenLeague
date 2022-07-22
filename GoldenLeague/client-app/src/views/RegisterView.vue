<template>
  <v-row justify="center">
    <v-col cols="12" lg="6">
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
      if (!this.$_isValid()) {
        return;
      }

      const response = await UserService.Register(this.login, this.password);
      if (response.status === 200 && !(response.data || {}).errors[0]) {
        this.$vToastify.customSuccess(
          "Użytkownik o nazwie " + this.login + " został utworzony"
        );
        this.$router
          .push({
            name: "Login",
          })
          .catch(() => {});
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
      if (this.password !== this.passwordConfirm) {
        this.$vToastify.validationError("Hasła muszą być takie same");
        return false;
      }
      return true;
    },
  },
};
</script>
