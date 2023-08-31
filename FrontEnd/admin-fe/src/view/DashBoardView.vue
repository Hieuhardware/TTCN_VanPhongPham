<template>
    <div>
        <div class="view">
            <div class="view-title"
            data-aos="fade-left"
            data-aos-duration="1500">
                Trang chủ
            </div>
            <div class="card">
                <div class="card-item">
                    <div class="card-item-header"
                    data-aos="fade-right"
                    data-aos-duration="1500">
                        <div class="card-itm-header-title">Thống kê chung</div>
                    </div>
                    <div class="card-item-content">
                        <div class="report-container">
                            <div class="report report-1"
                            data-aos="fade-down-right"
                            data-aos-duration="1500"
                            >
                            <div class="report-icon">
                                <i class="fas fa-cart-plus"></i>
                            </div>
                            <div class="report-title">
                                Đơn hàng trong tháng
                            </div>
                            <div class="report-number">
                                {{dataTotal.order}} đơn hàng mới
                            </div>
                            <div class="report-detail">
                                <router-link to="/order" class="navbar-item">
                                    <span>Chi tiết </span>
                                    <i class="fas fa-arrow-alt-circle-right"></i>
                                </router-link>
                            </div>
                        </div>
                        <div class="report report-2"
                        data-aos="fade-down-right"
                            data-aos-duration="1000">
                            <div class="report-icon">
                                <i class="fas fa-book"></i>
                            </div>
                            <div class="report-title">
                                Số lượng sản phẩm
                            </div>
                            <div class="report-number">
                                {{dataTotal.product}} sản phẩm
                            </div>
                            <div class="report-detail">
                                <router-link to="/product" class="navbar-item">
                                    <span>Chi tiết </span>
                                    <i class="fas fa-arrow-alt-circle-right"></i>
                                </router-link>
                            </div>
                        </div>
                        <div class="report report-3"
                        data-aos="fade-down-left"
                            data-aos-duration="1000">
                            <div class="report-icon">
                                <i class="fas fa-users"></i>
                            </div>
                            <div class="report-title">
                                Khách hàng mới trong tháng
                            </div>
                            <div class="report-number">
                                {{dataTotal.customer}} khách hàng mới
                            </div>
                            <div class="report-detail">
                                <router-link to="/customer" class="navbar-item">
                                    <span>Chi tiết </span>
                                    <i class="fas fa-arrow-alt-circle-right"></i>
                                </router-link>
                            </div>
                        </div>
                        <div class="report report-4"
                        data-aos="fade-down-left"
                            data-aos-duration="1500">
                            <div class="report-icon">
                                <i class="fas fa-coins"></i>
                            </div>
                            <div class="report-title">
                                Doanh thu tháng gần đây
                            </div>
                            <div class="report-number">
                                {{dataTotal.money}} vnd
                            </div>
                            <div class="report-detail">
                                <router-link to="/order" class="navbar-item">
                                    <span>Chi tiết </span>
                                    <i class="fas fa-arrow-alt-circle-right"></i>
                                </router-link>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
                <div class="card-item">
                    <div class="card-item-header"
                    data-aos="fade-right"
                    data-aos-duration="1500">
                        <div class="card-itm-header-title">Biểu đồ thống kê chi tiết</div>
                        <div class="time-button">
                            <button class="button" @click="renderMonthReport()">Báo cáo theo tháng</button>
                            <button class="button" @click="renderYearReport()">Báo cáo theo năm</button>
                        </div>
                    </div>
                    <div class="card-item-content">
                        <div class="time-filter"
                        data-aos="fade-left"
                        data-aos-duration="1500">
                            <div class="time">
                                {{mainTitle}}
                            </div>
                                <div class="time-container">
                                    <div class="time">
                                    <md-field>
                                        <label>Tháng</label>
                                            <md-select
                                                v-model="timeFilter.month"
                                                @md-selected="renderMonthReport"
                                            >
                                                <md-option
                                                    :value="item.id"
                                                    v-for="(item, index) in monthNumber"
                                                    :key="index+item.name"
                                                >
                                                    {{ item.name }}
                                                </md-option>
                                            </md-select>
                                    </md-field>
                                </div>
                                <div class="time">
                                    <md-field>
                                        <label>Năm</label>
                                            <md-select
                                            v-model="timeFilter.year"
                                            @md-selected="renderYearReport"
                                            >
                                            <md-option
                                                :value="item.id"
                                                v-for="(item, index) in yearNumber"
                                                :key="index+item.id"
                                            >
                                                {{ item.name }}
                                            </md-option>
                                            </md-select>
                                    </md-field>
                                </div>
                            </div>
                            
                        </div>
                        <div>
                            <Chart :chartData="chartData" :chartOptions="options" :hasChart="hasChart"></Chart>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <Loader v-if="hasLoader"></Loader>
        <ErrorPopup @close="close" :title="title"  v-if="hasError"></ErrorPopup>
    </div>
