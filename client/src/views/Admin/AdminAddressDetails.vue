<template>
  <b-container class="my-5">
    <h1>Address Details</h1>
    <b-form @submit.prevent="onFormSubmit">
      <b-form-group label="Location name">
        <b-input v-model="address.name" />
      </b-form-group>
      <b-form-row>
        <b-col cols="2">
          <b-form-group label="Street number">
            <b-input required v-model="address.streetNumber" />
          </b-form-group>
        </b-col>
        <b-col>
          <b-form-group label="Street name">
            <b-input required v-model="address.streetName" />
          </b-form-group>
        </b-col>
        <b-col cols="3">
          <b-form-group label="Unit/Apt. number">
            <b-input v-model="address.streetDetail" />
          </b-form-group>
        </b-col>
      </b-form-row>
      <b-form-row>
        <b-col cols="6">
          <b-form-group label="City">
            <b-input required v-model="address.city" />
          </b-form-group>
        </b-col>
        <b-col cols="3">
          <b-form-group label="State">
            <b-input required v-model="address.state" />
          </b-form-group>
        </b-col>
        <b-col cols="3">
          <b-form-group label="Postal code">
            <b-input required v-model="address.postalCode" />
          </b-form-group>
        </b-col>
      </b-form-row>
      <b-form-group label="Country">
        <b-select expanded v-model="address.country">
          <option
            v-for="country in countries"
            :key="country.code"
            :value="country.code"
          >{{ country.name + " (" + country.code + ")" }}</option>
        </b-select>
      </b-form-group>
      <b-button type="submit" variant="success" squared>Save</b-button>
    </b-form>
    <b-toast variant="success" title="Success" v-model="showAddressSuccessToast">Address updated!</b-toast>
  </b-container>
</template>

<script>
import { ACTIONS } from "@/store";

const countryApiUri = "https://restcountries.eu/rest/v2/all";

export default {
  name: "AdminAddressDetails",
  data() {
    return {
      id: null,
      address: {},
      countries: [],
      showAddressSuccessToast: false,
    };
  },
  created() {
    const id = this.$route.params.id;
    this.id = id;
  },
  mounted() {
    const address = this.$store.getters.findAddress(this.id);
    this.address = address;
    this.fetchCountries();
  },
  methods: {
    async fetchCountries() {
      fetch(countryApiUri)
        .then((res) => res.json())
        .then((json) => {
          const countries = json.map((country) => {
            return { name: country.name, code: country.alpha2Code };
          });

          this.countries = countries.sort((a, b) => a.name - b.name);
        });
    },
    async onFormSubmit() {
      const saved = await this.$store.dispatch(
        ACTIONS.ADDRESSES.UPDATE,
        this.address
      );

      if (saved) {
        this.address = this.$store.getters.findAddress(this.id);
        this.showAddressSuccessToast = true;
      }
    },
  },
};
</script>