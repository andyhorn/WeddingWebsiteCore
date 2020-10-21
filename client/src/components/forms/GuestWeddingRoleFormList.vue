<template>
    <b-container class="border border-secondary rounded p-4">
        <b-row class="mb-4">
            <b-col cols="4" v-for="role in roles" :key="`guest${guestId}_role${role.weddingRoleId}`" class="px-1 py-2">
                <b-form-checkbox switch v-model="values[role.weddingRoleId]"
                    @change="onRoleChanged($event, role.weddingRoleId)"><small>{{ role.name }}</small></b-form-checkbox>
            </b-col>
        </b-row>
        <b-row class="mb-3">
            <b-col class="d-flex justify-content-between">
                <b-button squared size="sm" variant="success" @click="onSave" :disabled="isSaveButtonDisabled">Save</b-button>
                <b-button squared size="sm" variant="warning" @click="onReset" class="text-light">Reset</b-button>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
import GuestWeddingRoleForm from "@/components/forms/GuestWeddingRoleForm";
import { ACTIONS } from "@/store";

export default {
    name: "GuestWeddingRoleFormList",
    components: {
        GuestWeddingRoleForm
    },
    props: [ "roles", "guestId" ],
    data() {
        return {
            savedValues: { },
            values: { }
        }
    },
    watch: {
        "roles": {
            deep: true,
            immediate: true,
            handler: function () {
                for (let role of this.roles) {
                    const found = role.guestWeddingRoles.find(x => x.guestId == this.guestId) != null;

                    this.$set(this.values, role.weddingRoleId, found);
                    this.$set(this.savedValues, role.weddingRoleId, found);
                }
            }
        },
    },
    computed: {
        isSaveButtonDisabled() {
            for (let role of this.roles) {
                if (this.values[role.weddingRoleId] != this.savedValues[role.weddingRoleId]) {
                    return false;
                }
            }

            return true;
        }
    },
    methods: {
        async onSave() {
            await Promise.all(this.roles.map(role => {
                const value = this.values[role.weddingRoleId];
                const guestWeddingRole = role.guestWeddingRoles.find(x => x.guestId == this.guestId);

                if (value && !guestWeddingRole) {
                    // create a new link
                    const weddingRole = {
                        guestId: this.guestId,
                        weddingRoleId: role.weddingRoleId
                    };

                    return await this.$store.dispatch(ACTIONS.WEDDING_ROLE_ACTIONS.CREATE, weddingRole);
                } else if (!value && !!guestWeddingRole) {
                    // delete an existing link
                    return await this.$store.dispatch(ACTIONS.WEDDING_ROLE_ACTIONS.DELETE, guestWeddingRole.guestWeddingRoleId)
                }

                return null;
            }));
        },
        onReset() {
            for (let role of this.roles) {
                this.$set(this.values, role.weddingRoleId, this.savedValues[role.weddingRoleId]);
            }
        },
        onRoleChanged(e, roleId) {
            // this.values[roleId] = e;
            this.$set(this.values, roleId, e);
        }
    }
}
</script>