import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/HomePage.vue';
import LoginForm from '../components/LoginForm.vue';
import AdministradorDashBoard from '../components/AdministradorDashBoard.vue';
import UserDashboard from '../components/UserDashBoard.vue';
import NotFound from '../views/NotFound.vue';
import BookList from '../components/BookList.vue';
import BookDetails from '../components/BookDetails.vue';
import UserList from '../components/UserList.vue';
import store from '../store';

const routes = [
  { path: '/', redirect: '/home' },
  { path: '/home', component: Home, meta: { requiresAuth: true } },
  { path: '/admin', component: AdministradorDashBoard, meta: { requiresAuth: true, isAdmin: true } },
  { path: '/user', component: UserDashboard, meta: { requiresAuth: true } },
  { path: '/livros', component: BookList, meta: { requiresAuth: true } },
  { path: '/livros/:id', name: 'BookDetail', component: BookDetails, meta: { requiresAuth: true } },
  { path: '/admin/usuarios', component: UserList, meta: { requiresAuth: true, isAdmin: true } },
  { path: '/login', component: LoginForm },
  { path: '/:catchAll(.*)', component: NotFound }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = store.getters.isAuthenticated;
  const isAdmin = store.getters.isAdmin;

  if (to.meta.requiresAuth && !isAuthenticated) {
    next({ path: '/login' });
  } else if (to.meta.isAdmin && !isAdmin) {
    next({ path: '/home' });
  } else if (to.path === '/login' && isAuthenticated) {
    next({ path: '/home' });
  } else {
    next();
  }
});

export default router;