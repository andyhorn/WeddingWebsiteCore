<template>
    <b-container class="mt-5">
        <h1 class="text-center">Registry Management</h1>
        <div class="my-1 d-flex justify-content-between">
            <b-button squared size="sm" variant="success" @click="onNewRegistry"><b-icon-plus /> New Registry</b-button>
            <b-button squared size="sm" variant="primary" @click="fetchRegistries"><b-icon-arrow-clockwise /> Refresh</b-button>
        </div>
        <b-table :fields="fields" :items="registries" show-empty>
            <template v-slot:cell(url)="row">
                <a :href="url">{{ url }}</a>
            </template>
        </b-table>

        <NewRegistryModal :visible="isNewRegistryModalVisible" @close="onNewRegistryModalClose" />
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
import NewRegistryModal from "@/components/modals/NewRegistryModal";

export default {
    name: "AdminRegistries",
    components: {
        NewRegistryModal
    },
    data() {
        return {
            fields: [
                "name",
                {
                    key: "url",
                    label: "URL"
                }
            ],
            isNewRegistryModalVisible: false
        }
    },
    computed: {
        registries() {
            return this.$store.getters.registries;
        }
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
        }
    }
}
</script>