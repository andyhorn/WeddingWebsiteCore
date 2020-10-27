<template>
  <b-container v-if="!!guest">
    <b-row>
      <b-col>
        <div class="d-flex align-items-center justify-content-start">
          <div class="mr-2 px-2">
            <span @click="$emit('promote', guest.guestId)"
              v-b-tooltip.hover title="Promote to Head Member"
              class="link"
              v-if="!!guest.guestId && !guest.isChild && headMemberId != guest.guestId">
              <b-icon-arrow-bar-up variant="dark" />
            </span>
            <span v-else-if="guest.isChild">
              <b-icon-arrow-bar-up variant="danger" />
            </span>
          </div>
          <b-button squared size="sm" variant="success" class="my-2 mr-2" @click="toggleCollapse">
            <b-icon-pencil class="mr-2" />
            <b-icon :icon="isGuestEditVisible ? 'chevron-up': 'chevron-down'" />
          </b-button>
          <p class="m-0 p-0">
            <b-icon-star-fill variant="warning" v-if="guest.isWeddingMember" />
            {{ guest.firstName }} {{ guest.lastName }}
          </p>
        </div>
        <p v-if="guest.isChild" class="m-0 p-0 text-subtitle text-italic">{{ parentName }}</p>
      </b-col>
      <b-col class="d-flex justify-content-end align-items-center">
        <RsvpInviteForm :guestId="guest.guestId" />
      </b-col>
      <b-col class="d-flex align-items-center justify-content-end" cols="1">
        <b-button-close @click="onDelete" />
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <b-collapse v-model="isGuestEditVisible" class="mt-3">
          <b-container v-if="isGuestEditVisible">
            <GuestForm :guest="guest" @close="onGuestEditClose" />
          </b-container>
        </b-collapse>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import { v4 as uuidv4 } from "uuid";
import GuestForm from "@/components/forms/GuestForm";
import Box from "@/components/Box.vue";
import RsvpInviteForm from "@/components/Admin/Guests/RsvpInviteForm.vue";

import { ACTIONS } from "@/store";

export default {
  name: "Guest",
  components: {
    GuestForm,
    RsvpInviteForm,
  },
  props: ["guest", "headMemberId"],
  data() {
    return {
      isGuestEditVisible: false,
      guestFamilyId: null,
    };
  },
  computed: {
    parentName() {
      if (this.guest.isChild) {
        let label = "Child";
        const parent = this.$store.getters.findGuest(this.guest.parentId);

        if (parent) {
          label += ` of ${parent.firstName} ${parent.lastName}`;
        }

        return label;
      }

      return "";
    },
  },
  methods: {
    async onDelete() {
      const name = this.guest.firstName + " " + this.guest.lastName;
      if (confirm("Are you sure you want to remove " + name)) {
        const familyId = this.guest.familyId;
        const guestId = this.guest.guestId;

        await this.$store.dispatch(ACTIONS.GUESTS.DELETE, guestId);

        if (familyId) {
          await this.$store.dispatch(ACTIONS.FAMILIES.FETCH, familyId);
        }
      }
    },
    onGuestEditClose() {
      this.toggleCollapse();
    },
    toggleCollapse() {
      this.isGuestEditVisible = !this.isGuestEditVisible;
    },
  },
};
</script>

<style scoped>
p.text-italic {
  font-style: italic;
}
p.text-subtitle {
  font-size: 0.7rem;
}
.fade-in-enter-active,
.fade-in-leave-active {
  transition: opacity 400ms;
}
.fade-in-enter,
.fade-in-leave-to {
  opacity: 0;
}
.animate {
  flex-grow: 1;
  transition: all 400ms;
}
.link:hover {
  cursor: pointer;
}
</style>