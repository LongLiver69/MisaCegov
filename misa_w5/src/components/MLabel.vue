<template>
  <label class="misa-lable" :class="{ required: required }">
    {{ title }}
  </label>
</template>

<script>
export default {
  name: "MLable",
  props: {
    title: String,
    required: Boolean,
  },
};
</script>

<style lang="scss" scoped>
.misa-lable {
  display: block;
  margin-bottom: 8px;

  &.required::after {
    content: "*";
    color: #e54848;
    margin-left: 6px;
  }
}
</style>
