<template>
  <b-modal
    :visible="visible"
    v-if="visible"
    id="new-guest-modal"
    title="Add New Guest"
    hide-footer
  >
    <GuestForm @close="onGuestFormClose" />
  </b-modal>
</template>

<script>
import { ACTIONS } from "@/store";
import GuestForm from "@/components/forms/GuestForm";
const Toast = require("@/helpers/toast");

export default {
  name: "NewGuestModal",
  props: ["families", "selectedFamilyId", "visible"],
  components: {
    GuestForm
  },
  data() {
    return {
    };
  },
  computed: {
  },
  methods: {
    onGuestFormClose(id) {
      this.$emit("close", id);
    }
  },
};
</script>

<style scoped>
.fade-in-enter-active,
.fade-in-leave-active {
  transition: opacity 400ms;
}
.fade-in-enter,
.fade-in-leave-to {
  opacity: 0;
}
</style>