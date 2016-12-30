using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApp.accountingwebservice;
using testApp.PayvastWS;

namespace testApp
{
    class WebServicesTools
    {
        dbManager dbManager;
        AccountingServiceSoapClient payvastWS;
        AccountingWebServiceSoapClient NezamSenfiWS;
        int fiscalPeriod;
        public WebServicesTools()
        {
            dbManager = new dbManager();
            payvastWS = new AccountingServiceSoapClient();
            NezamSenfiWS = new AccountingWebServiceSoapClient();
            fiscalPeriod = (int)dbManager.GetFiscalPeriod();
        }
        public void startImporting()
        {
            //ImportTemporaryUsers();
            ImportPermannentUsers();
            //ImportCooperativeUsers();
            //ImportProjectUsers();
        }

        #region tools
        List<string> toListOfString(string list)
        {
            return list.Replace("[", "").Replace("]", "").Replace("\"", "").Split(',').ToList();
        }
        List<int> toListOfInt(string list)
        {
            try
            {
                return toListOfString(list).Select(Int32.Parse).ToList();
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }
        void ImportPermannentUsers()
        {
            try
            {
                //int maxCode = (int)dbManager.GetMaxCode(ParantId.permanent, fiscalPeriod);
                int maxCode = 30616;

                string str_NewCods = NezamSenfiWS.GetNewMembersCodeByType(maxCode, MemberType.Permanent, Credentials.username, Credentials.password);
                List<int> NewCods = toListOfInt(str_NewCods);
                foreach (var code in NewCods)
                {
                    try
                    {
                        string str_memberInfo = NezamSenfiWS.GetMemberInfoByCode(code, MemberType.Permanent, Credentials.username, Credentials.password);
                        List<string> memberInfo = toListOfString(str_memberInfo);
                        string memberFullName = string.Format("{0} {1}", memberInfo[2], memberInfo[3]);
                        string res = payvastWS.AddFloatingAccountWithSpecifiedCode(ParantCode.permanent, code.ToString(), fiscalPeriod, memberFullName);
                        Console.WriteLine(res);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void ImportTemporaryUsers()
        {
            try
            {
                int maxCode = (int)dbManager.GetMaxCode(ParantId.temporary, fiscalPeriod);
                //int maxCode = 21650;

                string str_NewCods = NezamSenfiWS.GetNewMembersCodeByType(maxCode, MemberType.Temporary, Credentials.username, Credentials.password);
                List<int> NewCods = toListOfInt(str_NewCods);
                foreach (var code in NewCods)
                {
                    try
                    {
                        string str_memberInfo = NezamSenfiWS.GetMemberInfoByCode(code, MemberType.Temporary, Credentials.username, Credentials.password);
                        List<string> memberInfo = toListOfString(str_memberInfo);
                        string memberFullName = string.Format("{0} {1}", memberInfo[2], memberInfo[3]);
                        Console.WriteLine(memberFullName);
                        string res = payvastWS.AddFloatingAccountWithSpecifiedCode(ParantCode.temporary, code.ToString(), fiscalPeriod, memberFullName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        void ImportCooperativeUsers()
        {
            try
            {
                int maxCode = (int)dbManager.GetMaxCode(ParantId.cooperative, fiscalPeriod);
                //int maxCode = 824;

                string str_NewCods = NezamSenfiWS.GetNewCoCode(maxCode, Credentials.username, Credentials.password);
                List<int> NewCods = toListOfInt(str_NewCods);
                foreach (var code in NewCods)
                {
                    try
                    {
                        string str_memberInfo = NezamSenfiWS.GetCoInfoByCode(code, Credentials.username, Credentials.password);
                        List<string> memberInfo = toListOfString(str_memberInfo);
                        string memberFullName = string.Format("{0}_{1}", memberInfo[1], memberInfo[4]);
                        Console.WriteLine(memberFullName);
                        string res = payvastWS.AddFloatingAccountWithSpecifiedCode(ParantCode.cooperative, code.ToString(), fiscalPeriod, memberFullName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        void ImportProjectUsers()
        {
            try
            {
                int maxCode = (int)dbManager.GetMaxCode(ParantId.project, fiscalPeriod);
                //int maxCode = 41170;

                string str_NewCods = NezamSenfiWS.GetNewProjectsCode(maxCode, AgentId.shiraz, Credentials.username, Credentials.password);
                List<int> NewCods = toListOfInt(str_NewCods);
                foreach (var code in NewCods)
                {
                    try
                    {
                        string str_memberInfo = NezamSenfiWS.GetProjectInfo(code, Credentials.username, Credentials.password);
                        List<string> memberInfo = toListOfString(str_memberInfo);
                        string memberFullName = string.Format("{0}_{1}", memberInfo[2], memberInfo[6]);
                        Console.WriteLine(memberFullName);
                        string res = payvastWS.AddFloatingAccountWithSpecifiedCode(ParantCode.project, code.ToString(), fiscalPeriod, memberFullName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #region consts
        static class Credentials
        {
            public static readonly string username = "SNMFAccounting";
            public static readonly string password = "SNMFAccounting";
        }
        static class AgentId
        {
            public static readonly int shiraz = 1;
        }
        static class ParantId
        {
            public static readonly int permanent = 8;
            public static readonly int cooperative = 9;
            public static readonly int temporary = 11;
            public static readonly int project = 771;
        }
        static class ParantCode
        {
            public static readonly string permanent = "081";
            public static readonly string cooperative = "082";
            public static readonly string temporary = "083";
            public static readonly string project = "0771";
        }
        #endregion
    }
}
