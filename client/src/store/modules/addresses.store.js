import addressService from "@/services/addressService";

const MUTATIONS = {
    SET_ADDRESSES: "SET_ADDRESSES",
    RESET_ADDRESSES: "RESET_ADDRESSES",
    ADD_ADDRESS: "ADD_ADDRESS",
    REMOVE_ADDRESS: "REMOVE_ADDRESS"
};

const ACTIONS = {
    CREATE: "createAddress",
    UPDATE: "updateAddress",
    FETCH: "fetchAddress",
    FETCH_ALL: "fetchAllAddresses",
    DELETE: "deleteAddress"
};

const defaultState = {
    addresses: []
};

const initialState = () => defaultState;

const state = initialState();

const getters = {
    addresses: state => state.addresses,
    findAddress: state => id => state.addresses.find(x => x.addressId == id)
};

const actions = {
    [ACTIONS.FETCH_ALL]({ commit }) {
        return new Promise(async resolve => {
            const addresses = await addressService.getAll();
            if (addresses) {
                commit(MUTATIONS.SET_ADDRESSES, addresses);
            }

            resolve(addresses != null);
        });
    },

    [ACTIONS.FETCH]({ commit }, addressId) {
        return new Promise(async resolve => {
            const address = await addressService.getOne(addressId);
            if (address) {
                commit(MUTATIONS.ADD_ADDRESS, address);
            }

            resolve(address);
        });
    },

    [ACTIONS.UPDATE]({ commit }, address) {
        return new Promise(async resolve => {
            const updated = await addressService.update(address.addressId, address);
            if (updated) {
                const refreshedAddress = await addressService.getOne(address.addressId);
                commit(MUTATIONS.ADD_ADDRESS, refreshedAddress);
            }

            resolve(updated);
        });
    },

    [ACTIONS.DELETE]({ commit }, addressId) {
        return new Promise(async resolve => {
            const deleted = await addressService.delete(addressId);
            if (deleted) {
                commit(MUTATIONS.REMOVE_ADDRESS, addressId);
            }

            resolve(deleted);
        });
    },

    [ACTIONS.CREATE]({ commit }, addressData) {
        return new Promise(async resolve => {
            const addressId = await addressService.create(addressData);
            if (addressId) {
                const address = await addressService.getOne(addressId);
                commit(MUTATIONS.ADD_ADDRESS, address);
            }

            resolve(addressId);
        });
    }
};

const mutations = {
    [MUTATIONS.SET_ADDRESSES](state, addresses) {
        state.addresses = addresses;
    },

    [MUTATIONS.RESET_ADDRESSES](state) {
        state.addresses = [];
    },

    [MUTATIONS.REMOVE_ADDRESS](state, addressId) {
        const index = state.addresses.findIndex(x => x.addressId == addressId);
        if (index != -1) {
            state.addresses.splice(index, 1);
        }
    },

    [MUTATIONS.ADD_ADDRESS](state, address) {
        const index = state.addresses.findIndex(x => x.addressId == address.addressId);
        if (index == -1) {
            state.addresses.push(address);
        } else {
            state.addresses.splice(index, 1, address);
        }
    }
}

export default {
    state,
    getters,
    actions,
    mutations,
    ACTIONS
};