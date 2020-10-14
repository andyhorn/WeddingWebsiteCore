import { http } from "@/axios";

const endpoint = "categories";

export default {
    getAll: function () {
        return new Promise(resolve => {
            http.get(endpoint)
                .then(res => {
                    const categories = res.data;
                    resolve(categories);
                })
                .catch(err => resolve(null));
        });
    },

    getOne: function (categoryId) {
        return new Promise(resolve => {
            http.get(endpoint + "/" + categoryId)
                .then(res => {
                    const category = res.data;
                    resolve(category)
                })
                .catch(err => resolve(null));
        });
    },

    create: function (category) {
        return new Promise(resolve => {
            http.post(endpoint, category)
                .then(res => {
                    categoryId = res.data;
                    resolve(categoryId);
                })
                .catch(err => resolve(null));
        });
    },

    update: function (categoryId, category) {
        return new Promise(resolve => {
            http.put(endpoint + "/" + categoryId, category)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    },

    delete: function (categoryId) {
        return new Promise(resolve => {
            http.delete(endpoint + "/" + categoryId)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    }
}