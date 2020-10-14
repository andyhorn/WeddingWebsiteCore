<template>
  <div class="container py-5">
    <LoginForm @login="onLogin" />
  </div>
</template>

<script>
import LoginForm from "@/components/Login/LoginForm";
import { ACTIONS } from "@/store";
const Toast = require("@/helpers/toast");

export default {
  name: "Login",
  components: {
    LoginForm
  },
  methods: {
    async onLogin(formData) {
      const authenticated = await this.$store.dispatch(ACTIONS.AUTHENTICATION_ACTIONS.LOGIN, formData);
      if (authenticated) {
        Toast.success(this, "Authenticated");
        this.$router.push({ name: "Home" });
      } else {
        Toast.failure(this, "Login failed");
      }
    }
  }
};
</script>