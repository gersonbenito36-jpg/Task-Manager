<script setup>
import { ref } from 'vue'

const emit = defineEmits(['cerrar', 'guardar'])
const props = defineProps(['tarea', 'categorias'])

const fechaFin = ref((props.tarea?.FechaLimite ?? props.tarea?.fechaLimite ?? '').slice(0, 10));
const titulo = ref(props.tarea?.Titulo ?? props.tarea?.titulo ?? '')
const descripcion = ref(props.tarea?.Descripcion ?? props.tarea?.descripcion ?? '')
const categoriaId = ref(props.tarea?.CategoriaId ?? props.tarea?.categoriaId ?? '')
const estado = ref(props.tarea?.Estado ?? props.tarea?.estado ?? 'Pendiente')
const prioridad = ref(props.tarea?.Prioridad ?? props.tarea?.prioridad ?? 'Media')

function enviar() {
  const valCat = Number(categoriaId.value)

  // Validar que el usuario haya seleccionado una categoría válida
  if (!valCat || valCat <= 0) {
    alert('Por favor selecciona una categoría válida.')
    return
  }

  const tareaAGuardar = {
    FechaLimite: fechaFin.value,
    Titulo: titulo.value,
    Descripcion: descripcion.value,
    CategoriaId: valCat,
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
            <input type="date" id="fechaFin" name="fechaFin" v-model="fechaFin" required>

            <label for="titulo">Titulo</label>
            <input type="text" id="titulo" name="titulo" required placeholder="Titulo de la tarea" v-model="titulo">

            <label for="descripcion">Descripción</label>
            <textarea name="descripcion" id="descripcion" required placeholder="Descripcion de tarea" v-model="descripcion"></textarea>

            <label for="categoria">Categoría</label>
            <select name="categoria" id="categoria" v-model="categoriaId" required>
                <option value="" disabled selected>Seleccione una categoría</option>
                <!-- Usamos (cat.id || cat.Id) para garantizar que encuentre el ID sin importar si la API responde en minúscula o mayúscula -->
                <option v-for="cat in categorias" :key="cat.id || cat.Id" :value="cat.id || cat.Id">
                    {{ cat.nombre || cat.Nombre }}
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
                <option value="Media">Intermedio</option>
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
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center; /* Centra el formulario en la pantalla */
    background-color: rgba(0, 0, 0, 0.7);
    padding: 20px; /* Evita que toque los bordes en pantallas pequeñas */
    box-sizing: border-box;
}



form{
    display: flex;
    flex-direction: column;
    justify-content: center;
    border-radius: 8px;
    border: 2px solid ;
    padding: 20px;
    width: 25%;
    background-color: white;
    border: 2px solid #4F46E5;
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
    padding: 10px 14px;
    border-radius: 27px;
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
    border: 2px solid #4F46E5;
}
textarea{
    border: 2px solid #4F46E5;
}
select{
    border: 2px solid #4F46E5;
}
textarea{
    padding: 20px;
    border-radius: 8px;
}
input:focus{
    outline: none;
}
textarea:focus{
    outline: none;
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
    background-color: rgb(221, 45, 45);
    color: white;
    border: none;

}
.guardar{
    background-color: rgb(20, 20, 252);
    color: white;
    border: none;
}
.cancelar:hover{
    background-color: red;
    color: white;
    
}
.guardar:hover{
    background-color: rgb(1, 1, 216);
    color: white;
    
}
select{
    padding: 8px;
    border-radius: 8px;
}
h3{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    margin-left: 10px;
}


</style>