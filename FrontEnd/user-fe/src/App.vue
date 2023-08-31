<template>
  <div id="app">
    <Header></Header>
    <Banner></Banner>
    <TopNavBar></TopNavbar>
    <router-view></router-view>
    <Footer></Footer>
  </div>
</template>

<script>
import Header from './components/TheContent/Header.vue'
import Banner from './components/TheContent/Banner.vue'
import TopNavBar from './components/TheContent/TopNavBar.vue'
import Footer from '@/components/TheContent/Footer'

export default {
  name: 'App',
  components: {
    Header, Banner, TopNavBar, Footer,
  }
}
</script>

<style>
@import url("./styles/main.css");
@import url("./assets/fontawesome-5.15.1/css/all.min.css");
#app {
  background: #eee;
}

</style>
