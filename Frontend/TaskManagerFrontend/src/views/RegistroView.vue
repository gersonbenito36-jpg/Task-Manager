<script setup>
import { ref } from 'vue';
import  {useRouter}  from 'vue-router';

const router = useRouter();

const email = ref('');
const password = ref('');
const errorRegistro = ref('');
const mostrarContraseña = ref(false);

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
            <span class="miSpan">✓</span>
        </div>
        <h3>Task Manager</h3>
        <div class="information">
            <label for="email">Correo</label>
            <input type="email" name="email" id="email" placeholder="ejemplo@gmail.com" v-model="email">
            <label for="password">Contraseña</label>
            <input :type="mostrarContraseña ?'text': 'password'" name="password" id="password" placeholder="*********" v-model="password">

            <div class="verContra"><input v-model="mostrarContraseña" type="checkbox" id="check" name="check"><span>ver contraseña</span></div>
            
            <p v-if="errorRegistro" style="color: red;">{{ errorRegistro }}</p>
            
            <button type="submit">Registrarme</button>
        </div>
    </form>
</div>    
</template>


<style scoped>
.registro-container{
    background: linear-gradient(rgb(100, 59, 245),rgb(161, 160, 160),  rgb(100, 59, 245));
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
    border: 1px solid ;
    padding: 48px 48px 25px;
    background-color: rgb(245, 235, 235);
    border-radius: 10px;
    border: 1px solid #4F46E5;
    box-shadow: 0px 4px 10px  #131225;
    background: linear-gradient(#dadae6, #4F46E5, #dadae6);
}
.miSpan{
    color: white;
    background-color:#4F46E5;
    padding: 15px 20px;
    border-radius: 35px;
    font-weight: bolder;
    font-size: larger;
    margin: 0;
    box-shadow: 0 4px 8px black;
    border: 2px solid white;

}
.check-box{
    display: flex;
    justify-content: center;
    align-items: center;
    margin: 0;
}
.information{
    display: flex;
    justify-content: flex-start;
    flex-direction: column;
}
h3{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
}

label{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;    
    margin-top: 10px;
    color: #060436;
}
input{
    padding: 12px;
    width: 90%;
    margin: 4px;
    border-radius: 10px;
    border: 1px solid #4F46E5;
}
input:focus{
    outline: none;
    border: 2px solid #4F46E5;
    box-shadow: 0px 8px 8px black;
}
button{
    background-color: #4F46E5;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bold;
    border-radius: 20px;
    padding: 8px;
    margin-top: 20px;
    margin-bottom: 15px;
    color: white;
    border:2px solid white;
}

button:hover{
    background-color: rgb(253, 253, 255);
    color: #4F46E5;
    border:2px solid #4F46E5;
    cursor: pointer;
}
.verContra{
    display: flex;
    flex-direction: row ;
    align-items: flex-start;
    margin-top: 0px;
    font-size: smaller;
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
}
.verContra input{
    width: 20px;
    box-shadow: none;
}
</style>