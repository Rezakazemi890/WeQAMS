using DevExpress.ExpressApp;
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
    [DefaultProperty("TestPlanName")]
    [Persistent("TestPlan")]
    [XafDisplayName("Test Plan")]
    [ImageName("InsertTableOfCaptions")]
    [CreatableItem(false)]
    public class TestPlan : BaseObject
    {
        public TestPlan(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.TestPlanCreator = this.Session.GetObjectByKey<MyUser>((SecuritySystem.CurrentUser as MyUser).Oid);
            this.TestPlanCreateTime = DateTime.Now;
        }


        [XafDisplayName("Name")]
        public string TestPlanName
        {
            get { return GetPropertyValue<string>("TestPlanName"); }
            set { SetPropertyValue<string>("TestPlanName", value); }
        }

        private StepStatus _RunStatus;
        [XafDisplayName("Status")]
        [ImmediatePostData]
        public StepStatus RunStatus
        {
            get
            {
                if (this.TestRuns != null && this.TestRuns.Count > 0)
                {
                    if (this.TestRuns.All(x => x.RunStatus == StepStatus.UnTested))
                        _RunStatus = StepStatus.UnTested;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.Faild))
                        _RunStatus = StepStatus.Faild;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.ReTest))
                        _RunStatus = StepStatus.ReTest;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.Pass))
                        _RunStatus = StepStatus.Pass;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.Block))
                        _RunStatus = StepStatus.Block;
                    else
                        _RunStatus = StepStatus.Faild;
                }
                return _RunStatus;
            }
        }

        [XafDisplayName("Percent")]
        [EditorAlias("WebProgressBarEditor")]
        [ImmediatePostData]
        public double TestRunPercent
        {
            get
            {
                if (this.TestRuns != null && this.TestRuns.Count > 0)
                {
                    return Convert.ToDouble(this.TestRuns.Where(x => x.RunStatus == StepStatus.Pass).Count()) / Convert.ToDouble(this.TestRuns.Count) * 100;
                }
                return 0;
            }

        }

        [XafDisplayName("Create Time")]
        public DateTime TestPlanCreateTime
        {
            get { return GetPropertyValue<DateTime>("TestPlanCreateTime"); }
            set { SetPropertyValue<DateTime>("TestPlanCreateTime", value); }
        }

        [XafDisplayName("Creator")]
        [Association("CreatedTestPlans-MyUser")]
        public MyUser TestPlanCreator
        {
            get { return GetPropertyValue<MyUser>("TestPlanCreator"); }
            set { SetPropertyValue<MyUser>("TestPlanCreator", value); }
        }


        [XafDisplayName("Milestone")]
        [Association("TestPlan-Milestone")]
        public Milestone Milestone
        {
            get { return GetPropertyValue<Milestone>("Milestone"); }
            set { SetPropertyValue<Milestone>("Milestone", value); }
        }

        private DevExpress.Xpo.XPCollection<TestRun> _TestRuns;
        [XafDisplayName("Test Runs")]
        [DevExpress.Xpo.Aggregated]
        [Association("TestPlan-TestRun")]
        public DevExpress.Xpo.XPCollection<TestRun> TestRuns
        {
            get
            {
                if (_TestRuns == null)
                {
                    _TestRuns = GetCollection<TestRun>("TestRuns");
                }
                return _TestRuns;
            }
        }
    }
}
