<template>
    <div class="popup-container" >
        <div class="m-popup error-popup">
        <div class="popup-content">
            <i class="fas fa-exclamation-triangle" style="color:red"></i>
            <div class="popup-text">{{title}}</div>    
        </div>
        <div class="popup-footer">
            <div></div>
            <button class="button button-secondary" @click="$emit('close')">Đóng</button>
        </div>
    </div>
    </div>
    
</template>
<style scoped>
@import url('../../../styles/bases/popup.css');
</style>
<script>
export default {
    props:['title'],
    components:{
    },
    data(){
        return{
        }
    }
}
</script>