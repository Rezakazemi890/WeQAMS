using QAMS.Module.BusinessObjects;

namespace QAMS.Module.Web.Controllers
{
    partial class CalendarController
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
            this.saSyncCalendar = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);

            //saSyncCalendar
            this.saSyncCalendar.Caption = "SyncGoogleCalendar";
            this.saSyncCalendar.Category = "ObjectsCreation";
            this.saSyncCalendar.ConfirmationMessage = "Are you sure want to sync with Google Calendar?";
            this.saSyncCalendar.Id = "saSyncCalendar";
            this.saSyncCalendar.ImageName = "Project";
            this.saSyncCalendar.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.saSyncCalendar.QuickAccess = true;
            this.saSyncCalendar.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.saSyncCalendar.TargetObjectType = typeof(MyScheduler);
            this.saSyncCalendar.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.saSyncCalendar.ToolTip = null;
            this.saSyncCalendar.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.saSyncCalendar.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saSyncCalendar_Execute);


            this.Actions.Add(this.saSyncCalendar);
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saSyncCalendar;
    }
}
