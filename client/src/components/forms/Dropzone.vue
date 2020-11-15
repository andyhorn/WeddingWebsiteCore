<template>
    <b-container ref="container"
        class="dropzone-container d-flex justify-content-center align-items-center" 
        :style="{ height: containerHeight }" @click="onClick">
        <label>{{ message }}</label>
    </b-container>
</template>

<script>
const defaultHeight = 250;

export default {
    name: "Dropzone",
    props: [ "value", "multiple", "directory", "height", "accept" ],
    computed: {
        containerHeight() {
            return !!this.height 
                ? `${this.height}px`
                : `${defaultHeight}px`;
        },
        message() {
            return "click here or drag-and-drop files to upload";
        }
    },
    mounted() {
        const stopEvents = e => {
            e.preventDefault();
            e.stopPropagation();
        };

        [ "dragenter", "dragover", "dragleave", "drop" ].forEach(name => 
            this.$refs["container"].addEventListener(name, stopEvents));

        this.$refs["container"].addEventListener("drop", e => this.onDrop(e));
    },
    methods: {
        onDrop(e) {
            const files = e.dataTransfer.files;
            this.emitFiles(files);
        },
        onClick() {
            const file = document.createElement("input");
            file.type = "file";
            
            file.setAttribute("accept", this.accept);
            file.onchange = (e) => this.emitFiles(e.target.files);
                        
            file.click();
            file.remove();
        },
        emitFiles(files) {
            this.$emit("input", files);
        }
    }
}
</script>

<style scoped>
    .dropzone-container {
        border: 1px solid grey;
        border-radius: 5px;
        background: #89FAF3;
        
    }
    .dropzone-container:hover,
    .dropzone-container *:hover {
        background: #CBF6F3;
        cursor: pointer;
    }
</style>