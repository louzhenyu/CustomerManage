using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request
{
    public class SetVipCardRP : IAPIRequestParameter
    {
        #region VipAmount接口合并进来新增的参数
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 会员编码
        /// </summary>
        public string VipCode { get; set; }
        /// <summary>
        /// 余额/返现来源（23=人工调整余额；24=人工调整返现）
        /// </summary>
        public string AmountSourceID { get; set; }

        #endregion

        /// <summary>
        /// 卡ID
        /// </summary>
        public string VipCardID { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public int OperationType { get; set; }

        /// <summary>
        /// 调整金额
        /// </summary>
        public decimal BalanceMoney { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string ChangeReason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 新卡号
        /// </summary>
        public string NewCardCode { get; set; }


        /// <summary>
        /// 会员卡类型Id
        /// </summary>
        public int VipCardTypeId { get; set; }

        

        public void Validate() { }
    }
}
