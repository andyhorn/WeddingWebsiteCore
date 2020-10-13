<template>
  <b-form @submit.prevent="onSubmit">
    <b-form-group id="email-input-group" label="Email address" label-for="email-address">
      <b-form-input
        type="email"
        id="email-address"
        v-model="email"
        placeholder="Enter email address"
        required
        focus
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
    <b-form-group
      id="remember-me-checkbox-group"
      label="Remember me?"
      label-for="remember-me-checkbox"
    >
      <b-form-checkbox id="remember-me-checkbox" v-model="rememberMe" />
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
      password: "",
      rememberMe: false
    };
  },
  methods: {
    onSubmit() {
      const formData = {
        email: this.email,
        password: this.password,
        rememberMe: this.rememberMe
      };

      this.$emit("login", formData);
    }
  }
};
</script>