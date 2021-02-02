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
    [XafDisplayName("CalculatedValueForChart")]
    public class CalculatedValueForChart : XPCustomObject
    {
        public CalculatedValueForChart(Session session) : base(session) { }
        public CalculatedValueForChart() : base() { }

        public override void AfterConstruction()
        {
            Oid = Guid.NewGuid();
            base.AfterConstruction();
        }

        [Browsable(false)]
        public Guid Oid { get; set; }



        List<CalculatedValueForChartItem> _Item = null;

        [XafDisplayName("CalculatedValueForChartItems")]
        public List<CalculatedValueForChartItem> CalculatedValueForChartItems
        {
            get
            {
                if (_Item == null && CalculatedChartBase != null)
                {
                    _Item = new List<CalculatedValueForChartItem>();
                    CalculatedChartBase.CalculatedChartBaseResultDetails.
                        ForEach(x =>
                        {
                            CalculatedValueForChartItem i = new CalculatedValueForChartItem(Session);
                            i.ChartArgument = x.Argument;
                            i.ChartValue = Convert.ToDouble(x.Value);
                            i.ChartSeries = x.Series;
                            _Item.Add(i);
                        });
                }
                return _Item;
            }
        }

        [XafDisplayName("CalculatedChartBase")]
        public CalculatedChartBase CalculatedChartBase { get; set; }
    }
}
