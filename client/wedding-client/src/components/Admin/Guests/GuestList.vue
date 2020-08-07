<template>
  <div>
    <p v-if="guests.length == 0" class="text-center">No guests.</p>
    <Box v-for="guest in guests" :key="guest.guestId">
      <Guest :guest="guest" />
    </Box>
  </div>
</template>

<script>
import Guest from "@/components/Admin/Guests/Guest.vue";
import Box from "@/components/Box.vue";

export default {
  name: "GuestList",
  props: ["guests"],
  components: {
    Guest,
    Box
  }
};
</script>