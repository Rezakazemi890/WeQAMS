using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace QAMS.Module.Web.Editors
{
    [PropertyEditor(typeof(double), "WebProgressBarEditor", false)]
    public class WebProgressBarEditor : WebPropertyEditor
    {
        public WebProgressBarEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        protected override WebControl CreateEditModeControlCore()
        {
            return BuildProgress();
        }

        protected override WebControl CreateViewModeControlCore()
        {
            return BuildProgress();
        }

        protected override object GetControlValueCore()
        {
            return null;
        }

        protected override void ReadEditModeValueCore()
        {
            UpdatePosition();
        }

        protected override void ReadViewModeValueCore()
        {
            UpdatePosition();
        }

        ASPxProgressBar _baseBar;
        public ASPxProgressBar BuildProgress()
        {
            _baseBar = new ASPxProgressBar();

            RenderHelper.SetupASPxWebControl(_baseBar);

            _baseBar.ShowPosition = true;
            _baseBar.DisplayMode = ProgressBarDisplayMode.Percentage;
            _baseBar.Minimum = 0;
            _baseBar.Maximum = 100;
            _baseBar.Width = Unit.Pixel(200);
            _baseBar.Height = Unit.Percentage(100);
            // _baseBar.Paddings.Padding = Unit.Percentage(0);
            return _baseBar;
        }

        public double GetValue()
        {
            if (this.PropertyValue != null
                && this.PropertyValue is double
                && Convert.ToDouble(this.PropertyValue) > 0)
                return Convert.ToDouble(this.PropertyValue);
            return 0;
        }

        private void UpdatePosition()
        {
            if (_baseBar != null)
            {
                _baseBar.Position = Convert.ToDecimal(GetValue());
            }
        }
    }
}
