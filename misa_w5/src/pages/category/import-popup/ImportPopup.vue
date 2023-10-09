<template>
  <MPopup :title="title" width="800px" @closePopup="close">
    <template #header-btns>
      <div class="header-btns">
        <MPopper placement="top" hover arrow :content="$_MISAResource.form.tooltip.help">
          <div class="icon24 help"></div>
        </MPopper>
      </div>
    </template>

    <template #default>
      <div class="content">
        <div class="toolbar-step">
          <div class="step step-one" :class="fileInfo !== null ? 'finish' : 'active'">
            {{ $_MISAResource.popup.step.stepOne }}
          </div>
          <div class="line" :class="fileInfo !== null ? 'finish' : 'active'"></div>
          <div class="step step-two" :class="fileInfo !== null ? 'active' : {}">
            {{ $_MISAResource.popup.step.stepTwo }}
          </div>
        </div>
        <div class="content-step">
          <!-- Step 1 -->
          <MUpload @getFile="setFileInfo" v-if="fileInfo === null"></MUpload>

          <!-- Step 2.1 -->
          <div class="import-container" v-if="fileInfo !== null && !isCheckStep">
            <MImportAttachment :fileInfo="fileInfo" />
            <div class="config-container">
              <div class="option sheet-option">
                <MLabel :title="$_MISAResource.label.importSheet" required />
                <MCombobox
                  :options="sheetOptions"
                  v-model="sheetIndex"
                  hasIconTick
                  :error="sheetIndex === null || sheetIndex === -1"
                />
                <div class="error-text" v-if="[null, -1].includes(sheetIndex)">
                  {{
                    sheetIndex === null
                      ? $_MISAResource.validate.sheetFormat
                      : sheetIndex === -1
                      ? $_MISAResource.validate.sheetError
                      : ""
                  }}
                </div>
              </div>
              <div class="option title-line-option">
                <MLabel :title="$_MISAResource.label.titleLine" required />
                <input type="number" class="number-input" min="1" v-model="titleLine" />
              </div>
            </div>
          </div>

          <!-- Step 2.2 -->
          <div class="check-container" v-if="fileInfo !== null && isCheckStep">
            <div class="row-detail">
              <div
                class="box-detail"
                v-for="(box, index) in boxDetails"
                :key="index"
                :class="box.color"
              >
                <div class="box-detail__title">{{ box.title }}</div>
                <div class="box-detail__number">{{ box.number }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>

    <template #popup-footer>
      <div class="footer">
        <div class="footer-left">
          <MButton
            variant="btn-outline-primary"
            :title="$_MISAResource.button.downloadSampleFile"
            @click="downloadSampleFile"
          ></MButton>
        </div>
        <div class="footer-right">
          <MButton
            variant="btn-outline-secondary"
            :title="$_MISAResource.button.cancel"
            @click="close"
          ></MButton>

          <template v-if="isCheckStep === false">
            <MButton
              variant="btn-primary"
              :title="$_MISAResource.button.continue"
              @click="onContinue"
              :disabled="fileInfo === null"
            ></MButton>
          </template>

          <template v-if="isCheckStep === true">
            <MButton
              variant="btn-outline-secondary"
              :title="$_MISAResource.button.back"
              @click="backImportStep"
            ></MButton>
            <MButton
              variant="btn-primary"
              :title="$_MISAResource.button.import"
              @click="submitImport"
              :disabled="fileInfo === null"
            ></MButton>
          </template>
        </div>
      </div>
    </template>
  </MPopup>
</template>

<script>
import awardService from "@/services/category/awardService";

export default {
  name: "ImportPopup",
  data() {
    return {
      file: null,
      fileInfo: null, // gồm các thuộc tính: fileName, fileSize, sheets, titleLine, isValid
      sheetIndex: null,
      titleLine: null,
      isCheckStep: false, // Thể hiện đang ở bước thống kê bản ghi hợp lệ
      boxDetails: [
        {
          type: "total",
          title: this.$_MISAResource.boxDetail.title.recordNumber,
          color: "black",
          number: 0,
        },
        {
          type: "valid",
          title: this.$_MISAResource.boxDetail.title.valid,
          color: "primary",
          number: 0,
        },
        {
          type: "invalid",
          title: this.$_MISAResource.boxDetail.title.invalid,
          color: "danger",
          number: 0,
        },
      ],
    };
  },
  props: {
    title: String,
  },
  computed: {
    sheetOptions() {
      return this.fileInfo.sheets.map((item, index) => {
        return { value: index, name: item };
      });
    },
  },
  mounted() {
    // Đăng ký nhận sự kiện khi file nhập khẩu được thay đổi từ MFileInfo
    this.$_emitter.on("changeFile", this.handleChangeFile);
  },
  unmounted() {
    // Hủy nhận sự kiện reload khi file nhập khẩu được thay đổi
    this.$_emitter.off("changeFile", this.handleChangeFile);
  },
  methods: {
    /**
     * Sự kiện đóng popup nhập khẩu
     * Created by: ntlong ( 07/08/2023 )
     */
    close() {
      this.$emit("close");
    },

    /**
     * Lấy ra các thông tin và kiểm tra tính hợp lệ của file được upload
     * Created by: ntlong ( 07/08/2023 )
     */
    async setFileInfo(file) {
      try {
        this.file = file;
        const fileInfo = await awardService.getFileInfo(file);
        this.fileInfo = fileInfo;
        this.titleLine = fileInfo.titleLine;
      } catch (error) {
        this.fileInfo = null;
        console.log(error);
      }
    },

    /**
     * Xử lý tải file nhập khẩu mẫu về máy
     * Created by: ntlong ( 07/08/2023 )
     */
    downloadSampleFile() {
      awardService.getSampleImportFile().then((file) => {
        const fileURL = window.URL.createObjectURL(file);
        // Tạo một thẻ a để download file từ URL
        const link = document.createElement("a");
        // Thiết lập thuộc tính href là fileURL
        link.href = fileURL;
        link.setAttribute("download", "Mau nhap khau danh hieu thi dua.xlsx");
        link.click();
        window.URL.revokeObjectURL(fileURL);
      });
    },

    /**
     * Xử lý đổi file nhập khẩu
     * Created by: ntlong ( 07/08/2023 )
     */
    async handleChangeFile(newFile) {
      await this.setFileInfo(newFile);
    },

    /**
     * Xử lý sự kiện khi ấn vào nút tiếp tục để nhảy sang bước kiểm tra
     * Created by: ntlong ( 23/08/2023 )
     */
    async onContinue() {
      try {
        const result = await awardService.getImportDataStatistics(
          this.file,
          this.sheetIndex,
          this.titleLine
        );
        this.boxDetails[0].number = result.total;
        this.boxDetails[1].number = result.validNumber;
        this.boxDetails[2].number = result.invalidNumber;

        // Chuyển sang bước kiểm tra
        this.isCheckStep = true;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Xử lý sự kiện khi ấn vào nút quay lại để về bước nhập
     * Created by: ntlong ( 23/08/2023 )
     */
    backImportStep() {
      // Quay lại bước nhập
      this.isCheckStep = false;
    },

    /**
     * Xử lý sự kiện nhập khẩu vào database
     * Created by: ntlong ( 23/08/2023 )
     */
    async submitImport() {
      try {
        if (this.boxDetails[1]?.number > 0) {
          // Có ghi hợp lệ
          await awardService.importExcel(this.file, this.sheetIndex, this.titleLine);
          this.close();
          await this.$store.dispatch("awardStore/refreshDisplay");
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/pages/category/import-popup/import-popup.scss";
</style>
