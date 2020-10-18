<template>
    <b-form @submit.prevent="onSubmit">
        <b-row>
            <b-col>
                <b-form-group label="Name/Alias" :state="nameState" description="Required">
                    <b-form-input v-model="name" :state="nameState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col cols="4">
                <b-form-group label="Street Number" :state="streetNumberState" description="Required">
                    <b-form-input v-model="streetNumber" :state="streetNumberState" />
                </b-form-group>
            </b-col>
            <b-col cols="8">
                <b-form-group label="Street Name" :state="streetNameState" description="Required">
                    <b-form-input v-model="streetName" :state="streetNameState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Unit/Apt. Number">
                    <b-form-input v-model="streetDetail" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col cols="6">
                <b-form-group label="City" :state="cityState" description="Required">
                    <b-form-input v-model="city" :state="cityState" />
                </b-form-group>
            </b-col>
            <b-col cols="4">
                <b-form-group label="State" :state="stateState" description="Required">
                    <b-form-input v-model="state" :state="stateState" />
                </b-form-group>
            </b-col>
            <b-col cols="2">
                <b-form-group label="Postal Code" :state="postalCodeState" description="Required">
                    <b-form-input v-model="postalCode" :state="postalCodeState" />
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col>
                <b-form-group label="Country" :state="countryState" description="Required">
                    <b-select v-model="country" :state="countryState">
                        <option v-for="country in countries"
                            :key="country.code"
                            :native-value="country.code"
                            :value="country.code"
                        >
                            {{ `${country.name} (${country.code})` }}
                        </option>
                    </b-select>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col class="d-flex justify-content-between">
                <b-button squared size="sm" variant="success" type="submit" :disabled="isSaveButtonDisabled">Save</b-button>
                <b-button squared size="sm" variant="danger" @click="onCancel">Cancel</b-button>
            </b-col>
        </b-row>
    </b-form>
</template>

<script>
import { ACTIONS } from "@/store";
import { http } from "@/axios";
const Toast = require("@/helpers/toast");

const COUNTRY_LIST_API = "https://restcountries.eu/rest/v2/all";
const DEFAULT_COUNTRY_CODE = "US";

export default {
    name: "AddressForm",
    props: [ "address" ],
    data() {
        return {
            id: null,
            name: null,
            streetNumber: null,
            streetName: null,
            streetDetail: null,
            city: null,
            state: null,
            postalCode: null,
            country: DEFAULT_COUNTRY_CODE,
            nameState: null,
            streetNumberState: null,
            streetNameState: null,
            cityState: null,
            stateState: null,
            postalCodeState: null,
            countryState: null,
            countries: []
        }
    },
    async mounted() {
        const countries = await this.fetchCountryList();
        this.countries = countries;
    },
    computed: {
        isSaveButtonDisabled() {
            return this.nameState !== true
                || this.streetNumberState !== true
                || this.streetNameState !== true
                || this.cityState !== true
                || this.stateState !== true
                || this.postalCodeState !== true
                || this.countryState !== true;
        }
    },
    watch: {
        "address": {
            immediate: true,
            deep: true,
            handler: function () {
                if (this.address == null) return;

                this.id = this.address.addressId;
                this.name = this.address.name;
                this.streetNumber = this.address.streetNumber;
                this.streetDetail = this.address.streetDetail;
                this.city = this.address.city;
                this.state = this.address.state;
                this.country = this.address.country;
            }
        },
        "name": function () {
            this.nameState = !!this.name && !!this.name.trim();
        },
        "streetNumber": function () {
            this.streetNumberState = !!this.streetNumber && !!this.streetNumber.trim();
        },
        "streetName": function () {
            this.streetNameState = !!this.streetName && !!this.streetName.trim();
        },
        "city": function () {
            this.cityState = !!this.city && !!this.city.trim();
        },
        "state": function () {
            this.stateState = !!this.state && !!this.state.trim();
        },
        "postalCode": function () {
            this.postalCodeState = !!this.postalCode && !!this.postalCode.trim();
        },
        "country": {
            immediate: true,
            handler: function () {
                this.countryState = !!this.country && !!this.country.trim();
            }
        }
    },
    methods: {
        clear() {
            this.id = null;
            this.name = null;
            this.streetNumber = null;
            this.streetName = null;
            this.streetDetail = null;
            this.city = null;
            this.state = null;
            this.postalCode = null;
            this.country = DEFAULT_COUNTRY_CODE;
        },
        resetStates() {
            this.nameState = null;
            this.streetNumberState = null;
            this.streetNameState = null;
            this.cityState = null;
            this.stateState = null;
            this.postalCodeState = null;
            this.countryState = null;
        },
        close(success) {
            if (success) Toast.success(this, "Address saved!");

            this.$emit("close", success);
            this.clear();
            this.resetStates();
        },
        onCancel() {
            this.close(false);
        },
        async onSubmit() {
            if (this.isSaveButtonDisabled) return;

            const address = {
                addressId: this.id || undefined,
                name: this.name,
                streetNumber: this.streetNumber,
                streetName: this.streetName,
                streetDetail: this.streetDetail,
                city: this.city,
                state: this.state,
                postalCode: this.postalCode,
                country: this.country
            };

            const command = this.id == null
                ? ACTIONS.ADDRESS_ACTIONS.CREATE
                : ACTIONS.ADDRESS_ACTIONS.UPDATE;

            const success = await this.$store.dispatch(command, address);
            if (!!success) {
                await this.$store.dispatch(ACTIONS.ADDRESS_ACTIONS.FETCH_ALL);
                this.close(success);
            } else {
                Toast.error("Unable to save address.");
            }
        },
        fetchCountryList() {
            return new Promise(async (resolve) => {
                const countries = await fetch(COUNTRY_LIST_API)
                    .then(res => res.json())
                    .catch(err => {
                        console.log(err)
                        resolve(null);
                    });

                const options = countries
                    .sort((a, b) => a.name - b.name)
                    .map(country => {
                        return {
                            name: country.name,
                            code: country.alpha2Code
                        };
                    });
                
                return resolve(options);
            });
        }
    }
}
</script>