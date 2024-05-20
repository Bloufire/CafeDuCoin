// Import necessary functions from vue-router for creating the router
import { createRouter, createWebHistory } from 'vue-router';

// Import the components to be used in routes
import Login from '../views/LoginForm.vue';
import Register from '../views/RegisterForm.vue';
import GameList from '../views/GameList.vue';
import GameDetail from '../views/GameDetail.vue';

// Define the routes for the application
const routes = [
    { path: '/', redirect: '/login' },
    { path: '/login', component: Login },
    { path: '/register', component: Register },
    { path: '/games', component: GameList },
    { path: '/games/:id', component: GameDetail, props: true }
];

// Create the router instance with web history mode and the defined routes
const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;