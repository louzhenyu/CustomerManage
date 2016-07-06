using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response
{
    public class GetVIPCardRD:IAPIResponseData
    {
        #region 属性
        /// <summary>
        /// 卡ID
        /// </summary>
        public string VipCardID { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 卡内码
        /// </summary>
        public string VipCardISN { get; set; }
        /// <summary>
        /// 会员卡编号
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string VipCardName { get; set; }
        /// <summary>
        /// 卡状态ID
        /// </summary>
        public int VipCardStatusId { get; set; }
        
        /// <summary>
        /// 入会时间
        /// </summary>
        public string MembershipTime { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string MembershipUnitName { get; set; }
        /// <summary>
        /// 累计总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 卡内余额
        /// </summary>
        public decimal BalanceAmount { get; set; }
        /// <summary>
        /// 当前有效返现金额
        /// </summary>
        public decimal ValidReturnAmount { get; set; }
        /// <summary>
        /// 累计返现总金额
        /// </summary>
        public decimal TotalReturnAmount { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CraeteUserName { get; set; }
        /// <summary>
        /// 售卡人
        /// </summary>
        public string SalesUserName { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string VipCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 会员积分
        /// </summary>
        public decimal Integration { get; set; }
        /// <summary>
        /// 会员创建人姓名（销售人）
        /// </summary>
        public string VipCreateByName { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// 判定会员生日是否可以修改
        /// </summary>
        public string Col22 { get; set; }
        /// <summary>
        /// 累计积分
        /// </summary>
        public decimal CumulativeIntegral { get; set; }
        /// <summary>
        /// 有效起始日期
        /// </summary>
        public string BeginDate { get; set; }
        /// <summary>
        /// 有效结束日期
        /// </summary>
        public string EndDate { get; set; }

        public List<VipCardStatusChangeLog> StatusLogList { get; set; }

        #endregion
    }

    public class VipCardStatusChangeLog
    {
        #region 属性
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        
        /// <summary>
        /// 门店名称
        /// </summary>
        public string UnitName { get; set; }
        ///// <summary>
        ///// 卡状态ID
        ///// </summary>
        //public int VipCardStatusID { get; set; }
        /// <summary>
        /// 卡操作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string ChangeReason { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        #endregion
    }
}
