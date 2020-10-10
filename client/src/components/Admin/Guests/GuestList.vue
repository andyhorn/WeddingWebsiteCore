<template>
  <div>
    <p v-if="guests.length == 0" class="text-center">No guests.</p>
    <Box v-for="family in families" :key="family.familyId">
      <b-container>
        <b-row class="py-2">
          <b-col>
            <p class="m-0 p-0">{{ family.name }}</p>
          </b-col>
          <b-col>
            <b-button-close @click="onDelete" />
          </b-col>
        </b-row>
      </b-container>
      <p v-if="family.members.length == 0">No family members added</p>
      <Box v-for="member in family.members" :key="member.guestId">
        <Guest :guest="member" />
      </Box>
    </Box>
  </div>
</template>

<script>
import Guest from "@/components/Admin/Guests/Guest.vue";
import Box from "@/components/Box.vue";

export default {
  name: "GuestList",
  props: ["guests", "families"],
  components: {
    Guest,
    Box
  },
  mounted() {
    console.log(this.guests);
    console.log(this.families);
  }
};
</script>