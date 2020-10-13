import { http } from "@/axios";

const endpoint = "guests";

export default {
    create: function (guest) {
        return new Promise(async (resolve) => {
            http.post(endpoint, guest)
                .then(res => {
                    const newUserId = res.data;
                    resolve(newUserId);
                })
                .catch(err => {
                    resolve(false);
                });
        });
    },

    update: function (id, guest) {
        return new Promise(async (resolve) => {
            http.put(endpoint + "/" + id, guest)
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
    },

    getOne: function (id) {
        return new Promise(async (resolve) => {
            http.get(endpoint + "/" + id)
                .then(res => {
                    const guestData = res.data;
                    resolve(guestData);
                })
                .catch(err => {
                    resolve(null);
                });
        });
    },

    getAll: function () {
        return new Promise(async (resolve) => {
            http.get(endpoint)
                .then(res => {
                    const guestData = res.data;
                    resolve(guestData);
                })
                .catch(err => {
                    resolve(null);
                });
        });
    }
}