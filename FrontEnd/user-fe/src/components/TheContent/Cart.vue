<template>
  <div class="cart"
  data-aos="flip-right"
     data-aos-easing="ease-out-cubic"
     data-aos-duration="1500">
    <div class="cart-header">Giỏ hàng</div>
    <div class="cart-content">
      <NoData v-if="noData"></NoData>
      <table v-else>
        <thead>
          <tr data-aos="fade-right"
        data-aos-duration="2000">
            <th>#</th>
            <th>Hình ảnh</th>
            <th>Tên sản phẩm</th>
            <th>Đơn giá</th>
            <th>Đơn vị tính</th>
            <th>Số lượng</th>
            <th>Thành tiền</th>
            <th>Xóa</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item,index) in items" :key="item.id+index+item.name"
          data-aos="fade-left"
        data-aos-duration="2000">
            <td style="min-width: 40px">{{ index+1 }}</td>
            <td style="min-width: 40px; display: flex; justify-content: center">
              <img :src="item.image || avatar" alt="" />
            </td>
            <td style="min-width: 400px">{{item.name}}</td>
            <td style="min-width: 100px">{{item.price}}</td>
            <td style="min-width: 120px">{{item.unit}}</td>
            <td style="min-width: 110px">
              <input type="number" v-model="item.quantity" @change="changeQuantity(item)"/>
            </td>
            <td style="min-width: 120px">{{item.price*item.quantity}}</td>
            <td style="min-width: 40px"><i class="far fa-trash-alt" @click="deleteCartItem(item)"></i></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="cart-footer"
    data-aos="fade-right"
        data-aos-duration="2000">
      <button class="button"><router-link to="/content">Tiếp tục mua hàng</router-link></button>
      <button class="button" @click="payment()">Thanh toán</button>
    </div>
    <div class="payment-form" v-if="isPayMent">
      <div class="payment-form-content">
        <div class="cart-header">
            <div>Đặt hàng</div>
            <i class="far fa-times-circle" @click="isPayMent= false"></i>
        </div>
        <div class="form-content">
          <div class="form-content-info">
            <div class="form-address">
                <div style="font-weight:600">Thông tin nhận hàng</div>
              <div class="address-item">
                <i class="fas fa-user" ></i>
                <span>Họ tên người nhận:</span>
                <input type="text" class="input" placeholder="Họ tên..." v-model="payMentInfo.name" 
                :class="{'input-error': !isName.isTrue}" @change="ValidateOrder" :title="isName.title"/>
              </div>
              <div class="address-item">
                <i class="fas fa-phone-alt"></i>
                <span>Số điện thoại:</span>
                <input
                  type="text"
                  class="input"
                  placeholder="Số điện thoại..."
                  v-model="payMentInfo.tel"
                  :class="{'input-error': !isTel.isTrue}" @change="ValidateOrder" :title="isTel.title"
                />
              </div>
            </div>
            <div class="form-note">
                <div style="font-weight:600">....</div>
                <div class="address-item">
                <i class="fas fa-home"></i>
                <span>Địa chỉ nhận hàng:</span>
                <input type="text" class="input" placeholder="Địa chỉ..." v-model="payMentInfo.address"
                :class="{'input-error': !isAddress.isTrue}" @change="ValidateOrder" :title="isAddress.title"/>
              </div>
              <div class="address-item">
                <i class="fas fa-comment-dots"></i>
                <span>Ghi chú:</span>
                <input type="text" class="input" placeholder="Ghi chú..." v-model="payMentInfo.notes"/>
              </div>
            </div>
          </div>
          <div class="table">
              <table>
            <thead>
              <tr>
                <th>#</th>
                <th>Hình ảnh</th>
                <th>Tên sản phẩm</th>
                <th>Đơn giá</th>
                <th>Đơn vị tính</th>
                <th>Số lượng</th>
                <th>Thành tiền</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item,index) in items" :key="index+item.id+item.image">
                <td style="min-width: 40px">{{ index+1 }}</td>
                <td
                  style="
                    min-width: 40px;
                    display: flex;
                    justify-content: center;
                  "
                >
                  <img :src="item.image || avatar" alt="" />
                </td>
                <td style="min-width: 400px">{{item.name}}</td>
                <td style="min-width: 100px">{{item.price}}</td>
                <td style="min-width: 120px">{{item.unit}}</td>
                <td style="min-width: 110px">{{ item.quantity }}</td>
                <td style="min-width: 120px">{{item.price*item.quantity}}</td>
              </tr>
            </tbody>
          </table>
          <div class="total-money">Tổng tiền thanh toán: {{totalMoney}}đ</div>
          </div>
        </div>
        <div class="cart-footer">
          <button class="button" @click="GetDefault()">Điền mặc định</button>
          <button class="button" @click="AddOrder()">Đặt hàng</button>
        </div>
      </div>
    </div>
    <Loader v-if="hasLoader"></Loader>
    <ErrorPopup @close="close" :title="title"  v-if="hasError"></ErrorPopup>
    <QuestionPopup :title="title" @close="close" @yes="yes" v-if="hasQuestion"></QuestionPopup>
    <ToastMesage :mesage="title" @closeToast="closeToast" v-if="hasToast"></ToastMesage>
  </div>
