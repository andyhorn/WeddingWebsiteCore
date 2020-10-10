<template>
  <div class="time-container d-flex align-items-center">
    <b-form-spinbutton
      vertical
      :min="hourMin"
      :max="hours.length - 1"
      :formatter-fn="hourFormatter"
      v-model="hourIndex"
    />
    <span class="colon">:</span>
    <b-form-spinbutton
      vertical
      :min="minuteMin"
      max="59"
      step="step"
      :formatter-fn="formatter"
      v-model="minute"
    />
    <span v-if="showSeconds" class="colon">:</span>
    <b-form-spinbutton
      vertical
      :min="secondMin"
      max="59"
      v-if="showSeconds"
      :formatter-fn="formatter"
      v-model="second"
    />
    <b-form-spinbutton
      :disabled="isAmPmSelectorDisabled"
      class="ml-3"
      vertical
      min="0"
      max="1"
      v-model="isAm"
      :formatter-fn="() => isAm == 0 ? 'AM' : 'PM'"
      wrap
    />
  </div>
</template>

<script>
import * as DateTime from "@/helpers/dateTime";

export default {
  name: "TimePicker",
  props: {
    step: {
      type: Number,
      default: 1,
    },
    showSeconds: {
      type: Boolean,
      default: false,
    },
    min: {
      type: String,
      default: "00:00:00",
    },
    value: {
      type: String | null,
      required: true,
    },
  },
  data() {
    return {
      isAm: 0,
      isAmPmSelectorDisabled: false,
      hourIndex: 0,
      minute: 0,
      second: 0,
      hours: [],
      minutes: [],
      seconds: [],
    };
  },
  created() {
    this.hours.push("12");
    for (let i = 1; i < 12; i++) {
      this.hours.push(i.toString());
    }
    for (let i = 0; i < 60; i++) {
      // const value = String("00" + i.toString()).slice(-2);
      this.minutes.push(i);
      this.seconds.push(i);
    }

    if (this.value != null && this.value.includes(":")) {
      const hour = this.value.split(":")[0];

      if (parseInt(hour) == 12) {
        this.hourIndex = 0;
        this.isAm = 1;
      } else if (parseInt(hour) > 12) {
        this.hourIndex = parseInt(hour) % 12;
        this.isAm = 1;
      }
    }
  },
  watch: {
    hourMin: {
      immediate: true,
      handler: function () {
        if (this.hourMin != null && this.hourIndex < this.hourMin) {
          this.hourIndex = this.hourMin;
        }
      },
    },
    minuteMin: {
      immediate: true,
      handler: function () {
        if (this.minuteMin != null && this.minute < this.minuteMin) {
          this.minute = this.minuteMin;
        }
      },
    },
    secondMin: {
      immediate: true,
      handler: function () {
        if (this.secondMin != null && this.second < this.secondMin) {
          this.second = this.secondMin;
        }
      },
    },
    hourIndex() {
      this.emitValue();
    },
    minute() {
      this.emitValue();
    },
    second() {
      this.emitValue();
    },
    isAm() {
      this.emitValue();
    },
  },
  computed: {
    hourMin() {
      // restrict the hour values by index
      if (!this.min) {
        return 0;
      }

      const hourMin = parseInt(this.min.split(":")[0]);

      if (hourMin >= 12) {
        this.isAm = 1;
        this.isAmPmSelectorDisabled = true;
      } else {
        this.isAmPmSelectorDisabled = false;
      }

      if (hourMin == 12 || hourMin == 0) {
        return 0;
      }

      if (hourMin >= 12) {
        return this.hours.findIndex((h) => h == hourMin - 12);
      } else {
        return this.hours.findIndex((h) => h == hourMin);
      }
    },
    minuteMin() {
      if (!this.min || !this.min.includes(":")) {
        return 0;
      }

      const minuteMin = parseInt(this.min.split(":")[1]);

      return minuteMin;
    },
    secondMin() {
      if (!this.min || !this.min.includes(":")) {
        return 0;
      }

      const secondMin = parseInt(this.min.split(":")[2]);

      return secondMin;
    },
  },
  methods: {
    hourFormatter(index) {
      const val = this.hours[index];
      return this.formatter(val);
    },
    formatter(num) {
      let val = "00" + num;
      val = val.slice(-2);
      return val.toString();
    },
    emitValue() {
      const vm = this;
      this.$nextTick(() => {
        const value = vm.makeTimeString();
        vm.$emit("input", value);
      });
    },
    makeTimeString() {
      let hourValue = this.hours[this.hourIndex];

      if (hourValue == 12 && this.isAm == 1) {
        hourValue = 0;
      }

      const hour = this.isAm == 0 ? hourValue : Number(hourValue) + 12;

      const hours = this.formatter(hour == 24 ? 0 : hour);
      const minutes = this.formatter(this.minute);
      const seconds = this.formatter(this.second);

      return `${hours}:${minutes}:${seconds}`;
    },
  },
};
</script>

<style scoped>
.time-container > .colon {
  font-size: 3rem;
}
</style>