using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheGreatFollowUpper.Properties;

namespace TheGreatFollowUpper
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            pg.SelectedObject = Settings.Default;

        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var r = MessageBox.Show("Save changes?", GreatFollowUpperAddin.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            switch (r)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    return;
                case DialogResult.Yes:
                    Settings.Default.Save();
                    break;
                case DialogResult.No:
                    Settings.Default.Reload();
                    break;
            }
        }
    }
}
