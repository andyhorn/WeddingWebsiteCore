import { http, setToken as st, removeToken as rt } from "@/axios";

const endpoint = "users";

const authenticate = async (email, password) => {
    if (!email || !password) {
        return null;
    }

    const data = Object.assign({}, { email, password });

    try {
        const response = await http.post(endpoint + "/login", JSON.stringify(data));
        if (response.data) {
            return response.data;
        }
    } catch { }

    return null;
};

const refresh = async (token) => {
    const response = await http.post(endpoint + "/refresh", JSON.stringify(token));

    if (response.data) {
        return response.data;
    }

    return null;
}

const setToken = (token) => {
    st(token);
}

const removeToken = () => {
    rt();
}

export default {
    authenticate,
    refresh,
    setToken,
    removeToken
}