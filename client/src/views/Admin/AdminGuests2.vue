<template>
    <b-container>
        <b-row class="my-3">
            <b-col cols="1">
                <b-dropdown variant="success">
                    <template v-slot:button-content>
                        <b-icon-plus /> New
                    </template>
                    <b-dropdown-item @click="onNewFamily">Family</b-dropdown-item>
                    <b-dropdown-item @click="onNewGuest">Guest</b-dropdown-item>
                </b-dropdown>
            </b-col>
            <b-col cols="10">
                <h1 class="text-center">Guest List ({{ guests.length }})</h1>
            </b-col>
            <b-col cols="1" />
        </b-row>
        <b-row>
            <b-col>
                <b-table :fields="familyFields" :items="families" selectable
                    select-mode="single" @row-selected="item => onFamilySelected(item)"
                    striped>

                    <template v-slot:cell(headMemberId)="row">
                        {{ printGuestName(row.item.headMemberId) }}
                    </template>

                    <template v-slot:cell(addressId)="row">
                        {{ printAddress(row.item.addressId) }}
                    </template>

                    <template v-slot:cell(rsvps)="row">
                        <FamilyRsvpInviteForm :familyId="row.item.familyId" />
                    </template>

                    <template v-slot:cell(options)="row">
                        <b-button squared size="sm" variant="outline-danger" @click="onFamilyDelete(row.item.familyId)">Delete</b-button>
                    </template>

                    <template v-slot:row-details="parentRow">
                        <b-container class="border border-secondary rounded p-3 bg-white">
                            <h3>The {{ parentRow.item.name }} family</h3>
                            <b-table :fields="guestFields" :items="guests.filter(x => x.familyId == parentRow.item.familyId)"
                                selectable select-mode="single" @row-selected="onGuestSelected">

                                <template v-slot:cell(name)="row">
                                    {{ printGuestName(row.item.guestId) }}
                                </template>

                                <template v-slot:cell(parentId)="row">
                                    {{ printGuestName(row.item.parentId) }}
                                </template>

                                <template v-slot:cell(options)="row">
                                    <b-button squared size="sm" variant="outline-danger" @click="onGuestDelete(row.item.guestId)">Delete</b-button>
                                </template>

                                <template v-slot:row-details="row">
                                    <b-container class="border border-secondary rounded p-3 bg-white">
                                        <div class="d-flex align-items-baseline mb-3">
                                            <p class="m-0 p-0"><strong>Edit Guest:</strong></p>
                                            <h2 class="ml-2">{{ `${row.item.firstName} ${row.item.lastName}` }}</h2>
                                        </div>
                                        <GuestForm :guest="row.item" @close="onGuestEditClose(row.item.guestId)" />
                                    </b-container>
                                </template>

                            </b-table>
                        </b-container>
                    </template>
                </b-table>
            </b-col>
        </b-row>

        <NewGuestModal :visible="isNewGuestModalVisible" @close="onNewGuestModalClose" />
        <NewFamilyModal :visible="isNewFamilyModalVisible" @close="onNewFamilyModalClose" />
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
import Guest from "@/components/Admin/Guests/Guest";
import NewGuestModal from "@/components/modals/NewGuestModal";
import NewFamilyModal from "@/components/modals/NewFamilyModal";
import GuestForm from "@/components/forms/GuestForm";
import FamilyRsvpInviteForm from "@/components/Admin/Guests/FamilyRsvpInviteForm";

