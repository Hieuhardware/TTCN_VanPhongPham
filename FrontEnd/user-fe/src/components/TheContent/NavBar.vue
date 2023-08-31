<template>
    <div class="navbar"
    data-aos="flip-left"
     data-aos-easing="ease-out-cubic"
     data-aos-duration="1000">
        <div class="navbar__title">
            <i class="fas fa-bars" ></i>
            <span style="font-size:20px; font-weight:500"
            data-aos="fade-right"
            data-aos-duration="2000">DANH MỤC SẢN PHẨM</span>
        </div>
        <div class="navbar__item" v-for="item in list" :key="item.id" @click="$emit('changeCategory',item)"
        data-aos="fade-left"
        data-aos-duration="1500">{{item.name}}</div>
    </div>
</template>
<script>
import api from '@/js/api'
export default {
    data() {
        return {
            list:[]
        }
    },
    created(){
        this.getItems();
    },
    methods:{
        getItems(){
            this.$axios.get(api.CategoryApi).then(res=> {
                this.list = res.data;
            });
        }
    }
}
</script>
<style scoped>
.navbar{
    min-width: 300px;
    border: 1px solid #ccc;
    border-radius: 8px;
    background: #fff;
    height: 100%;
}
.navbar__title{
    display: flex;
    gap: 12px;
    height: 50px;
    padding: 0 24px;
    align-items: center;
}
.navbar__item{
    display: block;
    width: 100%;
    height: 50px;
    line-height: 50px;
    text-align: left;
    padding: 0 24px;
    border-top: 1px solid #ccc;
    transition: all linear 1s;
    cursor: pointer;
}
.navbar__item:hover{
    background: #eee;
}
</style>