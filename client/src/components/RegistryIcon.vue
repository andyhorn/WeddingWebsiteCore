<template>
    <img class="img icon" :src="src" :class="{ 'mx-2' : spaced }"/>
</template>

<script>
import { ACTIONS } from "@/store";

export default {
    name: "RegistryIcon",
    props: [ "iconId", "spaced" ],
    computed: {
        src() {
            if (this.icon == null) return "";

            return `data:image/gif;base64,${this.icon.data}`;
        },
        icon() {
            return this.$store.getters.findRegistryIcon(this.iconId);
        }
    },
}
</script>