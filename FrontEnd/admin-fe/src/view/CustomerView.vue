<template>
    <div>
        <div class="view">
            <div class="view-title" 
            data-aos="flip-left"
            data-aos-duration="1000">
                Quản lý tài khoản khách hàng
            </div>
            <div class="card">
                <div class="card-item"
                data-aos="flip-right"
                data-aos-easing="ease-out-cubic"
                data-aos-duration="1200">
                    <div class="card-item-header">
                        <div class="card-itm-header-title" 
                        data-aos="zoom-out-right"
                        data-aos-duration="1500"
                        >Tìm kiếm</div>
                    </div>
                    <div class="card-item-content"
                    data-aos="fade-left"
                    data-aos-duration="1500">
                        <input type="text" class="input" placeholder="Nhập tên tài khoản..." title="Tìm kiếm tài khoản" v-model="search">
                        <button class="button" @click="findCustomer">Tìm kiếm</button>
                        <button class="button button-secondary" @click="refresh">Làm mới</button>
                    </div>
                </div>
            </div>
        </div>
        <BaseView :title="header" :items="items" :tableheader="tableHeader" v-if="!noData" @add="addItem" @edit="editItem" @detail="detailItem" @delete="deleteItem"></BaseView>
        <div class="pagination" v-show="!noData">
            <Pagination ref="pagination" :itemCount="totalItems" :maxDisplayPage="3" :page="page" :perPage="20" @pageChange="pageChange"></Pagination>
        </div>
        <div class="nodata" v-if="noData">
            Không có dữ liệu để hiển thị!
        </div>
        <Loader v-if="hasLoader"></Loader>
        <ErrorPopup @close="close" :title="title"  v-if="hasError"></ErrorPopup>
        <CustomerForm title="khách hàng" :status="status" :item="item" v-if="showForm" @close="closeForm" @success="success"></CustomerForm>
        <ToastMesage :mesage="title" @closeToast="closeToast" v-if="hasToast"></ToastMesage>
        <QuestionPopup :title="title" @close="close" @yes="yes" v-if="hasQuestion"></QuestionPopup>
    </div>
</template>
<script>
import BaseView from '../components/Bases/BaseView'
import Pagination from '@/components/Bases/Pagination'
import Loader from '@/components/Bases/Loader'
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
import api from '@/js/api'
import CustomerForm from '@/view/forms/CustomerForm'
import ToastMesage from '@/components/Bases/ToastMesage'
import QuestionPopup from '@/components/Bases/BasePopup/QuestionPopup'
export default {
    components:{
        BaseView, Pagination, Loader, ErrorPopup, CustomerForm, ToastMesage, QuestionPopup
    },
    data() {
        return {
            tableHeader:[{id:"name",name:"Tên khách hàng"},{id:"email",name:"Tài khoản"},{id:"tel",name:"Số điện thoại"}],
            header:"Danh sách tài khoản",
            items:[],
            page:1,
            totalItems:0,
            search:"",
            hasLoader:false,
            hasError:false,
            title: "",
            noData:false,
            showForm: false,
            status:0,
            item:{},
            hasToast:false,
            hasQuestion:false
        }
    },
    created(){
        this.getCustomer();
    },
    methods:{
        closeToast(){
            this.hasToast= false
        },
        success(){
            this.title = "Cập nhật hệ thống thành công."
            this.hasToast = true;
            setTimeout(() => {
                this.hasToast = false
            }, 3000);
            this.page = 1,
            this.search=""
            this.getCustomer();
        },
        closeForm(){
            this.showForm =false
        },
        addItem(){
            this.status=0
            this.item={};
            this.showForm=true;
        },
        editItem(item){
            this.status=1
            this.item = item
            this.showForm=true;
        },
        detailItem(item){
            this.status=2
            this.item = item
            this.showForm=true;
        },
        deleteItem(item){
            this.item = item;
            this.title = `Bạn có thật sự muốn xóa khách hàng này không.`
            this.hasQuestion = true;
        },
        yes(){
            this.$axios.delete(`${api.CustomerApi}/${this.item.id}`).then(()=>{
                this.title = "Cập nhật hệ thống thành công."
                this.hasToast = true;
                setTimeout(() => {
                    this.hasToast = false
                }, 3000);
                this.hasQuestion= false;
                this.page = 1,
                this.search=""
                this.getCustomer();
            })
            .catch(()=>{
                this.hasQuestion=false
                this.title = "Có lỗi xảy ra, vui lòng kiểm tra lại."
                this.hasError = true;
            })
        },
        getCustomer(){
            let param={
                PageNumber:this.page,
                filter: this.search
            }
            this.hasLoader = true
            this.$axios.get(`${api.CustomerApi}/pagination`,{params:param}).then(res=>{
                console.log(res.data);
                this.items=res.data.data;
                this.totalItems=res.data.totalRecord;
                if(this.totalItems == 0){
                    this.noData = true;
                }
                else{
                    this.noData=false
                }
            }).finally(()=>this.hasLoader=false)
        },
        pageChange(value){
            this.page = value;
            this.getCustomer();
            window.scrollTo({ top: 0, behavior: "smooth" });
        },
        async findCustomer(){
            this.$refs.pagination.resetPage();
            this.page=1
            this.getCustomer();
            setTimeout(() => {
                this.header= `Có ${this.totalItems} kết quả tìm kiếm trùng khớp`
            }, 100);
        },
        refresh(){
            this.header="Danh sách khách hàng";
            this.noData =false;
            this.$refs.pagination.resetPage();
            this.page=1
            this.search="";
            this.getCustomer();
        },

        close(){
            this.hasError=false
            this.hasQuestion= false
        }
    },

}
</script>
<style scoped>
.view{
    width: 100%;
    padding: 12px;
    overflow: hidden;
}
.card{
    width: 100%;
    display: flex;
    flex-direction: column;
    gap: 12px;
}
.view-title{
    font-size: 30px;
    font-weight: 600;
    height: 40px;
    line-height: 40px;
    padding: 0 12px;
}
.card-item{
    background-color: #fff;
    border-radius: 8px;
    padding: 12px;
    box-shadow: 0px 1px 2px rgba(0, 0, 0, 0.1), 0px 1px 9px rgba(0, 0, 0, 0.06),
    0px 3px 5px rgba(0, 0, 0, 0.07);
}
.card-item-header{
    display: flex;
    justify-content: space-between;
    border-bottom: 2px solid #ccc;
    margin-bottom: 8px;
    padding: 0 0 6px 0;
}
.card-itm-header-title{
    height: 40px;
    line-height: 40px;
    font-size: 22px;
    
}
.card-item-content{
    display: flex;
    gap: 12px;
}
.card-item-content input{
    width: 400px;
}
.pagination{
    width: 100%;
    display: flex;
    justify-content: center;
    padding: 24px 0;
}
.nodata{
    width: 100%;
    font-size: 32px;
    font-weight: 600;
    color: red;
    display: flex;
    justify-content: center;
}
</style>