import Vue from 'vue'
import Vuex from 'vuex'
import { http, addToken, stripToken } from "@/axios";

Vue.use(Vuex);

export const TOKEN_IDENTIFIER = "x-kaw-auth-token-x";

const defaultState = {
  token = "",
  user = {
    id = "",
    firstName = "",
    lastName = "",
    email = ""
  }
};

function initialState() {
  return defaultState;
}

export default new Vuex.Store({
  state: initialState,
  mutations: {
    reset(state) {
      const s = initialState();
      Object.keys(s).forEach(key => state[key] = s[key);]
    }
  },
  actions: {
  },
  modules: {
  }
})
