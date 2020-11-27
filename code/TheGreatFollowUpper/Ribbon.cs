using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.


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
            if (!License.IsLicenseValid())
                return;

            var explorer = Globals.GreatFollowUpperAddin.Application.ActiveExplorer();
            if (explorer?.Selection == null || explorer.Selection.Count <= 0)
                return;

            if (explorer.Selection.Count > 1)
                return;

            var item = explorer.Selection[1];
            if (!(item is Outlook.MailItem))
            {
                MessageBox.Show(@"Type of selected item not supported", GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var mail = (Outlook.MailItem)item;

            if (Control.ModifierKeys == Keys.Shift)
            {
                var fupf = new FollowUpForm(mail, sender.Tag);
                fupf.Show();
            }
            else
            {
                DateTime? followUpDate = MailItemExtensions.ParseFollowUpDate(sender.Tag);
                mail.TreatOutgoing(true, followUpDate, false, false, false, false, null);
            }
            
            //var x = CategoryPA.InvokeRequestResponseService(mail).Result;

            //Helper.GetConversationInformation(mail);
        }

        #endregion
    }
}
