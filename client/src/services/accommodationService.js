import { http } from "@/axios";

const endpoint = "accommodations";

export default {
    getAll: function () {
        return new Promise(resolve => {
            http.get(endpoint)
                .then(res => {
                    const accommodations = res.data;
                    resolve(accommodations);
                })
                .catch(err => resolve(null));
        });
    },

    getOne: function (id) {
        return new Promise(resolve => {
            http.get(endpoint + "/" + id)
                .then(res => {
                    const accommodation = res.data;
                    resolve(accommodation);
                })
                .catch(err => resolve(null));
        });
    },

    create: function (accommodation) {
        return new Promise(resolve => {
            http.post(endpoint, accommodation)
                .then(res => {
                    const accommodationId = res.data;
                    resolve(accommodationId);
                })
                .catch(err => resolve(null));
        });
    },

    update: function (accomodationId, accommodation) {
        return new Promise(resolve => {
            http.put(endpoint + "/" + accomodationId, accommodation)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    },

    delete: function (accommodationId) {
        return new Promise(resolve => {
            http.delete(endpoint + "/" + accommodationId)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    }
}