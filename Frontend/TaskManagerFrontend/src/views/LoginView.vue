<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';



const router = useRouter();
const email = ref('');
const password = ref('');
const errorLogin = ref('');

async function iniciarSesion(){
    errorLogin.value = ''

    try {
        const respuesta = await fetch ('http://localhost:5111/api/auth/login',{
            method: 'POST',
            headers:{
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Email : email.value,
                Password: password.value
            })
        })
        if(!respuesta.ok){
            errorLogin.value = 'Correo o contraseña incorrectos'
            return;
        }
        const datos = await respuesta.json()
        localStorage.setItem('token', datos.token)
        router.push('/tareas')
    } catch(error){
        errorLogin.value= 'No se pudo conectar al servidor'
        console.error(error)
    }
}

</script>

<template>
    <div class="login-container">
            <form @submit.prevent="iniciarSesion">
                <div class="logo-box">
                    <span>✓</span>
                </div>
                <div><h3>Task Manager</h3></div>
                    <p>Organiza tus tareas en un solo lugar</p>
                    <label for="correo">Correo electrónico</label>
                    <input v-model="email" type="email" id="correo" placeholder="Correo electrónico">
                    <label for="pasword">Contraseña</label>
                    <input v-model="password" type="password" id="password" placeholder="*********">
                    <div class="password"><span class="check"><input type="checkbox" name="check" id="check">Mostrar contraseña</span></div>
                    <p v-if="errorLogin" style="color: red;">{{ errorLogin }}</p>
                    <button type="submit">Iniciar sesión</button>
                    <div class="registro-form">
                         <RouterLink to="registro">Crear registro</RouterLink>
                    </div>
            </form>
    </div>
</template>

<style scoped>
.login-container{
    background: linear-gradient(90deg, rgb(182, 182, 255), rgb(22, 22, 65));
    margin: 0%;
    width: 100%;
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
}
form{
    display: flex;
    align-items: center;
    flex-direction: column;
    border: 2px dotted #4F46E5;
    background-color: rgb(241, 243, 247);
    border-radius: 12px;
    padding: 48px 48px 25px;
    max-width: 275px;
    box-shadow: 0px 15px 25px rgba(0, 0, 0, 0.55);
}
.logo-box{
    background-color: #4F46E5;
    width: 15%;
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 12px;
    border-radius: 15px;
}
span{
    font-size: larger;
    color: white;
    font-weight: bold;
}
.password{
    display: flex;
    align-items: center;
    justify-content: flex-start;
    font-weight: lighter;
    width: 100%;
    margin-top: 2px;
    margin-top: 1px;
}
.check{
    display: flex;
    align-items: center;
    color: black;
    font-size: smaller;
    font-weight: lighter;
}
.check input{
    width: auto;
}

p{
    margin-top: 1px;
}
label{
    font-size: large;
    align-self: flex-start;
}
input{
    padding: 12px;
    margin: 4px;
    width: 90%;
    background-color: #c6c5d8;
    border-radius: 6px;
    border: none;
}
input:focus{
    outline: none;
    border: 2px solid #4F46E5;
    background-color: white;
}
button{
    background-color: #4F46E5;
    padding: 10px;
    width: 90%;
    margin-top: 16px;
    border-radius: 8px;
    color: white;
    font-weight: bold;
    font-size: medium;
    border: 2px dotted #9c0000;
}
button:hover{
    background-color: rgb(37, 79, 168);
    cursor: pointer;
}
.registro-form{
    color: blue;
    margin-top: 25px;
    margin-bottom: 0;
}
.registro-form:hover{
    font-weight: bolder;
    cursor: pointer;
}

</style>