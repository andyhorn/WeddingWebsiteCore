<template>
  <div class="container pt-3">
    <h1 class="text-center mb-3">Guest List</h1>
    <b-button block variant="success" v-b-modal.create-guest-modal>Add Guest</b-button>
    <b-modal :id="modalId" title="Add New Guest" @hide="onCancel" hide-footer>
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
              <b-form-group
                id="family-select-group"
                label="Family"
                label-for="family-select-input"
                :state="states.emailAddressState"
              ></b-form-group>
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
    <div class="py-5">
      <GuestList :guests="guestList" />
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
      modalId: "create-guest-modal",
      newGuest: {
        firstName: "",
        lastName: "",
        isChild: false
      },
      states: {
        firstNameState: null,
        lastNameState: null
      },
      messages: {
        invalidFirstName: "First name is required.",
        invalidLastName: "Last name is required."
      }
    };
  },
  computed: {
    guestList() {
      return this.$store.state.guestList;
    }
  },
  methods: {
    async fetchAllGuests() {
      await this.$store.dispatch("fetchAllGuests");
    },
    async onCreateGuest() {
      await this.$store.dispatch("createNewGuest", this.newGuest);
      this.$store.dispatch("fetchAllGuests");
      this.$bvModal.hide(this.modalId);
    },
    onCancel() {
      this.newGuest = {};
      this.states.firstNameState = null;
      this.states.lastNameState = null;
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
    }
  },
  mounted() {
    this.fetchAllGuests();
  }
};
</script>