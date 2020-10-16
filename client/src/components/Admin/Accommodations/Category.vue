<template>
    <b-container class="border rounded my-2 py-3">
        <div class="d-flex justify-content-between align-items-center">
            <p class="m-0 p-0" :class="parent == null ? 'top-level' : 'sub-category'">
                <strong>{{ category.name }}</strong>
            </p>
            <p class="m-0 p-0 pointer" @click="onDelete"><b-icon icon="x" /></p>
        </div>
        <AdminAccommodation v-for="accommodation in accommodations" :key="accommodation.accommodationId" 
            :accommodation="accommodation" />
        <Category class="ml-2" v-for="category in children" :key="category.categoryId"
            :category="category" />
    </b-container>
</template>

<script>
import AdminAccommodation from "@/components/Admin/Accommodations/Accommodation";
import { ACTIONS } from "@/store";

export default {
    name: "AdminCategory",
    props: ["category"],
    components: {
        AdminAccommodation,
        Category: () => import("./Category.vue")
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
    },
    methods: {
        onDelete() {
            this.$store.dispatch(ACTIONS.CATEGORY_ACTIONS.DELETE, this.category.categoryId);
        }
    }
}
</script>

<style scoped>
    .pointer:hover {
        cursor: pointer;
        color: silver;
    }
    .top-level {
        font-size: 1.2rem;
        text-decoration: underline;
    }
    .sub-category {
        font-size: 0.9rem;
        font-style: italic;
    }
</style>