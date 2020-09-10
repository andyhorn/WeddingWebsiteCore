import { http } from "@/axios";

const endpoint = "addresses";

export default {
    getAll: function () {
        return new Promise(resolve => {
            http.get(endpoint)
                .then(res => {
                    const addresses = res.data;
                    resolve(addresses);
                })
                .catch(err => resolve(null));
        });
    },

    getOne: function (id) {
        return new Promise(resolve => {
            http.get(endpoint + "/" + id)
                .then(res => {
                    const address = res.data;
                    resolve(address);
                })
                .catch(err => resolve(null));
        });
    },

    create: function (address) {
        return new Promise(resolve => {
            http.post(endpoint, address)
                .then(res => {
                    const addressId = res.data;
                    resolve(addressId);
                })
                .catch(err => resolve(null));
        });
    },

    update: function (addressId, address) {
        return new Promise(resolve => {
            http.put(endpoint + "/" + addressId, address)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    },

    delete: function (addressId) {
        return new Promise(resolve => {
            http.delete(endpoint + "/" + addressId)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    }
}