</template>
<script>
import NoData from '@/components/Bases/NoData'
import defaultAvatar from "../../assets/image/img_default.jpg";
import Loader from '@/components/Bases/Loader'
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
import QuestionPopup from '@/components/Bases/BasePopup/QuestionPopup'
import ToastMesage from '@/components/Bases/ToastMesage'
import api from '@/js/api'
export default {
  components:{
    Loader,QuestionPopup,ToastMesage,ErrorPopup,NoData
  },
  data() {
    return {
      totalMoney:0,
      noData:false,
      items: [],
      avatar: defaultAvatar,
      isPayMent: false,
      customer:{},
      hasLoader:false,
      hasQuestion:false,
      hasError:false,
      hasToast:false,
      title:"",
      payMentInfo:{
        name:"",
        tel:"",
        address:"",
        notes:"",
      },
      selectedItem:{},
      isName:{title:"",isTrue:true},
      isTel:{title:"",isTrue:true},
      isAddress:{title:"",isTrue:true},
    };
  },
  created(){
    this.customer = this.getCustomer;
    if(!this.customer){
      this.$router.replace({path:"/content"})
    }
    this.getCart();
  },
  computed:{
    getCustomer(){
      return this.$store.state.customer;
    }
  },
  methods:{
    payment(){
      if(!this.items.length){
        this.title = "Không có sản phẩm nào trong giỏ hàng, vui lòng thêm hàng vào giỏ trước khi đặt."
        this.hasError = true;
        return
      }
      this.totalMoney = this.items.reduce((total,item)=>total+item.quantity*item.price,0);
      this.isPayMent =true
    },
    getCart(){
      this.hasLoader=true;
      this.$axios.get(`${api.CartApi}/${this.customer.id}`).then(res=>{
        this.items=res.data;
        if(this.items.length === 0){
          this.noData =true
        }
        else{
          this.noData= false
        }
        this.$store.commit("CHANGE_CART",this.items.length);
      }).finally(()=>this.hasLoader=false)
    },
    changeQuantity(item){
      if(item.quantity==0){
        this.selectedItem = item;
        this.yes();
      }
      this.hasLoader=true;
      this.$axios.put(api.CartApi, item).finally(()=>this.hasLoader=false)

    },
    deleteCartItem(item){
      this.selectedItem = item;
      console.log(this.selectedItem)
      this.title="Bạn có chắc muốn xóa mặt hàng này không?";
      this.hasQuestion=true;   
    },
    close(){
      this.hasQuestion=false;
      this.hasError=false;
    },
    yes(){
      this.hasLoader=true;
      this.hasQuestion=false;

      this.$axios.delete(`${api.CartApi}/${this.selectedItem.cus_id}/${this.selectedItem.prod_id}`).then(()=>{
        this.title="Xóa thành công";
        this.hasToast=true;
        setTimeout(() => {
          this.hasToast=false;
        }, 3000);
        this.getCart();
      }).finally(()=>this.hasLoader=false)
    },
    ValidateOrder(){
            let check = 0;
            if(!this.payMentInfo.address){
                this.isAddress.title="Địa chỉ nhận hàng không được để trống.";
                this.isAddress.isTrue = false;
                this.title ="Địa chỉ nhận hàng không được để trống.";
                check= check+1;
            }
            else{
                this.isAddress.title="";
                this.isAddress.isTrue = true;
            }
            if(!this.payMentInfo.tel){
                this.isTel.title="Số điện thoại không được phép bỏ trống.";
                this.isTel.isTrue = false;
                this.title ="Số điện thoại không được phép bỏ trống.";
                check= check+1;
            }
            else{
                this.isTel.title="";
                this.isTel.isTrue = true;
            }
            if(this.payMentInfo.tel && !(/^\d{10}$/.test(this.payMentInfo.tel))){
                this.isTel.title="Số diện thoại không đúng định dạng.";
                this.isTel.isTrue = false;
                this.title ="Số điện thoại không đúng định dạng.";
                check= check+1;
            }
            else{
                this.isTel.title="";
                this.isTel.isTrue = true;
            }
            if(!this.payMentInfo.name){
                this.isName.title="Họ tên không được phép bỏ trống.";
                this.isName.isTrue = false;
                this.title ="Họ tên không được phép bỏ trống.";
                check= check+1;
            }
            else{
                this.isName.title="";
                this.isName.isTrue = true;
            }
            return check;
        },
    GetDefault(){
      let customer = this.getCustomer;
      this.payMentInfo.name=customer.name;
      this.payMentInfo.tel=customer.tel;
      this.payMentInfo.address=customer.address
    },
    async AddOrder(){
      if(this.ValidateOrder()>0){
        this.hasError=true;
      }
      else{
        var today = new Date();
        var date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
        let order = {
          id:1,
          cus_id: this.getCustomer.id,
          order_date: date, 
          status:"Đang chờ xác nhận", 
          notes: this.payMentInfo.notes, 
          address:this.payMentInfo.address, 
          tel:this.payMentInfo.tel, 
          name:this.payMentInfo.name
        }
        let addedOrder;
        await this.$axios.post(api.OrderApi,order).then(res=>{
          addedOrder = res.status
        })
        if(addedOrder ==201){
            let orderId = await this.$axios.get(`${api.OrderApi}/newOrder`).then(res=>res.data);
            let od = this.items.map(item=>({
              prod_id:item.prod_id,
              order_id: orderId, 
              quantity: item.quantity, 
              price: item.price
            }))
            this.$axios.post(api.OrderDetailApi,od).then(res=>{
              if(res.status==201){
                this.title="Tạo đơn hàng thành công."
                this.hasToast=true;
                setTimeout(() => {
                  this.hasToast=false;
                }, 3000);
                this.deleteCard();
                this.isPayMent=false;
                this.$router.replace({path:"/content"})
              }
            })
          }
      }
    },
    deleteCard(){
      this.$axios.delete(`${api.CartApi}/${this.customer.id}`).then(res=>{
        if(res.status==200){
          this.$store.commit("CHANGE_CART",0)
        }
      })
    },
    closeToast(){
      this.hasToast=false;
    }
  },
  watch:{
    getCustomer(value){
      this.customer= value;
    }
  },
};
</script>
<style scoped>
.cart {
  border-radius: 8px;
  background-color: #fff;
  margin: 24px 48px;
  min-height: 400px;
}
.cart-header {
  height: 40px;
  line-height: 40px;
  padding: 0 12px;
  font-size: 24px;
  font-weight: 600;
  border-bottom: 2px solid #ccc;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.cart-content {
  padding: 24px;
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 400px;
}
table {
  border: 1px solid #ccc;
  border-collapse: collapse;
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
.far {
  color: rgb(253, 63, 63);
  opacity: .8;
}
.far:hover {
  color: red;
  opacity: 1;
}
.cart-footer {
  display: flex;
  gap: 12px;
  justify-content: flex-end;
  padding: 12px 24px;
  border-top: 2px solid #ccc;
}
.payment-form {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}
.payment-form-content {
  background: #fff;
  border-radius: 8px;
}
.form-content {
  padding: 12px;
  display: flex;
  justify-content: center;
  display: flex;
  flex-direction: column;
  gap: 12px;
}
.form-content-info{
    display: flex;
    justify-content: space-between;
}
.form-content .table{
    max-height: 200px;
    overflow: auto;
}
.address-item {
  display: flex;
  gap: 24px;
  justify-items: center;
  margin: 12px 0;
}
.address-item i{
  width: 30px;
  line-height: 40px;
}
.address-item span{
    width: 180px;
    line-height: 40px;
}
.total-money{
  height: 40px;
  font-size: 20px;
  line-height: 40px;
  color: #3daa12;
  font-weight: 600;
}
</style>