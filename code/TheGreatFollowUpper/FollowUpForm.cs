using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper
{
    internal partial class FollowUpForm : Form
    {
        private readonly Outlook.MailItem _mail;

        public FollowUpForm(Outlook.MailItem mail, object followUpOption = null)
        {
            InitializeComponent();

            _mail = mail;

            this.Text = GreatFollowUpperAddin.Name;

            this.mail.Text = mail.Subject;

            this.laterToday_CheckedChanged(null, null);

            //Follow Up Option
            if (followUpOption != null)
            {
                var optionToCheck = followUpOptions.Controls.OfType<RadioButton>().SingleOrDefault(r => r.Tag?.ToString() == (string)followUpOption);
                if (optionToCheck != null)
                    optionToCheck.Checked = true;
            }

            //Categories
            var mailCategories = mail.ParentMailItem()?.Categories?.Split(new[] {"; "}, StringSplitOptions.None) ?? new string[] { };

            foreach (string category in 
                Globals.GreatFollowUpperAddin.Application.Session.Categories.Cast<Outlook.Category>().Select(a => a.Name)
                .Union(mailCategories))
            {
                var i = categoriesListBox.Items.Add(category);
                categoriesListBox.SetItemChecked(i, mailCategories.Contains(category));
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();


            DateTime? flagDate = null;
            if (AddFlag)
                flagDate = MailItemExtensions.ParseFollowUpDate(FollowUpValue);

            var categories = string.Join("; ", categoriesListBox.CheckedItems.Cast<string>());

            _mail.TreatOutgoing(AddFlag, flagDate, clearFlagOnReply.Checked, AddReminder, MoveToInbox, true, categories);

            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }





        private void none_CheckedChanged(object sender, EventArgs e)
        {
            move.Enabled = !none.Checked;
            move.Checked = !none.Checked && Properties.Settings.Default.DefaultMoveToInbox;

            addReminder.Enabled = !none.Checked;

            clearFlagOnReply.Enabled = !none.Checked;
        }

        private void move_CheckedChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys != Keys.Shift)
                return;

            Properties.Settings.Default.DefaultMoveToInbox = move.Checked;
            Properties.Settings.Default.Save();
        }

        private void laterToday_CheckedChanged(object sender, EventArgs e)
        {
            addReminder.Text = laterToday.Checked ? 
                $"Add reminder at { Properties.Settings.Default.DefaultSameDayReminder.ToString(@"h\:mm") }" : 
                $"Add reminder at { Properties.Settings.Default.DefaultOtherDayReminder.ToString(@"h\:mm") }";
        }


        public bool AddFlag => !this.none.Checked;
        public object FollowUpValue => followUpOptions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)?.Tag;

        public bool AddReminder => this.addReminder.Checked;

        public bool MoveToInbox => this.move.Checked;

        //public bool AddCategories => true;
        //public string Categories => _mail.ParentMailItem()?.Categories;

        private void mail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //((Outlook.ItemEvents_10_Event) _mail).Close += (ref bool cancel) =>
            //{
            //    this.Show();
            //};
            _mail.Display();
        }

        private void btnNewCategory_Click(object sender, EventArgs e)
        {
            var r = InputBox.Show("Name?", GreatFollowUpperAddin.Name);

            if (r.ReturnCode != DialogResult.OK)
                return;

            Globals.GreatFollowUpperAddin.Application.Session.Categories.Add(r.Text);

            var i = categoriesListBox.Items.Add(r.Text);
            categoriesListBox.SetItemChecked(i, true);
        }
    }
}
