import { http } from "@/axios";

const endpoint = "rsvps";

export default {
    getAll: function () {
        return new Promise(resolve => {
            http.get(endpoint)
                .then(res => {
                    const rsvps = res.data;
                    resolve(rsvps);
                })
                .catch(err => resolve(null));
        });
    },

    getOne: function (rsvpId) {
        return new Promise(resolve => {
            http.get(endpoint + "/" + rsvpId)
                .then(res => {
                    const rsvp = res.data;
                    resolve(rsvp);
                })
                .catch(err => resolve(null));
        });
    },

    create: function (rsvp) {
        return new Promise(resolve => {
            http.post(endpoint, rsvp)
                .then(res => {
                    const rsvpId = res.data;
                    resolve(rsvpId);
                })
                .catch(err => resolve(false));
        });
    },

    update: function (id, rsvp) {
        return new Promise(resolve => {
            http.put(endpoint + "/" + id, rsvp)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    },

    delete: function (rsvpId) {
        return new Promise(resolve => {
            http.delete(endpoint + "/" + rsvpId)
                .then(res => resolve(true))
                .catch(err => resolve(false));
        });
    }
}