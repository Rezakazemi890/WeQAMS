using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace QAMS.Module.Editors.ChartEditor
{
    [NonPersistent]
    [XafDefaultProperty("Value")]
    [XafDisplayName("CalculatedChartBaseResultDetails")]
    [ImageName("CalculatedChartBaseResultDetails")]
    public class CalculatedChartBaseResultDetails : BaseObject
    {
        public CalculatedChartBaseResultDetails()
        {

        }

        public CalculatedChartBaseResultDetails(Session session) : base(session)
        {

        }

        [XafDisplayName("Value")]
        [ModelDefault("AllowEdit", "false")]
        public string Value { get; set; }


        [XafDisplayName("Argument")]
        [ModelDefault("AllowEdit", "false")]
        public string Argument { get; set; }

        [XafDisplayName("Series")]
        [ModelDefault("AllowEdit", "false")]
        public string Series { get; set; }
    }
}
