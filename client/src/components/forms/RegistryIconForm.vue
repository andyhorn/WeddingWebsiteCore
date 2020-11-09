<template>
    <b-container>
        <b-row>
            <b-col>
                <b-form-group label="Title" :state="titleState">
                    <b-form-input v-model="title" :state="titleState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Image" :state="imageState">
                    <b-form-file v-model="imageFile"
                        placeholder="Select an image..."
                        accept="image/*"
                        :state="imageState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row v-if="src != null">
            <b-col>
                <b-form-group label="Preview">
                    <img class="img icon" :src="src" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col class="d-flex justify-content-between">
                <b-button squared size="sm" variant="success" @click="onSubmit" :disabled="!isFormValid">Save</b-button>
                <b-button squared size="sm" variant="danger" @click="onCancel">Cancel</b-button>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "RegistryIconForm",
    props: [ "iconId" ],
    data() {
        return {
            id: null,
            title: null,
            titleState: null,
            imageFile: null,
            imageState: null,
            src: null
        }
    },
    watch: {
        "imageFile": async function () {
            if (this.imageFile == null) {
                this.imageState = false;
                return;
            }

            const base64 = await this.getImageBase64String();
            const resized = await this.getResizedBase64String(base64, 256, 256);
            this.src = resized;
            this.imageState = true;
        },
        "title": function () {
            this.titleState = !!this.title && !!this.title.trim();
        }
    },
    computed: {
        isFormValid() {
            return this.titleState === true
                && this.imageState === true;
        }
    },
    methods: {
        getImageBase64String() {
            const vm = this;
            return new Promise(resolve => {
                if (vm.imageFile == null) return resolve("");

                const reader = new FileReader();

                reader.onload = e => {
                    const imageSrc = e.target.result;
                    return resolve(imageSrc);
                }
                
                reader.readAsDataURL(vm.imageFile);
            });
        },
        getResizedBase64String(base64, width, height) {
            return new Promise(resolve => {
                const canvas = document.createElement("canvas");
                
                canvas.width = width;
                canvas.height = height;
                
                const context = canvas.getContext("2d");
                const image = document.createElement("img");

                image.src = base64;
                image.onload = function() {
                    let scaleFactor = image.width > image.height
                        ? width / image.width
                        : height / image.height;

                    if (scaleFactor > 1)
                        scaleFactor = 1;

                    context.scale(scaleFactor, scaleFactor);
                    context.drawImage(image, 0, 0);

                    return resolve(canvas.toDataURL());
                }
            });
        },
        stripBase64String(base64) {
            const key = "base64,";
            const length = key.length;
            const index = base64.indexOf(key);
            const stripped = base64.slice(index + length);
            return stripped.trim();
        },
        clear() {
            this.imageFile = null;
            this.title = null;
        },
        resetStates() {
            this.imageState = null;
            this.titleState = null;
        },
        close(data) {
            this.clear();
            this.resetStates();
            this.$emit("close", data);
        },
        async onSubmit() {            
            const icon = {
                id: this.id || undefined,
                title: this.title,
                data: this.stripBase64String(this.src)
            };

            const command = this.id != null
                ? ACTIONS.REGISTRY_ICONS.UPDATE
                : ACTIONS.REGISTRY_ICONS.CREATE;
            
            const result = await this.$store.dispatch(command, icon);
            if (result) {
                Toast.success("Icon saved!");
                this.close(result);
            } else {
                Toast.failure("Could not save icon.");
            }
        },
        onCancel() {
            this.close(null);
        }
    }
}
</script>