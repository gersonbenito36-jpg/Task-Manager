<script setup>
import router from '@/router';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const email = ref('');
const password = ref('');
const errorRegistro = ref('');

async function registrarse() {
    errorRegistro.value = ''

    try{
        const respuesta = await fetch('http://localhost:5111/api/auth/registro', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Email: email.value,
                Password: password.value
            })
        })

        if(!respuesta.ok){
            errorRegistro.value = 'No se pudo completar el registro. Verifica tus datos'
            return
        }
        router.push('/login')
    } catch(error){
        errorRegistro.value = "No se pudo conectar con el servidor"
        console.error(error)
    }

}

</script>


<template>
<div class="registro-container">
    <form @submit.prevent="registrarse">
        <div class="check-box">
            <span>✓</span>
            <h3>Task Manager</h3>
        </div>
        <div class="information">
            <label for="email">Correo</label>
            <input type="email" name="email" id="email" placeholder="ejemplo@gmail.com" v-model="email">
            <label for="password">Contraseña</label>
            <input type="password" name="password" id="password" placeholder="*********" v-model="password">
            <p v-if="errorRegistro" style="color: red;">{{ errorRegistro }}</p>
            
            <button type="submit">Registrarme</button>
        </div>
    </form>
</div>    
</template>


<style scoped>
.registro-container{
    background-color: gray;
    margin: 0;
    width: 100%;
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
}
form{
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    border: 2px dotted rgb(114, 62, 197);
    padding: 48px 48px 25px;
    background-color: rgb(245, 235, 235);
    border-radius: 10px;
}
.check-box{
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 20px;
    flex-direction: row;
}
.information{
    display: flex;
    justify-content: center;
    flex-direction: column;
}

label{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;    
    margin-top: 10px 
}
input{
    padding: 12px;
    width: 90%;
    margin: 4px;
    border-radius: 10px;
    border: none;
    outline: none;
}
input:hover{
    border: 1px dashed violet;
}
button{
    background-color: rgb(154, 154, 255);
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
    border-radius: 10px;
    padding: 8px;
    margin-top: 20px;
    margin-bottom: 15px;
}
button:hover{
    background-color: blue;
    color: white;
    border:2px solid rgb(15, 238, 15);
}
span{
    color: white;
    background-color: blue;
    padding: 6px 10px;
    border-radius: 8px;
    font-weight: bolder;
}

</style>