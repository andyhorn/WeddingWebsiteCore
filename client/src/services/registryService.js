import { http } from "@/axios";

const endpoint = "registries";

export default {
    getAll: function () {
        return new Promise(resolve => {
            http.get(endpoint)
                .then(res => {
                    const registries = res.data;
                    resolve(registries);
                })
                .catch(err => resolve(null));
        });
    },

    getOne: function (id) {
        return new Promise(resolve => {
            http.get(endpoint + "/" + id)
                .then(res => {
                    const registry = res.data;
                    resolve(registry);
                })
                .catch(err => resolve(null));
        });
    },

    create: function (registry) {
        return new Promise(resolve => {
            http.post(endpoint, registry)
                .then(res => {
                    const registryId = res.data;
                    resolve(registryId);
                })
                .catch(err => resolve(null));
        });
    },

    update: function (registryId, registry) {
        return new Promise(resolve => {
            http.put(endpoint + "/" + registryId, registry)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    },

    delete: function (registryId) {
        return new Promise(resolve => {
            http.delete(endpoint + "/" + registryId)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    }
}