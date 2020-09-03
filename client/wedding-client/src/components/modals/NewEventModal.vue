<template>
  <b-modal :visible="visible" @hide="onClose" title="New Event">
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
            <DateTimePicker v-model="event.startTime" />
          </b-form-group>
        </b-col>
        <b-col>
          <b-form-group
            label="Event end time"
            :state="eventEndTimeState"
            :invalid-feedback="eventEndTimeFeedback"
          >
            <DateTimePicker v-model="event.endTime" />
          </b-form-group>
        </b-col>
      </b-row>
    </b-container>
    <div slot="modal-footer">
      <b-button square size="sm" variant="success" @click="onSave">Save</b-button>
    </div>
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
            eventEndDateMin: null,
            eventStartTimeMinDate: new Date()
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