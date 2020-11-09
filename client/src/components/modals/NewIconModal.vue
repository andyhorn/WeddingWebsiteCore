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
import { ACTIONS } from "@/store";
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
            if (id) {
                this.$store.dispatch(ACTIONS.REGISTRY_ICONS.FETCH, id);
            }
        }
    }
}
</script>