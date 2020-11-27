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
            var result = form.ShowDialog();


            if (form.none.Checked)
                return;

            foreach (var ctrl in form.Controls)
            {
                var r = ctrl as RadioButton;
                if (r != null && r.Checked)
                {
                    Helper.Flag(mail, r.Tag);
                    break;
                }
            }

            if (form.move.Checked)
            {
                var inbox = Application.ActiveExplorer().Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
                mail.Move(inbox);
            }

            GetConversationInformation(mail);











        }









        public void GetConversationInformation(Outlook.MailItem theMailItem)
        {

            //https://msdn.microsoft.com/en-us/library/office/ff184625.aspx

            if (!((Outlook.Folder)theMailItem.Parent).Store.IsConversationEnabled)
                return;

            var conv = theMailItem.GetConversation();

            if (conv == null)
                return;

            var table = conv.GetTable();

            Debug.WriteLine("Conversation Items Count: " + table.GetRowCount().ToString());
            
            // Obtain root items and enumerate Conversation.
            Outlook.SimpleItems simpleItems = conv.GetRootItems();
            foreach (object item in simpleItems)
            {
                if (item is Outlook.MailItem)
                {
                    Outlook.MailItem mail = item as Outlook.MailItem;
                    Outlook.Folder inFolder = mail.Parent as Outlook.Folder;
                    string msg = mail.Subject + " in folder " + inFolder.Name;
                    Debug.WriteLine(msg);
                }

                // Call EnumerateConversation to access child nodes of root items.
                _flaggedItems = new List<Outlook.MailItem>();
                EnumerateConversation(item, conv);

                if (_flaggedItems.Count == 2) //THe old one + the new one = 2
                {
                    var result = MessageBox.Show("There is 1 mail that was previously marked for follow up. Want me to complete it?", "The Great Followupper", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        var item2 = _flaggedItems.First();

                        //http://www.pcreview.co.uk/threads/outlook-mark-a-mailitem-as-complete.2634810/

                        item2.ReminderSet = false;
                        item2.FlagIcon = Outlook.OlFlagIcon.olNoFlagIcon;
                        item2.FlagStatus = Outlook.OlFlagStatus.olFlagComplete;

                        //item2.ClearTaskFlag();


                        item2.Save();


                    }


                }
            }
        }

        private List<Outlook.MailItem> _flaggedItems;

        void EnumerateConversation(object item, Outlook.Conversation conversation)
        {
            Outlook.SimpleItems items = conversation.GetChildren(item);
            if (items.Count > 0)
            {
                foreach (object myItem in items)
                {
                    if (myItem is Outlook.MailItem)
                    {
                        var mailItem = myItem as Outlook.MailItem;
                        var inFolder = mailItem.Parent as Outlook.Folder;
                        //string msg = mailItem.Subject + " in folder " + inFolder.Name;
                        //Debug.WriteLine(msg);

                        if (mailItem.FlagStatus == Outlook.OlFlagStatus.olFlagMarked)
                            _flaggedItems.Add(mailItem);
                    }
                    // Continue recursion.
                    EnumerateConversation(myItem, conversation);
                }
            }
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
