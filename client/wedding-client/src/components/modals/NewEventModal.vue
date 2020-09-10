<template>
  <b-modal size="lg" :visible="visible" @hide="onClose" hide-footer title="New Event">
    <b-form @submit.prevent="onSubmit">
      <b-container>
        <b-row>
          <b-col>
            <b-form-group :state="eventNameState" invalid-feedback="Name is required.">
              <h3>Title</h3>
              <b-form-input v-model="event.name" required :state="eventNameState" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-form-group>
              <h3>Description</h3>
              <b-form-textarea v-model="event.description" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row class="pb-5 pt-2">
          <b-col>
            <h2>
              Start time
              <span v-if="eventStartTimeState">
                <b-icon icon="check" variant="success" />
              </span>
              <span v-else-if="eventStartTimeState === false">
                <b-icon icon="x" variant="danger" />
              </span>
            </h2>
            <p v-if="eventStartTimeState === false" class="text-danger">{{ eventStartTimeFeedback }}</p>
            <DateTimePicker v-model="event.startTime" :dateMin="eventStartDateMin" />
          </b-col>
          <b-col>
            <h2>
              End time
              <span v-if="eventEndTimeState">
                <b-icon icon="check" variant="success" />
              </span>
              <span v-else-if="eventEndTimeState === false">
                <b-icon icon="x" variant="danger" />
              </span>
            </h2>
            <p v-if="eventEndTimeState === false" class="text-danger">{{ eventEndTimeFeedback }}</p>
            <DateTimePicker
              v-model="event.endTime"
              :timeMin="eventEndTimeMin"
              :dateMin="eventEndDateMin"
            />
          </b-col>
        </b-row>
      </b-container>
      <div class="footer px-3 space-buttons">
        <b-button
          square
          size="sm"
          variant="success"
          type="submit"
          :disabled="isSaveButtonDisabled"
        >Save</b-button>
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
        startTime: {
          date: null,
          time: null,
        },
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
    };
  },
  computed: {
    eventEndTimeMin() {
      if (
        this.event.startTime.date != null &&
        this.event.startTime.time != null &&
        this.event.endTime.date != null &&
        DateTime.compareDates(
          this.event.endTime.date,
          this.event.startTime.date
        ) == 0
      ) {
        return this.event.startTime.time;
      }

      return "";
    },
    eventEndDateMin() {
      if (this.event.startTime.date != null) {
        return this.event.startTime.date;
      }

      return new Date();
    },
    isSaveButtonDisabled() {
      if (
        this.event.name != null &&
        this.event.startTime.date != null &&
        this.event.startTime.time != null &&
        this.event.endTime.date != null &&
        this.event.endTime.time != null
      )
        return false;

      return true;
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
          this.event.startTime.date != null &&
          this.event.startTime.time != null
        ) {
          this.eventStartTimeState = true;
        } else {
          this.eventStartTimeState = null;
        }
      },
    },
    "event.endTime": {
      deep: true,
      handler: function () {
        if (
          this.event.endTime.date != null &&
          this.event.endTime.time != null
        ) {
          this.eventEndTimeState = true;
        } else {
          this.eventEndTimeState = null;
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