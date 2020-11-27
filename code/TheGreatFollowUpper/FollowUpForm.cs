using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGreatFollowUpper
{
    public partial class FollowUpForm : Form
    {
        public FollowUpForm()
        {
            InitializeComponent();
        }

        private void flag_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void none_CheckedChanged(object sender, EventArgs e)
        {
            move.Enabled = !none.Checked;
            move.Checked = !none.Checked;
        }

        private void move_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
