import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/LoginForm.vue';
import Register from '../views/RegisterForm.vue';
import GameList from '../views/GameList.vue';
import GameDetail from '../views/GameDetail.vue';

const routes = [
    { path: '/', redirect: '/login' },
    { path: '/login', component: Login },
    { path: '/register', component: Register },
    { path: '/games', component: GameList },
    { path: '/games/:id', component: GameDetail, props: true }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;