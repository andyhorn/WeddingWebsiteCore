<template>
  <b-modal
    :visible="visible"
    id="new-family-modal"
    title="Create New Family"
    @hide="onCancel"
    hide-footer
  >
    <form @submit.prevent="onSubmit" :disabled="isBusy">
      <b-container>
        <b-row>
          <b-col>
            <b-form-group
              id="family-name-input-group"
              label="Family name"
              label-for="family-name-input"
              invalid-feedback="Family name is required."
              :state="familyNameState"
            >
              <b-form-input
                id="family-name-input"
                v-model="name"
                :state="familyNameState"
                @input="onFamilyNameInput"
                trim
                required
              />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-form-group
              id="head-member-select-group"
              label="Family Head"
              label-for="head-member-select"
            >
              <b-select v-model="headMemberId" :disabled="guests.length == 0">
                <b-select-option
                  v-for="guest in guests"
                  :key="guest.guestId"
                  :value="guest.guestId"
                >{{ guest.firstName }} {{ guest.lastName }}</b-select-option>
              </b-select>
            </b-form-group>
          </b-col>
        </b-row>
      </b-container>
      <b-button type="submit" :disabled="isBusy">
        <b-spinner small v-if="isBusy" />
        <span v-else>Save</span>
      </b-button>
    </form>
  </b-modal>
</template>

<script>
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "NewFamilyModal",
    props: ["guests", "visible"],
    data() {
        return {
            name: "",
            headMemberId: null,
            familyNameState: null,
            isBusy: false
        }
    },
    methods: {
        onCancel() {
            this.close();
        },
        async onSubmit() {
            this.isBusy = true;
            const family = {
                name: this.name,
                headMemberId: this.headMemberId
            };

            const familyId = await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.CREATE, family);
            if (!familyId) {
              Toast.error("Unable to create new family.");
              return;
            }

            if (this.headMemberId) {
              const guest = this.$store.getters.findGuest(this.headMemberId);
              guest.familyId = familyId;
              await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.UPDATE, guest);
            }

            Toast.success("Family saved!");

            this.close();
        },
        onFamilyNameInput() {
            if (this.name && this.name.length) {
                this.familyNameState = true;
            } else {
                this.familyNameState = false;
            }
        },
        close() {
            this.name = "";
            this.headMemberId = null;
            this.familyNameState = null;
            this.isBusy = false;
            this.$emit("closed");
        }
    }
}
</script>