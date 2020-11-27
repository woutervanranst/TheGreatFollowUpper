using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using System.Diagnostics;

namespace TheGreatFollowUpper
{
    public partial class GreatFollowUpperAddin
    {
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon();
        }


        public static GreatFollowUpperAddin Instance;

        //Outlook.Inspectors inspectors;

        Outlook.Items SentItems;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //var x = Application.Session.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail).Items;
            //x.ItemAdd += X_ItemAdd;

            SentItems = Application.GetNamespace("MAPI").GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail).Items;
            SentItems.ItemAdd += SentItems_ItemAdd;

            //var wb = this.Application;
            //((Word.ApplicationEvents4_Event)wb).NewDocument += new Word.ApplicationEvents4_NewDocumentEventHandler(Application_NewDocument);

            //Application.



            Instance = this;

    //If MsgBox("ItemAdd hooks set. Implement fix?", vbInformation + vbYesNo, "Event Test Initialised") = vbYes Then
    //    ' Events may not fire against Exchange 2010
    //    On Error Resume Next
    //    ReceivedItems.GetFirst
    //    SentItems.GetFirst
    //    Err.Clear
    //End If

        }

        

        private void SentItems_ItemAdd(object Item)
        {
            var mail = Item as Outlook.MailItem;
            if (mail == null)
                return;

            var form = new FollowUpForm();
            form.subjectText.Text = mail.Subject;
            var result = form.ShowDialog();

            if (form.none.Checked)
                return;

            foreach (var ctrl in form.Controls)
            {
                var r = ctrl as RadioButton;
                if (r != null && r.Checked)
                {
                    Helper.Flag(mail, r.Tag, form.addReminder.Checked);
                    break;
                }
            }


            if (form.move.Checked)
            {
                var inbox = Application.ActiveExplorer().Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
                mail.Move(inbox);
            }

            Helper.GetConversationInformation(mail);
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
