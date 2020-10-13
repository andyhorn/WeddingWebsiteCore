import rsvpService from "@/services/rsvpService";

const MUTATIONS = {
    SET_RSVPS: "SET_RSVPS",
    RESET_RSVPS: "RESET_RSVPS",
    ADD_RSVP: "ADD_RSVP",
    REMOVE_RSVP: "REMOVE_RSVP"
};

const ACTIONS = {
    CREATE: "createRsvp",
    UPDATE: "updateRsvp",
    FETCH: "fetchRsvp",
    FETCH_ALL: "fetchAllRsvps",
    DELETE: "deleteRsvp"
};

const defaultState = {
    rsvps: []
};

const initialState = () => defaultState;

const state = initialState();

const getters = {
    rsvps: state => state.rsvps,
    findRsvp: state => rsvpId => state.rsvps.find(x => x.rsvpId == rsvpId),
    findRsvpsForGuest: state => guestId => state.rsvps.filter(x => x.guestId == guestId)
};

const actions = {
    [ACTIONS.FETCH_ALL]({ commit }) {
        return new Promise(async resolve => {
            const rsvps = await rsvpService.getAll();

            if (rsvps) {
                commit(MUTATIONS.SET_RSVPS, rsvps);
            }

            resolve(rsvps != null);
        });
    },

    [ACTIONS.FETCH]({ commit }, rsvpId) {
        return new Promise(async resolve => {
            const rsvp = await rsvpService.getOne(rsvpId);

            if (rsvp) {
                commit(MUTATIONS.ADD_RSVP, rsvp);
            }

            resolve(rsvp);
        });
    },

    [ACTIONS.DELETE]({ commit }, rsvpId) {
        return new Promise(async resolve => {
            const deleted = await rsvpService.delete(rsvpId);

            if (deleted) {
                commit(MUTATIONS.REMOVE_RSVP, rsvpId);
            }

            resolve(deleted);
        });
    },

    [ACTIONS.UPDATE]({ commit }, payload) {
        return new Promise(async resolve => {
            const updated = await rsvpService.update(payload.rsvpId, payload);

            if (updated) {
                const rsvp = await rsvpService.getOne(payload.rsvpId);
                commit(MUTATIONS.ADD_RSVP, rsvp);
            }

            resolve(updated);
        });
    },

    [ACTIONS.CREATE]({ commit }, payload) {
        return new Promise(async resolve => {
            const rsvpId = await rsvpService.create(payload);

            if (rsvpId) {
                const rsvp = await rsvpService.getOne(rsvpId);
                commit(MUTATIONS.ADD_RSVP, rsvp);
            }

            resolve(rsvpId);
        });
    }
};

const mutations = {
    [MUTATIONS.SET_RSVPS](state, rsvps) {
        state.rsvps = rsvps;
    },

    [MUTATIONS.ADD_RSVP](state, rsvp) {
        const rsvps = state.rsvps;
        const index = state.rsvps.findIndex(x => x.rsvpId == rsvp.rsvpId);

        if (index == -1) {
            rsvps.push(rsvp);
        } else {
            rsvps.splice(index, 1, rsvp);
        }

        state.rsvps = rsvps;
    },

    [MUTATIONS.REMOVE_RSVP](state, rsvpId) {
        const rsvps = state.rsvps;
        const index = rsvps.findIndex(x => x.rsvpId == rsvpId);

        if (index != -1) {
            rsvps.splice(index, 1);
            state.rsvps = rsvps;
        }
    },

    [MUTATIONS.RESET_RSVPS](state) {
        state.rsvps = [];
    }
};

export default {
    ACTIONS,
    state,
    getters,
    actions,
    mutations
}