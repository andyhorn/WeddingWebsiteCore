<template>
  <b-container class="pt-5">
        <h2 class="mb-3">Addresses ({{ addresses.length }})</h2>
    <div class="d-flex justify-content-between align-items-center mb-2">
      <b-button squared size="sm" variant="success" @click="isNewAddressModalVisible = true">New Address</b-button>
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
    <NewAddressModal :visible="isNewAddressModalVisible" @close="onNewAddressModalClose" />
  </b-container>
</template>

<script>
import NewAddressModal from "@/components/modals/NewAddressModal";
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
  name: "AdminAddresses",
  components: {
    NewAddressModal
  },
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
      isNewAddressModalVisible: false
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
        name: "Admin-Address-Details",
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
    onNewAddressModalClose(success) {
      if (success) Toast.success(this, "Address saved!");

      this.isNewAddressModalVisible = false;
    }
  },
};
</script>