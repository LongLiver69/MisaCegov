<template>
  <div
    class="m-menu-item"
    :class="{ active: isActive }"
    @mouseenter="showTooltip"
    @mouseleave="hideTooltip"
    @click="handleClickMenuItem"
  >
    <div class="m-menu-item__icon icon24" :class="name"></div>
    <MTooltip :content="label" v-show="zoomOutSidebar && isShowTooltip" />
    <div class="m-menu-item__text" v-show="!zoomOutSidebar">
      {{ label }}
    </div>
    <div :class="{ 'has-extend': hasExtend }" v-show="!zoomOutSidebar"></div>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "MenuItem",
  data() {
    return {
      isShowTooltip: false,
      // "true": Nếu menu-item được chọn
      isActive: false,
    };
  },
  props: {
    name: String,
    // Tên m-menu-item (Không bắt buộc)
    label: String,
    // "true": Nếu Item đó có các option con (Không bắt buộc)
    hasExtend: Boolean,
  },
  computed: {
    ...mapState(["zoomOutSidebar"]),
  },
  methods: {
    /**
     * Hiển thị tooltip
     * Created by: ntlong ( 12/08/2023 )
     */
    showTooltip() {
      this.isShowTooltip = true;
    },

    /**
     * Ẩn tooltip
     * Created by: ntlong ( 12/08/2023 )
     */
    hideTooltip() {
      this.isShowTooltip = false;
    },

    handleClickMenuItem() {
      this.isActive = true;
    },
  },
};
</script>

<style lang="scss" scoped>
.m-menu-item {
  width: calc(100% - 16px);
  height: 41px;
  margin: 0 8px;
  border-radius: 8px;
  display: flex;
  align-items: center;
  position: relative;
  overflow: hidden;

  &.active,
  &.active:hover {
    color: #ff6d00;
    background: #fbe9e7;
  }

  &:hover {
    cursor: pointer;
    color: #ff6d00;
    background-color: #eff7ff;
  }
}

.m-menu-item__icon {
  margin: 0 8px;
}

.m-menu-item__icon::before {
  content: "";
  display: block;
}

.m-menu-item__icon::before {
  background-image: url("@/assets/img/sprites2.svg");
  position: absolute;
  width: 20px;
  height: 20px;
  top: 2px;
  left: 2px;
}

.m-menu-item__icon.dashboard::before {
  background-position: -2px -2px;
}

.m-menu-item:hover .dashboard::before {
  background-position: -2px -26px;
}

.m-menu-item__icon.movement::before {
  background-position: -52px -2px;
  left: 4px;
  width: 16px;
}

.m-menu-item:hover .movement::before {
  background-position: -52px -26px;
}

.m-menu-item__icon.reward::before {
  background-position: -74px -2px;
}

.m-menu-item:hover .reward::before {
  background-position: -74px -26px;
}

.m-menu-item__icon.decision::before {
  background-image: url("@/assets/icons/ms-icons/ic_quyet-dinh.svg");
  width: 24px;
  height: 24px;
  top: 0;
  left: 0;
}

.m-menu-item:hover .decision::before {
  background-image: url("@/assets/icons/ms-icons/ic_quyet-dinh--hover.svg");
}

.m-menu-item__icon.profile::before {
  background-position: -26px -2px;
}

.m-menu-item:hover .profile::before {
  background-position: -26px -26px;
}

.m-menu-item__icon.search::before {
  background-position: -196px -28px;
  width: 16px;
  height: 16px;
  top: 4px;
  left: 4px;
}

.m-menu-item:hover .search::before {
  background-position: -220px -28px;
}

.m-menu-item__icon.report::before {
  background-position: -100px -2px;
  left: 4px;
  width: 16px;
}

.m-menu-item:hover .report::before {
  background-position: -100px -26px;
}

.m-menu-item__icon.category::before {
  background-position: -124px -2px;
  left: 4px;
  width: 16px;
}

.m-menu-item:hover .category::before {
  background-position: -124px -26px;
}

.m-menu-item__icon.system::before {
  background-position: -146px -2px;
}

.m-menu-item:hover .system::before {
  background-position: -146px -26px;
}

.m-menu-item__icon.guide::before {
  background-image: url("@/assets/icons/ms-icons/ic_huong-dan.svg");
  width: 24px;
  height: 24px;
  top: 0;
  left: 0;
}

.m-menu-item:hover .guide::before {
  background-image: url("@/assets/icons/ms-icons/ic_huong-dan--hover.svg");
}

.m-menu-item__text {
  margin-left: 6px;
  font-size: 14px;
  font-weight: 500;
  white-space: nowrap;
}

.has-extend {
  width: 24px;
  height: 24px;
  position: absolute;
  right: 12px;
}

.has-extend::before {
  content: "";
  display: block;
  width: 24px;
  height: 24px;
  background-image: url("@/assets/icons/ms-icons/ic_arrow_right.svg");
}

.m-menu-item:hover .has-extend::before {
  background-image: url("@/assets/icons/ms-icons/ic_arrow_right--hover.svg");
}
</style>