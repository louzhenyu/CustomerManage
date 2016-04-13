using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL
{
    public class BuryingPointEntity
    {
        public BuryingPointEntity()
        {
        }

        /// <summary>
        /// 请求日期
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ReturnTime { get; set; }

        /// <summary>
        /// 请求端SessionID
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// Header
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 请求URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public string SysType { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequstParameters { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 频道
        /// </summary>
        public string ChannelID { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string ReturnData { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
