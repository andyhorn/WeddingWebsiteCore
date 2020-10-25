<template>
  <b-container>
    <h1 class="text-center mt-3 mb-5">RSVPs</h1>
    <b-table :fields="eventFields" :items="events" selectable select-mode="single" @row-selected="onEventSelected" show-empty>
      
      <template v-slot:cell(invites)="row">
        {{ invitesForEvent(row.item.eventId).length }}
      </template>

      <template v-slot:cell(responses)="row">
        {{ invitesForEvent(row.item.eventId).filter(x => x.hasResponded).length }}
      </template>

      <template v-slot:cell(attending)="row">
        {{ invitesForEvent(row.item.eventId).filter(x => x.isAttending).length }}
      </template>

      <template v-slot:cell(denied)="row">
        {{ invitesForEvent(row.item.eventId).filter(x => x.hasResponded && !x.isAttending).length }}
      </template>

      <template v-slot:row-details="row">
        <b-container class="border border-secondary m-0 p-3">
          <b-table :fields="guestFields" :items="guestsForEvent(row.item.eventId)" show-empty>

            <template v-slot:cell(name)="guestRow">
              {{ printGuestName(guestRow.item.guestId) }}
            </template>

            <template v-slot:cell(status)="guestRow">
              {{ getGuestStatusForEvent(guestRow.item.guestId, row.item.eventId) }}
            </template>

            <template v-slot:cell(options)="guestRow">
              <RsvpInviteForm :guestId="guestRow.item.guestId" />
            </template>

          </b-table>
        </b-container>
      </template>

    </b-table>
  </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
import RsvpInviteForm from "@/components/Admin/Guests/RsvpInviteForm";

export default {
    name: "AdminRSVP",
    components: {
      RsvpInviteForm
    },
    data() {
      return {
        eventFields: [
          "name",
          "invites",
          "responses",
          "attending",
          "denied"
        ],
        guestFields: [
          "name",
          "status",
          "options"
        ]
      }
    },
    mounted() {
      this.fetch();
    },
    computed: {
      guests() {
        return this.$store.getters.guests;
      },
      rsvps() {
        return this.$store.getters.rsvps;
      },
      events() {
        return this.$store.getters.events;
      }
    },
    methods: {
      onEventSelected(rows) {
        const openEvents = rows.map(x => x.eventId);

        for (let event of this.events) {
          const isOpen = openEvents.includes(event.eventId);

          if (event._showDetails != isOpen)
            this.$set(event, "_showDetails", isOpen);
        }
      },
      invitesForEvent(eventId) {
        const rsvps = this.rsvps.filter(x => x.eventId == eventId) || [];
        return rsvps;
      },
      guestsForEvent(eventId) {
        const guestIds = this.rsvps
          .filter(x => x.eventId == eventId)
          .map(x => x.guestId);

        const guests = this.guests.filter(x => guestIds.includes(x.guestId));
        return guests;
      },
      printGuestName(guestId) {
        const guest = this.guests.find(x => x.guestId == guestId);
        return `${guest.firstName} ${guest.lastName}`;
      },
      getGuestStatusForEvent(guestId, eventId) {
        const rsvp = this.rsvps.find(x => x.guestId == guestId && x.eventId == eventId);

        if (rsvp == null) return "Not invited";

        if (!rsvp.hasResponded) return "No response";

        if (rsvp.isAttending) return "Attending";

        return "Not attending";
      },
      async fetch() {
        await Promise.all([
          this.fetchGuests(),
          this.fetchRsvps(),
          this.fetchEvents()
        ]);
      },
      async fetchGuests() {
        await this.$store.dispatch(ACTIONS.GUESTS.FETCH_ALL);
      },
      async fetchRsvps() {
        await this.$store.dispatch(ACTIONS.RSVPS.FETCH_ALL);
      },
      async fetchEvents() {
        await this.$store.dispatch(ACTIONS.EVENTS.FETCH_ALL);
      }
    }
}
</script>