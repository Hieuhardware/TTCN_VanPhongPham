<template>
    <div class="banner">
        <div class="logo"
        data-aos="zoom-in-right"
        data-aos-duration="1500">
            <img src="../../assets/image/logo.png" width="50" height="50" alt="">
        </div>
        <div class="search-box"
        >
            <div class="search">
                <input class="input" type="search" placeholder="Tìm kiếm..." v-model="search" @input="findProduct()" autocomplete="false">
                <div class="search-result" v-if="search">
                    <div class="searchTitle" v-if="!items.length">Không có kết quả nào trùng khớp</div>
                    <div class="searchTitle" v-else>Hiển thị {{number}}/{{total}} kết quả tìm kiếm trùng khớp.</div>
                    <div @click="closeSearchBox()">
                        <router-link :to="'/detail/' + item.id" class="search-result-item" v-for="item in items" :key="item.id" 
                        data-aos="fade-left"
                        data-aos-easing="ease-out-cubic"
                        data-aos-duration="1500">
                        <img :src="item.image || avatar" alt="">
                        <div class="name">{{item.name}}</div>
                        <div class="price">{{item.price}}đ</div>
                        </router-link>
                    </div>
                    
                </div>
            </div>
            <button class="button">
                <div class="price" @click="closeSearch()">Tìm kiếm</div>
            </button>
        </div>
        
        <ErrorPopup @close="close" :title="title"  v-if="hasError"></ErrorPopup>
    </div>
</template>
<script>
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
import defaultAvatar from "../../assets/image/img_default.jpg"
import api from '@/js/api'
export default {
    components:{
        ErrorPopup
    },
    data() {
        return {
            items:[],
            total:0,
            number:0,
            search:"",
            hasError:false,
            title:"",
            avatar: defaultAvatar
        }
    },
    methods:{
        findProduct(){
            if(this.search){
                this.$axios.get(`${api.ProductApi}/search/${this.search}`).then(res=>{
                    console.log(res.data);
                    this.items= res.data.data;
                    this.number=res.data.data.length;
                    this.total= res.data.total
                })
            }
        },
        closeSearchBox(){
            setTimeout(() => {
                this.search=""
            }, 100);
        },
        closeSearch(){
            if(this.search){
                this.$router.push({path:`/listdetail/${this.search}`})
                setTimeout(() => {
                this.search=""
            }, 100); 
            }
            else{
                this.title ="Vui lòng điền thông tin tìm kiếm."
                this.hasError=true
            }
        },
        close(){
            this.hasError =false
        }
    }
}
</script>
<style scoped>
.banner{
    padding: 0 48px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    z-index: 9997
}
.search-box{
    display: flex;
    gap: 12px;
}
.search{
    position: relative;
    z-index: 9999
}
.input{
    width: 400px;
    padding-left: 60px;
    background-image: url('../../assets/image/magnifier.png');
    background-position: 12px center;
    background-size: 32px 32px;
    background-repeat: no-repeat;
}
.search-result{
    position: absolute;
    top: 100%;
    left: 0;
    width: 100%;
    background: #fff;
    border-radius: 4px;
    z-index: 9999;
}
.searchTitle{
    color: #757e72;
    font-weight: 600;
    font-size: 16px;
    height: 30px;
    line-height: 30px;
    padding: 0 12px;
}
.search-result-item{
    min-height: 40px;
    display: flex;
    gap: 12px;
    align-items: center;
    width: 100%;
    padding: 4px 12px;
    color: #333;
    transition: all linear .6s;
}
.search-result-item:hover{
    background:rgba(148,187,233,1);
    color: #3DAA12;
}
.search-result-item img{
    height: 36px;
    width: 36px;
    border-radius: 100%;
}
.search-result-item .name{
    flex-grow: 1;
    text-overflow: ellipsis;
}
</style>