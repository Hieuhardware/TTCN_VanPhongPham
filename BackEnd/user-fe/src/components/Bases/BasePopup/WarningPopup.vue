<template>
<div class="popup-container" id="WarningPopup">
        <div class="popup-bg"></div>
    <div class="m-popup error-popup">
        <div class="popup-content">
            <i class="fas fa-exclamation-triangle" style="color:yellow"></i>
            <div class="popup-text">{{title}}</div>
        </div>
        <div class="popup-footer">
            <div></div>
            <button class="m-button" @click="$emit('close')">Đồng ý</button>
        </div>
    </div>
</div>
</template>
<style scoped>
@import url('../../../styles/Bases/popup.css');
</style>
<script>
export default {
    props:['title'],
    components:{
    },
}
</script>