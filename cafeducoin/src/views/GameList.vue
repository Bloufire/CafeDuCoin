<template>
    <div class="game-list-page">
        <GameList :games="games" />
    </div>
</template>

<script>
// Import the GameListComponent component
import GameList from '../components/GameListComponent.vue';
// Import the axios instance configured with base URL and interceptors
import axios from '../services/axios';

export default {
    components: {
        GameList
    },
    data() {
        return {
            games: []
        };
    },
    async created() {
        try {
            // Fetch the list of games from the server
            const response = await axios.get('/games');
            this.games = response.data;
        } catch (error) {
            console.error(error);
        }
    }
};
</script>

<style>
/* Styles for game list page component */
</style>