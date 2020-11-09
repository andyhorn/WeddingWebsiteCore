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
        "imageFile": function () {
            if (this.imageFile == null) {
                this.imageState = false;
                return;
            }

            const reader = new FileReader();

            const vm = this;
            reader.onload = e => {
                vm.src = e.target.result;
                this.imageState = true;
            }

            reader.readAsDataURL(this.imageFile);
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
            const getImageData = () => {
                const key = "base64,";
                const index = this.src.indexOf(key);
                const dataStr = this.src.slice(index + key.length);
                return dataStr
            };

            const getBytes = base64String => {
                const binaryString = atob(base64String);
                const len = binaryString.length;
                const bytes = new Uint8Array(len);

                for (let i = 0; i < len; i++)
                    bytes[i] = binaryString.charCodeAt(i);
                
                return bytes.buffer;
            };

            const base64StringData = getImageData();
            const bytes = getBytes(base64StringData);
            
            const icon = {
                id: this.id || undefined,
                title: this.title,
                data: base64StringData
            };

            console.log(icon)

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