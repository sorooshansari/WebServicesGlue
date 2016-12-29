using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServicesTools wsTools = new WebServicesTools();
            wsTools.startImporting();
            var a = Console.ReadLine();
        }
    }
}
