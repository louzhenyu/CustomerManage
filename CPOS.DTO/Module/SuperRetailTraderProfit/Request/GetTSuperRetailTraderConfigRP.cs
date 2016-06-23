using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetTSuperRetailTraderConfigRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        /// <summary>
        /// 分销商姓名
        /// </summary>
        public string SuperRetailTraderName { get; set; }
        /// <summary>
        /// 分销商来源 User =员工、Vip=会员
        /// </summary>
        public string SuperRetailTraderFrom { get; set; }
        /// <summary>
        /// 加盟开始时间
        /// </summary>
        public string JoinSatrtTime { get; set; }
        /// <summary>
        /// 加盟结束时间
        /// </summary>
        public string JoinEndTime { get; set; }
        /// <summary>
        /// 分销商 上级编号
        /// </summary>
        public Guid? SuperRetailTraderID { get; set; }

        /// <summary>
        /// 是否查找近30天数据
        /// </summary>
        public int IsFlag { get; set; }
        public void Validate()
        {

        }
    }
}
