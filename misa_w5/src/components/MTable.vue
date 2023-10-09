<template>
  <div class="m-table__body">
    <table>
      <thead>
        <tr>
          <th>
            <span
              class="icon24 checkmark checkmark-all"
              :class="setClassCheckboxAll(selectedAwards, displayAwards)"
              @click="onClickSelectAll"
            ></span>
          </th>
          <th
            v-for="(item, index) in columns"
            :key="index"
            :style="{ minWidth: item.width }"
            :title="item.name"
            @click="onSort(item.field)"
          >
            <div class="column-name">
              <p>{{ item.name }}</p>
              <div
                class="icon24 icon--sort"
                :class="
                  sortField === item.field && sortType === true
                    ? 'asc'
                    : sortField === item.field && sortType === false
                    ? 'desc'
                    : ''
                "
              ></div>
            </div>
          </th>
        </tr>
      </thead>
      <MLoading v-if="showLoading" />
      <div v-if="showNoData" class="no-data">{{ $_MISAResource.table.noData }}</div>
      <tbody v-if="!showNoData">
        <tr
          v-for="award in displayAwards"
          :key="award.awardId"
          :class="{ 'row-clicked': clickedId === award.awardId }"
          @click="onClickTr(award.awardId)"
          @mouseleave="onCloseMoreList"
        >
          <td>
            <span
              class="icon24 checkmark"
              :class="{
                checked: selectedAwards.some((obj) => obj.awardId === award.awardId),
              }"
              @click="onSelectCheckbox(award)"
            ></span>
            <!-- Hai nút action hiện lên khi hover vào một dòng -->
            <div class="row-action">
              <MPopper placement="top" hover arrow :content="$_MISAResource.tooltip.edit"
                ><div class="icon24 action--edit" @click="onClickEditButton(award)"></div
              ></MPopper>
              <MPopper placement="top" hover arrow :content="$_MISAResource.tooltip.more"
                ><div class="icon24 action--more" @click="onClickMoreButton"></div
              ></MPopper>
              <ul class="more-list" v-show="showMoreList">
                <li
                  :class="{
                    disabled: award.awardStatus === $_MISAEnum.AWARD_STATUS.ACTIVE,
                  }"
                  @click="onActiveInMore(award)"
                >
                  {{ $_MISAResource.contextMenu.active }}
                </li>
                <li
                  :class="{
                    disabled: award.awardStatus === $_MISAEnum.AWARD_STATUS.INACTIVE,
                  }"
                  @click="onInactiveInMore(award)"
                >
                  {{ $_MISAResource.contextMenu.inactive }}
                </li>
                <li @click="onDeleteAwardInMore(award)" style="color: red">
                  {{ $_MISAResource.contextMenu.delete }}
                </li>
              </ul>
            </div>
          </td>

          <td
            v-for="(item, index) in columns"
            :key="index"
            :class="[
              index === 5 && award[item.field] === $_MISAEnum.AWARD_STATUS.ACTIVE ? 'active' : '',
              index === 5 && award[item.field] === $_MISAEnum.AWARD_STATUS.INACTIVE
                ? 'inactive'
                : '',
            ]"
            @dblclick="onClickEditButton(award)"
            :title="convertValue(item.field, award[item.field])"
          >
            {{ convertValue(item.field, award[item.field]) }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <MPaging />
</template>

<script>
import MPaging from "@/components/MPaging.vue";
import MLoading from "@/components/MLoading.vue";

import convertData from "@/utils/conversion.js";

import { mapActions, mapState } from "vuex";

export default {
  name: "MTable",
  props: {
    columns: Array,
  },
  components: { MPaging, MLoading },
  data() {
    return {
      clickedId: -1,
      showMoreList: false,
      showLoading: true,
    };
  },
  computed: {
    ...mapState("awardStore", [
      "displayAwards",
      "selectedAwards",
      "pageSize",
      "pageCount",
      "currentPage",
      "recordRange",
      "sortField",
      "sortType",
    ]),
    showNoData() {
      return this.displayAwards.length === 0;
    },
  },
  async created() {
    await this.$store.dispatch("awardStore/getDisplayAwards");
    await this.$store.dispatch("awardStore/refreshDisplay");
    this.showLoading = false;
  },
  methods: {
    /**
     * Sự kiện click vào 1 dòng trên bảng thì đổi màu
     * Created by: ntlong ( 02/07/2023 )
     */
    onClickTr(awardId) {
      this.clickedId = awardId;
    },

    /**
     * Sự kiện click ô checkbox chọn/bỏ tất cả ở thead
     * Created by: ntlong ( 06/07/2023 )
     */
    ...mapActions("awardStore", ["onClickSelectAll"]),

    /**
     * Sự kiện click vào một ô checkbox ở tbody
     * Created by: ntlong ( 28/06/2023 )
     */
    ...mapActions("awardStore", ["onSelectCheckbox"]),

    /**
     * Hàm kiểm tra xem ô checkbox-all ở thead đang ở trạng thái nào
     * Created by: ntlong ( 28/06/2023 )
     */
    setClassCheckboxAll() {
      var unSelectedCount = 0; // Biến đếm số bản ghi không được chọn
      var displayCount = this.displayAwards?.length;
      for (let i = 0; i < displayCount; i++) {
        if (!this.selectedAwards.some((item) => item.awardId === this.displayAwards[i].awardId)) {
          unSelectedCount++;
        }
      }
      var className;
      if (unSelectedCount === 0) {
        className = "checked-all"; // Chọn tất cả
      } else if (unSelectedCount === displayCount) {
        className = ""; // Không bản ghi nào được chọn
      } else {
        className = "checked-multiple"; // Chọn nhiều
      }
      return className;
    },

    /**
     * Hàm kiểm tra xem ô checkbox của 1 bản ghi có đang được chọn không
     * Created by: ntlong ( 28/06/2023 )
     */
    isCheckedAward(selectedAwards, award) {
      selectedAwards.forEach((item) => {
        if (item.awardId === award.awardId) {
          return true;
        }
      });
      return false;
    },

    /**
     * Sự kiện mở form sửa
     * Created by: ntlong ( 28/06/2023 )
     */
    async onClickEditButton(award) {
      await this.$store.commit("awardStore/setAwardForEdit", award);
      await this.$store.commit("awardStore/setShowEditForm", true);
    },

    /**
     * Sự kiện chọn nút mở rộng ở từng bản ghi
     * Created by: ntlong ( 28/06/2023 )
     */
    onClickMoreButton() {
      this.showMoreList = !this.showMoreList;
    },

    /**
     * Sự kiện chọn đóng chức năng mở rộng ở từng bản ghi
     * Created by: ntlong ( 28/06/2023 )
     */
    onCloseMoreList() {
      this.showMoreList = false;
    },

    /**
     * Sự kiện chuyển trạng thái bản ghi thành 'Sử dụng' ở chức năng mở rộng
     * Created by: ntlong ( 11/07/2023 )
     */
    async onActiveInMore(award) {
      if (award.awardStatus !== this.$_MISAEnum.AWARD_STATUS.ACTIVE) {
        award.awardStatus = this.$_MISAEnum.AWARD_STATUS.ACTIVE;
        await this.$store.dispatch("awardStore/updateOneAward", award);
        this.$store.commit("setShowToast", true);
        this.showMoreList = false;
      }
    },

    /**
     * Sự kiện chuyển trạng thái bản ghi thành 'Ngừng sử dụng' ở chức năng mở rộng
     * Created by: ntlong ( 11/07/2023 )
     */
    async onInactiveInMore(award) {
      if (award.awardStatus !== this.$_MISAEnum.AWARD_STATUS.INACTIVE) {
        award.awardStatus = this.$_MISAEnum.AWARD_STATUS.INACTIVE;
        await this.$store.dispatch("awardStore/updateOneAward", award);
        this.$store.commit("setShowToast", true);
        this.showMoreList = false;
      }
    },

    /**
     * Sự kiện ấn vào nút xóa 1 bản ghi ở chức năng mở rộng
     * Created by: ntlong ( 11/07/2023 )
     */
    onDeleteAwardInMore(award) {
      this.showMoreList = false;
      if (award.isSystemData === true) {
        this.$store.commit("awardStore/setSelectedSystemData", [award]);
        this.$store.commit("setDialogType", this.$_MISAResource.dialog.type.notify);
        this.$store.commit("setShowDialog", true);
      } else {
        this.$store.commit("setDeletedAward", award);
        this.$store.commit("setDialogType", this.$_MISAResource.dialog.type.deleteOne);
        this.$store.commit("setShowDialog", true);
      }
    },

    /**
     * Convert dữ liệu từ enum sang text để hiển thị ra bảng
     * @param field: tên cột
     * @param value: Giá trị cần convert
     * Created by: ntlong ( 15/07/2023 )
     */
    convertValue(field, value) {
      return convertData(field, value);
    },

    /**
     * Gọi sự kiện sắp xếp
     * Created by: ntlong ( 01/08/2023 )
     */
    onSort(sortField) {
      this.$store.dispatch("awardStore/handleSort", sortField);
    },

    /**
     * Tính toán để trả về loại sắp xếp nào (tăng, giảm)
     * Created by: ntlong ( 01/08/2023 )
     */
    getSortTypeIcon(field) {
      if (this.sortField === field) {
        return "asc";
      } else {
        return "desc";
      }
    },
  },
};
</script>

<style lang="scss" scoped>
// @import "@/styles/base/table.css";

.m-table__body {
  width: 100%;
  height: calc(100% - 54px - 40px);
  background-color: #fff;
  border-bottom: 1px solid #ddd;
  overflow: auto;
}

.m-table table {
  font-size: 14px;
  background-color: #fff;
  border-collapse: separate;
  border-spacing: 0;
}

.m-table table thead {
  position: sticky;
  top: 0;
  text-align: left;
  background: #fff;
  z-index: 2;

  & tr {
    height: 45px;
    &:hover {
      cursor: pointer;
    }
  }
}

.no-data {
  position: absolute;
  color: #9e9fab;
  display: flex;
  align-items: center;
  padding: 0 16px;
  height: 40px;
}

th {
  & .column-name {
    display: flex;
    justify-content: flex-start;
    align-items: center;
    & p {
      font-weight: 600;
    }

    & .icon24.icon--sort {
      margin-left: 5px;

      &::before {
        background: none;
      }

      &.asc::before {
        width: 10px;
        height: 12px;
        background: url("@/assets/img/sprites.svg") -163px -122px;
        position: absolute;
        top: 6px;
        left: 7px;
      }

      &.desc::before {
        width: 10px;
        height: 12px;
        background: url("@/assets/img/sprites.svg") -147px -122px;
        position: absolute;
        top: 6px;
        left: 7px;
      }
    }
  }
}

th,
td {
  border-right: 1px solid var(--border-color-default);
  border-bottom: 1px solid var(--border-color-default);
}

.icon24 {
  &::before {
    background-image: url("@/assets/icons/ms-icons/ic_checkbox_gray.svg");
  }
}

.checkmark {
  position: absolute;
  top: 8px;
  left: 16px;

  &.checkmark-all {
    position: absolute;
    top: 11px;
    left: 16px;

    &.checked-multiple {
      top: 15px;
      left: 20px;
    }

    &.checked-multiple::before {
      background-image: url("@/assets/img/sprites2.svg");
      background-position: -544px -144px;
    }

    &.checked-all::before {
      background-image: url("@/assets/icons/ms-icons/ic_checkbox_active.svg");
    }
  }

  &:hover::before {
    background-image: url("@/assets/icons/ms-icons/ic_checkbox_hover.svg");
  }

  &.checked {
    &::before {
      background-image: url("@/assets/icons/ms-icons/ic_checkbox_active.svg");
    }
  }
}

tr > :nth-child(1) ~ * {
  padding: 0 16px;
}

.m-table tbody tr {
  height: 40px;
}

.m-table th:nth-child(1) {
  width: 56px;
  min-width: 56px;
}

.m-table th:nth-child(2) {
  width: 70%;
}

tbody tr.row-clicked > * {
  background-color: #e0ebff;
}

tbody tr.row-clicked:hover > * {
  background-color: #e0ebff;
}

tbody tr:hover > * {
  cursor: pointer;
  background-color: #fbe9e7;
}

tr > :nth-child(1) {
  width: 57px;
  position: sticky;
  left: 0;
  background-color: #fff;
}

/* Ô checkbox ở cột đầu tiên trong bảng */
tr > :nth-child(1) > input {
  display: block;
  width: 14px;
  height: 24px;
  margin: 0 auto;
  text-align: center;
  border-radius: 50%;
}

tr > :nth-child(1) > input:hover {
  cursor: pointer;
  color: blue;
}

thead tr:first-child th {
  border-top: none;
}

tr > :nth-child(1) {
  border-left: none;
}

tbody tr:hover .row-action {
  display: flex;
  justify-content: center;
  align-items: center;
  column-gap: 8px;
}

tr .row-action {
  display: none;
  padding: 0;
  position: absolute;
  left: calc(100vw - 340px);
  top: 1px;
}

.main-content.zoom-in .row-action {
  left: calc(100vw - 190px);
}

.m-table .action--edit,
.m-table .action--more {
  display: block;
  width: 36px;
  height: 36px;
  background-color: #fff;
  border: 1px solid var(--border-color-default);
  border-radius: 50%;
  position: relative;
}

.m-table .action--edit::before,
.m-table .action--more::before {
  background-repeat: no-repeat;
  background-image: url(@/assets/img/sprites.svg);
  position: absolute;
}

.m-table .action--edit::before {
  background-position: -4px -76px;
  width: 16px;
  height: 16px;
  left: 9px;
  top: 9px;
}

.m-table .action--more::before {
  background-position: -171px -58px;
  width: 18px;
  height: 4px;
  left: 8px;
  top: 15px;
}

.row-action ul.more-list {
  list-style: none;
  padding: 0;
  margin: 12px;
  position: absolute;
  top: 25px;
  right: 0;
  background-color: #fff;
  border-radius: 4px;
  box-shadow: 0 0 16px rgba(23, 27, 42, 0.24);

  & li {
    display: list-item;
    white-space: nowrap;
    padding: 12px 36px 12px 24px;

    &:hover {
      background-color: #e0ebff;
    }

    &.disabled {
      opacity: 0.4;
      background-color: #fff;
    }
  }
}

// Cột cuối cùng
.m-table tbody tr > :last-child {
  &.active {
    &::before {
      display: inline-block;
      content: "";
      width: 8px;
      height: 8px;
      border-radius: 50%;
      margin-right: 10px;
      background-color: var(--color-primary);
    }
  }

  &.inactive {
    &::before {
      display: inline-block;
      content: "";
      width: 8px;
      height: 8px;
      border-radius: 50%;
      margin-right: 10px;
      background-color: #9e9e9e;
    }
  }
}
</style>
