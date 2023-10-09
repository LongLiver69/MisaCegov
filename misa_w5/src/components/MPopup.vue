<template>
  <div class="m-popup">
    <div class="default-popup" :style="[{ width: width }, { height: height }]">
      <div class="popup-container">
        <div class="popup__header">
          <div class="popup-header__title">{{ title }}</div>
          <div class="popup-header__actions">
            <slot name="header-btns"></slot>
            <MPopper placement="top" hover arrow :content="$_MISAResource.form.tooltip.close">
              <div class="icon24 close" @click="$emit('closePopup')"></div>
            </MPopper>
          </div>
        </div>
        <div class="popup__content">
          <slot></slot>
        </div>
        <div class="popup__footer">
          <slot name="popup-footer"></slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MPopup",
  props: {
    title: String,
    width: String,
    height: String,
  },
};
</script>

<style lang="scss" scoped>
.m-popup {
  width: 100%;
  height: 100%;
  position: fixed;
  top: 0;
  left: 0;
  background-color: rgba(0, 0, 0, 0.3);
  z-index: 5;
}

.default-popup {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);

  & .popup-container {
    width: 100%;
    display: flex;
    flex-direction: column;
    background-color: #fff;
    border-radius: 4px;
    box-shadow: 0 0 16px rgba(23, 27, 42, 0.24);
  }
}

.popup-header__title {
  font-size: 20px;
  padding: 24px 0 6px 24px;
  font-weight: 700;
  min-height: 24px;
}

.popup-header__actions {
  position: absolute;
  top: 24px;
  right: 24px;
  display: flex;
  justify-content: center;
  align-items: center;
  column-gap: 12px;

  & .close {
    display: block;
  }

  & .close::before {
    background-position: -246px -30px;
    width: 12px;
    height: 12px;
    position: absolute;
    top: 6px;
    left: 6px;
  }

  & .close:hover {
    cursor: pointer;
  }

  & .close:hover::before {
    background-position: -270px -30px;
  }
}

.popup__content {
  height: calc(100% - 56px);
}

.popup__footer {
  width: 100%;
  padding: 12px 24px;
  border-top: 1px solid #e0e0e0;
}
</style>
