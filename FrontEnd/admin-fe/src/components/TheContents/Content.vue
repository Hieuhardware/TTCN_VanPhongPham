<template>
    <div class="content">
        <Header></Header>
        <router-view></router-view>
    </div>
</template>
<script>
import Header from './Header'
export default {
    components:{
        Header,
    }
}
</script>
<style scoped>
.content{
    width: 100%;
}
</style>