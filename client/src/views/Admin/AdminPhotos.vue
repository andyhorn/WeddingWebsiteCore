<template>
    <b-container class="pt-5">
        <h2 class="text-center">Photos</h2>
        <Dropzone v-model="photoList" accept="image/*"/>
        <b-button variant="success" class="mt-3" @click="onUpload">Upload</b-button>
        <p v-if="!!uploading" class="text-center">{{ uploading }}</p>
    </b-container>    
</template>

<script>
import Dropzone from "@/components/forms/Dropzone";
import imageService from "@/services/imageService";
import { ACTIONS } from "@/store";

export default {
    name: "AdminPhotos",
    components: {
        Dropzone
    },
    data() {
        return {
            photoList: [],
            uploading: ''
        }
    },
    methods: {
        async onUpload() {
            while (this.photoList.length > 0) {
                const image = this.photoList.shift();
                this.uploading = `Uploading ${image.name}...`;
                await this.uploadSingleImage(image);
            }

            this.uploading = '';
        },
        async uploadSingleImage(image) {
            return new Promise(async (resolve, reject) => {
                const formData = new FormData();
                formData.append(image.name, image);

                await this.$store.dispatch(ACTIONS.IMAGES.CREATE, formData);

                return resolve();
            });
        }
    }
}
</script>

<style scoped>
    .photo-form {
        height: 20vh;
        min-height: 20vh;
    }
</style>