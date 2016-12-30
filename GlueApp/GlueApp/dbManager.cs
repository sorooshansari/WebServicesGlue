using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlueApp
{
    class dbManager
    {
        string connectionString =
            "Data Source=W12SRV-MALI;Initial Catalog=NMF13950913;Password=hslhk hfd kvlhtchv;Persist Security Info=True;User ID=sbsc;";
        List<DataTable> ExecSqlCommand(string SP, params SqlParameter[] parameters)
        {
            var res = new List<DataTable>();
            using (var con = new SqlConnection(connectionString))
            {
                string paramList = "";
                foreach (var p in parameters)
                    paramList += string.Format(" {0}", p.ParameterName);
                var command = string.Format("exec {0}{1}", SP, paramList);
                con.Open();
                using (SqlCommand cmd = new SqlCommand(command, con))
                {
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);
                    var dataReader = cmd.ExecuteReader();
                    while (!dataReader.IsClosed)
                    {
                        var dt = new DataTable();
                        dt.Load(dataReader);
                        res.Add(dt);
                    }
                }
            }
            return res;
        }
        int intResultSP(string SP, params SqlParameter[] parameters)
        {
            var dataTable = ExecSqlCommand(SP, parameters)[0];
            return dataTable.AsEnumerable().Select(x => x.Field<Int16>("res")).FirstOrDefault();
        }
        double doubleResultSP(string SP, params SqlParameter[] parameters)
        {
            var dataTable = ExecSqlCommand(SP, parameters)[0];
            return dataTable.AsEnumerable().Select(x => x.Field<double>("res")).FirstOrDefault();
        }
        public double GetMaxCode(int ParentId)
        {
            var SP1 = new SqlParameter("@ParentId", ParentId);
            return doubleResultSP("[dbo].[__WsMaxCode__]", SP1);
        }
        public int GetFiscalPeriod()
        {
            return intResultSP("[dbo].[__WsFisclPeriod__]");
        }
    }
}
