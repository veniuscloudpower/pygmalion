<template>
  <div class="container">
    <header class="jumbotron">
      <h3>
        <strong>{{currentUser.username}}</strong> Profile
      </h3>
    </header>
    <p>
      <strong>Token:</strong>
      {{currentUser.token.substr(0, 20)}}
    </p>
    <p>
      <strong>Id:</strong>
      {{currentUser.id}}
    </p>
    <p>
      <strong>Email:</strong>
      {{currentUser.email}}
    </p>
    <strong>Authorities:</strong>
    <ul>
      <li v-for="role in currentUser.roles" :key="role">{{role}}</li>
    </ul>
    <div class="row">
      <div class="col-md-3">
        <div class="card">
          <div class="mobile-clock-widget">
            <div class="bg-svg">

            </div>
            <div>
              <ul class="clock" id="clock">
                <li class="hour" id="hour"></li>
                <li class="min" id="min"></li>
                <li class="sec" id="sec"></li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>

  </div>





</template>

<script>
export default {
  name: 'HomePage',
  data()
  {
    return{
      clockset:{
        sec:0,
        min:0,
        hour:0,
        date:null,
        month:null,
        year:null,
      },
      months:['January','February','March','April','May','June','July','August','September','October','November','December']
    }
  },
  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  },
  methods:{
    clock:function(){
      var cd = new Date();

      this.clockset.hour = 30 * (cd.getHours() % 12 + cd.getMinutes() / 60);
      this.clockset.min = 6 * cd.getMinutes();
      this.clockset.sec = 6 * cd.getSeconds();

      this.clockset.date = cd.getDate();
      this.clockset.month = this.months[cd.getMonth()];
      this.clockset.year = cd.getFullYear();

      document.getElementById('sec').style.cssText="-webkit-transform:rotate("+this.clockset.sec+"deg);";
      document.getElementById('min').style.cssText="-webkit-transform:rotate("+this.clockset.min+"deg);";
      document.getElementById('hour').style.cssText="-webkit-transform:rotate("+this.clockset.hour+"deg);";

      setTimeout(this.clock, 1000)
    }
  },
  mounted() {
    if (!this.currentUser) {
      this.$router.push('/login');
    }
    else {
      this.clock()
    }
  }
};
</script>

<style scoped>
.container{
  padding-top: 20px;
}
</style>