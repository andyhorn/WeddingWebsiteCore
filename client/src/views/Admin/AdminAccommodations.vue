<template>
    <b-container class="my-5">
        <h1>Accommodations</h1>
        <Category v-for="category in topLevelCategories" :key="category.categoryId"
            :category="category" />
    </b-container>
</template>

<script>
import Category from "@/components/Admin/Accommodations/Category";

export default {
    name: "AdminAccommodations",
    components: {
        Category
    },
    computed: {
        categories() {
            return this.$store.getters.categories;
        },
        accommodations() {
            return this.$store.getters.accommodations;
        },
        topLevelCategories() {
            return this.$store.getters.categories.filter(x => x.parentId == null);
        }
    }
}
</script>