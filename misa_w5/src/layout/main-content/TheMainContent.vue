<template>
  <div class="main-content" :class="{ 'zoom-in': zoomOutSidebar }">
    <CompetitionAward />
  </div>
</template>

<script>
import { mapState } from "vuex";
import CompetitionAward from "../../pages/category/CompetitionAward.vue";
export default {
  name: "TheMainContent",
  components: { CompetitionAward },
  computed: {
    ...mapState(["zoomOutSidebar"]),
  },
};
</script>

<style lang="scss" scoped>
@import url("./main-content.scss");
</style>