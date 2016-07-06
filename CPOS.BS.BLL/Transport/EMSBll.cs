using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.Transport;
using CPOS.Common;
using System.Collections;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.BLL.Transport
{
    public class EMSBll : ITransportHandler
    {
        private string WaybillNo = string.Empty;
        private string Url = "http://211.156.193.140:8000/cotrackapi/api/track/mail/{0}";
        public EMSBll(string pWaybillNo)
        {
            this.WaybillNo = pWaybillNo;
        }
        public List<TransportEntity> SearchTransport()
        {
            string apiUrl = string.Format(Url, WaybillNo);
            Hashtable ht = new Hashtable();
            string result = HttpHelper.GetSoapRequest(string.Empty, apiUrl, 5, ht);
            EMSEntity res = result.DeserializeJSONTo<EMSEntity>();
            List<TransportEntity> listResult = new List<TransportEntity>();
            if (true)
            {
                res.traces.ForEach(t =>
                {
                    listResult.Add(new TransportEntity()
                    {
                        AcceptAddress = t.acceptAddress,
                        AcceptTime = t.acceptTime,
                        Remark = t.remark,
                        State = ""
                    });
                });
            }


            return listResult;
        }
    }
}
