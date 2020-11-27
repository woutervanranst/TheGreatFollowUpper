using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper
{
    internal static class Helper
    {
        public static void Flag(Outlook.MailItem mail, dynamic tag)
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
