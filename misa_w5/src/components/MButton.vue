<template>
  <button class="misa-button" :class="[nameIcon, variant]" :disabled="disabled">
    <div class="icon24" v-if="nameIcon"></div>
    <div class="button_title">{{ title }}</div>
  </button>
</template>

<script>
export default {
  name: "MButton",
  props: {
    /**
     * ----- Loại icon trong button -----
     * "btn--add": button có icon thêm mới
     * "btn--filter": button có icon lọc
     * Button không có icon không cần truyền
     */
    nameIcon: String,

    // Tên button (chữ trong button)
    title: String,

    /**
     * ----- Kiểu button (kiểu màu, layout, ...) -----
     * "btn-link" : chữ xanh, không border
     * "btn-primary": chữ trắng, nền xanh, không border
     * "btn-outline-primary": chữ lam, nền trắng, border lam
     * "btn-outline-secondary": chữ đen, nền trắng, boder xám
     * "btn-outline-danger": chữ đỏ, nền trắng, border đỏ
     * "btn-danger": chữ trắng, nền đỏ, không border
     */
    variant: String,

    // Button có attribute disabled không
    disabled: Boolean,
  },
};
</script>

<style lang="scss" scoped>
button.misa-button {
  height: 36px;
  font-weight: 500;
  background-color: rgb(237, 138, 138);
  border: none;
  border-radius: 4px;
  outline: none;
  display: flex;
  justify-content: center;
  align-items: center;

  &:hover {
    cursor: pointer;
  }

  &:disabled {
    opacity: 0.5;
    cursor: default;
  }

  & .icon24 {
    display: inline-block;
    position: relative;
    width: 24px;
    height: 24px;
    margin-right: 4px;
  }

  &.btn--filter {
    min-width: 102px;
    position: relative;

    & .icon24::before {
      position: absolute;
      top: 4px;
      right: 4px;
      width: 16px;
      height: 16px;
      background-position: -100px -52px;
    }

    &.filter-active .icon24::before {
      position: absolute;
      top: 0;
      right: 0;
      width: 24px;
      height: 24px;
      background: url(@/assets/icons/ms-icons/ic_filter_active.svg);
    }
  }

  &.btn--add {
    min-width: 162px;

    & .icon24::before {
      width: 16px;
      height: 16px;
      background-position: -196px -100px;
      position: absolute;
      top: 4px;
      right: 4px;
    }
  }

  &.btn--more {
    min-width: 42px;
    padding: 0 !important;

    & .icon24::before {
      width: 18px;
      height: 4px;
      background: url(@/assets/img/sprites.svg) no-repeat -171px -58px;
      position: absolute;
      top: 10px;
      left: 5px;
    }
  }

  &.btn-link {
    color: var(--color-primary);
    background-color: transparent;

    &:hover:not(:disabled),
    &:active {
      text-decoration: underline;
      color: rgb(0, 98, 204);
    }
  }

  &.btn-primary {
    padding: 0 16px;
    color: #fff;
    border: none;
    outline: none;
    background-color: var(--color-primary);

    &:hover:not(:disabled) {
      background: #1a73e8;
    }
  }

  &.btn-danger {
    padding: 0 16px;
    color: #fff;
    border: none;
    outline: none;
    background-color: var(--color-danger);

    &:hover:not(:disabled) {
      background: #e54340;
    }
  }

  &.btn-outline-primary {
    padding: 0 16px;
    color: var(--color-primary);
    border: 1px solid var(--color-primary);
    background-color: #fff;

    &:hover:not(:disabled) {
      background: #eef4ff;
    }

    &:active {
      background: #0062cc;
      color: #fff;
    }
  }

  &.btn-outline-secondary {
    padding: 0 16px;
    border: 1px solid var(--border-color-default);
    background-color: #fff;

    &:hover:not(:disabled) {
      background: #f4f5f8;
    }

    &:active {
      background: #ebebeb;
    }
  }

  &.btn-outline-danger {
    padding: 0 16px;
    color: var(--color-danger);
    border: 1px solid var(--color-danger);
    background-color: #fff;

    &:hover:not(:disabled) {
      background: #fef1f1;
    }

    &:active {
      background: #d63734;
      color: #fff;
    }
  }
}
</style>
