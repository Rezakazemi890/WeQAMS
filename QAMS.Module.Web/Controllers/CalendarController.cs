using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.GoogleCalendar;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using QAMS.Module.BusinessObjects;
using QAMS.Module.BusinessObjects;
using QAMS.Module.BusinessObjects.Tools;
using static Google.Apis.Calendar.v3.Data.Event;

namespace QAMS.Module.Web.Controllers
{
    public partial class CalendarController : ViewController
    {
        public CalendarController()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        UserCredential credential;
        CalendarService service;
        CalendarList calendarList;
        string activeCalendarId;

        private void saSyncCalendar_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (e.CurrentObject != null && e.CurrentObject is MyScheduler)
            {
                Syncronize(e.CurrentObject as MyScheduler);
                //MyMessageBox.ShowMessageBox(this.Application, "test",MessageTypes.Success,10000);
            }
        }


        public int ConvertGEventStatus(string gStatus)
        {
            if (gStatus == "confirmed")
                return 0;
            else if (gStatus == "tentative")
                return 1;
            else if (gStatus == "cancelled")
                return 3;
            else return 0;
        }

        Task<UserCredential> AuthorizeToGoogle()
        {
            Uri u = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string leftPart = u.GetLeftPart(UriPartial.Path);
            var splitList = leftPart.Split('/').ToList();
            splitList.Remove("");
            splitList.Remove(splitList.Last());

            string cred = HttpContext.Current.Server.MapPath("credentials.json").Replace(splitList.Last(), "").Replace(@"\\", @"\");
            using (FileStream stream = new FileStream(cred, FileMode.Open, FileAccess.Read))
            {
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new String[] { CalendarService.Scope.Calendar },
                        "user",
                        CancellationToken.None,
                        null);
            }
        }

        private EventsResource.ListRequest _request;
        public EventsResource.ListRequest ConnectToGoogle(DateTime FromDate, int dayCount, bool mustReconnect)
        {
            try
            {
                if (_request == null || mustReconnect)
                {
                    this.credential = AuthorizeToGoogle().Result;
                    this.service = new CalendarService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = this.credential,
                        ApplicationName = "QAMS"
                    });

                    // Define parameters of request.
                    EventsResource.ListRequest request = service.Events.List("primary");
                    request.TimeMin = FromDate;
                    request.ShowDeleted = false;
                    request.SingleEvents = true;
                    request.MaxResults = dayCount;
                    request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                    _request = request;
                }
                return _request;

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessageBox(this.Application, ex.Message, MessageTypes.Error, 20);

