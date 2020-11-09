<template>
    <b-container>
        <b-form @submit.prevent="onSubmit">
            <b-row>
                <b-col>
                    <b-form-group label="Name" :state="nameState">
                        <b-form-input v-model="name" :state="nameState" />
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-form-group label="URL" :state="urlState">
                        <b-input-group prepend="http://">
                            <b-form-input v-model="url" :state="urlState" />
                            <b-input-group-append>
                                <span><b-spinner v-if="isUrlTestBusy" class="mx-2" /></span>
                            </b-input-group-append>
                        </b-input-group>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-form-group>
                        <template slot="label">
                            Icon (<b-button variant="link" size="sm" class="p-0 m-0" 
                            @click="isNewIconModalVisible = true">New</b-button>)
                        </template>
                        <b-dropdown variant="outline-secondary">
                            <template #button-content>
                                Selected: 
                                <span v-if="iconId == null"><strong>None</strong></span>
                                <span v-else><RegistryIcon :iconId="iconId"/></span>
                            </template>
                            <b-dropdown-item href="#" @click="iconId = null">None</b-dropdown-item>
                            <b-dropdown-item href="#" v-for="icon in icons" :key="icon.id"
                                @click="onSelectIcon(icon.id)">
                                <!-- <img class="img icon" :src="makeImgSrc(icon.data)"/> -->
                                <RegistryIcon :iconId="icon.id" />
                                {{ icon.title }}
                            </b-dropdown-item>
                        </b-dropdown>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col class="d-flex justify-content-between">
                    <b-button squared size="sm" variant="success" type="submit" :disabled="!isFormValid">Save</b-button>
                    <b-button squared size="sm" variant="danger" @click="onCancel">Cancel</b-button>
                </b-col>
            </b-row>
        </b-form>
        <NewIconModal :visible="isNewIconModalVisible" @close="onIconModalClose" />
    </b-container>
</template>

<script>
import { ACTIONS } from "@/store";
import { http } from "@/axios";
import NewIconModal from "@/components/modals/NewIconModal";
import RegistryIcon from "@/components/RegistryIcon";
const Toast = require("@/helpers/toast");

export default {
    name: "RegistryForm",
    props: [ "registry" ],
    components: {
        NewIconModal,
        RegistryIcon
    },
    data() {
        return {
            id: null,
            name: null,
            url: null,
            iconId: null,
            iconData: null,
            nameState: null,
            urlState: null,
            urlTestTimeout: null,
            isUrlTestBusy: false,
            isNewIconModalVisible: false
        }
    },
    watch: {
        "name": function () {
            this.nameState = !!this.name && !!this.name.trim();
        },
        "url": function () {
            this.validateUri(this.url)
        },
        "registry": {
            immediate: true,
            deep: true,
            handler: function () {
                if (this.registry == null) return;

                this.id = this.registry.registryId;
                this.name = this.registry.name;

                if (this.registry.url) {
                    const match = /https?:\/\//;
                    this.url = this.registry.url;
                    this.url = this.url.replace(match, "");
                }

                this.iconId = this.registry.iconId;
            }
        }
    },
    computed: {
        isFormValid() {
            return this.nameState === true
                && this.urlState === true;
        },
        icons() {
            return this.$store.getters.registryIcons;
        },
        iconSrc() {
            if (this.iconId == null)
                return "";

            const icon = this.icons.find(x => x.id == this.iconId);
            return this.makeImgSrc(icon.data);
        }
    },
    mounted() {
    },
    methods: {
        makeImgSrc(data) {
            return `data:image/gif;base64,${data}`;
        },
        clear() {
            this.id = null;
            this.name = null;
            this.url = null;
            this.iconId = null;
        },
        resetStates() {
            this.nameState = null;
            this.urlState = null;
        },
        close(id) {
            this.$emit("close", id);
            this.clear();
            this.resetStates();
        },
        onNewIcon() {

        },
        onIconModalClose(id) {
            if (id) {
                this.iconId = id;
            }

            this.isNewIconModalVisible = false;
        },
        onSelectIcon(id) {
            if (id) 
                this.iconId = id;
            else
                this.iconId = null;
        },
        async onSubmit() {
            if (!this.isFormValid) return;

            const registry = {
                registryId: this.id || undefined,
                name: this.name,
                url: this.url,
                iconId: this.iconId
            };

            const command = this.id == null
                ? ACTIONS.REGISTRIES.CREATE
                : ACTIONS.REGISTRIES.UPDATE;

            const success = await this.$store.dispatch(command, registry);
            if (success) {
                Toast.success("Registry saved!");
                this.close(success);
            } else {
                Toast.error("Unable to save registry.");
            }
        },
        onCancel() {
            this.close(null);
        },
        validateUri(url) {
            this.isUrlTestBusy = true;

            clearTimeout(this.urlTestTimeout);

            if (!url) {
                this.urlState = false;
                this.isUrlTestBusy = false;
                return
            }

            this.urlTestTimeout = setTimeout(async () => {
                http.post("/registries/validate", JSON.stringify(url))
                    .then(() => this.urlState = true)
                    .catch(() => this.urlState = false)
                    .finally(() => this.isUrlTestBusy = false);
            }, 1.5 * 1000);
        }
    }
}
</script>