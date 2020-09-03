<template>
  <div class="container">
    <b-row class="mb-1">
      <b-col cols="4" class="mx-0 p-1">
        <b-datepicker
          v-if="showDate"
          @input="onDateInput"
          :value="value.date"
          button-only
          :min="minDate"
          :max="maxDate"
        />
      </b-col>
      <b-col class="d-flex justify-content-center align-items-center">{{ date }}</b-col>
    </b-row>
    <b-row class="mt-1">
      <b-col cols="4" class="mx-0 p-1">
        <b-timepicker v-if="showTime" @input="onTimeInput" :value="value.time" button-only hour12 />
      </b-col>
      <b-col class="d-flex justify-content-center align-items-center">{{ time }}</b-col>
    </b-row>
  </div>
</template>

<script>
export default {
    name: "DateTimePicker",
    props: {
        variant: {
            type: String,
            default: "primary"
        },
        value: {
            type: Object,
            required: true
        },
        minTime: {
            type: [String]
        },
        maxTime: {
            type: [String]
        },
        minDate: {
            type: [String, Date]
        },
        maxDate: {
            type: [String, Date]
        }
    },
    data() {
        return {
            showDate: true,
            showTime: false,
            title: "None Selected",
            date: null,
            time: null
        }
    },
    computed: {
        buttonType() {
            return `outline-${this.variant}`;
        }
    },
    methods: {
        onDateInput(val) {
            this.date = val;
            this.showTime = true;
        },
        onTimeInput(val) {
            if (this.verifyMinTime(val) && this.verifyMaxTime(val)) {
                this.time = val;
                this.emit();
            }
        },
        verifyMinTime(val) {
            if (this.minTime == null) return true;

            const minHour = this.minTime.split(":")[0];
            const minMinute = this.minTime.split(":")[1];

            const hour = val.split(":")[0];
            const minute = val.split(":")[1];

            if (hour > minHour) return true;
            if (minute >= minMinute) return true;

            return false;
        },
        verifyMaxTime(val) {
            if (this.maxTime == null) return true;

            const maxHour = this.maxTime.split(":")[0];
            const maxMinute = this.maxTime.split(":")[1];

            const hour = val.split(":")[0];
            const minute = val.split(":")[1];

            if (hour < maxHour) return true;
            if (minute <= maxMinute) return true;

            return false;
        },
        emit() {
            const data = {
                date: this.date,
                time: this.time
            };

            this.$emit("input", data);
        }
    }
}
</script>