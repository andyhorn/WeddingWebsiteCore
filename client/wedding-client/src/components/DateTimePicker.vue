<template>
  <div class="container d-flex flex-column align-items-center">
    <b-calendar v-model="date" hide-header :min="new Date(dateMin)" />
    <TimePicker v-model="time" :min="timeMin" class="mt-3" />
  </div>
</template>

<script>
import TimePicker from "./TimePicker";

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
      type: String,
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
      time: null,
    };
  },
  watch: {
    dateMin: {
      handler: function () {
        if (this.date != null && this.dateMin > new Date(this.date)) {
          this.date = this.dateMin.toDateString();
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
        console.log(newVal);
        if (newVal.date != null && newVal.date != this.date) {
          this.date = newVal.date;
        }
        if (newVal.time != null && newVal.time != this.time) {
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

      console.log(data);
      this.$emit("input", data);
    },
  },
};
</script>