                throw;
            }
        }

        private void Syncronize(MyScheduler myScheduler)
        {
            try
            {
                int inserted=0;
                int updated=0;
                int deleted=0;
                // List events.
                Events events = ConnectToGoogle(myScheduler.StartOn.AddDays(-7), 30, true).Execute();

                if (events != null && (SecuritySystem.CurrentUser as MyUser).Email.ToLower() != events.Summary.ToLower())
                {
                    MyMessageBox.ShowMessageBox(
                        this.Application,
                        "Set Your Gmail Acount (acount@gmail.com) To Email in Your User.",
                        MessageTypes.Error,
                        20);
                }

                //AddToMySchedulerFromGoogle
                foreach (Event googleEvent in events.Items)
                {
                    CriteriaOperator cri = CriteriaOperator.Parse("GEventId=?", googleEvent.Id);
                    MyScheduler existScheduler = ObjectSpace.FindObject<MyScheduler>(cri);
                    if (existScheduler != null)
                    {
                        if (
                            googleEvent.Start != null
                            && googleEvent.Start.DateTime != null
                            && existScheduler.StartOn != googleEvent.Start.DateTime.Value
                            )
                            existScheduler.StartOn = googleEvent.Start.DateTime.Value;

                        if (
                            googleEvent.End != null
                            && googleEvent.End.DateTime != null
                            && existScheduler.EndOn != googleEvent.End.DateTime.Value
                            )
                            existScheduler.EndOn = googleEvent.End.DateTime.Value;

                        if (
                            existScheduler.UpdateDateTime == null
                            ||
                                (
                                    existScheduler.UpdateDateTime != null
                                    && existScheduler.UpdateDateTime.Value < googleEvent.Updated.Value.AddSeconds(-googleEvent.Updated.Value.Second)
                                )
                            )
                            existScheduler.UpdateDateTime = googleEvent.Updated;

                        if (
                            string.IsNullOrEmpty(existScheduler.OwnerEmail)
                            || existScheduler.OwnerEmail != (SecuritySystem.CurrentUser as MyUser).Email
                            )
                            existScheduler.OwnerEmail = (SecuritySystem.CurrentUser as MyUser).Email;

                        if (
                            string.IsNullOrEmpty(existScheduler.Subject)
                            || (!string.IsNullOrEmpty(googleEvent.Summary)
                            && existScheduler.Subject != googleEvent.Summary)
                            )
                            existScheduler.Subject = googleEvent.Summary;

                        if (
                            string.IsNullOrEmpty(existScheduler.GKind)
                            || (!string.IsNullOrEmpty(googleEvent.Kind)
                            && existScheduler.GKind != googleEvent.Kind)
                            )
                            existScheduler.GKind = googleEvent.Kind;

                        if (
                            existScheduler.GCreatedTime == null
                            || existScheduler.GCreatedTime != googleEvent.Created
                            )
                            existScheduler.GCreatedTime = googleEvent.Created;

                        if (
                            !string.IsNullOrEmpty(googleEvent.CreatedRaw)
                            && (existScheduler.GCreatedRaw == null
                            || existScheduler.GCreatedRaw != DateTime.Parse(googleEvent.CreatedRaw))
                            )
                            existScheduler.GCreatedRaw = DateTime.Parse(googleEvent.CreatedRaw);

                        if (
                            existScheduler.Status != ConvertGEventStatus(googleEvent.Status)
                            )
                            existScheduler.Status = ConvertGEventStatus(googleEvent.Status);

                        if (
                            (!string.IsNullOrEmpty(googleEvent.Location) &&
                            string.IsNullOrEmpty(existScheduler.Location))
                            ||
                            existScheduler.Location != googleEvent.Location
                            )
                            existScheduler.Location = googleEvent.Location;

                        if (
                            googleEvent.Creator != null
                            && (string.IsNullOrEmpty(existScheduler.GCreator)
                            || existScheduler.GCreator != googleEvent.Creator.Email)
                            )
                            existScheduler.GCreator = googleEvent.Creator.Email;

                        if (existScheduler.MustSync == null)
                            existScheduler.MustSync = true;

                        if (existScheduler.Label != 3 && existScheduler.Label != 2)
                            existScheduler.Label = 3;
                        updated++;
                    }
                    else
                    {
                        MyScheduler newScheduler = ObjectSpace.CreateObject<MyScheduler>();
                        if (googleEvent.Start != null && googleEvent.Start.DateTime != null)
                            newScheduler.StartOn = googleEvent.Start.DateTime.Value;
                        else
                            return;

                        if (googleEvent.End != null && googleEvent.End.DateTime != null)
                            newScheduler.EndOn = googleEvent.End.DateTime.Value;
                        else
                            return;

                        newScheduler.UpdateDateTime = googleEvent.Updated;
                        newScheduler.OwnerEmail = (SecuritySystem.CurrentUser as MyUser).Email;
                        newScheduler.GEventId = googleEvent.Id;
                        newScheduler.Subject = googleEvent.Summary;
                        newScheduler.GKind = googleEvent.Kind;
                        newScheduler.GCreatedTime = googleEvent.Created;
                        if (!string.IsNullOrEmpty(googleEvent.CreatedRaw))
                            newScheduler.GCreatedRaw = DateTime.Parse(googleEvent.CreatedRaw);
                        newScheduler.Status = ConvertGEventStatus(googleEvent.Status);
                        newScheduler.Location = googleEvent.Location;
                        if (googleEvent.Creator != null)
                            newScheduler.GCreator = googleEvent.Creator.Email;
                        if (newScheduler.MustSync == null)
                            newScheduler.MustSync = true;
                        newScheduler.Label = 3;
                        inserted++;
                    }
                }

                ObjectSpace.CommitChanges();

                //DeleteFromMySchedulerIfDeletedInGoogle
                CriteriaOperator inDBCri =
                    CriteriaOperator.Parse(
                        "StartOn >=? && EndOn<=?",
                        ConnectToGoogle(myScheduler.StartOn.AddDays(-7), 30, false).TimeMin,
                        ConnectToGoogle(myScheduler.StartOn.AddDays(-7), 30, false).TimeMin.Value.AddDays(30)
                        );

                List<MyScheduler> SchedulersInDB =
                    ObjectSpace.GetObjects<MyScheduler>(inDBCri)
                                                    .OrderBy(x => x.StartOn).ToList();

                List<MyScheduler> mustDelete =
                    SchedulersInDB.Where(
                        x => !string.IsNullOrEmpty(x.GEventId)
                        && events.Items.ToList().All(y => y.Id != x.GEventId)
                            ).ToList();

                if (mustDelete != null && mustDelete.Count > 0)
                {
                    ObjectSpace.Delete(mustDelete);
                    ObjectSpace.CommitChanges();
                    deleted += mustDelete.Count;

                }

                //insert MyScheduler Events into Google Calendar
                List<MyScheduler> mustInsert =
                    SchedulersInDB.Where(
                        x => string.IsNullOrEmpty(x.GEventId)
                        && x.MustSync == true
                        && events.Items.ToList().All(y => y.Id != x.GEventId)
                            ).ToList();

                foreach (var item in mustInsert)
                {
                    Event newEvent = new Event()
                    {
                        Summary = item.Subject,
                        Location = item.Location,
                        Description = item.Description,
                        Start = new EventDateTime()
                        {
                            DateTime = item.StartOn,
                            TimeZone = "Asia/Tehran",
                        },
                        End = new EventDateTime()
                        {
                            DateTime = item.EndOn,
                            TimeZone = "Asia/Tehran",
                        },
                        Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                        Attendees = new EventAttendee[] {
                                new EventAttendee() { Email =item.OwnerEmail}
                        },
                        Reminders = new Event.RemindersData()
                        {
                            UseDefault = false,
                            Overrides = new EventReminder[] {
                                    new EventReminder() { Method = "email", Minutes = 1 * 60 },
                                    new EventReminder() { Method = "sms", Minutes = 10 },
                        }
                        }
                    };

                    String calendarId = "primary";
                    EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
                    Event createdEvent = request.Execute();
                    item.GEventId = createdEvent.Id;
                    item.UpdateDateTime = createdEvent.Updated;
                    item.Label = 2;
                }

                inserted += mustInsert.Count;

                ObjectSpace.CommitChanges();

                //update google from MyScheduler
                List<MyScheduler> mustUpdate =
                        SchedulersInDB.Where(
                            x => !string.IsNullOrEmpty(x.GEventId)
                            && x.MustSync == true
                           && x.UpdateDateTime > events.Items.ToList().Where(y => y.Id == x.GEventId).FirstOrDefault().Updated
                                ).ToList();

                foreach (var item in mustUpdate)
                {
                    UpdateEvent(item);
                }
                updated += mustUpdate.Count;

                //delete google from MyScheduler

                Session session = ((XPObjectSpace)ObjectSpace).Session;
                CriteriaOperator criteria = CriteriaOperator.Parse(
                        "StartOn >=? && EndOn<=?",
                        ConnectToGoogle(myScheduler.StartOn.AddDays(-7), 30, false).TimeMin,
                        ConnectToGoogle(myScheduler.StartOn.AddDays(-7), 30, false).TimeMin.Value.AddDays(30)
                        );

                List<MyScheduler> mustDeleteInGoogle =
                    session.GetObjects(
                            session.GetClassInfo(typeof(MyScheduler))
                            , criteria
                            , null
                            , 30
                            , true
                            , true
                        ).OfType<MyScheduler>().Where(x => x.IsDeleted == true
                                                                                        && !string.IsNullOrEmpty(x.GEventId)
                                                                                        && x.MustSync == true
                                                                                        )
                                                                                        .Where(x => events.Items.ToList().All(y => y.Id == x.GEventId))
                        .ToList();


                foreach (var item in mustDeleteInGoogle)
                {
                    DeleteEvent(item);
                    deleted++;
                }

                ObjectSpace.CommitChanges();

                string msg =
                    string.Format(
                        "Sync completed.  {0} Event inserted. {1} Event Updated.  {2} Event Deleted.",
                        inserted,
                        updated,
                        deleted
                        );
                
                MyMessageBox.ShowMessageBox(
                                this.Application,
                                msg,
                                MessageTypes.Success,
                                10);

                this.View.Refresh();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowMessageBox(
                    this.Application,
                    ex.Message + " Please Check internet Connection.",
                    MessageTypes.Error,
                    20);
            }
        }

        public Event UpdateEvent(MyScheduler myScheduler)
        {
            if (service == null)
                ConnectToGoogle(myScheduler.StartOn.AddDays(-1), 3, true);

            String calendarId = "primary";
            EventsResource.GetRequest requestEvent = service.Events.Get(calendarId, myScheduler.GEventId);
            Event newEvent = requestEvent.Execute();


            newEvent.Summary = myScheduler.Subject;
            newEvent.Location = myScheduler.Location;
            newEvent.Description = myScheduler.Description;
            newEvent.Start = new EventDateTime()
            {
                DateTime = myScheduler.StartOn,
                TimeZone = "Asia/Tehran",
            };
            newEvent.End = new EventDateTime()
            {
                DateTime = myScheduler.EndOn,
                TimeZone = "Asia/Tehran",
            };
            newEvent.Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" };
            newEvent.Attendees = new EventAttendee[] {
                                new EventAttendee() { Email =myScheduler.OwnerEmail}
                        };
            newEvent.Reminders = new Event.RemindersData()
            {
                UseDefault = false,
                Overrides = new EventReminder[] {
                                    new EventReminder() { Method = "email", Minutes = 1 * 60 },
                                    new EventReminder() { Method = "sms", Minutes = 10 },
                        }
            };


            EventsResource.UpdateRequest request = service.Events.Update(newEvent, calendarId, myScheduler.GEventId);
            Event createdEvent = request.Execute();
            myScheduler.GEventId = createdEvent.Id;
            return createdEvent;
        }

        public string DeleteEvent(MyScheduler myScheduler)
        {
            if (service == null)
                ConnectToGoogle(myScheduler.StartOn.AddDays(-1), 3, true);

            String calendarId = "primary";
            EventsResource.DeleteRequest request = service.Events.Delete(calendarId, myScheduler.GEventId);
            string deletedEvent = request.Execute();
            return deletedEvent;
        }
    }
}
