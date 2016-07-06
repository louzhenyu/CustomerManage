using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    public class LogConsole
    {
        public static void PrintLog(string pMsg)
        {
            Task.Factory.StartNew(() =>
                HttpHelper.SendSoapRequest(DateTime.Now + "  " + pMsg, "http://182.254.242.12:56789/log/push/")
            );
        }
    }
}
