<template>
    <b-modal :visible="visible" v-if="visible" title="Create a New Wedding Party Role" hide-footer>
        <WeddingRoleForm @close="onClose" />
    </b-modal>
</template>

<script>
import WeddingRoleForm from "@/components/forms/WeddingRoleForm";

export default {
    name: "NewWeddingRoleModal",
    components: {
        WeddingRoleForm
    },
    props: [ "visible" ],
    methods: {
        onClose(id) {
            this.$emit("close", id);
        }
    }
}
</script>