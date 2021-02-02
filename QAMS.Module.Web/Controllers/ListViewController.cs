using DevExpress.ExpressApp;

using DevExpress.Persistent.Base.General;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Web.ASPxTreeList;
using DevExpress.ExpressApp.TreeListEditors.Web;
using DevExpress.XtraTreeList;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;

namespace QAMS.Module.Web.Controllers
{
    public class ListViewController : ObjectViewController<ListView,BaseObject>
    {

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
        private ASPxGridListEditor gridListEditor;
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            gridListEditor = View.Editor as ASPxGridListEditor;
            if (gridListEditor != null)
            {
                ASPxGridView gridView = gridListEditor.Grid;

                gridView.ExpandAll();
                gridView.DataBound += GridView_DataBound;

                gridView.Settings.GridLines = System.Web.UI.WebControls.GridLines.Both;
                gridView.Settings.ShowFooter = true;
                gridView.Settings.ShowStatusBar = GridViewStatusBarMode.Auto;
                gridView.SettingsBehavior.AutoExpandAllGroups = true;

            }

        }

        private void GridView_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            grid.DataBound -= GridView_DataBound;
            grid.ExpandAll();
        }
    }
}

