<template>
  <b-form @submit.prevent="onFormSubmit">
    <h2>New Address</h2>

    <b-form-group label="Location name">
      <b-input v-model="name" />
    </b-form-group>

    <b-form-row>
      <b-col cols="2">
        <b-form-group label="Street number">
          <b-input v-model="streetNumber" required />
        </b-form-group>
      </b-col>
      <b-col>
        <b-form-group label="Street name">
          <b-input v-model="streetName" required />
        </b-form-group>
      </b-col>
      <b-col cols="3">
        <b-form-group label="Unit/Apt. number">
          <b-input v-model="streetDetail" />
        </b-form-group>
      </b-col>
    </b-form-row>

    <b-form-row>
      <b-col>
        <b-form-group label="City">
          <b-input v-model="city" required />
        </b-form-group>
      </b-col>
      <b-col>
        <b-form-row>
          <b-col>
            <b-form-group label="State/Province">
              <b-input v-model="state" required />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Postal Code">
              <b-input v-model="postalCode" required />
            </b-form-group>
          </b-col>
        </b-form-row>
      </b-col>
    </b-form-row>

    <b-form-group label="Country">
      <b-select v-model="country" required>
        <option
          v-for="country in countryOptions"
          :key="country.code"
          :native-value="country.code"
          :value="country.code"
        >{{ country.name + " (" + country.code + ")" }}</option>
      </b-select>
    </b-form-group>

    <b-button type="submit" squared variant="success">Save address</b-button>
  </b-form>
</template>

<script>
import { http } from "@/axios";
import { ACTIONS } from "@/store";

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
      const addressData = {
        name: this.name,
        streetNumber: this.streetNumber,
        streetName: this.streetName,
        streetDetail: this.streetDetail,
        city: this.city,
        state: this.state,
        postalCode: this.postalCode,
        country: this.country,
      };

      const addressId = await this.$store.dispatch(
        ACTIONS.ADDRESS_ACTIONS.CREATE,
        addressData
      );

      if (addressId) {
        this.clearForm();
        this.$emit("saved", addressId);
      }
    },
    clearForm() {
      this.name = "";
      this.streetNumber = "";
      this.streetName = "";
      this.streetDetail = "";
      this.city = "";
      this.state = "";
      this.postalCode = "";
      this.country = "";
    },
  },
};
</script>