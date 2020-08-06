import { BToast } from "bootstrap-vue";

export default class Toast {
    constructor() {
        this.toast = new BToast();
        this.message = "";
        this.title = "";
        this.variant = "primary";
        this.dismissable = true;
    }

    setMessage(message) {
        this.message = message;
    }

    setTitle(title) {
        this.title = title;
    }

    setVariant(variant) {
        this.variant = variant;
    }

    setDismissable(isDismissable) {
        this.dismissable = isDismissable;
    }

    show() {
        this.toast.$bvToast.toast(
            this.message, {
            'title': this.title,
            'variant': this.variant,
            'no-auto-hide': !this.isDismissable,
            duration: 3000
        }
        );
    }
}