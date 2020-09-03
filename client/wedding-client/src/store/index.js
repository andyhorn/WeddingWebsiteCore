import Vue from 'vue'
import Vuex from 'vuex'
import authentication from "@/store/modules/authentication.store.js";
import guests from "@/store/modules/guests.store.js";
import families from "@/store/modules/families.store.js";
import events from "@/store/modules/events.store.js";

Vue.use(Vuex);

export const TOKEN_IDENTIFIER = "x-kaw-auth-token-x";
export const ACTIONS = {
  FAMILY_ACTIONS: families.FAMILY_ACTIONS,
  GUEST_ACTIONS: guests.GUEST_ACTIONS,
  AUTHENTICATION_ACTIONS: authentication.AUTHENTICATION_ACTIONS
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
    events
  },
  getters: {

  }
})
