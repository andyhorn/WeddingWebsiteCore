<template>
    <b-container>
        <b-form @submit.prevent="onSubmit">
            <b-row>
                <b-col>
                    <b-form-group label="Name" :state="nameState">
                        <b-form-input v-model="name" :state="nameState" />
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-form-group label="URL" :state="urlState">
                        <b-form-input v-model="url" :state="urlState" />
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col class="d-flex justify-content-between">
                    <b-button squared size="sm" variant="success" type="submit" :disabled="!isFormValid">Save</b-button>
                    <b-button squared size="sm" variant="danger" @click="onCancel">Cancel</b-button>
                </b-col>
            </b-row>
        </b-form>
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "RegistryForm",
    props: [ "registry" ],
    data() {
        return {
            id: null,
            name: null,
            url: null,
            nameState: null,
            urlState: null
        }
    },
    watch: {
        "name": function () {
            this.nameState = !!this.name && !!this.name.trim();
        },
        "url": function () {
            this.urlState = !!this.url && !!this.url.trim();
        },
        "registry": {
            immediate: true,
            deep: true,
            handler: function () {
                if (this.registry == null) return;

                this.id = this.registry.registryId;
                this.name = this.registry.name;
                this.url = this.registry.url;
            }
        }
    },
    computed: {
        isFormValid() {
            return this.nameState === true
                && this.urlState === true;
        }
    },
    methods: {
        clear() {
            this.id = null;
            this.name = null;
            this.url = null;
        },
        resetStates() {
            this.nameState = null;
            this.urlState = null;
        },
        close(id) {
            this.$emit("close", id);
            this.clear();
            this.resetStates();
        },
        async onSubmit() {
            if (!this.isFormValid) return;

            const registry = {
                registryId: this.id || undefined,
                name: this.name,
                url: this.url
            };

            const command = this.id == null
                ? ACTIONS.REGISTRY_ACTIONS.CREATE
                : ACTIONS.REGISTRY_ACTIONS.UPDATE;

            const success = await this.$store.dispatch(command, registry);
            if (success) {
                Toast.success("Registry saved!");
                this.close(success);
            } else {
                Toast.error("Unable to save registry.");
            }
        },
        onCancel() {
            this.close(null);
        }
    }
}
</script>