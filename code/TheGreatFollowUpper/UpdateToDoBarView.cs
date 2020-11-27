using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatFollowUpper
{
    internal class UpdateToDoBarView
    {
        private void UpdateToDoBarViewMethod()
        {
            var x =
                GreatFollowUpperAddin.InboxFolder.Store.GetSpecialFolder(
                    Microsoft.Office.Interop.Outlook.OlSpecialFolders.olSpecialFolderAllTasks);
            foreach (Microsoft.Office.Interop.Outlook.TableView view in x.Views)
            {
                if (view.Name == "To-Do List")
                {
                    //Outlook.OrderFields ofs = ((dynamic) view).GroupByFields;
                    Microsoft.Office.Interop.Outlook.OrderFields ofs = view.GroupByFields;

                    view.GroupByFields.RemoveAll();
                    view.Apply();
                    view.Save();

                    //view.GroupByFields.Add("http://schemas.microsoft.com/mapi/id/{00062008-0000-0000-C000-000000000046}/8539101f", false);
                    //view.Save();

                    //http://schemas.microsoft.com/mapi/id/{00062008-0000-0000-C000-000000000046}/8539101f

                    //foreach (Outlook.OrderField orderField in ofs)
                    //{
                    //    var x = orderField.ViewXMLSchemaName;
                    //}
                    //var vv = v.GroupByFields

                    //var vv = (Outlook.TableView)toDoFolder.CurrentView;
                    //foreach (Outlook. vvvv in ((dynamic)x).GroupByFields)
                    //{
                    //    var vvv = vvvv.Name;
                    //}

                }
                //Outlook.OutlookBarStorage zzz = x.Contents;
                //foreach (Outlook.OutlookBarGroup abc in zzz.Groups)
                //{
                //    var z = abc.Name;
                //}
            }
        }
    }
}
