<template>
    <div class="main-content">
            <NavBar @changeCategory="changeCategory"></NavBar>
            <div class="view">
                <ListProduct ref="listProduct" v-if="!isDetail" :title="categoryName" :totalItems="totalItems" :items="items" @pageChange="pageChange" @viewDetail="viewDetail"></ListProduct>
                <ProductDetail v-else :item="item"></ProductDetail>
            </div>
    </div>
</template>
<script>
import NavBar from '@/components/TheContent/NavBar'
import ListProduct from '@/components/Bases/ListProduct'
import ProductDetail from '@/view/ProductDetail'
import api from '@/js/api'
export default {
    components:{
        NavBar, 
        ProductDetail,
        ListProduct
    },
    data() {
        return {
            items:[],
            item:{},
            isDetail:false,
            categoryName:"",
            category:1,
            totalItems: 0,
        }
    },
    created(){
        this.getData();
    },
    methods:{
        async getData(){
            await this.$axios.get(`${api.ProductApi}/pagination`,{params:{
                id:this.category,
                PageNumber:1,
                }}).then(res =>{
            this.items = res.data.data;
            console.log(res.data);
            this.totalItems = res.data.totalRecord;
        });
        await this.$axios.get(`${api.CategoryApi}/${this.category}`).then(res => this.categoryName= res.data.name);
        },
        async changeCategory(value){
            this.category = value.id;
            this.isDetail =false;
            await this.$axios.get(`${api.ProductApi}/pagination`,{params:{
                id:value.id,
                PageNumber:1,
                }}).then(res =>{
            this.items = res.data.data;
            this.totalItems = res.data.totalRecord;
            this.$refs.listProduct.resetPage();
        });
        await this.$axios.get(`${api.CategoryApi}/${this.category}`).then(res => this.categoryName= res.data.name);
        },
        async pageChange(value){
            await this.$axios.get(`${api.ProductApi}/pagination`,{params:{
                id:this.category,
                PageNumber:value,
                }}).then(res =>{
            this.items = res.data.data;
            console.log(res.data);
            this.totalItems = res.data.totalRecord;
            window.scrollTo({ top: 0, behavior: "smooth" });
        });
        },
        viewDetail(value){
            this.isDetail =true;
            this.item = value;
        }
    }
}
</script>
<style scoped>
.main-content{
    display: flex;
    gap: 12px;
    padding: 24px 48px;
    background: #eee;
}
.view{
    width: 100%;
    overflow: hidden;
}
</style>