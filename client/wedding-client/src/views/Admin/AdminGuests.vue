<template>
  <div class="container pt-3">
    <h1 class="text-center mb-3">Guest List ({{guestList.length}})</h1>
    <b-button block variant="success" v-b-modal="createGuestModal">Add Guest</b-button>
    <b-button block variant="success" v-b-modal="createFamilyModal">Add Family</b-button>
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
          <b-row>
            <b-col cols="9">
              <b-form-group id="family-select-group" label="Family" label-for="family-select-input">
                <b-form-select v-model="newGuest.familyId" :options="familyValues" />
              </b-form-group>
            </b-col>
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
      <GuestList :guests="guestList" :families="families" />
    </div>
  </div>
</template>

<script>
import GuestList from "@/components/Admin/Guests/GuestList.vue";

export default {
  name: "AdminGuests",
  components: {
    GuestList
  },
  data() {
    return {
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
      }
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
    async fetchAllGuests() {
      await this.$store.dispatch("fetchAllGuests");
    },
    async fetchAllFamilies() {
      await this.$store.dispatch("fetchAllFamilies");
    },
    async onCreateGuest() {
      await this.$store.dispatch("createNewGuest", this.newGuest);
      this.$store.dispatch("fetchAllGuests");
      this.$bvModal.hide(this.createGuestModal);
    },
    async onCreateFamily() {
      await this.$store.dispatch("createNewFamily", this.newFamily);
      this.$store.dispatch("fetchAllFamilies");
      this.$bvModal.hide(this.createFamilyModal);
    },
    onGuestCancel() {
      this.newGuest = {};
      this.states.firstNameState = null;
      this.states.lastNameState = null;
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
    }
  },
  mounted() {
    this.fetchAllGuests();
    this.fetchAllFamilies();
  }
};
</script>