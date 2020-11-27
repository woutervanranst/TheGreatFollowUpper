using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper.Extensions
{
    internal class FollowUpMailItem : FollowUpItem
    {
        private readonly Outlook.MailItem _item;

        protected internal FollowUpMailItem(Outlook.MailItem m)
        {
            if (m == null)
                throw new ArgumentException("MailItem is null");

            _item = m;
        }

        public override string Subject => _item.Subject;

        public override string Categories
        {
            get { return _item.Categories; }
            set { _item.Categories = value; }
        }

        public override FollowUpItem ParentItem
        {
            get
            {
                var m = _item.ParentMailItem();
                if (m == null)
                    return null;

                return new FollowUpMailItem(_item.ParentMailItem());
            }
        }

        public bool ClearFlagOnReply
        {
            get
            {
                var prop = _item.UserProperties["TgfClearFlagOnReply"];
                if (prop != null && prop.Value == true)
                    return true;

                return false;
            }
            set
            {
                var prop = _item.UserProperties.Add("TgfClearFlagOnReply", Outlook.OlUserPropertyType.olYesNo);
                prop.Value = value;
                _item.Save();
            }
        }

        public bool DoNotUpdateCategories
        {
            get
            {
                var prop = _item.UserProperties["TgfDoNotUpdateCategories"];
                if (prop != null && prop.Value == true)
                    return true;

                return false;
            }
            set
            {
                var prop = _item.UserProperties.Add("TgfDoNotUpdateCategories", Outlook.OlUserPropertyType.olYesNo);
                prop.Value = value;
                _item.Save();
            }
        }

        protected override void SetClearFlagOnReply()
        {
            ClearFlagOnReply = true;
        }

        protected override void AddReminder(DateTime? flagDate)
        {
            _item.ReminderTime = flagDate.Value;
            //_item.ReminderTime = _item.TaskDueDate.Add(flagDate.Value.Date == DateTime.Today ?
            //    Properties.Settings.Default.DefaultSameDayReminder :
            //    Properties.Settings.Default.DefaultOtherDayReminder);
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

        private Regex _PreviousFlagRequest = new Regex(@"(?=@)[^\s]*(?:-)");
        private Regex _ExchangeOnlineExternalTag = new Regex(@"^\[External\]");

        public override string FlagRequest
        {
            get
            {
                //return _item.TaskSubject.Replace(_item.Subject, "").Trim();
                return _item.FlagRequest;
            }
            set
            {
                _item.FlagRequest = value;


                var ts = _PreviousFlagRequest.Replace(_item.TaskSubject, "");
                ts = _ExchangeOnlineExternalTag.Replace(ts, "");
                _item.TaskSubject = $"{value}-{ts}";

                if (!_item.TaskSubject.StartsWith("@"))
                    _item.TaskSubject = $"@{_item.TaskSubject}";
            }
        }

        public DateTime SentOn => _item.SentOn;

        protected override void Flag(DateTime? flagDate)
        {
            //http://www.slipstick.com/developer/code-samples/set-flag-follow-up-using-vba/
            _item.MarkAsTask(Outlook.OlMarkInterval.olMarkThisWeek);
            _item.TaskStartDate = flagDate.Value;
            _item.TaskDueDate = flagDate.Value;
        }

        protected override void SaveWrapper()
        {
            _item.Save();
        }

        public override void Categorize()
        {
            //If this is one of our own mails, moved to inbox, don't update categories
            if (!DoNotUpdateCategories)
            {
                var pmi = _item.ParentMailItem();
                if (pmi == null)
                    return;

                Categories = pmi.Categories;
                Save();
            }
        }

        public override bool CanMove()
        {
            return true;
        }
        protected override void Move()
        {
            if (_item.Parent.FullFolderPath != GreatFollowUpperAddin.InboxFolder.FullFolderPath)
            {
                //Add a flag not to update the categories on the incoming mail as it is a mail from us
                DoNotUpdateCategories = true;
                //var prop = _item.UserProperties.Add(MailItemExtensions._doNotUpdateCategoriesUserPropertyName, Outlook.OlUserPropertyType.olYesNo);
                //prop.Value = true;
                //_item.Save();

                _item.Move(GreatFollowUpperAddin.InboxFolder);
            }
        }

        protected override void CompletePreviousFlags()
        {
            _item.CompletePreviousFlags();
        }

        public void ClearParentFlag()
        {
            //If the parent mail was marked to clear it's flag when a reply arrives, do so
            var parentMailItem = _item.ParentMailItem();
            if (parentMailItem == null)
                return;

            var parentFollowUpMailItem = (GetFollowUpItem(parentMailItem) as FollowUpMailItem);
            if (parentFollowUpMailItem == null)
                return;

            if (parentFollowUpMailItem.ClearFlagOnReply)
                parentFollowUpMailItem._item.MarkTaskComplete(); //TODO: CLEAN THIS UP
        }
    }
}
