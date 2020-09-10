<template>
  <b-container class="pt-5">
    <NewEventModal :visible="isNewEventModalVisible" @close="isNewEventModalVisible = false" />
    <h1 class="text-center">Events</h1>
    <b-button squared size="sm" variant="success" @click="isNewEventModalVisible = true">
      <b-icon-plus class="mr-2" />New Event
    </b-button>
    <b-container class="mt-5">
      <b-table :fields="fields" :items="events">
        <template v-slot:cell(startTime)="data">{{ parseDate(data.item.startTime) }}</template>
        <template v-slot:cell(endTime)="data">{{ parseDate(data.item.endTime) }}</template>
        <template v-slot:cell(options)="data">
          <b-button
            variant="success"
            squared
            class="mr-1 text-light"
            @click="onEditEvent(data.item.eventId)"
          >Edit</b-button>
          <b-button
            variant="danger"
            squared
            class="ml-1"
            @click="onDeleteEvent(data.item.eventId)"
          >Delete</b-button>
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
        "startTime",
        "endTime",
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
      const utcDate = DateTime.parseDateString(dateString);
      const localDateString = utcDate.toLocaleString();
      return localDateString;
    },
  },
};
</script>