<script setup>
import { ref } from 'vue'

const emit = defineEmits(['cerrar', 'guardar'])
const props = defineProps(['tarea', 'categorias'])

const fechaFin = ref(props.tarea?.FechaLimite ?? '')
const titulo = ref(props.tarea?.Titulo ?? '')
const descripcion = ref(props.tarea?.Descripcion ?? '')
const categoriaId = ref(props.tarea?.CategoriaId ?? '')
const estado = ref(props.tarea?.Estado ?? 'Pendiente')
const prioridad = ref(props.tarea?.Prioridad ?? 'Media')

function enviar() {
  const tareaAGuardar = {
    FechaLimite: fechaFin.value,
    Titulo: titulo.value,
    Descripcion: descripcion.value,
    CategoriaId: Number(categoriaId.value),
    Estado: estado.value,
    Prioridad: prioridad.value
  }
  emit('guardar', tareaAGuardar)
}
</script>

<template>
<div class="editar-container">
    <form @submit.prevent="enviar">
        <div class="check-box">
            <span>✓</span>
            <h3>Task Manager</h3>
        </div>

        <div class="information">
            <label for="fechaFin">Ingrese fecha plazo</label>
            <input type="date" id="fechaFin" name="fechaFin" v-model="fechaFin">

            <label for="titulo">Titulo</label>
            <input type="text" id="titulo" name="titulo" required placeholder="Titulo de la tarea" v-model="titulo">

            <label for="descripcion">Descripción</label>
            <textarea name="descripcion" id="descripcion" required placeholder="Descripcion de tarea" v-model="descripcion"></textarea>

            <select name="categoria" id="categoria" v-model="categoriaId">
                <option value="" disabled>Seleccione una categoría</option>
                <option v-for="categoria in categorias" :key="categoria.categoriaId" :value="categoria.categoriaId">
                    {{ categoria.nombre }}
                </option>
            </select>

            <label for="estado">Estado</label>
            <select name="estado" id="estado" v-model="estado">
                <option value="Pendiente">Pendiente</option>
                <option value="Finalizado">Finalizado</option>
            </select>

            <label for="prioridad">Prioridad</label>
            <select name="prioridad" id="prioridad" v-model="prioridad">
                <option value="Baja">Baja</option>
                <option value="Media">Media</option>
                <option value="Alta">Alta</option>
            </select>
        </div>
        <div class="botones">
            <button type="button" class="cancelar" @click="emit('cerrar')">Cancelar</button>
            <button type="submit" class="guardar">Guardar</button>
        </div>
    </form>
</div>
</template>


<style scoped>
.editar-container{
    background-color: gray;
    min-width: 100%;
    height: 100vh;
    top: 0%;
    left: 0%;
    display: flex;
    justify-content: center;
    align-items: center;
}
form{
    display: flex;
    flex-direction: column;
    justify-content: center;
    border-radius: 8px;
    border: 2px solid blue;
    padding: 32px;
    width: 25%;
}
.check-box{
    display: flex;
    justify-content: center;
    flex-direction: row;
    align-items: center;
    font-weight: bold;
    width: 100%;
}
span{
    background-color: blue;
    color: white;
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 8px 16px;
    border-radius: 8px;
}
.information{
    display: flex;
    justify-content: center;
    flex-direction: column;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
    width: 100%;
}
label{
    margin-top: 10px;
}
input{
    padding: 12px 4px;
    border-radius: 8px;
}
textarea{
    padding: 20px;
    border-radius: 8px;
}
.botones{
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 10px;
    width: 100%;
}
button{
    margin-top: 16px;
    border-radius: 8px;
    padding: 6px 20px;
    cursor: pointer;
    font-weight: bold;
}
.cancelar{
    background-color: rgb(255, 91, 91);

}
.guardar{
    background-color: rgb(63, 66, 255) ;
}
.cancelar:hover{
    background-color: red;
    color: white;
    font-weight: bold;
}
.guardar:hover{
    background-color: blue;
    color: white;
    font-weight: bold;
}
select{
    padding: 8px;
    border-radius: 8px;
}

</style>