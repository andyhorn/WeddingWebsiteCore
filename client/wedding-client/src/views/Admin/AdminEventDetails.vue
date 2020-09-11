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
        <b-form-group class="col" label="Start time">
          <DateTimePicker v-model="startTime" />
        </b-form-group>
        <b-form-group class="col" label="End time">
          <DateTimePicker v-model="endTime" />
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
      <b-collapse id="newAddressCollapse" class="border rounded p-3 mb-5">
        <NewAddressForm @saved="id => event.addressId = id" />
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
    };
  },
  computed: {
    addresses() {
      return this.$store.getters.addresses;
    },
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
    this.fetch(id);
    this.$store.dispatch(ACTIONS.ADDRESS_ACTIONS.FETCH_ALL);
  },
  methods: {
    fetch(id) {
      const event = this.$store.getters.findEvent(id);
      this.event = event;
    },
    async onSaveEvent() {
      const saveData = deepCopy(this.event);
      const id = this.event.eventId;
      await this.$store.dispatch(ACTIONS.EVENT_ACTIONS.UPDATE, saveData);
      this.fetch(id);
    },
  },
};
</script>

<style scoped>
</style>