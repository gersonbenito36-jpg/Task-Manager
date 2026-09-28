<script setup>
import { ref } from 'vue'

const props = defineProps(['categoria'])
const emit = defineEmits(['cerrar', 'guardar'])

const nombre = ref(props.categoria?.Nombre ?? '')

function enviar() {
  const categoriaAGuardar = {
    Nombre: nombre.value
  }
  emit('guardar', categoriaAGuardar)
}
</script>

<template>
<div class="container">
    <div class="categoria-container">
        <form @submit.prevent="enviar">
            <div class="check-box">
                <span>✓</span>
                <h3>Task Manager</h3>
            </div>

            <div class="information">
                <label for="nombre">Nombre de la categoría</label>
                <input type="text" id="nombre" name="nombre" required placeholder="Ej: Trabajo" v-model="nombre">
            </div>

            <div class="botones">
                <button type="button" class="cancelar" @click="emit('cerrar')">Cancelar</button>
                <button type="submit" class="guardar" @click="guardar">Guardar</button>
            </div>
        </form>
    </div>
</div>
</template>

<style scoped>
.container {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: rgba(0, 0, 0, 0.8);
}

.categoria-container {
    background-color: white;
    padding: 40px;
    width: 320px;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    border: 2px solid #4F46E5;
}

.check-box {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 12px;
    margin-bottom: 20px;
}

.check-box span {
    color: white;
    background-color: #4F46E5;
    padding: 6px 10px;
    border-radius: 45px;
    font-weight: bolder;
}
h3{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
}

.information {
    display: flex;
    flex-direction: column;
}

label {
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
    margin-bottom: 6px;
}

input {
    padding: 12px;
    border-radius: 10px;
    border: 1px solid #4F46E5;
    outline: none;
    margin-bottom: 20px;
}

input:hover {
    border: 2px solid #4F46E5;
    box-shadow: 0 4px 6px 0 black;
}

.botones {
    display: flex;
    justify-content: space-between;
}

button {
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
    border-radius: 10px;
    padding: 8px 16px;
    cursor: pointer;
    border: none;
}

.cancelar {
    background-color: rgb(211, 34, 34);
    color: white;
}

.cancelar:hover {
    background-color: red;
    color: white;
    font-weight: bolder;
}

.guardar {
    background-color: blue;
    color: white;
}

.guardar:hover {
    background-color: rgb(0, 0, 155);
    color: white;
}
</style>