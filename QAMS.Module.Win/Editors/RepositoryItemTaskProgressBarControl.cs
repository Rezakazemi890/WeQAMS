using DevExpress.Accessibility;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.Win.Editors
{
    public class RepositoryItemTaskProgressBarControl : RepositoryItemProgressBar
    {
        protected internal const string EditorName = "TaskProgressBarControl";
        protected internal static void Register()
        {
            if (!EditorRegistrationInfo.Default.Editors.Contains(EditorName))
            {
                EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(TaskProgressBarControl),
                    typeof(RepositoryItemTaskProgressBarControl), typeof(ProgressBarViewInfo),
                    new ProgressBarPainter(), true, EditImageIndexes.ProgressBarControl, typeof(ProgressBarAccessible)));
            }
        }
        static RepositoryItemTaskProgressBarControl()
        {
            Register();
        }
        public RepositoryItemTaskProgressBarControl()
        {
            Maximum = 100;
            Minimum = 0;
            ShowTitle = true;
            Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            StartColor = System.Drawing.Color.Red;
            EndColor = System.Drawing.Color.Green;

        }
        protected override int ConvertValue(object val)
        {
            try
            {
                float number = Convert.ToSingle(val);
                return (int)((Convert.ToDouble(number + 0.001) * Maximum));
            }
            catch { }
            return Minimum;
        }
        public override string EditorTypeName { get { return EditorName; } }
    }
}
