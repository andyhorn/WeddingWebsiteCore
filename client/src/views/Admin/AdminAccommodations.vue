<template>
    <b-container class="my-5">
        <h1 class="text-center">Accommodation Management</h1>
        <div class="border border-secondary rounded p-4 my-2">
            <h2>Categories</h2>
            <b-button class="my-1" variant="success" squared size="sm" @click="onNewCategory">New Category</b-button>
            <b-table
                selectable
                show-empty
                caption="Select a category to edit."
                select-mode="single"
                :busy="isBusy"
                :tbody-tr-class="categoryClass"
                :items="categories"
                :fields="categoryFields"
                @row-selected="onCategorySelected"
            >
                <template v-slot:cell(parentId)="data">
                    <span v-if="data.item.parentId == null">None</span>
                    <span v-else>{{ categories.find(x => x.categoryId == data.item.parentId).name }}</span>
                </template>

                <template v-slot:cell(options)="data">
                    <b-button class="mx-auto" size="sm" squared variant="outline-danger" @click="onDeleteCategory(data.item.categoryId)">Delete</b-button>
                </template>
            </b-table>
            <b-collapse v-model="isCategoryEditVisible">
                <CategoryForm 
                    v-if="isCategoryEditVisible" 
                    :category="categoryUnderEdit" 
                    :visible="isCategoryEditVisible"
                    @close="onCategoryEditClose" />
            </b-collapse>
        </div>

        <div class="border border-secondary rounded p-4 my-2">
            <h2>Accommodations</h2>
            <b-button class="my-1" variant="success" squared size="sm" @click="onNewAccommodation">New Accommodation</b-button>
            <b-table
                selectable
                show-empty
                caption="Select an accommodation to edit."
                select-mode="single"
                :busy="isBusy"
                :tbody-tr-class="accommodationClass"
                :items="accommodations"
                :fields="accommodationFields"
                @row-selected="onAccommodationSelected"
            >
                <template v-slot:cell(addressId)="data">
                    <span v-if="data.item.addressId == null">None</span>
                    <span v-else>{{ printAddress(data.item.addressId) }}</span>
                </template>

                <template v-slot:cell(categoryId)="data">
                    <span v-if="data.item.categoryId == null">Uncategorized</span>
                    <span v-else>
                        <p>
                            <span v-for="(category, index) in printCategoryTree(data.item.categoryId).split(',')"
                                :key="category"
                            >
                                <span v-if="index == 0">{{ category }}</span>
                                <span v-else :style="{ 'padding-left': `${(index - 1) * 15}px` }">
                                    <b-icon-arrow-return-right /> {{ category }}
                                </span>
                                <br />
                            </span>
                        </p>
                    </span>
                </template>

                <template v-slot:cell(options)="data">
                    <b-button class="mx-auto" size="sm" squared variant="outline-danger" @click="onDeleteAccommodation(data.item.accommodationId)">Delete</b-button>
                </template>
            </b-table>

            <b-collapse v-model="isAccommodationEditVisible">
                <AccommodationForm :accommodation="accommodationUnderEdit" @close="onAccommodationEditClose" />
            </b-collapse>
        </div>

        <NewCategoryModal :visible="isNewCategoryModalVisible" @close="onCategoryModalClose" />
        <NewAccommodationModal :visible="isNewAccommodationModalVisible" @close="onAccommodationModalClose" />
    </b-container>
</template>

<script>
import Category from "@/components/Admin/Accommodations/Category";
import NewCategoryModal from "@/components/modals/NewCategoryModal";
import CategoryForm from "@/components/forms/CategoryForm";
import NewAccommodationModal from "@/components/modals/NewAccommodationModal";
import AccommodationForm from "@/components/forms/AccommodationForm";
import AdminAccommodation from "@/components/Admin/Accommodations/Accommodation";
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
    name: "AdminAccommodations",
    components: {
        Category,
        NewCategoryModal,
        CategoryForm,
        NewAccommodationModal,
        AccommodationForm,
        AdminAccommodation
    },
    data() {
        return {
            isBusy: false,
            isNewCategoryModalVisible: false,
            isNewAccommodationModalVisible: false,
            isCategoryEditVisible: false,
            isAccommodationEditVisible: false,
            accommodationUnderEdit: null,
            categoryUnderEdit: null,
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
                {
                    key: "options",
                    tdClass: "optionsColumn"
                }
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
        onNewAccommodation() {
            this.isNewAccommodationModalVisible = true;
        },
        onAccommodationModalClose(success) {
            if (success) Toast.success(this, "Accommodation saved!");

            this.isNewAccommodationModalVisible = false;
        },
        onAccommodationSelected(rows) {
            if (rows.length > 0) {
                this.accommodationUnderEdit = rows[0];
                this.isAccommodationEditVisible = true;
            } else {
                this.isAccommodationEditVisible = false;
                this.accommodationUnderEdit = null;
            }
        },
        onAccommodationEditClose(success) {
            if (success) Toast.success(this, "Accommodation saved!");

            this.isAccommodationEditVisible = false;
        },
        async onDeleteAccommodation(accommodationId) {
            if (confirm("Are you sure you want to delete this accommodation?")) {
                this.isBusy = true;
                await this.$store.dispatch(ACTIONS.ACCOMMODATION_ACTIONS.DELETE, accommodationId);
                this.fetch();
            }
        },
        onNewCategory() {
            this.isNewCategoryModalVisible = true;
        },
        onCategoryModalClose(success) {
            if (success) Toast.success(this, "Category saved!");

            this.isNewCategoryModalVisible = false;
        },
        onCategorySelected(rows) {
            if (rows.length > 0) {
                this.categoryUnderEdit = rows[0];
                this.isCategoryEditVisible = true;
            } else {
                this.categoryUnderEdit = null;
                this.isCategoryEditVisible = false;
            }
        },
        onCategoryEditClose(success) {
            if (success) Toast.success(this, "Category saved!");

            this.isCategoryEditVisible = false;
            this.categoryUnderEdit = null;
        },
        async onDeleteCategory(categoryId) {
            if (confirm("Are you sure you want to delete this category?")) {
                this.isBusy = true;
                const category = this.categories.find(x => x.categoryId == categoryId);
                const parentId = category.parentId == null ? null 
                    : this.categories.find(x => x.categoryId == category.parentId).categoryId;

                for (let accommodation of this.accommodations.filter(x => x.categoryId == categoryId)) {
                    accommodation.categoryId = parentId;
                }

                await this.$store.dispatch(ACTIONS.CATEGORY_ACTIONS.DELETE, categoryId);
                this.fetch();
            }
        },
        async fetch() {
            this.isBusy = true;
            await Promise.all([
                this.$store.dispatch(ACTIONS.ACCOMMODATION_ACTIONS.FETCH_ALL),
                this.$store.dispatch(ACTIONS.CATEGORY_ACTIONS.FETCH_ALL)
            ]);
            this.isBusy = false;
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
        printCategoryTree(categoryId) {
            if (categoryId == null) return "";

            const category = this.categories.find(x => x.categoryId == categoryId)
            if (category == null) return "";

            const name = category.name;

            if (category.parentId != null)
                return this.printCategoryTree(category.parentId) + "," + name;
            else 
                return name;
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
    async mounted() {
        await this.fetch();
    }
}
</script>

<style>
    .optionsColumn {
        min-width: 10rem;
    }
</style>