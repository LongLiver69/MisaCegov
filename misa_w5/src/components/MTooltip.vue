<template>
  <div class="m-tooltip">{{ content }}</div>
</template>

<script>
export default {
  name: "MTooltip",
  props: {
    content: String,
  },
};
</script>

<style lang="scss" scoped>
.m-tooltip {
  display: inline-block;
  position: fixed;
  background-color: #fff;
  color: #182d4b;
  border: 1px solid hsla(0, 0%, 62%, 0.30980392156862746);
  padding: 8px 15px;
  border-radius: 8px;
  margin-top: 0;
  left: 60px;
  font-size: 13px;
  letter-spacing: 0.5px;
  box-shadow: 0 1.5px 2px 0 rgba(0, 0, 0, 0.1);
  z-index: 3;

  &::before {
    content: "";
    display: block;
    position: absolute;
    left: -4px;
    top: 10px;
    transform: rotate(45deg);
    width: 10px;
    height: 10px;
    background-color: inherit;
    border-left: 1px solid hsla(0, 0%, 62%, 0.30980392156862746);
    border-bottom: 1px solid hsla(0, 0%, 62%, 0.30980392156862746);
  }
}
</style>
