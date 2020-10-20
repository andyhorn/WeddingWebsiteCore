import registryService from "@/services/registryService";

const MUTATIONS = {
    SET_REGISTRIES: "SET_REGISTRIES",
    RESET_REGISTRIES: "RESET_REGISTRIES",
    ADD_REGISTRY: "ADD_REGISTRY",
    REMOVE_REGISTRY: "REMOVE_REGISTRY"
};

const ACTIONS = {
    CREATE: "createRegistry",
    UPDATE: "updateRegistry",
    FETCH: "fetchRegistry",
    FETCH_ALL: "fetchAllRegistries",
    DELETE: "deleteRegistry"
};

const defaultState = {
    registries: []
};

const initialState = () => defaultState;
const state = initialState();

const getters = {
    registries: state => state.registries,
    findRegistry: state => id => state.registries.find(x => x.registryId == id)
};

const actions = {
    [ACTIONS.FETCH_ALL]({ commit }) {
        return new Promise(async (resolve) => {
            const registries = await registryService.getAll();
            if (registries) {
                commit(MUTATIONS.SET_REGISTRIES, registries);
            }

            resolve(registries != null);
        });
    },

    [ACTIONS.FETCH]({ commit }, registryId) {
        return new Promise(async resolve => {
            const registry = await registryService.getOne(registryId);
            if (registry) {
                commit(MUTATIONS.ADD_REGISTRY, registry);
            }

            resolve(registry);
        });
    },

    [ACTIONS.UPDATE]({ commit }, registry) {
        return new Promise(async resolve => {
            const updated = await registryService.update(registry.registryId, registry);
            if (updated) {
                commit(MUTATIONS.ADD_REGISTRY, registry);
            }

            resolve(updated);
        });
    },

    [ACTIONS.DELETE]({ commit }, registryId) {
        return new Promise(async resolve => {
            const deleted = await registryService.delete(registryId);
            if (deleted) {
                commit(MUTATIONS.REMOVE_REGISTRY, registryId);
            }

            resolve(deleted);
        });
    },

    [ACTIONS.CREATE]({ commit }, registryData) {
        return new Promise(async resolve => {
            const registryId = await registryService.create(registryData);
            if (registryId) {
                const registry = await registryService.getOne(registryId);
                commit(MUTATIONS.ADD_REGISTRY, registry);
            }
    
            resolve(registryId);
        })
    }
};

const mutations = {
    [MUTATIONS.SET_REGISTRIES](state, registries) {
        state.registries = registries;
    },

    [MUTATIONS.RESET_REGISTRIES](state) {
        state.registries = [];
    },

    [MUTATIONS.REMOVE_REGISTRY](state, registryId) {
        const index = state.registries.findIndex(x => x.registryId == registryId);
        if (index != -1) {
            state.registries.splice(index, 1);
        }
    },

    [MUTATIONS.ADD_REGISTRY](state, registry) {
        const index = state.registries.findIndex(x => x.registryId == registry.registryId);
        if (index == -1) {
            state.registries.push(registry);
        } else {
            state.registries.splice(index, 1, registry);
        }
    }
};

export default {
    state,
    getters,
    actions,
    mutations,
    ACTIONS
}