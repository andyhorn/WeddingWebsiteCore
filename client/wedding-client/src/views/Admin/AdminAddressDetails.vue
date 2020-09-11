<template>
  <h1>{{ id }}</h1>
</template>

<script>
export default {
  name: "AdminAddressDetails",
  data() {
    return {
      id: null,
    };
  },
  created() {
    const id = this.$route.params.id;
    this.id = id;
  },
};
</script>