import guestService from "@/services/guestService.js";

const GUEST_MUTATIONS = {
    SET_GUESTS: "SET_GUESTS",
    RESET_GUESTS: "RESET_GUESTS",
    ADD_GUEST: "ADD_GUEST",
    REMOVE_GUEST: "REMOVE_GUEST"
}

const GUEST_ACTIONS = {
    FETCH_ALL: "fetchAllGuests",
    FETCH: "fetchGuest",
    DELETE: "deleteGuest",
    UPDATE: "updateGuest",
    CREATE: "createGuest"
}

const defaultState = {
    guests: []
};

function initialState() {
    return defaultState;
}

const state = initialState();

const getters = {
    guests: (state) => {
        return state.guests
    },
    findGuest: (state) => (id) => {
        return state.guests.find(g => g.guestId == id);
    },
    children: (state) => {
        return state.guests.filter(g => g.isChild);
    },
    nonChildren: (state) => {
        return state.guests.filter(g => !g.isChild);
    },
    guestsWithoutFamilies: (state) => {
        return state.guests.filter(g => !g.familyId);
    },
    guestsInFamily: (state) => (id) => {
        return state.guests.filter(g => g.familyId == id);
    }
}

const actions = {
    [GUEST_ACTIONS.FETCH_ALL]({ commit }) {
        return new Promise(async (resolve) => {
            const guests = await guestService.getAll();
            if (guests) {
                commit(GUEST_MUTATIONS.SET_GUESTS, guests);
                return resolve(true);
            } else {
                return resolve(false);
            }
        });
    },
    [GUEST_ACTIONS.FETCH]({ commit }, guestId) {
        return new Promise(async (resolve) => {
            const guest = await guestService.getOne(guestId);

            if (guest) {
                commit(GUEST_MUTATIONS.ADD_GUEST, guest);
                resolve(true);
            } else {
                resolve(false);
            }
        });
    },
    [GUEST_ACTIONS.DELETE]({ commit }, guestId) {
        return new Promise(async (resolve) => {
            const deleted = await guestService.delete(guestId);
            if (deleted) {
                commit(GUEST_MUTATIONS.REMOVE_GUEST, guestId);
            }

            resolve(deleted);
        });
    },
    [GUEST_ACTIONS.UPDATE]({ commit }, guest) {
        return new Promise(async (resolve) => {
            const updated = await guestService.update(guest.guestId, guest);
            if (updated) {
                // guest = await guestService.getOne(guest.guestId);
                commit(GUEST_MUTATIONS.ADD_GUEST, guest);
            }

            resolve(updated);
        });
    },
    [GUEST_ACTIONS.CREATE]({ commit }, guest) {
        return new Promise(async (resolve) => {
            const guestId = await guestService.create(guest);
            if (guestId) {
                const guest = await guestService.getOne(guestId);
                commit(GUEST_MUTATIONS.ADD_GUEST, guest);
            }

            resolve(guestId);
        });
    }
}

const mutations = {
    [GUEST_MUTATIONS.SET_GUESTS](state, data) {
        state.guests = data;
    },
    [GUEST_MUTATIONS.ADD_GUEST](state, guest) {
        const index = state.guests.findIndex(g => g.guestId == guest.guestId);
        if (index == -1) {
            state.guests.push(guest);
        } else {
            state.guests.splice(index, 1, guest);
        }
    },
    [GUEST_MUTATIONS.REMOVE_GUEST](state, id) {
        const index = state.guests.findIndex(g => g.guestId == id);
        state.guests.splice(index, 1);
    },
    [GUEST_MUTATIONS.RESET_GUESTS](state) {
        state.guests = [];
    }
}

export default {
    state,
    getters,
    actions,
    mutations,
    GUEST_ACTIONS
};