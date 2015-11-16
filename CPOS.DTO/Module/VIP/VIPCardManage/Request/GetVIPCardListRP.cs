using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request
{/// <summary>
    /// 获取会员卡管理列表请求内容
    /// </summary>
    public class GetVIPCardListRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 会员卡号码
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 会员卡类型ID
        /// </summary>
        public string VipCardTypeID { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 会员卡状态ID
        /// </summary>
        public string VipCardStatusId { get; set; }
        /// <summary>
        /// 办卡起始日期
        /// </summary>
        public string BeginDate { get; set; }
        /// <summary>
        /// 办卡结束日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VIPID { get; set; }
        /// <summary>
        /// 页码。为空则默认为0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数。为空则为15
        /// </summary>
        public int PageSize { get; set; }

        #endregion

        public void Validate() { }
    }
}
