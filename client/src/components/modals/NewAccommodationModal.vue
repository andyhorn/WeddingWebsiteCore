<template>
    <b-modal :visible="visible" id="new-accommodation-modal"
        title="Create a New Accommodation"
        @hide="onClose"
        hide-footer
    >
        <b-container>
            <b-form @submit.prevent="onSubmit">
                <b-row>
                    <b-col>
                        <b-form-group label="Name" 
                            :state="nameState"
                            description="Required"
                        >
                            <b-form-input v-model="name" :state="nameState" />
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <b-form-group label="Description">
                            <b-form-textarea v-model="Description" />
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <b-form-group label="Location">
                            <b-select v-model="addressId">
                                <option :value="null">None</option>
                                <option v-for="address in addresses"
                                    :key="address.addressId"
                                    :value="address.addressId"
                                >
                                    {{ address.Name }}
                                </option>
                            </b-select>
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <b-form-group label="Category" :state="categoryState" description="Required">
                            <b-select v-model="categoryId" :state="categoryState">
                                <option :value="null">None</option>
                                <option v-for="category in categories"
                                    :key="category.categoryId"
                                    :value="category.categoryId"
                                >
                                    {{ category.name }}
                                </option>
                            </b-select>
                        </b-form-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col class="d-flex justify-content-between">
                        <b-button squared size="sm" variant="success" type="submit" :disabled="isSaveButtonDisabled">Save</b-button>
                        <b-button squared size="sm" variant="danger" @click="onCancel">Cancel</b-button>
                    </b-col>
                </b-row>
            </b-form>
        </b-container>
    </b-modal>
</template>

<script>
import { ACTIONS } from "@/store";

export default {
    name: "NewAccommodationModal",
    props: ["visible"],
    data() {
        return {
            name: null,
            description: null,
            addressId: null,
            categoryId: null,
            nameState: null,
            categoryState: null
        }
    },
    watch: {
        "name": function() {
            if (!!this.name && !!this.name.trim()) {
                this.nameState = true;
            } else {
                this.nameState = false;
            }
        },
        "categoryId": function() {
            if (this.categoryId != null) {
                this.categoryState = true;
            } else {
                this.categoryState = false;
            }
        }
    },
    computed: {
        addresses() {
            return this.$store.getters.addresses;
        },
        categories() {
            return this.$store.getters.categories;
        },
        isSaveButtonDisabled() {
            return this.nameState !== true
                || this.categoryState !== true;
        }
    },
    methods: {
        clear() {
            this.name = null;
            this.description = null;
            this.addressId = null;
            this.categoryId = null;
        },
        resetStates() {
            this.nameState = null;
            this.categoryState = null;
        },
        close() {
            this.$emit("closed");
            this.clear();
        },
        onCancel() {
            this.close();
        },
        async onSubmit() {
            if (this.nameState !== true || this.categoryState !== true) return;

            const accommodation = {
                name: this.name,
                description: this.description,
                addressId: this.addressId,
                categoryId: this.categoryId
            };

            await this.$store.dispatch(ACTIONS.ACCOMMODATION_ACTIONS.CREATE, accommodation);
            
            this.close();
        }
    }
}
</script>