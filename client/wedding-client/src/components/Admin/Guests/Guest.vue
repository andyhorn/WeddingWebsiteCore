<template>
  <b-container>
    <b-row>
      <b-col class="d-flex align-items-center justify-content-begin">
        <p class="m-0 p-0">{{ guest.firstName }} {{ guest.lastName }}</p>
      </b-col>
      <b-col v-if="guest.family" class="d-flex align-items-center justify-content-center">
        <p class="m-0 p-0">Family: {{ guest.family }}</p>
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