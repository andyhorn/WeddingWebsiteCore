<template>
  <b-container>
    <b-row>
      <b-col>
        <div class="d-flex align-items-center justify-content-start">
          <b-button squared size="sm" variant="success" class="my-2 mr-2" @click="toggleCollapse">
            <b-icon :icon="collapseOpen ? 'chevron-up': 'chevron-down'" />
            <b-icon-pencil />
          </b-button>
          <p class="m-0 p-0">
            <b-icon-star-fill variant="warning" v-if="guest.isWeddingMember" />
            {{ guest.firstName }} {{ guest.lastName }}
          </p>
        </div>
        <p v-if="guest.isChild" class="m-0 p-0 text-subtitle text-italic">Child</p>
      </b-col>
      <b-col class="d-flex align-items-center justify-content-end">
        <b-button-close @click="onDelete" />
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <b-collapse :id="collapseId" class="mt-3">
          <b-row>
            <b-col>
              <b-form-group
                id="first-name-group"
                label="First name"
                label-for="first-name"
                :state="firstNameState"
                invalid-feedback="First name is required."
              >
                <b-input v-model="guest.firstName" id="first-name" :state="firstNameState" />
              </b-form-group>
            </b-col>
            <b-col>
              <b-form-group
                id="last-name-group"
                label="Last name"
                label-for="last-name"
                :state="lastNameState"
                invalid-feedback="Last name is required."
              >
                <b-input v-model="guest.lastName" id="last-name" :state="lastNameState" />
              </b-form-group>
            </b-col>
          </b-row>
          <b-row>
            <b-col cols="2" class="d-flex justify-content-center">
              <b-form-group id="is-child-group" label="Is a Child" label-for="child-switch">
                <b-form-checkbox switch v-model="guest.isChild" />
              </b-form-group>
            </b-col>
            <b-col cols="4" class="d-flex justify-content-center">
              <b-form-group
                id="is-wedding-member-group"
                label="Is a Wedding Party member"
                label-for="wedding-member-switch"
              >
                <b-form-checkbox switch v-model="guest.isWeddingMember" />
              </b-form-group>
            </b-col>
            <b-col>
              <b-form-group
                id="family-assignment-group"
                label="Assign to family"
                label-for="family-assignment"
              >
                <b-select v-model="guest.familyId" :disabled="families.length == 0">
                  <b-select-option
                    v-for="family in families"
                    :key="family.familyId"
                    :value="family.familyId"
                  >{{ family.name }}</b-select-option>
                </b-select>
              </b-form-group>
            </b-col>
          </b-row>
          <b-row>
            <b-col>
              <b-button
                :disabled="!firstNameState || !lastNameState"
                size="sm"
                squared
                variant="success"
                @click="onSave"
              >Save</b-button>
            </b-col>
          </b-row>
        </b-collapse>
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
      collapseOpen: false,
    }
  },
  computed: {
    families() {
      return this.$store.getters.families;
    },
    collapseId() {
      return this.guest.guestId;
    },
    firstNameState() {
      return !!this.guest.firstName && !!this.guest.firstName.trim();
    },
    lastNameState() {
      return !!this.guest.lastName && !!this.guest.lastName.trim();
    }
  },
  mounted() {
    console.log(this.guest)
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
    async onSave() {
      const data = {... this.guest};
      const familyId = this.familyAssignmentId;

      data.familyId = familyId;
      await this.$store.dispatch(ACTIONS.GUEST_ACTIONS.UPDATE, data);

      await this.$store.dispatch(ACTIONS.FAMILY_ACTIONS.FETCH, familyId);

      this.familyAssignmentId = null;
    },
    toggleCollapse() {
      this.$root.$emit("bv::toggle::collapse", this.collapseId);
      this.collapseOpen = !this.collapseOpen;
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