<template>
    <div class="navbar">
        <div class="logo">
            <img src="../../assets/image/logo.png" width="100" height="100" alt="">
        </div>
        <div class="navbar-items">
            <router-link to="/dashboard" class="navbar-item">
                <i class="fas fa-chart-line"></i>
                <span>Trang chủ</span>
            </router-link>
            <router-link to="/category" class="navbar-item">
                <i class="fas fa-grip-lines"></i>
                <span>Danh mục</span>
            </router-link>
            <router-link to="/product" class="navbar-item">
                <i class="fas fa-store"></i>
                <span>Hàng hóa</span>
            </router-link>
            <router-link to="/customer" class="navbar-item">
                <i class="fas fa-users"></i>
                <span>Khách hàng</span>
            </router-link>
            <router-link to="/order" class="navbar-item">
                <i class="fas fa-shopping-cart"></i>
                <span>Đơn hàng</span>
            </router-link>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
        }
    },
}
</script>
<style scoped>
.navbar{
    display: flex;
    flex-direction: column;
    background-color: #ccc;
    min-width: 250px;
    max-width: 250px;
    min-height: 700px;
}
.logo{
    height: 100px;
    width: 250px;
    border-bottom: 2px solid #eee;
    display: flex;
    align-items: center;
    justify-content: center;
}
.navbar-item{
    display: block;
    opacity: 0.8;
    height: 60px;
    padding: 4px 12px;
    display: flex;
    gap: 24px;
    align-items: center;
    border-bottom: 1px solid #eee;
    transition: all linear 0.5s;
}
.navbar-item:hover{
    opacity: 1;
    background-color: rgba(41, 139, 245, .5);
    color: #fff;
    cursor: pointer;
}
</style>