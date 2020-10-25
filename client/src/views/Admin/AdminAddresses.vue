<template>
  <b-container class="pt-5">
        <h2 class="text-center">Addresses</h2>
    <div class="d-flex justify-content-between align-items-center mb-2">
      <b-button squared size="sm" variant="success" @click="isNewAddressModalVisible = true"><b-icon-plus /> New Address</b-button>
      <b-button squared size="sm" variant="primary" @click="fetch">
        <b-icon-arrow-clockwise />Refresh
      </b-button>
    </div>
    <b-table :items="addresses" :fields="fields" selectable select-mode="single" @row-selected="onAddressSelected" show-empty>
      <template v-slot:cell(options)="data">
        <b-button class="text-danger" variant="link" squared size="sm" @click="onDeleteAddress(data.item.addressId)">Delete</b-button>
      </template>
    </b-table>
    <b-collapse v-model="isAddressEditVisible">
      <b-container class="border border-secondary rounded my-5 p-3">
        <h3>Edit Address</h3>
        <AddressForm :address="addressUnderEdit" @close="onAddressEditClose" v-if="isAddressEditVisible" />
      </b-container>
    </b-collapse>
    <NewAddressModal :visible="isNewAddressModalVisible" @close="onNewAddressModalClose" />
  </b-container>
</template>

<script>
import NewAddressModal from "@/components/modals/NewAddressModal";
import AddressForm from "@/components/forms/AddressForm";
import { ACTIONS } from "@/store";
import arraySort from "@/helpers/arraySort";
const Toast = require("@/helpers/toast");

export default {
  name: "AdminAddresses",
  components: {
    AddressForm,
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
      addressUnderEdit: null,
      isNewAddressModalVisible: false,
      isAddressEditVisible: false
    };
  },
  computed: {
    addresses() {
      return arraySort(this.$store.getters.addresses, "name");
    },
  },
  mounted() {
    this.fetch();
  },
  methods: {
    async fetch() {
      await this.$store.dispatch(ACTIONS.ADDRESSES.FETCH_ALL);
    },
    async onDeleteAddress(addressId) {
      const deleteAddress = confirm(
        "Are you sure you want to delete this address?"
      );

      if (deleteAddress) {
        await this.$store.dispatch(ACTIONS.ADDRESSES.DELETE, addressId);
      }
    },
    onNewAddressModalClose(success) {
      this.isNewAddressModalVisible = false;
    },
    onAddressSelected(rows) {
      if (rows.length > 0) {
        this.addressUnderEdit = rows[0];
        this.isAddressEditVisible = true;
      } else {
        this.isAddressEditVisible = false;
        this.addressUnderEdit = null;
      }
    },
    onAddressEditClose(success) {
      this.isAddressEditVisible = false;
    }
  },
};
</script>