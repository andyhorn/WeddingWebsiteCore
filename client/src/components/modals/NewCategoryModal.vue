<template>
    <b-modal :visible="visible"
        v-if="visible"
        id="new-category-modal"
        title="Create a New Category"
        @hide="onClose"
        hide-footer
    >
        <b-container>
            <CategoryForm @close="onClose" />
        </b-container>
    </b-modal>
</template>

<script>
import CategoryForm from "@/components/forms/CategoryForm";

export default {
    name: "NewCategoryModal",
    props: ["visible"],
    components: {
        CategoryForm
    },
    methods: {
        onClose(success) {
            this.$emit("close", success);
        },
    }
}
</script>