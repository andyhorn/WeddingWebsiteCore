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
    <b-container class="mt-5">
      <b-table :fields="fields" :items="events" show-empty>
        <template v-slot:cell(date)="data">{{ getDate(data.item) }}</template>
        <template v-slot:cell(time)="data">{{ getTimes(data.item) }}</template>
        <template v-slot:cell(address)="data">
          <span>{{ printAddress(data.item.addressId) }}</span>
        </template>
        <template v-slot:cell(options)="data">
          <div class="d-flex">
            <b-button
              variant="success"
              squared
              size="sm"
              class="mr-1 text-light"
              @click="onEditEvent(data.item.eventId)"
            >Edit</b-button>
            <b-button
              variant="danger"
              squared
              size="sm"
              class="ml-1"
              @click="onDeleteEvent(data.item.eventId)"
            >Delete</b-button>
          </div>
        </template>
        <template v-slot:empty>
          <p class="text-center mt-3 text-dark">No events found</p>
        </template>
      </b-table>
    </b-container>
  </b-container>
</template>

<script>
import NewEventModal from "@/components/modals/NewEventModal.vue";
import { ACTIONS } from "@/store";
import * as DateTime from "@/helpers/dateTime";

export default {
  name: "AdminEvents",
  components: {
    NewEventModal,
  },
  data() {
    return {
      isNewEventModalVisible: false,
      fields: [
        "name",
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
      return this.$store.getters.events;
    },
  },
  mounted() {
    this.fetch();
  },
  methods: {
    async fetch() {
      this.isTableBusy = true;
      await this.$store.dispatch(ACTIONS.EVENT_ACTIONS.FETCH_ALL);
      this.isTableBusy = false;
    },
    async onRefreshTable() {
      this.fetch();
    },
    onEditEvent(eventId) {
      this.$router.push({
        name: "Admin-Event-Details",
        params: { id: eventId },
      });
    },
    async onDeleteEvent(eventId) {
      const deleteEvent = confirm(
        "Are you sure you want to delete this event?"
      );

      if (deleteEvent) {
        this.isTableBusy = true;
        await this.$store.dispatch(ACTIONS.EVENT_ACTIONS.DELETE, eventId);
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