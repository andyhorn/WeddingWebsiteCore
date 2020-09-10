<template>
  <b-form @submit.prevent="onFormSubmit">
    <h2>Address</h2>
    <b-form-row>
      <b-form-group class="col" label="Street number">
        <b-input v-model="streetNumber" required />
      </b-form-group>
    </b-form-row>
    <b-form-row>
      <b-form-group class="col" label="Street name">
        <b-input v-model="streetName" required />
      </b-form-group>
    </b-form-row>
    <b-form-row>
      <b-form-group class="col" label="Unit/Apt. number">
        <b-input v-model="streetDetail" />
      </b-form-group>
    </b-form-row>

    <b-form-row>
      <b-form-group class="col" label="City">
        <b-input v-model="city" required />
      </b-form-group>
      <b-form-group class="col" label="State/Province">
        <b-input v-model="state" required />
      </b-form-group>
    </b-form-row>
    <b-form-row>
      <b-form-group class="col" label="Country">
        <b-select v-model="country" required>
          <option
            v-for="country in countryOptions"
            :key="country.code"
            :native-value="country.code"
            :value="country.code"
          >{{ country.name + " (" + country.code + ")" }}</option>
        </b-select>
      </b-form-group>
    </b-form-row>
    <b-button type="submit" squared variant="success">Save address</b-button>
  </b-form>
</template>

<script>
import { http } from "@/axios";
const countryApiUri = "https://restcountries.eu/rest/v2/all";
const defaultCountryCode = "US";

export default {
  name: "NewAddressForm",
  data() {
    return {
      name: "",
      streetNumber: "",
      streetName: "",
      streetDetail: "",
      city: "",
      state: "",
      postalCode: "",
      country: "",
      countryOptions: [],
    };
  },
  mounted() {
    this.fetchCountryList().then(() => (this.country = defaultCountryCode));
  },
  methods: {
    async fetchCountryList() {
      const countries = await fetch(countryApiUri).then((res) => res.json());
      this.countryOptions = countries
        .sort((a, b) => a.name - b.name)
        .map((json) => {
          return { name: json.name, code: json.alpha2Code };
        });
    },
    async onFormSubmit() {
      console.log(this);
    },
  },
};
</script>