<template>
  <label class="upload-container" @dragover.prevent @drop.prevent="handleDropFile">
    <!-- <div class="no-file"></div> -->
    <input type="file" accept=".xls,.xlsx,.ods" style="display: none" @change="uploadFile" />
  </label>
</template>

<script>
export default {
  methods: {
    /**
     * Xử lý khi kéo file vào popup nhập khẩu
     * Created by: ntlong ( 23/08/2023 )
     */
    handleDropFile(event) {
      // Lấy file đầu tiên từ danh sách các file được kéo vào
      const files = event.dataTransfer.files;
      // Chỉ lấy 1 file đầu tiên đúng định dạng của excel
      var file = files[0];
      // Kiểm tra file đúng định dạng không
      if (file.name.endsWith(".xls") || file.name.endsWith(".xlsx")) {
        this.$emit("getFile", file);
      } else {
        this.$store.commit("setDialogType", this.$_MISAResource.dialog.type.error);
        this.$store.commit("setError", this.$_MISAResource.dialog.description.errorFileFormat);
        this.$store.commit("setShowDialog", true);
      }
    },

    /**
     * Xử lý khi upload 1 file để nhập khẩu
     * Created by: ntlong ( 23/08/2023 )
     */
    uploadFile(event) {
      const file = event.target.files[0];
      if (file !== undefined) {
        this.$emit("getFile", file);
      }
    },
  },
};
</script>

<style lang="scss" scoped>
.upload-container {
  height: 100%;
  border: 1px dashed #ddd;
  border-radius: 4px;
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;
  background: url(@/assets/img/add_attachment.svg);
  background-repeat: no-repeat;
  background-position: center;

  & .no-file {
    width: 342px;
    height: 111px;
  }
}
</style>
