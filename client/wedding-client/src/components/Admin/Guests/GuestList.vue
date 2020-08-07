<template>
  <div>
    <ul v-if="guests.length">
      <li v-for="guest in guests" :key="guest.guestId">{{ guest }}</li>
    </ul>
    <p v-else class="text-center">No guests.</p>
  </div>
</template>

<script>
export default {
  name: "GuestList",
  props: ["guests"]
};
</script>