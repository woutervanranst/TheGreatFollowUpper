using System;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper.Extensions
{
    internal abstract class FollowUpItem
    {
        internal enum FollowUpTrigger
        {
            //ItemSent,
            //ItemContextClick,
            //Form
            Ribbon,
            ItemSent

        }

        public static FollowUpItem GetFollowUpItem(object o)
        {
            return GetFollowUpItemInt((dynamic)o); //Cast to dynamic coz otherwise the overloads don't work properly
        }

        private static FollowUpItem GetFollowUpItemInt(Outlook.MailItem m)
        {
            return new FollowUpMailItem(m);
        }

        private static FollowUpItem GetFollowUpItemInt(Outlook.TaskItem t)
        {
            return new FollowUpTaskItem(t);
        }

        private static FollowUpItem GetFollowUpItemInt(dynamic o)
        {
            return null; //TODO: to Object, MessageClass print to debug
        }


        public void DoFollowUp(bool addFlag, DateTime? flagDate, bool clearFlagOnReply, bool addReminder, bool moveToInbox, 
            bool updateCategories, string flagRequest = null, string categories = null)
        {
            //Add Flag
            if (addFlag)
                Flag(flagDate);

            //Add Flag Request
            if (flagRequest != null)
                FlagRequest = flagRequest;

            //Set Reminder
            if (addReminder)
                AddReminder(flagDate);
            ReminderSet = addReminder;

            //Update Categories
            if (updateCategories)
                Categories = categories;

            Save();

            //Clear the flag on reply
            if (clearFlagOnReply)
                SetClearFlagOnReply();

            //Move to inbox
            if (moveToInbox)
                Move();

            CompletePreviousFlags();
        }

        public abstract string Subject { get; }
        public abstract string Categories { get; set; }

        public abstract FollowUpItem ParentItem { get; }


        protected abstract void Flag(DateTime? flagDate);
        public abstract string FlagRequest { get; set; }
        protected abstract void SetClearFlagOnReply();
        protected abstract void CompletePreviousFlags();
        public abstract bool ReminderSet { get; set; }


        protected abstract void AddReminder(DateTime? flagDate);

        public abstract void Display();

        public abstract bool CanMove();
        protected abstract void Move();

        public void Save()
        {
            try
            {
                SaveWrapper();
            }
            catch (Exception e)
            {
                throw new Exception($"Error while saving item '{this.Subject}'\n{e.Message}", e);
            }
        }
        protected abstract void SaveWrapper();

        public abstract void Categorize();
    }
}
