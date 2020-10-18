<template>
  <Box class="py-2 mb-3">
    <b-container>
      <b-row>
        <b-col class="d-flex justify-content-start align-items-center">
          <b-button size="sm" class="mr-3" variant="primary" @click="toggleCollapse">
            <b-icon :icon="collapseOpen ? 'chevron-up' : 'chevron-down'" />
          </b-button>
          <p class="m-0 p-0" v-if="headMember">
            <span class="text-italic">{{ headMember.firstName }} {{ headMember.lastName }}</span>
            and family
          </p>
          <p class="m-0 p-0" v-else>
            <span class="text-italic">{{ family.name }}</span> family
          </p>
        </b-col>
        <b-col class="d-flex justify-content-end align-items-center">
          <FamilyRsvpInviteForm :familyId="family.familyId" class="mr-3" />
          <a href @click.prevent="onDeleteFamily">
            <b-icon-x-circle />
          </a>
        </b-col>
      </b-row>
      <b-collapse :id="collapseId" class="mt-3">
        <p v-if="members.length == 1" class="m-0 p-0">No other members.</p>
        <b-button
          squared
          size="sm"
          variant="success"
          class="my-2"
          @click="openGuestModal"
        >Add member</b-button>
        <Box class="mb-3" v-if="headMember">
          <Guest :guest="headMember" :headMemberId="headMember.guestId" />
        </Box>
        <Box v-for="member in nonHeadMembers" :key="member.guestId">
          <b-container>
            <b-row>
              <b-col>
                <Guest :guest="member" :headMemberId="headMember ? headMember.guestId : null" />
              </b-col>
            </b-row>
          </b-container>
        </Box>
        <Box class="mt-3">
          <b-form-group label="Address">
            <b-input-group>
              <b-select v-model="family.addressId" @input="onAddressChange">
                <option :value="null">None</option>
                <option
                  v-for="address in addresses"
                  :key="address.addressId"
                  :value="address.addressId"
                >
                  <span v-if="!!address.name">({{ address.name }})</span>
                  {{ address.fullString }}
                </option>
              </b-select>
              <b-input-group-append>
                <b-button variant="primary" @click="toggleAddressCollapse">New address</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
          <b-collapse :id="addressCollapseId" class="p-3" v-model="showNewAddressForm">
              <AddressForm @close="onAddressClose" />
          </b-collapse>
        </Box>
      </b-collapse>
    </b-container>
    <b-toast
      :id="addressSavedToastId"
      variant="success"
      title="Address saved!"
    >Successfully updated address for {{ family.name }} family.</b-toast>
    <b-toast
      :id="addressNotSavedToastId"
      variant="danger"
      title="Error!"
    >Oops! We weren't able to save the new address for the {{ family.name }} family.</b-toast>
  </Box>
</template>

<script>
import { v4 as uuidv4 } from "uuid";
import { ACTIONS } from "@/store";
import Box from "@/components/Box.vue";
import Guest from "@/components/Admin/Guests/Guest.vue";
import AddressForm from "@/components/forms/AddressForm";
import FamilyRsvpInviteForm from "@/components/Admin/Guests/FamilyRsvpInviteForm";

export default {
  name: "Family",
  props: ["family"],
  components: {
    Box,
    Guest,
    AddressForm,
    FamilyRsvpInviteForm,
  },
  data() {
    return {
      collapseOpen: false,
      collapseId: uuidv4(),
      addressCollapseId: uuidv4(),
      addressSavedToastId: uuidv4(),
      addressNotSavedToastId: uuidv4(),
      showNewAddressForm: false,
    };
  },
  computed: {
    addresses() {
      return this.$store.getters.addresses;
    },
    headMember() {
      if (!this.headMemberId) return null;

      return this.members.find((m) => m.guestId == this.family.headMemberId);
    },
    nonHeadMembers() {
      const members = this.members.filter(
        (x) => x.guestId != this.family.headMemberId
      );
      const sorted = members.sort((a, b) => {
        if (a.firstName == b.firstName) return 0;
        return a.firstName < b.firstName ? -1 : 1;
      });
      return sorted;
    },
    members() {
      return this.$store.getters.guestsInFamily(this.family.familyId);
    },
  },
  methods: {
    toggleCollapse() {
      this.$root.$emit("bv::toggle::collapse", this.collapseId);
      this.collapseOpen = !this.collapseOpen;
    },
    toggleAddressCollapse() {
      this.showNewAddressForm = !this.showNewAddressForm;
    },
    openGuestModal() {
      this.$emit("newGuest", this.family.familyId);
    },
    showAddressSavedToast() {
      this.$bvToast.show(this.addressSavedToastId);
    },
    showAddressNotSavedToast() {
      this.$bvToast.show(this.addressNotSavedToastId);
    },
    onAddressClose(id) {
      if (id) {
        this.family.addressId = id;
        this.onAddressChange();
      }
    },
    async promoteGuest(guestId) {
      let family = { ...this.family };
      family.headMemberId = guestId;
      await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.UPDATE, family);
    },
    async onDeleteFamily() {
      const name = this.family.name;

      if (confirm("Are you sure you want to delete the " + name + " family?")) {
        const id = this.family.familyId;
        await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.DELETE, id);

        const affectedGuests = this.$store.getters.guestsInFamily(id);
        for (let guest of affectedGuests) {
          guest.familyId = null;
          await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.UPDATE, guest);
        }
      }
    },
    async onAddressChange(addressId) {
      const updated = await this.$store.dispatch(
        ACTIONS.FAMILY_ACTIONS.UPDATE,
        this.family
      );

      if (updated) {
        this.showAddressSavedToast();
        this.showNewAddressForm = false;
      } else {
        this.showAddressNotSavedToast();
      }
    },
  },
};
</script>