<template>
    <b-container class="mt-5">
        <h1 class="text-center">Registry Management</h1>
        <div class="my-1 d-flex justify-content-between">
            <b-button squared size="sm" variant="success" @click="onNewRegistry"><b-icon-plus /> New Registry</b-button>
            <b-button squared size="sm" variant="primary" @click="fetchRegistries"><b-icon-arrow-clockwise /> Refresh</b-button>
        </div>
        <b-table :fields="fields" :items="registries" show-empty selectable select-mode="single" @row-selected="onRegistrySelected">

            <template v-slot:cell(url)="row">
                <b-button variant="link" @click="openLink(row.item.url)">{{ row.item.url }}</b-button>
            </template>

            <template v-slot:cell(options)="row">
                <b-button size="sm" variant="link" class="text-danger" @click="onDeleteRegistry(row.item.registryId)">Delete</b-button>
            </template>

            <template v-slot:row-details="row">
                <b-container class="p-3 border border-secondary">
                    <h3>Edit {{ row.item.name }} Registry</h3>
                    <RegistryForm :registry="registries.find(x => x.registryId == row.item.registryId)" @close="onRegistryEditClose(row.item.registryId)" />
                </b-container>
            </template>

        </b-table>

        <NewRegistryModal :visible="isNewRegistryModalVisible" @close="onNewRegistryModalClose" />
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
import NewRegistryModal from "@/components/modals/NewRegistryModal";
import RegistryForm from "@/components/forms/RegistryForm";

export default {
    name: "AdminRegistries",
    components: {
        NewRegistryModal,
        RegistryForm
    },
    data() {
        return {
            fields: [
                "name",
                {
                    key: "url",
                    label: "URL"
                },
                "options"
            ],
            isNewRegistryModalVisible: false
        }
    },
    computed: {
        registries() {
            return this.$store.getters.registries;
        }
    },
    mounted() {
        this.fetchRegistries();
    },
    methods: {
        async fetchRegistries() {
            await this.$store.dispatch(ACTIONS.REGISTRY_ACTIONS.FETCH_ALL);
        },
        async onNewRegistry() {
            this.isNewRegistryModalVisible = true;
        },
        onNewRegistryModalClose() {
            this.isNewRegistryModalVisible = false;
        },
        onRegistrySelected(rows) {
            const openRegistries = rows.map(x => x.registryId);

            for (let registry of this.registries) {
                const isOpen = openRegistries.includes(registry.registryId);

                if (registry._showDetails != isOpen)
                    this.$set(registry, "_showDetails", isOpen);
            }
        },
        onRegistryEditClose(id) {
            const registry = this.registries.find(x => x.registryId == id);

            if (registry == null) return;

            this.$set(registry, "_showDetails", false);
        },
        onDeleteRegistry(id) {
            if (confirm("Are you sure you want to delete this registry?")) {
                this.$store.dispatch(ACTIONS.REGISTRY_ACTIONS.DELETE, id);
            }
        },
        openLink(url) {
            window.open(url)
        }
    }
}
</script>