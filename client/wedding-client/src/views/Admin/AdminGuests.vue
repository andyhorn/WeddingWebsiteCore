<template>
  <div class="container pt-3">
    <h1 class="text-center mb-3">Guest List ({{guestList.length}})</h1>
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
    <b-modal :id="createFamilyModal" title="Create a Family" @hide="onFamilyCancel" hide-footer>
      <b-form @submit.prevent="onCreateFamily">
        <b-container>
          <b-row>
            <b-col>
              <b-form-group
                id="family-name-input-group"
                label="Family Name"
                label-for="family-name-input"
                :invalid-feedback="messages.invalidFamilyNameMessage"
                :state="states.familyNameState"
              >
                <b-form-input
                  id="family-name-input"
                  v-model="newFamily.name"
                  @input="onFamilyNameInput"
                  :state="states.familyNameState"
                />
              </b-form-group>
            </b-col>
          </b-row>
          <b-row>
            <b-col>
              <b-button variant="success" type="submit">Save</b-button>
            </b-col>
          </b-row>
        </b-container>
      </b-form>
    </b-modal>
    <div class="py-5">
      <p v-if="guestList.length == 0" class="text-center">No guests.</p>
      <Box v-for="family in families" :key="family.familyId" class="py-2 mb-3">
        <b-container>
          <b-row>
            <b-col class="d-flex justify-content-start align-items-center">
              <b-button v-b-toggle="family.familyId.toString()" class="mr-3">
                <b-icon-chevron-down />
              </b-button>
              <p
                class="m-0 p-0"
              >{{ family.headMember.firstName }} {{ family.headMember.lastName }} ({{ family.members.length }})</p>
            </b-col>
            <b-col class="d-flex justify-content-end align-items-center">
              <a href @click.prevent="onDeleteFamily(family)">
                <b-icon-x-circle />
              </a>
            </b-col>
          </b-row>
          <b-collapse :id="family.familyId.toString()" class="mt-3">
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
    </div>
  </div>
</template>

<script>
import GuestList from "@/components/Admin/Guests/GuestList.vue";
import Box from "@/components/Box.vue";
import Guest from "@/components/Admin/Guests/Guest.vue";

export default {
  name: "AdminGuests",
  components: {
    GuestList,
    Guest,
    Box
  },
  data() {
    return {
      selectedFamilyId: 0,
      createGuestModal: "create-guest-modal",
      createFamilyModal: "create-family-modal",
      newGuest: {
        firstName: "",
        lastName: "",
        familyId: null,
        isChild: false
      },
      newFamily: {
        name: ""
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
    guestList() {
      return this.$store.state.guestList;
    },
    families() {
      return this.$store.state.families;
    },
    familyValues() {
      return this.families.map(x => {
        return {
          value: x.familyId,
          text: x.name
        };
      });
    }
  },
  methods: {
    onDeleteFamily(family) {
      this.$http.delete("families/" + family.familyId).then(() => this.fetch());
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
    async fetchAllGuests() {
      await this.$store.dispatch("fetchAllGuests");
    },
    async fetchAllFamilies() {
      await this.$store.dispatch("fetchAllFamilies");
    },
    async onCreateGuest() {
      if (this.selectedFamilyId) {
        this.newGuest.familyId = this.selectedFamilyId;
      }

      await this.$store.dispatch("createNewGuest", this.newGuest);

      // this.$store.dispatch("fetchAllGuests");
      // this.$store.dispatch("fetchAllFamilies");
      this.fetch();
      this.$bvModal.hide(this.createGuestModal);
    },
    async onCreateFamily() {
      await this.$store.dispatch("createNewFamily", this.newFamily);
      // this.$store.dispatch("fetchAllFamilies");
      this.fetch();
      this.$bvModal.hide(this.createFamilyModal);
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
    }
  },
  mounted() {
    this.fetch();
  }
};
</script>

<style scoped>
</style>