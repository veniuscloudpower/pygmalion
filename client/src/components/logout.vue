<template>
  <h1>Logout</h1>
</template>


<script>
export default {
name: "Logout",
created() {

this.$store.dispatch("auth/logout").then(
() => {
  this.$router.push("/login")
}
)
}
}

</script>