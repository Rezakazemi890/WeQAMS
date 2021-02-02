using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace QAMS.Module.Win.Editors
{
    [PropertyEditor(typeof(float), "ProgressFieldEditor", false)]
    public class ProgressPropertyEditor : DXPropertyEditor
    {
        public ProgressPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        protected override object CreateControlCore()
        {
            return new TaskProgressBarControl();
        }

        protected override RepositoryItem CreateRepositoryItem()
        {
            return new RepositoryItemTaskProgressBarControl();
        }

        protected override void SetupRepositoryItem(RepositoryItem item)
        {
            RepositoryItemTaskProgressBarControl repositoryItem = (RepositoryItemTaskProgressBarControl)item;
            repositoryItem.Maximum = 100;
            repositoryItem.Minimum = 0; 
            repositoryItem.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            repositoryItem.StartColor = System.Drawing.Color.Red;
            repositoryItem.EndColor = System.Drawing.Color.LightGreen;            
            //repositoryItem.CustomDisplayText += RepositoryItem_CustomDisplayText;
            base.SetupRepositoryItem(item);
        }

        private void RepositoryItem_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            RepositoryItemTaskProgressBarControl bar = sender as RepositoryItemTaskProgressBarControl;
            if (bar != null)
            {                
                //e.DisplayText = ((float)e.Value * bar.Maximum / 100).ToString();
            }
        }
    }
}
