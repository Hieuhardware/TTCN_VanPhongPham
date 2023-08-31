import Vue from "vue"
import VueRouter from "vue-router"

Vue.use(VueRouter)

import CategoryView from '@/view/CategoryView'
import CustomerView from '@/view/CustomerView'
import DashBoardView from '@/view/DashBoardView'
import OrderView from '@/view/OrderView'
import ProductView from '@/view/ProductView'
const routes = [
    { path: '/', redirectTo: '/dashboard' },
    { path: '/category', component: CategoryView },
    { path: '/customer', component: CustomerView },
    { path: '/dashboard', component: DashBoardView },
    { path: '/order', component: OrderView },
    { path: '/product', component: ProductView },
]

const router = new VueRouter({
    routes
})

export default router;