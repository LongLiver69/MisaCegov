<template>
  <div class="misa-combobox" @mouseleave="onCloseDropdownPanel">
    <input
      type="text"
      v-model="inputValue"
      :placeholder="placeholder"
      :disabled="disabled"
      :readonly="readonly"
      :class="{ error: error }"
      @input="debouncedSearch"
    />
    <div class="icon-combo--dropdown" @click="onToggleDropdownPanel"></div>
    <div v-if="showDropdownPanel && !disabled" class="combo-dropdown-panel" :class="position">
      <div
        class="combo-dropdown__option"
        v-for="(option, index) in dropdownOptions"
        :key="index"
        :class="{ selected: isSelected(option), highlight: isHighlighted(option) }"
        @mousedown="selectOption(option)"
      >
        <div class="combobox-option--text">{{ option.name }}</div>
        <div class="combobox-option--icon icon24" v-show="hasIconTick && isSelected(option)"></div>
      </div>
    </div>
  </div>
</template>

<script>
import debounce from "lodash.debounce";
export default {
  name: "MCombobox",
  props: {
    // Những dữ liệu để lựa chọn
    options: Array,

    placeholder: String,

    /**
     * Dữ liệu của combobox hiển thị ra ở trên hay dưới
     * "show-above" : hiển thị lên trên
     * mặc định không truyền gì là hiển thị lên trên
     */
    position: String,

    // thuộc tính disabled cho thẻ input
    disabled: Boolean,

    // thuộc tính readonly cho thẻ input
    readonly: Boolean,

    // Thể hiện có icon tick ở option được chọn không
    hasIconTick: Boolean,

    // Kiểm tra giá trị input trong combobox có đang lỗi không
    error: Boolean,

    // Trường mặc định của v-model ở bên ngoài
    modelValue: Number,
  },
  data() {
    return {
      showDropdownPanel: false,
      selectedOption: Object, // Option đang được chọn
      highlightedOption: Object,
      dropdownOptions: Array,
      inputValue: String,
    };
  },
  created() {
    this.debouncedSearch = debounce(this.search, 500);
  },
  watch: {
    modelValue(newValue) {
      if (newValue !== -1 && newValue !== -2) {
        // -1: trường nhập không phù hợp | -2: để trống
        this.selectedOption = this.options.find((item) => item.value === newValue);
        this.inputValue = { ...this.selectedOption }.name;
      }
    },
    options() {
      this.dropdownOptions = this.options;
      this.selectedOption = { ...this.options[0] };
      this.highlightedOption = { ...this.selectedOption };
      this.inputValue = { ...this.selectedOption }.name;
      this.$emit("update:modelValue", this.selectedOption.value);
    },
  },
  mounted() {
    this.dropdownOptions = this.options;
    this.selectedOption = { ...this.options[0] };
    this.highlightedOption = { ...this.selectedOption };
    this.inputValue = { ...this.selectedOption }.name;
    this.$emit("update:modelValue", this.selectedOption.value);
  },
  methods: {
    /**
     * Sự kiện mở thẻ chứa các option
     * Created by: ntlong (02/07/2023)
     */
    onToggleDropdownPanel() {
      this.showDropdownPanel = !this.showDropdownPanel;
      this.dropdownOptions = this.options;
    },

    /**
     * Sự kiện đóng thẻ chứa các option
     * Created by: ntlong (02/07/2023)
     */
    onCloseDropdownPanel() {
      this.showDropdownPanel = false;
    },

    /**
     * Sự kiện chọn 1 option
     * Created by: ntlong (02/07/2023)
     */
    selectOption(option) {
      // Binding 2 chiều
      this.selectedOption = { ...option };
      this.highlightedOption = { ...this.selectedOption };
      this.inputValue = { ...this.selectedOption }.name;
      this.showDropdownPanel = false;
      this.$emit("update:modelValue", this.selectedOption.value);

      // Các option sẽ được xuất hiện đầy đủ sau khi chọn 1 option bất kỳ
      // Tránh trường hợp mất 1 số option sau khi search
      this.dropdownOptions = this.options;
    },

    /**
     * Tìm kiếm các option trong combobox với inputValue được nhập
     * Created by: ntlong (02/07/2023)
     */
    search() {
      this.showDropdownPanel = true;
      this.dropdownOptions = this.options.filter((item) =>
        item.name.toLowerCase().includes(this.inputValue.toLowerCase())
      );
      // Trường hợp nhập không tìm được lựa chọn nào trong danh sách
      if (this.dropdownOptions.length === 0 && this.inputValue !== "") {
        this.showDropdownPanel = false;
        this.$emit("update:modelValue", -1);
      }

      // Trường hợp nhập có xuất hiện lựa chọn nhưng không có option đã chọn trước đó
      if (this.dropdownOptions.length > 0 && !this.dropdownOptions.includes(this.selectedOption)) {
        this.highlightedOption = { ...this.dropdownOptions[0] };
      } else if (this.dropdownOptions.includes(this.selectedOption)) {
        this.highlightedOption = { ...this.selectedOption };
      }

      // Trường hợp để trống
      if (this.inputValue === "") {
        this.dropdownOptions = this.options;
        this.highlightedOption = { ...this.options[0] };
        this.$emit("update:modelValue", -2);
      }
    },

    /**
     * Kiểm tra option này có đang được chọn không
     * Created by: ntlong (02/07/2023)
     */
    isSelected(option) {
      return this.selectedOption.value === option.value;
    },

    /**
     * Kiểm tra option này có đang được highlight không
     * Created by: ntlong (02/07/2023)
     */
    isHighlighted(option) {
      return this.highlightedOption.value === option.value;
    },
  },
};
</script>

<style lang="scss" scoped>
.misa-combobox {
  position: relative;
  width: 100%;
  max-width: 100%;
  height: 36px;

  & input {
    width: 100%;
    height: 100%;
    padding: 0 40px 0 12px;

    &:has(~ .icon-combo--dropdown:hover) {
      border: 1px solid var(--color-primary);
    }

    &.error {
      border-color: var(--color-danger);
    }
  }

  & .icon-combo--dropdown {
    position: absolute;
    top: 0;
    right: 0;
    width: 36px;
    height: 36px;

    &:hover {
      cursor: pointer;
    }

    &::before {
      content: "";
      position: absolute;
      top: 15px;
      left: 13px;
      width: 10px;
      height: 6px;
      background-image: url("@/assets/img/sprites2.svg");
      background-position: -175px -57px;
    }
  }

  & .combo-dropdown-panel {
    position: absolute;
    top: 36px;
    left: 0;
    min-width: 100%;
    padding: 8px 0;
    background-color: #fff;
    border-radius: 4px;
    overflow: hidden;
    box-shadow: 0 0 16px rgba(23, 27, 42, 0.24);
    z-index: 4;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;

    &.show-above {
      top: -156px;
    }

    & .combo-dropdown__option {
      width: calc(100% - 16px);
      height: 36px;
      padding-left: 10px;
      margin: 0 8px;
      border-radius: 4px;
      display: flex;
      justify-content: space-between;
      align-items: center;
      column-gap: 8px;

      &.highlight {
        background: #dfebff;
      }

      &:hover {
        cursor: pointer;
        background: #dfebff;
      }

      & .combobox-option--icon {
        margin-right: 12px;
      }

      & .combobox-option--icon::before {
        background-position: -196px -30px;
        width: 16px;
        height: 12px;
        position: absolute;
        top: 6px;
        left: 4px;
      }
    }
  }
}
</style>
