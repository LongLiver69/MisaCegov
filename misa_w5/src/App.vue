<template>
  <TheHeader />
  <div class="main-container">
    <TheSideBar />
    <TheMainContent />
  </div>
  <MToast v-if="showToast" message="Lưu thành công" />
  <MDialog v-if="showDialog" />
</template>

<script>
import TheHeader from "@/layout/header/TheHeader.vue";
import TheSideBar from "@/layout/sidebar/TheSideBar.vue";
import TheMainContent from "@/layout/main-content/TheMainContent.vue";

import { mapState } from "vuex";

export default {
  name: "App",
  components: {
    TheHeader,
    TheSideBar,
    TheMainContent,
  },

  computed: {
    ...mapState(["showToast", "showDialog"]),
  },
};
</script>

<style lang="scss">
@import "./styles/main.scss";
.main-container {
  width: 100vw;
  height: calc(100vh - 56px);
}
</style>
