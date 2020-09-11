<template>
  <b-modal
    :visible="visible"
    id="new-guest-modal"
    title="Add New Guest"
    @show="onOpen"
    @hide="onCancel"
    hide-footer
  >
    <form @submit.prevent="onSubmit" :disabled="isBusy">
      <b-container>
        <b-row>
          <b-col>
            <b-form-group
              id="first-name-input-group"
              label="First Name"
              label-for="first-name-input"
              invalid-feedback="First name is required."
              :state="firstNameState"
            >
              <b-form-input
                id="first-name-input"
                v-model="firstName"
                :state="firstNameState"
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
              label="Last Name"
              label-for="last-name-input"
              invalid-feedback="Last name is required."
              :state="lastNameState"
            >
              <b-form-input
                id="last-name-input"
                v-model="lastName"
                :state="lastNameState"
                @input="onLastNameInput"
                trim
                required
              />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-form-group id="family-select-group" label="Family" label-for="family-select">
              <b-select v-model="familyId" :disabled="families.length == 0">
                <b-select-option
                  v-for="family in families"
                  :key="family.familyId"
                  :value="family.familyId"
                >{{ family.name }}</b-select-option>
              </b-select>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="3">
            <b-form-group id="is-child-switch-group" label="Is a Child" label-for="is-child-switch">
              <b-form-checkbox id="is-child-switch" v-model="isChild" switch />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              id="wedding-member-switch-group"
              label="Is a Wedding Party member"
              label-for="wedding-member-switch"
            >
              <b-form-checkbox switch id="wedding-member-switch" v-model="isWeddingMember" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row v-if="isChild">
          <b-col>
            <b-form-group label="Parent">
              <b-select v-model="parentId">
                <option v-for="guest in guests" :key="guest.guestId" :value="guest.guestId">
                  <span>{{ guest.firstName }} {{ guest.lastName }}</span>
                </option>
              </b-select>
            </b-form-group>
          </b-col>
        </b-row>
      </b-container>
      <b-button type="Submit" :disabled="isBusy">
        <b-spinner small v-if="isBusy" />
        <span v-else>Save</span>
      </b-button>
    </form>
  </b-modal>
</template>

<script>
import { ACTIONS } from "@/store";

export default {
  name: "NewGuestModal",
  props: ["families", "selectedFamilyId", "visible"],
  data() {
    return {
      firstName: "",
      lastName: "",
      isChild: false,
      parentId: null,
      isWeddingMember: false,
      familyId: null,
      firstNameState: null,
      lastNameState: null,
      isBusy: false,
    };
  },
  computed: {
    guests() {
      return this.$store.getters.nonChildren;
    },
  },
  methods: {
    onOpen() {
      if (this.selectedFamilyId) {
        this.familyId = this.selectedFamilyId;
      }
    },
    onCancel() {
      this.close();
    },
    async onSubmit() {
      this.isBusy = true;
      const guest = {
        firstName: this.firstName,
        lastName: this.lastName,
        isChild: this.isChild,
        isWeddingMember: this.isWeddingMember,
        familyId: this.familyId,
        parentId: this.isChild ? this.parentId : null,
      };

      await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.CREATE, guest);

      if (guest.familyId) {
        await this.$store.dispatch(
          ACTIONS.FAMILY_ACTIONS.FETCH,
          guest.familyId
        );
      }

      this.close();
    },
    onFirstNameInput() {
      if (this.firstName && this.firstName.length) {
        this.firstNameState = true;
      } else {
        this.lastNameState = false;
      }
    },
    onLastNameInput() {
      if (this.lastName && this.lastName.length) {
        this.lastNameState = true;
      } else {
        this.lastNameState = false;
      }
    },
    close() {
      this.firstName = "";
      this.lastName = "";
      this.isChild = false;
      this.parentId = null;
      this.isWeddingMember = false;
      this.familyId = null;
      this.firstNameState = null;
      this.lastNameState = null;
      this.isBusy = false;
      this.$emit("closed");
    },
  },
};
</script>

<style scoped>
</style>