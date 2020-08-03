<template>
  <b-form @submit.prevent="onSubmit">
    <b-form-group id="email-input-group" label="Email address" label-for="email-address">
      <b-form-input
        type="email"
        id="email-address"
        v-model="email"
        placeholder="Enter email address"
        required
      />
    </b-form-group>
    <b-form-group id="password-input-group" label="Password" label-for="password">
      <b-form-input
        type="password"
        id="password"
        v-model="password"
        placeholder="Enter password"
        required
      />
    </b-form-group>
    <b-button type="submit" variant="success" class="mx-1">Login</b-button>
    <b-button type="reset" variant="warning" class="mx-1">Clear</b-button>
  </b-form>
</template>

<script>
export default {
  name: "LoginForm",
  data() {
    return {
      email: "",
      password: ""
    };
  },
  methods: {
    onSubmit() {
      const formData = {
        email: this.email,
        password: this.password
      };

      this.$emit("login", formData);
    }
  }
};
</script>