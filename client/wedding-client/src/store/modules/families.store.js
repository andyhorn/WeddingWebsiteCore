import familyService from "@/services/familyService.js";

const FAMILY_MUTATIONS = {
    SET_FAMILIES: "SET_FAMILIES",
    RESET_FAMILIES: "RESET_FAMILIES",
    ADD_FAMILY: "ADD_FAMILY",
    REMOVE_FAMILY: "REMOVE_FAMILY"
}

const FAMILY_ACTIONS = {
    CREATE: "createFamily",
    UPDATE: "updateFamily",
    FETCH: "fetchFamily",
    FETCH_ALL: "fetchAllFamilies",
    DELETE: "deleteFamily"
}

const defaultState = {
    families: []
}

function initialState() {
    return defaultState;
}

const state = initialState();

const getters = {
    families: (state) => {
        return state.families;
    },
    family: (state) => (familyId) => {
        return state.families.find(f => f.familyId == familyId);
    },
    familiesWithoutMembers: (state) => {
        return state.families.filter(f => !(f.members.length));
    }
}

const actions = {
    [FAMILY_ACTIONS.FETCH_ALL]({ commit }) {
        return new Promise(async (resolve) => {
            const families = await familyService.getAll();
            if (families) {
                commit(FAMILY_MUTATIONS.SET_FAMILIES, families);
            }

            resolve(families != null);
        });
    },
    [FAMILY_ACTIONS.FETCH]({ commit }, familyId) {
        return new Promise(async (resolve) => {
            const family = await familyService.getOne(familyId);
            if (family) {
                commit(FAMILY_MUTATIONS.ADD_FAMILY, family);
            }

            resolve(family);
        });
    },
    [FAMILY_ACTIONS.DELETE]({ commit }, familyId) {
        return new Promise(async (resolve) => {
            const deleted = await familyService.delete(familyId);
            if (deleted) {
                commit(FAMILY_MUTATIONS.REMOVE_FAMILY, familyId);
            }

            resolve(deleted);
        });
    },
    [FAMILY_ACTIONS.UPDATE]({ commit }, family) {
        return new Promise(async (resolve) => {
            const updated = await familyService.update(family.familyId, family);
            if (updated) {
                family = await familyService.getOne(family.familyId);
                commit(FAMILY_MUTATIONS.ADD_FAMILY, family);
            }

            resolve(updated);
        });
    },
    [FAMILY_ACTIONS.CREATE]({ commit }, data) {
        return new Promise(async (resolve) => {
            const familyId = await familyService.create(data);
            if (familyId) {
                const family = await familyService.getOne(familyId);
                commit(FAMILY_MUTATIONS.ADD_FAMILY, family);
                resolve(familyId);
            } else {
                resolve(null);
            }
        });
    }
}

const mutations = {
    [FAMILY_MUTATIONS.SET_FAMILIES](state, data) {
        state.families = data;
    },
    [FAMILY_MUTATIONS.RESET_FAMILIES](state) {
        state.families = defaultState;
    },
    [FAMILY_MUTATIONS.REMOVE_FAMILY](state, familyId) {
        const index = state.families.findIndex(f => f.familyId == familyId);
        if (index != -1) {
            state.families.splice(index, 1);
        }
    },
    [FAMILY_MUTATIONS.ADD_FAMILY](state, data) {
        const index = state.families.findIndex(f => f.familyId == data.familyId);
        if (index == -1) {
            state.families.push(data);
        } else {
            state.families.splice(index, 1, data);
        }
    }
}

export default {
    state,
    getters,
    actions,
    mutations,
    FAMILY_ACTIONS
}