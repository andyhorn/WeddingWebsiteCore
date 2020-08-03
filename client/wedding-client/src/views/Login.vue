<template>
  <div class="container py-5">
    <LoginForm @login="onLogin" />
  </div>
</template>

<script>
import LoginForm from "@/components/Login/LoginForm";

export default {
  name: "Login",
  components: {
    LoginForm
  },
  methods: {
    async onLogin(formData) {
      const authenticated = await this.$store.dispatch("login", formData);

      if (authenticated) {
        this.$router.push({ name: "Admin" });
      }
    }
  }
};
</script>