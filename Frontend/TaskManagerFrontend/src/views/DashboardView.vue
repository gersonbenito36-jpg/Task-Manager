<script setup>
</script>   

<template>
    <header>
       <div style="padding: 20px; background-color: white;"></div>
       <nav style="display: flex; flex-direction: row; width: 100%; justify-content: center; box-shadow:  0 8px 8px rgba(0, 0, 0, 0.3);">
        <div style="background-color: white; width: 33.3%; display: flex; flex-direction: row;  justify-content: flex-end; align-items: center;">
            <div class="logo-box">
                <span>✓</span>
            </div>
            <div class="taskManager">
                <h3>TaskManage</h3>
            </div>
        </div>
        <div style="background-color: white; width: 33.3%; display: flex; justify-content:center; gap: 20px; align-items: center;">
            <RouterLink to="/tareas" style="font-weight: bolder; color: black; text-decoration: none; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;  border-radius: 8px;">Tareas</RouterLink>
            <RouterLink to="/categorias" style="font-weight: bolder; color: black; text-decoration: none; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; ">Categorias</RouterLink>
        </div>
        <div style="background-color: white; width: 33.3%; display: flex; flex-direction:  row; justify-content: flex-start; gap: 20px; align-items: center;">
            
                <h3 style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: medium; ">Nombre usuario</h3>
                <button style="padding: 6px 16px; cursor: pointer; border-radius: 12px; margin-right: 20px; margin-bottom: 6px; font-weight: bold; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; background-color: rgb(250, 30, 0); color: white; border: none;">Salir</button>
        </div>
       </nav>
    </header>
    <main>
        <RouterView />
    </main>
</template>

<style scoped>
.logo-box{
   display: flex;
   justify-content: center;
   align-items: center;
   background-color: #4F46E5;
   color: rgb(247, 244, 244);
   margin-left: 20px;
   padding: 0;
   width: 40px;
   height: 30px;
   border-radius: 10px;
}
span{
    font-size: larger;
    color: white;
    font-weight: bolder;
}
.taskManager{
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: bolder;
    justify-content: center;
    text-align: center;
    font-size: small;
    margin-top: 6px;
    margin-left: 10px;
}

</style>