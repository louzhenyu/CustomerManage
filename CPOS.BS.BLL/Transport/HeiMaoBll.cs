using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPOS.Common;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity.Transport;

namespace JIT.CPOS.BS.BLL.Transport
{
    public class HeiMaoBll : ITransportHandler
    {
        private readonly string Cid = "DL";
        private string WaybillNo = string.Empty;
        public HeiMaoBll(string pWaybillNo)
        {
            this.WaybillNo = pWaybillNo;
        }

        /// <summary>
        /// 获取物流信息
        /// </summary>
        /// <param name="pWaybillNo">运单号</param>
        /// <returns></returns>
        public List<TransportEntity> SearchTransport()
        {
            return null;
            HttpHelper.PostData("", "http://dms.ta-q-bin.com.cn:8080/ServiceQuery.asmx/GetWaybills?cid=" + Cid + "&noFlag=0&historyFlag=2&no=" + WaybillNo);
        }
    }
}
