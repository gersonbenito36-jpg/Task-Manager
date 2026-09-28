<script setup>
import { ref } from 'vue';
import CrearCategoria from '@/components/CrearCategoria.vue';
import Eliminar from '@/components/Eliminar.vue';
import { onMounted } from 'vue';

const modalCategoria = ref(false);
const modalEliminar = ref(false);

const categoriaSeleccionada = ref(null)
const categoriaEditarSeleccionada = ref(null)
const categorias = ref([]);

function abrirCreacion() {
    categoriaEditarSeleccionada.value = null
    modalCategoria.value = true
}

function abrirEdicion(categoria) {
    categoriaEditarSeleccionada.value = categoria
    modalCategoria.value = true
}

function abrirModalEliminar(categoria) {
    categoriaSeleccionada.value = categoria
    modalEliminar.value = true
}

async function guardarCategoria(categoriaAGuardar) {
    const token = localStorage.getItem('token')
    const esEdicion = categoriaEditarSeleccionada.value !== null

    const url = esEdicion
        ? `http://localhost:5111/api/categorias/editarCategoria/${categoriaEditarSeleccionada.value.categoriaId}`
        : 'http://localhost:5111/api/categorias/crearCategoria'

    const metodo = esEdicion ? 'PUT' : 'POST'

    try {
        const respuesta = await fetch(url, {
            method: metodo,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(categoriaAGuardar)
        })

        if (!respuesta.ok) {
            const detalleError = await respuesta.text()
            console.error('Error al guardar la categoría:', respuesta.status, detalleError)
            return
        }

        modalCategoria.value = false

    } catch (error) {
        console.error('No se pudo conectar con el servidor', error)
    }
}

async function eliminarCategoria() {
    const token = localStorage.getItem('token')
    const categoriaId = categoriaSeleccionada.value.id
    try{
        const respuesta = await fetch(`http://localhost:5111/api/categorias/eliminarCategoria/${categoriaId}`, {
            method: 'DELETE',
            headers:{
                'Authorization' : `Bearer ${token}`
            }
        })
        if(!respuesta.ok){
            const detalleError = await respuesta.text()
            console.error("Error al eliminar categoria", respuesta.status, detalleError)
            return
        }

        modalEliminar.value = false;
        cargarCategorias()
    } catch(error){
        console.error("No se pudo conectar con el servidor", error)
    }
}




async function cargarCategorias() {
    const token = localStorage.getItem('token')
    try {
        const respuesta = await fetch('http://localhost:5111/api/categorias', {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
        if (!respuesta.ok) {
            console.error('Error al cargar categorías')
            return
        }
        categorias.value = await respuesta.json()
    } catch (error) {
        console.error('No se pudo conectar con el servidor', error)
    }
}

onMounted(() => {
    cargarCategorias()
})
</script>

<template>
    <div class="cabecera">
        <div class="header-categoria">
            <h2>Categorías registradas</h2>
            <p>Organiza tus tareas por tipo</p>
        </div>
        <div class="masCategorias">
            <button class="nuevaCategoria" @click="abrirCreacion">+Categoría</button>
        </div>
    </div>

    <table class="tabla-categorias">
        <thead>
            <tr>
                <th>Nombre</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="categoria in categorias" :key="categoria.categoriaId">
                <td>{{ categoria.nombre }}</td>
                <td class="botones">
                    <button class="editar" @click="abrirEdicion(categoria)">Editar</button>
                    <button class="eliminar" @click="abrirModalEliminar(categoria)">Eliminar</button>
                </td>
            </tr>
        </tbody>
    </table>

    <CrearCategoria
        v-if="modalCategoria"
        :categoria="categoriaEditarSeleccionada"
        @cerrar="modalCategoria = false"
        @guardar="guardarCategoria"
    />
    <Eliminar
        v-if="modalEliminar"
        :tarea="categoriaSeleccionada"
        @cerrar="modalEliminar = false"
        @confirmar="eliminarCategoria"
    />
</template>

<style scoped>
.cabecera {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 20px;
}

.header-categoria h2 {
    margin: 0;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
}

.header-categoria p {
    margin: 4px 0 0;
    color: gray;
}

.nuevaCategoria {
    background-color: #4F46E5;
    color: white;
    border: none;
    border-radius: 8px;
    padding: 10px 18px;
    font-weight: bold;
    cursor: pointer;
}
.nuevaCategoria:hover{
    box-shadow: 0 4px 8px black;
}

.tabla-categorias {
    width: 100%;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
}

.tabla-categorias thead {
    background-color: #4F46E5;
    color: white;
    font-weight: bold;
    box-shadow: 0 8 16px rgb(0, 0, 0, 0.3);
}

.tabla-categorias th,
.tabla-categorias td {
    padding: 12px 16px;
    text-align: left;
    border-bottom: 1px dotted #4F46E5;
    box-shadow: 0 10px 16px rgb(0, 0, 0, 0.4);
}

.tabla-categorias tbody tr:hover {
    background-color: rgb(246, 239, 253);
}

.botones {
    display: flex;
    gap: 8px;
}

.botones button {
    border-radius: 16px;
    cursor: pointer;
    padding: 8px 14px;
}
.editar{
    background-color: rgb(3, 139, 3);
    font-weight: bold;
    border: none;
    color: white;
}
.eliminar{
    background-color: rgb(211, 17, 17);
    font-weight: bold;
    border: none;
    color: white;
}
.editar:hover{
    box-shadow: 0 4px 8px black;
    color: black;
}
.eliminar:hover{
    box-shadow: 0 4px 8px black;
    color: black;
}
</style>