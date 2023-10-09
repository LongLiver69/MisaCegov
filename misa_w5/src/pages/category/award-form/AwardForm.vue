<template>
  <MPopup :title="title" height="500px" @closePopup="closeForm">
    <template #header-btns>
      <div class="header-btns">
        <MPopper placement="top" hover arrow :content="$_MISAResource.form.tooltip.help">
          <div class="icon24 help" @click="redirectToHelpPage"></div>
        </MPopper>
      </div>
    </template>

    <template #default>
      <div class="form__content">
        <div class="field--name">
          <MLabel for="nameInput" :title="$_MISAResource.table.columnName.name" required />
          <MInput
            id="nameInput"
            :placeholder="$_MISAResource.form.placeholder.nameInput"
            ref="autoFocus"
            v-model="newAward.awardName"
            @onBlurInput="newAward.awardName === null ? (newAward.awardName = '') : {}"
            :disabled="isDisabled"
            :error="newAward.awardName === ''"
          />
          <div class="error-text" v-if="newAward.awardName === ''">
            {{ $_MISAResource.validate.nameFormat }}
          </div>
        </div>
        <div class="field--code">
          <MLabel for="codeInput" :title="$_MISAResource.table.columnName.code" required />
          <MInput
            id="codeInput"
            :placeholder="$_MISAResource.form.placeholder.codeInput"
            v-model="newAward.awardCode"
            @onBlurInput="newAward.awardCode === null ? (newAward.awardCode = '') : {}"
            :disabled="isDisabled"
            :error="newAward.awardCode === ''"
          />
          <div class="error-text" v-if="newAward.awardCode === ''">
            {{ $_MISAResource.validate.codeFormat }}
          </div>
        </div>
        <div class="field--object">
          <MLabel :title="$_MISAResource.table.columnName.object" required />
          <div class="object-option">
            <label>
              <input type="checkbox" v-model="isChecked1" :disabled="isDisabled" />
              <span class="checkmark"></span>
              <span class="checkbox__text">{{ $_MISAResource.field.object.individual }}</span>
            </label>
            <label>
              <input type="checkbox" v-model="isChecked2" :disabled="isDisabled" />
              <span class="checkmark"></span>
              <span class="checkbox__text">{{ $_MISAResource.field.object.group }}</span>
            </label>
          </div>
          <div class="error-text" v-if="!isChecked1 && !isChecked2">
            {{ $_MISAResource.validate.objectFormat }}
          </div>
        </div>
        <div class="field--level">
          <MLabel :title="$_MISAResource.table.columnName.level" required />
          <MCombobox
            :options="levelOption"
            v-model="newAward.awardLevel"
            :placeholder="$_MISAResource.form.placeholder.levelInput"
            :disabled="isDisabled"
            :hasIconTick="true"
            :error="newAward.awardLevel === -2 || newAward.awardLevel === -1"
          />
          <div class="error-text" v-if="[-1, -2].includes(newAward.awardLevel)">
            {{
              newAward.awardLevel === -2
                ? $_MISAResource.validate.levelFormat
                : newAward.awardLevel === -1
                ? $_MISAResource.validate.levelError
                : ""
            }}
          </div>
        </div>
        <div class="field--type">
          <MLabel :title="$_MISAResource.table.columnName.type" required />
          <div class="type-option">
            <label>
              <input type="checkbox" v-model="isChecked3" :disabled="isDisabled" />
              <span class="checkmark"></span>
              <span class="checkbox__text">{{ $_MISAResource.field.type.frequent }}</span>
            </label>
            <label>
              <input type="checkbox" v-model="isChecked4" :disabled="isDisabled" />
              <span class="checkmark"></span>
              <span class="checkbox__text">{{ $_MISAResource.field.type.periodic }}</span>
            </label>
          </div>
          <div class="error-text" v-if="!isChecked3 && !isChecked4">
            {{ $_MISAResource.validate.typeFormat }}
          </div>
        </div>
        <div class="field--note">
          <MLabel for="noteInput" title="Ghi chú" />
          <textarea
            id="noteInput"
            :placeholder="$_MISAResource.form.placeholder.noteInput"
            :disabled="isDisabled"
          ></textarea>
        </div>
        <div class="field--status" v-if="typeForm === this.$_MISAEnum.FORM_MODE.EDIT">
          <MLabel :title="$_MISAResource.table.columnName.status" />
          <div class="status-option">
            <label>
              <input type="radio" name="checktype" value="1" v-model="this.newAward.awardStatus" />
              <span class="checkmark checkmark--radio"></span>
              <span class="checkbox__text">{{ $_MISAResource.field.status.active }}</span>
            </label>
            <label>
              <input type="radio" name="checktype" value="2" v-model="this.newAward.awardStatus" />
              <span class="checkmark checkmark--radio"></span>
              <span class="checkbox__text">{{ $_MISAResource.field.status.inactive }}</span>
            </label>
          </div>
        </div>
      </div>
    </template>
    <template #popup-footer>
      <div class="footer">
        <!-- Footer của form thêm mới -->
        <MButton
          variant="btn-outline-secondary"
          :title="$_MISAResource.button.cancel"
          @click="closeForm"
        ></MButton>
        <template v-if="typeForm === this.$_MISAEnum.FORM_MODE.ADD">
          <MPopper placement="top" hover arrow content="Ctrl + Shift + S" closeDelay="0">
            <MButton
              variant="btn-outline-primary"
              :title="$_MISAResource.button.saveAndAdd"
              @click="onSaveAndAdd"
            ></MButton>
          </MPopper>

          <MPopper placement="top" hover arrow content="Ctrl + S">
            <MButton variant="btn-primary" :title="$_MISAResource.button.save" @click="onSave">
            </MButton>
          </MPopper>
        </template>

        <!-- Footer của form sửa -->
        <template v-else>
          <MPopper placement="top" hover arrow content="Ctrl + S">
            <MButton
              variant="btn-primary"
              :title="$_MISAResource.button.saveChange"
              @click="onSaveChange"
            ></MButton>
          </MPopper>
        </template>
      </div>
    </template>
  </MPopup>
