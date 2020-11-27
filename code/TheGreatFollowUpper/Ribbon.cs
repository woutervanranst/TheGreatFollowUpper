using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using TheGreatFollowUpper.Extensions;
using TheGreatFollowUpper.Util;
using TheGreatFollowUpper.Views;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        //private Office.IRibbonUI ribbon;

        //public Ribbon()
        //{
        //}

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("TheGreatFollowUpper.Ribbon.xml");
        }
        private static string GetResourceText(string resourceName)
        {
            //if (!License.IsLicenseValid())
            //    return null;

            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }
        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226

        //public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        //{
        //    this.ribbon = ribbonUI;
        //}

        public void TaskContext_Click(dynamic sender)
        {
            var explorer = Globals.GreatFollowUpperAddin.Application.ActiveExplorer();
            if (explorer?.Selection == null)
                return;
            if (explorer.Selection.Count > 1 && Control.ModifierKeys == Keys.Shift) //Do not support multi selection and pop up dialog box
                return;


            foreach (object i in explorer.Selection)
            {
                FollowUpItem item;
                try
                {
                    item = FollowUpItem.GetFollowUpItem(i);
                }
                catch (RuntimeBinderException)
                {
                    MessageBox.Show("Type of a selected item not supported", GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }

                if (Control.ModifierKeys == Keys.Shift)
                {
                    var fupf = new FollowUpForm(item, FollowUpItem.FollowUpTrigger.Ribbon, sender.Tag);
                    fupf.Show();
                }
                else
                {
                    DateTime? followUpDate = Utils.ParseFollowUpDate(sender.Tag);
                    item.DoFollowUp(true, followUpDate, false, item.ReminderSet, false, false);
                }
            }

            


            //if (!License.IsLicenseValid())
            //    return;

            //var explorer = Globals.GreatFollowUpperAddin.Application.ActiveExplorer();
            //if (explorer?.Selection == null || explorer.Selection.Count != 1)
            //    return;

            //FollowUpItem item;
            //try
            //{
            //    item = FollowUpItem.GetFollowUpItem(explorer.Selection[1]);
            //}
            //catch (RuntimeBinderException)
            //{
            //    MessageBox.Show("Type of selected item not supported", GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            ////var mail = (Outlook.MailItem)item;

            //if (Control.ModifierKeys == Keys.Shift)
            //{
            //    var fupf = new FollowUpForm(item, FollowUpItem.FollowUpTrigger.Ribbon, sender.Tag);
            //    fupf.Show();
            //}
            //else
            //{
            //    DateTime? followUpDate = Utils.ParseFollowUpDate(sender.Tag);
            //    item.DoFollowUp(true, followUpDate, false, item.ReminderSet, false, false);
            //}
        }

        #endregion
    }
}
