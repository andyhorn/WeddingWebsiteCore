<template>
    <b-modal :visible="visible" v-if="visible"
        title="Create a New Registry Link" hide-footer
    >
        <b-container>
            <RegistryForm @close="onClose" />
        </b-container>
    </b-modal>
</template>

<script>
import RegistryForm from "@/components/forms/RegistryForm";

export default {
    name: "NewRegistryModal",
    props: [ "visible" ],
    components: {
        RegistryForm
    },
    methods: {
        onClose(id) {
            this.$emit("close", id);
        }
    }
}
</script>