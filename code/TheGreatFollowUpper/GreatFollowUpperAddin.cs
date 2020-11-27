using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TheGreatFollowUpper.Extensions;
using TheGreatFollowUpper.Properties;
using TheGreatFollowUpper.Views;

[assembly: RuntimeCompatibility(WrapNonExceptionThrows = false)] //Used for Exception Wrapping https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.runtimecompatibilityattribute.wrapnonexceptionthrows(v=vs.110).aspx

namespace TheGreatFollowUpper
{
    public partial class GreatFollowUpperAddin
    {
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            //Commented here because License is not yet initialized on the moment it is created (or something like that)
            //if (!License.IsLicenseValid())
            //    return null;

            return new Ribbon();
        }

        private Outlook.Items _inbox;
        private Outlook.Items _sentItems;

        //private Outlook.Explorer _explorer;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //Global Error handlers
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
            try
            {
                if (!License.IsLicenseValid())
                    return;
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }
            

            _inbox = InboxFolder.Items;
            _inbox.ItemAdd += Categorize;
            _inbox.ItemAdd += Inbox_ItemAdd;

            _sentItems = SentItemsFolder.Items;
            _sentItems.ItemAdd += Categorize;
            _sentItems.ItemAdd += SentItems_ItemAdd;

            //GetAllFlagRequests();

            if (Settings.Default.DoTaskDump)
            { 
                var t = new Thread(TaskDumper.TaskDump);
                t.Start();
            }
        }

        private void Categorize(object item)
        {
            var i = FollowUpItem.GetFollowUpItem(item);

            i?.Categorize();
        }

        //private Outlook.Explorer _e;

        //public static void GetAllFlagRequests()
        //{
        //    //var kk =
        //    //    GreatFollowUpperAddin.ToDoFolder.Items.Cast<dynamic>().Where(t => IsActiveToDo(t)).Select(k => k.FlagRequest).Distinct().ToList();

        //    //return;

        //    var l = new List<string>();

        //    var x = GreatFollowUpperAddin.ToDoFolder.Items.Cast<dynamic>()
        //        .Where(t => IsActiveToDo(t))
        //        .Select(t => (FollowUpItem) FollowUpItem.GetFollowUpItem(t))
        //        .Select(t => t.FlagRequest)
        //        .Distinct().ToList();


        //}

        /// <summary>
        /// Invoked when a new item is added to the Sent Items folder
        /// </summary>B
        /// <param name="item"></param>
        private void SentItems_ItemAdd(object item)
        {
            var mail = item as Outlook.MailItem;
            if (mail == null)
                return;

            if (mail.SentOn < DateTime.Now.AddHours(Settings.Default.IgnoreSentItemsBeforeHours))
                //Sent longer than X hours ago -- don't bother
                return;

            var fupf = new FollowUpForm(new FollowUpMailItem(mail), FollowUpItem.FollowUpTrigger.ItemSent);
            fupf.Show();
        }

        private void Inbox_ItemAdd(object item)
        {
            var mail = (FollowUpItem.GetFollowUpItem(item) as FollowUpMailItem);
            if (mail == null)
                return;

            mail.ClearParentFlag();

            //COMMENTED BECAUSE NOTHING BELOW HERE
            //if (mail.SentOn < DateTime.Now.AddHours(Settings.Default.IgnoreSentItemsBeforeHours))
            //    //Sent longer than X hours ago -- don't bother
            //    return;
        }

        public static string Name => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public static Outlook.MAPIFolder InboxFolder => Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
        public static Outlook.MAPIFolder SentItemsFolder => Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
        public static Outlook.MAPIFolder ToDoFolder => Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderToDo);


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            /*
             * This cast is safe -- see 
             *  https://msdn.microsoft.com/en-us/library/system.unhandledexceptioneventargs.exceptionobject%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
             *  > Apply the RuntimeCompatibilityAttribute attribute with a RuntimeCompatibilityAttribute.WrapNonExceptionThrows value of true to the assembly that contains the event handler. This wraps all exceptions not derived from the Exception class in a RuntimeWrappedException object. You can then safely cast (in C#) or convert (in Visual Basic) the object returned by this property to an Exception object, and retrieve the original exception object from the RuntimeWrappedException.WrappedException property. Note that some compilers, such as the C# and Visual Basic compilers, automatically apply this attribute.
             *  >> https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.runtimecompatibilityattribute.wrapnonexceptionthrows(v=vs.110).aspx
             *  >>> added on top of this file before the namespace
             */
            HandleError((Exception)e.ExceptionObject);
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleError(e.Exception);
        }
    
        private static void HandleError(Exception e)
        {
            MessageBox.Show($"Whoops... There was an error! Please send this to wouter.vanranst@gmail.com:\n\n{e}", GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
