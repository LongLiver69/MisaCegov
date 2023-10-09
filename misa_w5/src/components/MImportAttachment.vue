<template>
  <div class="import-attachment">
    <MFileInfo :fileInfo="fileInfo"></MFileInfo>
    <div class="file-caution">
      <div class="file-caution-img"></div>
      <div class="file-caution-text">
        Lưu ý<span style="color: red">*</span>: Bạn vui lòng chọn sheet muốn nhập khẩu và <br />dòng
        tiêu đề của sheet đó.
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MImportAttachment",
  props: {
    fileInfo: Object,
  },
};
</script>

<style lang="scss" scoped>
.import-attachment {
  flex: 1;
  width: 100%;
  border: 1px solid var(--border-color-default);
  border-radius: 4px;
}

.file-caution {
  & .file-caution-img {
    width: 100%;
    height: 200px;
    background: url(@/assets/img/img_import.svg) no-repeat 50% 50%;
  }

  & .file-caution-text {
    text-align: center;
  }
}
</style>
