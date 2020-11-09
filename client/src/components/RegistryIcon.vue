<template>
    <img class="img icon mx-2" :src="src" />
</template>

<script>
import { ACTIONS } from "@/store";

export default {
    name: "RegistryIcon",
    props: [ "iconId" ],
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