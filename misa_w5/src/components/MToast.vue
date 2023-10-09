<template>
  <div class="m-toast">
    <span class="m-toast__icon icon24"></span>
    <span class="m-toast__message">{{ message }}</span>
  </div>
</template>

<script>
export default {
  name: "MToast",
  props: {
    message: String,
  },
  mounted() {
    setTimeout(() => {
      this.$store.commit("setShowToast", false);
    }, 2500);
  },
};
</script>

<style lang="scss" scoped>
.m-toast {
  position: fixed;
  top: 16px;
  left: 50%;
  width: 400px;
  height: 56px;
  transform: translateX(-50%);
  display: flex;
  align-items: center;
  justify-content: flex-start;
  background-color: #fff;
  border-radius: 4px;
  box-sizing: border-box;
  box-shadow: 0 0 16px rgba(23, 27, 42, 0.24);
  z-index: 10;

  & .m-toast__icon {
    width: 20px;
    height: 20px;
    margin-left: 16px;

    &::before {
      background: url(@/assets/icons/ms-icons/ic_toast_success.svg) no-repeat;
      width: 20px;
      height: 20px;
    }
  }

  & .m-toast__message {
    padding: 11px 8px;
  }
}
</style>
