<template>
  <div class="container">{{ $route.params.id }}</div>
</template>

<script>
export default {
  name: "AdminEventDetails",
  data() {
    return {
      event: null,
    };
  },
  created() {
    const id = this.$route.params.id;
    this.event = this.$store.getters.findEvent(id);
  },
  mounted() {
    console.log(this.event);
  },
};
</script>

<style scoped>
</style>