using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TheGreatFollowUpper
{
    internal static class CategoryPA
    {
        public static async Task<string> InvokeRequestResponseService(Outlook.MailItem mail)
            {
                using (var client = new HttpClient())
                {
                    var scoreRequest = new
                    {
                        Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "Subject", mail.Subject
                                            },
                                            {
                                                "From: (Name)", ""
                                            },
                                            {
                                                "From: (Address)", ""
                                            },
                                            {
                                                "From: (Type)", ""
                                            },
                                            {
                                                "To: (Name)", string.Join(";", mail.Recipients.Cast<Outlook.Recipient>().Select(r => r.Name))
                                            },
                                            {
                                                "To: (Address)", ""
                                            },
                                            {
                                                "To: (Type)", ""
                                            },
                                            {
                                                "CC: (Name)", ""
                                            },
                                            {
                                                "CC: (Address)", ""
                                            },
                                            {
                                                "CC: (Type)", ""
                                            },
                                            {
                                                "BCC: (Name)", ""
                                            },
                                            {
                                                "BCC: (Address)", ""
                                            },
                                            {
                                                "BCC: (Type)", ""
                                            },
                                            {
                                                "Billing Information", ""
                                            },
                                            {
                                                "Categories", ""
                                            },
                                            {
                                                "Importance", ""
                                            },
                                            {
                                                "Sensitivity", ""
                                            },
                                }
                            }
                        },
                    },
                        GlobalParameters = new Dictionary<string, string>()
                        {
                        }
                    };

                    const string apiKey = "EwZJWsfMREbiAYDWrkqqFKoA2tY7VFWQ7vc9wm26XbNrT6ZW8vcCWOLjTHcwGTghmVorFnXzqF4hH6Rxn8wt3Q=="; // Replace this with the API key for the web service
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/60a9dcf9aedf45409dd8b76cc05a620e/services/3edf4bde2e2a4ee48523597e3e9c769c/execute?api-version=2.0&format=swagger");

                    // WARNING: The 'await' statement below can result in a deadlock
                    // if you are calling this code from the UI thread of an ASP.Net application.
                    // One way to address this would be to call ConfigureAwait(false)
                    // so that the execution does not attempt to resume on the original context.
                    // For instance, replace code such as:
                    //      result = await DoSomeTask()
                    // with the following:
                    //      result = await DoSomeTask().ConfigureAwait(false)

                    HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        return result;
                        //Console.WriteLine("Result: {0}", result);
                    }
                    else
                    {
                        return "";
                        Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                        // Print the headers - they include the requert ID and the timestamp,
                        // which are useful for debugging the failure
                        Console.WriteLine(response.Headers.ToString());

                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                }
            }
    }
}
