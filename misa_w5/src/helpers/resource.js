const MISAResource = {
  productName: "MISA CeGov",
  pageTitle: "Danh hiệu thi đua",

  header: {
    notify: "Thông báo",
    help: "Hướng dẫn",
    moreMenu: "Chức năng khác",
    docsHelp: "Văn bản quy định",
    setting: "Thiết lập hệ thống",
  },

  sidebar: {
    dashboard: "Tổng quan",
    movement: "Thi đua",
    reward: "Khen thưởng",
    decision: "Quyết định",
    profile: "Hồ sơ",
    search: "Tra cứu",
    report: "Báo cáo",
    category: "Danh mục",
    system: "Thiết lập",
    guide: "Hướng dẫn",
    zoomOut: "Thu gọn",
  },

  validate: {
    nameFormat: "Tên danh hiệu thi đua không được để trống.",
    codeFormat: "Mã danh hiệu không được để trống.",
    levelFormat: "Cấp khen thưởng không được để trống.",
    objectFormat: "Đối tượng khen thưởng không được để trống.",
    typeFormat: "Loại phong trào không được để trống.",
    levelError: " Cấp khen thưởng không đúng. Vui lòng chọn Cấp khen thưởng trong danh sách.",
    sheetFormat: "Sheet nhập khẩu không được để trống.",
    sheetError: "Sheet nhập khẩu không đúng. Vui lòng chọn sheet nhập khẩu trong danh sách.",
  },

  label: {
    titleLine: "Dòng tiêu đề",
    importSheet: "Sheet nhập khẩu",
  },

  contextMenu: {
    import: "Nhập khẩu",
    export: "Xuất khẩu",
    active: "Sử dụng",
    inactive: "Ngừng sử dụng",
    delete: "Xóa",
  },

  table: {
    title: "Danh hiệu thi đua",
    columnName: {
      order: "STT",
      name: "Tên danh hiệu thi đua",
      code: "Mã danh hiệu",
      object: "Đối tượng khen thưởng",
      level: "Cấp khen thưởng",
      type: "Loại phong trào",
      status: "Trạng thái",
    },
    noData: "Không có dữ liệu",
  },

  field: {
    object: {
      all: "Tất cả",
      individual: "Cá nhân",
      group: "Tập thể",
      individualOrGroup: "Cá nhân và tập thể",
      family: "Gia đình",
    },

    level: {
      all: "Tất cả",
      national: "Cấp Nhà nước",
      province: "Cấp Tỉnh/tương đương",
      district: "Cấp Huyện/tương đương",
      commune: "Cấp Xã/tương đương",
    },

    type: {
      all: "Tất cả",
      frequent: "Thường xuyên",
      periodic: "Theo đợt",
      frequentOrPeriodic: "Thường xuyên; Theo đợt",
    },

    status: {
      all: "Tất cả",
      active: "Sử dụng",
      inactive: "Ngừng sử dụng",
    },
  },

  form: {
    title: {
      add: "Thêm danh hiệu thi đua",
      edit: "Sửa danh hiệu thi đua",
      import: "Nhập khẩu",
    },
    tooltip: {
      help: "Hướng dẫn",
      close: "Đóng",
    },
    placeholder: {
      nameInput: "Nhập tên danh hiệu thi đua",
      codeInput: "Nhập mã danh hiệu",
      levelInput: "Chọn hiện vật khen thưởng",
      noteInput: "Nhập ghi chú",
    },
  },

  tooltip: {
    edit: "Sửa",
    more: "Thêm nữa...",
  },

  placeholder: {
    search: "Nhập mã hoặc tên danh hiệu...",
  },

  button: {
    cancel: "Hủy",
    saveAndAdd: "Lưu & thêm mới",
    save: "Lưu",
    saveChange: "Lưu thay đổi",
    continue: "Tiếp tục",
    filter: "Bộ lọc",
    unfilter: "Bỏ lọc",
    apply: "Áp dụng",
    close: "Đóng",
    disagree: "Không",
    delete: "Xóa",
    deleteAward: "Xóa danh hiệu",
    addAward: "Thêm danh hiệu",
    unselect: "Bỏ chọn",
    active: "Sử dụng",
    inactive: "Ngừng sử dụng",
    downloadSampleFile: "Tải tệp mẫu",
    changeFile: "Đổi tệp khác",
    back: "Quay lại",
    import: "Nhập khẩu",
  },

  popup: {
    step: {
      stepOne: "Chọn tệp nhập khẩu",
      stepTwo: "Xuất khẩu",
    },
  },

  boxDetail: {
    title: {
      recordNumber: "Số bản ghi",
      valid: "Hợp lệ",
      invalid: "Không hợp lệ",
    },
  },

  file: {
    validate: {
      valid: "Hợp lệ",
      invalid: "Không hợp lệ",
    },
  },

  dialog: {
    type: {
      notify: "notify",
      deleteOne: "deleteOne",
      deleteMany: "deleteMany",
      error: "error",
      error500: "error500",
      error901: "error901",
    },
    title: {
      notify: "Thông báo",
      deleteOne: "Xóa Danh hiệu thi đua",
      deleteMany: "Xóa Danh hiệu thi đua",
      error: "MISA CeGov",
      error500: "MISA CeGov",
      error901: "MISA CeGov",
    },
    button: {
      close: "Đóng",
      disagree: "Không",
      delete: "Xóa danh hiệu",
    },
    description: {
      notify: {
        text1: "Danh hiệu ",
        text2: " là dữ liệu hệ thống, bạn không thể xóa.",
      },
      deleteOne: {
        text1: "Bạn có chắc chắn muốn xóa Danh hiệu thi đua ",
        text2: " đã chọn không?",
      },
      deleteMany: {
        text1: "Xóa ",
        text2: " danh hiệu đã chọn?",
      },
      error500: "Có lỗi xảy ra vui lòng liên hệ Misa để được giúp đỡ!",
      error901: {
        text1: "Mã danh hiệu thi đua ",
        text2: " đã tồn tại trong danh sách. Xin vui lòng kiểm tra lại.",
      },
      errorFileFormat: "Nhập khẩu chỉ hỗ trợ định dạng *.xls, *.xlsx",
    },
  },
};

export default MISAResource;