export default {
    name: "AdminGuests2",
    components: {
        Guest,
        NewGuestModal,
        NewFamilyModal,
        GuestForm,
        FamilyRsvpInviteForm
    },
    data() {
        return {
            isNewGuestModalVisible: false,
            isNewFamilyModalVisible: false,
            familyFields: [
                "name",
                {
                    key: "headMemberId",
                    label: "Head Member"
                },
                {
                    key: "addressId",
                    label: "Address"
                },
                "RSVPs",
                "options"
            ],
            guestFields: [
                "inviteCode",
                "name",
                {
                    key: "isWeddingMember",
                    label: "Wedding Party Member?",
                    formatter: val => val ? "Yes" : "No"
                },
                {
                    key: "isChild",
                    label: "Is a Child?",
                    formatter: val => val ? "Yes" : "No"
                },
                {
                    key: "isUnderTen",
                    label: "Is Under Ten?",
                    formatter: val => val ? "Yes" : "No"
                },
                {
                    key: "parentId",
                    label: "Parent"
                },
                "options"
            ]
        }
    },
    computed: {
        families() {
            return this.$store.getters.families;
        },
        guests() {
            return this.$store.getters.guests;
        },
        children() {
            return this.$store.getters.children;
        },
        nonChildren() {
            return this.$store.getters.nonChildren;
        }
    },
    mounted() {
        this.fetch();
    },
    methods: {
        onGuestSelected(rows) {
            const openGuests = rows.map(x => x.guestId);

            for (let guest of this.guests) {
                const isOpen = openGuests.includes(guest.guestId);

                if (guest._showDetails != isOpen)
                    this.$set(guest, "_showDetails", isOpen);
            }
        },
        onGuestEditClose(id) {
            const guest = this.guests.find(x => x.guestId == id);
            if (guest == null) return;

            this.$set(guest, "_showDetails", false);
        },
        onFamilySelected(items) {
            const openFamilies = items.map(x => x.familyId);

            for (let family of this.families) {
                const isOpen = openFamilies.includes(family.familyId);

                if (family._showDetails != isOpen)
                    this.$set(family, "_showDetails", isOpen);
            }
        },
        async fetch() {
            await Promise.all([
                this.fetchFamilies(),
                this.fetchGuests(),
                this.fetchAddresses(),
                this.fetchEvents(),
                this.fetchRsvps()
            ]);
        },
        async fetchFamilies() {
            await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.FETCH_ALL);
        },
        async fetchGuests() {
            await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.FETCH_ALL);
        },
        async fetchAddresses() {
            await this.$store.dispatch(ACTIONS.ADDRESS_ACTIONS.FETCH_ALL);
        },
        async fetchEvents() {
            await this.$store.dispatch(ACTIONS.EVENT_ACTIONS.FETCH_ALL);
        },
        async fetchRsvps() {
            await this.$store.dispatch(ACTIONS.RSVP_ACTIONS.FETCH_ALL);
        },
        familyMembers(familyId) {
            return this.guests.filter(x => x.familyId == familyId);
        },
        async onFamilyDelete(id) {
            const family = this.families.find(x => x.familyId == id);
            if (family == null) return;

            if (confirm(`Are you sure you want to delete the ${family.name} family?`)) {
                await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.DELETE, id);
                
                const affectedGuests = this.guests.filter(x => x.familyId == id);
                await Promise.all(affectedGuests.map(x => this.$store.dispatch(ACTIONS.GUEST_ACTIONS.FETCH, x)));
            }
        },
        async onGuestDelete(id) {
            const guest = this.guests.find(x => x.guestId == id);

            if (confirm(`Are you sure you want to delete ${guest.firstName} ${guest.lastName}?`)) {
                await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.DELETE, id);
            }
        },
        onNewGuest() {
            this.isNewGuestModalVisible = true;
        },
        onNewFamily() {
            this.isNewFamilyModalVisible = true;
        },
        onNewGuestModalClose() {
            this.isNewGuestModalVisible = false;
        },
        onNewFamilyModalClose() {
            this.isNewFamilyModalVisible = false;
        },
        printGuestName(id) {
            if (id == null) return "None";

            const guest = this.guests.find(x => x.guestId == id);
            if (guest == null) return "Error";

            return `${guest.firstName} ${guest.lastName}`;
        },
        printAddress(id) {
            if (id == null) return "";

            const address = this.addresses.find(x => x.addressId == id);
            return address.name;
        }
    }
}
</script>