import MISAEnum from "@/helpers/enum.js";
import MISAResource from "@/helpers/resource.js";

/**
 * Convert dữ liệu trả về từ backend sang dữ liệu hiển thị cho user
 * @param {*} field : trường cần convert
 * @param {*} value : giá trị cần convert
 * @returns
 */
export default function convertData(field, value) {
  try {
    switch (field) {
      case "awardObject":
        switch (value) {
          case MISAEnum.AWARD_OBJECT.INDIVIDUAL:
            return MISAResource.field.object.individual; // "Cá nhân"
          case MISAEnum.AWARD_OBJECT.GROUP:
            return MISAResource.field.object.group; // "Tập thể"
          case MISAEnum.AWARD_OBJECT.INDIVIDUAL_OR_GROUP:
            return MISAResource.field.object.individualOrGroup; // "Cá nhân và tập thể"
          case MISAEnum.AWARD_OBJECT.FAMILY:
            return MISAResource.field.object.family; // "Gia đình"
          default:
            return "";
        }

      case "awardLevel":
        switch (value) {
          case MISAEnum.AWARD_LEVEL.NATIONAL:
            return MISAResource.field.level.national; // "Cấp nhà nước"
          case MISAEnum.AWARD_LEVEL.PROVINCE:
            return MISAResource.field.level.province; // "Cấp Tỉnh/tương đương"
          case MISAEnum.AWARD_LEVEL.DISTRICT:
            return MISAResource.field.level.district; // "Cấp Huyện/tương đương"
          case MISAEnum.AWARD_LEVEL.COMMUNE:
            return MISAResource.field.level.commune; // "Cấp Xã/tương đương"
          default:
            return "";
        }

      case "awardType":
        switch (value) {
          case MISAEnum.AWARD_TYPE.FREQUENT:
            return MISAResource.field.type.frequent; // "Thường xuyên"
          case MISAEnum.AWARD_TYPE.PERIODIC:
            return MISAResource.field.type.periodic; // "Theo đợt"
          case MISAEnum.AWARD_TYPE.FREQUENT_OR_PERIODIC:
            return MISAResource.field.type.frequentOrPeriodic; // "Thường xuyên; Theo đợt"
          default:
            return "";
        }

      case "awardStatus":
        switch (value) {
          case MISAEnum.AWARD_STATUS.ACTIVE:
            return MISAResource.field.status.active; // "Sử dụng"
          case MISAEnum.AWARD_STATUS.INACTIVE:
            return MISAResource.field.status.inactive; // "Ngừng sử dụng"
          default:
            return "";
        }

      default:
        return value ? value : "";
    }
  } catch (error) {
    console.log(error);
  }
}
