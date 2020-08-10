<template>
  <Box class="py-2 mb-3">
    <b-container>
      <b-row>
        <b-col class="d-flex justify-content-start align-items-center">
          <b-button class="mr-3" @click="toggleCollapse">
            <b-icon :icon="collapseOpen ? 'chevron-up' : 'chevron-down'" />
          </b-button>
          <p class="m-0 p-0">
            <span class="text-italic">Family of</span>
            {{ family.headMember.firstName }} {{ family.headMember.lastName }} ({{ family.members.length }})
          </p>
        </b-col>
        <b-col class="d-flex justify-content-end align-items-center">
          <a href @click.prevent="onDeleteFamily(family)">
            <b-icon-x-circle />
          </a>
        </b-col>
      </b-row>
      <b-collapse :id="collapseId" class="mt-3">
        <p v-if="family.members.length == 1" class="m-0 p-0">No other members.</p>
        <b-button
          squared
          size="sm"
          variant="success"
          class="my-2"
          @click="openGuestModal(family)"
        >Add member</b-button>
        <Box
          v-for="member in family.members.filter(x => x.guestId != family.headMemberId)"
          :key="member.guestId"
        >
          <Guest :guest="member" />
        </Box>
      </b-collapse>
    </b-container>
  </Box>
</template>

<script>
import Box from "@/components/Box.vue";
import Guest from "@/components/Admin/Guests/Guest.vue";

export default {
  name: "Family",
  props: ["family"],
  components: {
    Box,
    Guest
  },
  data() {
    return {
      collapseOpen: false
    };
  },
  computed: {
    collapseId() {
      return this.family.familyId.toString();
    }
  },
  methods: {
    toggleCollapse() {
      this.$root.$emit("bv::toggle::collapse", this.collapseId);
      this.collapseOpen = !this.collapseOpen;
    }
  }
};
</script>