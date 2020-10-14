// import Vue from "vue";
// const vm = new Vue({});

const success = (vm, message) => {
    vm.$bvToast.toast(message, {
        title: "Success!",
        variant: "success"
    });
}

const failure = (vm, message) => {
    vm.$bvToast.toast(message, {
        title: "Oops!",
        variant: "danger"
    });
}

const error = (vm, message) => {
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