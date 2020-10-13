import { BToast } from "bootstrap-vue";

const DEFAULT_AUTOHIDE_DELAY_MS = 1000;

export default class Toast {
    constructor() {
        this.toast = new BToast();
        this.message = "";
        this.title = "";
        this.variant = "primary";
        this.autoHide = true;
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

    setAutoHide(autoHide) {
        this.autoHide = autoHide;
    }

    show() {
        this.toast.$bvToast.toast(
            this.message, {
            title: this.title,
            variant: this.variant,
            noAutoHide: !this.autoHide,
            autoHideDelay: DEFAULT_AUTOHIDE_DELAY_MS,
            toaster: 'b-toaster-bottom-right'
        });
    }
}