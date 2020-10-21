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
                    <b-form-group label="Description">
                        <b-form-textarea v-model="description" />
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
    name: "WeddingRoleForm",
    props: [ "weddingRole" ],
    data() {
        return {
            name: null,
            description: null,
            nameState: null
        }
    },
    watch: {
        "name": function () {
            this.nameState = !!this.name && !!this.name.trim();
        },
        "weddingRole": {
            immediate: true,
            deep: true,
            handler: function () {
                if (!this.weddingRole) return;

                this.name = this.weddingRole.name;
                this.description = this.weddingRole.description;
            }
        }
    },
    computed: {
        isFormValid() {
            return this.nameState === true;
        }
    },
    methods: {
        clear() {
            this.name = null;
            this.description = null;
        },
        resetStates() {
            this.nameState = null;
        },
        close(id) {
            this.$emit("close", id);
            this.clear();
            this.resetStates();
        },
        onCancel() {
            this.close(null);
        },
        async onSubmit() {
            const role = Object.assign({}, this.weddingRole, {
                name: this.name,
                description: this.description
            });

            const command = !!this.weddingRole 
                ? ACTIONS.WEDDING_ROLE_ACTIONS.UPDATE
                : ACTIONS.WEDDING_ROLE_ACTIONS.CREATE;

            const success = await this.$store.dispatch(command, role);
            if (success) {
                Toast.success("Role saved!");
                this.close(success);
            } else {
                Toast.error("Unable to save role.");
            }
        }
    }
}
</script>