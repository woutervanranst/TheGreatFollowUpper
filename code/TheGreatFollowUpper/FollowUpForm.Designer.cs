namespace TheGreatFollowUpper
{
    partial class FollowUpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flag = new System.Windows.Forms.Button();
            this.none = new System.Windows.Forms.RadioButton();
            this.in1day = new System.Windows.Forms.RadioButton();
            this.in2days = new System.Windows.Forms.RadioButton();
            this.in3days = new System.Windows.Forms.RadioButton();
            this.in4days = new System.Windows.Forms.RadioButton();
            this.in5days = new System.Windows.Forms.RadioButton();
            this.firstMonday = new System.Windows.Forms.RadioButton();
            this.firstTuesday = new System.Windows.Forms.RadioButton();
            this.firstWednesday = new System.Windows.Forms.RadioButton();
            this.firstThursday = new System.Windows.Forms.RadioButton();
            this.firstFriday = new System.Windows.Forms.RadioButton();
            this.move = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.subjectText = new System.Windows.Forms.Label();
            this.addReminder = new System.Windows.Forms.CheckBox();
            this.laterToday = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // flag
            // 
            this.flag.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.flag.Location = new System.Drawing.Point(166, 420);
            this.flag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flag.Name = "flag";
            this.flag.Size = new System.Drawing.Size(112, 35);
            this.flag.TabIndex = 0;
            this.flag.Text = "&OK";
            this.flag.UseVisualStyleBackColor = true;
            this.flag.Click += new System.EventHandler(this.flag_Click);
            // 
            // none
            // 
            this.none.AutoSize = true;
            this.none.Checked = true;
            this.none.Location = new System.Drawing.Point(66, 89);
            this.none.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.none.Name = "none";
            this.none.Size = new System.Drawing.Size(138, 24);
            this.none.TabIndex = 1;
            this.none.TabStop = true;
            this.none.Tag = "None";
            this.none.Text = "Don\'t follow up";
            this.none.UseVisualStyleBackColor = true;
            this.none.CheckedChanged += new System.EventHandler(this.none_CheckedChanged);
            // 
            // in1day
            // 
            this.in1day.AutoSize = true;
            this.in1day.Location = new System.Drawing.Point(66, 155);
            this.in1day.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.in1day.Name = "in1day";
            this.in1day.Size = new System.Drawing.Size(157, 24);
            this.in1day.TabIndex = 1;
            this.in1day.Tag = "1";
            this.in1day.Text = "In 1 business day";
            this.in1day.UseVisualStyleBackColor = true;
            // 
            // in2days
            // 
            this.in2days.AutoSize = true;
            this.in2days.Location = new System.Drawing.Point(66, 188);
            this.in2days.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.in2days.Name = "in2days";
            this.in2days.Size = new System.Drawing.Size(165, 24);
            this.in2days.TabIndex = 1;
            this.in2days.Tag = "2";
            this.in2days.Text = "In 2 business days";
            this.in2days.UseVisualStyleBackColor = true;
            // 
            // in3days
            // 
            this.in3days.AutoSize = true;
            this.in3days.Location = new System.Drawing.Point(66, 221);
            this.in3days.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.in3days.Name = "in3days";
            this.in3days.Size = new System.Drawing.Size(165, 24);
            this.in3days.TabIndex = 1;
            this.in3days.Tag = "3";
            this.in3days.Text = "In 3 business days";
            this.in3days.UseVisualStyleBackColor = true;
            // 
            // in4days
            // 
            this.in4days.AutoSize = true;
            this.in4days.Location = new System.Drawing.Point(66, 254);
            this.in4days.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.in4days.Name = "in4days";
            this.in4days.Size = new System.Drawing.Size(165, 24);
            this.in4days.TabIndex = 1;
            this.in4days.Tag = "4";
            this.in4days.Text = "In 4 business days";
            this.in4days.UseVisualStyleBackColor = true;
            // 
            // in5days
            // 
            this.in5days.AutoSize = true;
            this.in5days.Location = new System.Drawing.Point(66, 287);
            this.in5days.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.in5days.Name = "in5days";
            this.in5days.Size = new System.Drawing.Size(165, 24);
            this.in5days.TabIndex = 1;
            this.in5days.Tag = "5";
            this.in5days.Text = "In 5 business days";
            this.in5days.UseVisualStyleBackColor = true;
            // 
            // firstMonday
            // 
            this.firstMonday.AutoSize = true;
            this.firstMonday.Location = new System.Drawing.Point(258, 155);
            this.firstMonday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.firstMonday.Name = "firstMonday";
            this.firstMonday.Size = new System.Drawing.Size(125, 24);
            this.firstMonday.TabIndex = 1;
            this.firstMonday.Tag = "Monday";
            this.firstMonday.Text = "First Monday";
            this.firstMonday.UseVisualStyleBackColor = true;
            // 
            // firstTuesday
            // 
            this.firstTuesday.AutoSize = true;
            this.firstTuesday.Location = new System.Drawing.Point(258, 188);
            this.firstTuesday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.firstTuesday.Name = "firstTuesday";
            this.firstTuesday.Size = new System.Drawing.Size(129, 24);
            this.firstTuesday.TabIndex = 1;
            this.firstTuesday.Tag = "Tuesday";
            this.firstTuesday.Text = "First Tuesday";
            this.firstTuesday.UseVisualStyleBackColor = true;
            // 
            // firstWednesday
            // 
            this.firstWednesday.AutoSize = true;
            this.firstWednesday.Location = new System.Drawing.Point(258, 221);
            this.firstWednesday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.firstWednesday.Name = "firstWednesday";
            this.firstWednesday.Size = new System.Drawing.Size(153, 24);
            this.firstWednesday.TabIndex = 1;
            this.firstWednesday.Tag = "Wednesday";
            this.firstWednesday.Text = "First Wednesday";
            this.firstWednesday.UseVisualStyleBackColor = true;
            // 
            // firstThursday
            // 
            this.firstThursday.AutoSize = true;
            this.firstThursday.Location = new System.Drawing.Point(258, 254);
            this.firstThursday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.firstThursday.Name = "firstThursday";
            this.firstThursday.Size = new System.Drawing.Size(134, 24);
            this.firstThursday.TabIndex = 1;
            this.firstThursday.Tag = "Thursday";
            this.firstThursday.Text = "First Thursday";
            this.firstThursday.UseVisualStyleBackColor = true;
            // 
            // firstFriday
            // 
            this.firstFriday.AutoSize = true;
            this.firstFriday.Location = new System.Drawing.Point(258, 287);
            this.firstFriday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.firstFriday.Name = "firstFriday";
            this.firstFriday.Size = new System.Drawing.Size(112, 24);
            this.firstFriday.TabIndex = 1;
            this.firstFriday.Tag = "Friday";
            this.firstFriday.Text = "First Friday";
            this.firstFriday.UseVisualStyleBackColor = true;
            // 
            // move
            // 
            this.move.AutoSize = true;
            this.move.Enabled = false;
            this.move.Location = new System.Drawing.Point(66, 336);
            this.move.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(281, 24);
            this.move.TabIndex = 2;
            this.move.Text = "Move to Inbox (Hold shift to toggle)";
            this.move.UseVisualStyleBackColor = true;
            this.move.CheckedChanged += new System.EventHandler(this.move_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Subject:";
            // 
            // subjectText
            // 
            this.subjectText.AutoSize = true;
            this.subjectText.Location = new System.Drawing.Point(78, 48);
            this.subjectText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subjectText.Name = "subjectText";
            this.subjectText.Size = new System.Drawing.Size(51, 20);
            this.subjectText.TabIndex = 4;
            this.subjectText.Text = "label2";
            // 
            // addReminder
            // 
            this.addReminder.AutoSize = true;
            this.addReminder.Location = new System.Drawing.Point(66, 373);
            this.addReminder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addReminder.Name = "addReminder";
            this.addReminder.Size = new System.Drawing.Size(183, 24);
            this.addReminder.TabIndex = 5;
            this.addReminder.Text = "Add reminder at 9:00";
            this.addReminder.UseVisualStyleBackColor = true;
            // 
            // laterToday
            // 
            this.laterToday.AutoSize = true;
            this.laterToday.Location = new System.Drawing.Point(66, 122);
            this.laterToday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.laterToday.Name = "laterToday";
            this.laterToday.Size = new System.Drawing.Size(114, 24);
            this.laterToday.TabIndex = 6;
            this.laterToday.Tag = "0";
            this.laterToday.Text = "Later today";
            this.laterToday.UseVisualStyleBackColor = true;
            this.laterToday.CheckedChanged += new System.EventHandler(this.laterToday_CheckedChanged);
            // 
            // FollowUpForm
            // 
            this.AcceptButton = this.flag;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 494);
            this.Controls.Add(this.laterToday);
            this.Controls.Add(this.addReminder);
            this.Controls.Add(this.subjectText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.move);
            this.Controls.Add(this.firstFriday);
            this.Controls.Add(this.firstThursday);
            this.Controls.Add(this.firstWednesday);
            this.Controls.Add(this.firstTuesday);
            this.Controls.Add(this.firstMonday);
            this.Controls.Add(this.in5days);
            this.Controls.Add(this.in4days);
            this.Controls.Add(this.in3days);
            this.Controls.Add(this.in2days);
            this.Controls.Add(this.in1day);
            this.Controls.Add(this.none);
            this.Controls.Add(this.flag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FollowUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "The Great Followupper - by Wouter Van Ranst";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button flag;
        public System.Windows.Forms.RadioButton none;
        public System.Windows.Forms.RadioButton in1day;
        public System.Windows.Forms.RadioButton in2days;
        public System.Windows.Forms.RadioButton in3days;
        public System.Windows.Forms.RadioButton in4days;
        public System.Windows.Forms.RadioButton in5days;
        public System.Windows.Forms.RadioButton firstMonday;
        public System.Windows.Forms.RadioButton firstTuesday;
        public System.Windows.Forms.RadioButton firstWednesday;
        public System.Windows.Forms.RadioButton firstThursday;
        public System.Windows.Forms.RadioButton firstFriday;
        public System.Windows.Forms.CheckBox move;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label subjectText;
        public System.Windows.Forms.CheckBox addReminder;
        public System.Windows.Forms.RadioButton laterToday;
    }
}