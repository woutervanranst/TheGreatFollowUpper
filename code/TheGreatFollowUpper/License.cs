using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheGreatFollowUpper
{
    internal static class License
    {
        public static bool IsLicenseValid()
        {
            if (HasLicense())
                return true;

            var r = InputBox.Show($"License for {GetUserId()}?", GreatFollowUpperAddin.Name);

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
            var userId = GetUserId();

            var requiredLicense = CalculateMD5Hash(userId + "TgfLicense");

            var includedLicenses = GetLicenses();

            if (!includedLicenses.Contains(requiredLicense))
                return false;

            return true;
        }

        private static string[] GetLicenses()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.Licenses))
                return new string[] {};

            return JArray.Parse(Properties.Settings.Default.Licenses).ToObject<string[]>();
        }

        private static void AddLicense(string l)
        {
            var ls = GetLicenses().ToList();
            ls.Add(l);

            Properties.Settings.Default.Licenses = JsonConvert.SerializeObject(ls);
            Properties.Settings.Default.Save();
        }

        private static string GetUserId()
        {
            // https://stackoverflow.com/questions/4761521/get-the-email-address-of-the-current-user-in-outlook-2007
            string emailAddr = "";
            var user = Globals.GreatFollowUpperAddin.Application.ActiveExplorer().Session.CurrentUser;
            if (user.AddressEntry.Type == "EX")
                //Exchange Account
                emailAddr = user.AddressEntry.GetExchangeUser().PrimarySmtpAddress;
            else
            //Other account
                emailAddr = user.Address;
            return emailAddr.ToLower();
        }

        private static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder hex = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
