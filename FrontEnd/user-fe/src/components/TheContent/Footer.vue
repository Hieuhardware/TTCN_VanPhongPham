<template>
    <div class="footer">
        <div class="footer__content">
            <img src="../../assets/image/logo.png" width="50" height="50" alt="">
        </div>
        <div class="footer__content">
            <div class="icon-boder"><i class="fas fa-map-marker-alt"></i></div>
            <span>Số 298, đường Cầu Diễn, Phường Minh Khai, Quận Bắc Từ Liêm, Tp. Hà Nội</span>
        </div>
        <div class="footer__content">
            <div class="icon-boder"><i class="fas fa-phone-alt"></i></div>
            <span>0899 xxx 999</span>
        </div>
        <div class="footer__content">
            <div class="icon-boder"><i class="fas fa-envelope"></i></div>
            <span>vpphaui@gmail.com</span>
        </div>

    </div>
</template>
<style scoped>
.footer{
    display: flex;
    padding: 12px 48px;
    gap: 24px;
    align-items: center;
    justify-content: space-between;
    background: rgb(238,174,202);
background: linear-gradient(291deg, rgba(238,174,202,1) 0%, rgba(148,187,233,1) 100%);
}
.footer__content{
    display: flex;
    gap: 4px;
    align-items: center;
}
.icon-boder{
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
    border: 1px solid #000;
    border-radius: 4px;
}
</style>