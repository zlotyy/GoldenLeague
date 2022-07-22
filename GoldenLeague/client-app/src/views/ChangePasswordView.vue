<template>
  <v-row justify="center">
    <v-col cols="12" lg="6">
      <div>
        <v-card>
          <v-card-title>Zmiana hasła</v-card-title>
          <v-card-text>
            <v-text-field
              type="password"
              v-model="passwordPrevious"
              label="Aktualne hasło"
              @keyup.enter="Submit"
            >
            </v-text-field>
            <v-text-field
              type="password"
              v-model="passwordNew"
              label="Nowe hasło"
              @keyup.enter="Submit"
            >
            </v-text-field>
            <v-text-field
              type="password"
              v-model="passwordConfirm"
              label="Potwierdź hasło"
              @keyup.enter="Submit"
            >
            </v-text-field>
          </v-card-text>
          <div class="text-right card-container">
            <v-btn class="primary" outlined @click="Submit"> Zatwierdź </v-btn>
          </div>
        </v-card>
      </div>
    </v-col>
  </v-row>
</template>

<script>
import UserService from "@/services/UserService";

export default {
  name: "ChangePasswordView",
  data() {
    return {
      passwordPrevious: "",
      passwordNew: "",
      passwordConfirm: "",
    };
  },
  methods: {
    async Submit() {
      if (!this.$_isValid()) {
        return;
      }

      const response = await UserService.ChangePassword(
        this.passwordPrevious,
        this.passwordNew
      );

      if (response.status === 200 && !(response.data || {}).errors[0]) {
        this.$vToastify.customSuccess("Hasło zostało zaktualizowane");
        this.$router
          .push({
            name: "Home",
          })
          .catch(() => {});
      }
    },
    $_isValid() {
      if (this.passwordPrevious.length < 1) {
        this.$vToastify.validationError("Aktualne hasło nie może być puste");
        return false;
      }
      if (this.passwordNew.length < 1) {
        this.$vToastify.validationError("Nowe hasło nie może być puste");
        return false;
      }
      if (this.passwordNew !== this.passwordConfirm) {
        this.$vToastify.validationError(
          "Nowe i potwierdzone hasło muszą być takie same"
        );
        return false;
      }
      return true;
    },
  },
};
</script>
