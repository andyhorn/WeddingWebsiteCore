import store from "@/store"
import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home'
import Login from "@/views/Login";
// import AdminGuests from "@/views/Admin/AdminGuests";
import AdminGuests2 from "@/views/Admin/AdminGuests2";
import AdminRSVP from "@/views/Admin/AdminRSVP";
import AdminEvents from "@/views/Admin/AdminEvents";
import AdminAddresses from "@/views/Admin/AdminAddresses";
import AdminAccommodations from "@/views/Admin/AdminAccommodations";
import AdminRegistries from "@/views/Admin/AdminRegistries";
import AdminWeddingParty from "@/views/Admin/AdminWeddingParty";
import AdminPhotos from "@/views/Admin/AdminPhotos";

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
    component: AdminGuests2
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
  },
  {
    path: "/admin/addresses",
    name: "Admin-Addresses",
    component: AdminAddresses
  },
  {
    path: "/admin/accommodations",
    name: "Admin-Accommodations",
    component: AdminAccommodations
  },
  {
    path: "/admin/registries",
    name: "Admin-Registries",
    component: AdminRegistries
  },
  {
    path: "/admin/party",
    name: "Admin-Party",
    component: AdminWeddingParty
  },
  {
    path: "admin/photos",
    name: "Admin-Photos",
    component: AdminPhotos
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
  if (to.name.includes("Admin") && !store.getters.isLoggedIn) {
    return next({ name: "Home" });
  }

  return next()
})

export default router
