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
  actions: {
    // login({ commit }, payload) {
    //   return new Promise(async (resolve) => {
    //     const toast = new Toast();
    //     const email = payload.email;
    //     const password = payload.password;

    //     if (!email) {
    //       toast.setTitle("Authentication error.");
    //       toast.setMessage("Email is required.");
    //       toast.setVariant("danger");
    //       toast.show();
    //       return resolve(false);
    //     }

    //     if (!password) {
    //       toast.setTitle("Authentication error.");
    //       toast.setMessage("Password is required.");
    //       toast.setVariant("danger");
    //       toast.show();
    //       return resolve(false);
    //     }

    //     commit("authenticationAttempt");

    //     http.post("users/login", {
    //       email,
    //       password
    //     })
    //       .then(res => {
    //         const data = res.data;
    //         const userId = data.userId;
    //         const firstName = data.firstName;
    //         const lastName = data.lastName;
    //         const token = data.token;

    //         const userData = {
    //           userId,
    //           firstName,
    //           lastName,
    //           email
    //         };

    //         commit("setUserData", userData);
    //         commit("setToken", token);

    //         commit("authenticationSuccess");

    //         toast.setTitle("Logged in!");
    //         toast.setMessage("Authentication successful!");
    //         toast.setVariant("success");
    //         toast.show();

    //         return resolve(true);
    //       })
    //       .catch(err => {
    //         const status = err.response.data.status;

    //         toast.setTitle("Authentication error.");
    //         toast.setVariant("danger");

    //         if (status == 401) {
    //           toast.setMessage("Password does not match. Try again.");
    //         } else if (status == 404) {
    //           toast.setMessage("Email not found.");
    //         } else {
    //           toast.setMessage("Unable to log in. Check your credentials and try again.");
    //         }

    //         commit("authenticationFailure", err);
    //         toast.show();
    //         return resolve(false);
    //       });
    //   })
    // },
    // logout({ commit }) {
    //   commit("logout");
    //   const toast = new Toast();
    //   toast.setMessage("You have successfully logged out.");
    //   toast.setVariant("success");
    //   toast.setTitle("Logged out!");
    //   toast.show();
    // },
  },
  modules: {
    authentication,
    guests,
    families,
    events
  },
  getters: {

  }
})
