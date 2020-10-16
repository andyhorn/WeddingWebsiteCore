<template>
    <b-modal :visible="visible" id="new-category-modal"
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
const Toast = require("@/helpers/toast");

export default {
    name: "NewCategoryModal",
    props: ["visible"],
    components: {
        CategoryForm
    },
    methods: {
        onClose() {
            this.$emit("closed");
        },
    }
}
</script>