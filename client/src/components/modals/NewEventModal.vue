<template>
  <b-modal size="lg" :visible="visible" @hide="onClose" hide-footer title="New Event">
    <b-form @submit.prevent="onSubmit">
      <b-container>
        <b-row class="py-2">
          <b-col>
            <b-form-group :state="eventNameState" invalid-feedback="Name is required.">
              <h3>Title</h3>
              <b-form-input v-model="event.name" required :state="eventNameState" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row class="py-2">
          <b-col>
            <b-form-group>
              <h3>Description</h3>
              <b-form-textarea v-model="event.description" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row class="py-2">
          <b-col>
            <h3>Event Date & Time</h3>
            <!-- <span v-if="eventStartTimeState">
              <b-icon icon="check" variant="success" />
            </span>
            <span v-else-if="eventStartTimeState === false">
              <b-icon icon="x" variant="danger" />
            </span> -->
            <b-row class="py-2">
              <b-col>
                <b-form-group label="Date">
                  <b-form-datepicker v-model="event.date" :min="eventDateMin" />
                </b-form-group>
              </b-col>
            </b-row>
            <b-row class="py-2">
              <b-col>
                <b-form-group label="Start time">
                  <b-form-timepicker v-model="event.time.start" />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group label="End time">
                  <b-form-timepicker v-model="event.time.end" />
                </b-form-group>
              </b-col>
            </b-row>
          </b-col>
          <!-- <b-col>
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
          </b-col> -->
        </b-row>
        <b-row class="py-2 mb-3">
          <b-col>
            <h2>Location</h2>
            <b-input-group>
              <b-select expanded v-model="event.addressId">
                <option :value="null">None</option>
                <option
                  v-for="address in addresses"
                  :key="address.addressId"
                  :value="address.addressId"
                >
                  <span v-if="!!address.name">({{ address.name }})</span>
                  {{ address.fullString }}
                </option>
              </b-select>
              <b-input-group-append>
                <b-button variant="primary" v-b-toggle.newAddressCollapse>New Address</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-col>
        </b-row>
        <b-collapse
          id="newAddressCollapse"
          class="border rounded p-3 mb-5"
          v-model="showNewAddressForm"
        >
          <NewAddressForm @saved="onAddressSave" />
        </b-collapse>
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
import NewAddressForm from "@/components/forms/NewAddressForm";
import { ACTIONS } from "@/store";

import * as DateTime from "@/helpers/dateTime";

export default {
  name: "NewEventModal",
  props: ["visible"],
  components: {
    TimePicker,
    DateTimePicker,
    NewAddressForm,
  },
  data() {
    return {
      event: {
        name: "",
        description: "",
        date: null,
        time: {
          start: null,
          end: null
        },
        // startTime: {
        //   date: null,
        //   time: null,
        // },
        // endTime: {
        //   date: null,
        //   time: null,
        // },
      },
      eventStartTimeFeedback: "",
      // eventEndTimeFeedback: "",
      eventNameState: null,
      // eventStartTimeState: null,
      // eventEndTimeState: null,
      eventDateMin: new Date(),
      showNewAddressForm: false,
    };
  },
  computed: {
    addresses() {
      return this.$store.getters.addresses;
    },
    eventEndTimeMin() {
      // if (
      //   this.event.startTime.date != null &&
      //   this.event.startTime.time != null &&
      //   this.event.endTime.date != null &&
      //   DateTime.compareDates(
      //     this.event.endTime.date,
      //     this.event.startTime.date
      //   ) == 0
      // ) {
      //   return this.event.startTime.time;
      // }

      return "";
    },
    eventEndDateMin() {
      // if (this.event.startTime.date != null) {
      //   return this.event.startTime.date;
      // }

      return new Date();
    },
    isSaveButtonDisabled() {
      if (
        this.event.name != null
        // this.event.startTime.date != null &&
        // this.event.startTime.time != null &&
        // this.event.endTime.date != null &&
        // this.event.endTime.time != null
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
    // "event.startTime": {
    //   deep: true,
    //   handler: function () {
    //     if (
    //       this.event.startTime.date != null &&
    //       this.event.startTime.time != null
    //     ) {
    //       this.eventStartTimeState = true;
    //     } else {
    //       this.eventStartTimeState = null;
    //     }
    //   },
    // },
    // "event.endTime": {
    //   deep: true,
    //   handler: function () {
    //     if (
    //       this.event.endTime.date != null &&
    //       this.event.endTime.time != null
    //     ) {
    //       this.eventEndTimeState = true;
    //     } else {
    //       this.eventEndTimeState = null;
    //     }
    //   },
    // },
  },
  methods: {
    onAddressSave(id) {
      this.event.addressId = id;
      this.showNewAddressForm = false;
    },
    getTimeFromString(timeString) {
      return DateTime.parseTimeString(timeString);
    },
    onClose() {
      this.close();
    },
    async onSubmit() {
      if (this.verifyForm()) {
        // const startTime = this.makeDateTime(this.event.startTime);
        // const endTime = this.makeDateTime(this.event.endTime);

        const eventData = {
          name: this.event.name,
          description: this.event.description,
          addressId: this.event.addressId,
          // startTime: startTime,
          // endTime: endTime,
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
      return DateTime.makeDateTime(datetime);
    },
    verifyForm() {
      this.resetStates();

      if (this.event.name == null) {
        this.eventNameState = false;
      }

      // if (this.event.startTime.date == null) {
      //   this.eventStartTimeState = false;
      //   this.eventStartTimeFeedback = "Start date is required";
      //   return false;
      // }

      // if (this.event.startTime.time == null) {
      //   this.eventStartTimeState = false;
      //   this.eventStartTimeFeedback = "Start time is required";
      //   return false;
      // }

      // if (this.event.endTime.date == null) {
      //   this.eventEndTimeState = false;
      //   this.eventEndTimeFeedback = "End date is required";
      //   return false;
      // }

      // if (this.event.endTime.time == null) {
      //   this.eventEndTimeState = false;
      //   this.eventEndTimeFeedback = "End time is required";
      //   return false;
      // }

      return true;
    },
    resetStates() {
      this.eventNameState = null;
      // this.eventStartTimeState = null;
      // this.eventEndTimeState = null;
    },
    close() {
      this.event.name = "";
      this.event.description = "";
      this.event.date = null;
      this.event.time = {
        start: null,
        end: null
      };
      // this.event.startTime = {};
      // this.event.endTime = {};
      // this.eventStartTimeFeedback = "";
      // this.eventEndTimeFeedback = "";
      this.eventNameState = null;
      // this.eventStartTimeState = null;
      // this.eventEndTimeState = null;
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