<template>
  <Box class="py-2 mb-3">
    <b-container>
      <b-row>
        <b-col class="d-flex justify-content-start align-items-center">
          <b-button size="sm" class="mr-3" variant="primary" @click="toggleCollapse">
            <b-icon :icon="collapseOpen ? 'chevron-up' : 'chevron-down'" />
          </b-button>
          <p class="m-0 p-0">
            <span class="text-italic">Family of</span>
            {{ family.headMember.firstName }} {{ family.headMember.lastName }} ({{ family.members.length }})
          </p>
        </b-col>
        <b-col class="d-flex justify-content-end align-items-center">
          <a href @click.prevent="onDeleteFamily">
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
          @click="openGuestModal"
        >Add member</b-button>
        <Box
          v-for="member in family.members.filter(x => x.guestId != family.headMemberId)"
          :key="member.guestId"
        >
          <div class="d-flex align-items-center">
            <a @click="promoteGuest(member.guestId)" v-if="!member.isChild">
              <b-icon-arrow-bar-up />
            </a>
            <Guest :guest="member" />
          </div>
        </Box>
      </b-collapse>
    </b-container>
  </Box>
</template>

<script>
import familyService from "@/services/familyService.js";
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
    },
    openGuestModal() {
      this.$emit("newGuest", this.family);
    },
    async promoteGuest(guestId) {
      let putFamily = { ...this.family };
      putFamily.headMemberId = guestId;
      await this.$http
        .put("families/" + this.family.familyId, putFamily)
        .then(() => this.$emit("update", this.family.familyId))
        .catch(err => console.log(err));
    },
    onDeleteFamily() {
      this.$emit("delete", this.family.familyId);
    }
  }
};
</script>