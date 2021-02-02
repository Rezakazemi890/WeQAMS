using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.Win.Editors
{
    public class TaskProgressBarControl : ProgressBarControl
    {
        static TaskProgressBarControl()
        {
            RepositoryItemTaskProgressBarControl.Register();
        }
        public override string EditorTypeName { get { return RepositoryItemTaskProgressBarControl.EditorName; } }
        protected override object ConvertCheckValue(object val)
        {
            return val;
        }
    }
}
