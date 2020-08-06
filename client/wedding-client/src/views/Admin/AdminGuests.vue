<template>
  <div class="container pt-3">
    <h1 class="text-center mb-3">Guest List</h1>
    <b-button block variant="success" v-b-modal.create-guest-modal>Add Guest</b-button>
    <b-modal id="create-guest-modal" title="Add New Guest" @ok="onCreateGuest" @cancel="onCancel">
      <b-container>
        <b-row>
          <b-col>
            <b-form-group
              id="first-name-input-group"
              label="First name"
              label-for="first-name-input"
              :invalid-feedback="firstNameInvalidMessage"
              :state="firstNameState"
            >
              <b-form-input
                id="first-name-input"
                v-model="newGuest.firstName"
                :state="firstNameState"
                trim
              />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              id="last-name-input-group"
              label="Last name"
              label-for="last-name-input"
              :invalid-feedback="lastNameInvalidMessage"
              :state="lastNameState"
            >
              <b-form-input
                id="last-name-input"
                v-model="newGuest.lastName"
                :state="lastNameState"
                trim
              />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="9">
            <b-form-group
              id="email-input-group"
              label="Email address"
              label-for="email-address-input"
              :invalid-feedback="emailAddressInvalidMessage"
              :state="emailAddressState"
            >
              <b-form-input
                id="email-address-input"
                v-model="newGuest.email"
                :state="emailAddressState"
                trim
              />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group id="is-child-switch-group" label="Is a Child" label-for="is-child-switch">
              <b-form-checkbox id="is-child-switch" v-model="newGuest.isChild" switch />
            </b-form-group>
          </b-col>
        </b-row>
      </b-container>
    </b-modal>
    <div class="py-5">
      <GuestList :guests="guests" />
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
      guests: [],
      newGuest: {
        firstName: "",
        lastName: "",
        email: "",
        isChild: false
      }
    };
  },
  methods: {
    async fetchAllGuests() {
      this.$http
        .get("/guests")
        .then(res => {
          console.log(res);
          const data = res.data;
          this.guests = data;
        })
        .catch(err => {
          console.log(err);
        });
    },
    onCreateGuest() {
      console.log("create guest");
      console.log(this.newGuest);
    },
    onCancel() {
      this.newGuest = {};
    }
  },
  mounted() {
    this.fetchAllGuests();
  }
};
</script>