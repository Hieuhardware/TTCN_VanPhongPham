<template>
  <div id="app">
    <Login v-if="!hasUser"></Login>
    <NavBar v-if="hasUser"></NavBar>
    <Content v-if="hasUser"></Content>
  </div>
</template>

<script>
import NavBar from './components/TheContents/NavBar'
import Content from './components/TheContents/Content'
import Login from '@/components/TheContents/Login'
import 'vue2-datepicker/index.css';
export default {
  name: 'App',
  components: {
    NavBar,
    Content,
    Login
  },
  data() {
    return {
      hasUser:false,
    }
  },
  computed:{
    getAdmin(){
      return this.$store.state.admin
    }
  },
  watch:{
    getAdmin: function(val){
      if(val){
        this.hasUser = true;
      }
      else{
        this.hasUser =false;
      }
    }
  }
}
</script>

<style>
@import url("./styles/main.css");
@import url("./assets/fontawesome-5.15.1/css/all.min.css");

#app{
  background-color: #F4F6F9;
  display: flex;
}
</style>
