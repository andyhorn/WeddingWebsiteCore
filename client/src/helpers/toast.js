import { BToast } from "bootstrap-vue";

const success = function (message) {
    const toaster = new BToast();
    toaster.$bvToast.toast(message, {
        title: "Success!",
        variant: "success"
    });
}

const failure = message => {
    const toaster = new BToast();
    toaster.$bvToast.toast(message, {
        title: "Oops!",
        variant: "danger"
    });
}

const error = message => {
    const toaster = new BToast();
    toaster.$bvToast.toast(message, {
        title: "Error!",
        variant: "warning"
    });
}

export {
    success,
    failure,
    error
}