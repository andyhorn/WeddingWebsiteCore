import axios from "axios";

const baseURL = process.env.VUE_APP_BASE_URL || "/api";

const instance = axios.create({
    baseURL
});

export default instance;