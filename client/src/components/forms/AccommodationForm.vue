<template>
    <b-form @submit.prevent="onSubmit">
        <b-row>
            <b-col>
                <b-form-group label="Name" :state="nameState" description="Required">
                    <b-form-input v-model="name" :state="nameState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Description">
                    <b-form-textarea v-model="description" />
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
            <b-col>
                <b-form-group label="Location">
                    <b-select v-model="addressId">
                        <option :value="null">None</option>
                        <option v-for="address in addresses"
                            :key="address.addressId"
                            :value="address.addressId"
                        >
                            {{ address.name }}
                        </option>
                    </b-select>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col class="d-flex justify-content-between">
                <b-button squared size="sm" variant="success" type="submit">Save</b-button>
                <b-button squared size="sm" variant="danger" @click="onCancel">Cancel</b-button>
            </b-col>
        </b-row>
    </b-form>
</template>

<script>
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "AccommodationForm",
    props: ["accommodation"],
    data() {
        return {
            id: null,
            name: null,
            description: null,
            addressId: null,
            categoryId: null,
            nameState: null,
            categoryState: null
        }
    },
    watch: {
        "name": function () {
            this.nameState = !!this.name && !!this.name.trim();
        },
        "categoryId": function () {
            this.categoryState = this.categoryId != null;
        },
        "accommodation": {
            immediate: true,
            deep: true,
            handler: function () {
                if (this.accommodation != null) {
                    this.id = this.accommodation.accommodationId;
                    this.name = this.accommodation.name;
                    this.description = this.accommodation.description;
                    this.addressId = this.accommodation.addressId;
                    this.categoryId = this.accommodation.categoryId;
                }
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
            return this.nameState !== true || this.categoryState !== true;
        }
    },
    methods: {
        close(success) {
            this.$emit("close", success);
            this.clear();
            this.resetStates();
        },
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
        onCancel() {
            this.close(false);
        },
        async onSubmit() {
            if (this.nameState !== true || this.categoryState !== true)
                return;

            const accommodation = {
                accommodationId: this.id || undefined,
                name: this.name,
                description: this.description,
                addressId: this.addressId,
                categoryId: this.categoryId
            };

            const command = this.id == null
                ? ACTIONS.ACCOMMODATION_ACTIONS.CREATE
                : ACTIONS.ACCOMMODATION_ACTIONS.UPDATE;

            const success = await this.$store.dispatch(command, accommodation);
            if (!!success) {
                await this.$store.dispatch(ACTIONS.ACCOMMODATION_ACTIONS.FETCH_ALL);
                this.close(true);
            } else {
                Toast.error(this, "Unable to save accommodation.");
            }
        }
    }
}
</script>