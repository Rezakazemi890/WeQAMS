using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.Editors.ChartEditor
{
    public interface CalculatedChartBase
    {

        [VisibleInListView(false), NonPersistentDc]
        [EditorAlias("CalculatedValueChartEditor")]
        [XafDisplayName("CalculatedValueForChart")]
        CalculatedValueForChart CalculatedValueForChart { get; }


        [XafDisplayName("CalculatedChartBaseResultDetails")]
        List<CalculatedChartBaseResultDetails> CalculatedChartBaseResultDetails { get; }
    }
}
