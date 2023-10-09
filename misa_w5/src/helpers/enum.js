const MISAEnum = {
  FORM_MODE: {
    ADD: 0,
    EDIT: 1,
  },
  // Đối tượng khen thưởng
  AWARD_OBJECT: {
    INDIVIDUAL: 1, // "Cá nhân"
    GROUP: 2, // "Tập thể"
    INDIVIDUAL_OR_GROUP: 3, // "Cá nhân và tập thể"
    FAMILY: 4, // "Gia đình"
  },
  // Cấp khen thưởng
  AWARD_LEVEL: {
    NATIONAL: 1, // "Cấp nhà nước"
    PROVINCE: 2, // "Cấp Tỉnh/tương đương"
    DISTRICT: 3, // "Cấp Huyện/tương đương"
    COMMUNE: 4, // "Cấp Xã/tương đương"
  },
  // Loại phong trào
  AWARD_TYPE: {
    FREQUENT: 1, // "Thường xuyên"
    PERIODIC: 2, // "Theo đợt"
    FREQUENT_OR_PERIODIC: 3, // "Thường xuyên; Theo đợt"
  },
  // Trạng thái danh hiệu
  AWARD_STATUS: {
    ACTIVE: 1, // "Sử dụng"
    INACTIVE: 2, // "Ngừng sử dụng"
  },

  // Kiểu dữ liệu của các cột trong file excel
  CONTENT_FORMAT: {
    NONE: 0,
    STRING: 1,
    INT: 2,
    BOOLEAN: 3,
    TIME: 4,
    DATE: 5,
    DATETIME: 6,
    ENUM: 7,

    PERSONALID: 8,
    NAME: 9,
    GENDER: 10,
    DOF: 11,
    NUMBER: 12,
    PHONE: 13,
    CURRENCY: 14,
  },

  // Giá trị của thuộc tính căn chỉnh phần tử trong file excel
  ALIGN: {
    NONE: 0,
    MIDDLE: 1,
    TOP: 2,
    BOTTOM: 4,
    CENTER: 8,
    LEFT: 16,
    RIGHT: 32,
    TOP_LEFT: 18,
    TOP_CENTER: 10,
    TOP_RIGHT: 34,
    MIDDLE_LEFT: 17,
    MIDDLE_CENTER: 9,
    MIDDLE_RIGHT: 33,
    BOTTOM_LEFT: 20,
    BOTTOM_CENTER: 12,
    BOTTOM_RIGHT: 36,
  },
};
export default MISAEnum;
