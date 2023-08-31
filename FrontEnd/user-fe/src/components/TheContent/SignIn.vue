<template>
    <div class="sign-in"
    data-aos="flip-right"
     data-aos-easing="ease-out-cubic"
     data-aos-duration="1500">
        <div class="signin-header"
        data-aos="fade-right"
        data-aos-duration="2000">
            Đăng nhập
        </div>
        <div class="singin-content">
            <div class="form">
                <div class="signin-content--item"
                data-aos="fade-right"
                data-aos-duration="2000">
                    <i class="fas fa-envelope"></i>
                    <span>Email:</span>
                    <input type="text" class="input" placeholder="Email..." v-model="email">
                </div>
                <div class="signin-content--item"
                data-aos="fade-right"
                data-aos-duration="2000">
                    <i class="fas fa-key"></i>
                    <span>Mật khẩu:</span>
                    <input type="password" class="input" placeholder="Mật khẩu..." v-model="password">
                </div>
                <div class="signin-content--item" style="display:flex; justify-content:center"
                data-aos="fade-right"
                data-aos-duration="2000">
                    <button class="button" @click="SignIn()">Đăng Nhập</button>
                </div>
            </div>
        </div>
        <div class="signin-footer"
        data-aos="fade-right"
        data-aos-duration="1500">
            Chưa có tài khoản? <router-link to="/signup">Đăng kí</router-link>
        </div>
        <ErrorPopup :title="title" @close="close" v-if="hasError"></ErrorPopup>
    </div>
</template>
<script>
import api from '@/js/api'
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
export default {
    components:{
        ErrorPopup,
    },
    data() {
        return {
            email:"",
            password:"",
            title:"",
            hasError:false,
        }
    },
    methods:{
        SignIn(){
            if(this.email && this.password){
                this.$axios.get(`${api.CustomerApi}/${this.email}`).then(res =>{
                    if(res.data){
                        if(res.data.password == this.password){
                            this.$store.commit("TOGGLE_CUSTOMER",res.data);
                            this.$router.replace({path:'/content'});
                            window.scrollTo({ top: 0, behavior: "smooth" });
                        }
                        else{
                            this.title ="Mật khẩu không chính xác."
                            this.hasError=true;
                        }
                    }
                    else{
                            this.title ="Email không chính xác."
                            this.hasError=true;
                    }
                })
            }
            else{
                this.title ="Vui lòng nhập Email và mật khẩu."
                this.hasError=true;
            }
        },
        close(){
            this.hasError = false;
        },
    }
}
</script>
<style scoped>
.sign-in{
    margin: 24px 48px;
    background-color: #fff;
    border-radius: 8px;
}
.signin-header{
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
.singin-content{
    display: flex;
    min-height: 350px;
    align-items: center;
    justify-content: center;
    background: rgb(27,246,4);
    background: linear-gradient(90deg, rgba(27,246,4,0.1) 0%, rgba(252,255,251,0.04) 50%, rgba(27,246,4,0.1) 100%);
}
.signin-content--item{
    display: flex;
    gap: 24px;
    justify-items: center;
    margin: 24px 0;
}
.signin-content--item i{
    line-height: 40px;
}
.signin-content--item span{
    width: 120px;
    line-height: 40px;
}
.signin-content--item input{
    width: 400px;
    border: 2px solid transparent;
}
.signin-content--item input:focus,input:hover{
    border: 2px solid #3DAA12;
}
.signin-footer{
    height: 60px;
    padding: 0 24px;
    line-height: 60px;
    font-size: 20px;
    border-top: 2px solid #ccc;
}
.form{
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
}
</style>