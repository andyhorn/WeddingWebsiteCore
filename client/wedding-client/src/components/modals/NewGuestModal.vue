<template>
  <b-modal
    :visible="visible"
    id="new-guest-modal"
    title="Add New Guest"
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
            <b-form-group id="is-child-switch-group" label="Is a Child" label-for="is-child-switch">
              <b-form-checkbox id="is-child-switch" v-model="isChild" switch />
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
    props: ['families', 'visible'],
    data() {
        return {
            firstName: "",
            lastName: "",
            isChild: false,
            familyId: null,
            firstNameState: null,
            lastNameState: null,
            isBusy: false
        }
    },
    methods: {
        onCancel() {
            this.close();
        },
        async onSubmit() {
            this.isBusy = true;
            const guest = {
                firstName: this.firstName,
                lastName: this.lastName,
                isChild: this.isChild,
                familyId: this.familyId
            };

            await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.CREATE, guest);
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
            this.firstName = "",
            this.lastName = "",
            this.isChild = false,
            this.familyId = null
            this.firstNameState = null;
            this.lastNameState = null;
            this.isBusy = false;
            this.$emit("closed");
        }
    }
}
</script>

<style scoped>
</style>