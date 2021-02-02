using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.XtraScheduler.GoogleCalendar;
using Google.Apis.Calendar.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Subject")]
    [Persistent("MyScheduler")]
    [XafDisplayName("Scheduler")]
    //[ImageName("ChartDataLabels_LineNone")]
    [CreatableItem(false)]
    public class MyScheduler : Event
    {
        public MyScheduler(Session session) : base(session)
        {
            
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.MustSync = true;
            this.OwnerEmail = (this.Session.GetObjectByKey<MyUser>((SecuritySystem.CurrentUser as MyUser).Oid)).Email;
            this.GCreatedTime = DateTime.Now;
            this.GCreatedRaw = DateTime.Now;
            this.Label = 2;
        }
        public string GEventId
        {
            get { return GetPropertyValue<string>("GEventId"); }
            set { SetPropertyValue<string>("GEventId", value); }
        }

        public string GKind
        {
            get { return GetPropertyValue<string>("GKind"); }
            set { SetPropertyValue<string>("GKind", value); }
        }

        public DateTime? GCreatedTime
        {
            get { return GetPropertyValue<DateTime?>("GCreatedTime"); }
            set { SetPropertyValue<DateTime?>("GCreatedTime", value); }
        }
        public DateTime? GCreatedRaw
        {
            get { return GetPropertyValue<DateTime?>("GCreatedRaw"); }
            set { SetPropertyValue<DateTime?>("GCreatedRaw", value); }
        }
        public DateTime? UpdateDateTime
        {
            get { return GetPropertyValue<DateTime?>("UpdateDateTime"); }
            set { SetPropertyValue<DateTime?>("UpdateDateTime", value); }
        }
        public string GCreator
        {
            get { return GetPropertyValue<string>("GCreator"); }
            set { SetPropertyValue<string>("GCreator", value); }
        }
        public string OwnerEmail
        {
            get { return GetPropertyValue<string>("OwnerEmail"); }
            set { SetPropertyValue<string>("OwnerEmail", value); }
        }

        public bool? MustSync
        {
            get { return GetPropertyValue<bool?>("MustSync"); }
            set { SetPropertyValue<bool?>("MustSync", value); }
        }
        //public DXGoogleCalendarSync dXGoogleCalendarSync { get; set; }

        protected override void OnSaving()
        {
            if (XPObjectSpace.FindObjectSpaceByObject(this).IsModified)
                this.UpdateDateTime = DateTime.Now;
            base.OnSaving();
        }

        protected override void OnSaved()
        {
            base.OnSaved();
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
        }
    }
}
