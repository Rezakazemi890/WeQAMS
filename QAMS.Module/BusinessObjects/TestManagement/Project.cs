using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using QAMS.Module.BusinessObjects;
using QAMS.Module.BusinessObjects.Tools;
using QAMS.Module.Editors.ChartEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects.TestManagement
{
    [DefaultClassOptions]
    [DefaultProperty("ProjectName")]
    [Persistent("Project")]
    [XafDisplayName("Project")]
    [ImageName("Products")]
    [CreatableItem(false)]
    public class Project : MyAttachment, CalculatedChartBase
    {
        public Project(Session session) : base(session)
        {
        }

        [XafDisplayName("ID")]
        public int ProjectId
        {
            get { return GetPropertyValue<int>("ProjectId"); }
            set { SetPropertyValue<int>("ProjectId", value); }
        }

        [XafDisplayName("Name")]
        public string ProjectName
        {
            get { return GetPropertyValue<string>("ProjectName"); }
            set { SetPropertyValue<string>("ProjectName", value); }
        }

        [XafDisplayName("Description")]
        [Size(SizeAttribute.Unlimited)]
        public string ProjectDescription
        {
            get { return GetPropertyValue<string>("ProjectDescription"); }
            set { SetPropertyValue<string>("ProjectDescription", value); }
        }

        [XafDisplayName("Reference")]
        public string ProjectReference
        {
            get { return GetPropertyValue<string>("ProjectReference"); }
            set { SetPropertyValue<string>("ProjectReference", value); }
        }

        CalculatedValueForChart _CalculatedValueForChart;
        [EditorAlias("CalculatedValueChartEditor")]
        [XafDisplayName("Test Run Result Status Of Project")]
        [VisibleInListView(false)]
        public CalculatedValueForChart CalculatedValueForChart
        {
            get
            {
                if (_CalculatedValueForChart == null)
                {
                    _CalculatedValueForChart = XPObjectSpace.FindObjectSpaceByObject(this).CreateObject<CalculatedValueForChart>();
                }
                _CalculatedValueForChart.CalculatedChartBase = this;
                return _CalculatedValueForChart;
            }
        }

        private List<CalculatedChartBaseResultDetails> _CalculatedChartBaseResultDetails;
        [Browsable(false)]
        public List<CalculatedChartBaseResultDetails> CalculatedChartBaseResultDetails
        {
            get
            {
                if (_CalculatedChartBaseResultDetails == null)
                {
                    _CalculatedChartBaseResultDetails = new List<CalculatedChartBaseResultDetails>();

                    int PassCount = 0;
                    int FaildCount = 0;
                    int BlockCount = 0;
                    int ReTestCount = 0;
                    int UnTestedCount = 0;
                    string projectName = string.Empty;

                    foreach (TestCase tcase in this.TestCases.ToList())
                    {
                        PassCount += tcase.TestRunResults.ToList().Where(x => x.RunStatus == StepStatus.Pass).Count();
                        FaildCount += tcase.TestRunResults.ToList().Where(x => x.RunStatus == StepStatus.Faild).Count();
                        BlockCount += tcase.TestRunResults.ToList().Where(x => x.RunStatus == StepStatus.Block).Count();
                        ReTestCount += tcase.TestRunResults.ToList().Where(x => x.RunStatus == StepStatus.ReTest).Count();
                        UnTestedCount += tcase.TestRunResults.ToList().Where(x => x.RunStatus == StepStatus.UnTested).Count();
                        projectName = tcase.Project.ProjectName;
                    }

                    //pass
                    CalculatedChartBaseResultDetails PassCd =
                        XPObjectSpace.FindObjectSpaceByObject(this).CreateObject<CalculatedChartBaseResultDetails>();
                    PassCd.Value = PassCount.ToString();
                    PassCd.Argument = StepStatus.Pass.ToString();
                    //PassCd.Series = projectName;
                    PassCd.Series = StepStatus.Pass.ToString();
                    _CalculatedChartBaseResultDetails.Add(PassCd);

                    //Faild
                    CalculatedChartBaseResultDetails FailCd =
                            XPObjectSpace.FindObjectSpaceByObject(this).CreateObject<CalculatedChartBaseResultDetails>();
                    FailCd.Value = FaildCount.ToString();
                    FailCd.Argument = StepStatus.Faild.ToString();
                    //FailCd.Series = projectName;
                    FailCd.Series = StepStatus.Faild.ToString();
                    _CalculatedChartBaseResultDetails.Add(FailCd);

                    //Block
                    CalculatedChartBaseResultDetails BlockCd =
                                XPObjectSpace.FindObjectSpaceByObject(this).CreateObject<CalculatedChartBaseResultDetails>();
                    BlockCd.Value = BlockCount.ToString();
                    BlockCd.Argument = StepStatus.Block.ToString();
                    //BlockCd.Series = projectName;
                    BlockCd.Series = StepStatus.Block.ToString();
                    _CalculatedChartBaseResultDetails.Add(BlockCd);

                    //ReTest
                    CalculatedChartBaseResultDetails ReTestCd =
                            XPObjectSpace.FindObjectSpaceByObject(this).CreateObject<CalculatedChartBaseResultDetails>();
                    ReTestCd.Value = ReTestCount.ToString();
                    ReTestCd.Argument = StepStatus.ReTest.ToString();
                    //ReTestCd.Series = projectName;
                    ReTestCd.Series = StepStatus.ReTest.ToString();
                    _CalculatedChartBaseResultDetails.Add(ReTestCd);

                    //UnTested
                    CalculatedChartBaseResultDetails UnTestCd =
                            XPObjectSpace.FindObjectSpaceByObject(this).CreateObject<CalculatedChartBaseResultDetails>();
                    UnTestCd.Value = UnTestedCount.ToString();
                    UnTestCd.Argument = StepStatus.UnTested.ToString();
                    //UnTestCd.Series = projectName;
                    UnTestCd.Series = StepStatus.UnTested.ToString();
                    _CalculatedChartBaseResultDetails.Add(UnTestCd);

                    this.CalculatedValueForChart.CalculatedChartBase = this;
                }
                return _CalculatedChartBaseResultDetails;
            }
        }

        private DevExpress.Xpo.XPCollection<MyUser> _ProjectUsers;
        [XafDisplayName("Project Users")]
        //[DevExpress.Xpo.Aggregated]
        [Association("ProjectUsers-Project")]
        public DevExpress.Xpo.XPCollection<MyUser> ProjectUsers
        {
            get
            {
                if (_ProjectUsers == null)
                {
                    _ProjectUsers = GetCollection<MyUser>("ProjectUsers");
                }
                return _ProjectUsers;
            }
        }

        private DevExpress.Xpo.XPCollection<TestCase> _TestCases;
        [XafDisplayName("Test Case")]
        //[DevExpress.Xpo.Aggregated]
        [Association("TestCase-Project")]
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
