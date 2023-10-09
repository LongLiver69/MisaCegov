import awardService from "@/services/category/awardService.js";

// ---------------------state---------------------

const state = () => ({
  displayAwards: [],
  totalRecords: 0,
  selectedAwards: [],
  searchText: null,
  filter: null,
  sortField: null,
  sortType: null,
  showAddNewForm: false,
  showEditForm: false,
  showFilterBox: false,
  filterActive: false,
  awardForEdit: {},
  pageSize: 20,
  pageCount: 1,
  currentPage: 1,
  recordRange: {
    recordFrom: 0,
    recordTo: 0,
  },
  selectedSystemData: [],
  errorAward: {},
});

// ---------------------getters---------------------

const getters = {};

// ---------------------mutations---------------------

const mutations = {
  /**
   * Thay đổi dữ liệu của các danh hiệu hiển thị trên màn hình
   * @param {Object[]} awards - Danh sách các danh hiệu thi đua
   * Created by: ntlong (03/07/2023)
   */
  setDisplayAwards(state, awards) {
    state.displayAwards = awards;
  },

  /**
   * Thay đổi dữ liệu của tổng số bản ghi
   * @param {Number} total - Tổng số bản ghi
   * Created by: ntlong (03/07/2023)
   */
  setTotalRecords(state, total) {
    state.totalRecords = total;
  },

  /**
   * Thay đổi dữ liệu của text tìm kiếm
   * @param {string} value - text tìm kiếm
   * Created by: ntlong (03/07/2023)
   */
  setSearchText(state, value) {
    state.searchText = value;
  },

  /**
   * Thay đổi dữ liệu của đối tượng chứa các giá trị lọc
   * @param {Object} text - đối tượng chứa các giá trị lọc
   * Created by: ntlong (03/07/2023)
   */
  setFilter(state, filter) {
    // Dùng spread để không lưu địa chỉ của object mới
    // Tránh tự động thay đổi theo
    state.filter = { ...filter };
  },

  /**
   * Thay đổi dữ liệu của kiểu sắp xếp
   * @param {boolean} type - kiểu sắp xếp
   * Created by: ntlong (03/07/2023)
   */
  setSortType(state, type) {
    state.sortType = type;
  },

  /**
   * Thay đổi dữ liệu của trường được sắp xếp
   * @param {string} field - trường được sắp xếp
   * Created by: ntlong (03/07/2023)
   */
  setSortField(state, field) {
    state.sortField = field;
  },

  /**
   * Thay đổi dữ liệu mở form thêm mới
   * Created by: ntlong (03/07/2023)
   */
  openAddNewForm(state) {
    state.showAddNewForm = true;
  },

  /**
   * Thay đổi dữ liệu đóng các form
   * Created by: ntlong (03/07/2023)
   */
  closeForm(state) {
    state.showAddNewForm = false;
    state.showEditForm = false;
  },

  /**
   * Thay đổi dữ liệu mở form sửa
   * @param {boolean} value - dữ liệu cho phép mở form sửa
   * Created by: ntlong (03/07/2023)
   */
  setShowEditForm(state, value) {
    state.showEditForm = value;
  },

  /**
   * Thay đổi dữ liệu của các bản ghi được chọn (thêm vào)
   * @param {object} award - bản ghi được chọn
   * Created by: ntlong (03/07/2023)
   */
  addToSelectedAwards(state, award) {
    state.selectedAwards.push(award);
  },

  /**
   * Thay đổi dữ liệu của các bản ghi được chọn (xóa đi)
   * @param {string} award - bản ghi được chọn
   * Created by: ntlong (03/07/2023)
   */
  removeFromSelectedAwards(state, award) {
    state.selectedAwards = state.selectedAwards.filter((item) => item.awardId !== award.awardId);
  },

  /**
   * Thay đổi dữ liệu đóng mở bộ lọc
   * @param {boolean} value - giá trị đóng hay mở
   * Created by: ntlong (03/07/2023)
   */
  setShowFilterBox(state, value) {
    state.showFilterBox = value;
  },

  /**
   * Thay đổi dữ liệu số lượng bản ghi giới hạn một trang
   * @param {Number} value - giá trị số lượng
   * Created by: ntlong (03/07/2023)
   */
  setPageSize(state, value) {
    state.pageSize = value;
  },

  /**
   * Thay đổi dữ liệu số thứ tự của trang hiện tại
   * @param {Number} value - giá trị số thứ tự
   * Created by: ntlong (03/07/2023)
   */
  setCurrentPage(state, value) {
    state.currentPage = value;
  },

  /**
   * Thay đổi dữ liệu tổng số trang hiện có
   * @param {Number} value - giá trị số lượng
   * Created by: ntlong (03/07/2023)
   */
  setPageCount(state, value) {
    state.pageCount = value;
  },

  /**
   * Thay đổi dữ liệu đối tượng thể hiện phạm vi các bản ghi đang hiển thị ở trang hiện tại
   * @param {Object} value - đối tượng có 2 trường là điểm bắt đầu và kết thúc
   * Created by: ntlong (03/07/2023)
   */
  setRecordRange(state, value) {
    state.recordRange = { ...value };
  },

  /**
   * Thay đổi dữ liệu danh sách các bản ghi được chọn thành các bản ghi ở trang đang hiển thị
   * (Tức là chọn tất cả)
   * Created by: ntlong (03/07/2023)
   */
  selectAll(state) {
    state.selectedAwards = state.displayAwards;
  },

  /**
   * Thay đổi dữ liệu danh sách các bản ghi được chọn thành danh sách rỗng
   * (Tức là bỏ tất cả các ô đang chọn)
   * Created by: ntlong (03/07/2023)
   */
  unSelectAll(state) {
    state.selectedAwards = [];
  },

  /**
   * Thay đổi dữ liệu của bản ghi được chọn để sửa
   * @param {Object} value - đối tượng bản ghi được chọn để sửa
   * Created by: ntlong (03/07/2023)
   */
  setAwardForEdit(state, value) {
    state.awardForEdit = { ...value };
  },

  /**
   * Thay đổi dữ liệu thể hiện bộ lọc đang hoạt động hay không
   * @param {boolean} value - giá trị
   * Created by: ntlong (03/07/2023)
   */
  setFilterActive(state, value) {
    state.filterActive = value;
  },

  /**
   * Thay đổi list dữ liệu hệ thống đang được chọn
   * @param {boolean} value - giá trị
   * Created by: ntlong (03/07/2023)
   */
  setSelectedSystemData(state, value) {
    state.selectedSystemData = value;
  },

  /**
   * Thay đổi dữ liệu của bản ghi không thỏa mãn
   * @param {Object} value - bản ghi không thỏa mãn
   * Created by: ntlong (03/07/2023)
   */
  setErrorAward(state, value) {
    state.errorAward = value;
  },
};

