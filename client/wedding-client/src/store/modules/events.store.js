import eventService from "@/services/eventService";

const EVENT_MUTATIONS = {
    SET_EVENTS: "SET_EVENTS",
    RESET_EVENTS: "RESET_EVENTS",
    ADD_EVENT: "ADD_EVENT",
    REMOVE_EVENT: "REMOVE_EVENT"
}

const EVENT_ACTIONS = {
    CREATE: "createEvent",
    UPDATE: "updateEvent",
    FETCH: "fetchEvent",
    FETCH_ALL: "fetchAllEvents",
    DELETE: "deleteEvent"
}

const defaultState = {
    events: []
}

function initialState() {
    return defaultState;
}

const state = initialState();

const getters = {
    events: state => state.events,
    findEvent: state => eventId => state.events.find(e => e.eventId == eventId),
}

const actions = {
    [EVENT_ACTIONS.FETCH_ALL]({ commit }) {
        return new Promise(async resolve => {
            const events = await eventService.getAll();
            if (events) {
                commit(EVENT_MUTATIONS.SET_EVENTS, events);
            }

            resolve(events != null);
        });
    },

    [EVENT_ACTIONS.FETCH]({ commit }, eventId) {
        return new Promise(async resolve => {
            const event = await eventService.getOne(eventId);
            if (event) {
                commit(EVENT_MUTATIONS.ADD_EVENT, event);
            }

            resolve(event);
        });
    },

    [EVENT_ACTIONS.DELETE]({ commit }, eventId) {
        return new Promise(async resolve => {
            const deleted = await eventService.delete(eventId);
            if (deleted) {
                commit(EVENT_MUTATIONS.REMOVE_EVENT, eventId);
            }

            resolve(deleted);
        });
    },

    [EVENT_ACTIONS.UPDATE]({ commit }, event) {
        return new Promise(async resolve => {
            const updated = await eventService.update(event.eventId, event);
            if (updated) {
                event = await eventService.getOne(event.eventId);
                commit(EVENT_MUTATIONS.ADD_EVENT, event);
            }

            resolve(updated);
        });
    },

    [EVENT_ACTIONS.CREATE]({ commit }, data) {
        return new Promise(async resolve => {
            const eventId = await eventService.create(data);
            if (eventId) {
                const event = await eventService.getOne(eventId);
                commit(EVENT_MUTATIONS.ADD_EVENT, event);
            }

            resolve(eventId);
        });
    }
}

const mutations = {
    [EVENT_MUTATIONS.SET_EVENTS](state, events) {
        state.events = events;
    },

    [EVENT_MUTATIONS.RESET_EVENTS](state) {
        state.events = defaultState;
    },

    [EVENT_MUTATIONS.REMOVE_EVENT](state, eventId) {
        const index = state.events.findIndex(e => e.eventId == eventId);
        if (index != -1) {
            state.events.splice(index, 1);
        }
    },

    [EVENT_MUTATIONS.ADD_EVENT](state, event) {
        const index = state.events.findIndex(e => e.eventId == event.eventId);
        if (index == -1) {
            state.events.push(event);
        } else {
            state.events.splice(index, 1, event);
        }
    }
}

export default {
    state,
    getters,
    actions,
    mutations,
    EVENT_ACTIONS
}