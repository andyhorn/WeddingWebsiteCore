<template>
    <b-modal :visible="visible" id="new-category-modal"
        title="Create a New Category"
        @hide="onClose"
        hide-footer
    >
        <b-container>
            <b-form @submit.prevent="onSubmit">
                <b-row>
                    <b-col>
                        <b-form-group label="Name">
                            <b-form-input v-model="name" />
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <b-form-group label="Parent">
                            <b-form-select v-model="parentId">
                                <option :value="null">None</option>
                                <option v-for="possibleParent in possibleParents"
                                    :key="possibleParent.categoryId"
                                    :value="possibleParent.categoryId"
                                >
                                    {{ possibleParent.name }}
                                </option>
                            </b-form-select>
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col class="d-flex justify-content-between">
                        <b-button type="submit" squared size="sm" variant="success">Save</b-button>
                        <b-button squared size="sm" variant="danger" @click="onClose">Cancel</b-button>
                    </b-col>
                </b-row>
            </b-form>
        </b-container>
    </b-modal>
</template>

<script>
import { ACTIONS } from "@/store";
const actions = ACTIONS.CATEGORY_ACTIONS;
const Toast = require("@/helpers/toast");

export default {
    name: "NewCategoryModal",
    props: ["visible"],
    computed: {
        possibleParents() {
            return this.$store.getters.categories;
        }
    },
    data() {
        return {
            name: null,
            parentId: null
        }
    },
    methods: {
        async onSubmit() {
            const category = {
                name: this.name,
                parentId: this.parentId
            };

            const categoryId = await this.$store.dispatch(actions.CREATE, category);
            if (categoryId != null) {
                Toast.success(this, "Category saved!");
                this.onClose();
            } else {
                Toast.error(this, "Unable to save new Category");
            }
        },
        onClose() {
            this.$emit("closed");
            this.clear();
        },
        clear() {
            this.name = null;
            this.parentId = null;
        }
    }
}
</script>