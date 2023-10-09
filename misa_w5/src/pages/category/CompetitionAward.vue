<template>
  <div class="page-container">
    <div class="page__title">{{ $_MISAResource.pageTitle }}</div>
    <div class="page__content">
      <div class="m-table">
        <div class="m-table__toolbar">
          <div class="toolbar__left">
            <div class="search-group">
              <MInput
                :placeholder="$_MISAResource.placeholder.search"
                hasIcon="has-icon"
                v-model="searchValue"
                @keydown.enter="onSearch"
                @onClickIconInput="onSearch"
              />
              <span
                class="icon24 icon--close"
                :class="{
                  'has-text': searchValue !== null && searchValue !== '',
                }"
                @click="deleteSearchValue"
              ></span>
            </div>
            <div class="filter-group">
              <MButton
                nameIcon="btn--filter"
                :class="{ 'filter-active': filterActive }"
                variant="btn-outline-secondary"
                :title="$_MISAResource.button.filter"
                @click="onClickFilterButton"
              />
              <FilterBox v-show="showFilterBox" />
              <!-- Nút bỏ lọc chỉ hiện khi bảng được lọc -->
              <MButton
                v-if="filterActive"
                class="unfiltered"
                variant="btn-link"
                :title="$_MISAResource.button.unfilter"
                @click="onUnfilteredBtn"
              />
            </div>
          </div>
          <div class="toolbar__right">
            <!-- Khi chưa select vào ô checkbox nào -->
            <div class="toolbar-right__default" v-if="selectedAwards.length === 0">
              <MButton
                nameIcon="btn--add"
                variant="btn-primary"
                :title="$_MISAResource.button.addAward"
                @click="openAddNewForm"
              />
              <div class="more--imex" @mouseleave="showMoreAction = false">
                <MPopper
                  hover
                  placement="top"
                  arrow
                  :content="$_MISAResource.tooltip.more"
                  closeDelay="0"
                >
                  <MButton
                    nameIcon="btn--more"
                    variant="btn-outline-secondary"
                    @click="toggleImportExport"
                  />
                </MPopper>
                <ul v-show="showMoreAction">
                  <li @click="showImportPopup = true">{{ $_MISAResource.contextMenu.import }}</li>
                  <li @click="handleExportExcel">{{ $_MISAResource.contextMenu.export }}</li>
                </ul>
              </div>
            </div>

            <!-- Khi có bất kỳ ô checkbox nào được select -->
            <div class="toolbar-right__action" v-if="selectedAwards.length !== 0">
              <div class="selected-count">
                Đã chọn:
                <strong>{{ selectedAwards.length }}</strong>
              </div>
              <MButton
                variant="btn-link"
                :title="$_MISAResource.button.unselect"
                @click="onUnCheckBtn"
              />
              <MButton
                variant="btn-outline-primary"
                :title="$_MISAResource.button.active"
                @click="onActiveSelectedAwards"
              />
              <MButton
                variant="btn-outline-secondary"
                :title="$_MISAResource.button.inactive"
                @click="onInactiveSelectedAwards"
              />
              <MButton
                variant="btn-outline-danger"
                :title="$_MISAResource.button.delete"
                @click="onDeleteSelectedAwards"
              />
            </div>
          </div>
        </div>

        <MTable :columns="columns" />
      </div>
    </div>
  </div>

  <AwardForm
    v-if="showAddNewForm"
    :typeForm="$_MISAEnum.FORM_MODE.ADD"
    :title="$_MISAResource.form.title.add"
  />
  <AwardForm
    v-if="showEditForm"
    :typeForm="$_MISAEnum.FORM_MODE.EDIT"
    :title="$_MISAResource.form.title.edit"
  />
  <ImportPopup
    v-if="showImportPopup"
    :title="$_MISAResource.form.title.import"
    @close="showImportPopup = false"
  />
</template>

