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
        <Box class="mb-3">
          <Guest :guest="members.find(g => g.guestId == family.headMemberId)" />
        </Box>
        <Box
          v-for="member in members.filter(g => g.guestId != family.headMemberId)"
          :key="member.guestId"
        >
          <div class="d-flex align-items-center">
            <a
              @click="promoteGuest(member.guestId)"
              v-if="!member.isChild && family.headMemberId != member.guestId"
            >
              <b-icon-arrow-bar-up />
            </a>
            <Guest :guest="member" />
          </div>
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
            <NewAddressForm @saved="id => family.addressId = id" />
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
import NewAddressForm from "@/components/forms/NewAddressForm";

export default {
  name: "Family",
  props: ["family"],
  components: {
    Box,
    Guest,
    NewAddressForm,
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
      return this.members.find((m) => m.guestId == this.family.headMemberId);
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
      console.log("address changed; updating family");
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