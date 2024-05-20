<template>
  <div class="game-list">
    <div v-for="game in games" :key="game.id" :class="{ 'game-card': true, 'unavailable': game.available === false }">
      <img :src="generateRandomImageUrl()" alt="Game Image">
      <div class="game-details">
        <h2 class="title">{{ game.name }}</h2>
        <p class="description">{{ game.description }}</p>
        <p class="status">Status: {{ game.available ? "Available" : "Unavailable" }}</p>
        <button @click="viewDetails(game.name)" class="detail-button">View Details</button>
      </div>
    </div>
  </div>
</template>

<script>
import { v4 as uuidv4 } from 'uuid';

export default {
  props: {
    games: Array // Array of game objects
  },
  methods: {
    viewDetails(gameName) {
      // Navigate to game detail page using Vue Router
      this.$router.push(`/games/${gameName}`);
    },
    generateRandomImageUrl() {
      const gameId = uuidv4();
      return `https://picsum.photos/200/200/?random=${gameId}`;
    }
  }
};
</script>

<style scoped>
/* Styles for game list component */
.game-list {
  display: flex;
  flex-direction: column;
  align-items: center; /* Center the cards horizontally */
}

.game-card {
  width: 100%;
  max-width: 750px; /* Limit the maximum width of the card */
  display: flex;
  margin-bottom: 20px;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.game-card.unavailable {
  background-color: #ccc; /* Grey background for unavailable games */
}

.game-card img {
  height: auto;
  margin-right: 10px;
}

.game-details {
  flex: 1;
}

.title {
  font-weight: bold;
}

.description {
  margin-bottom: 10px;
}

.status {
  margin-bottom: 10px;
}

.detail-link {
  color: blue;
}
</style>