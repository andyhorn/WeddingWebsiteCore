import serviceFactory from "@/services/serviceFactory";
import Vue from "vue";

export default function (name, idKey, endpoint, namePlural = null) {
    if (namePlural == null) namePlural = name;
    
    const nameCapitalized = `${name[0].toUpperCase()}${name.slice(1)}`;
    const namePluralCapitalized = `${namePlural[0].toUpperCase()}${namePlural.slice(1)}`;

    const set = () => `SET_${namePlural.toUpperCase()}`;
    const reset = () => `RESET_${namePlural.toUpperCase()}`;
    const add = () => `ADD_${name.toUpperCase()}`;
    const remove = () => `REMOVE_${name.toUpperCase()}`;

    const service = serviceFactory(endpoint);

    const defaultState = {
        [namePlural]: []
    };

    const initialState = () => defaultState;

    const state = initialState();

    const MUTATIONS = {
        [set()]: set(),
        [reset()]: reset(),
        [add()]: add(),
        [remove()]: remove(),
    };

    const ACTIONS = {
        CREATE: `create${nameCapitalized}`,
        UPDATE: `update${nameCapitalized}`,
        FETCH: `fetch${nameCapitalized}`,
        FETCH_ALL: `fetchAll${namePluralCapitalized}`,
        DELETE: `delete${nameCapitalized}`
    };

    const getters = {
        [namePlural]: state => state[namePlural],
        [`find${nameCapitalized}`]: state => id => state[namePlural].find(x => x[idKey] == id)
    };

    const actions = {
        [ACTIONS.FETCH_ALL]({ commit }) {
            return new Promise(async resolve => {
                const items = await service.getAll();
                if (items) {
                    commit(MUTATIONS[set()], items);
                }

                resolve(items != null);
            });
        },

        [ACTIONS.FETCH]({ commit }, id) {
            return new Promise(async resolve => {
                const item = await service.getOne(id);
                if (item) {
                    commit(MUTATIONS[add()], item);
                }

                resolve(item);
            });
        },

        [ACTIONS.UPDATE]({ commit }, item) {
            return new Promise(async resolve => {
                const updated = await service.update(item[idKey], item);
                if (updated) {
                    // const refresh = await service.getOne(item[idKey]);
                    // commit(MUTATIONS[add()], refresh);
                }

                resolve(updated);
            });
        },

        [ACTIONS.DELETE]({ commit }, id) {
            return new Promise(async resolve => {
                const deleted = await service.delete(id);
                if (deleted) {
                    commit(MUTATIONS[remove()], id);
                }

                resolve(deleted);
            });
        },

        [ACTIONS.CREATE]({ commit }, data) {
            return new Promise(async resolve => {
                const id = await service.create(data);
                if (id) {
                    const item = await service.getOne(id);
                    commit(MUTATIONS[add()], item);
                }

                resolve(id);
            });
        }
    };

    const updateInPlace = (existing, update) => {
        for (var key of Object.keys(update)) 
            Vue.set(existing, key, update[key]);
    }

    const mutations = {
        [MUTATIONS[set()]](state, items) {
            state[namePlural] = items;
        },

        [MUTATIONS[reset()]](state) {
            state[namePlural] = [];
        },

        [MUTATIONS[remove()]](state, id) {
            const index = state[namePlural].findIndex(x => x[idKey] == id);
            if (index != -1) {
                state[namePlural].splice(index, 1);
            }
        },

        [MUTATIONS[add()]](state, item) {
            const index = state[namePlural].findIndex(x => x[idKey] == item[idKey]);
            if (index == -1) {
                state[namePlural].push(item);
            } else {
                // updateInPlace(state[namePlural], item);
                Vue.set(state[namePlural], index, item);
            }
        }
    };

    return {
        state,
        getters,
        actions,
        mutations,
        ACTIONS
    };
}