<script setup>
import { defineEmits } from 'vue';
import { defineProps } from 'vue';
const props = defineProps(['tarea'])
const emit = defineEmits(['cerrar', 'confirmar'])

</script>


<template>
    <div class="container">
        <div class="eliminar-container">
            <div class="eliminar">
                <h4>¿Seguro que quieres eliminar este elemento?</h4>
                <h5>Una vez eliminado ya no podrás recuperarlo</h5>
            </div>
            <div class="botones">
                <button @click="emit('cerrar')" class="cancelar">Cancelar</button>
                <button @click="emit('confirmar')" class="boton-eliminar">Eliminar</button>
            </div>
        </div>
    </div>
</template>

<style scoped>
.container{
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: rgb(131, 128, 128);
}
.eliminar-container{
    background-color: blanchedalmond;
    padding: 30px;
    max-width: 40%;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.6);
}
.eliminar{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
}
.botones{
    display: flex;
    justify-content: center;
    gap: 30px;
}
button{
    padding: 10px 20px;
    border-radius: 12px;
    cursor: pointer;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
}
.cancelar{
    background-color: rgb(180, 198, 248);
    border: 1px dashed blue;
}
.cancelar:hover{
    background-color: rgb(58, 58, 218);
    color: white;
}
.boton-eliminar{
    background-color: rgb(253, 116, 116) ;
    border: 1px dashed red;
}
.boton-eliminar:hover{
    background-color: rgb(248, 23, 23);
    color: white;
}


</style>