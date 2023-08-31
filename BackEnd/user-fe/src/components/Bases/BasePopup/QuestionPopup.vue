<template>
<div class="popup-container" id="InfoPupup">
        <div class="popup-bg"></div>
    <div class="m-popup info-popup">
        <div class="popup-content">
            <i class="fas fa-question"></i>
            <div class="popup-text">{{title}}</div>
        </div>
        <div class="popup-footer">
            <button class="button button-secondary" @click="$emit('close')">Hủy</button>
            <div class="list-button">
            <button class="button"  @click="$emit('yes')">Có</button>
            </div>
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