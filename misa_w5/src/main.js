import { createApp } from "vue";
import App from "./App.vue";
import Popper from "vue3-popper";
import emitter from "tiny-emitter/instance";

import store from "@/store/index";
import MISAEnum from "@/helpers/enum.js";
import MISAResource from "@/helpers/resource.js";
import registerComponent from "./register";

const app = createApp(App);
app.use(store);
app.component("MPopper", Popper);
registerComponent(app);
app.config.globalProperties.$_emitter = emitter;
app.config.globalProperties.$_MISAEnum = MISAEnum;
app.config.globalProperties.$_MISAResource = MISAResource;

app.mount("#app");
