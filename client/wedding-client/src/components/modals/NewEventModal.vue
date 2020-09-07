<template>
  <b-modal size="lg" :visible="visible" @hide="onClose" hide-footer title="New Event">
    <b-form @submit.prevent="onSubmit">
      <b-container>
        <b-row>
          <b-col>
            <b-form-group
              label="Event Title"
              :state="eventNameState"
              invalid-feedback="Name is required."
            >
              <b-form-input v-model="event.name" required :state="eventNameState" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-form-group label="Event Description">
              <b-form-textarea v-model="event.description" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="12">
            <h2>Event start time</h2>
          </b-col>
          <b-col>
            <b-form-group
              label="Event start date"
              :state="eventStartTimeState"
              :invalid-feedback="eventStartTimeFeedback"
            >
              <div class="d-flex justify-content-center align-items-center">
                <b-calendar
                  hide-header
                  v-model="event.startTime.date"
                  :min="eventStartDateMin"
                  required
                />
              </div>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              label="Event start time"
              :state="eventStartTimeState"
              :invalid-feedback="eventStartTimeFeedback"
            >
              <div class="d-flex justify-content-center align-items-center">
                <b-time hide-header v-model="event.startTime.time" required class="mt-1" />
              </div>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="12">
            <h2>Event end time</h2>
          </b-col>
          <b-col>
            <DateTimePicker
              v-model="event.endTime"
              :dateMin="eventEndDateMin"
              :timeMin="eventEndTimeMin"
            />
          </b-col>
        </b-row>
      </b-container>
      <div class="footer px-3 space-buttons">
        <b-button square size="sm" variant="success" type="submit">Save</b-button>
        <b-button square size="sm" variant="warning" type="reset">Clear</b-button>
        <b-button square size="sm" variant="danger" @click="onClose">Cancel</b-button>
      </div>
    </b-form>
  </b-modal>
</template>

<script>
import TimePicker from "@/components/TimePicker";
import DateTimePicker from "@/components/DateTimePicker";
import { ACTIONS } from "@/store";

import * as DateTime from "@/helpers/dateTime";

export default {
  name: "NewEventModal",
  props: ["visible"],
  components: {
    TimePicker,
    DateTimePicker,
  },
  data() {
    return {
      event: {
        name: "",
        description: "",
        startTime: {},
        endTime: {
          date: null,
          time: null,
        },
      },
      eventStartTimeFeedback: "",
      eventEndTimeFeedback: "",
      eventNameState: null,
      eventStartTimeState: null,
      eventEndTimeState: null,
      eventStartDateMin: new Date(),
      eventEndDateMin: new Date().toDateString(),
      eventStartTimeMin: "00:00:00",
      // eventEndTimeMin: null,
    };
  },
  computed: {
    eventEndTimeMin() {
      if (
        this.event.startTime.date != null &&
        this.event.startTime.time != null &&
        this.event.endTime.date != null &&
        this.event.endTime.date == this.event.startTime.date
      ) {
        return this.event.startTime.time;
      }

      return null;
    },
  },
  watch: {
    "event.name": function () {
      if (!!this.event.name && !!this.event.name.trim()) {
        this.eventNameState = true;
      } else {
        this.eventNameState = false;
      }
    },
    "event.startTime": {
      deep: true,
      handler: function () {
        if (
          this.event.startTime.date == null &&
          this.event.startTime.time == null
        ) {
          this.eventEndTimeMin = null;
          this.eventEndDateMin = new Date().toDateString();
          return;
        }

        if (this.event.startTime.date != null) {
          this.eventEndDateMin = this.event.startTime.date;
        }
      },
    },
  },
  methods: {
    getTimeFromString(timeString) {
      const date = new Date(0, 0, 0, 0, 0, 0, 0);
      const s = timeString.split(":");

      if (s.length > 0) {
        date.setHours(s[0]);
      }

      if (s.length > 1) {
        date.setMinutes(s[1]);
      }

      if (s.length > 2) {
        date.setSeconds(s[2]);
      }

      if (s.length > 3) {
        date.setMilliseconds(s[3]);
      }

      return date;
    },
    onClose() {
      this.close();
    },
    async onSubmit() {
      if (this.verifyForm()) {
        const startTime = this.makeDateTime(this.event.startTime);
        const endTime = this.makeDateTime(this.event.endTime);

        const eventData = {
          name: this.event.name,
          description: this.event.description,
          startTime: startTime,
          endTime: endTime,
        };

        const id = await this.$store.dispatch(
          ACTIONS.EVENT_ACTIONS.CREATE,
          eventData
        );
        if (id) {
          this.close();
        } else {
          this.$bvToast.toast({
            message: "Unable to save event",
            variant: "danger",
          });
        }
      }
    },
    makeDateTime(datetime) {
      const { date, time } = datetime;
      const dateObject = new Date(date + " " + time);
      return dateObject;
    },
    verifyForm() {
      this.resetStates();

      if (this.event.name == null) {
        this.eventNameState = false;
      }

      if (this.event.startTime.date == null) {
        this.eventStartTimeState = false;
        this.eventStartTimeFeedback = "Start date is required";
        return false;
      }

      if (this.event.startTime.time == null) {
        this.eventStartTimeState = false;
        this.eventStartTimeFeedback = "Start time is required";
        return false;
      }

      if (this.event.endTime.date == null) {
        this.eventEndTimeState = false;
        this.eventEndTimeFeedback = "End date is required";
        return false;
      }

      if (this.event.endTime.time == null) {
        this.eventEndTimeState = false;
        this.eventEndTimeFeedback = "End time is required";
        return false;
      }

      return true;
    },
    resetStates() {
      this.eventNameState = null;
      this.eventStartTimeState = null;
      this.eventEndTimeState = null;
    },
    close() {
      this.event.name = "";
      this.event.description = "";
      this.event.startTime = {};
      this.event.endTime = {};
      this.eventStartTimeFeedback = "";
      this.eventEndTimeFeedback = "";
      this.eventNameState = null;
      this.eventStartTimeState = null;
      this.eventEndTimeState = null;
      this.$emit("close");
    },
  },
};
</script>

<style scoped>
.space-buttons > button {
  margin-left: 3px;
  margin-right: 3px;
}
.space-buttons > button:first-child {
  margin-left: 0;
}
.space-buttons > button:last-child {
  margin-right: 0;
}
</style>