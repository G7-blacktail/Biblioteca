import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/HomePage.vue';
import LoginForm from '../components/LoginForm..vue';
import AdministradorDashBoard from '../components/AdministradorDashBoard.vue';
import UserDashboard from '../components/UserDashBoard.vue';
import NotFound from '../views/NotFound.vue';
import BookList from '../components/BookList.vue'
import BookDetails from '../components/BookDetails.vue'
import AddBook from '../components/AddBook.vue'
import UserList from '../components/UserList.vue'

const routes = [
  { path: '/', component: Home },
  { path: '/login', component: LoginForm },
  { path: '/admin', component: AdministradorDashBoard, meta: { requiresAuth: true, isAdmin: true } },
  { path: '/user', component: UserDashboard, meta: { requiresAuth: true } },
  { path: '/:catchAll(.*)', component: NotFound },
  { path: '/livros', component: BookList },
  { path: '/livros/:id', name: 'BookDetail', component: BookDetails },
  { path: '/admin/novo-livro', component: AddBook, meta: { requiresAuth: true, isAdmin: true } },
  { path: '/admin/usuarios', component: UserList, meta: { requiresAuth: true, isAdmin: true } }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

// Middleware para proteger rotas
router.beforeEach((to, from, next) => {
  const isAuthenticated = false; 
  const isAdmin = false; 

  if (to.meta.requiresAuth && !isAuthenticated) {
    next({ path: '/login' });
  } else if (to.meta.isAdmin && !isAdmin) {
    next({ path: '/user' });
  } else {
    next();
  }
});

export default router;
