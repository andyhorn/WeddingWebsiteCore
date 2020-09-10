<template>
  <div class="container d-flex flex-column align-items-center">
    <b-calendar v-model="date" hide-header :min="dateMin" />
    <TimePicker v-model="time" :min="timeMin" class="mt-3" />
  </div>
</template>

<script>
import TimePicker from "./TimePicker";
import * as DateTime from "@/helpers/dateTime";

export default {
  name: "DateTimePicker",
  components: {
    TimePicker,
  },
  props: {
    value: {
      type: Object,
      required: true,
    },
    dateMin: {
      type: String | null,
      required: false,
      default: "",
    },
    timeMin: {
      type: String,
      required: false,
      default: "",
    },
  },
  data() {
    return {
      date: null,
      time: "00:00:00",
    };
  },
  watch: {
    dateMin: {
      deep: true,
      handler: function () {
        if (this.date != null) {
          const currentDateIsLessThanMinimumDate =
            DateTime.compareDates(this.date, this.dateMin) == -1;

          if (currentDateIsLessThanMinimumDate) {
            this.date = this.dateMin;
          }
        }
      },
    },
    date: {
      deep: true,
      handler: function () {
        this.emit();
      },
    },
    time: {
      deep: true,
      handler: function () {
        this.emit();
      },
    },
    value: {
      deep: true,
      immediate: true,
      handler: function (newVal) {
        if (newVal.date != null && newVal.date != this.date) {
          this.date = newVal.date;
        }
        if (newVal.time != null && newVal.time != this.time) {
          if (newVal.time > 12) {
            this.time = newVal.time % 12;
          }
          this.time = newVal.time;
        }
      },
    },
  },
  methods: {
    emit() {
      const data = {
        date: this.date,
        time: this.time,
      };

      // console.log(data);
      this.$emit("input", data);
    },
  },
};
</script>