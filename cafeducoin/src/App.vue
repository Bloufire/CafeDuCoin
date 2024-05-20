<template>
  <div id="app">
    <HeaderBanner text="Cafe du Coin" />
    <router-view />
  </div>
</template>

<script>
import HeaderBanner from './components/HeaderBanner.vue';

export default {
  components: {
    HeaderBanner
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
