<template>
  <div class="file-info">
    <div class="file-icon icon24"></div>
    <div class="file-name">{{ fileInfo.fileName }}</div>
    <div class="file-size">{{ fileInfo.fileSize }}</div>
    <div class="file-accepted">
      <div class="icon icon24" :class="true ? 'icon-success' : 'icon-unsuccess'"></div>
      <div>
        {{
          fileInfo.isValid
            ? $_MISAResource.file.validate.valid
            : $_MISAResource.file.validate.invalid
        }}
      </div>
    </div>
    <MButton
      :title="$_MISAResource.button.changeFile"
      variant="btn-link"
      @click="onClickChangeFileButton"
    ></MButton>
    <input
      type="file"
      ref="changeFileInput"
      accept=".xls,.xlsx,.ods"
      style="display: none"
      @change="handleEmitNewFile"
    />
  </div>
</template>

<script>
export default {
  name: "MFileInfo",
  props: {
    fileInfo: Object,
  },
  methods: {
    /**
     * Sự kiện khi click vào nút thay đổi file
     * Created by: ntlong ( 15/08/2023 )
     */
    onClickChangeFileButton() {
      this.$refs.changeFileInput.click();
    },

    /**
     * Xử lý sự kiện khi file đã thay đổi
     * Created by: ntlong ( 15/08/2023 )
     */
    handleEmitNewFile(event) {
      const file = event.target.files[0];
      this.$_emitter.emit("changeFile", file);
    },
  },
};
</script>

<style lang="scss" scoped>
.file-info {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  width: 100%;
  height: 48px;
  padding: 0 100px 0 16px;
  border-bottom: 1px solid var(--border-color-default);
}

.file-icon {
  margin-right: 8px;
  &::before {
    background-image: url("@/assets/icons/ms-icons/ic_file_gray.svg");
    position: absolute;
    top: 3px;
    left: 4px;
    width: 16px;
    height: 16px;
  }
}

.file-name {
  flex: 1;
  margin-right: 32px;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
}

.file-size {
  width: 80px;
  margin-right: 10px;
}

.file-accepted {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  width: 120px;
  margin-right: 36px;

  & .icon {
    display: block;
    margin-right: 4px;

    &::before {
      position: absolute;
      top: 4px;
      left: 4px;
      width: 16px;
      height: 16px;
    }

    &.icon-success::before {
      background-image: url("@/assets/img/sprites2.svg");
      background-position: -112px -144px;
    }

    &.icon-unsuccess::before {
      background-image: url("@/assets/icons/ms-icons/ic_warning_red.svg");
    }
  }
}
</style>
