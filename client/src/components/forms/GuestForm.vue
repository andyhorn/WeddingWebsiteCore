<template>
    <b-form @submit.prevent="onSubmit">
        <b-row>
            <b-col>
                <b-form-group label="First Name" :state="firstNameState" description="Required">
                    <b-form-input v-model="firstName" :state="firstNameState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Last Name" :state="lastNameState" description="Required">
                    <b-form-input v-model="lastName" :state="lastNameState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Family">
                    <b-select v-model="familyId">
                        <option :value="null">None</option>
                        <option v-for="family in families"
                            :key="family.familyId"
                            :value="family.familyId"
                        >
                            {{ family.name }}
                        </option>
                    </b-select>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Is a Wedding Party Member?">
                    <b-form-checkbox switch v-model="isWeddingMember" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col cols="3" class="d-flex flex-column align-items-start justify-content-around">
                <b-form-group label="Is a Child?">
                    <b-form-checkbox switch v-model="isChild" />
                </b-form-group>
            </b-col>
            <b-col cols="3" v-if="isChild">
                <b-form-group label="Is Under Ten (10)?">
                    <b-form-checkbox switch v-model="isUnderTen" />
                </b-form-group>
            </b-col>
            <b-col v-if="isChild">
                <b-form-group label="Parent" :state="parentState" description="Required">
                    <b-form-select v-model="parentId" :state="parentState">
                        <option :value="null">None</option>
                        <option v-for="parent in parents"
                            :key="parent.guestId"
                            :value="parent.guestId"
                        >
                            {{ `${parent.firstName} ${parent.lastName}` }}
                        </option>
                    </b-form-select>
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
</template>

<script>
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "GuestForm",
    props: [ "guest" ],
    data() {
        return {
            id: null,
            firstName: null,
            lastName: null,
            familyId: null,
            isChild: false,
            isUnderTen: false,
            isWeddingMember: false,
            parentId: null,
            firstNameState: null,
            lastNameState: null,
            parentState: null
        }
    },
    computed: {
        isFormValid() {
            return this.firstNameState
                && this.lastNameState
                && (this.isChild && this.parentState || !this.isChild && this.parentState == null);
        },
        families() {
            return this.$store.getters.families;
        },
        parents() {
            return this.$store.getters.nonChildren;
        }
    },
    watch: {
        "firstName": function () {
            this.firstNameState = !!this.firstName && !!this.firstName.trim();
        },
        "lastName": function () {
            this.lastNameState = !!this.lastName && !!this.lastName.trim();
        },
        "parentId": function () {
            if (this.isChild)
                this.parentState = this.parentId != null;
            else
                this.parentState = null;
        },
        "isChild": function () {
            if (!this.isChild) {
                this.parentId = null;
                this.isUnderTen = false;
            } else {
                if (this.guest != null) {
                    this.parentId = this.guest.parentId;
                    this.isUnderTen = this.guest.isUnderTen;
                }
            }
        },
        "guest": {
            immediate: true,
            deep: true,
            handler: function () {
                if (this.guest == null) return;

                this.id = this.guest.guestId;
                this.firstName = this.guest.firstName;
                this.lastName = this.guest.lastName;
                this.familyId = this.guest.familyId;
                this.isChild = this.guest.isChild;
                this.isUnderTen = this.guest.isUnderTen;
                this.isWeddingMember = this.guest.isWeddingMember;
                this.parentId = this.guest.parentId;
            }
        }
    },
    methods: {
        clear() {
            this.id = null;
            this.firstName = null;
            this.lastName = null;
            this.familyId = null;
            this.isChild = false;
            this.isUnderTen = false;
            this.isWeddingMember = false;
            this.parentId = null;
        },
        resetStates() {
            this.firstNameState = null;
            this.lastNameState = null;
            this.parentState = null;
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

            const guest = {
                guestId: this.id || undefined,
                firstName: this.firstName,
                lastName: this.lastName,
                familyId: this.familyId,
                isChild: this.isChild,
                isUnderTen: this.isChild ? this.isUnderTen : false,
                isWeddingMember: this.isWeddingMember,
                parentId: this.isChild ? this.parentId : null,
                inviteCode: this.guest == null
                    ? null : this.guest.inviteCode
            };

            const command = this.id == null
                ? ACTIONS.GUEST_ACTIONS.CREATE
                : ACTIONS.GUEST_ACTIONS.UPDATE;

            const success = await this.$store.dispatch(command, guest);
            if (success) {
                const refresh = ACTIONS.GUEST_ACTIONS.FETCH_ALL;
                await this.$store.dispatch(refresh);
                Toast.success("Guest saved!");
                this.close(success);
            } else {
                Toast.error("Unable to save guest.");
            }
        }
    }
}
</script>