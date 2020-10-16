<template>
    <b-modal :visible="visible" v-if="visible" id="new-accommodation-modal"
        title="Create a New Accommodation"
        hide-footer
    >
        <b-container>
            <AccommodationForm @close="onClose" />
        </b-container>
    </b-modal>
</template>

<script>
import AccommodationForm from "@/components/forms/AccommodationForm";
const Toast = require("@/helpers/toast");

export default {
    name: "NewAccommodationModal",
    props: ["visible"],
    components: {
        AccommodationForm
    },
    methods: {
        onClose(success) {
            this.$emit("close", success);
        }
    }
}
</script>