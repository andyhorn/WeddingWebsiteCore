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
            <b-row class="py-2">
              <b-col>
                <b-form-group label="Date">
                  <b-form-datepicker 
                    v-model="event.date" 
                    :min="eventDateMin" 
                    :state="eventDateState" />
                </b-form-group>
              </b-col>
            </b-row>
            <b-row class="py-2">
              <b-col>
                <b-form-group label="Start time">
                  <b-form-timepicker 
                    v-model="event.time.start" 
                    :state="eventStartTimeState" />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group label="End time"
                  :invalid-feedback="eventEndTimeFeedback"
                  :state="eventEndTimeState">
                  <b-form-timepicker 
                    reset-button
                    :reset-value="null"
                    v-model="event.time.end" 
                    :disabled="event.time.start == null" 
                    :state="eventEndTimeState"/>
                </b-form-group>
              </b-col>
            </b-row>
          </b-col>
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
          :disabled="!validateForm"
          @click="onSubmit"
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
        addressId: null,
        name: null,
        description: null,
        date: null,
        time: {
          start: null,
          end: null
        },
      },
      eventStartTimeState: null,
      eventEndTimeFeedback: null,
      eventEndTimeState: null,
      eventNameState: null,
      eventDateState: null,
      eventDateMin: new Date(),
      showNewAddressForm: false,
    };
  },
  computed: {
    addresses() {
      return this.$store.getters.addresses;
    },
    isSaveButtonDisabled() {
      return !this.validateForm();
    },
  },
  watch: {
    "event.name": {
      handler: function () {
        if (this.event.name == null) {
          this.eventNameState = null;
        } else if (!!this.event.name && !!this.event.name.trim()) {
          this.eventNameState = true;
        } else {
          this.eventNameState = false;
        }
      }
    },
    "event.time.end": {
      handler: function () {
        if (!this.event.time.end) {
          this.eventEndTimeState = null;
        } else {
          const endIsLessThan = DateTime.compareTimes(
            this.event.time.end,
            this.event.time.start
          ) == -1;

          if (endIsLessThan) {
            this.eventEndTimeState = false;
            this.eventEndTimeFeedback = "End time cannot be earlier than start time.";
          } else {
            this.eventEndTimeState = true;
          }
        }
      }
    },
    "event.date": {
      handler: function () {
        if (this.event.date == null) {
          this.eventDateState = null;
        } else {
          this.eventDateState = true;
        }
      }
    },
    "event.time.start": {
      handler: function () {
        if (this.event.time.start == null) {
          this.eventStartTimeState = null;
        } else {
          this.eventStartTimeState = true;
        }
      }
    }
  },
  methods: {
    onAddressSave(id) {
      this.event.addressId = id;
      this.showNewAddressForm = false;
    },
    onClose() {
      this.clear();
      this.$emit("close");
    },
    getTimes()
    {
      let data = {
        start: null,
        end: null
      }

      const start = new Date(this.event.date);

      const startHour = this.event.time.start.split(":")[0];
      const startMinute = this.event.time.start.split(":")[1];

      start.setHours(startHour);
      start.setMinutes(startMinute);
      start.setSeconds(0);
      start.setMilliseconds(0);

      data.start = start;

      if (this.event.time.end != null) {
        const end = new Date(this.event.date);

        const endHour = this.event.time.end.split(":")[0];
        const endMinute = this.event.time.end.split(":")[1];

        end.setHours(endHour);
        end.setMinutes(endMinute);
        end.setSeconds(0);
        end.setMilliseconds(0);

        data.end = end;
      }

      return { start: data.start, end: data.end };
    },
    async onSubmit() {
      if (this.validateForm()) {

        const { start, end } = this.getTimes();

        const eventData = {
          name: this.event.name,
          description: this.event.description,
          addressId: this.event.addressId,
          startTime: start,
          endTime: end
        };

        console.log(eventData)

        const id = await this.$store.dispatch(
          ACTIONS.EVENT_ACTIONS.CREATE,
          eventData
        );
        if (id) {
          this.onClose();
        } else {
          this.$bvToast.toast("Unable to save event", {
            title: "Error",
            variant: "danger",
          });
        }
      }
    },
    validateForm() {
      return this.eventStartTimeState 
        && this.eventEndTimeState != false 
        && this.eventNameState
        && this.eventDateState;
    },
    resetStates() {
      this.eventNameState = null;
      this.eventEndTimeState = null;
      this.eventStartTimeState = null;
      this.eventDateState = null;
      this.eventEndTimeFeedback = null;
    },
    clear() {
      this.resetStates();
      this.event.addressId = null;
      this.event.name = null;
      this.event.description = null;
      this.event.date = null;
      this.event.time = {
        start: null,
        end: null
      };
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