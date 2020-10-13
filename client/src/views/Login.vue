<template>
  <div class="container py-5">
    <LoginForm @login="onLogin" />
  </div>
</template>

<script>
import LoginForm from "@/components/Login/LoginForm";
import { ACTIONS } from "@/store";
import { success, failure } from "@/helpers/toast";

export default {
  name: "Login",
  components: {
    LoginForm
  },
  methods: {
    async onLogin(formData) {
      const authenticated = await this.$store.dispatch(ACTIONS.AUTHENTICATION_ACTIONS.LOGIN, formData);
      if (authenticated) {
        success("Authenticated");
        this.$router.push({ name: "Home" });
      } else {
        failure("Login failed");
      }
    }
  }
};
</script>