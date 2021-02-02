using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using QAMS.Module.BusinessObjects;
using QAMS.Module.BusinessObjects.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects.TestManagement
{

    [DefaultClassOptions]
    [DefaultProperty("AutomationTypeName")]
    [Persistent("AutomationType")]
    [XafDisplayName("AutomationType")]
    [ImageName("ShowAllValue")]
    [CreatableItem(false)]
    public class AutomationType : BaseObject
    {
        public AutomationType(Session session) : base(session)
        {
        }

        [XafDisplayName("ID")]
        public int AutomationTypeId
        {
            get { return GetPropertyValue<int>("AutomationTypeId"); }
            set { SetPropertyValue<int>("AutomationTypeId", value); }
        }

        [XafDisplayName("AutomationName")]
        public string AutomationTypeName
        {
            get { return GetPropertyValue<string>("AutomationTypeName"); }
            set { SetPropertyValue<string>("AutomationTypeName", value); }
        }

        private DevExpress.Xpo.XPCollection<TestCase> _TestCases;
        [XafDisplayName("Related Test Cases")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        [Association("AutomationType-TestCase")]
        public DevExpress.Xpo.XPCollection<TestCase> TestCases
        {
            get
            {
                if (_TestCases == null)
                {
                    _TestCases = GetCollection<TestCase>("TestCases");
                }
                return _TestCases;
            }
        }
    }
}
