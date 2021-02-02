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
    [DefaultProperty("MilestoneName")]
    [Persistent("Milestone")]
    [XafDisplayName("Milestone")]
    [ImageName("GaugeStyleLinearHorizontal")]
    [CreatableItem(false)]
    public class Milestone : MyAttachment
    {
        public Milestone(Session session) : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.MilestoneStartTime = DateTime.Now;
        }

        [XafDisplayName("ID")]
        public int MilestoneId
        {
            get { return GetPropertyValue<int>("MilestoneId"); }
            set { SetPropertyValue<int>("MilestoneId", value); }
        }

        [XafDisplayName("Name")]
        public string MilestoneName
        {
            get { return GetPropertyValue<string>("MilestoneName"); }
            set { SetPropertyValue<string>("MilestoneName", value); }
        }

        [XafDisplayName("Description")]
        [Size(SizeAttribute.Unlimited)]
        public string MilestoneDescription
        {
            get { return GetPropertyValue<string>("MilestoneDescription"); }
            set { SetPropertyValue<string>("MilestoneDescription", value); }
        }

        [XafDisplayName("Reference")]
        public string MilestoneReference
        {
            get { return GetPropertyValue<string>("MilestoneReference"); }
            set { SetPropertyValue<string>("MilestoneReference", value); }
        }

        [XafDisplayName("Start Time")]
        [ModelDefault("EditMask", "d")]
        [ModelDefault("DisplayFormat", "{0:d}")]
        public DateTime? MilestoneStartTime
        {
            get { return GetPropertyValue<DateTime?>("MilestoneStartTime"); }
            set { SetPropertyValue<DateTime?>("MilestoneStartTime", value); }
        }


        [XafDisplayName("End Time")]
        [ModelDefault("EditMask", "d")]
        [ModelDefault("DisplayFormat", "{0:d}")]
        public DateTime? MilestoneEndTime
        {
            get { return GetPropertyValue<DateTime?>("MilestoneEndTime"); }
            set { SetPropertyValue<DateTime?>("MilestoneEndTime", value); }
        }

        private Project _Project;
        [XafDisplayName("Project")]
        public Project Project
        {
            get
            {
                if (_Project == null && TestRuns != null && TestRuns.Count > 0)
                {
                    _Project = TestRuns.FirstOrDefault().TestRunResults.FirstOrDefault().TestCase.Project;
                }
                return _Project;
            }
        }

        private StepStatus _MilestoneStatus;
        [XafDisplayName("Status")]
        public StepStatus MilestoneStatus
        {
            get
            {
                if (this.TestRuns != null && this.TestRuns.Count > 0)
                {
                    if (this.TestRuns.All(x => x.RunStatus == StepStatus.UnTested))
                        _MilestoneStatus = StepStatus.UnTested;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.Faild))
                        _MilestoneStatus = StepStatus.Faild;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.ReTest))
                        _MilestoneStatus = StepStatus.ReTest;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.Pass))
                        _MilestoneStatus = StepStatus.Pass;
                    else if (this.TestRuns.All(x => x.RunStatus == StepStatus.Block))
                        _MilestoneStatus = StepStatus.Block;
                    else
                        _MilestoneStatus = StepStatus.Faild;
                }
                return _MilestoneStatus;
            }
        }

        [XafDisplayName("Percent")]
        [EditorAlias("WebProgressBarEditor")]
        public double MilestonePercent
        {
            get
            {
                if (this.TestRuns != null && this.TestRuns.Count > 0)
                {
                    return Convert.ToDouble(this.TestRuns.Select(x => Convert.ToDouble(x.TestRunPercent)).Average());
                }
                return 0;
            }
        }

        private DevExpress.Xpo.XPCollection<TestRun> _TestRuns;
        [XafDisplayName("Test Run")]
        //[DevExpress.Xpo.Aggregated]
        [Association("TestRun-Milestone")]
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

        private DevExpress.Xpo.XPCollection<TestPlan> _TestPlans;
        [XafDisplayName("Test Plan")]
        //[DevExpress.Xpo.Aggregated]
        [Association("TestPlan-Milestone")]
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

        private DocumentType documentType;

        [VisibleInListView(false), VisibleInDetailView(false)]
        public DocumentType DocumentType
        {
            get
            {
                return documentType;
            }
            set
            {
                SetPropertyValue<DocumentType>(nameof(DocumentType), ref documentType, value);
            }
        }
    }
}
