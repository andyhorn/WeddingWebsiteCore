<template>
  <div class="container">
    {{ event }}
    <b-form @submit.prevent="onSaveEvent" v-if="!!event">
      <b-row>
        <b-col>
          <h2>Title</h2>
          <b-input v-model="event.name" />
        </b-col>
        <b-col>
          <h2>Description</h2>
          <b-textarea v-model="event.description" />
        </b-col>
      </b-row>
      <b-row class="py-3">
        <b-col>
          <h2>Start time</h2>
          <DateTimePicker v-model="startTime" />
        </b-col>
        <b-col>
          <h2>End time</h2>
          <DateTimePicker v-model="endTime" />
        </b-col>
      </b-row>
      <b-row class="py-3 mb-5">
        <b-col>
          <h2>Address</h2>
          <p>addresses here...</p>
        </b-col>
      </b-row>
    </b-form>
  </div>
</template>

<script>
import DateTimePicker from "@/components/DateTimePicker";
import * as DateTime from "@/helpers/dateTime";

export default {
  name: "AdminEventDetails",
  components: {
    DateTimePicker,
  },
  data() {
    return {
      event: null,
    };
  },
  computed: {
    startTime: {
      get() {
        const obj = {
          date: this.event.startTime.split("T")[0],
          time: this.event.startTime.split("T")[1],
        };
        return obj;
      },
      set(val) {
        const str = `${val.date}T${val.time}`;
        this.event.startTime = str;
      },
    },
    endTime: {
      get() {
        const obj = {
          date: this.event.endTime.split("T")[0],
          time: this.event.endTime.split("T")[1],
        };

        return obj;
      },
      set(val) {
        const str = `${val.date}T${val.time}`;
        this.event.endTime = str;
      },
    },
  },
  mounted() {
    const id = this.$route.params.id;
    const event = this.$store.getters.findEvent(id);

    this.event = event;
  },
};
</script>

<style scoped>
</style>