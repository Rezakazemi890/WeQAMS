using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects.Tools
{
    public enum MessageTypes
    {
        Attention,
        Error,
        Warning,
        Success,
        None
    }
    public static class MyMessageBox
    {

        public static void ShowMessageBox(this XafApplication xafApplication, string message, MessageTypes messageType, int delay)
        {
            if (xafApplication is WinApplication)
            {
                XtraMessageBox.Show(message);
            }

            if (xafApplication is WebApplication)
            {
                string msg;
                if (messageType == MessageTypes.Attention ||
                    messageType == MessageTypes.Error)
                {
                    msg = "alertify.set('notifier','position', 'bottom-right'); " + Environment.NewLine +
                          string.Format("var notification = alertify.notify('{0}', 'error', {1}, function(){{ }}); ", message, delay);

                }

                else if (messageType == MessageTypes.Warning ||
                            messageType == MessageTypes.None)
                {
                    msg = "alertify.set('notifier','position', 'bottom-right'); " + Environment.NewLine +
                          string.Format("var notification = alertify.notify('{0}', 'warning', {1}, function(){{ }}); ", message, delay);

                }

                else if (messageType == MessageTypes.Success)
                {
                    msg = "alertify.set('notifier','position', 'bottom-right'); " + Environment.NewLine +
                          string.Format("var notification = alertify.notify('{0}', 'success', {1}, function(){{ }}); ", message, delay);
                }
                else
                {
                    throw new UserFriendlyException("To Developpers: unknow" + messageType.ToString());

                }

                string id = "id" + Guid.NewGuid().ToString().Replace("-", "");
                //string msg = "alert('aaa')";

                WebWindow.CurrentRequestWindow.RegisterClientScript(id, msg);
            }
        }
    }


}

