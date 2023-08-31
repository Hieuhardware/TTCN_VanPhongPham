<template>
    <div class="m-toast-msg">
        <div class="m-toast-content">
            <i class="fas fa-exclamation"></i>
            <div class="m-toast-mesage">{{mesage}}</div>
            <i class="fas fa-times" style="color=red"  @click="$emit('closeToast')"></i>
        </div>
    </div>
</template>
<script>
export default {
    props:["mesage"]
}
</script>
<style scoped>
.m-toast-msg {
    position: fixed;
    right: 20px;
    top: 60px;
    max-width: 300px;
    padding: 4px;
    z-index: 9999;
}

.m-toast-content {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 4px;
    max-width: 250px;
    margin: 4px 0;
    background-color: #eeeeee;
    padding: 4px;
    border: 1px solid #35bf22;
    border-radius: 4px;
}

.m-toast-content .m-toast-mesage {
    color: #35bf22;
    line-height: 24px;
    font-weight: 600;
    margin-left: 4px;
}
.m-toast-content i{
    font-size: 24px;
}
</style>