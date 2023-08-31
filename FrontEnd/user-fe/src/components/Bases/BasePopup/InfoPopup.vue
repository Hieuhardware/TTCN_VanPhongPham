<template>
<div class="popup-container">
        <div class="popup-bg"></div>
    <div class="m-popup info-popup">
        <div class="popup-content">
            <i class="fas fa-exclamation"></i>
            <div class="popup-text">{{title}}</div>
        </div>
        <div class="popup-footer">
            <button class="button button-secondary" @click="$emit('close')">Không</button>
            <button class="button"  @click="$emit('yes')">Có</button>
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