<template>
    <b-container class="my-5">
        <div class="border border-secondary rounded p-4 my-2">
            <h2>Categories</h2>
            <b-button class="my-1" variant="success" squared size="sm" @click="onNewCategory">New Category</b-button>
            <b-table
                :tbody-tr-class="categoryClass"
                :items="categories"
                :fields="categoryFields"
            >
                <template v-slot:cell(parentId)="data">
                    <span v-if="data.item.parentId == null">None</span>
                    <span v-else>{{ categories.find(x => x.categoryId == data.item.parentId).name }}</span>
                </template>

                <template v-slot:cell(options)="data">
                    <b-button class="mx-1" size="sm" squared variant="success" @click="onEditCategory(data.item.categoryI)">Edit</b-button>
                    <b-button class="mx-1" size="sm" squared variant="outline-danger" @click="onDeleteCategory(data.item.categoryId)">Delete</b-button>
                </template>
            </b-table>
        </div>

        <div class="border border-secondary rounded p-4 my-2">
            <h2>Accommodations</h2>
            <b-button class="my-1" variant="success" squared size="sm" @click="onNewAccommodation">New Accommodation</b-button>
            <b-table
                :tbody-tr-class="accommodationClass"
                :items="accommodations"
                :fields="accommodationFields"
            >
                <template v-slot:cell(addressId)="data">
                    <span v-if="data.item.addressId == null">None</span>
                    <span v-else>{{ printAddress(data.item.addressId )}}</span>
                </template>

                <template v-slot:cell(categoryId)="data">
                    <span v-if="data.item.categoryId == null">Uncategorized</span>
                    <span v-else>{{ categories.find(x => x.categoryId == data.item.categoryId).name }}</span>
                </template>

                <template v-slot:cell(options)="data">
                    <b-button class="mx-1" size="sm" squared variant="success" @click="onEditAccommodation(data.item.accommodationId)">Edit</b-button>
                    <b-button class="mx-1" size="sm" squared variant="outline-danger" @click="onDeleteAccommodation(data.item.accommodationId)">Delete</b-button>
                </template>
            </b-table>
        </div>

        <NewCategoryModal :visible="isNewCategoryModalVisible" @closed="isNewCategoryModalVisible = false" />
        <NewAccommodationModal :visible="isNewAccommodationModalVisible" @closed="isNewAccommodationModalVisible = false" />
    </b-container>
</template>

<script>
import Category from "@/components/Admin/Accommodations/Category";
import NewCategoryModal from "@/components/modals/NewCategoryModal";
import NewAccommodationModal from "@/components/modals/NewAccommodationModal";
import AdminAccommodation from "@/components/Admin/Accommodations/Accommodation";
import { ACTIONS } from "@/store";

export default {
    name: "AdminAccommodations",
    components: {
        Category,
        NewCategoryModal,
        NewAccommodationModal,
        AdminAccommodation
    },
    data() {
        return {
            isNewCategoryModalVisible: false,
            isNewAccommodationModalVisible: false,
            categoryFields: [
                {
                    key: "categoryId",
                    label: "ID",
                },
                "name",
                {
                    key: "parentId",
                    label: "Subcategory Of"
                },
                "options"
            ],
            accommodationFields: [
                {
                    key: "accommodationId",
                    label: "ID"
                },
                "name",
                "description",
                {
                    key: "addressId",
                    label: "Location"
                },
                {
                    key: "categoryId",
                    label: "Category"
                },
                "options"
            ]
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
            this.isNewCategoryModalVisible = true;
        },
        onNewAccommodation() {
            this.isNewAccommodationModalVisible = true;
        },
        onEditCategory(categoryId) {

        },
        async onDeleteCategory(categoryId) {
            if (confirm("Are you sure you want to delete this category?")) {
                await this.$store.dispatch(ACTIONS.CATEGORY_ACTIONS.DELETE, categoryId);
            }
        },
        onEditAccommodation(accommodationId) {

        },
        async onDeleteAccommodation(accommodationId) {
            if (confirm("Are you sure you want to delete this accommodation?")) {
                await this.$store.dispatch(ACTIONS.ACCOMMODATION_ACTIONS.DELETE, accommodationId);
            }
        },
        async fetch() {
            await Promise.all([
                this.$store.dispatch(ACTIONS.ACCOMMODATION_ACTIONS.FETCH_ALL),
                this.$store.dispatch(ACTIONS.CATEGORY_ACTIONS.FETCH_ALL)
            ]);
        },
        printAddress(addressId) {
            if (addressId == null) return "";

            const address = this.$store.getters.findAddress(addressId);
            if (!address) return "";

            let str = `${address.streetNumber} ${address.streetName}`;
            if (address.streetDetail) str += ` Unit ${address.streetDetail}`;

            str += `, ${address.city}, ${address.state} ${address.postalCode}`;

            return str;
        },
        accommodationClass(item, type) {
            if (!item || type != "row") return;

            if (item.categoryId == null) return "table-danger";
        },
        categoryClass(item, type) {
            if (!item || type != "row") return;

            if (item.parentId == null) return "table-info";
        }
    },
    mounted() {
        this.fetch();
    }
}
</script>