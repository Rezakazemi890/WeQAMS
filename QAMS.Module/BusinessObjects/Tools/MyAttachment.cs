using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects.Tools
{
    [DefaultProperty("File")]
    [FileAttachment("File")]
    [NonPersistent]
    public abstract class MyAttachment : BaseObject
    {
        protected MyAttachment(Session session):base(session)
        {
        }

        [Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[RuleRequiredField("FileAttachmentBaseRule", "Save", "File should be assigned")]
        public FileData File { get; set; }
    }
}
