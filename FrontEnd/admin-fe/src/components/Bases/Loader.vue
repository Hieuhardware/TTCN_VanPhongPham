<template>
     <div class="m-loader">
        <div class="loader"></div>
    </div>
</template>
<script>
export default {
}
</script>
<style scoped>

/*thiết lập vị trí của loader*/
.m-loader {
    position: fixed;
    inset: 0;
    background-color: rgba(0, 0, 0, 0.2);
}


/* thiết lập loader*/
.loader {
    position: absolute;
    background: url('../../assets/loading.svg') no-repeat;
    width: 32px;
    height: 32px;
    top: 50%;
    right: 50%;
    transform: translate(50% 50%);
    animation: spin 1.5s linear infinite;
} 


/* thiết lập quay của loader*/
@keyframes spin {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
</style>