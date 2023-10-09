<template>
  <div class="misa-paging">
    <div class="paging__left">
      <span
        >Tổng số: <strong>{{ totalRecords }}</strong> bản ghi</span
      >
    </div>
    <div class="paging__right">
      <div>Số bản ghi/trang</div>
      <MCombobox
        :options="numberRecordList"
        position="show-above"
        readonly
        :hasIconTick="true"
        v-model="size"
      />
      <span class="record-range"
        ><strong>{{ recordRange.recordFrom }}</strong
        ><span> - </span> <strong>{{ recordRange.recordTo }}</strong> bản ghi</span
      >
      <div
        class="icon24 icon--back-page"
        :class="{ disabled: currentPage === 1 }"
        @click="onPreviousPage"
      ></div>
      <div
        class="icon24 icon--next-page"
        :class="{ disabled: currentPage === pageCount }"
        @click="onNextPage"
      ></div>
    </div>
  </div>
</template>

<script>
import MCombobox from "@/components/MCombobox.vue";
import { mapState } from "vuex";

export default {
  name: "MPaging",
  components: {
    MCombobox,
  },
  data() {
    return {
      numberRecordList: [
        { value: 10, name: 10 },
        { value: 20, name: 20 },
        { value: 50, name: 50 },
        { value: 100, name: 100 },
      ],
      size: 10,
    };
  },
  computed: {
    ...mapState("awardStore", [
      "awards",
      "totalRecords",
      "pageSize",
      "pageCount",
      "currentPage",
      "recordRange",
    ]),
  },
  mounted() {
    this.$store.commit("awardStore/setPageSize", 10);
  },
  watch: {
    size(newValue) {
      this.$store.commit("awardStore/setPageSize", newValue);
      this.$store.dispatch("awardStore/refreshDisplay");
    },
  },
  methods: {
    /**
     * Sự kiện click chuột vào ô sang trang trước
     * Author: NTLong (04/07/2023)
     */
    onPreviousPage() {
      if (this.currentPage > 1) {
        this.$store.commit("awardStore/setCurrentPage", this.currentPage - 1);
        this.$store.dispatch("awardStore/refreshDisplay");
      }
    },
    /**
     * Sự kiện click chuột vào ô sang trang sau
     * Author: NTLong (04/07/2023)
     */
    onNextPage() {
      if (this.currentPage < this.pageCount) {
        this.$store.commit("awardStore/setCurrentPage", this.currentPage + 1);
        this.$store.dispatch("awardStore/refreshDisplay");
      }
    },
  },
};
</script>

<style lang="scss" scoped>
.misa-paging {
  max-width: 100%;
  height: 40px;
  padding: 0 16px;
  background-color: #fff;
  border-bottom-left-radius: 10px;
  border-bottom-right-radius: 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;

  & .paging__right {
    display: flex;
    justify-content: flex-end;
    align-items: center;

    & .misa-combobox {
      width: 85px;
      margin: 0 16px;
    }

    & .record-range {
      font-weight: 400;
    }

    & .icon24:hover {
      cursor: pointer;
    }

    & .icon--back-page::before {
      background-position: -240px 0;
      transform: rotate(180deg);
    }

    & .icon--back-page.disabled::before {
      opacity: 0.4;
    }

    & .icon--back-page {
      margin: 0 10px 0 20px;

      &:hover {
        background-color: #f3f3f3;
      }
    }

    & .icon--back-page.disabled {
      cursor: auto;
      background-color: #fff;
    }

    & .icon--next-page:hover {
      background-color: #f3f3f3;
    }

    & .icon--next-page::before {
      background-position: -240px 0;
    }

    & .icon--next-page.disabled::before {
      opacity: 0.4;
    }

    & .icon--next-page.disabled {
      cursor: auto;
      background-color: #fff;
    }
  }
}
</style>
