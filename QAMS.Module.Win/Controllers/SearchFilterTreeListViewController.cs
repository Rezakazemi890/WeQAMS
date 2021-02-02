using DevExpress.ExpressApp;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.Persistent.Base.General;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraTreeList.Nodes;

namespace QAMS.Module.Win.Controllers
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

        private TreeListEditor treeListEditor;
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            treeListEditor = View.Editor as TreeListEditor;
            if (treeListEditor != null)
            {
                TreeList treeList = treeListEditor.TreeList;
                treeList.OptionsFind.AllowFindPanel = true;
                treeList.OptionsBehavior.EnableFiltering = true;
                treeList.OptionsFilter.FilterMode = FilterMode.Extended;
                treeList.OptionsView.ShowVertLines = true;
                treeList.OptionsView.ShowHorzLines = true;
                treeList.OptionsView.EnableAppearanceEvenRow = true;
                //treeList.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
                treeList.OptionsView.AutoCalcPreviewLineCount = true;
                treeList.OptionsView.ShowSummaryFooter = true;
                treeList.OptionsView.ShowIndicator = true;
                treeList.OptionsBehavior.ExpandNodesOnFiltering = true;
                treeList.OptionsBehavior.ExpandNodesOnIncrementalSearch = true;
                treeList.OptionsBehavior.AutoScrollOnSorting = true;
                treeList.OptionsView.AnimationType = TreeListAnimationType.AnimateAllContent;
                treeList.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
                treeList.OptionsView.TreeLineStyle = LineStyle.Solid;
                treeList.OptionsNavigation.AutoMoveRowFocus = true;
                treeList.OptionsView.ShowAutoFilterRow = true;
                treeList.OptionsView.BestFitMode = TreeListBestFitMode.Full;
                treeList.OptionsView.BestFitMode = TreeListBestFitMode.Full;


                treeList.DataSourceChanged += delegate (object sender, EventArgs args)
                {
                    treeList.ExpandAll();
                };

                treeList.AfterFocusNode += delegate (object sender, NodeEventArgs args)
                {
                    if (treeList.FocusedNode == null)
                        treeList.FocusedNode = FocusedNode;
                    FocusedNode = treeList.FocusedNode;
                };

                treeList.NodesReloaded += delegate (object sender, EventArgs args)
                {
                    if (FocusedNode != null && !treeList.IsLookUpMode && treeList.AllNodesCount > 0 &&
                        (Frame.Context == TemplateContext.LookupControl || Frame.Context == TemplateContext.LookupWindow))
                    {
                        try
                        {
                            treeList.ExpandAll();
                            treeList.FocusedNode = treeList.FindNode(
                                x => x.Tag != null && this.ObjectSpace.GetKeyValue(x.Tag) ==
                                     this.ObjectSpace.GetKeyValue(FocusedNode.Tag));
                        }
                        catch
                        {
                            // ignored






                        }
                    }
                };
            }
        }
    }
}
