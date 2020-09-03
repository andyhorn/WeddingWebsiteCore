<template>
  <div class="container py-5">
    <LoginForm @login="onLogin" />
  </div>
</template>

<script>
import LoginForm from "@/components/Login/LoginForm";
import { ACTIONS } from "@/store";

export default {
  name: "Login",
  components: {
    LoginForm
  },
  methods: {
    async onLogin(formData) {
      const authenticated = await this.$store.dispatch(ACTIONS.AUTHENTICATION_ACTIONS.LOGIN, formData);

      if (authenticated) {
        this.$router.push({ name: "Home" });
      }
    }
  }
};
</script>