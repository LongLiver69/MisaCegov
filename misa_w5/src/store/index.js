import { createStore } from "vuex";

import awardStore from "./modules/awardStore";
const store = createStore({
  state: {
    zoomOutSidebar: false,
    showToast: false,
    // Dialog
    showDialog: false,
    dialogType: null,
    deletedAward: null,
    error: null,
  },
  mutations: {
    /**
     * Gán giá trị thể hiện sidebar có được thu nhỏ không
     * @param {boolean} value: giá trị cần gán
     * Created by: ntlong ( 24/07/2023 )
     */
    setZoomOutSidebar(state, value) {
      state.zoomOutSidebar = value;
    },

    /**
     * Gán giá trị cho showToast
     * @param {boolean} value: giá trị cần gán
     * Created by: ntlong ( 24/07/2023 )
     */
    setShowToast(state, value) {
      state.showToast = value;
    },

    /**
     * Thay đổi dữ liệu đóng/mở dialog
     * @param {boolean} value - giá trị
     * Created by: ntlong (03/07/2023)
     */
    setShowDialog(state, value) {
      state.showDialog = value;
    },

    /**
     * Thay đổi dữ liệu thể hiện kiểu dialog
     * @param {boolean} value - giá trị
     * Created by: ntlong (03/07/2023)
     */
    setDialogType(state, value) {
      state.dialogType = value;
    },

    /**
     * Thay đổi dữ liệu đối tượng được chọn để xóa
     * @param {boolean} value - đối tượng được chọn để xóa
     * Created by: ntlong (03/07/2023)
     */
    setDeletedAward(state, value) {
      state.deletedAward = value;
    },

    /**
     * Thay đổi dữ liệu của lỗi chưa xác định được từ trước nhận từ backend
     * @param {Object} value - lỗi
     * Created by: ntlong (03/07/2023)
     */
    setError(state, value) {
      state.error = value;
    },
  },
  actions: {},
  modules: {
    awardStore,
  },
});

export default store;
