using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
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
    [Persistent("TestRunResult")]
    [XafDisplayName("Test Run Result")]
    [ImageName("GoToNextHeaderFooter")]
    [CreatableItem(false)]
    public class TestRunResult : BaseObject
    {
        public TestRunResult(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [XafDisplayName("Test Case")]
        [Association("TestRunResult-TestCase")]
        public TestCase TestCase
        {
            get { return GetPropertyValue<TestCase>("TestCase"); }
            set { SetPropertyValue<TestCase>("TestCase", value); }
        }

        [XafDisplayName("Version")]
        public string Version
        {
            get { return GetPropertyValue<string>("Version"); }
            set { SetPropertyValue<string>("Version", value); }
        }

        [XafDisplayName("Defects")]
        public string Defects
        {
            get { return GetPropertyValue<string>("Defects"); }
            set { SetPropertyValue<string>("Defects", value); }
        }

        [XafDisplayName("Comment")]
        [Size(SizeAttribute.Unlimited)]
        public string Comment
        {
            get { return GetPropertyValue<string>("Comment"); }
            set { SetPropertyValue<string>("Comment", value); }
        }

        [XafDisplayName("Run Status")]
        [ImmediatePostData]
        public StepStatus RunStatus
        {
            get { return GetPropertyValue<StepStatus>("RunStatus"); }
            set { SetPropertyValue<StepStatus>("RunStatus", value); }
        }

        [XafDisplayName("Elapsed Day")]
        public int ElapsedDay
        {
            get { return GetPropertyValue<int>("ElapsedDay"); }
            set { SetPropertyValue<int>("ElapsedDay", value); }
        }

        [XafDisplayName("Elapsed Time")]
        public TimeSpan ElapsedTime
        {
            get { return GetPropertyValue<TimeSpan>("ElapsedTime"); }
            set { SetPropertyValue<TimeSpan>("ElapsedTime", value); }
        }

        [XafDisplayName("Test Run")]
        [Association("TestRun-TestRunResults")]
        public TestRun TestRun
        {
            get { return GetPropertyValue<TestRun>("TestRun"); }
            set { SetPropertyValue<TestRun>("TestRun", value); }
        }

        [XafDisplayName("RunAssigned")]
        [ModelDefault("AllowEdit", "false")]
        public MyUser RunAssigned
        {
            get { return GetPropertyValue<MyUser>("RunAssigned"); }
            set { SetPropertyValue<MyUser>("RunAssigned", value); }
        }


        private DevExpress.Xpo.XPCollection<RunResult> _RunResults;
        [XafDisplayName("Run Result")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowNew", "false")]
        [ModelDefault("AllowEdit", "false")]
        [Association("RunResult-TestRunResult")]
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

        protected override void OnSaving()
        {
            base.OnSaving();
            if (XPObjectSpace.FindObjectSpaceByObject(this).IsModified)
            {
                RunResult runResult = XPObjectSpace.FindObjectSpaceByObject(this).CreateObject<RunResult>();
                runResult.CreateDateTime = DateTime.Now;
                runResult.Comment = this.Comment;
                runResult.Defects = this.Defects;
                runResult.ElapsedDay = this.ElapsedDay;
                runResult.ElapsedTime = this.ElapsedTime;
                runResult.RunAssigned = this.RunAssigned;
                runResult.RunStatus = this.RunStatus;
                runResult.Version = this.Version;
                runResult.TestRunResult = this;
                this.RunResults.Add(runResult);
            }
        }

        protected override void OnDeleted()
        {         
            base.OnDeleted();
            if(this != null && this.RunResults!= null && this.RunResults.Count> 0)
            {
                this.Session.Delete(this.RunResults);
            }
        }
    }
}
