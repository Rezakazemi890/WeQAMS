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

namespace QAMS.Module.Web.Controllers
{
    public class SearchFilterTreeListViewController : ObjectViewController<ListView, ITreeNode>
    {
        private TreeListNode FocusedNode { get; set; }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
        private TreeListNode focusedNode { get; set; }
        private ASPxTreeListEditor treeListEditor;
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            treeListEditor = View.Editor as ASPxTreeListEditor;
            if (treeListEditor != null)
            {
                ASPxTreeList treeList = treeListEditor.TreeList;
                treeList.SettingsBehavior.AutoExpandAllNodes = true;
                treeList.SettingsBehavior.ExpandCollapseAction = TreeListExpandCollapseAction.Button;
                treeList.SettingsBehavior.ExpandNodesOnFiltering = true;                
                treeList.ExpandAll();

                treeList.SettingsBehavior.AllowHeaderFilter = true;
                treeList.Settings.GridLines = System.Web.UI.WebControls.GridLines.Both;
                treeList.Settings.ShowTreeLines = true;
                treeList.SettingsBehavior.SortMode = TreeListColumnSortMode.Default;
                treeList.SettingsEditing.AllowNodeDragDrop = true;
                treeList.Settings.SuppressOuterGridLines = true;
                treeList.PreRender += (sender, e) =>
                {
                    //if ((bool)System.Web.HttpContext.Current.Session["firstLoad"])
                        treeList.ExpandAll();
                };


                treeListEditor.DataSourceChanged += delegate (object sender, EventArgs args)
                {
                    treeList.ExpandAll();
                };

                treeListEditor.FocusedObjectChanged += delegate (object sender, EventArgs args)
                {
                    if (treeList.FocusedNode == null)
                        treeListEditor.FocusedObject= focusedNode;
                    focusedNode = treeList.FocusedNode;
                };

                treeListEditor.TreeList.Load += delegate (object sender, EventArgs e) {
                    ((ASPxTreeList)sender).ExpandAll();
                };

            }

        }

    }
}