<script>
import AwardForm from "@/pages/category/award-form/AwardForm.vue";
import FilterBox from "@/pages/category/FilterBox.vue";
import ImportPopup from "@/pages/category/import-popup/ImportPopup.vue";

import awardService from "@/services/category/awardService.js";

import { mapState, mapMutations } from "vuex";

export default {
  name: "CompetitionAward.vue",
  components: { AwardForm, FilterBox, ImportPopup },
  data() {
    return {
      theadData: [],
      searchValue: "",
      showMoreAction: false,
      showImportPopup: false,
      columns: [
        {
          field: "awardName",
          name: this.$_MISAResource.table.columnName.name,
          width: "310px",
        },
        {
          field: "awardCode",
          name: this.$_MISAResource.table.columnName.code,
          width: "160px",
        },
        {
          field: "awardObject",
          name: this.$_MISAResource.table.columnName.object,
          width: "220px",
        },
        {
          field: "awardLevel",
          name: this.$_MISAResource.table.columnName.level,
          width: "200px",
        },
        {
          field: "awardType",
          name: this.$_MISAResource.table.columnName.type,
          width: "200px",
        },
        {
          field: "awardStatus",
          name: this.$_MISAResource.table.columnName.status,
          width: "180px",
        },
      ],
    };
  },
  computed: {
    ...mapState("awardStore", [
      "filter",
      "filterActive",
      "showAddNewForm",
      "showEditForm",
      "showFilterBox",
      "selectedAwards",
    ]),
  },
  methods: {
    /**
     * Xử lý sự kiện tìm kiếm
     * Created by: ntlong ( 02/07/2023 )
     */
    async onSearch() {
      await this.$store.commit("awardStore/setSearchText", this.searchValue);
      await this.$store.dispatch("awardStore/refreshDisplay");
    },

    /**
     * Xử lý sự kiện khi click vào icon xóa chữ ở ô tìm kiếm khi đã nhập
     * Created by: ntlong ( 11/07/2023 )
     */
    async deleteSearchValue() {
      this.searchValue = null;
      await this.$store.commit("awardStore/setSearchText", this.searchValue);
      await this.$store.dispatch("awardStore/refreshDisplay");
    },

    /**
     * Xử lý sự kiện được emit khi click vào icon ở component MInput
     * Created by: ntlong ( 11/07/2023 )
     */
    async onClickIconInput(event) {
      const value = event.target.value;
      await this.$store.commit("awardStore/setSearchText", value);
      await this.$store.dispatch("awardStore/refreshDisplay");
    },

    /**
     * Xử lý sự kiện đóng mở bộ lọc
     * Created by: ntlong ( 02/07/2023 )
     */
    onClickFilterButton() {
      this.$store.commit("awardStore/setShowFilterBox", !this.showFilterBox);
    },

    /**
     * Xử lý khi ấn nút bỏ lọc
     * Created by: ntlong ( 02/07/2023 )
     */
    onUnfilteredBtn() {
      this.$store.dispatch("awardStore/handleUnfiltered");
      this.$store.commit("awardStore/setShowFilterBox", false);
    },

    /**
     * Xử lý khi ấn vào nút thêm mới
     * Created by: ntlong ( 28/06/2023 )
     */
    ...mapMutations("awardStore", ["openAddNewForm"]),

    /**
     * Xử lý khi ấn vào nút mở rộng
     * Created by: ntlong ( 28/06/2023 )
     */
    toggleImportExport() {
      this.showMoreAction = !this.showMoreAction;
    },

    /**
     * Xử lý khi ấn vào nút bỏ chọn
     * Created by: ntlong ( 10/07/2023 )
     */
    onUnCheckBtn() {
      this.$store.commit("awardStore/unSelectAll");
    },

    /**
     * Xử lý sự kiện ấn vào nút 'Sử dụng'
     * Created by: ntlong ( 10/07/2023 )
     */
    async onActiveSelectedAwards() {
      const listIds = this.selectedAwards.map(({ awardId }) => awardId);
      await this.$store.dispatch("awardStore/updateManyAwardStatus", {
        listIds: listIds,
        newAwardStatus: this.$_MISAEnum.AWARD_STATUS.ACTIVE,
      });
      await this.$store.commit("setShowToast", true);
    },

    /**
     * Xử lý sự kiện ấn vào nút 'Ngừng sử dụng'
     * Created by: ntlong ( 10/07/2023 )
     */
    async onInactiveSelectedAwards() {
      const listIds = this.selectedAwards.map(({ awardId }) => awardId);
      await this.$store.dispatch("awardStore/updateManyAwardStatus", {
        listIds: listIds,
        newAwardStatus: this.$_MISAEnum.AWARD_STATUS.INACTIVE,
      });
      await this.$store.commit("setShowToast", true);
    },

    /**
     * Xử lý sự kiện ấn vào nút xóa các bản ghi được chọn ở checkbox
     * Created by: ntlong ( 11/07/2023 )
     */
    onDeleteSelectedAwards() {
      const arrFake = this.selectedAwards;
      const selectedSystemData = arrFake.filter((item) => item.isSystemData === true);
      if (selectedSystemData?.length === 0) {
        // Không có dữ liệu hệ thống được chọn
        this.$store.commit("setDialogType", this.$_MISAResource.dialog.type.deleteMany);
        this.$store.commit("setShowDialog", true);
      } else {
        this.$store.commit("awardStore/setSelectedSystemData", selectedSystemData);
        this.$store.commit("setDialogType", this.$_MISAResource.dialog.type.notify);
        this.$store.commit("setShowDialog", true);
      }
    },

    /**
     * Xử lý sự kiện lưu bản ghi thêm mới vào database
     * Created by: ntlong ( 04/07/2023 )
     */
    onSaveAndAdd(obj) {
      this.tbodyData.push(obj);
      this.showAddNewForm = false;
      setTimeout(() => {
        this.showAddNewForm = true;
      }, 0);
    },

    /**
     * Xử lý sự kiện lưu bản ghi thêm mới vào database ($emit)
     * Created by: ntlong ( 04/07/2023 )
     */
    onSave(obj) {
      this.tbodyData.push(obj);
      this.showAddNewForm = false;
    },

    /**
     * Xử lý sự kiện lưu thay đổi ($emit)
     * Created by: ntlong ( 04/07/2023 )
     */
    onSaveChange(newObj, oldObj) {
      this.tbodyData.forEach((award) => {
        if (award === oldObj) {
          award = newObj;
        }
      });
      this.showEditForm = false;
    },

    /**
     * Xử lý sự kiện khi click vào xuất khẩu
     * Created by: ntlong ( 04/08/2023 )
     */
    handleExportExcel() {
      awardService.exportExcel().then((file) => {
        const fileURL = window.URL.createObjectURL(file);
        // Tạo một thẻ a để download file từ URL
        const link = document.createElement("a");
        // Thiết lập thuộc tính href là fileURL
        link.href = fileURL;
        link.setAttribute("download", "Danh hieu thi dua.xlsx");
        link.click();
        window.URL.revokeObjectURL(fileURL);
      });
    },
  },
};
</script>

<style lang="scss" scoped>
@import url("@/styles/base/table.scss");
.page__title {
  height: 35px;
  font-size: 20px;
  font-weight: 700;
  line-height: 35px;
  margin-bottom: 16px;
}

.page-container {
  width: 100%;
  height: 100%;
  padding: 16px;
  box-shadow: inset 0 1.5px 2px 0 rgba(0, 0, 0, 0.1);
  background-color: #f4f5f8;
  display: flex;
  flex-direction: column;
}

.page__content {
  flex: 1;
  max-height: calc(100% - 35px - 16px);
}

:deep(.popper) {
  white-space: nowrap;
}
</style>
