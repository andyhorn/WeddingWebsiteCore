import store from "@/store"
import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from "../views/Login.vue";
import AdminGuests from "../views/Admin/AdminGuests.vue";
import AdminRSVP from "../views/Admin/AdminRSVP.vue";
import AdminEvents from "../views/Admin/AdminEvents.vue";

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/admin/guests",
    name: "Admin-Guests",
    component: AdminGuests
  },
  {
    path: "/admin/rsvp",
    name: "Admin-RSVP",
    component: AdminRSVP
  },
  {
    path: "/admin/events",
    name: "Admin-Events",
    component: AdminEvents
  }
  // {
  //   path: '/about',
  //   name: 'About',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
]

const router = new VueRouter({
  routes
});

router.beforeEach((to, from, next) => {
  if (to.name.includes("Admin") && !store.state.isLoggedIn) {
    return next({ name: "Home" });
  }

  return next()
})

export default router
