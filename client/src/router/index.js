import store from "@/store"
import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home'
import Login from "@/views/Login";
import AdminGuests from "@/views/Admin/AdminGuests";
import AdminRSVP from "@/views/Admin/AdminRSVP";
import AdminEvents from "@/views/Admin/AdminEvents";
import AdminEventDetails from "@/views/Admin/AdminEventDetails";
import AdminAddresses from "@/views/Admin/AdminAddresses";
import AdminAddressDetails from "@/views/Admin/AdminAddressDetails";

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
  },
  {
    path: "/admin/events/:id",
    name: "Admin-Event-Details",
    component: AdminEventDetails
  },
  {
    path: "/admin/addresses",
    name: "Admin-Addresses",
    component: AdminAddresses
  },
  {
    path: "/admin/addresses/:id",
    name: "Admin-Address-Details",
    component: AdminAddressDetails
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
