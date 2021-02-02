using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.Editors.ChartEditor
{
    [NonPersistent]
    [XafDisplayName("CalculatedValueForChartItem")]
    public class CalculatedValueForChartItem : XPCustomObject
    {
        public CalculatedValueForChartItem(Session session) : base(session) { }
        public CalculatedValueForChartItem() : base() { }

        public override void AfterConstruction()
        {
            Oid = Guid.NewGuid();
            base.AfterConstruction();
        }

        [Browsable(false)]
        public Guid Oid { get; set; }

        [XafDisplayName("ChartValue")]
        public double ChartValue
        {
            get { return GetPropertyValue<double>("ChartValue"); }

            set { SetPropertyValue<double>("ChartValue", value); }
        }

        [XafDisplayName("ChartArgument")]
        public string ChartArgument
        {
            get { return GetPropertyValue<string>("ChartArgument"); }

            set { SetPropertyValue<string>("ChartArgument", value); }
        }

        [XafDisplayName("ChartSeries")]
        public string ChartSeries
        {
            get { return GetPropertyValue<string>("ChartSeries"); }

            set { SetPropertyValue<string>("ChartSeries", value); }
        }
    }
}
