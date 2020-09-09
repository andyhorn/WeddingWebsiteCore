<template>
  <b-container class="pt-5">
    <NewEventModal :visible="isNewEventModalVisible" @close="isNewEventModalVisible = false" />
    <h1 class="text-center">Events</h1>
    <b-button squared size="sm" variant="success" @click="isNewEventModalVisible = true">
      <b-icon-plus class="mr-2" />New Event
    </b-button>
    <div v-for="event in events" :key="event.id">{{ event }}</div>
  </b-container>
</template>

<script>
import NewEventModal from "@/components/modals/NewEventModal.vue";
import { ACTIONS } from "@/store";

export default {
  name: "AdminEvents",
  components: {
    NewEventModal,
  },
  data() {
    return {
      isNewEventModalVisible: false,
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
      await this.$store.dispatch(ACTIONS.EVENT_ACTIONS.FETCH_ALL);
    },
  },
};
</script>