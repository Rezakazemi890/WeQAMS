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
    [DefaultProperty("TestCaseTypeName")]
    [Persistent("TestCaseType")]
    [XafDisplayName("TestCase Type")]
    [ImageName("ActionGroup_EasyTestRecorder")]
    [CreatableItem(false)]
    public class TestCaseType : BaseObject
    {
        public TestCaseType(Session session) : base(session)
        {
        }

        [XafDisplayName("ID")]
        public int TestCaseTypeId
        {
            get { return GetPropertyValue<int>("TestCaseTypeId"); }
            set { SetPropertyValue<int>("TestCaseTypeId", value); }
        }

        [XafDisplayName("Name")]
        public string TestCaseTypeName
        {
            get { return GetPropertyValue<string>("TestCaseTypeName"); }
            set { SetPropertyValue<string>("TestCaseTypeName", value); }
        }

        private DevExpress.Xpo.XPCollection<TestCase> _TestCases;
        [XafDisplayName("Related Test Case")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        [Association("TestCaseType-TestCase")]
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
