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
      try {
        await this.$store.dispatch("login", formData);
        this.$bvToast.toast("Login successful, redirecting in 3, 2, 1 ...", {
          title: "Logged in!",
          variant: "success"
        });

        const vm = this;
        setTimeout(() => {
          vm.$router.push({ name: "Admin" });
        }, 3000);
      } catch (e) {
        let errorMessage = "";

        if (e.status == 404) {
          errorMessage = "Email not found.";
        } else if (e.status == 401) {
          errorMessage = "Email or password were incorrect - try again.";
        } else {
          errorMessage =
            "Unable to log in. Check your credentials and try again.";
        }

        this.$bvToast.toast(errorMessage, {
          title: "Authentication failed",
          variant: "danger"
        });
      }
    }
  }
};
</script>