using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatFollowUpper.LicenseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Email? ");
            var account = Console.ReadLine();

            var l = TheGreatFollowUpper.License.GenerateRequiredLicense(account);

            Console.WriteLine(l);

            Console.ReadLine();
            
        }
    }
}
