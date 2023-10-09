<template>
  <div class="m-dialog">
    <div class="m-dialog__container">
      <span class="m-dialog__button--close icon24" @click="closeDialog"></span>
      <div class="m-dialog__top">
        <div class="m-dialog__title">{{ title }}</div>
        <div class="m-dialog__message">
          <template v-if="dialogType === $_MISAResource.dialog.type.deleteOne">
            <span
              >{{ $_MISAResource.dialog.description.deleteOne.text1 }}
              <strong>{{ deletedAward.awardName }} </strong
              >{{ $_MISAResource.dialog.description.deleteOne.text2 }}</span
            >
          </template>

          <template v-if="dialogType === $_MISAResource.dialog.type.deleteMany">
            <span
              >{{ $_MISAResource.dialog.description.deleteMany.text1
              }}<strong> {{ selectedAwards.length }} </strong
              >{{ $_MISAResource.dialog.description.deleteMany.text2 }}
            </span>
          </template>

          <template v-if="dialogType === $_MISAResource.dialog.type.notify">
            <span
              >{{ $_MISAResource.dialog.description.notify.text1 }}
              <strong> {{ selectedSystemDataName }} </strong
              >{{ $_MISAResource.dialog.description.notify.text2 }}</span
            >
          </template>

          <template v-if="dialogType === $_MISAResource.dialog.type.error500">
            <span>{{ $_MISAResource.dialog.description.error500 }}</span>
          </template>

          <template v-if="dialogType === $_MISAResource.dialog.type.error901">
            <span
              >{{ $_MISAResource.dialog.description.error901.text1 }}
              <strong> {{ errorAward.awardCode }} </strong
              >{{ $_MISAResource.dialog.description.error901.text2 }}</span
            >
          </template>

          <template v-if="dialogType === $_MISAResource.dialog.type.error">
            <span>{{ error }}</span>
          </template>
        </div>
      </div>
      <div class="m-dialog__footer">
        <template
          v-if="
            dialogType === $_MISAResource.dialog.type.deleteOne ||
            dialogType === $_MISAResource.dialog.type.deleteMany
          "
        >
          <MButton
            variant="btn-outline-secondary"
            :title="$_MISAResource.dialog.button.close"
            @click="closeDialog"
          ></MButton>
          <MButton
            variant="btn-danger"
            :title="$_MISAResource.dialog.button.delete"
            @click="handleSubmit"
          ></MButton>
        </template>
        <template v-else>
          <MButton
            variant="btn-outline-secondary"
            :title="$_MISAResource.dialog.button.close"
            @click="closeDialog"
          ></MButton>
        </template>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "MDialog",
  computed: {
    ...mapState("awardStore", ["selectedAwards", "selectedSystemData", "errorAward"]),
    ...mapState(["deletedAward", "dialogType", "error"]),

    // Tiêu đề của dialog
    title() {
      return this.$_MISAResource.dialog.title[this.dialogType];
    },

    // Lấy ra tên các bản ghi được chọn để xóa và nối thành chuỗi
    selectedSystemDataName() {
      return this.selectedSystemData.map((obj) => obj.awardName).join(", ");
    },
  },
  methods: {
    closeDialog() {
      this.$store.commit("setShowDialog", false);
    },

    handleSubmit() {
      if (this.dialogType === this.$_MISAResource.dialog.type.deleteOne) {
        this.$store.dispatch("awardStore/deleteOneAward", this.deletedAward.awardId);
      } else if (this.dialogType === this.$_MISAResource.dialog.type.deleteMany) {
        const listIds = this.selectedAwards.map(({ awardId }) => awardId);
        this.$store.dispatch("awardStore/deleteManyAward", listIds);
      } else {
        return;
      }
      this.$store.commit("setShowDialog", false);
    },
  },
};
</script>

<style lang="scss" scoped>
.m-dialog {
  width: 100%;
  height: 100%;
  position: fixed;
  top: 0;
  left: 0;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 5;
}

.m-dialog__container {
  display: flex;
  flex-direction: column;
  width: 480px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: #fff;
  border-radius: 4px;
  box-shadow: 0 0 16px rgba(23, 27, 42, 0.24);

  & .m-dialog__button--close {
    position: absolute;
    top: 16px;
    right: 16px;
    cursor: pointer;

    &::before {
      background-position: -246px -30px;
      width: 12px;
      height: 12px;
      position: absolute;
      top: 6px;
      left: 6px;
    }

    &:hover::before {
      background-position: -270px -30px;
    }
  }

  & .m-dialog__top {
    min-height: 144px;
    padding: 20px 16px 16px;

    & .m-dialog__title {
      font-size: 20px;
      font-weight: 700;
      padding-bottom: 20px;
    }

    & .m-dialog__message {
      display: block;
      white-space: pre-wrap;
    }
  }

  & .m-dialog__footer {
    position: relative;
    height: 60px;
    padding: 0 24px 24px 16px;
    display: flex;
    justify-content: flex-end;
    align-items: flex-start;
    column-gap: 8px;
  }
}
</style>
