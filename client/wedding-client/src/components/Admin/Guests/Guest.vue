<template>
  <div class="d-flex justify-content-between align-items-center">
    <p class="m-0 p-0">{{ guest.firstName }} {{ guest.lastName}}</p>
    <b-button variant="danger" @click="onDelete">Delete</b-button>
  </div>
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
      }
    }
  }
};
</script>