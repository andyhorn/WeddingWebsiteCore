<template>
    <b-container>
        <p>Category</p>
        <p v-if="parent == null">Top level</p>
        <Category v-for="category in children" :key="category.categoryId"
            :category="category" />
    </b-container>
</template>

<script>
import AdminAccommodation from "@/components/Admin/Accommodations/Accommodation";
import Category from "@/components/Admin/Accommodations/Category";

export default {
    name: "AdminCategory",
    props: ["category"],
    components: {
        AdminAccommodation,
        Category
    },
    computed: {
        accommodations() {
            return this.$store.getters.accommodations.filter(x => x.categoryId == this.category.categoryId);
        },
        parent() {
            if (this.category.parentId)
                return this.$store.getters.categories.filter(x => x.categoryId == this.category.parentId);
            
            return null;
        },
        children() {
            return this.$store.getters.categories.filter(x => x.parentId == this.category.categoryId);
        }
    }
}
</script>