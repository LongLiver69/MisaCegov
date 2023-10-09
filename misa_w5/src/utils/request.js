import axios from "axios";
import qs from "qs";
import store from "@/store/index";
import MISAResource from "@/helpers/resource.js";

const request = axios.create({
  baseURL: "https://localhost:7105/api",
  paramsSerializer: (params) => {
    // Sử dụng thư viện qs để serialize các tham số với định dạng lặp lại
    return qs.stringify(params, { arrayFormat: "repeat" });
  },
});

// Thêm một bộ đón chặn request
request.interceptors.request.use(
  function (config) {
    // Làm gì đó trước khi request dược gửi đi
    return config;
  },
  function (error) {
    // Làm gì đó với lỗi request
    return Promise.reject(error);
  }
);

// Thêm một bộ đón chặn response
request.interceptors.response.use(
  function (response) {
    // Bất kì mã trạng thái nào nằm trong tầm 2xx đều khiến hàm này được trigger
    // Làm gì đó với dữ liệu response
    return response;
  },
  function (error) {
    // Bất kì mã trạng thái nào lọt ra ngoài tầm 2xx đều khiến hàm này được trigger
    // Làm gì đó với lỗi response
    if (!error.response || error.response.status >= 500) {
      // Show dialog báo lỗi
      store.commit("setDialogType", MISAResource.dialog.type.error500);
      store.commit("setShowDialog", true);
      return Promise.reject();
    }
    return Promise.reject(error.response.data);
  }
);

export default request;
