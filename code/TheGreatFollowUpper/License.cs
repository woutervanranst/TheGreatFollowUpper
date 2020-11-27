using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheGreatFollowUpper.Properties;
using TheGreatFollowUpper.Util;
using Outlook = Microsoft.Office.Interop.Outlook;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TheGreatFollowUpper.LicenseGenerator")]
namespace TheGreatFollowUpper
{
    internal static class License
    {
        public static bool IsLicenseValid()
        {
            if (HasLicense())
                return true;

            var r = InputBox.Show($"License for {GetAccounts().First()}?", GreatFollowUpperAddin.Name);

            if (r.ReturnCode != DialogResult.OK)
                return false;

            AddLicense(r.Text);

            var lic = HasLicense();
            if (lic)
                MessageBox.Show("Thanks, enjoy!", GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Incorrect license key", GreatFollowUpperAddin.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);

            return lic;
        }


        private static bool HasLicense()
        {
            var accounts = GetAccounts();
            var includedLicenses = GetLicenses();

            if (!accounts.Any(account => includedLicenses.Contains(GenerateRequiredLicense(account))))
                return false;

            return true;
        }

        internal static string GenerateRequiredLicense(string account)
        {
            return CalculateMD5Hash(account + "TgfLicense");
        }

        private static string[] GetLicenses()
        {
            if (string.IsNullOrEmpty(Settings.Default.Licenses))
                return new string[] {};

            return JArray.Parse(Settings.Default.Licenses).ToObject<string[]>();
        }

        private static void AddLicense(string l)
        {
            var ls = GetLicenses().ToList();
            ls.Add(l);

            Settings.Default.Licenses = JsonConvert.SerializeObject(ls);
            Settings.Default.Save();
        }

        //private static string GetUserId()
        //{
        //    // https://stackoverflow.com/questions/4761521/get-the-email-address-of-the-current-user-in-outlook-2007
        //    string emailAddr = "";
        //    var user = Globals.GreatFollowUpperAddin.Application.ActiveExplorer().Session.CurrentUser;
        //    if (user.AddressEntry.Type == "EX")
        //    {
        //        //Exchange Account
        //        //emailAddr = user.AddressEntry.GetExchangeUser().PrimarySmtpAddress;
        //        var kak = user.AddressEntry.GetExchangeUser().GetExchangeUserManager().PrimarySmtpAddress;
        //        var currentUser = Globals.GreatFollowUpperAddin.Application.Session.CurrentUser.AddressEntry.GetExchangeUser();
        //        emailAddr = currentUser.PrimarySmtpAddress;
        //    }
        //    else
        //        //Other account
        //        emailAddr = user.Address;
        //    return emailAddr.ToLower();
        //}

        private static List<string> GetAccounts()
        {
            // https://social.msdn.microsoft.com/Forums/vstudio/en-US/d2d9bf23-e27f-4f30-9199-42833d6919a4/email-address-of-the-current-user?forum=vsto
            var accounts = new Linqqer<Outlook.Accounts, Outlook.Account>(Globals.GreatFollowUpperAddin.Application.ActiveExplorer().Session.Accounts);
            return accounts.Select(a => a.SmtpAddress).ToList();
        }

        private static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder hex = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
