using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Transport
{
    [XmlRoot("Request")]
    public class ShunFengRPEntity
    {
        private string _service = "RouteService";
        /// <summary>
        /// 服务名称
        /// </summary>
        [XmlAttribute("service")]
        public string service { get { return _service; } set { _service = value; } }
        private string _lang = "zh-CN";
        /// <summary>
        /// 语言
        /// </summary>
        [XmlAttribute("lang")]
        public string lang { get { return _lang; } set { _lang = value; } }
        public string Head { get; set; }
        public ShunFengRPBody Body { get;set; }
    }

    public class ShunFengRPBody
    {
        public ShunFengRPBodyRoute RouteRequest { get; set; }
    }

    public class ShunFengRPBodyRoute
    {
        /// <summary>
        /// 如果 tracking_type=1，则此值 为顺丰运单号 如果 tracking_type = 2，则此值 为客户订单号
        /// </summary>
        private string _tracking_type = "2";
        [XmlAttribute("tracking_type")]
        public string tracking_type { get { return _tracking_type; } set { _tracking_type = value; } }
        private string _method_type = "1";
        /// <summary>
        /// 1：标准路由查询    2：定制路由查询
        /// </summary>
        [XmlAttribute("method_type")]
        public string method_type { get { return _method_type; } set { _method_type = value; } }
        /// <summary>
        /// 运单号   多个用逗号,分隔
        /// </summary>
        [XmlAttribute("tracking_number")]
        public string tracking_number { get; set; }
    }
}
