<template>
  <b-container class="pt-5">
    <NewEventModal :visible="isNewEventModalVisible" @close="isNewEventModalVisible = false" />
    <h1 class="text-center">Events</h1>
    <b-button squared size="sm" variant="success" @click="isNewEventModalVisible = true">
      <b-icon-plus class="mr-2" />New Event
    </b-button>
  </b-container>
</template>

<script>
import NewEventModal from "@/components/modals/NewEventModal.vue";

export default {
    name: "AdminEvents",
    components: {
        NewEventModal
    },
    data() {
        return {
            isNewEventModalVisible: false
        }
    }
}
</script>