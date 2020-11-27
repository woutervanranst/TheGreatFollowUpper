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
using System.Threading;
using System.Threading.Tasks;

namespace TheGreatFollowUpper
{
    public partial class GreatFollowUpperAddin
    {
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            //if (!License.IsLicenseValid())
            //    return null;

            return new Ribbon();
        }

        private Outlook.Items _inbox;
        private Outlook.Items _sentItems;

        //private Outlook.Explorer _explorer;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            if (!License.IsLicenseValid())
                return;

            _inbox = InboxFolder.Items; //Application.GetNamespace("MAPI").GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Items;
            _inbox.ItemAdd += Inbox_ItemAdd;

            _sentItems = SentItemsFolder.Items; //Application.GetNamespace("MAPI").GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail).Items;
            _sentItems.ItemAdd += SentItems_ItemAdd;

            var t = new Thread(TaskDump);
            t.Start();

            //_explorer = Application.ActiveExplorer();
            //_explorer.SelectionChange += () =>
            //{
            //    if (Application.ActiveExplorer().Selection.Count != 1)
            //        return;

            //    var sel = Application.ActiveExplorer().Selection[1] as Outlook.TaskItem;
            //    if (sel != null)
            //    {
            //        sel.EntryID 
            //        sel.PropertyChange += name =>
            //        {
            //            if (name != "Subject")
            //                return;



            //            var x = sel.Subject;
            //        };
            //    }
            //};
        }


        /// <summary>
        /// Invoked when a new item is added to the Sent Items folder
        /// </summary>
        /// <param name="item"></param>
        private void SentItems_ItemAdd(object item)
        {
            if (!(item is Outlook.MailItem))
                return;

            var mail = (Outlook.MailItem)item;
            var fupf = new FollowUpForm(mail);
            fupf.Show();
        }

        private void Inbox_ItemAdd(object item)
        {
            if (!(item is Outlook.MailItem))
                return;

            var mail = (Outlook.MailItem)item;
            mail.TreatIncoming();
        }

        private void TaskDump()
        {
            try
            {
                //var f = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderToDo);

                var ts = ToDoFolder.Items.Cast<dynamic>().Where(t => IsActiveToDo(t)).Select(t => $"[{t.EntryID}] - {t.Subject}");

                var p = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"The Great FollowUpper - ToDo {DateTime.Now.ToString("yyyyMMdd-HHmmss")}.txt");

                File.WriteAllLines(p, ts);
            }
            catch (Exception e)
            {
                //Silently exit because otherwise it makes the application go boom
                MessageBox.Show($"Error when dumping ToDos to file:\n{e.ToString()}", Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsActiveToDo(dynamic t)
        {
            try
            {
                return
                    (t is Outlook.TaskItem && !t.Complete) ||
                    (t is Outlook.MailItem && t.FlagStatus == (int)Outlook.OlFlagStatus.olFlagMarked);
            }
            catch (Exception)
            {
                //For encrypted msgs it throws an error
                return false;
            }
        }

        public static string Name => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public static Outlook.MAPIFolder InboxFolder => Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
        public static Outlook.MAPIFolder SentItemsFolder => Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
        public static Outlook.MAPIFolder ToDoFolder => Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderToDo);

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
