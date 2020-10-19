<template>
  <b-modal
    :visible="visible"
    v-if="visible"
    id="new-guest-modal"
    title="Add New Guest"
    hide-footer
  >
    <GuestForm @close="onGuestFormClose" :selectedFamily="familyId" />
  </b-modal>
</template>

<script>
import { ACTIONS } from "@/store";
import GuestForm from "@/components/forms/GuestForm";
const Toast = require("@/helpers/toast");

export default {
  name: "NewGuestModal",
  props: ["familyId", "visible"],
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
