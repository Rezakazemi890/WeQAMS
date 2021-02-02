using QAMS.Module.BusinessObjects.TestManagement;

namespace QAMS.Module.Controllers
{
    partial class ActionController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.popTestCaseAssignRunResultNP = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popUserAssignRunResultNP = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);

            //popTestCaseAssignRunResultNP
            this.popTestCaseAssignRunResultNP.AcceptButtonCaption = null;
            this.popTestCaseAssignRunResultNP.CancelButtonCaption = null;
            this.popTestCaseAssignRunResultNP.Caption = "Create Run Results";
            this.popTestCaseAssignRunResultNP.Category = "ObjectsCreation";
            this.popTestCaseAssignRunResultNP.ConfirmationMessage = null;
            this.popTestCaseAssignRunResultNP.Id = "popTestCaseAssignRunResultNP";
            this.popTestCaseAssignRunResultNP.ImageName = "AddParagraphToTableOfContents";
            this.popTestCaseAssignRunResultNP.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.popTestCaseAssignRunResultNP.QuickAccess = true;
            this.popTestCaseAssignRunResultNP.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.Independent;
            this.popTestCaseAssignRunResultNP.TargetObjectType = typeof(TestRunResult);
            this.popTestCaseAssignRunResultNP.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popTestCaseAssignRunResultNP.ToolTip = null;
            this.popTestCaseAssignRunResultNP.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popTestCaseAssignRunResultNP.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popTestCaseAssignRunResultNP_CustomizePopupWindowParams);
            this.popTestCaseAssignRunResultNP.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popTestCaseAssignRunResultNP_Execute);

            //popUserAssignRunResultNP
            this.popUserAssignRunResultNP.AcceptButtonCaption = null;
            this.popUserAssignRunResultNP.CancelButtonCaption = null;
            this.popUserAssignRunResultNP.Caption = "Assign Users To Run Results";
            this.popUserAssignRunResultNP.Category = "ObjectsCreation";
            this.popUserAssignRunResultNP.ConfirmationMessage = null;
            this.popUserAssignRunResultNP.Id = "popUserAssignRunResultNP";
            this.popUserAssignRunResultNP.ImageName = "NewEmployee";
            this.popUserAssignRunResultNP.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.popUserAssignRunResultNP.QuickAccess = true;
            this.popUserAssignRunResultNP.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.popUserAssignRunResultNP.TargetObjectType = typeof(TestRunResult);
            this.popUserAssignRunResultNP.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popUserAssignRunResultNP.ToolTip = null;
            this.popUserAssignRunResultNP.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popUserAssignRunResultNP.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popUserAssignRunResultNP_CustomizePopupWindowParams);
            this.popUserAssignRunResultNP.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popUserAssignRunResultNP_Execute);


            this.Actions.Add(this.popTestCaseAssignRunResultNP);
            this.Actions.Add(this.popUserAssignRunResultNP);
        }

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popTestCaseAssignRunResultNP;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popUserAssignRunResultNP;
        #endregion
    }
}
