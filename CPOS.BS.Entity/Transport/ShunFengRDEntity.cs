using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Transport
{
    [XmlRoot("Response")]
    public class ShunFengRDEntity
    {
        private string _service = "RouteService";
        /// <summary>
        /// 服务名称
        /// </summary>
        [XmlAttribute("service")]
        public string service { get { return _service; } set { _service = value; } }
        public string Head { get; set; }
        [XmlArrayItem("RouteResponse")]
        public List<ShunFengRDBodyRoutes> Body { get; set; }
    }

    public class ShunFengRDBodyRoutes
    {
        public ShunFengRDBodyRoute[] Route { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        [XmlAttribute("mailno")]
        public string mailno { get; set; }
    }

    public class ShunFengRDBodyRoute
    {
        /// <summary>
        /// 路由节点发生的时间，格式：YYYYMM-DD HH24:MM:SS，示例：2012-7-30 09:30:00
        /// </summary>
        [XmlAttribute("accept_time")]
        public string accept_time { get; set; }
        /// <summary>
        /// 路由节点发生的地点
        /// </summary>
        [XmlAttribute("accept_address")]
        public string accept_address { get; set; }
        /// <summary>
        /// 路由节点具体描述
        /// </summary>
        [XmlAttribute("remark")]
        public string remark { get; set; }
        /// <summary>
        /// 路由节点操作码
        /// </summary>
        [XmlAttribute("opcode")]
        public string opcode { get; set; }
    }
}
