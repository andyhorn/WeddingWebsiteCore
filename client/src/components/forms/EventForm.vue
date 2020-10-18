<template>
    <b-container>
        <b-form @submit.prevent="onSubmit">
            <b-row>
                <b-col>
                    <b-form-group label="Name/Title" :state="nameState" description="Required">
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
                <b-col>
                    <b-form-group label="Event Date & Time">
                        <b-row>
                            <b-col class="d-flex align-items-center justify-content-stretch">
                                <b-form-group class="flex-fill" label="Date" :state="dateState" description="Required">
                                    <b-form-datepicker v-model="date" :min="new Date()" :state="dateState" />
                                </b-form-group>
                            </b-col>
                            <b-col class="d-flex flex-column justify-content-center align-items-around">
                                <b-form-group label="Start Time" :state="startTimeState" description="Required">
                                    <b-form-timepicker v-model="startTime" :state="startTimeState" minutes-step="15" />
                                </b-form-group>
                                <b-form-group label="End Time" :state="endTimeState">
                                    <b-form-timepicker v-model="endTime" :state="endTimeState" minutes-step="15" :disabled="startTime == null" />
                                </b-form-group>
                            </b-col>    
                        </b-row>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-form-group label="Location">
                        <b-input-group>
                            <b-form-select v-model="addressId">
                                <option :value="null">None</option>
                                <option v-for="address in addresses"
                                    :key="address.addressId"
                                    :value="address.addressId"
                                    :native-value="address.addressId"
                                >
                                    {{ address.name }}
                                </option>
                            </b-form-select>
                            <b-input-group-append>
                                <b-button variant="primary" @click="isAddressFormVisible = true">New Address</b-button>
                            </b-input-group-append>
                        </b-input-group>
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
        <b-collapse v-model="isAddressFormVisible">
            <b-container v-if="isAddressFormVisible" class="my-3 p-3 border border-secondary rounded">
                <h3>New Address</h3>
                <AddressForm @close="onAddressFormClose" />
            </b-container>
        </b-collapse>
    </b-container>
</template>

<script>
import AddressForm from "@/components/forms/AddressForm";
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "EventForm",
    components: {
        AddressForm
    },
    props: [ "event" ],
    data() {
        return {
            id: null,
            name: null,
            description: null,
            date: null,
            startTime: null,
            endTime: null,
            addressId: null,
            nameState: null,
            dateState: null,
            startTimeState: null,
            endTimeState: null,
            isAddressFormVisible: false
        }
    },
    computed: {
        addresses() {
            return this.$store.getters.addresses;
        },
        isFormValid() {
            return this.nameState === true
                && this.dateState === true
                && this.startTimeState === true
                && this.endTimeState === true;
        }
    },
    watch: {
        "name": function () {
            this.nameState = !!this.name && !!this.name.trim();
        },
        "date": function () {
            this.dateState = this.date != null;
        },
        "startTime": function () {
            this.startTimeState = this.startTime != null;
        },
        "endTime": function () {
            this.endTimeState = this.endTime != null
                && this.startTime <= this.endTime;
        },
        "event": {
            immediate: true,
            deep: true,
            handler: function () {
                if (this.event == null) return;

                console.log(this.event)
                this.id = this.event.eventId;
                this.name = this.event.name;
                this.description = this.event.description;
                this.date = this.getDateFromString(this.event.startTime);
                this.startTime = this.getTimeFromString(this.event.startTime);
                this.endTime = this.event.endTime == null
                    ? null 
                    : this.getTimeFromString(this.event.endTime);

                console.log(this.date)
                console.log(this.startTime)
                console.log(this.endTime)
            }
        }
    },
    methods: {
        clear() {
            this.id = null;
            this.name = null;
            this.description = null;
            this.date = null;
            this.startTime = null;
            this.endTime = null;
            this.addressId = null;
        },
        resetStates() {
            this.nameState = null;
            this.dateState = null;
            this.startTimeState = null;
            this.endTimeState = null;
        },
        close(success) {
            this.$emit("close", success);
            this.clear();
            this.resetStates();
        },
        onCancel() {
            this.close(false);
        },
        getDateFromString(date) {
            console.log("Getting date from string: " + date)
            const dateTime = new Date(date);
            const dateString = dateTime.toLocaleDateString();
            console.log("Date string: " + dateString)

            const dateStringSplit = dateString.split("/");

            if (dateStringSplit.length < 3) return null;

            const month = dateStringSplit[0];
            const day = dateStringSplit[1];
            const year = dateStringSplit[2];

            return `${year}-${month}-${day}`;
        },
        getTimeFromString(time) {
            const timeSplit = time.split(/[: ]/);

            if (timeSplit.length < 4) return null;

            let hour = Number(timeSplit[0]);
            const minute = Number(timeSplit[1]);
            const second = Number(timeSplit[2]);

            const amPm = timeSplit[3];

            if (amPm == "PM") {
                hour = (hour + 12) % 24;
            }

            return `${hour}:${minute}:00`;
        },
        makeDateTime(date, time) {
            if (date == null || time == null) return;

            const dateSplit = date.split("-");
            const timeSplit = time.split(":");

            if (dateSplit.length < 3 || timeSplit.length < 2) return;

            const year = dateSplit[0];
            const month = dateSplit[1];
            const day = dateSplit[2];

            const hour = timeSplit[0];
            const minute = timeSplit[1];
            const second = 0;
            const ms = 0;

            const dateTime = new Date(year, month - 1, day, hour, minute, second, ms);

            return dateTime;
        },
        async onSubmit() {
            if (!this.isFormValid) return;

            const event = {
                eventId: this.id || undefined,
                name: this.name,
                description: this.description,
                startTime: this.makeDateTime(this.date, this.startTime),
                endTime: this.endTime == null ? null : this.makeDateTime(this.date, this.endTime),
                addressId: this.addressId
            };

            console.log(event)

            const command = this.id == null
                ? ACTIONS.EVENT_ACTIONS.CREATE
                : ACTIONS.EVENT_ACTIONS.UPDATE;

            const success = await this.$store.dispatch(command, event);
            if (success) {
                const fetchAll = ACTIONS.EVENT_ACTIONS.FETCH_ALL;
                await this.$store.dispatch(fetchAll);
                this.close(success);
            } else {
                Toast.error(this, "Unable to save event.");
            }
        },
        onAddressFormClose(id) {
            if (id) this.addressId = id;

            this.isAddressFormVisible = false;
        }
    }
}
</script>