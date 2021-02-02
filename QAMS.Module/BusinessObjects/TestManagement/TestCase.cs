using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using QAMS.Module.BusinessObjects;
using DevExpress.ExpressApp.Editors;

namespace QAMS.Module.BusinessObjects.TestManagement
{
    public enum TestCaseNodeType
    {
        [ImageName("Squarified")]
        TestSuit = 0,

        [ImageName("TrackingChanges_Accept")]
        TestCase = 1,

        [ImageName("BO_Product")]
        Section = 2
    }

    public enum Priority
    {
        [ImageName("State_Priority_Normal")]
        Normal = 0,

        [ImageName("State_Priority_Low")]
        Low = 1,

        [ImageName("State_Priority_High")]
        High = 2,

        [ImageName("FollowUp")]
        Critical = 3,
    }

    #region Attribute

    [DefaultClassOptions]
    [DefaultProperty("Name")]
    [Persistent("TestCase")]
    [XafDisplayName("Test Case")]
    [CreatableItem(false)]
    [ImageName("LeftRight")]
    [Appearance(
        "HideIFTestSuitOrSection", 
        Criteria = "TestCaseNodeType='TestSuit' Or TestCaseNodeType='Section'", 
        AppearanceItemType = "ViewItem", 
        TargetItems = "Priority,Reference,EstimateTime,AutomationType,TestRunResults,CaseSteps", 
        //BackColor = "Red", 
        Visibility = ViewItemVisibility.Hide)]
    [Appearance(
        "ColorOfTestSuit",
        Criteria = "TestCaseNodeType='TestSuit'",
        TargetItems = "*",
        BackColor = "LightGreen")]
    [Appearance(
        "ColorOfSection",
        Criteria = "TestCaseNodeType='Section'",
        TargetItems = "*",
        BackColor = "Yellow")]

    #endregion
    public class TestCase : BaseObject, ITreeNode
    {
        public TestCase(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CreatorUser = this.Session.GetObjectByKey<MyUser>((SecuritySystem.CurrentUser as MyUser).Oid);
            this.TestCaseCreateTime = DateTime.Now;
        }

        #region ItreeNodeProperty
        [XafDisplayName("Name")]
        [ToolTip("Ex: Test Run 2014-08-01, Build 240 or Version 3.0")]
        [Size(FieldSizeAttribute.Unlimited)]
        public new String Name { get; set; }

        [XafDisplayName("Owner")]
        [DataSourceProperty("OwnerDataSourceFoTestCase")]
        public TestCase Owner { get; set; }

        private List<TestCase> _OwnerDataSourceForTestCase;
        [Browsable(false)]
        public List<TestCase> OwnerDataSourceFoTestCase
        {
            get
            {
                if (_OwnerDataSourceForTestCase == null)
                {
                    _OwnerDataSourceForTestCase = XPObjectSpace.FindObjectSpaceByObject(this.Session).GetObjects<TestCase>().ToList();
                }
                return _OwnerDataSourceForTestCase;
            }
        }


        [XafDisplayName("Nodes")]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        public IList<TestCase> Nodes
        {
            get
            {
                return XPObjectSpace.FindObjectSpaceByObject(this.Session).GetObjects<TestCase>(CriteriaOperator.Parse("Owner=?", this));
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public ITreeNode Parent => this.Owner;

        private IBindingList children;
        [VisibleInListView(false), VisibleInDetailView(false)]
        [DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
        public IBindingList Children
        {
            get
            {
                if (children == null)
                {
                    children = new BindingList<TestCase>(this.Nodes);
                }
                return children;
            }
        }
        #endregion

        [XafDisplayName("ID")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int TestCaseId
        {
            get { return GetPropertyValue<int>("TestCaseId"); }
            set { SetPropertyValue<int>("TestCaseId", value); }
        }

        [XafDisplayName("Create Time")]
        public DateTime TestCaseCreateTime
        {
            get { return GetPropertyValue<DateTime>("TestCaseCreateTime"); }
            set { SetPropertyValue<DateTime>("TestCaseCreateTime", value); }
        }

        [XafDisplayName("Creator User")]
        [Association("TestCase-CreatorUser")]
        public MyUser CreatorUser
        {
            get { return GetPropertyValue<MyUser>("CreatorUser"); }
            set { SetPropertyValue<MyUser>("CreatorUser", value); }
        }

        [XafDisplayName("Node Type")]
        [ImmediatePostData]
        public TestCaseNodeType TestCaseNodeType
        {
            get { return GetPropertyValue<TestCaseNodeType>("TestCaseNodeType"); }
            set { SetPropertyValue<TestCaseNodeType>("TestCaseNodeType", value); }
        }

        [XafDisplayName("Priority")]
        public Priority Priority
        {
            get { return GetPropertyValue<Priority>("Priority"); }
            set { SetPropertyValue<Priority>("Priority", value); }
        }

        [XafDisplayName("Reference")]
        public string Reference
        {
            get { return GetPropertyValue<string>("Reference"); }
            set { SetPropertyValue<string>("Reference", value); }
        }

        [XafDisplayName("Estimate Time")]
        public TimeSpan EstimateTime
        {
            get { return GetPropertyValue<TimeSpan>("EstimateTime"); }
            set { SetPropertyValue<TimeSpan>("EstimateTime", value); }
        }

        [XafDisplayName("Type")]
        [Association("TestCaseType-TestCase")]
        public TestCaseType TestCaseType
        {
            get { return GetPropertyValue<TestCaseType>("TestCaseType"); }
            set { SetPropertyValue<TestCaseType>("TestCaseType", value); }
        }

        [XafDisplayName("Automation Type")]
        [Association("AutomationType-TestCase")]
        public AutomationType AutomationType
        {
            get { return GetPropertyValue<AutomationType>("AutomationType"); }
            set { SetPropertyValue<AutomationType>("AutomationType", value); }
        }

        [XafDisplayName("Project")]
        [Association("TestCase-Project")]
        public Project Project
        {
            get { return GetPropertyValue<Project>("Project"); }
            set { SetPropertyValue<Project>("Project", value); }
        }

        private DevExpress.Xpo.XPCollection<TestRunResult> _TestRunResults;
        [XafDisplayName("Related Test Runs")]
        [Association("TestRunResult-TestCase")]
        //[DevExpress.Xpo.Aggregated]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("AllowNew", "false")]
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

        private DevExpress.Xpo.XPCollection<TestCaseAttach> _TestCaseAttachs;
        [XafDisplayName("Attachment")]
        [DevExpress.Xpo.Aggregated]
        [Association("TestCaseAttach-TestCase")]
        public DevExpress.Xpo.XPCollection<TestCaseAttach> TestCaseAttachs
        {
            get
            {
                if (_TestCaseAttachs == null)
                {
                    _TestCaseAttachs = GetCollection<TestCaseAttach>("TestCaseAttachs");
                }
                return _TestCaseAttachs;
            }
        }

        private DevExpress.Xpo.XPCollection<CaseStep> _CaseSteps;
        [XafDisplayName("Case Steps")]
        [DevExpress.Xpo.Aggregated]
        [Association("CaseStep-TestCase")]
        public DevExpress.Xpo.XPCollection<CaseStep> CaseSteps
        {
            get
            {
                if (_CaseSteps == null)
                {
                    _CaseSteps = GetCollection<CaseStep>("CaseSteps");
                }
                return _CaseSteps;
            }
        }
    }
}