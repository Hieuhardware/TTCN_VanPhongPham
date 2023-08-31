<template>
    <div class="login">
        <div class="login-form">
            <div class="login-form-container">
                <div class="login-content--item" style="display:flex; justify-content:center">
                    <div class="login-header">Đăng nhập quản trị viên</div>
                </div>
                <div class="login-content--item">
                <i class="fas fa-user"></i>
                <span>Tên đăng nhập:</span>
                <input type="text" class="input" placeholder="Tên đăng nhập..." v-model="admin.username">
                </div>
                <div class="login-content--item">
                    <i class="fas fa-key"></i>
                    <span>Mật khẩu:</span>
                    <input type="password" class="input" placeholder="Mật khẩu..." v-model="admin.password">
                </div>
                <div class="login-content--item" style="display:flex; justify-content:center">
                <button class="button" @click="login()">Đăng Nhập</button>
                </div>
            </div>
        </div>
        <Loader v-if="hasLoader"></Loader>
        <ErrorPopup @close="close" :title="title"  v-if="hasError"></ErrorPopup>
    </div>
</template>
<script>
import api from '@/js/api'
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
import Loader from '@/components/Bases/Loader'
export default {
    components:{
        ErrorPopup,
        Loader
    },
    data() {
        return {
            admin:{
                username:"",
                password:""
            },
            hasError:false,
            hasLoader:false,
            title:""
        }
    },
    methods:{
        async login(){
            if(!this.admin.username && !this.admin.password){
                this.title= "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu."
                this.hasError=true
                return
            }
            let user =await this.$axios.get(`${api.AdminApi}/${this.admin.username}`).then(res => res.data)
            if(user){
                if(this.admin.password == user.password){
                    this.$store.commit("TOGGLE_ADMIN",user)
                    this.$router.push({path:"/dashboard"}).catch(()=>{})
                }
                else{
                    this.title= "Sai mật khẩu, vui lòng nhập lại."
                    this.hasError=true
                }
            }
            else{
                this.title= "Tài khoản không tồn tại, vui lòng kiểm tra lại."
                this.hasError=true;
            }
        },
        close(){
            this.hasError=false;
        }
    }
}
</script>
<style scoped>
.login{
    width: 100vw;
    height: 100vh;
    background-color: rgba(0, 0, 0, .2);
    background-image: url('../../assets/image/banner.jpg');
    background-size: 100% 100%;
    display: flex;
    justify-content: center;
    align-items: center;
}
.login-form{
    width: 50%;
    height: 50%;
    min-width: 500px;
    min-height: 200px;
    background: rgb(176,255,251);
    background: linear-gradient(90deg, rgba(176,255,251,0.8) 0%, rgba(144,144,238,0.8) 80%, rgba(8,59,244,0.8) 100%);
    border-radius: 8px;
    box-shadow: 0px 9px 15px rgba(0, 0, 0, 0.3), 0px 3px 54px rgba(0, 0, 0, 0.18),
    0px 18px 30px rgba(0, 0, 0, 0.21);
    display: flex;
    justify-content: center;
    align-items: center;
}
.login-form-container{
    width: 100%;
    padding: 24px;
}
.login-content--item{
    display: flex;
    align-items: center;
    margin: 12px 0;
}
.login-header{
    height: 60px;
    line-height: 60px;
    font-size: 32px;
    color: rgb(59, 8, 226);
    font-weight: 600;
    background: -webkit-linear-gradient(45deg,rgb(47, 3, 192), rgb(26, 74, 249));
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}
.login-content--item i{
    min-width: 40px;
    height: 40px;
    font-size: 32px;
    display: flex;
    justify-content: center;
    align-items: center;
}
.login-content--item span{
    min-width: 150px;
    padding: 0 12px;
}
.login-content--item input{
    width: 100%;
    min-width: 200px;
    border: 2px solid transparent;
    transition: all linear .6s;;
}
.login-content--item input:focus,input:hover{
    border: 2px solid #045ebe;
}
</style>