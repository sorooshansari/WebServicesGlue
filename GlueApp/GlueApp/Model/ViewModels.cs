using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlueApp.Model
{
    class ViewModels
    {
    }
    public class log
    {
        public log(bool isError, string message)
        {
            this.isError = isError;
            this.message = message;
            this.dateTime = DateTime.Now.ToString();
        }
        public bool isError { get; set; }
        public string dateTime { get; set; }
        public string message { get; set; }
    }
}
