<template>
  <div class="container pt-3">
    <h1 class="text-center mb-3">Guest List ({{ guests.length }})</h1>
    <a href @click.prevent="openGuestModal(null)">
      <b-icon-plus />New Family
    </a>
    <b-modal :id="createGuestModal" title="Add New Guest" @hide="onGuestCancel" hide-footer>
      <form @submit.prevent="onCreateGuest">
        <b-container>
          <b-row>
            <b-col>
              <b-form-group
                id="first-name-input-group"
                label="First name"
                label-for="first-name-input"
                :invalid-feedback="messages.invalidFirstName"
                :state="states.firstNameState"
              >
                <b-form-input
                  id="first-name-input"
                  v-model="newGuest.firstName"
                  :state="states.firstNameState"
                  @input="onFirstNameInput"
                  trim
                  required
                />
              </b-form-group>
            </b-col>
          </b-row>
          <b-row>
            <b-col>
              <b-form-group
                id="last-name-input-group"
                label="Last name"
                label-for="last-name-input"
                :invalid-feedback="messages.invalidLastName"
                :state="states.lastNameState"
              >
                <b-form-input
                  id="last-name-input"
                  v-model="newGuest.lastName"
                  :state="states.lastNameState"
                  @input="onLastNameInput"
                  trim
                  required
                />
              </b-form-group>
            </b-col>
          </b-row>
          <b-row v-if="selectedFamilyId">
            <b-col>
              <b-form-group
                id="is-child-switch-group"
                label="Is a Child"
                label-for="is-child-switch"
              >
                <b-form-checkbox id="is-child-switch" v-model="newGuest.isChild" switch />
              </b-form-group>
            </b-col>
          </b-row>
        </b-container>
        <b-button type="submit">Save</b-button>
      </form>
    </b-modal>
    <div class="py-5">
      <p v-if="guests.length == 0" class="text-center">No guests.</p>
      <Family
        v-for="family in families"
        :key="family.familyId"
        :family="family"
        @update="onUpdate"
        @delete="onDeleteFamily"
        @newGuest="openGuestModal"
      />
    </div>
  </div>
</template>

<script>
import GuestList from "@/components/Admin/Guests/GuestList.vue";
import Family from "@/components/Admin/Guests/Family.vue";
import Box from "@/components/Box.vue";
import Guest from "@/components/Admin/Guests/Guest.vue";
import { ACTIONS } from "@/store";

export default {
  name: "AdminGuests",
  components: {
    GuestList,
    Guest,
    Family,
    Box
  },
  data() {
    return {
      selectedFamilyId: 0,
      createGuestModal: "create-guest-modal",
      newGuest: {
        firstName: "",
        lastName: "",
        familyId: null,
        isChild: false
      },
      states: {
        firstNameState: null,
        lastNameState: null,
        familyNameState: null
      },
      messages: {
        invalidFirstName: "First name is required.",
        invalidLastName: "Last name is required.",
        invalidFamilyName: "Family name is required."
      },
      chevrons: {}
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
    async onCreateGuest() {
      const guestData = this.newGuest;

      if (this.selectedFamilyId) {
        guestData.familyId = this.selectedFamilyId;
      }

      await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.CREATE, guestData);
    },
    openGuestModal(family) {
      if (family) {
        this.selectedFamilyId = family.familyId;
        this.newGuest.lastName = family.headMember.lastName;
      } else {
        this.selectedFamilyId = 0;
      }

      this.$bvModal.show(this.createGuestModal);
    },
    openFamilyModal() {
      this.$bvModal.show(this.createFamilyModal);
    },
    onGuestCancel() {
      this.newGuest = {};
      this.states.firstNameState = null;
      this.states.lastNameState = null;
      this.selectedFamilyId = 0;
    },
    onFamilyCancel() {
      this.newFamily = {};
      this.states.familyNameState = null;
    },
    onFirstNameInput() {
      if (this.newGuest.firstName && this.newGuest.firstName.length) {
        this.states.firstNameState = true;
      } else {
        this.states.firstNameState = false;
      }
    },
    onLastNameInput() {
      if (this.newGuest.lastName && this.newGuest.lastName.length) {
        this.states.lastNameState = true;
      } else {
        this.states.lastNameState = false;
      }
    },
    onFamilyNameInput() {
      if (this.newFamily.name && this.newFamily.name.length) {
        this.states.familyNameState = true;
      } else {
        this.states.familyNameState = false;
      }
    },
    async fetch() {
      await this.fetchAllFamilies();
      await this.fetchAllGuests();
    },
    closeModal(modalName) {
      this.$bvModal.hide(modalName);
    }
  },
  mounted() {
    this.fetch();
  }
};
</script>

<style scoped>
</style>