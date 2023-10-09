<template>
  <div class="misa-input" :class="hasIcon">
    <div class="input__icon" v-if="hasIcon" @click="onClickIconInput"></div>
    <input
      :id="id"
      type="text"
      :placeholder="placeholder"
      v-model="dataValue"
      :disabled="disabled"
      :class="{ error: error }"
      ref="mInput"
      @input="onInput"
      @blur="onBlurInput"
    />
  </div>
</template>

<script>
export default {
  name: "MInput",
  data() {
    return {
      dataValue: "",
      showErrorText: false,
    };
  },
  props: {
    placeholder: String,
    // "has-icon": Nếu input có icon (icon search)
    hasIcon: String,
    // Dòng báo lỗi khi không nhập
    errorText: String,
    // Nếu form sửa các input sẽ có attr disabled
    disabled: Boolean,
    // id cua trường input
    id: String,
    // Kiểm tra input có đang lỗi không
    error: Boolean,

    // Trường mặc định của v-model ở bên ngoài
    modelValue: String,
  },
  watch: {
    modelValue(newValue) {
      this.dataValue = newValue;
    },
  },
  methods: {
    /**
     * Lấy giá trị của input nhập vào và $emit ra ngoài
     * Created by: ntlong (11/07/2023)
     */
    onInput() {
      this.$emit("update:modelValue", this.dataValue);
    },

    /**
     * Xử lý nếu blur khỏi trường bắt buộc sẽ báo lỗi
     * Created by: ntlong (11/07/2023)
     */
    onBlurInput() {
      this.$emit("onBlurInput");
    },

    /**
     * Emit sự kiện ra ngoài khi click vào icon ở component MInput
     * Created by: ntlong (11/07/2023)
     */
    onClickIconInput() {
      this.$emit("onClickIconInput");
    },
  },
};
</script>

<style lang="scss" scoped>
@import url("@/styles/base/input.scss");

.misa-input.has-icon {
  width: 265px;
  height: 36px;
  margin-right: 10px;
  position: relative;
}

.misa-input input {
  width: 100%;
  height: 36px;
  padding: 9px 12px 9px 12px;

  &.error {
    border-color: var(--color-danger);
  }
}

.misa-input.has-icon input {
  padding: 9px 40px 9px 12px;
}

.input__icon {
  width: 36px;
  height: 36px;
  position: absolute;
  top: 0;
  right: 0;
}

.input__icon::before {
  content: "";
  display: inline-block;
  width: 16px;
  height: 16px;
  background-repeat: no-repeat;
  background-image: url(@/assets/img/sprites.svg);
  background-position: -292px -28px;
  position: absolute;
  top: 10px;
  right: 10px;
}

.input__icon:hover {
  cursor: pointer;
}

.input__icon:hover + input {
  border: 1px solid var(--color-primary);
}

input:disabled {
  border: 1px solid var(--border-color-default);
  color: #b5b5b5;
  background-color: #ebebeb;
}
</style>
