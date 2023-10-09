import request from "@/utils/request.js";
import store from "@/store/index";
import MISAEnum from "@/helpers/enum.js";
import MISAResource from "@/helpers/resource.js";

export default {
  /**
   * Lấy ra tất cả danh hiệu trong bảng award
   * Created by: ntlong ( 18/07/2023 )
   */
  async getAwards() {
    try {
      const res = await request.get("/v1/awards");
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },

  /// <summary>
  /// Lấy danh sách bản ghi bao gồm phân trang, lọc, tìm kiếm, sắp xếp
  /// Ý nghĩa "FPSS": Filter-Paging-Search-Sort
  /// </summary>
  /// @param name="page": Số thứ tự của trang cần lấy
  /// @param name="size": Số lượng tối đa một trang
  /// @param name="search": Chuỗi tìm kiếm
  /// @param name="searchFields": Danh sách các trường cần tìm kiếm
  /// @param name="filteredObject": Giá trị lọc của đối tượng khen thưởng
  /// @param name="filteredLevel": Giá trị lọc của cấp khen thưởng
  /// @param name="filteredType": Giá trị lọc của loại khen thưởng
  /// @param name="filteredStatus": Giá trị lọc của trạng thái
  /// @param name="sortField": Trường được sắp xếp
  /// @param name="sortType": Kiểu sắp xếp: true là tăng dần, false là giảm dần
  /// <returns>Tổng số và danh sách bản ghi theo trang cần hiển thị</returns>
  /// Created by: ntlong ( 25/07/2023 )
  async getFPSSAwards(state) {
    try {
      const res = await request.get("/v1/Awards/ListFPSS", {
        params: {
          page: state.currentPage,
          size: state.pageSize,
          search: state.searchText,
          searchFields: ["awardName", "awardCode"],
          filteredObject: state.filter?.filteredObject,
          filteredLevel: state.filter?.filteredLevel,
          filteredType: state.filter?.filteredType,
          filteredStatus: state.filter?.filteredStatus,
          sortField: state.sortField ?? "AwardStatus",
          sortType: state.sortType ?? true,
        },
      });
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Thêm 1 danh hiệu vào trong bảng award
   * @param newAward: Đối tượng danh hiệu cần thêm mới
   * Created by: ntlong ( 18/07/2023 )
   */
  async insertOneAward(newAward) {
    try {
      const res = await request.post("/v1/Awards", newAward);
      store.commit("awardStore/closeForm"); // Đóng form
      store.commit("setShowToast", true); // Thông báo Thành Công
      return res.data;
    } catch (data) {
      if (data?.ErrorCode === 409) {
        // Show dialog báo lỗi
        store.commit("awardStore/setErrorAward", newAward);
        store.commit("setShowDialog", true);
        store.commit("setDialogType", MISAResource.dialog.type.error901);
        return;
      }
      if (data) {
        // Show dialog báo lỗi
        store.commit("setDialogType", MISAResource.dialog.type.error);
        store.commit("setError", data.UserMessage);
        store.commit("setShowDialog", true);
      }
    }
  },

  /**
   * Sửa 1 danh hiệu
   * @param newAward: Đối tượng danh hiệu cần sửa
   * Created by: ntlong ( 18/07/2023 )
   */
  async updateOneAward(newAward) {
    try {
      const res = await request.put(`/v1/Awards/${newAward.awardId}`, newAward);
      store.commit("awardStore/closeForm"); // Đóng form
      store.commit("setShowToast", true); // Thông báo Thành Công
      return res.data;
    } catch (data) {
      if (data?.ErrorCode === 409) {
        // Show dialog báo lỗi
        store.commit("awardStore/setErrorAward", newAward);
        store.commit("setDialogType", MISAResource.dialog.type.error901);
        store.commit("setShowDialog", true);
        return;
      }
      if (data) {
        // Show dialog báo lỗi
        store.commit("setDialogType", MISAResource.dialog.type.error);
        store.commit("setError", data.UserMessage);
        store.commit("setShowDialog", true);
      }
    }
  },

  /**
   * Xóa 1 danh hiệu
   * @param id: Id của danh hiệu cần xóa
   * Created by: ntlong ( 18/07/2023 )
   */
  async deleteOneAward(id) {
    try {
      const res = await request.delete(`/v1/Awards/${id}`);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Xóa nhiều danh hiệu
   * @param listIds: Mảng các id của danh hiệu cần xóa
   * Created by: ntlong ( 18/07/2023 )
   */
  async deleteManyAward(listIds) {
    try {
      await request.delete(`/v1/Awards`, {
        headers: {
          "Content-Type": "application/json",
        },
        data: listIds,
      });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Sửa trạng thái của nhiều danh hiệu
   * @param listIds: Mảng các id của danh hiệu cần sửa
   * @param newAwardStatus: trạng thái mới
   * Created by: ntlong ( 18/07/2023 )
   */
  async updateManyAwardStatus(listIds, newAwardStatus) {
    try {
      await request.put(`/v1/Awards/Status`, listIds, {
        headers: {
          "Content-Type": "application/json",
        },
        params: { newStatus: newAwardStatus },
      });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Sửa trạng thái của nhiều danh hiệu
   * @param listIds: Mảng các id của danh hiệu cần sửa
   * @param newAwardStatus: trạng thái mới
   * Created by: ntlong ( 18/07/2023 )
   */
  async exportExcel() {
    try {
      const tableFormat = {
        tableTitle: MISAResource.table.title,
        sheetName: MISAResource.table.title,
        columns: [
          {
            fieldName: "Order",
            displayName: MISAResource.table.columnName.order,
            format: MISAEnum.CONTENT_FORMAT.INT,
            align: MISAEnum.ALIGN.MIDDLE_CENTER,
          },
          {
            fieldName: "AwardName",
            displayName: MISAResource.table.columnName.name,
            format: MISAEnum.CONTENT_FORMAT.NAME,
            align: MISAEnum.ALIGN.MIDDLE_LEFT,
          },
          {
            fieldName: "AwardCode",
            displayName: MISAResource.table.columnName.code,
            format: MISAEnum.CONTENT_FORMAT.STRING,
            align: MISAEnum.ALIGN.MIDDLE_LEFT,
          },
          {
            fieldName: "AwardObject",
            displayName: MISAResource.table.columnName.object,
            format: MISAEnum.CONTENT_FORMAT.ENUM,
            align: MISAEnum.ALIGN.MIDDLE_LEFT,
          },
          {
            fieldName: "AwardLevel",
            displayName: MISAResource.table.columnName.level,
            format: MISAEnum.CONTENT_FORMAT.ENUM,
            align: MISAEnum.ALIGN.MIDDLE_LEFT,
          },
          {
            fieldName: "AwardType",
            displayName: MISAResource.table.columnName.type,
            format: MISAEnum.CONTENT_FORMAT.ENUM,
            align: MISAEnum.ALIGN.MIDDLE_LEFT,
          },
          {
            fieldName: "AwardStatus",
            displayName: MISAResource.table.columnName.status,
            format: MISAEnum.CONTENT_FORMAT.ENUM,
            align: MISAEnum.ALIGN.MIDDLE_LEFT,
          },
        ],
      };
      const res = await request.post(`/v1/Awards/Exportation`, tableFormat, {
        responseType: "blob",
      });
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Lấy ra file nhập khẩu mẫu
   * Created by: ntlong ( 18/07/2023 )
   */
  async getSampleImportFile() {
    try {
      const res = await request.get(`/v1/Awards/SampleImportFile`, {
        responseType: "blob",
      });
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Lấy thông tin và kiểm tra tính hợp lệ của file được upload
   * @param {File} file: File cần lấy thông tin
   * Created by: ntlong ( 25/07/2023 )
   */
  async getFileInfo(file) {
    try {
      // Tạo đối tượng FormData và thêm tệp tin vào
      const formData = new FormData();
      formData.append("file", file);
      const res = await request.post("/v1/Awards/FileInfo", formData);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Lấy ra số bản ghi hợp lệ và không hợp lệ của file được upload
   * @param {File} file: File cần lấy thông tin
   * @param {Int} sheetIndex: Thứ tự của sheet cần check
   * @param {Int} titleLine: Thứ tự của dòng tiêu đề
   * Created by: ntlong ( 25/07/2023 )
   */
  async getImportDataStatistics(file, sheetIndex, titleLine) {
    try {
      // Tạo đối tượng FormData và thêm thông tin
      const formData = new FormData();
      formData.append("file", file);
      formData.append("sheetIndex", sheetIndex);
      formData.append("titleLine", titleLine);
      const res = await request.post("/v1/Awards/FileChecking", formData);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Nhập dữ liệu từ file vào database
   * @param {File} file: File cần lấy thông tin
   * @param {Int} sheetIndex: Thứ tự của sheet cần check
   * @param {Int} titleLine: Thứ tự của dòng tiêu đề
   * Created by: ntlong ( 25/07/2023 )
   */
  async importExcel(file, sheetIndex, titleLine) {
    try {
      // Tạo đối tượng FormData và thêm thông tin
      const formData = new FormData();
      formData.append("file", file);
      formData.append("sheetIndex", sheetIndex);
      formData.append("titleLine", titleLine);
      const res = await request.post("/v1/Awards/Importation", formData);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  },
};
