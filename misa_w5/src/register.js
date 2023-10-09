import MButton from "@/components/MButton.vue";
import MCombobox from "@/components/MCombobox.vue";
import MDialog from "@/components/MDialog.vue";
import MInput from "@/components/MInput.vue";
import MLabel from "@/components/MLabel.vue";
import MLoading from "@/components/MLoading.vue";
import MMenuItem from "@/components/MMenuItem.vue";
import MPaging from "@/components/MPaging.vue";
import MPopup from "@/components/MPopup.vue";
import MTable from "@/components/MTable.vue";
import MToast from "@/components/MToast.vue";
import MTooltip from "@/components/MTooltip.vue";
import MUpload from "@/components/MUpload.vue";
import MImportAttachment from "@/components/MImportAttachment.vue";
import MFileInfo from "@/components/MFileInfo.vue";

export default function registerComponent(app) {
  app.component("MButton", MButton);
  app.component("MCombobox", MCombobox);
  app.component("MDialog", MDialog);
  app.component("MInput", MInput);
  app.component("MLabel", MLabel);
  app.component("MLoading", MLoading);
  app.component("MMenuItem", MMenuItem);
  app.component("MPaging", MPaging);
  app.component("MPopup", MPopup);
  app.component("MTable", MTable);
  app.component("MToast", MToast);
  app.component("MTooltip", MTooltip);
  app.component("MUpload", MUpload);
  app.component("MImportAttachment", MImportAttachment);
  app.component("MFileInfo", MFileInfo);
}
