<template>
  <div class="container py-5">
    <LoginForm />
  </div>
</template>

<script>
import LoginForm from "@/components/Login/LoginForm";

export default {
  name: "Login",
  components: {
    LoginForm
  }
};
</script>