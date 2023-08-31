import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import axios from 'axios'
import store from "./store"
import router from "./router"
import CKEditor from 'ckeditor4-vue';
import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import AOS from 'aos'
import 'aos/dist/aos.css'
Vue.use(VueMaterial)
Vue.use(CKEditor);
Vue.use(VueRouter);
Vue.prototype.$axios = axios;
Vue.config.productionTip = false

new Vue({
    created() {
        AOS.init();
    },
    render: h => h(App),
    store,
    router
}).$mount('#app')