using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using QAMS.Module.BusinessObjects.TestManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects.NPObjects
{

    [DefaultClassOptions]
    [DefaultProperty("TitleDescription")]
    [NonPersistent]
    [ImageName("AddParagraphToTableOfContents")]
    [XafDisplayName("Select Related Test cases")]
    [CreatableItem(false)]
    public class TestCaseAssignRunResultNP : BaseObject
    {
        public TestCaseAssignRunResultNP(Session session) : base(session)
        {
        }

        [XafDisplayName("Description")]
        [VisibleInDetailView(false)]
        public string TitleDescription
        {
            get
            {
                return "Select the list of test cases you want to add to these test run.";
            }
        }

        [XafDisplayName("Project")]
        [ModelDefault("AllowNew", "false")]
        [ImmediatePostData]
        public Project Project
        {
            get { return GetPropertyValue<Project>("Project"); }
            set { SetPropertyValue<Project>("Project", value); }
        }

        private DevExpress.Xpo.XPCollection<TestCase> _TestCases;
        [XafDisplayName("Test Cases")]
        [DataSourceProperty("TestCasesDataSource")]
        [ModelDefault("AllowNew", "false")]
        //[ModelDefault("AllowEdit", "false")]
        //[DevExpress.Xpo.Aggregated]
        public DevExpress.Xpo.XPCollection<TestCase> TestCases
        {
            get
            {
                if (_TestCases == null)
                {
                    _TestCases = new XPCollection<TestCase>(Session, false);
                }
                return _TestCases;
            }
        }

        private DevExpress.Xpo.XPCollection<TestCase> _TestCasesDataSource;
        [Browsable(false)]
        public DevExpress.Xpo.XPCollection<TestCase> TestCasesDataSource
        {
            get
            {
                if (_TestCasesDataSource == null && this.Project != null)
                {
                    CriteriaOperator cri = CriteriaOperator.Parse("Project=?",this.Project.Oid);
                    _TestCasesDataSource = new XPCollection<TestCase>(this.Session,cri);
                }
                return _TestCasesDataSource;
            }
        }

    }
}
