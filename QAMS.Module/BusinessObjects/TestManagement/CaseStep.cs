using DevExpress.ExpressApp.DC;
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
    public enum StepStatus
    {
        [ImageName("State_ItemVisibility_Hide")]
        UnTested = 0,

        [ImageName("State_Validation_Invalid")]
        Faild = 1,

        [ImageName("State_Validation_Valid")]
        Pass = 2,

        [ImageName("RecurringAppointment")]
        ReTest =3,

        [ImageName("Action_Debug_Stop")]
        Block = 4
    }

    [DefaultClassOptions]
    [DefaultProperty("ProjectName")]
    [Persistent("CaseStep")]
    [XafDisplayName("CaseStep")]
    [ImageName("ShowWeightedLegend")]
    [CreatableItem(false)]
    public class CaseStep : MyAttachment
    {
        public CaseStep(Session session) : base(session)
        {
        }

        [XafDisplayName("ID")]
        public int StepId
        {
            get { return GetPropertyValue<int>("StepId"); }
            set { SetPropertyValue<int>("StepId", value); }
        }

        [XafDisplayName("PreCondition")]
        public string PreCondition
        {
            get { return GetPropertyValue<string>("PreCondition"); }
            set { SetPropertyValue<string>("PreCondition", value); }
        }

        [XafDisplayName("Description")]
        [Size(SizeAttribute.Unlimited)]
        public string StepDescription
        {
            get { return GetPropertyValue<string>("StepDescription"); }
            set { SetPropertyValue<string>("StepDescription", value); }
        }

        [XafDisplayName("Excepted Result")]
        [Size(SizeAttribute.Unlimited)]
        public string StepExceptedResult
        {
            get { return GetPropertyValue<string>("StepExceptedResult"); }
            set { SetPropertyValue<string>("StepExceptedResult", value); }
        }

        [XafDisplayName("TestCase")]
        [Association("CaseStep-TestCase")]
        public TestCase TestCase
        {
            get { return GetPropertyValue<TestCase>("TestCase"); }
            set { SetPropertyValue<TestCase>("TestCase", value); }
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