</template>

<script>
import { mapMutations, mapState } from "vuex";

export default {
  name: "AwardForm",
  props: {
    typeForm: Number,
  },
  data() {
    return {
      newAward: {
        awardName: null,
        awardCode: null,
        awardObject: null,
        awardLevel: null,
        awardType: null,
        // Khi thêm mới trạng thái mặc định là sử dụng
        awardStatus: this.$_MISAEnum.AWARD_STATUS.ACTIVE,
        isSystemData: false,
      },
      levelOption: [
        {
          value: this.$_MISAEnum.AWARD_LEVEL.NATIONAL, // "Cấp Nhà nước"
          name: this.$_MISAResource.field.level.national,
        },
        {
          value: this.$_MISAEnum.AWARD_LEVEL.PROVINCE, // "Cấp Tỉnh/tương đương"
          name: this.$_MISAResource.field.level.province,
        },
        {
          value: this.$_MISAEnum.AWARD_LEVEL.DISTRICT, // "Cấp Huyện/tương đương"
          name: this.$_MISAResource.field.level.district,
        },
        {
          value: this.$_MISAEnum.AWARD_LEVEL.COMMUNE, // "Cấp Xã/tương đương"
          name: this.$_MISAResource.field.level.commune,
        },
      ],
      isChecked1: false, // check ô 'Cá Nhân'
      isChecked2: false, // check ô 'Tập thể'
      isChecked3: false, // check ô 'Thường xuyên'
      isChecked4: false, // check ô 'Theo đợt'
    };
  },
  computed: {
    ...mapState("awardStore", ["awardForEdit"]),

    // Tiêu đề của form
    title() {
      if (this.typeForm === this.$_MISAEnum.FORM_MODE.EDIT) {
        return this.$_MISAResource.form.title.edit;
      } else return this.$_MISAResource.form.title.add;
    },

    // Kiểm tra xem input đó có được phép sửa không
    isDisabled() {
      if (
        this.typeForm === this.$_MISAEnum.FORM_MODE.EDIT &&
        this.awardForEdit.isSystemData === true
      ) {
        return true;
      } else return false;
    },
  },
  mounted() {
    window.addEventListener("keydown", this.saveByKeyboard);

    this.$refs.autoFocus.$refs.mInput.focus();
    // Khi mở form thêm mới sẽ có sẵn các lựa chọn mắc định
    if (this.typeForm === this.$_MISAEnum.FORM_MODE.ADD) {
      this.isChecked1 = true;
      this.isChecked3 = true;
    }

    // Nếu là form sửa thì binding dữ liệu của bản ghi được chọn sửa vào form
    if (this.typeForm === this.$_MISAEnum.FORM_MODE.EDIT) {
      this.newAward = { ...this.awardForEdit };

      /**
       * Hiển thị ô checkbox được chọn ứng với giá trị hiện tại của bản ghi được chọn để sửa
       * Created by: ntlong ( 01/07/2023 )
       */
      // Đối tượng khen thưởng
      if (
        this.awardForEdit.awardObject === this.$_MISAEnum.AWARD_OBJECT.INDIVIDUAL // Cá nhân
      ) {
        this.isChecked1 = true;
      } else if (
        this.awardForEdit.awardObject === this.$_MISAEnum.AWARD_OBJECT.GROUP // Tập thể
      ) {
        this.isChecked2 = true;
      } else if (
        this.awardForEdit.awardObject === this.$_MISAEnum.AWARD_OBJECT.INDIVIDUAL_OR_GROUP // Cá nhân và tập thể
      ) {
        this.isChecked1 = true;
        this.isChecked2 = true;
      }

      // Loại khen thưởng
      if (
        this.awardForEdit.awardType === this.$_MISAEnum.AWARD_TYPE.FREQUENT // "Thường xuyên"
      ) {
        this.isChecked3 = true;
      } else if (
        this.awardForEdit.awardType === this.$_MISAEnum.AWARD_TYPE.PERIODIC // "Theo đợt"
      ) {
        this.isChecked4 = true;
      } else if (
        this.awardForEdit.awardType === this.$_MISAEnum.AWARD_TYPE.FREQUENT_OR_PERIODIC // "Thường xuyên; Theo đợt"
      ) {
        this.isChecked3 = true;
        this.isChecked4 = true;
      }
    }
  },
  unmounted() {
    window.removeEventListener("keydown", this.saveByKeyboard);
  },
  methods: {
    /**
     * Chuyển sang trang mới khi click vào icon hướng dẫn trong form
     * Created by: ntlong ( 01/07/2023 )
     */
    redirectToHelpPage() {
      window.open("https://helpcegov.misa.vn/kb/khai-bao-danh-hieu-thi-dua/", "_blank");
    },
    /**
     * Sự kiện đóng form
     * Created by: ntlong ( 01/07/2023 )
     */
    ...mapMutations("awardStore", ["closeForm"]),

    /**
     * Lấy dữ liệu các ô checkbox được chọn trong form
     * Created by: ntlong ( 11/07/2023 )
     */
    getCheckboxValue() {
      // Đối tượng khen thưởng
      if (this.isChecked1 && this.isChecked2) {
        this.newAward.awardObject = this.$_MISAEnum.AWARD_OBJECT.INDIVIDUAL_OR_GROUP;
      } else if (this.isChecked1) {
        this.newAward.awardObject = this.$_MISAEnum.AWARD_OBJECT.INDIVIDUAL;
      } else if (this.isChecked2) {
        this.newAward.awardObject = this.$_MISAEnum.AWARD_OBJECT.GROUP;
      } else {
        // Nếu không chọn đối tượng nào thì gán bằng null
        this.newAward.awardObject = null;
      }

      // Loại khen thưởng
      if (this.isChecked3 && this.isChecked4) {
        this.newAward.awardType = this.$_MISAEnum.AWARD_TYPE.FREQUENT_OR_PERIODIC;
      } else if (this.isChecked3) {
        this.newAward.awardType = this.$_MISAEnum.AWARD_TYPE.FREQUENT;
      } else if (this.isChecked4) {
        this.newAward.awardType = this.$_MISAEnum.AWARD_TYPE.PERIODIC;
      } else {
        // Nếu không chọn loại nào thì gán bằng null
        this.newAward.awardType = null;
      }
    },

    /**
     * validate trước khi save ( tất cả các trường không được để trống )
     * Created by: ntlong ( 04/07/2023 )
     */
    validateNewAward() {
      return (
        this.newAward.awardName !== "" &&
        this.newAward.awardName !== null &&
        this.newAward.awardCode !== "" &&
        this.newAward.awardCode !== null &&
        this.newAward.awardLevel !== "" &&
        this.newAward.awardLevel !== null &&
        this.newAward.awardObject !== null &&
        this.newAward.awardType !== null
      );
    },

    /**
     * Sự kiện ấn nút lưu và thêm mới trong form
     * Created by: ntlong ( 04/07/2023 )
     */
    async onSaveAndAdd() {
      await this.onSave();
      // Mở ra form mới nếu thêm thành công
      if (this.validateNewAward(this.newAward)) {
        await this.$store.commit("awardStore/openAddNewForm");
      }
    },

    /**
     * Sự kiện ấn nút lưu trong form
     * Created by: ntlong ( 04/07/2023 )
     */
    async onSave() {
      this.getCheckboxValue(); // Lấy các giá trị khi chọn checkbox
      if (this.validateNewAward()) {
        await this.$store.dispatch("awardStore/insertOneAward", this.newAward);
      }
    },

    /**
     * Sự kiện ấn nút lưu thay đổi trong form sửa
     * Created by: ntlong ( 04/07/2023 )
     */
    async onSaveChange() {
      this.getCheckboxValue(); // Lấy các giá trị khi chọn checkbox
      if (this.validateNewAward()) {
        await this.$store.dispatch("awardStore/updateOneAward", this.newAward);
        await this.$store.commit("awardStore/closeForm");
      }
    },

    /**
     * Sự kiện lưu bằng phím tắt
     * Created by: ntlong ( 04/07/2023 )
     */
    async saveByKeyboard(event) {
      if (event.ctrlKey && event.key == "s") {
        event.preventDefault();
        if (this.typeForm === this.$_MISAEnum.FORM_MODE.ADD) {
          await this.onSave();
        } else if (this.typeForm === this.$_MISAEnum.FORM_MODE.EDIT) {
          await this.onSaveChange();
        }
      } else if (event.ctrlKey && event.shiftKey && event.key == "S") {
        event.preventDefault();
        await this.onSaveAndAdd();
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/pages/category/award-form/award-form.scss";

:deep(.popper) {
  white-space: nowrap;
}

input:disabled {
  opacity: 0;
}
</style>
