using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Transport
{
    /// <summary>
    /// 返回到前台的物流信息
    /// </summary>
    public class TransportEntity
    {
        /// <summary>
        /// 路由节点发生的时间，格式：YYYYMM-DD HH24:MM:SS，示例：2012-7-30 09:30:00
        /// </summary>
        public string AcceptTime { get; set; }
        /// <summary>
        /// 路由节点发生的地点
        /// </summary>
        public string AcceptAddress { get; set; }
        /// <summary>
        /// 路由节点具体描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 路由节点操作码(顺丰特有)
        /// </summary>
        public string opcode { get; set; }
        /// <summary>
        /// 快递状态 got 已揽收  transite 运输中  signed 签收/妥投  signfail 异常签收，异常妥投
        /// </summary>
        public string State { get; set; }
    }
}
