<template>
    <b-container ref="container" 
        class="dropzone-container d-flex flex-column align-items-center justify-content-center" 
        :style="{ height: containerHeight }" @click="onClick">
        <p><b-icon-upload /></p>
        <label>{{ message }}</label>
        <p v-if="value && value.length > 0">{{ value.length }} file(s) selected.</p>
        <div class="files-list-container align-bottom d-flex">
            <Tag v-for="(file, index) in value" :key="index" :content="file.name" 
                @remove="onRemove(file)" />
        </div>
    </b-container>
</template>

<script>
const defaultHeight = 250;
import Tag from "@/components/Tag";

export default {
    name: "Dropzone",
    components: {
        Tag
    },
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
        onRemove(file) {
            const files = [...this.value];
            const index = files.indexOf(file);
            if (index != -1) {
                files.splice(index, 1);
                this.emitFiles(files);
            }
        },
        onDrop(e) {
            const files = e.dataTransfer.files;
            this.emitFiles(files);
        },
        onClick() {
            const file = document.createElement("input");
            file.type = "file";
            
            file.setAttribute("accept", this.accept);
            file.setAttribute("multiple", !!this.multiple)
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
        position: relative;
        border: 1px dashed grey;
        border-radius: 5px;
        background: #DEDCDC;
    }
    .dropzone-container:hover,
    .dropzone-container *:hover {
        background: #B3B3B3;
        cursor: pointer;
    }
    .files-list-container {
        position: absolute;
        bottom: 10px;
        left: 10px;
    }
</style>