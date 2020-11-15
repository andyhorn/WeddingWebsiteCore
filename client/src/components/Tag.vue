<template>
    <div class="d-flex align-items-center justify-content-between tag-wrapper px-2 py-1 m-1"
        @click="onClick">
        <p class="m-0 p-0">
            <span>{{ content }}</span>
            <a class="tag-remove text-danger pl-2" @click="$emit('remove')">
                <b-icon-x-circle />
            </a>
        </p>
    </div>
</template>

<script>
export default {
    name: "Tag",
    props: [ "content" ],
    methods: {
        onClick(e) {
            e.preventDefault();
            e.stopPropagation();
        }
    }
}
</script>

<style scoped>
    .tag-wrapper {
        border: 1px dotted grey;
        border-radius: 5px;
        background-color: #F0D1D1;
    }
    .tag-wrapper:hover {
        cursor: default;
    }
</style>