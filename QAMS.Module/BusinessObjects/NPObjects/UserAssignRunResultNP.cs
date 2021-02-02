using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
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
    [ImageName("NewEmployee")]
    [XafDisplayName("Select Related Users")]
    [CreatableItem(false)]
    public class UserAssignRunResultNP : BaseObject
    {
        public UserAssignRunResultNP(Session session) : base(session)
        {
        }

        [XafDisplayName("Description")]
        [VisibleInDetailView(false)]
        public string TitleDescription
        {
            get
            {
                return "Select the list of users you want to assign to these test run.";
            }
        }

        [XafDisplayName("Run Assigned")]
        [ModelDefault("AllowNew", "false")]
        [DataSourceProperty("RunAssignedDataSource")]
        [ImmediatePostData]
        public MyUser RunAssigned
        {
            get { return GetPropertyValue<MyUser>("RunAssigned"); }
            set { SetPropertyValue<MyUser>("RunAssigned", value); }
        }

        private DevExpress.Xpo.XPCollection<MyUser> _RunAssignedDataSource;
        [Browsable(false)]
        public DevExpress.Xpo.XPCollection<MyUser> RunAssignedDataSource
        {
            get
            {
                if (_RunAssignedDataSource == null 
                    && (SecuritySystem.CurrentUser as MyUser) != null
                    && (SecuritySystem.CurrentUser as MyUser).Project != null)
                {
                    CriteriaOperator cri = CriteriaOperator.Parse("Project=?", (SecuritySystem.CurrentUser as MyUser).Project.Oid);
                    _RunAssignedDataSource = new XPCollection<MyUser>(Session,cri);
                }
                return _RunAssignedDataSource;
            }
        }

    }
}
