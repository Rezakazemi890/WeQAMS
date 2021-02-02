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
    [Persistent("RunResult")]
    [XafDisplayName("Run Result")]
    [ImageName("GoToNextHeaderFooter")]
    [CreatableItem(false)]
    public class RunResult : BaseObject
    {
        public RunResult(Session session) : base(session)
        {
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

        [XafDisplayName("Date & Time")]
        public DateTime CreateDateTime
        {
            get { return GetPropertyValue<DateTime>("CreateDateTime"); }
            set { SetPropertyValue<DateTime>("CreateDateTime", value); }
        }

        [XafDisplayName("ElapsedDay")]
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

        [XafDisplayName("Test Run Result")]
        [Association("RunResult-TestRunResult")]
        public TestRunResult TestRunResult
        {
            get { return GetPropertyValue<TestRunResult>("TestRunResult"); }
            set { SetPropertyValue<TestRunResult>("TestRunResult", value); }
        }

        [Association("RunResult-MyUser")]
        [XafDisplayName("RunAssigned")]
        public MyUser RunAssigned
        {
            get { return GetPropertyValue<MyUser>("RunAssigned"); }
            set { SetPropertyValue<MyUser>("RunAssigned", value); }
        }

    }
}
