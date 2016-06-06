using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetSetoffToolsRD : IAPIResponseData
    {
        public List<SetOffToolsInfo> SetOffToolsList { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        /// <summary>
        /// 激励类型(1=现金，2=积分)
        /// </summary>
        public int SetoffRegAwardType { get; set; }
        /// <summary>
        /// 激励金额或激励积分
        /// </summary>
        public string SetoffRegPrize { get; set; }
        /// <summary>
        /// 集客销售成功提成比例
        /// </summary>
        public decimal? SetoffOrderPer { get; set; }
       
    }

    public class SetOffToolsInfo
    {
        public long? DisplayIndex { get; set; }
        public string SetoffEventID { get; set; } //集客活动ID
        public string SetoffToolID { get; set; }//集客工具ID
        public string ToolType { get; set; }//集客工具类型
        public string ObjectId { get; set; }//集客工具对象ID
        public string SetOffToolName { get; set; }//集客活动工具名称
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? IsRead { get; set; }//是否打开
        public string IsPush { get; set; }//是否推送
        public string URL { get; set; }
        public string ImageUrl { get; set; }
        public string ServiceLife { get; set; }
    }

}
