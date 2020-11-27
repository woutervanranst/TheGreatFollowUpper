using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper.Extensions
{
    internal class FollowUpTaskItem : FollowUpItem
    {
        private readonly Outlook.TaskItem _item;
        protected internal FollowUpTaskItem(Outlook.TaskItem t)
        {
            if (t == null)
                throw new ArgumentException("TaskItem is null");

            _item = t;
        }

        public override string Subject => _item.Subject;

        public override string Categories
        {
            get { return _item.Categories; }
            set { _item.Categories = value; }
        }

        public override FollowUpItem ParentItem => null;

        protected override void AddReminder(DateTime? flagDate)
        {
            _item.ReminderTime = _item.DueDate.Add(flagDate.Value.Date == DateTime.Today ?
                Properties.Settings.Default.DefaultSameDayReminder :
                Properties.Settings.Default.DefaultOtherDayReminder);
        }

        public override void Display()
        {
            _item.Display();
        }

        public override bool ReminderSet
        {
            get { return _item.ReminderSet; }
            set { _item.ReminderSet = value; }
        }

        public override string FlagRequest
        {
            // https://social.msdn.microsoft.com/Forums/sqlserver/en-US/94783892-937f-4f81-9feb-5d8e62e5e938/setting-followup-statuses-using-mapi?forum=outlookdev
            get { return _item.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/id/{00062008-0000-0000-C000-000000000046}/8530001F"); }
            set { _item.PropertyAccessor.SetProperty("http://schemas.microsoft.com/mapi/id/{00062008-0000-0000-C000-000000000046}/8530001F", value); }
        }

        protected override void Flag(DateTime? flagDate)
        {
            _item.StartDate = flagDate.Value;
            _item.DueDate = flagDate.Value;
        }

        protected override void SaveWrapper()
        {
            _item.Save();
        }

        public override void Categorize()
        {
            throw new NotImplementedException("Doesn't make sense to categorize task according to parent");
        }

        protected override void SetClearFlagOnReply()
        {
            throw new NotImplementedException("Doesn't make sense to clear flag on reply for a task");
        }

        public override bool CanMove()
        {
            return false;
        }
        protected override void Move()
        {
            throw new NotImplementedException("Doesn't make sense to move a Task");
        }

        protected override void CompletePreviousFlags()
        {
            //Nothing to see here
        }
    }
}
