using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.XtraTreeList;
using DomainComponents.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Data.Helpers;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraTreeList.Nodes;
using DragDropEffects = System.Windows.Forms.DragDropEffects;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base.General;

namespace QAMS.Module.Win.Editors
{
    [ListEditor(typeof(ITreeNode), true)]
    public class DragableTreeEditor : TreeListEditor
    {
        public DragableTreeEditor(IModelListView info) : base(info) { }

        private TreeList coreEditor;

        protected override object CreateControlsCore()
        {
            coreEditor = base.CreateControlsCore() as TreeList;

            if (AllowEdit)
            {
                coreEditor.DragOver += delegate (object sender, System.Windows.Forms.DragEventArgs e)
                {
                    e.Effect = DragDropEffects.Move;
                };

                coreEditor.AfterDropNode += delegate (object sender, AfterDropNodeEventArgs e)
                {
                    object dragedNode = (e.Node as DevExpress.ExpressApp.Win.Controls.ObjectTreeListNode).Object;
                    if (e.Node.ParentNode != null)
                    {
                        object newParentNode =
                            (e.Node.ParentNode as DevExpress.ExpressApp.Win.Controls.ObjectTreeListNode).Object;
                        IMemberInfo member = this.ObjectTypeInfo.FindMember("Owner");
                        member.SetValue(dragedNode, newParentNode);
                        this.Adapter.ObjectSpace.SetModified(dragedNode);
                        this.Adapter.ObjectSpace.CommitChanges();
                        this.Adapter.ObjectSpace.Refresh();
                    }
                    else
                    {
                        IMemberInfo member = this.ObjectTypeInfo.FindMember("Owner");
                        member.SetValue(dragedNode, null);
                        this.Adapter.ObjectSpace.SetModified(dragedNode);
                        this.Adapter.ObjectSpace.CommitChanges();
                        this.Adapter.ObjectSpace.Refresh();
                    }
                    return;
                };

                coreEditor.BeforeDropNode += delegate (object sender, BeforeDropNodeEventArgs e)
                {
                    object dragedNode = (e.SourceNode as DevExpress.ExpressApp.Win.Controls.ObjectTreeListNode).Object;
                    if (e.DestinationNode != null)
                    {
                        object newParentNode =
                            (e.DestinationNode as DevExpress.ExpressApp.Win.Controls.ObjectTreeListNode).Object;
                    }

                };
            }
            SetupColumns();
            this.OnAllowEditChanged();
            coreEditor.ExpandAll();
            coreEditor.OptionsBehavior.ExpandNodesOnFiltering = true;
            coreEditor.OptionsBehavior.ExpandNodesOnIncrementalSearch = true;
            coreEditor.OptionsView.ShowAutoFilterRow = true;
            return coreEditor;

        }

        private void SetupColumns()
        {
            if (this.AllowEdit == false) return;

            Model.Columns
                .Where(x => x.PropertyName != "Owner" && x.AllowEdit).ToList().
                    ForEach(x => x.AllowEdit = false);
        }

        protected override void OnAllowEditChanged()
        {
            base.OnAllowEditChanged();
            if (coreEditor != null)
            {
                coreEditor.OptionsBehavior.AutoChangeParent =
#pragma warning disable CS0618 // 'TreeListOptionsBehavior.DragNodes' is obsolete: 'Use the OptionsDragAndDrop.DragNodesMode property instead'
                    coreEditor.OptionsBehavior.DragNodes = AllowEdit;
#pragma warning restore CS0618 // 'TreeListOptionsBehavior.DragNodes' is obsolete: 'Use the OptionsDragAndDrop.DragNodesMode property instead'

                coreEditor.AllowDrop = this.AllowEdit;
                if (AllowEdit)
                {
                    coreEditor.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Multiple;
                    coreEditor.OptionsDragAndDrop.DropNodesMode = DropNodesMode.Advanced;
                }
            }
        }

        public override void Refresh()
        {
            base.Refresh();
        }
    }
}