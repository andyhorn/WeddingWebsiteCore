<template>
    <b-form @submit.prevent="onSubmit">
        <b-container>
            <b-row>
                <b-col>
                    <b-form-group label="Family Name" :state="nameState">
                        <b-form-input v-model="name" :state="nameState" />
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-form-group label="Family Head">
                        <b-select v-model="headMemberId">
                            <option :value="null">None</option>
                            <option v-for="guest in potentialHeads"
                                :key="guest.guestId"
                                :value="guest.guestId"
                            >
                                {{ `${guest.firstName} ${guest.lastName} `}}
                            </option>
                        </b-select>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col class="d-flex justify-content-between">
                    <b-button squared size="sm" variant="success" type="submit" :disabled="!isFormValid">Save</b-button>
                    <b-button squared size="sm" variant="danger" @click="onCancel">Cancel</b-button>
                </b-col>
            </b-row>
        </b-container>
    </b-form>
</template>

<script>
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "FamilyForm",
    props: [ "family" ],
    data() {
        return {
            id: null,
            name: null,
            headMemberId: null,
            nameState: null
        }
    },
    computed: {
        potentialHeads() {
            return this.$store.getters.guests
                .filter(x => !x.isChild)
                .filter(x => !x.familyId)
                .sort((a, b) => a.name < b.name ? -1 : a.name > b.name ? 1 : 0);
        },
        isFormValid() {
            return this.nameState === true;
        }
    },
    watch: {
        "name": function () {
            this.nameState = !!this.name && !!this.name.trim();
        },
        "family": {
            immediate: true,
            deep: true,
            handler: function () {
                if (this.family == null) return;

                this.id = this.family.familyId;
                this.name = this.family.name;
                this.headMemberId = this.family.headMemberId;
            }
        }
    },
    methods: {
        clear() {
            this.id = null;
            this.name = null;
            this.headMemberId = null;
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
            if (!this.isFormValid) return;

            const family = {
                familyId: this.id || undefined,
                name: this.name,
                headMemberId: this.headMemberId
            };

            const command = this.id == null
                ? ACTIONS.FAMILIES.CREATE
                : ACTIONS.FAMILIES.UPDATE;

            const success = await this.$store.dispatch(command, family);
            if (success) {
                Toast.success("Family saved!");
                await this.updateMembers(this.familyId || success);
                this.close(success);
            } else {
                Toast.error("Unable to save family.");
            }
        },
        async updateMembers(familyId) {
            await this.$store.dispatch(ACTIONS.FAMILIES.FETCH, familyId);
            const memberIds = this.$store.getters
                .findFamily(familyId)
                .members
                .map(y => y.guestId);

            const refreshes = this.$store.getters.guests
                .filter(x => memberIds.includes(x.guestId))
                .map(x => this.$store.dispatch(ACTIONS.GUESTS.FETCH, x.guestId));

            await Promise.all(refreshes);
        }
    }
}
</script>