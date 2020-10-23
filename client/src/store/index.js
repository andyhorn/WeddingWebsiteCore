import Vue from 'vue'
import Vuex from 'vuex'
import authentication from "@/store/modules/authentication.store.js";
import guests from "@/store/modules/guests.store.js";
import families from "@/store/modules/families.store.js";
import events from "@/store/modules/events.store.js";
import addresses from "@/store/modules/addresses.store.js";
import rsvps from "@/store/modules/rsvps.store.js";
import categories from "@/store/modules/categories.store.js";
import accommodations from "@/store/modules/accommodations.store.js";
import registries from "@/store/modules/registries.store.js";
import weddingRoles from "@/store/modules/weddingRoles.store.js";

Vue.use(Vuex);

export const TOKEN_IDENTIFIER = "x-kaw-auth-token-x";
export const ACTIONS = {
  FAMILY_ACTIONS: families.ACTIONS,
  GUEST_ACTIONS: guests.ACTIONS,
  EVENT_ACTIONS: events.ACTIONS,
  AUTHENTICATION_ACTIONS: authentication.AUTHENTICATION_ACTIONS,
  ADDRESS_ACTIONS: addresses.ACTIONS,
  RSVP_ACTIONS: rsvps.ACTIONS,
  ACCOMMODATION_ACTIONS: accommodations.ACTIONS,
  CATEGORY_ACTIONS: categories.ACTIONS,
  REGISTRY_ACTIONS: registries.ACTIONS,
  WEDDING_ROLE_ACTIONS: weddingRoles.ACTIONS,
}

const defaultState = {
  error: "",
  status: ""
};

function initialState() {
  return defaultState;
}

export default new Vuex.Store({
  state: initialState,
  mutations: {
    reset(state) {
      const s = initialState();
      Object.keys(s).forEach(key => state[key] = s[key]);
    },
  },
  actions: {},
  modules: {
    authentication,
    guests,
    families,
    events,
    addresses,
    rsvps,
    categories,
    accommodations,
    registries,
    weddingRoles,
  },
  getters: {

  }
})
