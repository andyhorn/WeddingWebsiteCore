<template>
  <div id="app">
    <div :style="style" class="header bg-light">
      <h1 class="text-center pt-3">Krystal &amp; Andy</h1>
      <div :class="{sticky: isSticky}" class="border-bottom bg-light" ref="header">
        <b-navbar>
          <b-navbar-nav class="mx-auto text-medium">
            <b-nav-item to="/">Home</b-nav-item>
            <b-nav-item to>RSVP</b-nav-item>
            <b-nav-item to>Photos</b-nav-item>
            <b-nav-item to>Events</b-nav-item>
            <b-nav-item to>Wedding Party</b-nav-item>
            <b-nav-item to>Gift Registries</b-nav-item>
            <b-nav-item-dropdown v-if="isLoggedIn" id="nav-dropdown-admin" text="Admin" left>
              <b-dropdown-item :to="{ name: 'Admin-Guests' }">Guests</b-dropdown-item>
              <b-dropdown-item :to="{ name: 'Admin-RSVP' }">RSVPs</b-dropdown-item>
              <b-dropdown-item :to="{ name: 'Admin-Photos' }">Photos</b-dropdown-item>
              <b-dropdown-item :to="{ name: 'Admin-Events' }">Events</b-dropdown-item>
              <b-dropdown-item :to="{ name: 'Admin-Party' }">Wedding Party</b-dropdown-item>
            </b-nav-item-dropdown>
            <b-nav-item v-if="isLoggedIn" @click="logout">Logout</b-nav-item>
          </b-navbar-nav>
        </b-navbar>
      </div>
    </div>
    <div>
      <router-view />
    </div>
  </div>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      isSticky: false,
      style: {
        paddingBottom: 0
      },
      stickyHeight: 0,
      triggerPosition: 0
    };
  },
  watch: {
    isLoggedIn: {
      immediate: true,
      handler: function(isLoggedIn) {
        if (!isLoggedIn && this.$route.name.includes("Admin")) {
          this.$router.push({ name: "Home" });
        }
      }
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.isLoggedIn;
    }
  },
  mounted() {
    const header = this.$refs.header;
    const app = this.$refs.app;

    this.triggerPosition = header.offsetTop;
    this.stickyHeight = header.offsetHeight;

    const vm = this;
    window.onscroll = function() {
      if (window.pageYOffset > vm.triggerPosition) {
        vm.isSticky = true;
        vm.style.paddingBottom = vm.stickyHeight + "px";
      } else {
        vm.isSticky = false;
        vm.style.paddingBottom = 0;
      }
    };
  }
};
</script>

<style scoped>
.header h1 {
  font-size: 5rem;
}
.text-medium {
  font-size: 1.5rem;
}
.sticky {
  transition: 100ms;
  position: fixed;
  top: 0;
  width: 100%;
  z-index: 100;
}
</style>