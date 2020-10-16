<template>
    <b-form @submit.prevent="onSubmit">
        <b-row>
            <b-col>
                <b-form-group label="Name" :state="nameState" description="Required" >
                    <b-form-input v-model="name" :state="nameState" />
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
                <b-button type="submit" squared size="sm" variant="success" :disabled="isSaveButtonDisabled">Save</b-button>
                <b-button squared size="sm" variant="danger" @click="onClose">Cancel</b-button>
            </b-col>
        </b-row>
    </b-form>
</template>

<script>
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "CategoryForm",
    props: ["category", "visible"],
    data() {
        return {
            id: null,
            name: null,
            parentId: null,
            nameState: null
        }
    },
    computed: {
        possibleParents() {
            const categories = this.$store.getters.categories;

            if (this.category && this.category.categoryId)
                return categories.filter(x => x.categoryId != this.category.categoryId);

            return categories;
        },
        isSaveButtonDisabled() {
            return this.nameState !== true;
        }
    },
    watch: {
        "category": {
            immediate: true,
            deep: true,
            handler: function (value) {
                if (value != null) {
                    this.id = value.categoryId || null;
                    this.name = value.name || null;
                    this.parentId = value.parentId || null;
                }
            }
        },
        "name": function () {
            if (!!this.name && !!this.name.trim()) {
                this.nameState = true;
            } else {
                this.nameState = false;
            }
        },
        "visible": function (newValue, previousValue) {
            if (!newValue && previousValue) {
                this.clear();
                this.resetStates();
            }
        }
    },
    methods: {
        clear() {
            this.name = null;
            this.id = null;
            this.parentId = null;
        },
        resetStates() {
            this.nameState = null;
        },
        close(success) {
            this.$emit("close", success);
            this.clear();
            this.resetStates();
        },
        onClose() {
            this.close(false);
        },
        async onSubmit() {
            if (!this.nameState) return;

            const category = {
                categoryId: this.id || undefined,
                name: this.name,
                parentId: this.parentId
            };

            const command = this.id == null
                ? ACTIONS.CATEGORY_ACTIONS.CREATE
                : ACTIONS.CATEGORY_ACTIONS.UPDATE;

            const success = await this.$store.dispatch(command, category);

            if (!!success) {
                await this.$store.dispatch(ACTIONS.CATEGORY_ACTIONS.FETCH_ALL);
                this.close(true);
            } else {
                Toast.error(this, "Unable to save category.");
            }
        }
    }
}
</script>