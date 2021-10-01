<template>
  <h1>Counter</h1>

  <p>This is a simple example of an Vue component.</p>

  <p aria-live="polite">
    Current count: <strong>{{ currentCount }}</strong>
  </p>

  <button class="btn btn-primary" @click="incrementCounter">Increment</button>
  <button class="btn btn-primary" @click="tmp">Log in</button>
</template>

<script>
// TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// eslint-disable-next-line import/extensions
import UsersService from "@/services/users-service";

export default {
  name: "Counter",
  data() {
    return {
      currentCount: 0
    };
  },
  methods: {
    incrementCounter() {
      this.currentCount++;
    },
    tmp() {
      UsersService.GetUser().then((response) => {
        console.log(response);
      });
    }
  }
};
</script>
