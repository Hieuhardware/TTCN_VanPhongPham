<template>
    <div class="form-container">
        <div class="form-content">
            <div class="form-header">
                <div class="form-title">{{titleHeader}}</div>
                <div class="form-action">
                    <div class="form-action-icon" v-if="!isAdd">
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
                    <div class="label">Họ tên người nhận hàng<span style="color:red"> * </span>:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="order.name" disabled>
                    </div>
                </div>
                <div class="form-body-item">
                    <div class="label">Số điện thoại:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="order.tel" disabled>
                    </div>
                </div>
                <div class="form-body-item">
                    <div class="label">Địa chỉ nhận hàng:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="order.address" disabled>
                    </div>
                </div>
                <div class="form-body-item" style="display:flex;justify-content:space-between">
                    <div>
                        <div class="label">Ngày đặt hàng:</div>
                        <div class="value" style="padding-top:6px">
                            <input type="text" class="input" v-model="order.order_date" disabled>
                        </div>
                    </div>
                    <div>
                    <div class="label">Trạng thái:</div>
                        <div class="value">
                            <vue-single-select
                                placeholder="Chọn 1 trạng thái"
                                max-height="100px"
                                v-model="order.status"
                                :options="options"
                                :required="true"
                            ></vue-single-select>
                        </div>
                    </div>
                </div>

                <div class="form-body-item">
                    <div class="label">Ghi chú:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="order.notes" disabled>
                    </div>
                </div>
                <div class="form-body-item" v-if="isDetail">
                    <div class="label">Đơn hàng chi tiết:</div>
                    <div class="value">
                        <table>
                            <thead>
                                <tr>
                                    <th>Sản phẩm</th>
                                    <th>Tên sản phẩm</th>
                                    <th>Số lượng</th>
                                    <th>Giá bán</th>                                    
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item,index) in orderDetails" :key="index+item.name">
                                    <td><img :src="item.image" alt=""></td>
                                    <td>{{item.name}}</td>
                                    <td>{{item.quantity}}</td>
                                    <td>{{item.price}}</td>
                                </tr>
                            </tbody>
                        </table>
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
import VueSingleSelect from "vue-single-select";
export default {
    props:["title","status","item"],
    components:{ErrorPopup, VueSingleSelect},
    data() {
        return {
            isAdd:true,
            editorConfig:{},
            isDetail: false,
            data:this.item,
            statusData:this.status,
            order:{
                id:0,
                cus_id:0,
                order_date:"",
                status:"",
                notes:"",
                address:"",
                tel:"",
                name:""
            },
            orderDetails:[],
            message:"",
            hasError: false,
            options: [
                'Đang chờ xác nhận',
                'Đang chờ xử lý',
                'Đang giao hàng',
                'Đã giao hàng',
                'Bị từ chối'
            ]
        }
    },
    created(){
        if(this.status == 1){
            this.isAdd = true
        }
        if(this.status == 1){
            this.isAdd = false
            this.order = this.data;
        }
        if(this.status == 2){
            this.isAdd = false
            this.order = this.data;
            this.isDetail =true
        }
        this.getDetailOrders();
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
        getDetailOrders(){
            this.$axios.get(`${api.OrderDetailApi}/${this.order.id}`).then(res=>{
                this.orderDetails = res.data;
            }).catch(()=>{
                this.message = "Có lỗi xảy ra, vui lòng kiểm tra lại."
                this.hasError = true
            })
        },
        getNewItem(){
            return {
                id:0,
                cus_id:0,
                order_date:"",
                status:"",
                notes:"",
                address:"",
                tel:"",
                name:""
            }
        },
        statusAdd(){
            this.order = this.getNewItem();
            this.isDetail = false;
            this.statusData=0;
        },
        statusEdit(){
            this.order = this.data;
            this.isDetail = false;
            this.statusData=1;
        },
        statusDetail(){
            this.order = this.data;
            this.isDetail = true;
            this.statusData=2;
        },
        addItem(){
            if(!this.order.name){
                this.message= "Tên người nhận không được để trống."
                this.hasError = true
                return
            }
            this.$axios.post(api.OrderApi,this.order).then(() => {
                this.$emit("close");
                this.$emit("success");
            })
            .catch(()=>{
                this.message = "Có lỗi xảy ra, vui lòng kiểm tra lại.";
                this.hasError= true;
            })
        },
        updateItem(){
            if(!this.order.name){
                this.message= "Tên người nhânh không được để trống."
                this.hasError = true
                return
            }
            this.$axios.put(`${api.OrderApi}/${this.order.id}`,this.order).then(() => {
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
.search-input{
    outline: none;
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
table {
  border: 1px solid #ccc;
  border-collapse: collapse;
  width: 100%;
}
table tr:nth-child(2n) {
  background: #eee;
}
table th {
  height: 40px;
  line-height: 40px;
  padding: 4px 12px;
  background: #ccc;
}
table img {
  width: 36px;
  height: 36px;
  border-radius: 100%;
}
table td {
  height: 40px;
  line-height: 40px;
  padding: 4px 12px;
  text-align: center;
}
table input {
  width: 100px;
  outline: none;
  border: 1px solid #333;
  background-color: #fff;
  color: #000;
  border-radius: 4px;
  padding: 2px;
}
table input:focus {
  border: 1px solid #3daa12;
}
</style>