import Vue from "vue";
import VueRouter from "vue-router";

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
      import(/* webpackChunkName: "about" */ "../views/RankingView.vue"),
  },
  {
    path: "/my-squad",
    name: "MySquad",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/MySquadView.vue"),
  },
  {
    path: "/ranking",
    name: "Ranking",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/RankingView.vue"),
  },
  {
    path: "/match-betting",
    name: "MatchBetting",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/MatchBettingView.vue"),
  },
  {
    path: "/info",
    name: "Info",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/InfoView.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
