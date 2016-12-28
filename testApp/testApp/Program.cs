using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApp.accountingwebservice;

namespace testApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountingWebServiceSoapClient client = new AccountingWebServiceSoapClient();
            string res = client.GetNewMembersCodeByType(10, MemberType.Permanent, "SNMFAccounting", "SNMFAccounting");
            Console.WriteLine(res);
        }
    }
}
