import { http } from "@/axios";

export default function (endpoint) {
    return {
        getAll: function () {
            return new Promise(resolve => {
                http.get(endpoint)
                    .then(res => {
                        const items = res.data;
                        resolve(items);
                    })
                    .catch(err => resolve(null));
            });
        },

        getOne: function (id) {
            return new Promise(resolve => {
                http.get(`${endpoint}/${id}`)
                    .then(res => {
                        const item = res.data;
                        resolve(item);
                    })
                    .catch(err => resolve(null));
            });
        },

        create: function (data) {
            return new Promise(resolve => {
                http.post(endpoint, data, { maxContentLength: 100000000, maxBodyLength: 100000000 })
                    .then(res => {
                        const itemId = res.data;
                        resolve(itemId);
                    })
                    .catch(err => resolve(null));
            });
        },

        update: function (itemId, item) {
            return new Promise(resolve => {
                http.put(`${endpoint}/${itemId}`, item)
                    .then(res => resolve(true))
                    .catch(err => resolve(false));
            });
        },

        delete: function (itemId) {
            return new Promise(resolve => {
                http.delete(`${endpoint}/${itemId}`)
                    .then(res => resolve(true))
                    .catch(err => resolve(null));
            });
        }
    }
};