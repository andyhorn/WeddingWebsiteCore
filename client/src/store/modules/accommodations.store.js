import moduleFactory from "@/store/modules/moduleFactory";

const name = "accommodation";
const idKey = "accommodationId";
const endpoint = "accommodations";
const namePlural = "accommodations";

export default moduleFactory(name, idKey, endpoint, namePlural);


// import accommodationService from "@/services/accommodationService.js";

// const ACCOMMODATION_MUTATIONS = {
//     SET_ACCOMMODATIONS: "SET_ACCOMMODATIONS",
//     RESET_ACCOMMODATIONS: "RESET_ACCOMMODATIONS",
//     ADD_ACCOMMODATION: "ADD_ACCOMMODATION",
//     REMOVE_ACCOMMODATION: "REMOVE_ACCOMMODATION"
// };

// const ACCOMMODATION_ACTIONS = {
//     FETCH_ALL: "fetchAllAccommodations",
//     FETCH: "fetchAccommodation",
//     DELETE: "deleteAccommodation",
//     UPDATE: "updateAccommodation",
//     CREATE: "createAccommodation"
// };

// const defaultState = {
//     accommodations: []
// };

// function initialState() {
//     return defaultState;
// }

// const state = initialState();

// const getters = {
//     accommodations: state => state.accommodations,
//     findAccommodation: state => id => state.accommodations.find(x => x.accommodationId == id)
// };

// const actions = {
//     [ACCOMMODATION_ACTIONS.FETCH_ALL]({ commit }) {
//         return new Promise(async (resolve) => {
//             const accommodations = await accommodationService.getAll();
//             if (accommodations) {
//                 commit(ACCOMMODATION_MUTATIONS.SET_ACCOMMODATIONS, accommodations);
//                 return resolve(true);
//             } else {
//                 return resolve(false);
//             }
//         });
//     },

//     [ACCOMMODATION_ACTIONS.FETCH]({ commit }, accommodationId) {
//         return new Promise(async (resolve) => {
//             const accommodation = await accommodationService.getOne(accommodationId);
//             if (accommodation) {
//                 commit(ACCOMMODATION_MUTATIONS.ADD_ACCOMMODATION, accommodation);
//                 return resolve(true);
//             } else {
//                 return resolve(false);
//             }
//         });
//     },

//     [ACCOMMODATION_ACTIONS.UPDATE]({ commit }, accommodation) {
//         return new Promise(async (resolve) => {
//             const updated = await accommodationService.update(accommodation.accommodationId, accommodation);
//             if (updated) {
//                 // accommodation = await accommodationService.getOne(accommodation.accommodationId);
//                 commit(ACCOMMODATION_MUTATIONS.ADD_ACCOMMODATION, accommodation);
//             }

//             resolve(updated);
//         });
//     },

//     [ACCOMMODATION_ACTIONS.DELETE]({ commit }, accommodationId) {
//         return new Promise(async (resolve) => {
//             const deleted = await accommodationService.delete(accommodationId);
//             if (deleted) {
//                 commit(ACCOMMODATION_MUTATIONS.REMOVE_ACCOMMODATION, accommodationId);
//             }

//             resolve(deleted);
//         });
//     },

//     [ACCOMMODATION_ACTIONS.CREATE]({ commit }, accommodation) {
//         return new Promise(async (resolve) => {
//             const accommodationId = await accommodationService.create(accommodation);
//             if (accommodationId) {
//                 const accommodation = await accommodationService.getOne(accommodationId);
//                 commit(ACCOMMODATION_MUTATIONS.ADD_ACCOMMODATION, accommodation);
//             }

//             resolve(accommodationId);
//         });
//     }
// };

// const mutations = {
//     [ACCOMMODATION_MUTATIONS.SET_ACCOMMODATIONS](state, accommodations) {
//         state.accommodations = accommodations;
//     },

//     [ACCOMMODATION_MUTATIONS.ADD_ACCOMMODATION](state, accommodation) {
//         const index = state.accommodations.findIndex(x => x.accommodationId == accommodation.accommodationId);
//         if (index == -1) {
//             state.accommodations.push(accommodation);
//         } else {
//             state.accommodations.splice(index, 1, accommodation);
//         }
//     },

//     [ACCOMMODATION_MUTATIONS.REMOVE_ACCOMMODATION](state, accommodationId) {
//         const index = state.accommodations.findIndex(x => x.accommodationId == accommodationId);
//         if (index != -1) {
//             state.accommodations.splice(index, 1);
//         }
//     },

//     [ACCOMMODATION_MUTATIONS.RESET_ACCOMMODATIONS](state) {
//         state.accommodations = [];
//     }
// }

// export default {
//     state,
//     getters,
//     actions,
//     mutations,
//     ACCOMMODATION_ACTIONS
// }
