import axios from "axios";

const baseURL = process.env.VUE_APP_BASE_URL || "/api";

const headers = {
    'Content-Type': 'application/json'
};

const http = axios.create({
    baseURL,
    headers
});

const setToken = function (token) {
    if (!http.defaults.headers)
        http.defaults.headers = headers;

    http.defaults.headers["Authorization"] = `Bearer ${token}`;
}

const stripToken = function () {
    if (http.defaults.headers && http.defaults.headers.Authorization)
        http.defaults.headers.Authorization = undefined;
}

export {
    http,
    setToken,
    stripToken
};