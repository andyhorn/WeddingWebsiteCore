import Vue from 'vue'
import Vuex from 'vuex'
import { http, addToken, stripToken } from "@/axios";

Vue.use(Vuex);

export const TOKEN_IDENTIFIER = "x-kaw-auth-token-x";

const defaultState = {
  error: "",
  token: "",
  user: {
    id: "",
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
      state.user.id = userId;
      state.status = "Saved user's ID.";
    },
    setToken(state, token) {
      state.token = token;
      state.status = "Saved JWT.";
    },
    resetToken(state) {
      state.token = "";
      state.status = "Reset JWT.";
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
    }
  },
  actions: {
    login({ commit }, payload) {
      return new Promise(async (resolve, reject) => {
        const email = payload.email;
        const password = payload.password;

        if (!email) {
          return reject("Email is required.");
        }

        if (!password) {
          return reject("Password is required.");
        }

        commit("authenticationAttempt");

        http.post("users/login", {
          email,
          password
        })
          .then(res => {
            console.log(res);
            const data = res.data;
            const id = data.id;
            const firstName = data.firstName;
            const lastName = data.lastName;
            const token = data.token;

            const userData = {
              id,
              firstName,
              lastName,
              email
            };

            commit("setUserData", userData);
            commit("setToken", token);

            commit("authenticationSuccess");

            return resolve();
          })
          .catch(err => {
            commit("authenticationFailure", err);
            return reject(err);
          });
      })
    },
    logout({ commit }) {
      commit("logout");
    }
  },
  modules: {
  }
})
