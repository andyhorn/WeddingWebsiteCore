import { http } from "@/axios";

const endpoint = "events";

export default {
    getAll: function () {
        return new Promise(async resolve => {
            http.get(endpoint)
                .then(res => {
                    const events = res.data;
                    resolve(events);
                })
                .catch(err => resolve(null));
        });
    },

    getOne: function (id) {
        return new Promise(async resolve => {
            http.get(endpoint + "/" + id)
                .then(res => {
                    const event = res.data;
                    resolve(event);
                })
                .catch(err => resolve(null));
        });
    },

    create: function (event) {
        return new Promise(async resolve => {
            http.post(endpoint, event)
                .then(res => {
                    const eventId = res.data;
                    resolve(eventId);
                })
                .catch(err => resolve(false));
        });
    },

    update: function (id, event) {
        return new Promise(async resolve => {
            http.put(endpoint + "/" + id, event)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    },

    delete: function (id) {
        return new Promise(async resolve => {
            http.delete(endpoint + "/" + id)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    }
}