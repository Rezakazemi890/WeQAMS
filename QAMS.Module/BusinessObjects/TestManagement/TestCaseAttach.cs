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
    [DefaultProperty("ProjectName")]
    [Persistent("TestCaseAttach")]
    [XafDisplayName("TestCase Attachment")]
    [ImageName("AttachFile")]
    [CreatableItem(false)]
    public class TestCaseAttach : MyAttachment
    {
        public TestCaseAttach(Session session) : base(session)
        {
        }

        [XafDisplayName("ID")]
        public int TestCaseAttachId
        {
            get { return GetPropertyValue<int>("TestCaseAttachId"); }
            set { SetPropertyValue<int>("TestCaseAttachId", value); }
        }

        [XafDisplayName("Date And Time")]
        [ModelDefault("EditMask", "d")]
        [ModelDefault("DisplayFormat", "{0:d}")]
        public DateTime? AttachmentDate
        {
            get { return GetPropertyValue<DateTime?>("AttachmentDate"); }
            set { SetPropertyValue<DateTime?>("AttachmentDate", value); }
        }

        [XafDisplayName("Description")]
        [Size(SizeAttribute.Unlimited)]
        public string TestCaseAttachDescription
        {
            get { return GetPropertyValue<string>("TestCaseAttachDescription"); }
            set { SetPropertyValue<string>("TestCaseAttachDescription", value); }
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

        [XafDisplayName("Test Case")]
        [Association("TestCaseAttach-TestCase")]
        public TestCase TestCase
        {
            get { return GetPropertyValue<TestCase>("TestCase"); }
            set { SetPropertyValue<TestCase>("TestCase", value); }
        }
    }
    public enum DocumentType
    {
        [ImageName("Action_Export_ToText")]
        Text = 0,

        [ImageName("Actions_Book")]
        Book = 1,

        [ImageName("AutoSize_Fill")]
        Software = 2,

        [ImageName("SetSelectedImagesStretchMode_UniformToFill")]
        Image = 3,

        [ImageName("ExportToDOC")]
        Document = 4,

        [ImageName("Actions_Question")]
        Unknown = 5
    };
}
