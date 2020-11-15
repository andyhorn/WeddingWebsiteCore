<template>
    <b-container class="pt-5">
        <h2 class="text-center">Photos</h2>
        <Dropzone v-model="photoList" accept="image/*"/>
    </b-container>    
</template>

<script>
import Dropzone from "@/components/forms/Dropzone";

export default {
    name: "AdminPhotos",
    components: {
        Dropzone
    },
    data() {
        return {
            photoList: []
        }
    },
    watch: {
        "photoList": {
            deep: true,
            handler: function () {
                console.log(this.photoList)
            }
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