<template>
  <div class="container" >
    <h1>{{this.$options.name}}</h1>
  </div>
</template>

<script>
export default {
  name: "Remove",
  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  },
  mounted() {
    if (!this.currentUser) {
      this.$router.push('/login');
    }
  }
}
</script>

<style scoped>
.container{
  padding-top: 20px;
}
</style>