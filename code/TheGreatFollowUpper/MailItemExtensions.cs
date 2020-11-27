using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace TheGreatFollowUpper
{
    internal static class MailItemExtensions
    {
        private const string _doNotUpdateCategoriesUserPropertyName = "TgfDoNotUpdateCategories";
        private const string _clearFlagOnReplyUserPropertyName = "TgfClearFlagOnReply";

        public static void TreatOutgoing(this Outlook.MailItem mail, bool addFlag, DateTime? flagDate, bool clearFlagOnReply, bool addReminder, bool moveToInbox, bool addCategories, string categories = null)
        {
            //Set Flag
            if (addFlag)
            {
                //http://www.slipstick.com/developer/code-samples/set-flag-follow-up-using-vba/
                mail.MarkAsTask(Outlook.OlMarkInterval.olMarkThisWeek);
                mail.TaskStartDate = flagDate.Value;
                mail.TaskDueDate = flagDate.Value;
            }

            //Set Reminder
            if (addReminder)
            {
                mail.ReminderTime = mail.TaskDueDate.Add(flagDate.Value.Date == DateTime.Today ?
                    Properties.Settings.Default.DefaultSameDayReminder :
                    Properties.Settings.Default.DefaultOtherDayReminder);

                mail.ReminderSet = true;
            }

            //Add Categories
            if (addCategories)
                mail.Categories = categories;
            
            mail.Save();

            //Clear the flag on reply
            if (clearFlagOnReply)
            {
                var prop = mail.UserProperties.Add(_clearFlagOnReplyUserPropertyName, Outlook.OlUserPropertyType.olYesNo);
                prop.Value = true;
                mail.Save();
            }

            //Move to inbox
            if (moveToInbox)
            {
                //var inboxFolder = Globals.GreatFollowUpperAddin.Application.ActiveExplorer().Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
                if (mail.Parent.FullFolderPath != GreatFollowUpperAddin.InboxFolder.FullFolderPath)
                {
                    //Add a flag not to update the categories on the incoming mail as it is a mail from us
                    var prop = mail.UserProperties.Add(_doNotUpdateCategoriesUserPropertyName, Outlook.OlUserPropertyType.olYesNo);
                    prop.Value = true;
                    mail.Save();

                    mail.Move(GreatFollowUpperAddin.InboxFolder);
                }
            }

            //Complete Previous Flags
            CompletePreviousFlags(mail);
        }

        public static void TreatIncoming(this Outlook.MailItem mail)
        {
            //If this is one of our own mails, moved to inbox, don't update categories
            var prop = mail.UserProperties[_doNotUpdateCategoriesUserPropertyName];
            if (prop == null || prop.Value != true)
            {
                mail.Categories = mail.ParentMailItem()?.Categories;
                mail.Save();
            }

            //If the parent mail was marked to clear it's flag when a reply arrives, do so
            var parentMail = mail.ParentMailItem();
            if (parentMail != null)
            {
                prop = parentMail.UserProperties[_clearFlagOnReplyUserPropertyName];
                if (prop != null && prop.Value == true)
                    parentMail.MarkTaskComplete();
            }
        }

        private static void CompletePreviousFlags(this Outlook.MailItem mail)
        {
            var m = mail;
            var l = new List<Outlook.MailItem>();

            while (m.ParentMailItem() != null)
            {
                m = m.ParentMailItem();

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
            var topic = "[ConversationTopic] = " + "\"" + mail.ConversationTopic + "\"";

            return folder.Items.Restrict(topic);
        }

        private static void MarkTaskComplete(this Outlook.MailItem mail)
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



        public static DateTime ParseFollowUpDate(object value)
        {
            if (value is string)
                value = ParseTagToType((string)value);

            if (value is DayOfWeek)
                return GetNextWeekday((DayOfWeek)value);
            if (value is int)
                return AddBusinessDays((int)value);

            ////ContextMenu only has string tags
            //var value2 = value as string;
            //if (value2 != null)
            //{
            //    return ParseFollowUpDate(ParseTagToType(value2));
            //}

            throw new NotImplementedException();
        }

        private static object ParseTagToType(string value2)
        {
            int businessDays;
            DayOfWeek day;

            if (int.TryParse(value2, out businessDays))
                return businessDays;

            if (Enum.TryParse<DayOfWeek>(value2, out day))
                return day;

            throw new NotImplementedException();
        }
        private static DateTime GetNextWeekday(DayOfWeek day)
        {
            var start = DateTime.Now;

            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            var daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;

            if (daysToAdd == 0)
                daysToAdd = 7; //hack: if we say next friday and it s friday, the result is 0

            return start.AddDays(daysToAdd);
        }
        private static DateTime AddBusinessDays(int businessDays)
        {
            DateTime startDate = DateTime.Now;

            int direction = Math.Sign(businessDays);
            if (direction == 1)
            {
                if (startDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    startDate = startDate.AddDays(2);
                    businessDays = businessDays - 1;
                }
                else if (startDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    startDate = startDate.AddDays(1);
                    businessDays = businessDays - 1;
                }
            }
            else
            {
                if (startDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    startDate = startDate.AddDays(-1);
                    businessDays = businessDays + 1;
                }
                else if (startDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    startDate = startDate.AddDays(-2);
                    businessDays = businessDays + 1;
                }
            }

            int initialDayOfWeek = Convert.ToInt32(startDate.DayOfWeek);

            int weeksBase = Math.Abs(businessDays / 5);
            int addDays = Math.Abs(businessDays % 5);

            if ((direction == 1 && addDays + initialDayOfWeek > 5) ||
                 (direction == -1 && addDays >= initialDayOfWeek))
            {
                addDays += 2;
            }

            int totalDays = (weeksBase * 7) + addDays;
            return startDate.AddDays(totalDays * direction);
        }
    }
}
