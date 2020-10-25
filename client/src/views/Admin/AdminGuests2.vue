<template>
    <b-container class="pb-5">
        <b-row class="my-3">
            <b-col>
                <h1 class="text-center">Guest List ({{ guests.length }})</h1>
                <b-dropdown size="sm" no-caret variant="link">
                    <template v-slot:button-content>
                        <b-button size="sm" squared variant="success">
                            <b-icon-plus /> New
                        </b-button>
                    </template>
                    <b-dropdown-item-button @click="onNewFamily">Family</b-dropdown-item-button>
                    <b-dropdown-item-button @click="() => onNewGuest()">Guest</b-dropdown-item-button>
                </b-dropdown>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <h3>Families</h3>
                <b-table :fields="familyFields" :items="families" striped show-empty :busy="isBusy" caption="Select a family to expand"
                    selectable select-mode="single" @row-selected="rows => onRowSelected(rows, 'families')">

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
                        <b-button squared size="sm" variant="link" class="text-danger" @click="onFamilyDelete(row.item.familyId)">Delete</b-button>
                    </template>

                    <template v-slot:row-details="parentRow">
                        <b-container class="border border-secondary rounded p-3 bg-white">
                            <h3>The {{ parentRow.item.name }} family</h3>
                            <b-button size="sm" variant="link" @click="onNewGuest(parentRow.item.familyId)">New Family Member</b-button>
                            <b-table :fields="guestFields" :items="guests.filter(x => x.familyId == parentRow.item.familyId)" show-empty :busy="isBusy"
                                selectable select-mode="single" @row-selected="rows => onRowSelected(rows, 'guests')">

                                <template v-slot:cell(parentId)="row">
                                    {{ printGuestName(row.item.parentId) }}
                                </template>

                                <template v-slot:cell(rsvps)="row">
                                    <RsvpInviteForm :guestId="row.item.guestId" />
                                </template>

                                <template v-slot:cell(options)="row">
                                    <div class="d-flex flex-column align-items-center justify-content-start">
                                        <b-button squared size="sm" variant="link" class="text-primary p-1"
                                            @click="onPromoteGuest(row.item.guestId, parentRow.item.familyId)">Promote</b-button>
                                        <b-button squared size="sm" variant="link" class="text-danger p-1"
                                            @click="onDeleteGuest(row.item.guestId)">Delete</b-button>
                                    </div>
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
        <b-row class="my-3">
            <b-col>
                <h3>Guests without a Family</h3>
                <b-table :items="guestsWithNoFamily" :fields="guestFields" caption="Select a Guest to edit" show-empty striped
                    selectable select-mode="single" @row-selected="rows => onRowSelected(rows, 'guests')">

                    <template v-slot:cell(options)="row">
                        <b-button class="text-danger" size="sm" variant="link" @click="onGuestDelete(row.item.guestId)">Delete</b-button>
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
            </b-col>
        </b-row>
        <NewGuestModal :visible="isNewGuestModalVisible" @close="onNewGuestModalClose" :familyId="newGuestFamilyId" />
        <NewFamilyModal :visible="isNewFamilyModalVisible" @close="onNewFamilyModalClose" />
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
import Guest from "@/components/Admin/Guests/Guest";
import NewGuestModal from "@/components/modals/NewGuestModal";
import NewFamilyModal from "@/components/modals/NewFamilyModal";
import GuestForm from "@/components/forms/GuestForm";
import RsvpInviteForm from "@/components/Admin/Guests/RsvpInviteForm";
import FamilyRsvpInviteForm from "@/components/Admin/Guests/FamilyRsvpInviteForm";
import arraySort from "@/helpers/arraySort";
const cloneDeep = require("clone-deep");
const Toast = require("@/helpers/toast");

