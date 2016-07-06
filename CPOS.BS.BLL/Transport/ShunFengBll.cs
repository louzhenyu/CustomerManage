using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPOS.Common;
using JIT.CPOS.BS.Entity.Transport;
using JIT.CPOS.Common;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.BLL.Transport
{
    public class ShunFengBll : ITransportHandler
    {
        private readonly string checkword = "j8DzkIFgmlomPt0aLuwU";
        private readonly string url = "https://bsp-oisp.test.sf-express.com/bsp-oisp/sfexpressService";
        private readonly string head = "BSPdevelop";
        private string WaybillNo = string.Empty;
        public ShunFengBll(string pWaybillNo)
        {
            this.WaybillNo = pWaybillNo;
        }
        /// <summary>
        /// 获取物流信息
        /// </summary>
        /// <returns></returns>
        public List<TransportEntity> SearchTransport()
        {
            ShunFengRPEntity sfr = new ShunFengRPEntity()
            {
                Body = new ShunFengRPBody()
                {
                    RouteRequest = new ShunFengRPBodyRoute()
                    {
                        tracking_number = WaybillNo
                    }
                },
                Head = head
            };
            string xmlData = XmlHelper.SerializeToXmlStr<ShunFengRPEntity>(sfr);

            string md5 = MD5Helper.Encryption(xmlData + checkword);
            string sign = Convert.ToBase64String(md5.GetBytes());
            string postData = string.Format("xml={0}&verifyCode={1}", xmlData, sign);
            string result = HttpHelper.SendSoapRequest(postData, url);
            ShunFengRDEntity res = XmlHelper.DeserializeFromXml<ShunFengRDEntity>(result);
            List<TransportEntity> listResult = new List<TransportEntity>();
            if (res.Head.Equals("OK"))
            {
                // 获取成功
                res.Body.ForEach(t =>
                {
                    for (int i = 0; i < t.Route.Length; i++)
                    {
                        listResult.Add(new TransportEntity()
                        {
                            AcceptAddress = t.Route[i].accept_address,
                            AcceptTime = t.Route[i].accept_time,
                            Remark = t.Route[i].remark,
                            opcode = t.Route[i].opcode
                        });
                    }
                });
            }

            return listResult;
            /*其中校验码的生成规则为：
             接入 BSP 前，顺丰 BSP 系统管理员会为每个接入客户分配一个“密钥”，以下把
            密钥简称为 checkword。
             按以下逻辑生成校验码：
            o 先把 XML 报文与 checkword 前后连接。
            o 把连接后的字符串做 MD5 编码。
            o 把 MD5 编码后的数据进行 Base64 编码，此时编码后的字符串即为校验码。
            */
        }
    }
}
