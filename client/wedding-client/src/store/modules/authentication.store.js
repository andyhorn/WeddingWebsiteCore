import authenticationService from "@/services/authenticationService.js";
import getUserData from "@/helpers/getUserData";

const LOCAL_STORAGE_TOKEN_KEY = "x-ak-ww-token-x";

const AUTHENTICATION_MUTATIONS = {
    AUTHENTICATION_ATTEMPT: "AUTHENTICATION_ATTEMPT",
    AUTHENTICATION_SUCCESS: "AUTHENTICATION_SUCCESS",
    AUTHENTICATION_FAILURE: "AUTHENTICATION_FAILURE",
    CHECKING_TOKEN: "CHECKING_TOKEN",
    TOKEN_FOUND: "TOKEN_FOUND",
    TOKEN_NOT_FOUND: "TOKEN_NOT_FOUND",
    REFRESH: "REFRESH",
    SET_USER_DATA: "SET_USER_DATA",
    SET_TOKEN: "SET_TOKEN",
    LOGOUT: "LOGOUT"
}

const AUTHENTICATION_ACTIONS = {
    LOGIN: "login",
    LOGOUT: "logout",
    CHECK_TOKEN: "checkToken",
    REFRESH: "refreshAuthentication"
}

const defaultState = {
    authenticationStatus: "",
    token: "",
    userId: 0,
    firstName: "",
    lastName: "",
    email: ""
}

const initialState = () => Object.assign({}, defaultState);

const state = initialState();

const getters = {
    fullName: state => `${state.firstName} ${state.lastName}`,
    isLoggedIn: state => state.token != null && state.userId != 0
}

const actions = {
    [AUTHENTICATION_ACTIONS.LOGIN]: async ({ commit }, { email, password, rememberMe = false }) => {
        return new Promise(async (resolve) => {
            commit(AUTHENTICATION_MUTATIONS.AUTHENTICATION_ATTEMPT);

            const data = await authenticationService.authenticate(email, password);
            if (data) {
                commit(AUTHENTICATION_MUTATIONS.AUTHENTICATION_SUCCESS);

                const { userData, token } = getUserData(data);

                commit(AUTHENTICATION_MUTATIONS.SET_USER_DATA, userData);
                commit(AUTHENTICATION_MUTATIONS.SET_TOKEN, token);

                if (rememberMe) {
                    localStorage.setItem(LOCAL_STORAGE_TOKEN_KEY, token);
                }

                return resolve(true);
            }

            commit(AUTHENTICATION_MUTATIONS.AUTHENTICATION_FAILURE);

            return resolve(false);
        });
    },
    [AUTHENTICATION_ACTIONS.LOGOUT]: async ({ commit }) => {
        return new Promise(resolve => {
            commit(AUTHENTICATION_MUTATIONS.LOGOUT);
            localStorage.removeItem(LOCAL_STORAGE_TOKEN_KEY);
            resolve();
        });
    },
    [AUTHENTICATION_ACTIONS.REFRESH]: async ({ commit }) => (token) => {
        return new Promise(async (resolve) => {
            commit(AUTHENTICATION_MUTATIONS.REFRESH);

            commit(AUTHENTICATION_MUTATIONS.AUTHENTICATION_ATTEMPT);
            const data = await authenticationService.refresh(token);

            if (data) {
                // the refresh was a success
                commit(AUTHENTICATION_MUTATIONS.AUTHENTICATION_SUCCESS);

                // get the authentication data
                const { userData, token } = getUserData(data);

                // save the data to the store
                commit(AUTHENTICATION_MUTATIONS.SET_USER_DATA, userData);
                commit(AUTHENTICATION_MUTATIONS.SET_TOKEN, token);
            } else {
                // the refresh failed
                commit(AUTHENTICATION_MUTATIONS.AUTHENTICATION_FAILURE);
            }

            resolve(data != null);
        });
    },
    [AUTHENTICATION_ACTIONS.CHECK_TOKEN]: async ({ commit, dispatch, state }) => {
        return new Promise(async (resolve) => {
            commit(AUTHENTICATION_MUTATIONS.CHECKING_TOKEN);

            const token = localStorage.getItem(LOCAL_STORAGE_TOKEN_KEY);

            if (token) {
                // a token was found, dispatch the refresh action
                commit(AUTHENTICATION_MUTATIONS.TOKEN_FOUND);
                const success = await dispatch(AUTHENTICATION_ACTIONS.REFRESH)(token);

                if (success) {
                    // the refresh was a success, refresh the token in the cache
                    localStorage.setItem(LOCAL_STORAGE_TOKEN_KEY, state.token);
                } else {
                    // the refresh failed, perform a logout and clear the token
                    // from the local cache
                    commit(AUTHENTICATION_MUTATIONS.LOGOUT);
                    localStorage.removeItem(LOCAL_STORAGE_TOKEN_KEY);
                }
            } else {
                commit(AUTHENTICATION_MUTATIONS.TOKEN_NOT_FOUND);
            }

            resolve();
        });
    }
}

const mutations = {
    [AUTHENTICATION_MUTATIONS.AUTHENTICATION_ATTEMPT]: (state) => {
        state.authenticationStatus = "Attempting authentication";
    },
    [AUTHENTICATION_MUTATIONS.AUTHENTICATION_FAILURE]: (state) => {
        state.authenticationStatus = "Authentication failed";
    },
    [AUTHENTICATION_MUTATIONS.AUTHENTICATION_SUCCESS]: (state) => {
        state.authenticationStatus = "Authentication succeeded";
    },
    [AUTHENTICATION_MUTATIONS.CHECKING_TOKEN]: (state) => {
        state.authenticationStatus = "Checking for cached token";
    },
    [AUTHENTICATION_MUTATIONS.LOGOUT]: (state) => {
        const init = initialState();
        Object.keys(init).forEach(key => state[key] = init[key]);
        state.authenticationStatus = "Logged out";
    },
    [AUTHENTICATION_MUTATIONS.REFRESH]: (state) => {
        state.authenticationStatus = "Refreshing authentication token";
    },
    [AUTHENTICATION_MUTATIONS.SET_TOKEN]: (state, token) => {
        state.token = token;
        authenticationService.setToken(token);
        state.authenticationStatus = "Token set";
    },
    [AUTHENTICATION_MUTATIONS.SET_USER_DATA]: (state, userData) => {
        state.userId = userData.userId;
        state.firstName = userData.firstName;
        state.lastName = userData.lastName;
        state.email = userData.email;
        state.authenticationStatus = "User data saved";
    },
    [AUTHENTICATION_MUTATIONS.TOKEN_FOUND]: (state) => {
        state.authenticationStatus = "Cached token found";
    },
    [AUTHENTICATION_MUTATIONS.TOKEN_NOT_FOUND]: (state) => {
        state.authenticationStatus = "Cached token not found";
    }
}

export default {
    state,
    getters,
    actions,
    mutations,
    AUTHENTICATION_ACTIONS
}