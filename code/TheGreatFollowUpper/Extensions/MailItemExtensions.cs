using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace TheGreatFollowUpper.Extensions
{
    internal static class MailItemExtensions
    {
        //public const string _doNotUpdateCategoriesUserPropertyName = "TgfDoNotUpdateCategories";
        //public const string _clearFlagOnReplyUserPropertyName = "TgfClearFlagOnReply";

        //public static void DoFollowUp(this Outlook.MailItem mail, bool addFlag, DateTime? flagDate, bool clearFlagOnReply, bool addReminder, bool moveToInbox, bool addCategories, string categories = null)
        //{
            //////Set Flag
            ////if (addFlag)
            ////{
            ////    Flag(mail, flagDate);
            ////}

            ////Set Reminder
            //if (addReminder)
            //{
            //    mail.ReminderTime = mail.TaskDueDate.Add(flagDate.Value.Date == DateTime.Today ?
            //        Properties.Settings.Default.DefaultSameDayReminder :
            //        Properties.Settings.Default.DefaultOtherDayReminder);

            //    mail.ReminderSet = true;
            //}

            ////Add Categories
            //if (addCategories)
            //    mail.Categories = categories;
            
            //mail.Save();

            ////Clear the flag on reply
            //if (clearFlagOnReply)
            //{
            //    var prop = mail.UserProperties.Add(_clearFlagOnReplyUserPropertyName, Outlook.OlUserPropertyType.olYesNo);
            //    prop.Value = true;
            //    mail.Save();
            //}

            ////Move to inbox
            //if (moveToInbox)
            //{
            //    if (mail.Parent.FullFolderPath != GreatFollowUpperAddin.InboxFolder.FullFolderPath)
            //    {
            //        //Add a flag not to update the categories on the incoming mail as it is a mail from us
            //        var prop = mail.UserProperties.Add(_doNotUpdateCategoriesUserPropertyName, Outlook.OlUserPropertyType.olYesNo);
            //        prop.Value = true;
            //        mail.Save();

            //        mail.Move(GreatFollowUpperAddin.InboxFolder);
            //    }
            //}

            ////Complete Previous Flags
            //CompletePreviousFlags(mail);
        //}

        //private static void Flag(Outlook.MailItem mail, DateTime? flagDate)
        //{
            
        //}

        //public static void TreatIncoming(this Outlook.MailItem mail)
        //{
        //    var item = (FollowUpMailItem)FollowUpItem.GetFollowUpItem(mail);

        //    //If this is one of our own mails, moved to inbox, don't update categories
        //    if (!item.DoNotUpdateCategories)
        //    {
        //        mail.Categories = mail.ParentMailItem()?.Categories;
        //        mail.Save();
        //    }

        //    //If the parent mail was marked to clear it's flag when a reply arrives, do so
        //    var parentMail = mail.ParentMailItem();
        //    if (parentMail != null)
        //    {
        //        if (item.ClearFlagOnReply)
        //            parentMail.MarkTaskComplete();
        //    }
        //}

        public static void CompletePreviousFlags(this Outlook.MailItem mail)
        {
            var m = mail;
            var l = new List<Outlook.MailItem>();

            while ((m = m.ParentMailItem()) != null)
            {
                if (m.FlagStatus == Outlook.OlFlagStatus.olFlagMarked)
                    l.Add(m);
            }

            if (l.Count == 1 && 
                MessageBox.Show("There is 1 mail that was previously marked for follow up. Want me to complete it?", 
                    GreatFollowUpperAddin.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                l.Single().MarkTaskComplete();
            else if (l.Count > 2)
                    MessageBox.Show("There are multiple flagged mails in this conversation. Please check manually",
                        GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);



            ////https://msdn.microsoft.com/en-us/library/office/ff184625.aspx

            //if (!((Outlook.Folder)mail.Parent).Store.IsConversationEnabled)
            //    return;

            //var conv = mail.GetConversation();
            //if (conv == null)
            //    return;

            //// Obtain root items and enumerate Conversation.
            //var simpleItems = conv.GetRootItems();
            //foreach (object simpleItem in simpleItems)
            //{
            //    // Call EnumerateConversation to access child nodes of root items.
            //    _flaggedItems = new List<Outlook.MailItem>();
            //    EnumerateConversation(simpleItem, conv);

            //    if (_flaggedItems.Count == 2) //THe old one + the new one = 2
            //    {
            //        var result = MessageBox.Show("There is 1 mail that was previously marked for follow up. Want me to complete it?", "The Great Followupper", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (result == DialogResult.Yes)
            //            _flaggedItems.First().MarkTaskComplete();
            //    }
            //    else if (_flaggedItems.Count > 2)
            //        MessageBox.Show("There are multiple flagged mails in this conversation. Please check manually");
            //}
        }

        /// <summary>
        /// Get the Parent Mail Item of this mail
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static Outlook.MailItem ParentMailItem(this Outlook.MailItem mail)
        {
            //WHEN YOU REFACTOR >> CACHE IT TO AVOID REPEAT CALLS


            //http://www.outlookcode.com/codedetail.aspx?id=1714
            var index = mail.ConversationIndex.Substring(0, mail.ConversationIndex.Length - 10);

            //Search Inbox & Sent Items
            foreach (var m in 
                SearchMailsInConversationInFolder(mail, Outlook.OlDefaultFolders.olFolderInbox).OfType<Outlook.MailItem>().Union(
                SearchMailsInConversationInFolder(mail, Outlook.OlDefaultFolders.olFolderSentMail).OfType<Outlook.MailItem>()))
            {
                if (m.ConversationIndex == index)
                    return m;
            }
            
            return null;



            ////http://www.outlookcode.com/codedetail.aspx?id=1714

            //var index = mail.ConversationIndex.Substring(0, mail.ConversationIndex.Length - 10);
            ////var inbox = Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            //var topic = $"urn:schemas:httpmail:thread-topic='{mail.ConversationTopic}'";
            ////var topic = "[ConversationTopic] = " + "\"" + mail.ConversationTopic + "\"";
            ////var items = inbox.Items.Restrict(topic);
            //var scope = "'" + Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).FolderPath +
            //            "','" + Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail).FolderPath + "'";
            //var items = Globals.GreatFollowUpperAddin.Application.AdvancedSearch(scope, topic);

            //foreach (var item in items.Results) //NOTE: LINQ expression results in casting errors
            //{
            //    if (!(item is Outlook.MailItem))
            //        continue;

            //    var m = (Outlook.MailItem) item;
            //    if (m.ConversationIndex == index)
            //        return m;
            //}

            //return null;

            ////For Each itm In itms If itm.ConversationIndex = strIndex Then Debug.Print itm.To Set FindParentMessage = itm            Exit For        End If    Next
        }
        private static Outlook.Items SearchMailsInConversationInFolder(Outlook.MailItem mail, Outlook.OlDefaultFolders folderType)
        {
            //http://www.outlookcode.com/codedetail.aspx?id=1714
            var folder = Globals.GreatFollowUpperAddin.Application.Session.GetDefaultFolder(folderType);
            var parsedConversationTopic = mail.ConversationTopic?.Replace("\"", "\"\"").Replace("'", "''"); // " If the search string contains a single quote character, escape the single quote character in the string with another single quote character." >> https://msdn.microsoft.com/en-us/library/office/aa210275%28v=office.11%29.aspx?f=255&MSPPError=-2147217396
            var topic = "[ConversationTopic] = " + "\"" + parsedConversationTopic  + "\"";

            return folder.Items.Restrict(topic);
        }

        internal static void MarkTaskComplete(this Outlook.MailItem mail)
        {
            //http://www.pcreview.co.uk/threads/outlook-mark-a-mailitem-as-complete.2634810/

            mail.ReminderSet = false;
            mail.FlagIcon = Outlook.OlFlagIcon.olNoFlagIcon;
            mail.FlagStatus = Outlook.OlFlagStatus.olFlagComplete;

            //mail.ClearTaskFlag();

            mail.Save();
        }

        //private static List<Outlook.MailItem> _flaggedItems;

        //private static void EnumerateConversation(object root, Outlook.Conversation conversation)
        //{
        //    var items = conversation.GetChildren(root);
        //    if (items.Count > 0)
        //    {
        //        foreach (var item in items)
        //        {
        //            if (item is Outlook.MailItem)
        //            {
        //                var mail = item as Outlook.MailItem;
        //                //var inFolder = mail.Parent as Outlook.Folder;
        //                //string msg = mailItem.Subject + " in folder " + inFolder.Name;
        //                //Debug.WriteLine(msg);

        //                if (mail.FlagStatus == Outlook.OlFlagStatus.olFlagMarked)
        //                    _flaggedItems.Add(mail);
        //            }
        //            // Continue recursion.
        //            EnumerateConversation(item, conversation);
        //        }
        //    }
        //}
    }
}
