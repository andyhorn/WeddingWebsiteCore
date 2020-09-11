<template>
  <b-icon :icon="icon" :variant="variant" />
</template>

<script>
export default {
  name: "RsvpIndicator",
  props: ["rsvp"],
  computed: {
    icon() {
      if (!!this.rsvp && this.rsvp.isAttending === true) {
        return "hand-thumbs-up";
      }

      if (!!this.rsvp && this.rsvp.isAttending === false) {
        return "hand-thumbs-down";
      }

      return "question-circle";
    },
    variant() {
      if (!!this.rsvp && this.rsvp.isAttending === true) {
        return "success";
      }

      if (!!this.rsvp && this.rsvp.isAttending === false) {
        return "danger";
      }

      return "default";
    },
  },
};
</script>