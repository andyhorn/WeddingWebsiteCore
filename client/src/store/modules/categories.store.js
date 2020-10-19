import categoryService from "@/services/categoryService.js";

const CATEGORY_MUTATIONS = {
    SET_CATEGORIES: "SET_CATEGORIES",
    RESET_CATEGORIES: "RESET_CATEGORIES",
    ADD_CATEGORY: "ADD_CATEGORY",
    REMOVE_CATEGORY: "REMOVE_CATEGORY"
};

const CATEGORY_ACTIONS = {
    FETCH_ALL: "fetchAllCategories",
    FETCH: "fetchCategory",
    DELETE: "deleteCategory",
    UPDATE: "updateCategory",
    CREATE: "createCategory"
};

const defaultState = {
    categories: []
};

function initialState() {
    return defaultState;
}

const state = initialState();

const getters = {
    categories: state => state.categories,
    findCategory: state => categoryId => state.categories.find(x => x.categoryId == categoryId)
};

const actions = {
    [CATEGORY_ACTIONS.FETCH_ALL]({ commit }) {
        return new Promise(async (resolve) => {
            const categories = await categoryService.getAll();
            if (categories) {
                commit(CATEGORY_MUTATIONS.SET_CATEGORIES, categories);
                resolve(true);
            } else {
                resolve(false);
            }
        });
    },

    [CATEGORY_ACTIONS.FETCH]({ commit }, categoryId) {
        return new Promise(async (resolve) => {
            const category = await categoryService.getOne(categoryId);
            if (category) {
                commit(CATEGORY_MUTATIONS.ADD_CATEGORY, category);
                resolve(true);
            } else {
                resolve(false);
            }
        });
    },

    [CATEGORY_ACTIONS.DELETE]({ commit, dispatch }, categoryId) {
        return new Promise(async (resolve) => {
            const deleted = await categoryService.delete(categoryId);
            if (deleted) {
                commit(CATEGORY_MUTATIONS.REMOVE_CATEGORY, categoryId);
                dispatch(CATEGORY_ACTIONS.FETCH_ALL);
            }

            resolve(deleted);
        });
    },

    [CATEGORY_ACTIONS.UPDATE]({ commit }, category) {
        return new Promise(async (resolve) => {
            const updated = categoryService.update(category.categoryId, category);
            if (updated) {
                // category = await categoryService.getOne(category.categoryId);
                commit(CATEGORY_MUTATIONS.ADD_CATEGORY, category);
            }

            resolve(updated);
        });
    },

    [CATEGORY_ACTIONS.CREATE]({ commit }, category) {
        return new Promise(async (resolve) => {
            const categoryId = await categoryService.create(category);
            if (categoryId) {
                const category = await categoryService.getOne(categoryId);
                commit(CATEGORY_MUTATIONS.ADD_CATEGORY, category);
            }

            resolve(categoryId);
        })
    }
};

const mutations = {
    [CATEGORY_MUTATIONS.ADD_CATEGORY](state, category) {
        const index = state.categories.findIndex(x => x.categoryId == category.categoryId);
        if (index == -1) {
            state.categories.push(category);
        } else {
            state.categories.splice(index, 1, category);
        }
    },

    [CATEGORY_MUTATIONS.REMOVE_CATEGORY](state, id) {
        const index = state.categories.findIndex(x => x.categoryId == id);
        if (index != -1) {
            state.categories.splice(index, 1);
        }
    },

    [CATEGORY_MUTATIONS.RESET_CATEGORIES](state) {
        state.categories = [];
    },

    [CATEGORY_MUTATIONS.SET_CATEGORIES](state, categories) {
        state.categories = categories;
    }
};

export default {
    state,
    getters,
    actions,
    mutations,
    CATEGORY_ACTIONS
};
