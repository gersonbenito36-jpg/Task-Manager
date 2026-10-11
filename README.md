# Task Manager

Aplicación full-stack de gestión de tareas. Proyecto personal para practicar autenticación con JWT, una API protegida por usuario y consumo de esa API desde una SPA en Vue.

## Funcionalidades

- Registro e inicio de sesión con JWT
- CRUD de tareas (título, descripción, fecha límite, estado, prioridad, categoría)
- CRUD de categorías
- Cada usuario ve y gestiona únicamente sus propias tareas y categorías
- Filtros opcionales por estado y categoría
- Rutas protegidas en el frontend según si hay sesión activa

## Tecnologías

**Backend:** ASP.NET Core Web API, Entity Framework Core, ASP.NET Core Identity, JWT, PostgreSQL (Neon)

**Frontend:** Vue 3 (Composition API), Vue Router, Pinia, Vite, CSS

**Herramientas:** Git, GitHub, VS Code (REST Client para probar la API), `dotnet user-secrets`

##Inicio des sesión
<img width="1365" height="634" alt="Login" src="https://github.com/user-attachments/assets/ababb58c-46d6-45bd-a99a-be9c51352561" />

##Gestión de Tareas
<img width="1364" height="626" alt="Tareas" src="https://github.com/user-attachments/assets/93561436-1815-4b5d-a185-398ee6a24aa9" />

##Gestión de Categorias
<img width="1365" height="633" alt="Categorias" src="https://github.com/user-attachments/assets/b997c450-461a-4cd6-9645-c78d38034393" />




