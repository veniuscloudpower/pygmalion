<template>
  <div class="container-fluid p-0">
      <div class="row m-0">

        <div class="col-12 p-0">
          <div class="login-card">
            <div>

              <div class="login-main login-form-card">
                <div>
                  <a class="logo text-left">
                    <img
                        class="img-fluid for-light"
                        src="../assets/images/logo/login.png"
                        alt="looginpage"
                    />
                  </a>
                </div>
        <Form @submit="handleLogin" >
          <h4>Sign in to account</h4>

          <div class="form-group">
            <label for="username">Username</label>
            <input name="username" id="username" v-model="username"  type="text" class="form-control" />
            <span name="username" class="error-feedback" />
          </div>
          <div class="form-group">
            <label for="password">Password</label>
            <input name="password" id="password" v-model="password"  type="password" class="form-control" />
            <span name="password" class="error-feedback" />
          </div>

          <div class="form-group">
            <button class="btn btn-primary btn-block" :disabled="loading">
            <span
                v-show="loading"
                class="spinner-border spinner-border-sm"
            ></span>
              <span>Login</span>
            </button>
          </div>

          <div class="form-group">
            <div v-if="message" class="alert alert-danger" role="alert">
              {{ message }}
            </div>
          </div>
        </Form>
      </div>
    </div>
    </div>
  </div>
  </div>
  </div>

</template>

<script>



export default {
  name: "Login",

  components: {
  },
  data() {


    return {
      loading: false,
      message: "",
      username:"",
      password:""
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  },
  created() {
    if (this.loggedIn) {
      this.$router.push("/dashboard");
    }
  },
  methods: {
    handleLogin(e) {
      e.preventDefault()
      this.loading = true

      this.$store.dispatch("auth/login",{ username: this.username,password:this.password}).then(
          () => {
            this.$router.push("/dashboard")
          },
          (error) => {
            this.loading = false;
            this.message =
                (error.response &&
                    error.response.data &&
                    error.response.data.message) ||
                error.message ||
                error.toString();
          }
      );
    },
  },
};
</script>

<style scoped>


</style>