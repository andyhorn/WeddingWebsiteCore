<template>
    <b-container class="my-5">
        <div class="d-flex flex-column align-items-start">
            <h1>Accommodations</h1>
            <b-button class="my-1" variant="success" squared size="sm">New Accommodation</b-button>
            <b-button class="my-1" variant="success" squared size="sm" @click="onNewCategory">New Category</b-button>
        </div>
        <Category v-for="category in topLevelCategories" :key="category.categoryId"
            :category="category" />

        <NewCategoryModal :visible="newCategoryModalIsVisible" @closed="newCategoryModalIsVisible = false" />
    </b-container>
</template>

<script>
import Category from "@/components/Admin/Accommodations/Category";
import NewCategoryModal from "@/components/modals/NewCategoryModal";
import { ACTIONS } from "@/store";

export default {
    name: "AdminAccommodations",
    components: {
        Category,
        NewCategoryModal
    },
    data() {
        return {
            newCategoryModalIsVisible: false,
        }
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
    },
    methods: {
        onNewCategory() {
            this.newCategoryModalIsVisible = true;
        },
        async fetch() {
            await Promise.all([
                this.$store.dispatch(ACTIONS.ACCOMMODATION_ACTIONS.FETCH_ALL),
                this.$store.dispatch(ACTIONS.CATEGORY_ACTIONS.FETCH_ALL)
            ]);
        }
    },
    mounted() {
        this.fetch();
    }
}
</script>