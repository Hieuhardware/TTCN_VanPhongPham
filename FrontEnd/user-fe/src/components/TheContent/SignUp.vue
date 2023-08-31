<template>
    <div class="sign-up"
    data-aos="flip-left"
     data-aos-easing="ease-out-cubic"
     data-aos-duration="1500">
        <div class="signup-header"
        data-aos="fade-left"
        data-aos-duration="2000">
            Đăng kí
        </div>
        <div class="signup-content">
            <div>
                <div class="signup-content--item" data-aos="fade-left"
                data-aos-duration="2000">
                <i class="fas fa-envelope"></i>
                <span>Email:</span>
                <input type="text" class="input" :class="{'input-error': !isEmail.isTrue}" 
                placeholder="Email..." v-model="customer.email" @change="ValidateSingUp" :title="isEmail.title">
            </div>
            <div class="signup-content--item" data-aos="fade-left"
                data-aos-duration="2000">
                <i class="fas fa-key"></i>
                <span>Mật khẩu:</span>
                <input type="password" class="input" :class="{'input-error': !isPassword.isTrue}" 
                placeholder="Mật khẩu..." v-model="customer.password" @change="ValidateSingUp" :title="isPassword.title">
            </div>
            <div class="signup-content--item" data-aos="fade-left"
        data-aos-duration="2000">
                <i class="fas fa-user"></i>
                <span>Họ tên:</span>
                <input type="text" class="input" :class="{'input-error': !isName.isTrue}" 
                placeholder="Họ tên..." v-model="customer.name" @change="ValidateSingUp" :title="isName.title">
            </div>
            <div class="signup-content--item" data-aos="fade-left"
        data-aos-duration="2000">
                <i class="fas fa-phone-alt"></i>
                <span>Số điện thoại:</span>
                <input type="text" class="input" :class="{'input-error': !isTel.isTrue}" 
                placeholder="Số điện thoại..." v-model="customer.tel" @change="ValidateSingUp" :title="isTel.title">
            </div>
            <div class="signup-content--item" data-aos="fade-left"
        data-aos-duration="2000">
                <i class="fas fa-home"></i>
                <span>Địa chỉ:</span>
                <input type="text" class="input" placeholder="Địa chỉ..." v-model="customer.address">
            </div>
            <div class="signup-content--item" style="display:flex; justify-content:center" data-aos="fade-left"
        data-aos-duration="2000">
                <button class="button" @click="SignUp">Đăng kí</button>
            </div>
            
            </div>
            
        </div>
        <div class="signup-footer" data-aos="fade-left"
        data-aos-duration="2000">
            Đã có tài khoản? <router-link to="/signin">Đăng nhập</router-link>
        </div>
        <ErrorPopup @close="close" :title="title"  v-if="hasError"></ErrorPopup>
        <InfoPopup @close="close" @yes="yes" :title="title" v-if="hasInfo"></InfoPopup>
    </div>
</template>
<script>
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
import InfoPopup from '@/components/Bases/BasePopup/InfoPopup'
import api from '@/js/api'
export default {
    components:{
        ErrorPopup,
        InfoPopup
    },
    data() {
        return {
            customer:{
                email: "", password: "", name: "", tel: "", address:""
            },
            isEmail:{title:"",isTrue:true},
            isPassword:{title:"",isTrue:true},
            isName:{title:"",isTrue:true},
            isTel:{title:"",isTrue:true},
            title:"",
            hasError: false,
            hasInfo: false
        }
    },
    methods:{
        ValidateSingUp(){
            let check =0;
            if(this.customer.tel? !(/^\d{10}$/.test(this.customer.tel)) : false){
                this.isTel.title="Số diện thoại không đúng định dạng.";
                this.isTel.isTrue = false;
                this.title ="Số điện thoại không đúng định dạng.";
                check= check+1;
            }
            else{
                this.isTel.title="";
                this.isTel.isTrue = true;
            }
            if(!this.customer.name){
                this.isName.title="Họ tên không được phép bỏ trống.";
                this.isName.isTrue = false;
                this.title ="Họ tên không được phép bỏ trống.";
                check= check+1;
            }
            else{
                this.isName.title="";
                this.isName.isTrue = true;
            }
            if(!this.customer.password){
                this.isPassword.title="Mật khẩu không được phép bỏ trống.";
                this.isPassword.isTrue = false;
                this.title ="Mật khẩu không được phép bỏ trống.";
                check= check+1;
            }
            else{
                this.isPassword.title="";
                this.isPassword.isTrue = true;
            }
            if(!this.customer.email){
                this.isEmail.title="Email không được phép bỏ trống.";
                this.isEmail.isTrue = false;
                this.title ="Email không được phép bỏ trống.";
                check= check+1;
            }
            else{
                this.isEmail.title="";
                this.isEmail.isTrue = true;
            }
            if(this.customer.email? !(/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.customer.email)) : false ){
                this.isEmail.title="Email không đúng định dạng.";
                this.isEmail.isTrue = false;
                this.title ="Email không đúng định dạng.";
                check= check+1;
            }
            else{
                this.isEmail.title="";
                this.isEmail.isTrue = true;
            }
            return check;
        },
        async SignUp(){
            if(this.ValidateSingUp()>0){
                this.hasError=true;
            }
            else{
                let check = await this.$axios.get(`${api.CustomerApi}/${this.customer.email}`).then(res => res.data);
                if(check){
                    this.title = "Email đã tồn tại trong hệ thống, vui lòng chọn email khác."
                    this.hasError = true;
                }
                else{
                    this.customer.date_create = new Date().toISOString().slice(0, 10);
                    this.$axios.post(api.CustomerApi,this.customer).then(res => {
                    if(res.status === 201){
                        this.title ="Đăng kí thành công, bạn có muốn đăng nhập không?";
                        this.hasInfo =true;
                    }
                })
                }
            }
        },
        close(){
            this.hasError=false;
            this.hasInfo = false;
        },
        yes(){
            this.$router.replace({path:'/signin'});
        }
    }
}
</script>
<style scoped>
.sign-up{
    margin: 24px 48px;
    background-color: #fff;
    border-radius: 8px;
}
.signup-header{
    height: 60px;
    font-weight: 600;
    font-size: 28px;
    border-bottom: 2px solid #ccc;
    padding: 0 24px;
    line-height: 60px;
    background: -webkit-linear-gradient(45deg,rgb(0, 139, 19), rgb(128, 246, 85));
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}
.signup-content{
    display: flex;
    justify-content: center;
    align-items: center;
    background: rgb(27,246,4);
    background: linear-gradient(90deg, rgba(27,246,4,0.1) 0%, rgba(252,255,251,0.04) 50%, rgba(27,246,4,0.1) 100%);
}
.signup-content--item{
    display: flex;
    gap: 24px;
    justify-items: center;
    margin: 24px 0;
}
.signup-content--item i{
    line-height: 40px;
}
.signup-content--item span{
    width: 120px;
    line-height: 40px;
}
.signup-content--item input{
    width: 400px;
    border: 2px solid transparent;
}
.signup-content--item input:hover,input:focus{
    border: 2px solid #3DAA12;
}
.signup-footer{
    height: 60px;
    padding: 0 24px;
    line-height: 60px;
    font-size: 20px;
    border-top: 2px solid #ccc;
}
</style>