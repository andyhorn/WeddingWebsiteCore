<template>
  <b-container>
    <b-row>
      <b-col>
        <p class="m-0 p-0">{{ guest.firstName }} {{ guest.lastName }}</p>
        <p v-if="guest.isChild" class="m-0 p-0 text-subtitle text-italic">Child</p>
      </b-col>
      <b-col v-if="guest.familyId == null && families.length > 0">
        <b-form-group
          id="family-assignment-group"
          label="Assign to family"
          label-for="family-assignment"
        >
          <b-select v-model="familyAssignmentId">
            <b-select-option
              v-for="family in families"
              :key="family.familyId"
              :value="family.familyId"
            >{{ family.name }}</b-select-option>
          </b-select>
        </b-form-group>
        <b-button size="sm" squared variant="success" @click="onFamilyAssignment">Save</b-button>
      </b-col>
      <b-col class="d-flex align-items-center justify-content-end">
        <b-button-close @click="onDelete" />
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import Box from "@/components/Box.vue";
import { ACTIONS } from "@/store";

export default {
  name: "Guest",
  props: ["guest"],
  data() {
    return {
      familyAssignmentId: null
    }
  },
  computed: {
    families() {
      return this.$store.getters.families;
    }
  },
  methods: {
    async onDelete() {
      const name = this.guest.firstName + " " + this.guest.lastName;
      if (confirm("Are you sure you want to remove " + name)) {
        const familyId = this.guest.familyId;
        const guestId = this.guest.guestId;

        await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.DELETE, guestId);

        if (familyId) {
          await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.FETCH, familyId);
        }
      }
    },
    async onFamilyAssignment() {
      const data = {... this.guest};
      const familyId = this.familyAssignmentId;

      data.familyId = familyId;
      await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.UPDATE, data);

      await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.FETCH, familyId);

      this.familyAssignmentId = null;
    }
  }
};
</script>

<style scoped>
p.text-italic {
  font-style: italic;
}
p.text-subtitle {
  font-size: 0.7rem;
}
</style>