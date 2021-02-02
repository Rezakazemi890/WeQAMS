using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using QAMS.Module.BusinessObjects.TestManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("UserName")]
    [Persistent("MyUser")]
    [XafDisplayName("MyUser")]
    //[ImageName("ChartDataLabels_LineNone")]
    [CreatableItem(false)]
    public class MyUser : PermissionPolicyUser
    {
        public MyUser(Session session) : base(session)
        {
        }

        public string Email
        {
            get { return GetPropertyValue<string>("Email"); }
            set { SetPropertyValue<string>("Email", value); }
        }

        [XafDisplayName("Project")]
        [Association("ProjectUsers-Project")]
        public Project Project
        {
            get { return GetPropertyValue<Project>("Project"); }
            set { SetPropertyValue<Project>("Project", value); }
        }

        private DevExpress.Xpo.XPCollection<RunResult> _RunResults;
        [XafDisplayName("Related Run Results")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        [Association("RunResult-MyUser")]
        public DevExpress.Xpo.XPCollection<RunResult> RunResults
        {
            get
            {
                if (_RunResults == null)
                {
                    _RunResults = GetCollection<RunResult>("RunResults");
                }
                return _RunResults;
            }
        }

        private DevExpress.Xpo.XPCollection<TestRun> _CreatedRuns;
        [XafDisplayName("Created Runs")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        [Association("CreatedRuns-MyUser")]
        public DevExpress.Xpo.XPCollection<TestRun> CreatedRuns
        {
            get
            {
                if (_CreatedRuns == null)
                {
                    _CreatedRuns = GetCollection<TestRun>("CreatedRuns");
                }
                return _CreatedRuns;
            }
        }

        private DevExpress.Xpo.XPCollection<TestPlan> _TestPlans;
        [XafDisplayName("Created Plans")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        [Association("CreatedTestPlans-MyUser")]
        public DevExpress.Xpo.XPCollection<TestPlan> TestPlans
        {
            get
            {
                if (_TestPlans == null)
                {
                    _TestPlans = GetCollection<TestPlan>("TestPlans");
                }
                return _TestPlans;
            }
        }

        private DevExpress.Xpo.XPCollection<TestCase> _TestCases;
        [XafDisplayName("Related Test Cases")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        [Association("TestCase-CreatorUser")]
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
