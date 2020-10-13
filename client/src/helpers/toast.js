import Vue from "vue";
const vm = new Vue({});

const success = message => {
    vm.$bvToast.toast(message, {
        title: "Success!",
        variant: "success"
    });
}

const failure = message => {
    vm.$bvToast.toast(message, {
        title: "Oops!",
        variant: "danger"
    });
}

const error = message => {
    vm.$bvToast.toast(message, {
        title: "Error!",
        variant: "warning"
    });
}

export {
    success,
    failure,
    error
}