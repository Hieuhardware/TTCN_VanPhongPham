<template>
    <div class="top-navbar">
        <router-link to="/content" class="top-navbar__item"
        data-aos="fade-left"
        data-aos-duration="1000">Trang chủ</router-link>
        <a href="#" class="top-navbar__item"
        data-aos="fade-left"
        data-aos-duration="1400">Giới thiệu</a>
        <a href="#" class="top-navbar__item"
        data-aos="fade-left"
        data-aos-duration="1800">Liên hệ</a>
        <a href="#" class="top-navbar__item"
        data-aos="fade-left"
        data-aos-duration="2100">Hỗ trợ</a>
        <a href="#" class="top-navbar__item"
        data-aos="fade-left"
        data-aos-duration="2400">Dịch vụ</a>
        <a href="#" class="top-navbar__item"
        data-aos="fade-left"
        data-aos-duration="2700">Chính sách</a>
        <a href="#" class="top-navbar__item"
        data-aos="fade-left"
        data-aos-duration="3000">Câu hỏi thường gặp</a>
    </div>
</template>
<script>
export default {
    
}
</script>
<style scoped>
.top-navbar{
    display: flex;
    padding: 0 48px;
    justify-content: flex-start;
    align-items: center;
    gap: 4px;
    background: rgb(238,174,202);
background: linear-gradient(291deg, rgba(238,174,202,1) 0%, rgba(148,187,233,1) 100%);
}
.top-navbar__item{
    height: 40px;
    min-width: 100px;
    padding: 0 12px;
    display: block;
    line-height: 40px;
    text-align: center;
    color: #000;
    font-size: 20px;
    border: 1px solid transparent;
    border-radius: 4px;
    font-weight: 600;
    transition: all linear 0.5s;
}
.top-navbar__item:hover{
    color: #fff;
    background: rgb(131,58,180);
background: linear-gradient(90deg, rgba(131,58,180,1) 0%, rgba(253,29,29,1) 50%, rgba(252,176,69,1) 100%);
    border: 1px solid ;
}
</style>