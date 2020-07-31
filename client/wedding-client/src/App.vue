<template>
  <div id="app">
    <div :style="style" class="header">
      <h1>Krystal &amp; Andy</h1>
      <div :class="{sticky: isSticky}" ref="header">
        <b-navbar>
          <b-navbar-nav class="mx-auto">
            <b-nav-item to="/">Home</b-nav-item>
            <b-nav-item to>RSVP</b-nav-item>
            <b-nav-item to>Photos</b-nav-item>
            <b-nav-item to>Events</b-nav-item>
            <b-nav-item to>Wedding Party</b-nav-item>
            <b-nav-item to>Gift Registries</b-nav-item>
          </b-navbar-nav>
        </b-navbar>
      </div>
    </div>
    <hr />
    <router-view />
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
  mounted() {
    const header = this.$refs.header;
    const app = this.$refs.app;

    this.triggerPosition = header.offsetTop;
    this.stickyHeight = header.offsetHeight;
    console.log(this.stickyHeight);

    const vm = this;
    window.onscroll = function() {
      if (window.pageYOffset > vm.triggerPosition) {
        vm.isSticky = true;
        vm.style.paddingBottom = vm.stickyHeight + 1 + "px";
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
.header a {
  font-size: 1.5rem;
}
.sticky {
  transition: 100ms;
  position: fixed;
  top: 0;
  width: 100%;
}
</style>