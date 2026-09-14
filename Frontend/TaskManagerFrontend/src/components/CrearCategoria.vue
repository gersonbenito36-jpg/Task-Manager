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
                <button type="submit" class="guardar">Guardar</button>
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
    background-color: rgba(0, 0, 0, 0.4);
}

.categoria-container {
    background-color: rgb(245, 235, 235);
    padding: 40px;
    width: 320px;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
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
    background-color: blue;
    padding: 6px 10px;
    border-radius: 8px;
    font-weight: bolder;
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
    border: 1px solid #ddd;
    outline: none;
    margin-bottom: 20px;
}

input:hover {
    border: 1px dashed violet;
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
    background-color: rgb(240, 128, 128);
}

.cancelar:hover {
    background-color: red;
    color: white;
}

.guardar {
    background-color: rgb(154, 154, 255);
}

.guardar:hover {
    background-color: blue;
    color: white;
}
</style>