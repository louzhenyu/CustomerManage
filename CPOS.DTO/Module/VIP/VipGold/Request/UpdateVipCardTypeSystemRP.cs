using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class UpdateVipCardTypeSystemRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public int VipCardTypeID { get; set; }
        /// <summary>
        /// 升级条件标识ID/基本权益标识ID
        /// </summary>
        public string OperateObjectID { get; set; }
        /// <summary>
        /// 操作类型（1=卡等级编辑；2=升级条件编辑；3=基本权益编辑；）
        /// </summary>
        public int OperateType { get; set; }
        /// <summary>
        /// 卡等级名称(OperateType =1时必填)
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 卡等级图片(OperateType =1时必填)
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 是否可充值(OperateType =1时必填) (0=否;1=是)
        /// </summary>
        public int IsPrepaid { get; set; }
        /// <summary>
        /// 是否在线销售(OperateType =1时必填) (0=否;1=是)
        /// </summary>
        public int IsOnlineSales { get; set; }
        /// <summary>
        /// 升级类型(1=购卡升级；2=充值升级；3=消费升级;)(OperateType =2时必填)
        /// </summary>
        public int UpGradeType { get; set; }
        /// <summary>
        /// 是否补差(1=可补;2=不可补;)
        /// </summary>
        public int IsExtraMoney { get; set; }
        /// <summary>
        /// 售价 (OperateType =2&& UpGradeType=1时必填)
        /// </summary>
        public decimal Prices { get; set; }
        /// <summary>
        /// 需兑换积分 (OperateType =2&& UpGradeType=1时必填)
        /// </summary>
        public int ExchangeIntegral { get; set; }
        /// <summary>
        /// 单次充值金额(OperateType =2&& UpGradeType=2时必填)
        /// </summary>
        public decimal OnceRechargeAmount { get; set; }
        /// <summary>
        /// 累积消费金额(OperateType =2&& UpGradeType=3时必填)
        /// </summary>
        public decimal BuyAmount { get; set; }
        /// <summary>
        /// 单次消费升级金额(OperateType =2&& UpGradeType=3时必填)
        /// </summary>
        public decimal OnceBuyAmount { get; set; }
        /// <summary>
        /// 会员折扣(OperateType =3时必填)
        /// </summary>
        public decimal CardDiscount { get; set; }
        /// <summary>
        /// 消费n元赠送1积分(OperateType =3时必填)
        /// </summary>
        public decimal PaidGivePoints { get; set; }
        /// <summary>
        /// 消费n元赠送比例积分(OperateType =3时必填)
        /// </summary>
        public decimal PaidGivePercetPoints { get; set; }
        public void Validate()
        {
            const int ERROR_LACK_VipCardTypeID = 311;//VipCardTypeID不能为空 
            const int ERROR_LACK_OperateType = 312;//操作类型（1=卡等级编辑；2=升级条件编辑；3=基本权益编辑；）            
            if (VipCardTypeID==null)
            {
                throw new APIException("请求参数中缺少VipCardTypeID或值为空.") { ErrorCode = ERROR_LACK_VipCardTypeID };
            }
            if (OperateType == null || OperateType==0)
            {
                throw new APIException("请求参数中缺少OperateType或值为空.") { ErrorCode = ERROR_LACK_OperateType };
            }
        } 
    }
}
