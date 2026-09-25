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


