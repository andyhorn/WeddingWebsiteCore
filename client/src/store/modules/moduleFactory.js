import serviceFactory from "@/services/serviceFactory";

export default function (name, idKey, endpoint, namePlural = null) {
    if (namePlural == null) namePlural = name;
    
    const nameCapitalized = `${name[0].toUpperCase()}${name.slice(1)}`;
    // const nameCapitalized = name;
    // nameCapitalized[0] = nameCapitalized[0].toUpperCase();

    const namePluralCapitalized = `${namePlural[0].toUpperCase()}${namePlural.slice(1)}`;
    // const namePluralCapitalized = namePlural;
    // namePluralCapitalized[0] = namePluralCapitalized[0].toUpperCase();

    const service = serviceFactory(endpoint);

    const defaultState = {
        [namePlural]: []
    };

    const initialState = () => defaultState;

    const state = initialState();

    const MUTATIONS = {
        [`SET_${namePlural.toUpperCase()}`]: [`SET_${namePlural.toUpperCase()}`],
        [`RESET_${namePlural.toUpperCase()}`]: [`RESET_${namePlural.toUpperCase()}`],
        [`ADD_${name.toUpperCase()}`]: [`ADD_${name.toUpperCase()}`],
        [`REMOVE_${name.toUpperCase()}`]: [`REMOVE_${name.toUpperCase()}`]
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
                    commit(MUTATIONS[`SET_${namePlural.toUpperCase()}`], items);
                }

                resolve(items != null);
            });
        },

        [ACTIONS.FETCH]({ commit }, id) {
            return new Promise(async resolve => {
                const item = await service.getOne(id);
                if (item) {
                    commit(MUTATIONS[`ADD${name.toUpperCase()}`], item);
                }

                resolve(item);
            });
        },

        [ACTIONS.UPDATE]({ commit }, item) {
            return new Promise(async resolve => {
                const updated = await service.update(item[idKey], item);
                if (updated) {
                    commit(MUTATIONS[`ADD_${name.toUpperCase()}`], item);
                }

                resolve(updated);
            });
        },

        [ACTIONS.DELETE]({ commit }, id) {
            return new Promise(async resolve => {
                const deleted = await service.delete(id);
                if (deleted) {
                    commit(MUTATIONS[`REMOVE_${name.toUpperCase()}`], id);
                }

                resolve(deleted);
            });
        },

        [ACTIONS.CREATE]({ commit }, data) {
            return new Promise(async resolve => {
                const id = await service.create(data);
                if (id) {
                    const item = await service.getOne(id);
                    commit(MUTATIONS[`ADD_${name.toUpperCase()}`], item);
                }

                resolve(id);
            });
        }
    };

    const mutations = {
        [MUTATIONS[`SET_${namePlural.toUpperCase()}`]](state, items) {
            state[namePlural] = items;
        },

        [MUTATIONS[`RESET_${namePlural.toUpperCase()}`]](state) {
            state[namePlural] = [];
        },

        [MUTATIONS[`REMOVE_${name.toUpperCase()}`]](state, id) {
            const index = state[namePlural].findIndex(x => x[idKey] == id);
            if (index != -1) {
                state[namePlural].splice(index, 1);
            }
        },

        [MUTATIONS[`ADD_${name.toUpperCase()}`]](state, item) {
            const index = state[namePlural].findIndex(x => x[idKey] == item[idKey]);
            if (index == -1) {
                state[namePlural].push(item);
            } else {
                state[namePlural].splice(index, 1, item);
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