<template>
    <b-container class="pb-5">
        <b-row class="my-3">
            <b-col>
                <h1 class="text-center">Guest List ({{ guests.length }})</h1>
                <b-dropdown size="sm" squared variant="success">
                    <template v-slot:button-content>
                        <b-icon-plus /> New
                    </template>
                    <b-dropdown-item @click="onNewFamily">Family</b-dropdown-item>
                    <b-dropdown-item @click="() => onNewGuest()">Guest</b-dropdown-item>
                </b-dropdown>
            </b-col>
        </b-row>
        <b-row class="my-3">
            <b-col>
                <h3>Guests without a Family</h3>
                <b-table :items="guestsWithNoFamily" :fields="guestFields" caption="Select a Guest to edit" show-empty striped>

                    <template v-slot:cell(options)="row">
                        <b-button class="text-success" size="sm" variant="link" @click="$set(row.item, '_showDetails', !row.item._showDetails)">Edit</b-button>
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
        <b-row>
            <b-col>
                <h3>Families</h3>
                <b-table :fields="familyFields" :items="families" striped show-empty :busy="isBusy" caption="Select a family to expand">

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
                        <b-button size="sm" class="text-success" variant="link" @click="$set(row.item, '_showDetails', !row.item['_showDetails'])">Edit</b-button>
                        <b-button squared size="sm" variant="link" class="text-danger" @click="onFamilyDelete(row.item.familyId)">Delete</b-button>
                    </template>

                    <template v-slot:row-details="parentRow">
                        <b-container class="border border-secondary rounded p-3 bg-white">
                            <h3>The {{ parentRow.item.name }} family</h3>
                            <b-button size="sm" variant="link" @click="onNewGuest(parentRow.item.familyId)">New Family Member</b-button>
                            <b-table :fields="guestFields" :items="guests.filter(x => x.familyId == parentRow.item.familyId)" show-empty :busy="isBusy">

                                <template v-slot:cell(name)="row">
                                    {{ printGuestName(row.item.guestId) }}
                                </template>

                                <template v-slot:cell(parentId)="row">
                                    {{ printGuestName(row.item.parentId) }}
                                </template>

                                <template v-slot:cell(rsvps)="row">
                                    <RsvpInviteForm :guestId="row.item.guestId" />
                                </template>

                                <template v-slot:cell(options)="row">
                                    <div class="d-flex flex-column align-items-center justify-content-start">
                                        <b-button size="sm" variant="link" class="text-success" 
                                            @click="$set(row.item, '_showDetails', !row.item['_showDetails'])">Edit</b-button>
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
        },
        guestsWithNoFamily() {
            return this.$store.getters.guests.filter(x => !x.familyId);
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
                await Promise.all(affectedGuests.map(x => this.$store.dispatch(ACTIONS.GUESTS.FETCH, x)));
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

            if (success) Toast.success("Guest promoted!");

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