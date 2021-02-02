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
    [DefaultProperty("TestRunName")]
    [Persistent("TestRun")]
    [XafDisplayName("Test Run")]
    [ImageName("ActionContainerMenuBarItem")]
    [CreatableItem(false)]
    public class TestRun : BaseObject
    {
        public TestRun(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.RunCreateTime = DateTime.Now;
            this.RunCreator = this.Session.GetObjectByKey<MyUser>( (SecuritySystem.CurrentUser as MyUser).Oid);
        }

        [XafDisplayName("ID")]
        public int TestRunId
        {
            get { return GetPropertyValue<int>("TestRunId"); }
            set { SetPropertyValue<int>("TestRunId", value); }
        }

        [XafDisplayName("Title")]
        public string TestRunName
        {
            get { return GetPropertyValue<string>("TestRunName"); }
            set { SetPropertyValue<string>("TestRunName", value); }
        }

        private StepStatus _RunStatus;
        [XafDisplayName("Status")]
        [ImmediatePostData]
        public StepStatus RunStatus
        {
            get
            {
                if (this.TestRunResults != null && this.TestRunResults.Count > 0)
                {
                    if (this.TestRunResults.All(x => x.RunStatus == StepStatus.UnTested))
                        _RunStatus = StepStatus.UnTested;
                    else if (this.TestRunResults.All(x => x.RunStatus == StepStatus.Faild))
                        _RunStatus = StepStatus.Faild;
                    else if (this.TestRunResults.All(x => x.RunStatus == StepStatus.ReTest))
                        _RunStatus = StepStatus.ReTest;
                    else if (this.TestRunResults.All(x => x.RunStatus == StepStatus.Pass))
                        _RunStatus = StepStatus.Pass;
                    else if (this.TestRunResults.All(x => x.RunStatus == StepStatus.Block))
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
                if (this.TestRunResults != null && this.TestRunResults.Count > 0)
                {
                    return Convert.ToDouble(this.TestRunResults.Where(x => x.RunStatus == StepStatus.Pass).Count()) / Convert.ToDouble(this.TestRunResults.Count) * 100;
                }
                return 0;
            }

        }

        [XafDisplayName("Create Time")]
        public DateTime RunCreateTime
        {
            get { return GetPropertyValue<DateTime>("RunCreateTime"); }
            set { SetPropertyValue<DateTime>("RunCreateTime", value); }
        }

        [XafDisplayName("Execute Time")]
        public TimeSpan RunExecuteTime
        {
            get { return GetPropertyValue<TimeSpan>("RunExecuteTime"); }
            set { SetPropertyValue<TimeSpan>("RunExecuteTime", value); }
        }

        [XafDisplayName("Creator")]
        [Association("CreatedRuns-MyUser")]
        public MyUser RunCreator
        {
            get { return GetPropertyValue<MyUser>("RunCreator"); }
            set { SetPropertyValue<MyUser>("RunCreator", value); }
        }

        [XafDisplayName("Automatic Add Test Run Result")]
        public bool AutomaticAddTestRunResult
        {
            get { return GetPropertyValue<bool>("AutomaticAddTestRunResult"); }
            set { SetPropertyValue<bool>("AutomaticAddTestRunResult", value); }
        }

        private string _RunEstimateTime = string.Empty;
        public string RunEstimateTime
        {
            get
            {
                if (_RunEstimateTime == null && _RunEstimateTime == string.Empty)
                {
                    if (
                        this.TestRunResults != null
                        && this.TestRunResults.Count > 0
                        && this.TestRunResults.Where(x => x.RunResults != null && x.RunResults.Count > 0) != null
                        && this.TestRunResults.Where(x => x.RunResults != null && x.RunResults.Count > 0).Count() > 0
                        )
                    {
                        int day = 0;
                        double minute = 0;
                        foreach (XPCollection<RunResult> trun in this.TestRunResults.Select(x => x.RunResults).ToList())
                        {
                            day += trun.Sum(x => x.ElapsedDay);
                            minute += trun.Sum(x => x.ElapsedTime.TotalMinutes);
                        }
                        int dayOfTime = Convert.ToInt32(minute / 1440);
                        string HHMM = TimeSpan.FromMinutes(minute % 1440).ToString(@"hh\:mm");
                        _RunEstimateTime = string.Format("{0} Day(s) and {1} ", dayOfTime, HHMM);
                    }
                }
                return _RunEstimateTime;
            }
        }

        [XafDisplayName("Milestone")]
        [Association("TestRun-Milestone")]
        public Milestone Milestone
        {
            get { return GetPropertyValue<Milestone>("Milestone"); }
            set { SetPropertyValue<Milestone>("Milestone", value); }
        }

        [Association("TestPlan-TestRun")]
        [XafDisplayName("Test Plan")]
        public TestPlan TestPlan
        {
            get { return GetPropertyValue<TestPlan>("TestPlan"); }
            set { SetPropertyValue<TestPlan>("TestPlan", value); }
        }

        private DevExpress.Xpo.XPCollection<TestRunResult> _TestRunResults;
        [XafDisplayName("Test Run Results")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowNew", "false")]
        [Association("TestRun-TestRunResults")]
        public DevExpress.Xpo.XPCollection<TestRunResult> TestRunResults
        {
            get
            {
                if (_TestRunResults == null)
                {
                    _TestRunResults = GetCollection<TestRunResult>("TestRunResults");
                }
                return _TestRunResults;
            }
        }
    }
}
