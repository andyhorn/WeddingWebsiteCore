<template>
  <b-container class="pt-5">
    <NewEventModal v-if="isNewEventModalVisible" :visible="isNewEventModalVisible" @close="isNewEventModalVisible = false" />
    <h1 class="text-center">Events</h1>
    <div class="d-flex justify-content-between align-items-center">
      <b-button squared size="sm" variant="success" @click="isNewEventModalVisible = true">
        <b-icon-plus class="mr-2" />New Event
      </b-button>
      <b-button squared size="sm" variant="primary" @click="onRefreshTable">
        <b-icon-arrow-clockwise class="mr-2" />Refresh
      </b-button>
    </div>
    <b-container class="pt-1">
      <b-table :fields="fields" :items="events" show-empty selectable select-mode="single" @row-selected="onEventSelected" >
        <template v-slot:cell(date)="data">{{ getDate(data.item) }}</template>
        <template v-slot:cell(time)="data">{{ getTimes(data.item) }}</template>
        <template v-slot:cell(address)="data">
          <span>{{ printAddress(data.item.addressId) }}</span>
        </template>
        <template v-slot:cell(options)="data">
          <b-button variant="link" squared size="sm" class="text-danger" @click="onDeleteEvent(data.item.eventId)">Delete</b-button>
        </template>
        <template v-slot:empty>
          <p class="text-center mt-3 text-dark">No events found</p>
        </template>
      </b-table>
    </b-container>
    <b-collapse v-model="isEventEditVisible">
      <b-container v-if="isEventEditVisible" class="my-2 p-3">
        <h3>Edit Event</h3>
        <EventForm :event="eventUnderEdit" @close="onEventEditClose" />
      </b-container>
    </b-collapse>
  </b-container>
</template>

<script>
import NewEventModal from "@/components/modals/NewEventModal.vue";
import EventForm from "@/components/forms/EventForm";
import { ACTIONS } from "@/store";
import * as DateTime from "@/helpers/dateTime";
import arraySort from "@/helpers/arraySort";
const Toast = require("@/helpers/toast");

export default {
  name: "AdminEvents",
  components: {
    NewEventModal,
    EventForm
  },
  data() {
    return {
      isNewEventModalVisible: false,
      isEventEditVisible: false,
      eventUnderEdit: null,
      fields: [
        {
          key: "name",
          sortable: true
        },
        "description",
        "date",
        "time",
        "address",
        "options",
      ],
      isTableBusy: false,
    };
  },
  computed: {
    events() {
      return arraySort(this.$store.getters.events, "name");
    },
  },
  mounted() {
    this.fetch();
  },
  methods: {
    async fetch() {
      this.isTableBusy = true;
      
      await Promise.all([
        this.$store.dispatch(ACTIONS.EVENTS.FETCH_ALL),
        this.$store.dispatch(ACTIONS.ADDRESSES.FETCH_ALL)
      ]);

      this.isTableBusy = false;
    },
    async onRefreshTable() {
      this.fetch();
    },
    onEventSelected(rows) {
      if (rows.length < 1) {
        this.isEventEditVisible = false;
        this.eventUnderEdit = null;
      } else {
        this.eventUnderEdit = rows[0];
        this.isEventEditVisible = true;
      }
    },
    onEventEditClose() {
      this.isEventEditVisible = false;
      this.eventUnderEdit = null;
    },
    async onDeleteEvent(eventId) {
      const deleteEvent = confirm(
        "Are you sure you want to delete this event?"
      );

      if (deleteEvent) {
        this.isTableBusy = true;
        await this.$store.dispatch(ACTIONS.EVENTS.DELETE, eventId);
        this.isTableBusy = false;
      }
    },
    parseDate(dateString) {
      const utcDate = new Date(dateString);
      const localDateString = utcDate.toLocaleString();
      return localDateString;
    },
    getDate(event) {
      const date = new Date(event.startTime);
      const dateString = date.toDateString();
      return dateString;
    },
    getTimes(event) {
      const date = new Date(event.startTime);
      const startTime = date.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });

      if (event.endTime != null) {
        const endTime = new Date(event.endTime).toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
        return `${startTime} - ${endTime}`;
      } else {
        return startTime;
      }
    },
    printAddress(addressId) {
      const address = this.$store.getters.findAddress(addressId);
      if (address) {
        let addressString = "";
        if (address.name) {
          addressString += "(" + address.name + ") ";
        }

        addressString += address.fullString;
        return addressString;
      }

      return "";
    },
  },
};
</script>