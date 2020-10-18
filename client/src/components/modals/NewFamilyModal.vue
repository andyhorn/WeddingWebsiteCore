<template>
  <b-modal
    :visible="visible"
    v-if="visible"
    id="new-family-modal"
    title="Create New Family"
    hide-footer
  >
    <FamilyForm @close="onFamilyFormClose" />
  </b-modal>
</template>

<script>
import FamilyForm from "@/components/forms/FamilyForm";

export default {
    name: "NewFamilyModal",
    props: ["visible"],
    components: {
      FamilyForm
    },
    methods: {
      onFamilyFormClose(id) {
        this.$emit("close", id);
      }
    }
}
</script>