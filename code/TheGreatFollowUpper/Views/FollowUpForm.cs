using System;
using System.Linq;
using System.Windows.Forms;
using TheGreatFollowUpper.Extensions;
using TheGreatFollowUpper.Properties;
using TheGreatFollowUpper.Util;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;
using System.Globalization;

namespace TheGreatFollowUpper.Views
{
    internal partial class FollowUpForm : Form
    {
        private readonly FollowUpItem _item;

        private int _remainingSecondsBeforeOkClick;
        private readonly Timer _updateOkButtonTimer;

        #region Form

        public FollowUpForm(FollowUpItem item, FollowUpItem.FollowUpTrigger intent, object followUpOption = null)
        {
            InitializeComponent();

            _item = item;

            this.Text = GreatFollowUpperAddin.Name;
            this.item.Text = item.Subject;

            //Follow Up Options GROUPBOX
            addReminder.Checked = item.ReminderSet;
            clearFlagOnReply.Checked = Settings.Default.DefaultClearFlagOnReply;

            if (followUpOption != null)
            {
                var optionToCheck = grpFollowUpOptions.Controls.OfType<RadioButton>().SingleOrDefault(r => r.Tag?.ToString() == (string) followUpOption);
                if (optionToCheck != null)
                    optionToCheck.Checked = true;
            }

            var reminderTimes = Enumerable.Range(0, 48).Select(i => new TimeSpan(0, 0, i * 30, 0)).Select(ts => ts.ToString(@"hh\:mm")).ToArray();
            reminderTime.Items.AddRange(reminderTimes);

            this.laterToday_CheckedChanged(null, null);

            //Categories GROUPBOX
            var catItem = item;
            if (item is FollowUpMailItem && intent == FollowUpItem.FollowUpTrigger.ItemSent)
                catItem = catItem.ParentItem;

            var cats = catItem?.Categories?.Split(new[] {"; "}, StringSplitOptions.None) ?? new string[] {};

            foreach (var cat in 
                Globals.GreatFollowUpperAddin.Application.Session.Categories.Cast<Outlook.Category>()
                    .Select(a => a.Name)
                    .Union(cats))
            {
                var i = lstCategories.Items.Add(cat);
                lstCategories.SetItemChecked(i, cats.Contains(cat));
            }


            ////Status GROUPBOX
            //IEnumerable<string> frs = JArray.Parse(Settings.Default.FlagRequests).ToObject<string[]>();
            //if (item.FlagRequest != null)
            //    frs = frs.Union(new string[] {item.FlagRequest});

            //Auto Close Timer
            if (Settings.Default.AutoCloseFormAfterSeconds > 0)
            {
                _remainingSecondsBeforeOkClick = Settings.Default.AutoCloseFormAfterSeconds;

                UpdateOkButton();

                _updateOkButtonTimer = new Timer
                {
                    Interval = 1000,
                    Enabled = true
                };
                _updateOkButtonTimer.Tick += (sender, args) => UpdateOkButton();
            }
            else
            {
                btnOK.Text = "&OK";
            }
        }

        private void UpdateOkButton()
        {
            if (_updateOkButtonTimer != null && _updateOkButtonTimer.Enabled && !btnOK.Enabled) //Hack for Nicolas: button is disabled by default, and only after 1 second becomes enabled
            {
                btnOK.Enabled = true;
                btnOK.Focus();
            }

            if (_remainingSecondsBeforeOkClick > 0)
            {
                btnOK.Text = $"&OK ({_remainingSecondsBeforeOkClick}s)";
                _remainingSecondsBeforeOkClick--;
            }
            else
            {
                btnOK.PerformClick();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Hide();
            _updateOkButtonTimer.Stop();
            _updateOkButtonTimer.Dispose();

            DateTime? flagDate = null;
            if (AddFlag)
                flagDate = Utils.ParseFollowUpDate(FollowUpValue, dtpCustom.Value);
            if (AddReminder)
            {
                var rtt = reminderTime.Text;
                if (!rtt.Contains(":") && rtt.Length <= 2) //CASE Parsing eg. 23 or 9
                    rtt += ":00";

                TimeSpan rt;
                if (!TimeSpan.TryParseExact(rtt, @"h\:mm", CultureInfo.InvariantCulture, out rt))
                    rt = DefaultReminderTime();

                flagDate = flagDate.Value.Date + rt;
            }

            var categories = string.Join("; ", this.lstCategories.CheckedItems.Cast<string>());

            _item.DoFollowUp(AddFlag, flagDate, clearFlagOnReply.Checked, AddReminder, MoveToInbox, true, categories: categories);

            Close();
        }
        private void Item_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _item.Display();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            _updateOkButtonTimer.Enabled = false;

            var sf = new SettingsForm();
            sf.ShowDialog();

            _updateOkButtonTimer.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if(...) added for for Nicolas: disable accidentally closing the form when it just pops up
            if (_remainingSecondsBeforeOkClick < Settings.Default.AutoCloseFormAfterSeconds + Settings.Default.SupressCancelDialogSeconds) 
                Close();
        }

        private void FollowUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateOkButtonTimer.Stop();
            _updateOkButtonTimer.Dispose();
        }

        #endregion

        #region Follow Up GROUPBOX

        private void none_CheckedChanged(object sender, EventArgs e)
        {
            move.Enabled = !none.Checked && _item.CanMove();
            move.Checked = !none.Checked && Settings.Default.DefaultMoveToInbox && _item.CanMove();

            addReminder.Enabled = !none.Checked;
            reminderTime.Enabled = !none.Checked;
            clearFlagOnReply.Enabled = !none.Checked;
        }

        private void move_CheckedChanged(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.Shift)
                return;

            Settings.Default.DefaultMoveToInbox = move.Checked;
            Settings.Default.Save();
        }

        private void clearFlagOnReply_CheckedChanged(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.Shift)
                return;

            Settings.Default.DefaultClearFlagOnReply = clearFlagOnReply.Checked;
            Settings.Default.Save();
        }

        private void laterToday_CheckedChanged(object sender, EventArgs e)
        {
            string t = DefaultReminderTime().ToString(@"hh\:mm");
            reminderTime.SelectedItem = t;
        }

        private TimeSpan DefaultReminderTime()
        {
            //addReminder.Text = laterToday.Checked ?
            //    $"Add reminder at {Settings.Default.DefaultSameDayReminder.ToString(@"h\:mm")}" :
            //    $"Add reminder at {Settings.Default.DefaultOtherDayReminder.ToString(@"h\:mm")}";

            return laterToday.Checked ?
                Settings.Default.DefaultSameDayReminder :
                Settings.Default.DefaultOtherDayReminder;
        }

        public bool AddFlag => !this.none.Checked;
        public object FollowUpValue => grpFollowUpOptions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)?.Tag;

        public bool AddReminder => this.addReminder.Checked;

        public bool MoveToInbox => this.move.Checked;

        #endregion

        #region Category GROUPBOX

        private void btnNewCategory_Click(object sender, EventArgs e)
        {
            var r = InputBox.Show("Name?", $"{GreatFollowUpperAddin.Name} - Add New Category");

            if (r.ReturnCode != DialogResult.OK)
                return;

            Globals.GreatFollowUpperAddin.Application.Session.Categories.Add(r.Text);

            var i = lstCategories.Items.Add(r.Text);
            lstCategories.SetItemChecked(i, true);
        }

        #endregion

        private void dtpCustom_ValueChanged(object sender, EventArgs e)
        {
            inCustom.Checked = true;
        }
    }
}