<template>
  <b-container class="pt-5">
    <div class="d-flex justify-content-between align-items-center">
      <h2 class="mb-3">Addresses ({{ addresses.length }})</h2>
      <b-button squared size="sm" variant="primary" @click="fetch">
        <b-icon-arrow-clockwise />Refresh
      </b-button>
    </div>
    <b-table :items="addresses" :fields="fields">
      <template v-slot:cell(options)="data">
        <div class="d-flex align-items-center justify-content-around">
          <b-button
            variant="primary"
            squared
            size="sm"
            @click="onEditAddress(data.item.addressId)"
          >Edit</b-button>
          <b-button
            variant="danger"
            squared
            size="sm"
            @click="onDeleteAddress(data.item.addressId)"
          >Delete</b-button>
        </div>
      </template>
    </b-table>
  </b-container>
</template>

<script>
import { ACTIONS } from "@/store";

export default {
  name: "AdminAddresses",
  data() {
    return {
      fields: [
        "name",
        "streetNumber",
        "streetName",
        { key: "streetDetail", label: "Unit/Apt. #" },
        "city",
        "state",
        "postalCode",
        "country",
        "options",
      ],
    };
  },
  computed: {
    addresses() {
      return this.$store.getters.addresses;
    },
  },
  mounted() {
    this.fetch();
  },
  methods: {
    async fetch() {
      await this.$store.dispatch(ACTIONS.ADDRESS_ACTIONS.FETCH_ALL);
    },
    onEditAddress(addressId) {
      this.$router.push({
        name: "Admin-Address-Edit",
        params: { id: addressId },
      });
    },
    async onDeleteAddress(addressId) {
      const deleteAddress = confirm(
        "Are you sure you want to delete this address?"
      );

      if (deleteAddress) {
        await this.$store.dispatch(ACTIONS.ADDRESS_ACTIONS.DELETE, addressId);
      }
    },
  },
};
</script>