export default {
    name: "AdminGuests2",
    components: {
        Guest,
        NewGuestModal,
        NewFamilyModal,
        GuestForm,
        RsvpInviteForm,
        FamilyRsvpInviteForm
    },
    data() {
        return {
            isBusy: false,
            isNewGuestModalVisible: false,
            isNewFamilyModalVisible: false,
            newGuestFamilyId: null,
            familyFields: [
                "name",
                {
                    key: "headMemberId",
                    label: "Head Member",
                    formatter: key => {
                        const guest = this.guests.find(x => x.guestId == key);

                        return guest ? `${guest.firstName} ${guest.lastName}` : "";
                    }
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
                {
                    key: "name",
                    formatter: (key, value, item) => `${item.firstName} ${item.lastName}`
                },
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
                    label: "Parent",
                },
                {
                    key: "rsvps",
                    label: "RSVPs"
                },
                {
                    key: "options",
                    thClass: "text-center"
                }
            ]
        }
        },
    computed: {
        families() {
            return arraySort(this.$store.getters.families, "name");
        },
        guests() {
            return arraySort(this.$store.getters.guests, "firstName");
        },
        children() {
            return arraySort(this.$store.getters.guests
                .filter(x => x.isChild), "firstName");
        },
        nonChildren() {
            return arraySort(this.$store.getters.guests
                .filter(x => !x.isChild), "firstName");
        },
        guestsWithNoFamily() {
            return arraySort(this.$store.getters.guests.filter(x => !x.familyId));
        }
    },
    mounted() {
        this.fetch();
        console.log(arraySort)
    },
    methods: {
        onGuestEditClose(id) {
            const guest = this.guests.find(x => x.guestId == id);
            if (guest == null) return;

            this.$set(guest, "_showDetails", false);
        },
        onRowSelected(rows, collection) {
            const idKey = collection == "guests" ? "guestId" : "familyId";
            const open = rows.map(x => x[idKey]);

            for (let entity of this[collection]) {
                const isOpen = open.includes(entity[idKey]);

                if (entity._showDetails != isOpen) {
                    this.$set(entity, "_showDetails", isOpen);
                }
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
            await this.$store.dispatch(ACTIONS.FAMILIES.FETCH_ALL);
        },
        async fetchGuests() {
            await this.$store.dispatch(ACTIONS.GUESTS.FETCH_ALL);
        },
        async fetchAddresses() {
            await this.$store.dispatch(ACTIONS.ADDRESSES.FETCH_ALL);
        },
        async fetchEvents() {
            await this.$store.dispatch(ACTIONS.EVENTS.FETCH_ALL);
        },
        async fetchRsvps() {
            await this.$store.dispatch(ACTIONS.RSVPS.FETCH_ALL);
        },
        familyMembers(familyId) {
            return this.guests.filter(x => x.familyId == familyId);
        },
        async onFamilyDelete(id) {
            const family = this.families.find(x => x.familyId == id);
            if (family == null) return;

            if (confirm(`Are you sure you want to delete the ${family.name} family?`)) {
                await this.$store.dispatch(ACTIONS.FAMILIES.DELETE, id);
                
                const affectedGuests = this.guests.filter(x => x.familyId == id);
                await Promise.all(affectedGuests.map(x => this.$store.dispatch(ACTIONS.GUESTS.FETCH, x.guestId)));
            }
        },
        async onGuestDelete(id) {
            const guest = this.guests.find(x => x.guestId == id);

            if (confirm(`Are you sure you want to delete ${guest.firstName} ${guest.lastName}?`)) {
                await this.$store.dispatch(ACTIONS.GUESTS.DELETE, id);
            }
        },
        onNewGuest(familyId) {
            this.newGuestFamilyId = familyId || null;
            this.isNewGuestModalVisible = true;
        },
        onNewFamily() {
            this.isNewFamilyModalVisible = true;
        },
        onNewGuestModalClose() {
            this.isNewGuestModalVisible = false;
            this.newGuestFamilyId = null;
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
        },
        async onPromoteGuest(guestId, familyId) {
            this.isBusy = true;
            const index = this.families.findIndex(x => x.familyId == familyId);
            if (index == -1) return;

            if (!confirm("Are you sure you want to make this guest the head of their household?")) return;

            this.$set(this.families[index], "headMemberId", guestId);

            const success = await this.$store.dispatch(ACTIONS.FAMILIES.UPDATE, this.families[index]);

            if (success) {
                Toast.success("Guest promoted!");
            }
            
            this.isBusy = false;
        },
        async onDeleteGuest(guestId) {
            if (confirm("Are you sure you want to delete this guest?")) {
                await this.$store.dispatch(ACTIONS.GUESTS.DELETE, guestId);
            }
        }
    }
}
</script>