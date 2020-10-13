<template>
  <b-dropdown text="Event RSVPs" right size="sm" variant="primary">
    <b-dropdown-form @submit.prevent="onFormSubmit">
      <table class="table">
        <thead>
          <tr>
            <th>Event</th>
            <th>Invited?</th>
            <th>Response</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="event in events" :key="event.eventId">
            <td class="text-center">{{ event.name }}</td>
            <td class="text-center">
              <b-form-checkbox switch v-model="invites" :value="event.eventId" />
            </td>
            <td class="text-center">
              <RsvpIndicator :rsvp="rsvps.find(x => x.eventId == event.eventId)" />
            </td>
          </tr>
        </tbody>
      </table>
      <b-button variant="outline-success" squared size="sm" type="submit" :disabled="isBusy">
        <span v-if="!isBusy">Save</span>
        <span v-else>
          <b-spinner />
        </span>
      </b-button>
    </b-dropdown-form>
  </b-dropdown>
</template>

<script>
import RsvpIndicator from "@/components/Admin/Guests/RsvpIndicator";
import { ACTIONS } from "@/store";

export default {
  name: "RsvpInviteForm",
  components: {
    RsvpIndicator,
  },
  props: ["guestId"],
  computed: {
    events() {
      return this.$store.getters.events;
    },
    rsvps() {
      return this.$store.getters.findRsvpsForGuest(this.guestId);
    },
  },
  data() {
    return {
      invites: [],
      isBusy: false,
    };
  },
  mounted() {
    this.setInvites();
  },
  methods: {
    setInvites() {
      this.invites = [];
      for (let event of this.events) {
        const rsvp = this.rsvps.find((x) => x.eventId == event.eventId);

        if (rsvp) {
          this.invites.push(event.eventId);
        }
      }
    },
    async onFormSubmit() {
      this.isBusy = true;
      const updates = this.events.map((event) => {
        return new Promise(async (resolve) => {
          const isInvited = this.invites.includes(event.eventId);
          await this.updateInviteForEvent(event.eventId, isInvited);
          resolve();
        });
      });

      await Promise.all(updates);
      this.setInvites();
      this.isBusy = false;
    },
    async updateInviteForEvent(eventId, isInvited) {
      const existingInvite = this.rsvps.find((x) => x.eventId == eventId);

      if (!!existingInvite && !isInvited) {
        // UNINVITE
        await this.$store.dispatch(
          ACTIONS.RSVP_ACTIONS.DELETE,
          existingInvite.rsvpId
        );
      } else if (!existingInvite && isInvited) {
        // NEW INVITE
        const newRsvp = {
          eventId: eventId,
          guestId: this.guestId,
        };

        await this.$store.dispatch(ACTIONS.RSVP_ACTIONS.CREATE, newRsvp);
      }
    },
  },
};
</script>