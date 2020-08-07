import Vue from 'vue'
import Vuex from 'vuex'
import { http, setToken, stripToken } from "@/axios";
import Toast from "@/services/toast";

Vue.use(Vuex);

export const TOKEN_IDENTIFIER = "x-kaw-auth-token-x";

const defaultState = {
  error: "",
  token: "",
  isLoggedIn: false,
  guestList: [],
  families: [],
  user: {
    userId: "",
    firstName: "",
    lastName: "",
    email: ""
  },
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
    authenticationAttempt(state) {
      state.status = "Authenticating user...";
    },
    authenticationSuccess(state) {
      state.status = "Authentication successful!";
      state.isLoggedIn = true;
    },
    authenticationFailure(state, err) {
      state.status = "Authentication unsuccessful.";
      state.error = err;
    },
    setUserData(state, data) {
      state.status = "Saved user data.";
      state.user = data;
    },
    setUserFirstName(state, firstName) {
      state.user.firstName = firstName;
      state.status = "Saved user's first name.";
    },
    setUserLastName(state, lastName) {
      state.user.lastName = lastName;
      state.status = "Saved user's last name.";
    },
    setUserEmail(state, email) {
      state.user.email = email;
      state.status = "Saved user's email.";
    },
    setUserId(state, userId) {
      state.user.userId = userId;
      state.status = "Saved user's ID.";
    },
    setToken(state, token) {
      state.token = token;
      setToken(token);
      state.status = "Saved JWT.";
    },
    resetToken(state) {
      state.token = "";
      state.status = "Reset JWT.";
      stripToken();
    },
    resetUserData(state) {
      state.user = {};
      state.status = "Reset user data.";
    },
    logout(state) {
      state.user = {};
      state.token = "";
      state.status = "Logged out.";
      state.error = "";
      state.isLoggedIn = false;
    },
    setGuestList(state, guestList) {
      state.guestList = guestList;
    },
    setFamilies(state, families) {
      state.families = families;
    }
  },
  actions: {
    login({ commit }, payload) {
      return new Promise(async (resolve) => {
        const toast = new Toast();
        const email = payload.email;
        const password = payload.password;

        if (!email) {
          toast.setTitle("Authentication error.");
          toast.setMessage("Email is required.");
          toast.setVariant("danger");
          toast.show();
          return resolve(false);
        }

        if (!password) {
          toast.setTitle("Authentication error.");
          toast.setMessage("Password is required.");
          toast.setVariant("danger");
          toast.show();
          return resolve(false);
        }

        commit("authenticationAttempt");

        http.post("users/login", {
          email,
          password
        })
          .then(res => {
            const data = res.data;
            const userId = data.userId;
            const firstName = data.firstName;
            const lastName = data.lastName;
            const token = data.token;

            const userData = {
              userId,
              firstName,
              lastName,
              email
            };

            commit("setUserData", userData);
            commit("setToken", token);

            commit("authenticationSuccess");

            toast.setTitle("Logged in!");
            toast.setMessage("Authentication successful!");
            toast.setVariant("success");
            toast.show();

            return resolve(true);
          })
          .catch(err => {
            const status = err.response.data.status;

            toast.setTitle("Authentication error.");
            toast.setVariant("danger");

            if (status == 401) {
              toast.setMessage("Password does not match. Try again.");
            } else if (status == 404) {
              toast.setMessage("Email not found.");
            } else {
              toast.setMessage("Unable to log in. Check your credentials and try again.");
            }

            commit("authenticationFailure", err);
            toast.show();
            return resolve(false);
          });
      })
    },
    logout({ commit }) {
      commit("logout");
      const toast = new Toast();
      toast.setMessage("You have successfully logged out.");
      toast.setVariant("success");
      toast.setTitle("Logged out!");
      toast.show();
    },
    createNewGuest({ commit }, guest) {
      return new Promise(async (resolve) => {
        const toast = new Toast();
        let success;
        http.post("guests", guest)
          .then(res => {
            toast.setTitle("Success!");
            toast.setMessage("Guest created successfully.");
            toast.setVariant("success");
            toast.show();
            success = true;
          })
          .catch(err => {
            toast.setTitle("Error!");
            toast.setMessage("Something went wrong. Try again.");
            toast.setVariant("danger");
            toast.show();
            success = false;
          })
          .finally(() => resolve(success));
      })
    },
    deleteGuest({ commit }, guestId) {
      return new Promise(async (resolve) => {
        let success;
        let toast = new Toast();
        http.delete("guests/" + guestId)
          .then(res => {
            success = true;
            toast.setVariant("success");
            toast.setTitle("Success!");
            toast.setMessage("Guest deleted successfully.");
            toast.show();
          })
          .catch(err => {
            success = false;
            toast.setVariant("danger");
            toast.setTitle("Error!");
            toast.setMessage("Unable to delete guest. Try again.");
            toast.show();
          })
          .finally(() => resolve(success));
      })
    },
    fetchAllGuests({ commit }) {
      return new Promise(async (resolve) => {
        let success;
        http.get("guests")
          .then(res => {
            if (res.data) {
              const guests = res.data;
              commit("setGuestList", guests);
            }
            success = true;
          })
          .catch(err => {
            success = false;
          })
          .finally(() => resolve(success));
      })
    },
    fetchAllFamilies({ commit }) {
      return new Promise(async (resolve) => {
        http.get("families")
          .then(res => {
            if (res.data) {
              const families = res.data;
              commit("setFamilies", families);
            }
            resolve(true);
          })
          .catch(() => resolve(false));
      });
    },
    createNewFamily({ commit }, family) {
      let success, toast = new Toast();
      return new Promise(async (resolve) => {
        http.post("families", family)
          .then(res => {
            success = true;
            toast.setVariant("success");
            toast.setTitle("Success!");
            toast.setMessage("Family created successfully.");
            toast.show();
          })
          .catch(err => {
            success = false;
            toast.setVariant("danger");
            toast.setTitle("Error!");
            toast.setMessage("Unable to create family.");
            toast.show();
          })
          .finally(() => resolve(success));
      });
    }
  },
  modules: {
  }
})
