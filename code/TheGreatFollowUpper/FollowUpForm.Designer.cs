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
            this.SuspendLayout();
            // 
            // flag
            // 
            this.flag.Location = new System.Drawing.Point(109, 279);
            this.flag.Name = "flag";
            this.flag.Size = new System.Drawing.Size(75, 23);
            this.flag.TabIndex = 0;
            this.flag.Text = "&OK";
            this.flag.UseVisualStyleBackColor = true;
            this.flag.Click += new System.EventHandler(this.flag_Click);
            // 
            // none
            // 
            this.none.AutoSize = true;
            this.none.Checked = true;
            this.none.Location = new System.Drawing.Point(44, 58);
            this.none.Name = "none";
            this.none.Size = new System.Drawing.Size(95, 17);
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
            this.in1day.Location = new System.Drawing.Point(44, 81);
            this.in1day.Name = "in1day";
            this.in1day.Size = new System.Drawing.Size(107, 17);
            this.in1day.TabIndex = 1;
            this.in1day.Tag = "1";
            this.in1day.Text = "In 1 business day";
            this.in1day.UseVisualStyleBackColor = true;
            // 
            // in2days
            // 
            this.in2days.AutoSize = true;
            this.in2days.Location = new System.Drawing.Point(44, 104);
            this.in2days.Name = "in2days";
            this.in2days.Size = new System.Drawing.Size(112, 17);
            this.in2days.TabIndex = 1;
            this.in2days.Tag = "2";
            this.in2days.Text = "In 2 business days";
            this.in2days.UseVisualStyleBackColor = true;
            // 
            // in3days
            // 
            this.in3days.AutoSize = true;
            this.in3days.Location = new System.Drawing.Point(44, 127);
            this.in3days.Name = "in3days";
            this.in3days.Size = new System.Drawing.Size(112, 17);
            this.in3days.TabIndex = 1;
            this.in3days.Tag = "3";
            this.in3days.Text = "In 3 business days";
            this.in3days.UseVisualStyleBackColor = true;
            // 
            // in4days
            // 
            this.in4days.AutoSize = true;
            this.in4days.Location = new System.Drawing.Point(44, 150);
            this.in4days.Name = "in4days";
            this.in4days.Size = new System.Drawing.Size(112, 17);
            this.in4days.TabIndex = 1;
            this.in4days.Tag = "4";
            this.in4days.Text = "In 4 business days";
            this.in4days.UseVisualStyleBackColor = true;
            // 
            // in5days
            // 
            this.in5days.AutoSize = true;
            this.in5days.Location = new System.Drawing.Point(44, 173);
            this.in5days.Name = "in5days";
            this.in5days.Size = new System.Drawing.Size(112, 17);
            this.in5days.TabIndex = 1;
            this.in5days.Tag = "5";
            this.in5days.Text = "In 5 business days";
            this.in5days.UseVisualStyleBackColor = true;
            // 
            // firstMonday
            // 
            this.firstMonday.AutoSize = true;
            this.firstMonday.Location = new System.Drawing.Point(172, 81);
            this.firstMonday.Name = "firstMonday";
            this.firstMonday.Size = new System.Drawing.Size(85, 17);
            this.firstMonday.TabIndex = 1;
            this.firstMonday.Tag = "Monday";
            this.firstMonday.Text = "First Monday";
            this.firstMonday.UseVisualStyleBackColor = true;
            // 
            // firstTuesday
            // 
            this.firstTuesday.AutoSize = true;
            this.firstTuesday.Location = new System.Drawing.Point(172, 104);
            this.firstTuesday.Name = "firstTuesday";
            this.firstTuesday.Size = new System.Drawing.Size(88, 17);
            this.firstTuesday.TabIndex = 1;
            this.firstTuesday.Tag = "Tuesday";
            this.firstTuesday.Text = "First Tuesday";
            this.firstTuesday.UseVisualStyleBackColor = true;
            // 
            // firstWednesday
            // 
            this.firstWednesday.AutoSize = true;
            this.firstWednesday.Location = new System.Drawing.Point(172, 127);
            this.firstWednesday.Name = "firstWednesday";
            this.firstWednesday.Size = new System.Drawing.Size(104, 17);
            this.firstWednesday.TabIndex = 1;
            this.firstWednesday.Tag = "Wednesday";
            this.firstWednesday.Text = "First Wednesday";
            this.firstWednesday.UseVisualStyleBackColor = true;
            // 
            // firstThursday
            // 
            this.firstThursday.AutoSize = true;
            this.firstThursday.Location = new System.Drawing.Point(172, 150);
            this.firstThursday.Name = "firstThursday";
            this.firstThursday.Size = new System.Drawing.Size(91, 17);
            this.firstThursday.TabIndex = 1;
            this.firstThursday.Tag = "Thursday";
            this.firstThursday.Text = "First Thursday";
            this.firstThursday.UseVisualStyleBackColor = true;
            // 
            // firstFriday
            // 
            this.firstFriday.AutoSize = true;
            this.firstFriday.Location = new System.Drawing.Point(172, 173);
            this.firstFriday.Name = "firstFriday";
            this.firstFriday.Size = new System.Drawing.Size(75, 17);
            this.firstFriday.TabIndex = 1;
            this.firstFriday.Tag = "Friday";
            this.firstFriday.Text = "First Friday";
            this.firstFriday.UseVisualStyleBackColor = true;
            // 
            // move
            // 
            this.move.AutoSize = true;
            this.move.Enabled = false;
            this.move.Location = new System.Drawing.Point(44, 205);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(94, 17);
            this.move.TabIndex = 2;
            this.move.Text = "Move to Inbox";
            this.move.UseVisualStyleBackColor = true;
            this.move.CheckedChanged += new System.EventHandler(this.move_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Subject:";
            // 
            // subjectText
            // 
            this.subjectText.AutoSize = true;
            this.subjectText.Location = new System.Drawing.Point(52, 31);
            this.subjectText.Name = "subjectText";
            this.subjectText.Size = new System.Drawing.Size(35, 13);
            this.subjectText.TabIndex = 4;
            this.subjectText.Text = "label2";
            // 
            // addReminder
            // 
            this.addReminder.AutoSize = true;
            this.addReminder.Location = new System.Drawing.Point(44, 229);
            this.addReminder.Name = "addReminder";
            this.addReminder.Size = new System.Drawing.Size(124, 17);
            this.addReminder.TabIndex = 5;
            this.addReminder.Text = "Add reminder at 9:00";
            this.addReminder.UseVisualStyleBackColor = true;
            // 
            // FollowUpForm
            // 
            this.AcceptButton = this.flag;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 314);
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
    }
}