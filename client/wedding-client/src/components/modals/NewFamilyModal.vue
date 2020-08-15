<template>
  <b-modal
    :visible="visible"
    id="new-family-modal"
    title="Create New Family"
    @hide="onCancel"
    hide-footer
  >
    <form @submit.prevent="onSubmit">
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
                v-model="familyName"
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
              <b-select v-model="headMemberId">
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
      <b-button type="submit">Save</b-button>
    </form>
  </b-modal>
</template>

<script>
import { ACTIONS } from "@/store";

export default {
    name: "NewFamilyModal",
    props: ["guests", "visible"],
    data() {
        return {
            familyName: "",
            headMemberId: null,
            familyNameState: null
        }
    },
    methods: {
        onCancel() {
            this.close();
        },
        async onSubmit() {
            const family = {
                familyName: this.familyName,
                headMemberId: this.headMemberId
            };

            const familyId = await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.CREATE, family);

            if (familyId && this.headMemberId) {
                const headMember = await this.$store.getters.findGuest(this.headMemberId);
                if (headMember) {
                    headMember.familyId = familyId;
                    await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.UPDATE, headMember);
                }
            }

            this.close();
        },
        onFamilyNameInput() {
            if (this.familyName && this.familyName.length) {
                this.familyNameState = true;
            } else {
                this.familyNameState = false;
            }
        },
        close() {
            this.familyName = "";
            this.headMemberId = null;
            this.familyNameState = null;
            this.$emit("closed");
        }
    }
}
</script>