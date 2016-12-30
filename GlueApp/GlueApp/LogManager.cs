using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GlueApp.Model;

namespace GlueApp
{
    class LogManager
    {
        public LogManager()
        {
            logs = new List<log>();
        }
        public static List<log> logs { get; set; }
        public void SaveLogs()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<log>));
            string fileName = string.Format("WSGlue_{0}.xml", DateTime.Now.ToString("yy_mm_dd_hh"));
            using (TextWriter textWriter = new StreamWriter(@fileName))
            {
                serializer.Serialize(textWriter, logs);
                logs = new List<log>();
            }
        }
    }
}
