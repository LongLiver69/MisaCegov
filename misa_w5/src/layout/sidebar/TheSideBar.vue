<template>
  <div class="sidebar" :class="{ 'zoom-out': zoomOutSidebar }">
    <div class="sidebar__top">
      <MMenuItem name="dashboard" :label="$_MISAResource.sidebar.dashboard" />
      <MMenuItem
        name="movement"
        :label="$_MISAResource.sidebar.movement"
        hasExtend="has-extend"
      />
      <MMenuItem
        name="reward"
        :label="$_MISAResource.sidebar.reward"
        hasExtend="has-extend"
      />
      <MMenuItem
        name="decision"
        :label="$_MISAResource.sidebar.decision"
        hasExtend="has-extend"
      />
      <MMenuItem
        name="profile"
        :label="$_MISAResource.sidebar.profile"
        hasExtend="has-extend"
      />
      <MMenuItem
        name="search"
        :label="$_MISAResource.sidebar.search"
        hasExtend="has-extend"
      />
      <MMenuItem name="report" :label="$_MISAResource.sidebar.report" />
      <div class="menu-line"></div>
      <MMenuItem
        name="category"
        :label="$_MISAResource.sidebar.category"
        hasExtend="has-extend"
      />
      <MMenuItem
        name="system"
        :label="$_MISAResource.sidebar.system"
        hasExtend="has-extend"
      />
      <div class="menu-line"></div>
      <MMenuItem
        name="guide"
        :label="$_MISAResource.sidebar.guide"
        hasExtend="has-extend"
      />
    </div>
    <div class="sidebar__bottom" @click="onZoomOut()">
      <div class="sidebar-bottom__icon icon24"></div>
      <div class="sidebar-bottom__text" v-if="!zoomOutSidebar">
        {{ $_MISAResource.sidebar.zoomOut }}
      </div>
    </div>
  </div>
</template>

<script>
import MMenuItem from "@/components/MMenuItem.vue";
import { mapState } from "vuex";
export default {
  name: "TheSideBar",
  components: { MMenuItem },
  computed: {
    ...mapState(["zoomOutSidebar"]),
  },
  methods: {
    /**
     * Sự kiện thu gọn/mở rộng sidebar
     * Author: NTLong (01/07/2023)
     */
    onZoomOut() {
      this.$store.commit("setZoomOutSidebar", !this.zoomOutSidebar);
    },
  },
};
</script>

<style lang="scss">
@import "./sidebar.scss";
</style>