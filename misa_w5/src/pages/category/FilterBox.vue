<template>
  <div class="filter-box">
    <div class="custom-arrow"></div>
    <div class="filter-container">
      <div class="filter__title">Lọc danh hiệu</div>
      <div class="icon24 btn--close" @click="onCloseFilterBox"></div>
      <div class="filter__content">
        <div>
          <MLabel :title="$_MISAResource.table.columnName.object" />
          <MCombobox :options="objectOption" v-model="attrsFilter.filteredObject" />
        </div>
        <div>
          <MLabel :title="$_MISAResource.table.columnName.level" />
          <MCombobox :options="levelOption" v-model="attrsFilter.filteredLevel" />
        </div>
        <div>
          <MLabel :title="$_MISAResource.table.columnName.type" />
          <MCombobox :options="typeOption" v-model="attrsFilter.filteredType" />
        </div>
        <div>
          <MLabel :title="$_MISAResource.table.columnName.status" />
          <MCombobox :options="statusOption" v-model="attrsFilter.filteredStatus" />
        </div>
      </div>
      <div class="filter__footer">
        <MButton
          variant="btn-outline-secondary"
          :title="$_MISAResource.button.cancel"
          @click="onCloseFilterBox"
        />
        <MButton
          variant="btn-primary"
          :title="$_MISAResource.button.apply"
          @click="handleFilterData"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "FilterBox",
  data() {
    return {
      attrsFilter: {
        filteredObject: null,
        filteredLevel: null,
        filteredType: null,
        filteredStatus: null,
      },

      objectOption: [
        { value: null, name: this.$_MISAResource.field.object.all }, // "Tất cả"
        {
          value: this.$_MISAEnum.AWARD_OBJECT.INDIVIDUAL, // "Cá nhân"
          name: this.$_MISAResource.field.object.individual,
        },
        {
          value: this.$_MISAEnum.AWARD_OBJECT.GROUP, // "Tập thể"
          name: this.$_MISAResource.field.object.group,
        },
        {
          value: this.$_MISAEnum.AWARD_OBJECT.INDIVIDUAL_OR_GROUP, // "Cá nhân và tập thể"
          name: this.$_MISAResource.field.object.individualOrGroup,
        },
        {
          value: this.$_MISAEnum.AWARD_OBJECT.FAMILY, // "Gia đình"
          name: this.$_MISAResource.field.object.family,
        },
      ],

      levelOption: [
        { value: null, name: this.$_MISAResource.field.level.all }, // "Tất cả"
        {
          value: this.$_MISAEnum.AWARD_LEVEL.NATIONAL, // "Cấp Nhà nước"
          name: this.$_MISAResource.field.level.national,
        },
        {
          value: this.$_MISAEnum.AWARD_LEVEL.PROVINCE, // "Cấp Tỉnh/tương đương"
          name: this.$_MISAResource.field.level.province,
        },
        {
          value: this.$_MISAEnum.AWARD_LEVEL.DISTRICT, // "Cấp Huyện/tương đương"
          name: this.$_MISAResource.field.level.district,
        },
        {
          value: this.$_MISAEnum.AWARD_LEVEL.COMMUNE, // "Cấp Xã/tương đương"
          name: this.$_MISAResource.field.level.commune,
        },
      ],

      typeOption: [
        { value: null, name: this.$_MISAResource.field.type.all }, // "Tất cả"
        {
          value: this.$_MISAEnum.AWARD_TYPE.FREQUENT, // "Thường xuyên"
          name: this.$_MISAResource.field.type.frequent,
        },
        {
          value: this.$_MISAEnum.AWARD_TYPE.PERIODIC, // "Theo đợt"
          name: this.$_MISAResource.field.type.periodic,
        },
      ],

      statusOption: [
        { value: null, name: this.$_MISAResource.field.status.all }, // "Tất cả"
        {
          value: this.$_MISAEnum.AWARD_STATUS.ACTIVE, // "Sử dụng"
          name: this.$_MISAResource.field.status.active,
        },
        {
          value: this.$_MISAEnum.AWARD_STATUS.INACTIVE, // "Ngừng sử dụng"
          name: this.$_MISAResource.field.status.inactive,
        },
      ],
    };
  },
  computed: {
    ...mapState("awardStore", ["filterActive", "filter"]),
  },
  watch: {
    filterActive(newValue) {
      if (newValue === false) {
        // Thay đổi trạng thái combobox khi bỏ lọc
        this.attrsFilter.filteredObject = null;
        this.attrsFilter.filteredLevel = null;
        this.attrsFilter.filteredType = null;
        this.attrsFilter.filteredStatus = null;
      }
    },
    filter(newValue) {
      if (newValue === null) {
        this.attrsFilter.filteredObject = null;
        this.attrsFilter.filteredLevel = null;
        this.attrsFilter.filteredType = null;
        this.attrsFilter.filteredStatus = null;
      }
    },
  },
  methods: {
    /**
     * Sự kiện đóng filter box
     * Created by: ntlong ( 28/06/2023 )
     */
    onCloseFilterBox() {
      this.$store.commit("awardStore/setShowFilterBox", false);
    },

    /**
     * Sự kiện ấn nút áp dụng lọc dữ liệu
     * Created by: ntlong ( 04/07/2023 )
     */
    handleFilterData() {
      this.$store.dispatch("awardStore/handleFilterData", this.attrsFilter);
    },
  },
};
</script>

<style lang="scss" scoped>
.filter-box {
  position: absolute;
  top: 50px;
  left: -275px;
  width: 360px;
  height: 440px;
  z-index: 4;
  background-color: #ffffff;
  text-align: start;
  box-shadow: 0 0 16px rgba(23, 27, 42, 0.24);

  & .custom-arrow {
    position: absolute;
    top: -16px;
    right: 57px;
    width: 0;
    border-width: 8px;
    border-style: solid;
    border-color: transparent transparent #fff transparent;
  }

  & .filter-container {
    width: 100%;
    position: absolute;
    top: 0px;

    & .filter__title {
      width: 100%;
      padding: 12px 24px 8px;
      text-align: start;
      font-size: 18px;
      font-weight: 700;
    }

    & .btn--close {
      position: absolute;
      top: 12px;
      right: 24px;

      &:hover {
        cursor: pointer;
      }

      &::before {
        content: "";
        position: absolute;
        top: 6px;
        left: 6px;
        background: url("@/assets/img/sprites2.svg") no-repeat -198px -54px;
        width: 12px;
        height: 12px;
      }
    }

    & .filter__content {
      width: 100%;
      height: 334px;
      padding: 12px 24px 24px;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;

      & div {
        width: 100%;
      }

      & .lable-input {
        color: #666;
      }
    }

    & .filter__footer {
      text-align: right;
      padding: 12px 24px;
      border-top: 1px solid var(--border-color-default);

      & > * {
        display: inline-block;
        margin-left: 10px;
      }
    }
  }
}
</style>