// ---------------------actions---------------------

const actions = {
  /**
   * Call API lấy dữ liệu danh hiệu theo trang cần hiển thị
   * Created by: ntlong ( 28/06/2023 )
   */
  async getDisplayAwards({ commit, state }) {
    try {
      var result = await awardService.getFPSSAwards(state);
      await commit("setTotalRecords", result.total);
      await commit("setDisplayAwards", result.data);
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Sự kiện click vào một ô checkbox ở tbody
   * Created by: ntlong ( 28/06/2023 )
   */
  onSelectCheckbox({ commit, state }, award) {
    if (state.selectedAwards.some((item) => item.awardId === award.awardId)) {
      commit("removeFromSelectedAwards", award);
    } else {
      commit("addToSelectedAwards", award);
    }
  },

  /**
   * Sự kiện khi click vào ô checkbox chọn tất cả
   * Created by: ntlong ( 28/06/2023 )
   */
  onClickSelectAll({ commit, state }) {
    if (state.selectedAwards.length === 0) {
      commit("selectAll");
    } else {
      commit("unSelectAll");
    }
  },

  /**
   * Update dữ liệu khi thay đổi
   * Created by: ntlong ( 28/06/2023 )
   */
  async updateData({ commit, state }) {
    // Update số lượng trang khi pageSize thay đổi
    var newPageCount = Math.ceil(state.totalRecords / state.pageSize);
    if (newPageCount < state.currentPage) {
      await commit("setCurrentPage", 1);
    }
    await commit("setPageCount", newPageCount);
    // Update phạm vi hiển thị của trang hiện tại
    var recordFrom = (state.currentPage - 1) * state.pageSize + 1;
    var recordTo = state.currentPage * state.pageSize;
    // Trường hợp không có bản ghi nào
    if (recordFrom > state.totalRecords) {
      recordFrom = state.totalRecords;
    }
    // Trường hợp số bản ghi ở trang đang hiển thị không bằng size của trang (Xảy ra ở trang cuối)
    if (recordTo > state.totalRecords) {
      recordTo = state.totalRecords;
    }
    await commit("setRecordRange", { recordFrom, recordTo });
  },

  /**
   * Refresd dữ liệu hiển thị khi thay đổi
   * Created by: ntlong ( 28/06/2023 )
   */
  async refreshDisplay({ dispatch }) {
    await dispatch("getDisplayAwards");
    await dispatch("updateData");
    await dispatch("getDisplayAwards");
  },

  /**
   * Xử lý sự kiện thêm mới 1 danh hiệu
   * Created by: ntlong ( 11/07/2023 )
   */
  async insertOneAward({ dispatch }, newAward) {
    try {
      await awardService.insertOneAward(newAward);
      await dispatch("getDisplayAwards");
      await dispatch("refreshDisplay");
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Xử lý sự kiện sửa 1 danh hiệu
   * Created by: ntlong ( 11/07/2023 )
   */
  async updateOneAward({ dispatch }, newAward) {
    try {
      await awardService.updateOneAward(newAward);
      await dispatch("getDisplayAwards");
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Xử lý sự kiện sửa 1 danh hiệu
   * Created by: ntlong ( 11/07/2023 )
   */
  async updateManyAwardStatus({ commit, dispatch }, { listIds, newAwardStatus }) {
    try {
      await awardService.updateManyAwardStatus(listIds, newAwardStatus);
      await commit("unSelectAll");
      await dispatch("getDisplayAwards");
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Xử lý sự kiện xóa 1 danh hiệu
   * Created by: ntlong ( 11/07/2023 )
   */
  async deleteOneAward({ dispatch }, id) {
    try {
      await awardService.deleteOneAward(id);
      await dispatch("getDisplayAwards");
      await dispatch("refreshDisplay");
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Xử lý sự kiện xóa nhiều danh hiệu
   * Created by: ntlong ( 11/07/2023 )
   */
  async deleteManyAward({ commit, dispatch }, listIds) {
    try {
      await awardService.deleteManyAward(listIds);
      await commit("unSelectAll");
      await dispatch("getDisplayAwards");
      await dispatch("refreshDisplay");
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Sự kiện ấn nút áp dụng lọc dữ liệu
   * Created by: ntlong ( 04/07/2023 )
   */
  async handleFilterData({ commit, dispatch, state }, attrsFilter) {
    // Đóng bộ lọc
    await commit("setShowFilterBox", false);

    // Nếu bất kỳ trường lọc nào khác null thì sẽ là đã được lọc
    if (
      attrsFilter.filteredObject !== null ||
      attrsFilter.filteredLevel !== null ||
      attrsFilter.filteredType !== null ||
      attrsFilter.filteredStatus !== null
    ) {
      // Nếu trạng thái vẫn là true thì không cần thay đổi
      if (state.filterActive !== true) {
        await commit("setFilterActive", true);
      }
      // } else {
      //   // Nếu trạng thái vẫn là false thì không cần thay đổi
      //   if (state.filterActive !== false) {
      //     await commit("setFilterActive", false);
      //   }
    }

    // Refresh lại dữ liệu hiển thị nếu có trường lọc thay đổi
    if (
      state.filter?.filteredObject !== attrsFilter.filteredObject ||
      state.filter?.filteredLevel !== attrsFilter.filteredLevel ||
      state.filter?.filteredType !== attrsFilter.filteredType ||
      state.filter?.filteredStatus !== attrsFilter.filteredStatus
    ) {
      // Cập nhật các giá trị lọc ở store
      await commit("setFilter", attrsFilter);
      await dispatch("refreshDisplay");
    } else {
      await commit("setFilter", attrsFilter);
    }
  },

  /**
   * Xử lý sự kiện bỏ lọc
   * Created by: ntlong (02/07/2023)
   */
  async handleUnfiltered({ commit, dispatch }) {
    // Cập nhật các giá trị lọc ở store
    await commit("setFilter", null);
    // Trạng thái chuyển thành không lọc
    await commit("setFilterActive", false);
    // Refresh lại dữ liệu hiển thị
    await dispatch("refreshDisplay");
  },

  /**
   * Xử lý sự kiện sắp xếp
   * Created by: ntlong (02/08/2023)
   */
  async handleSort({ commit, dispatch, state }, sortField) {
    if (state.sortField === null || state.sortField !== sortField) {
      await commit("setSortField", sortField); // Cập nhật giá trị trường được sắp xếp
      await commit("setSortType", true); // Cập nhật giá trị loại sắp xếp
    } else {
      if (state.sortType === true) {
        await commit("setSortType", false);
      } else if (state.sortType === false) {
        await commit("setSortType", null);
        await commit("setSortField", null);
      } else {
        // trường hợp sortType=null
        await commit("setSortType", true);
      }
    }
    // Refresh lại dữ liệu hiển thị
    await dispatch("refreshDisplay");
  },
};

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
};
