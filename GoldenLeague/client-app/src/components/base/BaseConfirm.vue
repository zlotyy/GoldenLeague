<template>
  <v-dialog v-model="dialog" persistent :max-width="width">
    <template v-slot:activator="{ on }">
      <slot v-bind:on="on"></slot>
    </template>
    <v-card>
      <v-card-title>{{ title }}</v-card-title>
      <v-card-text> {{ message }} </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="primary" outlined @click="dialog = false"> NIE </v-btn>
        <v-btn class="primary" outlined @click="confirm()"> TAK </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "BaseConfirm",
  props: {
    title: {
      type: String,
      default: "Potwierdź",
    },
    message: {
      type: String,
      default: "Czy chcesz potwierdzić operację?",
    },
    width: {
      type: Number,
      default: 400,
    },
  },
  data: () => ({
    dialog: false,
  }),
  methods: {
    confirm() {
      this.dialog = false;
      this.$emit("confirm");
    },
  },
};
</script>
