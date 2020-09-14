<template>
  <b-dropdown text="Event RSVPs" right size="sm" variant="primary">
    <b-dropdown-form @submit.prevent="onFormSubmit">
      <table class="table">
        <thead>
          <tr>
            <th>Event</th>
            <th>Status</th>
            <th>Options</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="event in events" :key="event.eventId">
            <td class="text-center">
              <router-link
                :to="{ name: 'Admin-Event-Details', params: { id: event.eventId }}"
              >{{ event.name }}</router-link>
            </td>
            <td class="text-center">{{ getEventStatus(event.eventId) }}</td>
            <td>
              <b-button-group size="sm">
                <b-button
                  variant="danger"
                  @click="onUninviteAll(event.eventId)"
                  :disabled="getEventStatus(event.eventId) == 'None'"
                >Uninvite All</b-button>
                <b-button
                  variant="success"
                  @click="onInviteAll(event.eventId)"
                  :disabled="getEventStatus(event.eventId) == 'All'"
                >Invite All</b-button>
              </b-button-group>
            </td>
          </tr>
        </tbody>
      </table>
    </b-dropdown-form>
  </b-dropdown>
</template>

<script>
import { ACTIONS } from "@/store";

export default {
  name: "FamilyRsvpInviteForm",
  props: ["familyId"],
  data() {
    return {
      invites: [],
    };
  },
  computed: {
    events() {
      return this.$store.getters.events;
    },
    rsvps() {
      return this.$store.getters.rsvps;
    },
    members() {
      const members = this.$store.getters.guests.filter(
        (x) => x.familyId == this.familyId
      );
      return members;
    },
  },
  methods: {
    getEventStatus(eventId) {
      const results = [];

      for (let member of this.members) {
        const rsvps = this.getRsvpsForGuest(member.guestId);
        const hasInvite = rsvps.some((x) => x.eventId == eventId);

        results.push(hasInvite);
      }

      return results.every((x) => x)
        ? "All"
        : results.every((x) => !x)
        ? "None"
        : "Mixed";
    },
    getRsvpsForGuest(guestId) {
      const rsvps = this.$store.getters.findRsvpsForGuest(guestId);
      return rsvps;
    },
    async onInviteAll(eventId) {
      const promises = [];

      const inviteGuest = (guestId) => {
        return new Promise(async (resolve) => {
          const rsvps = this.getRsvpsForGuest(guestId);
          const eventInvite = rsvps.find((x) => x.eventId == eventId);

          if (!eventInvite) {
            const newRsvp = {
              guestId,
              eventId,
            };

            await this.$store.dispatch(ACTIONS.RSVP_ACTIONS.CREATE, newRsvp);
          }

          resolve();
        });
      };

      this.members.forEach((guest) =>
        promises.push(inviteGuest(guest.guestId))
      );

      await Promise.all(promises);
    },
    async onUninviteAll(eventId) {
      const promises = [];

      const uninviteGuest = (guestId) => {
        return new Promise(async (resolve) => {
          const rsvps = this.getRsvpsForGuest(guestId);
          const eventInvite = rsvps.find((x) => x.eventId == eventId);

          if (eventInvite) {
            await this.$store.dispatch(
              ACTIONS.RSVP_ACTIONS.DELETE,
              eventInvite.rsvpId
            );
          }

          resolve();
        });
      };

      for (let guest of this.members) {
        promises.push(uninviteGuest(guest.guestId));
      }

      await Promise.all(promises);
    },
  },
};
</script>