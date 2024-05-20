<template>
  <div class="header-banner">
    <h1 class="banner-text">{{ text }}</h1>
  </div>
</template>

<script>
export default {
  props: {
    text: {
      type: String,
      required: true
    }
  }
};
</script>

<style scoped>
.header-banner {
  color: black;
  text-align: center;
  padding: 20px 0;
}

.banner-text {
  font-size: 50px; /* Adjust the font size as needed */
}
</style>
