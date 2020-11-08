<template>
    <b-modal :visible="visible"
        v-if="visible"
        size="med"
        title="Upload a new Icon"
        hide-footer
    >
        <b-container>
            <RegistryIconForm @close="onClose" />
        </b-container>
    </b-modal>
</template>

<script>
import RegistryIconForm from "@/components/forms/RegistryIconForm";

export default {
    name: "NewIconModal",
    props: [ "visible" ],
    components: {
        RegistryIconForm
    },
    methods: {
        onClose(id) {
            this.$emit("close", id);
        }
    }
}
</script>