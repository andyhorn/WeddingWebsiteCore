<template>
  <div class="container py-5">
    <h1>Event details</h1>
    <b-form @submit.prevent="onSaveEvent" v-if="!!event">
      <b-form-group label="Title">
        <b-input v-model="event.name" />
      </b-form-group>
      <b-form-group label="Description">
        <b-textarea v-model="event.description" />
      </b-form-group>
      <b-form-row>
        <b-form-group class="col" label="Date">
          <b-form-datepicker v-model="date" />
        </b-form-group>
      </b-form-row>
      <b-form-row>
        <b-form-group class="col" label="Start time">
          <b-form-timepicker v-model="startTime" />
        </b-form-group>
        <b-form-group class="col" label="End time">
          <b-form-timepicker v-model="endTime" />
        </b-form-group>
      </b-form-row>
      <b-form-group label="Location">
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
            <b-button v-b-toggle.newAddressCollapse variant="primary">New address</b-button>
          </b-input-group-append>
        </b-input-group>
      </b-form-group>
      <b-collapse
        id="newAddressCollapse"
        class="border rounded p-3 mb-5"
        v-model="showNewAddressForm"
      >
        <NewAddressForm @saved="onAddressSaved" />
      </b-collapse>
      <b-row>
        <b-col>
          <b-button squared variant="success" type="submit">Save</b-button>
        </b-col>
      </b-row>
    </b-form>
  </div>
</template>

<script>
import DateTimePicker from "@/components/DateTimePicker";
import NewAddressForm from "@/components/forms/NewAddressForm";
import * as DateTime from "@/helpers/dateTime";
import { deepCopy } from "@/helpers/utils";
import { ACTIONS } from "@/store";

export default {
  name: "AdminEventDetails",
  components: {
    DateTimePicker,
    NewAddressForm,
  },
  data() {
    return {
      event: null,
      date: null,
      startTime: null,
      endTime: null,
      showNewAddressForm: false,
    };
  },
  computed: {
    addresses() {
      return this.$store.getters.addresses;
    },
  },
  mounted() {
    const id = this.$route.params.id;
    this.fetch(id);
    this.$store.dispatch(ACTIONS.ADDRESS_ACTIONS.FETCH_ALL);
  },
  methods: {
    fetch(id) {
      const event = this.$store.getters.findEvent(id);
      this.event = event;

      const startDate = new Date(event.startTime);
      this.date = startDate;

      const startTime = startDate.toLocaleTimeString([], {
        hour12: false
      });

      const endTime = event.endTime == null 
        ? null
        : new Date(event.endTime).toLocaleTimeString([], {
          hour12: false
        });

      this.startTime = startTime;
      this.endTime = endTime;
    },
    async onSaveEvent() {
      const times = this.getTimes();

      const saveData = Object.assign({}, deepCopy(this.event), 
        { startTime: times.start }, 
        { endTime: times.end });

      const id = this.event.eventId;
      const success = await this.$store.dispatch(ACTIONS.EVENT_ACTIONS.UPDATE, saveData);

      if (success) {
        this.fetch(id);
        this.$bvToast.toast("Event updated!", {
          title: "Success",
          variant: "success"
        });
      }
    },
    onAddressSaved(id) {
      this.event.addressId = id;
      this.showNewAddressForm = false;
    },
    getTimes() {
      const getTime = (time) => {
        return {
          hour: time.split(":")[0],
          minute: time.split(":")[1]
        };
      };

      let data = {
        start: null,
        end: null
      };

      const start = new Date(this.date);
      const startTimes = getTime(this.startTime);

      start.setHours(startTimes.hour);
      start.setMinutes(startTimes.minute);
      start.setSeconds(0);
      start.setMilliseconds(0);

      data.start = start;

      if (this.endTime != null) {
        const endTimes = getTime(this.endTime);

        const end = new Date(this.date);
        end.setHours(endTimes.hour);
        end.setMinutes(endTimes.minute);
        end.setSeconds(0);
        end.setMilliseconds(0);

        data.end = end;
      }

      return data;
    }
  },
};
</script>

<style scoped>
</style>