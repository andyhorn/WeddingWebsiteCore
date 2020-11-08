<template>
    <b-container>
        <b-row>
            <b-col>
                <b-form-group label="Title">
                    <b-form-input v-model="title" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Image">
                    <b-form-file v-model="imageFile"
                        placeholder="Select an image..."
                        accept="image/*"/>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row v-if="src != null">
            <b-col>
                <b-form-group label="Preview">
                    <img :src="src" />
                </b-form-group>
            </b-col>
        </b-row>
    </b-container>
</template>

<script>
export default {
    name: "RegistryIconForm",
    data() {
        return {
            title: null,
            titleState: null,
            imageFile: null,
            imageDataState: null,
            src: null
        }
    },
    watch: {
        "imageFile": function () {
            if (this.imageFile == null) return;

            const reader = new FileReader();

            const vm = this;
            reader.onload = e => {
                vm.src = e.target.result;
            }

            reader.readAsDataURL(this.imageFile);
        }
    },
    computed: {
    },
    methods: {
    }
}
</script>