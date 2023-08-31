<template>
    <div class="form-container">
        <div class="form-content">
            <div class="form-header">
                <div class="form-title">{{titleHeader}}</div>
                <div class="form-action">
                    <div class="form-action-icon" v-if="!isAdd">
                        <i class="fas fa-plus" :class="{active: statusData==0}" @click="statusAdd"></i>
                        <i class="fas fa-edit" :class="{active: statusData==1}" @click="statusEdit"></i>
                        <i class="fas fa-eye" :class="{active: statusData==2}" @click="statusDetail"></i>
                    </div>
                    <div class="form-close">
                        <i class="fas fa-times" @click="$emit('close')"></i>
                    </div>
                </div>
            </div>

            <div class="form-body">
                <div class="form-body-item">
                    <div class="label">Email<span style="color:red"> * </span>:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="customer.email" :disabled="isDetail">
                    </div>
                </div>
                <div class="form-body-item">
                    <div class="label">Mật khẩu<span style="color:red"> * </span>:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="customer.password" :disabled="isDetail">
                    </div>
                </div>
                <div class="form-body-item">
                    <div class="label">Tên khách hàng<span style="color:red"> * </span>:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="customer.name" :disabled="isDetail">
                    </div>
                </div>
                <div class="form-body-item">
                    <div class="label">Số điện thoại:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="customer.tel" :disabled="isDetail">
                    </div>
                </div>
                <div class="form-body-item">
                    <div class="label">Địa chỉ:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="customer.address" :disabled="isDetail">
                    </div>
                </div>
            </div>
            <div class="form-footer">
                <button class="button button-secondary" @click="$emit('close')">Đóng</button>
                <button class="button" v-if="this.statusData == 0" @click="addItem()">Thêm mới</button>
                <button class="button" v-if="this.statusData == 1" @click="updateItem()">Cập nhật</button>
            </div>
        </div>
        <ErrorPopup :title="message" @close="close" v-if="hasError"></ErrorPopup>
    </div>
</template>
<script>
import api from '@/js/api'
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
export default {
    props:["title","status","item"],
    components:{ErrorPopup},
    data() {
        return {
            isAdd:true,
            editorConfig:{},
            isDetail: false,
            data:this.item,
            statusData:this.status,
            customer:{
                id:0,
                email:"",
                password:"",
                name:"",
                tel:"",
                address:""
            },
            message:"",
            hasError: false
        }
    },
    created(){
        if(this.status == 1){
            this.isAdd = true
        }
        if(this.status == 1){
            this.isAdd = false
            this.customer = this.data;
        }
        if(this.status == 2){
            this.isAdd = false
            this.customer = this.data;
            this.isDetail =true
        }
    },
    computed:{
        titleHeader(){
            if(this.statusData ==0){
                return "Thêm " + this.title
            }
            if(this.statusData ==1){
                return "Sửa " + this.title
            }
            if(this.statusData ==2){
                return "Xem chi tiết " + this.title
            }
            return ""
        },

    },
    methods:{
        getNewItem(){
            return {
                id:0,
                email:"",
                password:"",
                name:"",
                tel:"",
                address:""
            }
        },
        statusAdd(){
            this.customer = this.getNewItem();
            this.isDetail = false;
            this.statusData=0;
        },
        statusEdit(){
            this.customer = this.data;
            this.isDetail = false;
            this.statusData=1;
        },
        statusDetail(){
            this.customer = this.data;
            this.isDetail = true;
            this.statusData=2;
        },
        async addItem(){
            if(!this.customer.email){
                this.message= "Email không được để trống."
                this.hasError = true
                return
            }
            if(!(/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.customer.email))){
                this.message= "Email không đúng định dạng."
                this.hasError = true
                return
            }
            if(!this.customer.password){
                this.message= "Mật khẩu không được để trống."
                this.hasError = true
                return
            }
            if(!this.customer.name){
                this.message= "Tên khách hàng không được để trống."
                this.hasError = true
                return
            }
            if(this.customer.tel && !(/^\d{10}$/.test(this.customer.tel))){
                this.message= "Số điện thoại không đúng định dạng."
                this.hasError = true
                return
            }
            let user =await this.$axios.get(`${api.CustomerApi}/${this.customer.email}`).then(res=>res.data)
            if(user){
                    this.message= "Email đã tồn tại trong hệ thống, vui lòng kiểm tra lại."
                    this.hasError = true
                    return
            }
            this.customer.date_create = new Date().toISOString().slice(0, 10)
            this.$axios.post(api.CustomerApi,this.customer).then(() => {
                this.$emit("close");
                this.$emit("success");
            })
            .catch(()=>{
                this.message = "Có lỗi xảy ra, vui lòng kiểm tra lại.";
                this.hasError= true;
            })
        },
        updateItem(){
            if(!this.customer.email){
                this.message= "Email không được để trống."
                this.hasError = true
                return
            }
            if(!(/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.customer.email))){
                this.message= "Email không đúng định dạng."
                this.hasError = true
                return
            }
            if(!this.customer.password){
                this.message= "Mật khẩu không được để trống."
                this.hasError = true
                return
            }
            if(!this.customer.name){
                this.message= "Tên khách hàng không được để trống."
                this.hasError = true
                return
            }
            if(this.customer.tel && !(/^\d{10}$/.test(this.customer.tel))){
                this.message= "Số điện thoại không đúng định dạng."
                this.hasError = true
                return
            }
            this.$axios.put(`${api.CustomerApi}/${this.customer.id}`,this.customer).then(() => {
                this.$emit("close");
                this.$emit("success");
            })
            .catch(()=>{
                this.message = "Có lỗi xảy ra, vui lòng kiểm tra lại.";
                this.hasError= true;
            })
        },
        close(){
            this.hasError = false;
        }
    }
}
</script>
<style scoped>
.form-container{
    position: fixed;
    inset: 0;
    background-color: rgba(0, 0, 0, 0.3);
    display: flex;
    justify-content: center;
    align-items: center;
}
.form-content{
    background: #fff;
    border-radius: 8px;
    min-width: 800px;
    max-width: 60%;
    width: 60%;
    max-height: 600px;
    overflow: auto;
}
.form-header{
    height: 40px;
    display: flex;
    justify-content: space-between;
    padding: 0 12px;
    align-items: center;
    background: #eee;
    border-radius: 8px 8px 0 0;
}
.form-title{
    font-size: 24px;
    font-weight: 600;
}
.form-action{
    display: flex;
    gap: 12px;
}
.form-action-icon{
    display: flex;
    gap: 12px;
}
i{
    height: 36px;
    width: 36px;
    display: flex;
    justify-content: center;
    align-items: center;
}
i:hover{
    cursor: pointer;
    background: #fff;
}
.active{
    background: #fff;
}
.form-body{
    margin: 6px 0;
}
.form-body-item{
    padding: 6px 12px;
}
.label{
    font-size: 20px;
    font-weight: 600;
    padding: 6px 0;
}
.form-body-item .input{
    width: 100%;
}
.form-footer{
    width: 100%;
    padding: 12px;
    display: flex;
    justify-content: flex-end;
    justify-items: center;
    gap: 12px;
}
::-webkit-scrollbar {
    appearance: none;
    width: 10px;
    height: 10px;
}

::-webkit-scrollbar-thumb {
    background: #045ebe;
    border-radius: 25px;
}
</style>