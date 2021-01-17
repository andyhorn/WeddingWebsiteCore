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
import registryIcons from "@/store/modules/registryIcons.store.js";
import weddingRoles from "@/store/modules/weddingRoles.store.js";
import images from "@/store/modules/images.store.js";

Vue.use(Vuex);

export const TOKEN_IDENTIFIER = "x-kaw-auth-token-x";

export const ACTIONS = {
  FAMILIES: families.ACTIONS,
  GUESTS: guests.ACTIONS,
  EVENTS: events.ACTIONS,
  AUTHENTICATION_ACTIONS: authentication.AUTHENTICATION_ACTIONS,
  ADDRESSES: addresses.ACTIONS,
  RSVPS: rsvps.ACTIONS,
  ACCOMMODATIONS: accommodations.ACTIONS,
  CATEGORIES: categories.ACTIONS,
  REGISTRIES: registries.ACTIONS,
  REGISTRY_ICONS: registryIcons.ACTIONS,
  WEDDING_ROLES: weddingRoles.ACTIONS,
  IMAGES: images.ACTIONS
};

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
    registryIcons,
    weddingRoles,
    images
  },
  getters: {

  }
})
