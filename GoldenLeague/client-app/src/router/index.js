import Vue from "vue";
import VueRouter from "vue-router";
import store from "../store";

Vue.use(VueRouter);

// TODO
// const loadView = (viewName) => {
//   // route level code-splitting
//   // this generates a separate chunk (about.[hash].js) for this route
//   // which is lazy-loaded when the route is visited.
//   import(/* webpackChunkName: "about" */ `../views/${viewName}.vue`);
// };

const routes = [
  {
    path: "/",
    name: "Home",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/HomeView.vue"),
  },
  {
    path: "/log-in",
    name: "Login",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/LoginView.vue"),
  },
  {
    path: "/register",
    name: "Register",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/RegisterView.vue"),
  },
  // {
  //   path: "/my-squad",
  //   name: "MySquad",
  //   component: () =>
  //     import(/* webpackChunkName: "about" */ "../views/MySquadView.vue"),
  // },
  // {
  //   path: "/ranking",
  //   name: "Ranking",
  //   component: () =>
  //     import(/* webpackChunkName: "about" */ "../views/RankingView.vue"),
  // },
  {
    path: "/bookmaker-leagues",
    name: "BookmakerLeagues",
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../views/BookmakerLeaguesView.vue"
      ),
  },
  {
    path: "/bookmaker-bets",
    name: "BookmakerBets",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/BookmakerBetsView.vue"),
  },
  {
    path: "/info",
    name: "Info",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/InfoView.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
  scrollBehavior() {
    return { x: 0, y: 0 };
  },
});

router.beforeEach((to, from, next) => {
  const isAuthorized = store.getters["user/isAuthorized"];
  if (
    to.name !== "AuthError" &&
    to.name !== "Login" &&
    to.name !== "Register" &&
    !isAuthorized
  ) {
    next({ name: "Login" });
  } else if ((to.name === "Login" || to.name === "Register") && isAuthorized) {
    next({ name: "Home" });
  } else next();
});

export default router;
