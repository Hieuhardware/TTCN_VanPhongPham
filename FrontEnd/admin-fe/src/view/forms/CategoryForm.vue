<template>
    <div class="form-container">
        <div class="form-content">
            <div class="form-header">
                <div class="form-title">{{titleHeader}}</div>
                <div class="form-action">
                    <div class="form-action-icon" v-if="!isAdd">
                        <i class="fas fa-plus" :class="{active: statusData==0}" @click="statusAdd"></i>
                        <i class="fas fa-edit" :class="{active: statusData==1}" @click="statusEdit"></i>
                        <i class="fas fa-eye" :class="{active: statusData==2}" @click="statusDetail"></i>
                    </div>
                    <div class="form-close">
                        <i class="fas fa-times" @click="$emit('close')"></i>
                    </div>
                </div>
            </div>

            <div class="form-body">
                <div class="form-body-item">
                    <div class="label">Tên danh mục<span style="color:red"> * </span>:</div>
                    <div class="value">
                        <input type="text" class="input" v-model="category.name" :disabled="isDetail">
                    </div>
                </div>
                <div class="form-body-item">
                    <div class="label">Mô tả:</div>
                    <div class="value" v-if="!isDetail">
                        <ckeditor tag-name="textarea" v-model="category.description" :config="editorConfig"></ckeditor>
                    </div>
                    <div class="value" v-else>
                        <span v-html="category.description"></span>
                    </div>
                </div>
            </div>

            <div class="form-footer">
                <button class="button button-secondary" @click="$emit('close')">Đóng</button>
                <button class="button" v-if="this.statusData == 0" @click="addItem()">Thêm mới</button>
                <button class="button" v-if="this.statusData == 1" @click="updateItem()">Cập nhật</button>
            </div>
        </div>
        <ErrorPopup :title="message" @close="close" v-if="hasError"></ErrorPopup>
    </div>
</template>
<script>
import api from '@/js/api'
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
export default {
    props:["title","status","item"],
    components:{ErrorPopup},
    data() {
        return {
            isAdd:true,
            editorConfig:{},
            isDetail: false,
            data:this.item,
            statusData:this.status,
            category:{
                id:0,
                name:"",
                description:""
            },
            message:"",
            hasError: false
        }
    },
    created(){
        if(this.status == 1){
            this.isAdd = true
        }
        if(this.status == 1){
            this.isAdd = false
            this.category = this.data;
        }
        if(this.status == 2){
            this.isAdd = false
            this.category = this.data;
            this.isDetail =true
        }
    },
    computed:{
        titleHeader(){
            if(this.statusData ==0){
                return "Thêm " + this.title
            }
            if(this.statusData ==1){
                return "Sửa " + this.title
            }
            if(this.statusData ==2){
                return "Xem chi tiết " + this.title
            }
            return ""
        },

    },
    methods:{
        getNewItem(){
            return {
                id:0,
                name:"",
                description:""
            }
        },
        statusAdd(){
            this.category = this.getNewItem();
            this.isDetail = false;
            this.statusData=0;
        },
        statusEdit(){
            this.category = this.data;
            this.isDetail = false;
            this.statusData=1;
        },
        statusDetail(){
            this.category = this.data;
            this.isDetail = true;
            this.statusData=2;
        },
        addItem(){
            if(!this.category.name){
                this.message= "Tên danh mục không được để trống."
                this.hasError = true
                return
            }
            this.$axios.post(api.CategoryApi,this.category).then(() => {
                this.$emit("close");
                this.$emit("success");
            })
            .catch(()=>{
                this.message = "Có lỗi xảy ra, vui lòng kiểm tra lại.";
                this.hasError= true;
            })
        },
        updateItem(){
            if(!this.category.name){
                this.message= "Tên danh mục không được để trống."
                this.hasError = true
                return
            }
            this.$axios.put(`${api.CategoryApi}/${this.category.id}`,this.category).then(() => {
                this.$emit("close");
                this.$emit("success");
            })
            .catch(()=>{
                this.message = "Có lỗi xảy ra, vui lòng kiểm tra lại.";
                this.hasError= true;
            })
        },
        close(){
            this.hasError = false;
        }
    }
}
</script>
<style scoped>
.form-container{
    position: fixed;
    inset: 0;
    background-color: rgba(0, 0, 0, 0.3);
    display: flex;
    justify-content: center;
    align-items: center;
}
.form-content{
    background: #fff;
    border-radius: 8px;
    min-width: 800px;
    max-width: 60%;
    width: 60%;
    max-height: 600px;
    overflow: auto;
}
.form-header{
    height: 40px;
    display: flex;
    justify-content: space-between;
    padding: 0 12px;
    align-items: center;
    background: #eee;
    border-radius: 8px 8px 0 0;
}
.form-title{
    font-size: 24px;
    font-weight: 600;
}
.form-action{
    display: flex;
    gap: 12px;
}
.form-action-icon{
    display: flex;
    gap: 12px;
}
i{
    height: 36px;
    width: 36px;
    display: flex;
    justify-content: center;
    align-items: center;
}
i:hover{
    cursor: pointer;
    background: #fff;
}
.active{
    background: #fff;
}
.form-body{
    margin: 6px 0;
}
.form-body-item{
    padding: 6px 12px;
}
.label{
    font-size: 20px;
    font-weight: 600;
    padding: 6px 0;
}
.form-body-item .input{
    width: 100%;
}
.form-footer{
    width: 100%;
    padding: 12px;
    display: flex;
    justify-content: flex-end;
    justify-items: center;
    gap: 12px;
}
::-webkit-scrollbar {
    appearance: none;
    width: 10px;
    height: 10px;
}

::-webkit-scrollbar-thumb {
    background: #045ebe;
    border-radius: 25px;
}
</style>