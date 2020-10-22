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
import { ACTIONS } from "@/store";

export default {
    name: "GuestWeddingRoleFormList",
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
                    const found = role.guestIds && role.guestIds.includes(this.guestId);

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
            // for each role
            await Promise.all(this.roles.map(role => {
                return new Promise(async resolve => {
                    // check the value of the role and whether an existing link exists
                    const assigned = this.values[role.weddingRoleId];
                    const exists = role.guestIds && role.guestIds.includes(this.guestId);
                    let changed = false;

                    // if the role has been assigned and a role does not exist, create a new assignment
                    if (assigned && !exists) {
                        role.guestIds ? role.guestIds.push(this.guestId) : role.guestIds = [this.guestId];
                        changed = true;
                    }
                    // if the role is not assigned and an assignment exists, remove the assignment
                    else if (!assigned && exists) {
                        role.guestIds = role.guestIds.filter(x => x != this.guestId);
                        changed = true;
                    }

                    if (changed) {
                        const success = await this.$store.dispatch(ACTIONS.WEDDING_ROLE_ACTIONS.UPDATE, role);

                        if (success) {
                            this.$set(this.savedValues, this.guestId, assigned);
                        }
                    }

                    resolve();
                });
            }));
        },
        onReset() {
            for (let role of this.roles) {
                this.$set(this.values, role.weddingRoleId, this.savedValues[role.weddingRoleId]);
            }
        },
        onRoleChanged(e, roleId) {
            this.$set(this.values, roleId, e);
        }
    }
}
</script>