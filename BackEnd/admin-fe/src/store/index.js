import Vue from "vue"
import Vuex from "vuex"

Vue.use(Vuex)

const storeData = {
    state: {
        admin: "",
    },
    getters: {
        user: state => state.admin
    },
    mutations: {
        TOGGLE_ADMIN(state, admin) {
            state.admin = admin;
        },
    }
}
const store = new Vuex.Store(storeData)
export default store