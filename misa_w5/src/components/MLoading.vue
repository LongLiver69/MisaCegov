<template>
  <div class="m-loading"><div class="loader"></div></div>
</template>

<script>
export default {
  name: "MLoading",
};
</script>

<style lang="scss" scoped>
.m-loading {
  position: absolute;
  top: 50%;
  left: 55%;
  transform: translate(-50%);
}
.loader {
  width: 50px;
  height: 50px;
  margin: auto;
  border: 6px solid #e8e8e8;
  border-radius: 50%;
  border-top: 6px solid #ff6016;
  animation: spinner 1s ease-in-out infinite;
}

@keyframes spinner {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
