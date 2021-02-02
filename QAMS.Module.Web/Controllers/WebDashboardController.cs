using DevExpress.DashboardWeb;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Web;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.Web.Controllers
{
    public class WebDashboardController : ObjectViewController<DetailView, IDashboardData>
    {
        private WebDashboardViewerViewItem dashboardViewerViewItem;
        protected override void OnActivated()
        {
            base.OnActivated();
            dashboardViewerViewItem = View.FindItem("DashboardViewer") as WebDashboardViewerViewItem;
            if (dashboardViewerViewItem != null)
            {
                if (dashboardViewerViewItem.DashboardControl != null)
                {
                    if (SecuritySystem.CurrentUser != null
                           && (SecuritySystem.CurrentUser as BusinessObjects.MyUser) != null
                           && (SecuritySystem.CurrentUser as BusinessObjects.MyUser).Roles.Where(x => !x.IsAdministrative) != null
                           && (SecuritySystem.CurrentUser as BusinessObjects.MyUser).Roles.Where(x => !x.IsAdministrative).Count() > 0)
                        //if (dashboardViewerViewItem.DashboardControl.TemplateSourceDirectory.Contains("DashboardViewer"))
                        dashboardViewerViewItem.DashboardControl.WorkingMode = WorkingMode.ViewerOnly;
                }
                else
                {
                    dashboardViewerViewItem.ControlCreated += DashboardViewerViewItem_ControlCreated;
                }
            }
        }
        private void DashboardViewerViewItem_ControlCreated(object sender, EventArgs e)
        {
            if (SecuritySystem.CurrentUser != null
                && (SecuritySystem.CurrentUser as BusinessObjects.MyUser) != null
                && (SecuritySystem.CurrentUser as BusinessObjects.MyUser).Roles.Where(x => !x.IsAdministrative) != null
                && (SecuritySystem.CurrentUser as BusinessObjects.MyUser).Roles.Where(x => !x.IsAdministrative).Count() > 0)
                //if (dashboardViewerViewItem.DashboardControl.TemplateSourceDirectory.Contains("DashboardViewer") || dashboardViewerViewItem.DashboardControl.AppRelativeTemplateSourceDirectory.Contains("DashboardViewer"))
                dashboardViewerViewItem.DashboardControl.WorkingMode = WorkingMode.ViewerOnly;
        }
        protected override void OnDeactivated()
        {
            if (dashboardViewerViewItem != null)
            {
                dashboardViewerViewItem.ControlCreated -= DashboardViewerViewItem_ControlCreated;
                dashboardViewerViewItem = null;
            }
            base.OnDeactivated();
        }
    }
}
