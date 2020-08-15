<template>
  <div class="container pt-3">
    <h1 class="text-center mb-3">Guest List ({{ guests.length }})</h1>
    <p>{{ families.length }} families</p>
    <b-row>
      <b-col>
        <b-button size="sm" squared variant="success" @click="isNewGuestModalVisible = true">
          <b-icon-plus />New Guest
        </b-button>
      </b-col>
      <b-col class="d-flex justify-content-end">
        <b-button size="sm" squared variant="success" @click="isNewFamilyModalVisible = true">
          <b-icon-plus />New Family
        </b-button>
      </b-col>
    </b-row>
    <b-row>
      <p
        v-for="guest in guests"
        :key="guest.id"
      >{{ guest.firstName }} {{ guest.lastName }} {{ guest.familyId }}</p>
    </b-row>
    <NewGuestModal
      :families="families"
      :visible="isNewGuestModalVisible"
      @closed="isNewGuestModalVisible = false"
    />
    <NewFamilyModal
      :guests="guests"
      :visible="isNewFamilyModalVisible"
      @closed="isNewFamilyModalVisible = false"
    />
    <div class="py-5">
      <p v-if="guests.length == 0" class="text-center">No guests.</p>
      <!-- <Family
        v-for="family in families"
        :key="family.familyId"
        :family="family"
        @update="onUpdate"
        @delete="onDeleteFamily"
        @newGuest="openGuestModal"
      />-->
    </div>
  </div>
</template>

<script>
import GuestList from "@/components/Admin/Guests/GuestList.vue";
import Family from "@/components/Admin/Guests/Family.vue";
import Box from "@/components/Box.vue";
import Guest from "@/components/Admin/Guests/Guest.vue";
import NewGuestModal from "@/components/modals/NewGuestModal.vue";
import NewFamilyModal from "@/components/modals/NewFamilyModal.vue";
import { ACTIONS } from "@/store";

export default {
  name: "AdminGuests",
  components: {
    GuestList,
    Guest,
    Family,
    Box,
    NewGuestModal,
    NewFamilyModal
  },
  data() {
    return {
      isNewGuestModalVisible: false,
      isNewFamilyModalVisible: false
    };
  },
  computed: {
    familyIds() {
      return this.$store.getters.familyIds;
    },
    families() {
      return this.$store.getters.families;
    },
    guests() {
      return this.$store.getters.guests;
    }
  },
  methods: {
    async onDeleteFamily(familyId) {
      await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.DELETE);
    },
    async onDeleteGuest(guestId) {
      await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.DELETE);
    },
    async fetchAllFamilies() {
      await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.FETCH_ALL);
    },
    async fetchAllGuests() {
      await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.FETCH_ALL);
    },
    async fetch() {
      await this.fetchAllFamilies();
      await this.fetchAllGuests();
    }
  },
  mounted() {
    this.fetch();
  }
};
</script>

<style scoped>
</style>