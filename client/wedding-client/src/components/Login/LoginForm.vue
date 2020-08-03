<template>
  <form @submit.prevent="onSubmit">
    <b-form-group id="email-input-group" label="Email address" label-for="email-address">
      <b-form-input
        type="email"
        id="email-address"
        v-model="email"
        placeholder="Enter email address"
      />
    </b-form-group>
    <b-form-group id="password-input-group" label="Password" label-for="password">
      <b-form-input type="password" id="password" v-model="password" placeholder="Enter password" />
    </b-form-group>
    <b-form-group>
      <b-form-input type="submit" value="Login" class="btn btn-success" />
    </b-form-group>
  </form>
</template>

<script>
export default {
  name: "LoginForm",
  data() {
    return {
      email: "",
      password: ""
    };
  }
};
</script>