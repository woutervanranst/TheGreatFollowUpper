using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper
{
    internal static class TaskDumper
    {
        public static void TaskDump()
        {
            try
            {
                //var f = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderToDo);

                var ts = GreatFollowUpperAddin.ToDoFolder.Items.Cast<dynamic>().Where(t => IsActiveToDo(t)).Select(t => $"[{t.EntryID}] - {t.Subject}");

                var p = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"The Great FollowUpper - ToDo {DateTime.Now:yyyyMMdd-HHmmss}.txt");

                File.WriteAllLines(p, ts);
            }
            catch (Exception e)
            {
                //Silently exit because otherwise it makes the application go boom
                MessageBox.Show($"Error when dumping ToDos to file:\n{e.ToString()}", GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsActiveToDo(dynamic t)
        {
            try
            {
                return
                    (t is Outlook.TaskItem && !t.Complete) ||
                    (t is Outlook.MailItem && t.FlagStatus == (int)Outlook.OlFlagStatus.olFlagMarked);
            }
            catch (Exception)
            {
                //For encrypted msgs it throws an error
                return false;
            }
        }
    }
}
