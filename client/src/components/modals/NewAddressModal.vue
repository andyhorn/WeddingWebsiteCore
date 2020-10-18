<template>
    <b-modal :visible="visible"
        v-if="visible"
        size="lg"
        id="new-address-modal"
        title="Create a New Address"
        hide-footer
    >
        <b-container>
            <AddressForm @close="onClose" />
        </b-container>
    </b-modal>
</template>

<script>
import AddressForm from "@/components/forms/AddressForm";

export default {
    name: "NewAddressModal",
    props: [ "visible" ],
    components: {
        AddressForm
    },
    methods: {
        onClose(id) {
            this.$emit("close", id);
        }
    }
}
</script>