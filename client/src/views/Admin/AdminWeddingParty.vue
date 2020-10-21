<template>
    <b-container class="my-5">
        <h1 class="text-center">Wedding Party Management</h1>

        <b-tabs content-class="my-3 p-2">
            <b-tab>
                <template #title>
                    <h2>Roles</h2>
                </template>
                <div class="my-2 d-flex justify-content-between">
                    <b-button squared size="sm" variant="success" @click="onNewRole"><b-icon-plus /> New Role</b-button>
                    <b-button squared size="sm" variant="primary" @click="fetchRoles"><b-icon-arrow-clockwise /> Refresh</b-button>
                </div>
                <b-table :fields="roleFields" :items="roles" sort-by="name" small>
                    
                    <template v-slot:cell(members)="row">
                        {{ row.item.guestWeddingRoles.length }}
                    </template>

                    <template v-slot:cell(options)="row">
                        <b-button size="sm" variant="link" class="text-danger" @click="onRoleDelete(row.item.weddingRoleId)">Delete</b-button>
                    </template>

                </b-table>
            </b-tab>
            <b-tab>
                <template #title>
                    <h2>Assignments</h2>
                </template>
                <b-table :fields="memberFields" :items="members">

                    <template v-slot:cell(name)="row">
                        {{ `${row.item.firstName} ${row.item.lastName}` }}
                    </template>

                    <template v-slot:cell(roles)="row">
                        {{ printMemberRoles(row.item.guestId) }}
                    </template>

                </b-table>
            </b-tab>
        </b-tabs>

        <NewWeddingRoleModal :visible="isNewWeddingRoleModalVisible" @close="onNewWeddingRoleModalClose" />
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
import NewWeddingRoleModal from "@/components/modals/NewWeddingRoleModal";
import WeddingForm from "@/components/forms/WeddingRoleForm";

export default {
    name: "AdminWeddingParty",
    components: {
        NewWeddingRoleModal,
        WeddingForm
    },
    data() {
        return {
            isNewWeddingRoleModalVisible: false,
            roleFields: [
                {
                    key: "name",
                    sortable: true,
                    tdClass: "align-middle"
                },
                {
                    key: "description",
                    sortable: true,
                    tdClass: "align-middle"
                },
                {
                    key: "members",
                    sortable: true,
                    tdClass: "align-middle"
                },
                {
                    key: "options",
                    thClass: "text-center",
                    tdClass: "text-center"
                }
            ],
            memberFields: [
                "name",
                "roles"
            ]
        }
    },
    computed: {
        roles() {
            const roles = this.$store.getters.weddingRoles;
            console.log(roles)
            return roles;
        },
        members() {
            const members = this.$store.getters.guests
                .filter(x => x.isWeddingMember);
            console.log(members)
            return members;
        }
    },
    mounted() {
        this.fetch();
    },
    methods: {
        async fetch() {
            await Promise.all([
                this.fetchRoles(),
                this.fetchGuests()
            ]);
        },
        async fetchRoles() {
            await this.$store.dispatch(ACTIONS.WEDDING_ROLE_ACTIONS.FETCH_ALL);
        },
        async fetchGuests() {
            await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.FETCH_ALL);
        },
        onNewRole() {
            this.isNewWeddingRoleModalVisible = true;
        },
        onNewWeddingRoleModalClose() {
            this.isNewWeddingRoleModalVisible = false;
        },
        printMemberRoles(guestId) {
            const roleIds = this.roles
                .map(x => x.guestWeddingRoles)
                .filter(x => x.guestId == guestId)
                .map(x => x.weddingRoleId);

            const roles = roleIds.map(x => this.roles.find(x => x.weddingRoleId == x).name);

            if (roles.length)
                return roles.join(", ");

            return "None";
        }
    }
}
</script>