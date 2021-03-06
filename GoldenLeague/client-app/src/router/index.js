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
  {
    path: "/change-password",
    name: "ChangePassword",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/ChangePasswordView.vue"),
  },
  {
    path: "/bookmaker-leagues",
    name: "BookmakerLeagues",
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../views/BookmakerLeaguesView.vue"
      ),
    // TODO zrobić jako children
    // children: [
    //   {
    //     path: ":leagueId/rank",
    //     name: "BookmakerLeague",
    //     props: true,
    //     component: () =>
    //       import(
    //         /* webpackChunkName: "about" */ "../views/BookmakerLeagueView.vue"
    //       ),
    //   },
    // ],
  },
  {
    path: "/bookmaker-leagues/:leagueId/rank",
    name: "BookmakerLeague",
    props: true,
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../views/BookmakerLeagueView.vue"
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
  } else if (
    (to.name === "Login" || to.name === "Register" || to.name === "Home") &&
    isAuthorized
  ) {
    next({ name: "BookmakerLeagues" });
  } else next();
});

export default router;
