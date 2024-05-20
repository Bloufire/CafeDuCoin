<template>
    <div class="game-list-page">
        <GameList :games="games" />
    </div>
</template>

<script>
import GameList from '../components/GameListComponent.vue';
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