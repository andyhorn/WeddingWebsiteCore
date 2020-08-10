<template>
  <b-container>
    <b-row>
      <b-col>
        <p class="m-0 p-0">{{ guest.firstName }} {{ guest.lastName }}</p>
        <p v-if="guest.isChild" class="m-0 p-0 text-subtitle text-italic">Child</p>
      </b-col>
      <b-col class="d-flex align-items-center justify-content-end">
        <b-button-close @click="onDelete" />
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import Box from "@/components/Box.vue";

export default {
  name: "Guest",
  props: ["guest"],
  methods: {
    async onDelete() {
      if (confirm("Are you sure you want to remove this guest?")) {
        const deleted = await this.$store.dispatch(
          "deleteGuest",
          this.guest.guestId
        );
        this.$store.dispatch("fetchAllGuests");
        this.$store.dispatch("fetchAllFamilies");
      }
    }
  }
};
</script>

<style scoped>
p.text-italic {
  font-style: italic;
}
p.text-subtitle {
  font-size: 0.7rem;
}
</style>