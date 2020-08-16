<template>
  <div class="container pt-3">
    <h1 class="text-center mb-3">Guest List ({{ guests.length }})</h1>
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
    <NewGuestModal
      :families="families"
      :selectedFamilyId="selectedFamilyId"
      :visible="isNewGuestModalVisible"
      @closed="closeGuestModal"
    />
    <NewFamilyModal
      :guests="guestsWithoutFamilies.filter(g => !g.isChild)"
      :visible="isNewFamilyModalVisible"
      @closed="isNewFamilyModalVisible = false"
    />
    <div class="py-5">
      <p v-if="guests.length == 0" class="text-center">No guests.</p>
      <p
        v-if="guestsWithoutFamilies.length"
      >{{ guestsWithoutFamilies.length }} guests without families</p>
      <Box v-for="guest in guestsWithoutFamilies" :key="guest.guestId">
        <Guest :guest="guest" />
      </Box>
      <Family
        v-for="family in families"
        :key="family.familyId"
        :family="family"
        @newGuest="addGuestToFamily"
      />
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
      isNewFamilyModalVisible: false,
      selectedFamilyId: null
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
    },
    guestsWithoutFamilies() {
      return this.$store.getters.guestsWithoutFamilies.sort((a, b) => a.firstName - b.firstName);
    }
  },
  methods: {
    addGuestToFamily(familyId) {
      this.selectedFamilyId = familyId;
      this.isNewGuestModalVisible = true;
    },
    closeGuestModal() {
      this.selectedFamilyId = null;
      this.isNewGuestModalVisible = false;
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