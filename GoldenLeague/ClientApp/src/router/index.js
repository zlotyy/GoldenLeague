import { createWebHistory, createRouter } from "vue-router";
import Login from "@/components/Login.vue";
import Counter from "@/components/Counter.vue";
import FetchData from "@/components/FetchData.vue";

const routes = [
  {
    path: "/",
    name: "Login",
    component: Login
  },
  {
    path: "/Counter",
    name: "Counter",
    component: Counter
  },
  {
    path: "/FetchData",
    name: "FetchData",
    component: FetchData
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
