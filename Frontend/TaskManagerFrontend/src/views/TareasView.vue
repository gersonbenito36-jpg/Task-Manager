<script setup>
import { ref, onMounted } from 'vue';
import Editar from '@/components/Editar.vue';
import Eliminar from '@/components/Eliminar.vue';

const modalEliminar = ref(false);
const modalEditar = ref(false);

const tareaSeleccionada = ref(null)
const tareaEditarSeleccionada = ref(null)

const categorias = ref([])
const tareas = ref([])   

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
        console.log('Categorías:', categorias.value)
    } catch (error) {
        console.error('No se pudo conectar con el servidor', error)
    }
}
// función completa para traer las tareas
async function cargarTareas() {
    const token = localStorage.getItem('token')
    try {
        const respuesta = await fetch('http://localhost:5111/api/tareas', {
            headers: {
                'Content-Type': 'applicatio/JSON',
                'Authorization': `Bearer ${token}`
            }
        })
        if (!respuesta.ok) {
            console.error('Error al cargar tareas')
            return
        }
        tareas.value = await respuesta.json()
    } catch (error) {
        console.error('No se pudo conectar con el servidor', error)
    }
}

onMounted(() => {
    cargarCategorias()
    cargarTareas()   // se llama junto con cargarCategorias
})

function abrirCreacion() {
    tareaEditarSeleccionada.value = null
    modalEditar.value = true
}

function abrirEdicion(tarea) {
    tareaEditarSeleccionada.value = tarea;
    modalEditar.value = true;   
}

function abrirModalEliminar(tarea){
    tareaSeleccionada.value = tarea;
    modalEliminar.value = true;
}

async function guardarTarea(tareaAGuardar) {
    console.error('JSON que se va a mandar:', JSON.stringify(tareaAGuardar))

    const token = localStorage.getItem('token')
    const esEdicion = tareaEditarSeleccionada.value !== null

    const url = esEdicion
        ? `http://localhost:5111/api/tareas/editarTarea/${tareaEditarSeleccionada.value.TareaId}`
        : 'http://localhost:5111/api/tareas/crearTarea'

    const metodo = esEdicion ? 'PUT' : 'POST'

    try {
        const respuesta = await fetch(url, {
            method: metodo,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(tareaAGuardar)
        })

        if (!respuesta.ok) {
            const detalleError = await respuesta.text()
            console.error('Error al guardar la tarea:', respuesta.status, detalleError)
            return
        }

        modalEditar.value = false
        cargarTareas()   // ← NUEVO: refresca la tabla después de guardar

    } catch (error) {
        console.error('No se pudo conectar con el servidor', error)
    }
}
</script>

<template>
    <div class="cabecera">
        <div class="header-task">
            <h2>Tareas registradas</h2>
            <p>Gestiona tu flujo de trabajo de forma eficiente</p>
        </div>
        <div class="masTareas">
            <button class="nuevaTarea" @click="abrirCreacion">+Tarea</button>
        </div>
    </div>
    <table class="tabla-tareas">
        <thead>
            <tr>
                <th>Título</th>
                <th>Descripción</th>
                <th>Categoría</th>
                <th>Estado</th>
                <th>Prioridad</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="tarea in tareas" :key="tarea.tareaId">
                <td>{{ tarea.titulo }}</td>
                <td class="col-descripcion">{{ tarea.descripcion }}</td>
                <td>{{ tarea.categoria?.nombre }}</td>
                <td>{{ tarea.estado }}</td>
                <td>{{ tarea.prioridad }}</td>
                <td class="botones">
                    <button class="editar" @click="abrirEdicion(tarea)">Editar</button>
                    <button class="eliminar" @click="abrirModalEliminar(tarea)">Eliminar</button>
                </td>
            </tr>
        </tbody>
    </table>
    <Editar 
    v-if="modalEditar" 
    :tarea="tareaEditarSeleccionada"
    :categorias="categorias"
    @cerrar="modalEditar = false"
    @guardar="guardarTarea"
    />
    <Eliminar 
    v-if="modalEliminar" 
    :tarea="tareaSeleccionada"
    @cerrar="modalEliminar = false"
    />
</template>

<style scoped>
.cabecera{
    display: flex;
    width: 100%;
    flex-direction: row ;
    margin-left: 40px;
}
.masTareas{
    display: flex;
    justify-content: flex-end;
    width: 50%;
    padding: 0;
    margin: 10px;
}
.nuevaTarea{
    padding: 4px 20px 4px 20px;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: #4F46E5;
    color: white;
    border-radius: 10px;
    margin-left: 40px;
    margin-bottom: 20px;
    margin-top: 20px;
    border: none;
    font-weight: bolder;
    cursor: pointer;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif
}
.nuevaTarea:hover{
    background-color: rgb(35, 35, 87);
    
}
.tabla-tareas {
    width: 100%;
    border-collapse: collapse;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
}

.tabla-tareas thead {
    background-color: #4F46E5;
    color: white;
    font-weight: bold;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
}

.tabla-tareas th,
.tabla-tareas td {
    padding: 12px 16px;
    text-align: left;
    border-bottom: 1px solid #dfdbdb;
}

.tabla-tareas tbody tr:hover {
    background-color: rgb(224, 203, 245);
}

.col-descripcion {
    max-width: 250px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
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
    background-color: rgb(76, 170, 76);
    font-weight: bold;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
}
.eliminar{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
    background-color: rgb(245, 80, 80);
}
.editar:hover{
    background-color: rgb(0, 184, 0);
    color: white;
}
.eliminar:hover{
    background-color: red;
    color: white;
}
.header-task{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bolder;
}
.header-task h2{
    margin-bottom: 0px;
}
</style>