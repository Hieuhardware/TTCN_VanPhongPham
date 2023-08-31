<template>
    <div class="view">
        <div class="card"
        data-aos="flip-left"
        data-aos-easing="ease-out-cubic"
        data-aos-duration="800">
            <div class="card-item">
                <div class="card-item-header"
                data-aos="fade-right"
                data-aos-duration="1000">
                    <div class="card-itm-header-title">{{title}}</div>
                    <button class="button" @click="$emit('add')" v-if="!noAdd">Tạo mới</button>
                </div>
                <div class="card-item-content">
                    <table>
                        <thead>
                            <tr data-aos="fade-right"
                            data-aos-easing="linear"
                            data-aos-duration="800">
                                <th v-for="(item,index) in tableheader" :key="index+item.name">{{item.name}}</th>
                                <th>Chức năng</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(tr, index) in items" :key="index+tr.name" 
                            data-aos="fade-left"
                            data-aos-easing="linear"
                            data-aos-duration="800">
                                <td v-for="(td,index) in tableheader" :key="index+td.id">
                                    <span v-if="td.id == 'description'" v-html="tr[td.id]"></span>
                                    <span v-else>{{tr[td.id]}}</span>
                                </td>
                                <td style="text-align:center; width:200px">
                                    <button class="button" title="Sửa thông tin"><i class="fa fa-edit" @click="$emit('edit',tr)"></i></button>
                                    <button class="button" title="Xem chi tiết"><i class="fa fa-eye" @click="$emit('detail',tr)"></i></button>
                                    <button class="button" title="Xóa bản ghi"><i class="fa fa-trash" @click="$emit('delete',tr)"></i></button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props:["title","tableheader","items","noAdd"]
}
</script>
<style scoped>
.view{
    width: 100%;
    padding: 12px;
    overflow: hidden;
}
.card{
    width: 100%;
    display: flex;
    flex-direction: column;
    gap: 12px;
}
.view-title{
    font-size: 30px;
    font-weight: 600;
    height: 40px;
    line-height: 40px;
    padding: 0 12px;
}
.card-item{
    background-color: #fff;
    border-radius: 8px;
    padding: 12px;
    box-shadow: 0px 1px 2px rgba(0, 0, 0, 0.1), 0px 1px 9px rgba(0, 0, 0, 0.06),
    0px 3px 5px rgba(0, 0, 0, 0.07);
}
.card-item-header{
    display: flex;
    justify-content: space-between;
    border-bottom: 2px solid #ccc;
    margin-bottom: 8px;
    padding: 0 0 6px 0;
}
.card-itm-header-title{
    height: 40px;
    line-height: 40px;
    font-size: 22px;
    
}
.card-item-content{
    display: flex;
    gap: 12px;
}
.card-item-content input{
    width: 400px;
}
table{
    border-collapse: collapse;
    border: 1px solid #ccc;
    width: 100%;
}
table tr{
    height: 50px;
    border-bottom: 1px solid #ccc;
    transition: all linear .3s;
}
table tr:hover{
   background: #eee; 
}
table tr:nth-child(2n){
    background: #eee;
}
table th{
    min-width: 120px;
    padding: 0 12px;
    border-right: 1px dotted #ccc;
    
}
table td{
    padding: 0 12px;
    border-right: 1px dotted #ccc;
    width: auto;
}
table button{
    min-width: 40px;
    width: 40px;
    margin: 0 6px;
    padding: 0;
    text-align: center;
}
</style>