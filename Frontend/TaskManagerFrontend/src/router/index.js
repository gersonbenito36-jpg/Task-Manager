import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue'
import DashboardView from '@/views/DashboardView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/login',
    },
    {
      path: '/login',
      name: 'LoginView',
      component: LoginView,
    },
    {
      path: '/registro',
      name: 'RegistroView',
      component: () => import('../views/RegistroView.vue'),
    },
    {
      path: '/tareas',
      component: DashboardView,
      children: [
        {
          path: '',
          name: '/TareasView',
          component: () => import('../views/TareasView.vue'),
        },
      ],
    },
    {
      path: '/categorias',
      component: DashboardView,
      children: [
        {
          path: '',
          name: 'CategoriasView',
          component: () => import('../views/CategoriasView.vue'),
        },
      ],
    },
  ],
})


router.beforeEach((to) =>{
  const token = localStorage.getItem('token') // la variable token guarda el token cuandos e inicia sesión
  const rutasPublicas = ['/login', '/registro'] // la variable rutas publicas , guarda las rutas que cualquiera puede ver sin haber iniciado sesión
  if(!token && !rutasPublicas.includes(to.path)){
    return '/login'
  }
})

export default router