<template>
  <Box class="py-2 mb-3">
    <b-container>
      <b-row>
        <b-col class="d-flex justify-content-start align-items-center">
          <b-button size="sm" class="mr-3" variant="primary" @click="toggleCollapse">
            <b-icon :icon="collapseOpen ? 'chevron-up' : 'chevron-down'" />
          </b-button>
          <p class="m-0 p-0" v-if="headMember">
            <span class="text-italic">{{ headMember.firstName }} {{ headMember.lastName }}</span>
            and family
          </p>
          <p class="m-0 p-0" v-else>
            <span class="text-italic">{{ family.name }}</span> family
          </p>
        </b-col>
        <b-col class="d-flex justify-content-end align-items-center">
          <a href @click.prevent="onDeleteFamily">
            <b-icon-x-circle />
          </a>
        </b-col>
      </b-row>
      <b-collapse :id="collapseId" class="mt-3">
        <p v-if="members.length == 1" class="m-0 p-0">No other members.</p>
        <b-button
          squared
          size="sm"
          variant="success"
          class="my-2"
          @click="openGuestModal"
        >Add member</b-button>
        <Box class="mb-3">
          <Guest :guest="members.find(g => g.guestId == family.headMemberId)" />
        </Box>
        <Box
          v-for="member in members.filter(g => g.guestId != family.headMemberId)"
          :key="member.guestId"
        >
          <div class="d-flex align-items-center">
            <a
              @click="promoteGuest(member.guestId)"
              v-if="!member.isChild && family.headMemberId != member.guestId"
            >
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
import { v4 as uuidv4 } from "uuid";
import { ACTIONS } from "@/store";
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
      collapseOpen: false,
      collapseId: uuidv4()
    };
  },
  computed: {
    headMember() {
      return this.members.find(m => m.guestId == this.family.headMemberId);
    },
    members() {
      return this.$store.getters.guestsInFamily(this.family.familyId);
    }
  },
  methods: {
    toggleCollapse() {
      this.$root.$emit("bv::toggle::collapse", this.collapseId);
      this.collapseOpen = !this.collapseOpen;
    },
    openGuestModal() {
      this.$emit("newGuest", this.family.familyId);
    },
    async promoteGuest(guestId) {
      let family = {... this.family};
      family.headMemberId = guestId;
      await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.UPDATE, family);
    },
    async onDeleteFamily() {
      const name = this.family.name;

      if (confirm("Are you sure you want to delete the " + name + " family?")) {
        const id = this.family.familyId;
        await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.DELETE, id);
  
        const affectedGuests = this.$store.getters.guestsInFamily(id);
        for (let guest of affectedGuests) {
          guest.familyId = null;
          await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.UPDATE, guest);
        }
      }
    }
  }
};
</script>