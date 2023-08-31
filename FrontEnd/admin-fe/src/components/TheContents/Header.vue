<template>
    <div class="header">
        <div class="account">
            <img src="../../assets/image/avatar.png" alt="">
            <span>Administrator</span>
        </div>
        <div class="signout" @click="logout">
            <i class="fas fa-sign-out-alt"></i>
            <span>Đăng xuất</span>
        </div>
    </div>
</template>
<script>
export default {
    methods:{
        logout(){
            this.$store.commit("TOGGLE_ADMIN","")
        }
    }
}
</script>
<style scoped>
.header{
    height: 40px;
    display: flex;
    justify-content: flex-end;
    gap: 24px;
    width: 100%;
    padding: 0 48px;
    background: #fff;
}
.account{
    display: flex;
    align-items: center;
    gap: 12px;
}
.account img{
    width: 36px;
    height: 36px;
    border-radius: 100%;
}
.signout{
    display: flex;
    align-items: center;
    gap: 12px;
    opacity: .8;
    transition: all linear .5s;
}
.signout span{
    color: #007bff;
}
.signout:hover{
    opacity: 1;
    cursor: pointer;
}
</style>