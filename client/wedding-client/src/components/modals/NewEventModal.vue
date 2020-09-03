<template>
  <b-modal :visible="visible" @hide="onClose" hide-footer title="New Event">
    <b-form @submit.prevent="onSubmit">
      <b-container>
        <b-row>
          <b-col>
            <b-form-group
              label="Event Title"
              :state="eventNameState"
              invalid-feedback="Name is required."
            >
              <b-form-input v-model="event.name" :state="eventNameState" />
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
          <b-col>
            <b-form-group
              label="Event start time"
              :state="eventStartTimeState"
              :invalid-feedback="eventStartTimeFeedback"
            >
              <b-datepicker v-model="event.startTime.date" :min="eventStartDateMin" required />
              <b-timepicker v-model="event.startTime.time" required />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              label="Event end time"
              :state="eventEndTimeState"
              :invalid-feedback="eventEndTimeFeedback"
            >
              <b-datepicker v-model="event.endTime.date" :min="eventEndDateMin" required />
              <b-timepicker v-model="event.endTime.time" required />
            </b-form-group>
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
import DateTimePicker from "@/components/DateTimePicker.vue";

export default {
    name: "NewEventModal",
    props: ["visible"],
    components: {
      DateTimePicker
    },
    data() {
        return {
            event: {
                name: "",
                description: "",
                startTime: {},
                endTime: {}
            },
            eventStartTimeFeedback: "",
            eventEndTimeFeedback: "",
            eventNameState: null,
            eventStartTimeState: null,
            eventEndTimeState: null,
            eventStartDateMin: new Date(),
            eventEndDateMin: new Date()
        }
    },
    computed: {
        
    },
    watch: {
        "event.name": function() {
            if (!!this.event.name && !!this.event.name.trim()) {
                this.eventNameState = true;
            } else {
                this.eventNameState = false;
            }
        }
    },
    methods: {
        onClose() {
          this.close();
        },
        onSave() {
          console.log(this.event.startTime);
          console.log(this.event.endTime);
        },
        verifyForm() {

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
        }
    }
}
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