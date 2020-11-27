using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper
{
    internal static class Helper
    {

        public static void GetConversationInformation(Outlook.MailItem theMailItem)
        {

            //https://msdn.microsoft.com/en-us/library/office/ff184625.aspx

            if (!((Outlook.Folder)theMailItem.Parent).Store.IsConversationEnabled)
                return;

            var conv = theMailItem.GetConversation();
            if (conv == null)
                return;

            // Obtain root items and enumerate Conversation.
            var simpleItems = conv.GetRootItems();
            foreach (object simpleItem in simpleItems)
            {
                //if (simpleItem is Outlook.MailItem)
                //{
                //    var mail = simpleItem as Outlook.MailItem;
                //    var inFolder = mail.Parent as Outlook.Folder;
                //    //string msg = mail.Subject + " in folder " + inFolder.Name;
                //    //Debug.WriteLine(msg);
                //}

                // Call EnumerateConversation to access child nodes of root items.
                _flaggedItems = new List<Outlook.MailItem>();
                EnumerateConversation(simpleItem, conv);

                if (_flaggedItems.Count == 2) //THe old one + the new one = 2
                {
                    var result = MessageBox.Show("There is 1 mail that was previously marked for follow up. Want me to complete it?", "The Great Followupper", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var mail = _flaggedItems.First();

                        //http://www.pcreview.co.uk/threads/outlook-mark-a-mailitem-as-complete.2634810/

                        mail.ReminderSet = false;
                        mail.FlagIcon = Outlook.OlFlagIcon.olNoFlagIcon;
                        mail.FlagStatus = Outlook.OlFlagStatus.olFlagComplete;

                        //mail.ClearTaskFlag();

                        mail.Save();
                    }
                }
                else if (_flaggedItems.Count > 2)
                    MessageBox.Show("There are multiple flagged mails in this conversation. Please check manually");
            }
        }

        private static List<Outlook.MailItem> _flaggedItems;

        private static void EnumerateConversation(object root, Outlook.Conversation conversation)
        {
            var items = conversation.GetChildren(root);
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item is Outlook.MailItem)
                    {
                        var mail = item as Outlook.MailItem;
                        //var inFolder = mail.Parent as Outlook.Folder;
                        //string msg = mailItem.Subject + " in folder " + inFolder.Name;
                        //Debug.WriteLine(msg);

                        if (mail.FlagStatus == Outlook.OlFlagStatus.olFlagMarked)
                            _flaggedItems.Add(mail);
                    }
                    // Continue recursion.
                    EnumerateConversation(item, conversation);
                }
            }
        }


        public static void Flag(Outlook.MailItem mail, dynamic tag, bool addReminder = false)
        {
            int businessDays;
            if (int.TryParse(tag, out businessDays))
            {
                //Number of business days
                SetFlagBusinessDays(mail, businessDays);
            }
            else
            {
                DayOfWeek day;
                if (Enum.TryParse<DayOfWeek>(tag, out day))
                {
                    //Weekday
                    SetFlagWeekday(mail, day);
                }
            }

            if (addReminder)
            {
                mail.ReminderTime = mail.TaskDueDate.AddHours(9);
                //mail.ReminderOverrideDefault = true;
                mail.ReminderSet = true;
                mail.Save();
            }
        }
        private static void SetFlagBusinessDays(Outlook.MailItem mail, int businessDays)
        {
            SetFlag(mail, AddBusinessDays(businessDays));
        }

        private static void SetFlagWeekday(Outlook.MailItem mail, DayOfWeek day)
        {
            SetFlag(mail, GetNextWeekday(day));
        }

        private static void SetFlag(Outlook.MailItem mail, DateTime flag)
        {
            if (mail == null)
                return;

            //http://www.slipstick.com/developer/code-samples/set-flag-follow-up-using-vba/

            mail.MarkAsTask(Outlook.OlMarkInterval.olMarkThisWeek);
            mail.TaskStartDate = flag;
            mail.TaskDueDate = flag;

            //var conv = mail.GetConversation();
            //var table = conv.GetTable();
            ////var group = table.grou
            //var group = conv.GetRootItems();

            //foreach (Outlook.MailItem m in conv.GetChildren(mail))
            //{
            //    if (m.IsMarkedAsTask)
            //    {
            //        var x = 4;
            //    }
            //}


            mail.Save();
        }

        private static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime start = DateTime.Now;

            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;

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
