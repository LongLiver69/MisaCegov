<template>
  <div class="header">
    <div class="header__left">
      <div class="icon24 header-left__toggle"></div>
      <div class="header-left__logo"></div>
      <div class="header-left__title">{{ $_MISAResource.productName }}</div>
    </div>
    <div class="header__right">
      <MPopper hover arrow :content="$_MISAResource.header.notify">
        <div class="header-right__action notify"></div>
      </MPopper>
      <MPopper hover arrow :content="$_MISAResource.header.help">
        <div class="header-right__action help"></div>
      </MPopper>
      <MPopper hover arrow :content="$_MISAResource.header.moreMenu">
        <div class="header-right__action more-menu"></div>
      </MPopper>
      <MPopper hover arrow :content="$_MISAResource.header.docsHelp">
        <div class="header-right__action docs-help"></div>
      </MPopper>
      <MPopper hover arrow :content="$_MISAResource.header.setting">
        <div class="header-right__action setting"></div>
      </MPopper>
      <div class="header-right__user">
        <div class="user-avatar"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheHeader",
};
</script>

<style scoped>
@import "./header.scss";
</style>
