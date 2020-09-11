<template>
  <b-icon :icon="icon" :variant="variant" :animation="animation" font-scale="1.8" />
</template>

<script>
const PENDING_ICON = "stopwatch";
const PENDING_COLOR = "primary";
const ACCEPT_ICON = "hand-thumbs-up";
const ACCEPT_COLOR = "success";
const DECLINE_ICON = "hand-thumbs-down";
const DECLINE_COLOR = "danger";
const UNSENT_ICON = "x";
const UNSENT_COLOR = "default";

export default {
  name: "RsvpIndicator",
  props: ["rsvp"],
  computed: {
    hasInvite() {
      return !!this.rsvp;
    },
    isPending() {
      return !!this.rsvp && !this.rsvp.hasResponded;
    },
    isAttending() {
      return !!this.rsvp && this.rsvp.hasResponded && this.rsvp.isAttending;
    },
    icon() {
      if (!this.hasInvite) {
        return UNSENT_ICON;
      }

      if (this.hasInvite && this.isPending) {
        return PENDING_ICON;
      }

      if (this.hasInvite && this.isAttending) {
        return ACCEPT_ICON;
      }

      return DECLINE_ICON;
    },
    variant() {
      if (!this.hasInvite) {
        return UNSENT_COLOR;
      }

      if (this.hasInvite && this.isPending) {
        return PENDING_COLOR;
      }

      if (this.hasInvite && this.isAttending) {
        return ACCEPT_COLOR;
      }

      return DECLINE_COLOR;
    },
    animation() {
      if (this.icon == PENDING_ICON) {
        return "throb";
      }

      return null;
    },
  },
};
</script>