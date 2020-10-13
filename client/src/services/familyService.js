import { http } from "@/axios";

const endpoint = "families";

export default {
    getAll: function () {
        return new Promise(async (resolve) => {
            http.get(endpoint)
                .then(res => {
                    const families = res.data;
                    resolve(families);
                })
                .catch(err => {
                    resolve(null);
                });
        });
    },

    getOne: function (id) {
        return new Promise(async (resolve) => {
            http.get(endpoint + "/" + id)
                .then(res => {
                    const family = res.data;
                    resolve(family);
                })
                .catch(err => {
                    resolve(null);
                });
        });
    },

    create: function (family) {
        return new Promise(async (resolve) => {
            http.post(endpoint, family)
                .then(res => {
                    const familyId = res.data;
                    resolve(familyId);
                })
                .catch(err => {
                    resolve(false);
                });
        });
    },

    update: function (id, family) {
        return new Promise(async (resolve) => {
            http.put(endpoint + "/" + id, family)
                .then(res => {
                    resolve(true);
                })
                .catch(err => {
                    resolve(false);
                });
        });
    },

    delete: function (id) {
        return new Promise(async (resolve) => {
            http.delete(endpoint + "/" + id)
                .then(res => {
                    resolve(true);
                })
                .catch(err => {
                    resolve(false);
                });
        });
    }
}