</template>
<script>
import Loader from '@/components/Bases/Loader'
import ErrorPopup from '@/components/Bases/BasePopup/ErrorPopup'
import Chart from '@/view/Chart'
import api from '@/js/api'
export default {
    components:{
        Loader, ErrorPopup, Chart
    },
    data() {
        return {
            mainTitle:"",
            dataTotal:{},
            hasLoader:false,
            hasError:false,
            monthNumber: [],
            yearNumber:[],
            monthList:["Tháng 1","Tháng 2","Tháng 3","Tháng 4","Tháng 5","Tháng 6","Tháng 7","Tháng 8","Tháng 9","Tháng 10","Tháng 11","Tháng 12",],
            timeFilter:{
                month:"",
                year:""
            },
            chartData: {
                labels: [],
                datasets: [{
                label: 'Số đơn hàng',
                backgroundColor: ['#3cba9f'],
                data: [63.2, 52.1, 73.4, 78.4, 80.5, 79.2, 82.1]
                },
                {
                    label: 'Giá trị (Triệu VND)',
                backgroundColor: ['#8e5ea2'],
                data: [63.2, 52.1, 73.4, 78.4, 80.5, 79.2, 82.1]
                }
                ]
            },
            options: {
                title: {
                display: true,
                text: 'Đồ thị tình hình bán hàng theo thời gian'
                },
                responsive: true,
                aspectRatio: false
            },
            hasChart:false
        }
    },
    created(){
        let date = new Date();
        let nowMonth = date.getMonth()+1;
        for( let i =1; i<13; i++){
            this.monthNumber.push({id:i,name:`Tháng ${i}`})
        }
        this.timeFilter.month=nowMonth;
        let nowYear = date.getFullYear();
        for(let i = nowYear; i>2000; i--){
            this.yearNumber.push({id:i,name:i})
        }
        this.timeFilter.year= nowYear
        this.getDataTotal();
        this.renderMonthReport();
    },
    methods:{
        getDataTotal(){
            this.hasLoader = true;
            this.$axios.get(`${api.AdminApi}/datatotal`,{params:{
                month: this.timeFilter.month,
                year: this.timeFilter.year
            }}).then(res=>{
                this.dataTotal = res.data;
                console.log(this.dataTotal);
            }).finally(()=>this.hasLoader =false)
        },
        close(){
            this.hasError=false
        },
        async renderMonthReport(){
            this.hasChart = false
            let numberDate = this.daysInMonth(this.timeFilter.month,this.timeFilter.year);
            let dates =[]
            for (let index = 1; index <= numberDate; index++) {
                dates.push(index)
            }
            this.hasLoader = true;
            await this.$axios.get(`${api.AdminApi}/datachart/month`,{params:{
                day: numberDate,
                month: this.timeFilter.month,
                year: this.timeFilter.year
            }}).then(res=>{
                console.log(res.data);
                this.chartData.datasets[0].data = res.data.order;
                this.chartData.datasets[1].data = res.data.money.map(item=>item/1000000);
            }).finally(()=>this.hasLoader=false)
            this.chartData.labels = dates
            this.hasLoader=true
            setTimeout(() => {
                this.hasChart = true;
                this.mainTitle= `Biểu đồ thống kê theo tháng ${this.timeFilter.month} năm ${this.timeFilter.year}`
                this.hasLoader=false
            }, 500);
        },
        async renderYearReport(){
            this.hasChart = false
            this.hasLoader=true
            await this.$axios.get(`${api.AdminApi}/datachart/year`,{params:{
                year:this.timeFilter.year
            }}).then(res=>{
                this.chartData.datasets[0].data = res.data.order
                this.chartData.datasets[1].data = res.data.money.map(item=>item/1000000);
            }).finally(()=>this.hasLoader=false)
            this.chartData.labels = this.monthList;
            this.hasLoader =true;
            setTimeout(() => {
                this.hasChart = true;
                this.mainTitle= `Biểu đồ thống kê theo năm ${this.timeFilter.year}`
                this.hasLoader=false
            }, 500);
            
        },
        daysInMonth(month, year) {
            return new Date(year, month, 0).getDate();
        }
    },

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
    flex-wrap: wrap;
}
.report-container{
    display: flex;
    width: 100%;
    justify-content: center;
    align-items: center;
    gap: 12px;
}
.report{
    flex-basis: 25%;
    min-height: 250px;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    border-radius: 8px;
}
.report-1{
    background: rgb(30,221,240);
    background: linear-gradient(180deg, rgba(30,221,240,0.8) 0%, rgba(247,104,61,0.8) 60%, rgba(247,23,23,0.8) 100%);
}
.report-2{
    background: rgb(13,233,0);
    background: linear-gradient(207deg, rgba(13,233,0,0.8) 0%, rgba(153,255,129,0.8) 49%, rgba(12,166,0,0.8) 100%);
}
.report-3{
    background: rgb(237,38,16);
    background: linear-gradient(63deg, rgba(237,38,16,0.8) 0%, rgba(255,129,216,0.8) 49%, rgba(228,201,44,0.8) 100%);
}
.report-4{
    background: rgb(16,151,237);
    background: linear-gradient(111deg, rgba(16,151,237,0.8) 0%, rgba(129,255,196,0.8) 49%, rgba(0,252,236,0.8) 100%);
}
.report  i{
    font-size: 24px;
    color: #045ebe;
}
.report .report-icon i{
    font-size: 36px;
}
.report .report-title{
    text-align: center;
    font-size: 24px;
    color: #045ebe;
    line-height: 26px;
}
.report .report-number{
    text-align:center;
    font-size: 18px;
    color: #045ebe;
}
.choice{
    display: flex;
    gap: 12px;
}
.time-filter{
    display: flex;
    gap: 5%;
    justify-content: space-between;
    align-items: center;
    width: 100%;
    padding-right: 5%;
    color: #045ebe;
    font-size: 24px;
}
.time-container{
    display: flex;
    gap: 12px;

}
.time-button{
    display: flex;
    gap: 12px;
}
</style>