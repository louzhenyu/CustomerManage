using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CPOS.Common;
using JIT.CPOS.BS.Entity.Transport;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.BLL.Transport
{
    public class YunDaBll : ITransportHandler
    {
        private readonly string partnerid = "2012476666";
        private readonly string version = "1.4";
        private readonly string pwd = "";
        private readonly string url = "interface_transite_search.php";
        private string WaybillNo = string.Empty;

        public YunDaBll(string pWaybillNo)
        {
            this.WaybillNo = pWaybillNo;
        }

        /// <summary>
        /// 获取物流信息
        /// </summary>
        /// <returns></returns>
        public List<TransportEntity> SearchTransport()
        {
            string xmlData = string.Format("<mailnos><mailno>{0}</mailno></mailnos>", WaybillNo);
            string postData = string.Empty;

            string base64 = Convert.ToBase64String(xmlData.GetBytes());
            string validation = MD5Helper.Encryption(xmlData + partnerid + pwd).ToLower();

            postData = string.Format("partnerid={0}&version={1}&request={2}&xmldata={3}&validation={4}"
                , partnerid, HttpUtility.UrlEncode(version, Encoding.UTF8), "", HttpUtility.UrlEncode(base64, Encoding.UTF8), validation);

            string result = HttpHelper.SendSoapRequest(postData, url);
            YunDaRDEntity yundaRes = result.DeserializeJSONTo<YunDaRDEntity>();
            List<TransportEntity> listResult = new List<TransportEntity>();
            if (yundaRes.response.result.Equals("true"))
            {
                yundaRes.response.traces.ForEach(t =>
                {
                    listResult.Add(new TransportEntity()
                    {
                        AcceptAddress = t.station,
                        AcceptTime = t.time,
                        Remark = t.remark,
                        State = t.status
                    });
                });
            }

            return listResult;
            /*
            请求示例
            1. 假设 partnerid 为 YUNDA；密码为 123456； xmldata 内容为<order></order>
            2. xmldata 经过 BASE64 编码以后变成 PG9yZGVyPjwvb3JkZXI+
            3. 那么要签名的内容为 PG9yZGVyPjwvb3JkZXI+YUNDA123456，经过 md5 后的内容就为
            f197e870a12528e38cb483b4e371f4ea
            4. 然后再对 xmldata 经过 URL 编码，得到字符串 PG9yZGVyPjwvb3JkZXI%2B
            5. 同样需要对其他字段进行 URL 编码，否则可能会影响 POST 传递，具体请参见 HTTP
            POST 传输协议
            6. 最终要发送的数据为：
            partnerid=YUNDA&version=1.0&request=data&xmldata=PG9yZGVyPjwvb3JkZXI%2B&val
            idation=f197e870a12528e38cb483b4e371f4ea
            */
        }
    }
}
