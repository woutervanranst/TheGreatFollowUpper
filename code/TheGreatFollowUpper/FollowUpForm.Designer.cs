using Microsoft.Office.Interop.Outlook;
using System;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FollowUpForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.followUpOptions = new System.Windows.Forms.GroupBox();
            this.clearFlagOnReply = new System.Windows.Forms.CheckBox();
            this.laterToday = new System.Windows.Forms.RadioButton();
            this.addReminder = new System.Windows.Forms.CheckBox();
            this.move = new System.Windows.Forms.CheckBox();
            this.firstFriday = new System.Windows.Forms.RadioButton();
            this.firstThursday = new System.Windows.Forms.RadioButton();
            this.firstWednesday = new System.Windows.Forms.RadioButton();
            this.firstTuesday = new System.Windows.Forms.RadioButton();
            this.firstMonday = new System.Windows.Forms.RadioButton();
            this.in5days = new System.Windows.Forms.RadioButton();
            this.in4days = new System.Windows.Forms.RadioButton();
            this.in3days = new System.Windows.Forms.RadioButton();
            this.in2days = new System.Windows.Forms.RadioButton();
            this.in1day = new System.Windows.Forms.RadioButton();
            this.none = new System.Windows.Forms.RadioButton();
            this.mail = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNewCategory = new System.Windows.Forms.Button();
            this.categoriesListBox = new TheGreatFollowUpper.ColoredCheckedListBox();
            this.followUpOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(392, 370);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(15, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // followUpOptions
            // 
            this.followUpOptions.Controls.Add(this.clearFlagOnReply);
            this.followUpOptions.Controls.Add(this.laterToday);
            this.followUpOptions.Controls.Add(this.addReminder);
            this.followUpOptions.Controls.Add(this.move);
            this.followUpOptions.Controls.Add(this.firstFriday);
            this.followUpOptions.Controls.Add(this.firstThursday);
            this.followUpOptions.Controls.Add(this.firstWednesday);
            this.followUpOptions.Controls.Add(this.firstTuesday);
            this.followUpOptions.Controls.Add(this.firstMonday);
            this.followUpOptions.Controls.Add(this.in5days);
            this.followUpOptions.Controls.Add(this.in4days);
            this.followUpOptions.Controls.Add(this.in3days);
            this.followUpOptions.Controls.Add(this.in2days);
            this.followUpOptions.Controls.Add(this.in1day);
            this.followUpOptions.Controls.Add(this.none);
            this.followUpOptions.Location = new System.Drawing.Point(15, 39);
            this.followUpOptions.Name = "followUpOptions";
            this.followUpOptions.Size = new System.Drawing.Size(425, 305);
            this.followUpOptions.TabIndex = 8;
            this.followUpOptions.TabStop = false;
            this.followUpOptions.Text = "Follow Up";
            // 
            // clearFlagOnReply
            // 
            this.clearFlagOnReply.AutoSize = true;
            this.clearFlagOnReply.Enabled = false;
            this.clearFlagOnReply.Location = new System.Drawing.Point(7, 278);
            this.clearFlagOnReply.Margin = new System.Windows.Forms.Padding(4);
            this.clearFlagOnReply.Name = "clearFlagOnReply";
            this.clearFlagOnReply.Size = new System.Drawing.Size(235, 21);
            this.clearFlagOnReply.TabIndex = 21;
            this.clearFlagOnReply.Text = "Clear flag when receiving a reply";
            this.clearFlagOnReply.UseVisualStyleBackColor = true;
            // 
            // laterToday
            // 
            this.laterToday.AutoSize = true;
            this.laterToday.Location = new System.Drawing.Point(7, 49);
            this.laterToday.Margin = new System.Windows.Forms.Padding(4);
            this.laterToday.Name = "laterToday";
            this.laterToday.Size = new System.Drawing.Size(101, 21);
            this.laterToday.TabIndex = 20;
            this.laterToday.Tag = 0;
            this.laterToday.Text = "Later today";
            this.laterToday.UseVisualStyleBackColor = true;
            this.laterToday.CheckedChanged += new System.EventHandler(this.laterToday_CheckedChanged);
            // 
            // addReminder
            // 
            this.addReminder.AutoSize = true;
            this.addReminder.Enabled = false;
            this.addReminder.Location = new System.Drawing.Point(7, 249);
            this.addReminder.Margin = new System.Windows.Forms.Padding(4);
            this.addReminder.Name = "addReminder";
            this.addReminder.Size = new System.Drawing.Size(163, 21);
            this.addReminder.TabIndex = 19;
            this.addReminder.Text = "Add reminder at 9:00";
            this.addReminder.UseVisualStyleBackColor = true;
            // 
            // move
            // 
            this.move.AutoSize = true;
            this.move.Enabled = false;
            this.move.Location = new System.Drawing.Point(7, 220);
            this.move.Margin = new System.Windows.Forms.Padding(4);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(249, 21);
            this.move.TabIndex = 18;
            this.move.Text = "Move to Inbox (Hold shift to toggle)";
            this.move.UseVisualStyleBackColor = true;
            this.move.CheckedChanged += new System.EventHandler(this.move_CheckedChanged);
            // 
            // firstFriday
            // 
            this.firstFriday.AutoSize = true;
            this.firstFriday.Location = new System.Drawing.Point(211, 181);
            this.firstFriday.Margin = new System.Windows.Forms.Padding(4);
            this.firstFriday.Name = "firstFriday";
            this.firstFriday.Size = new System.Drawing.Size(99, 21);
            this.firstFriday.TabIndex = 7;
            this.firstFriday.Tag = System.DayOfWeek.Friday;
            this.firstFriday.Text = "First Friday";
            this.firstFriday.UseVisualStyleBackColor = true;
            // 
            // firstThursday
            // 
            this.firstThursday.AutoSize = true;
            this.firstThursday.Location = new System.Drawing.Point(211, 154);
            this.firstThursday.Margin = new System.Windows.Forms.Padding(4);
            this.firstThursday.Name = "firstThursday";
            this.firstThursday.Size = new System.Drawing.Size(120, 21);
            this.firstThursday.TabIndex = 8;
            this.firstThursday.Tag = System.DayOfWeek.Thursday;
            this.firstThursday.Text = "First Thursday";
            this.firstThursday.UseVisualStyleBackColor = true;
            // 
            // firstWednesday
            // 
            this.firstWednesday.AutoSize = true;
            this.firstWednesday.Location = new System.Drawing.Point(211, 128);
            this.firstWednesday.Margin = new System.Windows.Forms.Padding(4);
            this.firstWednesday.Name = "firstWednesday";
            this.firstWednesday.Size = new System.Drawing.Size(135, 21);
            this.firstWednesday.TabIndex = 9;
            this.firstWednesday.Tag = System.DayOfWeek.Wednesday;
            this.firstWednesday.Text = "First Wednesday";
            this.firstWednesday.UseVisualStyleBackColor = true;
            // 
            // firstTuesday
            // 
            this.firstTuesday.AutoSize = true;
            this.firstTuesday.Location = new System.Drawing.Point(211, 101);
            this.firstTuesday.Margin = new System.Windows.Forms.Padding(4);
            this.firstTuesday.Name = "firstTuesday";
            this.firstTuesday.Size = new System.Drawing.Size(115, 21);
            this.firstTuesday.TabIndex = 10;
            this.firstTuesday.Tag = System.DayOfWeek.Tuesday;
            this.firstTuesday.Text = "First Tuesday";
            this.firstTuesday.UseVisualStyleBackColor = true;
            // 
            // firstMonday
            // 
            this.firstMonday.AutoSize = true;
            this.firstMonday.Location = new System.Drawing.Point(211, 75);
            this.firstMonday.Margin = new System.Windows.Forms.Padding(4);
            this.firstMonday.Name = "firstMonday";
            this.firstMonday.Size = new System.Drawing.Size(110, 21);
            this.firstMonday.TabIndex = 11;
            this.firstMonday.Tag = System.DayOfWeek.Monday;
            this.firstMonday.Text = "First Monday";
            this.firstMonday.UseVisualStyleBackColor = true;
            // 
            // in5days
            // 
            this.in5days.AutoSize = true;
            this.in5days.Location = new System.Drawing.Point(7, 181);
            this.in5days.Margin = new System.Windows.Forms.Padding(4);
            this.in5days.Name = "in5days";
            this.in5days.Size = new System.Drawing.Size(146, 21);
            this.in5days.TabIndex = 12;
            this.in5days.Tag = 5;
            this.in5days.Text = "In 5 business days";
            this.in5days.UseVisualStyleBackColor = true;
            // 
            // in4days
            // 
            this.in4days.AutoSize = true;
            this.in4days.Location = new System.Drawing.Point(7, 154);
            this.in4days.Margin = new System.Windows.Forms.Padding(4);
            this.in4days.Name = "in4days";
            this.in4days.Size = new System.Drawing.Size(146, 21);
            this.in4days.TabIndex = 13;
            this.in4days.Tag = 4;
            this.in4days.Text = "In 4 business days";
            this.in4days.UseVisualStyleBackColor = true;
            // 
            // in3days
            // 
            this.in3days.AutoSize = true;
            this.in3days.Location = new System.Drawing.Point(7, 128);
            this.in3days.Margin = new System.Windows.Forms.Padding(4);
            this.in3days.Name = "in3days";
            this.in3days.Size = new System.Drawing.Size(146, 21);
            this.in3days.TabIndex = 14;
            this.in3days.Tag = 3;
            this.in3days.Text = "In 3 business days";
            this.in3days.UseVisualStyleBackColor = true;
            // 
            // in2days
            // 
            this.in2days.AutoSize = true;
            this.in2days.Location = new System.Drawing.Point(7, 101);
            this.in2days.Margin = new System.Windows.Forms.Padding(4);
            this.in2days.Name = "in2days";
            this.in2days.Size = new System.Drawing.Size(146, 21);
            this.in2days.TabIndex = 15;
            this.in2days.Tag = 2;
            this.in2days.Text = "In 2 business days";
            this.in2days.UseVisualStyleBackColor = true;
            // 
            // in1day
            // 
            this.in1day.AutoSize = true;
            this.in1day.Location = new System.Drawing.Point(7, 75);
            this.in1day.Margin = new System.Windows.Forms.Padding(4);
            this.in1day.Name = "in1day";
            this.in1day.Size = new System.Drawing.Size(139, 21);
            this.in1day.TabIndex = 16;
            this.in1day.Tag = 1;
            this.in1day.Text = "In 1 business day";
            this.in1day.UseVisualStyleBackColor = true;
            // 
            // none
            // 
            this.none.AutoSize = true;
            this.none.Checked = true;
            this.none.Location = new System.Drawing.Point(7, 22);
            this.none.Margin = new System.Windows.Forms.Padding(4);
            this.none.Name = "none";
            this.none.Size = new System.Drawing.Size(121, 21);
            this.none.TabIndex = 17;
            this.none.TabStop = true;
            this.none.Text = "Don\'t follow up";
            this.none.UseVisualStyleBackColor = true;
            this.none.CheckedChanged += new System.EventHandler(this.none_CheckedChanged);
            // 
            // mail
            // 
            this.mail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mail.Location = new System.Drawing.Point(12, 9);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(859, 18);
            this.mail.TabIndex = 9;
            this.mail.TabStop = true;
            this.mail.Text = "linkLabel1";
            this.mail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mail_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNewCategory);
            this.groupBox1.Controls.Add(this.categoriesListBox);
            this.groupBox1.Location = new System.Drawing.Point(446, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 305);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Categories";
            // 
            // btnNewCategory
            // 
            this.btnNewCategory.Location = new System.Drawing.Point(344, 276);
            this.btnNewCategory.Name = "btnNewCategory";
            this.btnNewCategory.Size = new System.Drawing.Size(75, 23);
            this.btnNewCategory.TabIndex = 1;
            this.btnNewCategory.Text = "New";
            this.btnNewCategory.UseVisualStyleBackColor = true;
            this.btnNewCategory.Click += new System.EventHandler(this.btnNewCategory_Click);
            // 
            // categoriesListBox
            // 
            this.categoriesListBox.CheckOnClick = true;
            this.categoriesListBox.Location = new System.Drawing.Point(6, 21);
            this.categoriesListBox.Name = "categoriesListBox";
            this.categoriesListBox.Size = new System.Drawing.Size(413, 242);
            this.categoriesListBox.TabIndex = 0;
            // 
            // FollowUpForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(881, 418);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mail);
            this.Controls.Add(this.followUpOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FollowUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Great Followupper - by Wouter Van Ranst";
            this.followUpOptions.ResumeLayout(false);
            this.followUpOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox followUpOptions;
        private System.Windows.Forms.RadioButton laterToday;
        private System.Windows.Forms.CheckBox addReminder;
        private System.Windows.Forms.CheckBox move;
        private System.Windows.Forms.RadioButton firstFriday;
        private System.Windows.Forms.RadioButton firstThursday;
        private System.Windows.Forms.RadioButton firstWednesday;
        private System.Windows.Forms.RadioButton firstTuesday;
        private System.Windows.Forms.RadioButton firstMonday;
        private System.Windows.Forms.RadioButton in5days;
        private System.Windows.Forms.RadioButton in4days;
        private System.Windows.Forms.RadioButton in3days;
        private System.Windows.Forms.RadioButton in2days;
        private System.Windows.Forms.RadioButton in1day;
        private System.Windows.Forms.RadioButton none;
        private System.Windows.Forms.LinkLabel mail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNewCategory;
        private System.Windows.Forms.CheckBox clearFlagOnReply;
        private ColoredCheckedListBox categoriesListBox;
    }
}