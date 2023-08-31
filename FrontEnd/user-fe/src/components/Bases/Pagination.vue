<template>
  <ul class="page-list">
    <li
      @click="prevPage"
      :class="['page-item', page == 1 && 'page-item--disabled']"
    >
      Trước
    </li>
    <li
      :class="[
        'page-item',
        p == page && 'page-item--active',
        p == '...' && 'page-item--dot',
      ]"
      v-for="(p, index) in pages"
      :key="index"
      @click="setPage(p)"
    >
      {{ p }}
    </li>
    <li
      @click="nextPage"
      :class="['page-item', page == pageCount && 'page-item--disabled']"
    >
      Sau
    </li>
  </ul>
</template>
<script>
export default {
  props: {
    //tổng số bản ghi 
    itemCount: Number,

    // số bản ghi trên 1 trang
    perPage: Number,

    //số trang hiển thị tối đa
    maxDisplayPage: {
      type: Number,
      default: 3,
    },
    
    // trang hiện tại
    page: {
      type: Number,
      default: 1,
    },
  },
  emits: ["pageChange"],
  methods: {
    resetPage(){
      this.setPage(1);
    },
    setPage(p) {
      this.$emit("pageChange", p);
    },
    // chuyển đến page kế tiếp
    nextPage() {
      this.setPage(Math.min(this.page + 1, this.pageCount));
    },
    //chuyển về trang trước đó
    prevPage() {
      this.setPage(Math.max(this.page - 1, 1));
    },
  },
  computed: {
    //lấy số trang hiển thị tối đa
    pageCount() {
      return Math.ceil(this.itemCount / this.perPage);
    },
    //tính toán ra mảng danh sách trang để hiển thị
    pages() {
      let sp = Math.max(1, this.page - ~~(this.maxDisplayPage / 2)); //trang bắt đầu hiển thị
      let ep = Math.min(this.pageCount, sp + this.maxDisplayPage - 1); //trang kết thúc hiển thị
      //nếu số trang chứa dữ liệu hiện tại mà bé hơn số trang hiển thị tối đa
      if (ep - sp + 1 < this.maxDisplayPage) {
        if (sp == 1) {
          ep = Math.min(this.pageCount, sp + this.maxDisplayPage - 1);
        } else if (ep == this.pageCount) {
          sp = Math.max(1, ep - this.maxDisplayPage + 1);
        }
      }
      const pages = Array.from({ length: ep - sp + 1 }, (_, i) => sp + i); //tạo ra mảng pages [sp..ep] để hiển thị
      if (sp > 2) {
        //thêm ... vào đầu nếu sp>2
        pages.unshift("...");
      }
      if (sp > 1) {
        //thêm 1 nếu sp>1
        pages.unshift(1);
      }
      if (ep < this.pageCount - 1) {
        //thêm ... vào cuối nếu ep<pagecount-1
        pages.push("...");
      }
      if (ep < this.pageCount) {
        //thêm trang cuối cùng nếu ep<pagecount
        pages.push(this.pageCount);
      }
      return pages;
    },
  },
  watch: {
    //số bản ghi trên 1 trang thay đổi
    perPage() {
      this.setPage(1);
    },
    page(val) {
      this.setPage(val);
    },
  },
};
</script>
<style>
.page-list {
    display: flex;
    align-items: center;
    list-style: none;
}

.page-item {
    cursor: pointer;
    padding: 0 0.5rem;
}

.page-item--active {
    border: 2px solid #2ca01c;
    pointer-events: none;
    color: #000;
    border-radius: 4px;
}

.page-item--dot {
    pointer-events: none;
}

.page-item--disabled {
    pointer-events: none;
    color: #9e9e9e;
}
</style>
