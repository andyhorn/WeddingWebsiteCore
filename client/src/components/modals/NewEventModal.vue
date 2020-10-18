<template>
  <b-modal size="lg" 
    :visible="visible"
    v-if="visible"
    hide-footer 
    title="New Event">
    <EventForm @close="onEventClose" />
    <b-collapse
      id="newAddressCollapse"
      class="border rounded p-3 mb-5"
      v-model="showNewAddressForm"
    >
      <AddressForm @close="onAddressSave" />
    </b-collapse>
      
  </b-modal>
</template>

<script>
import AddressForm from "@/components/forms/AddressForm";
import EventForm from "@/components/forms/EventForm";
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
  name: "NewEventModal",
  props: ["visible"],
  components: {
    AddressForm,
    EventForm
  },
  data() {
    return {
      showNewAddressForm: false
    }
  },
  methods: {
    onAddressSave(id) {
      this.event.addressId = id;
      this.showNewAddressForm = false;
    },
    onEventClose(id) {
      this.close();
    },
    close() {
      this.$emit("close");
    }
  },
};
</script>

<style scoped>
</style>