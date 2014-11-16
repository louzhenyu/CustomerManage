/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipCardRechargeRecordBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public List<VipCardRechargeRecordEntity> GetList(VipCardRechargeRecordEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            List<VipCardRechargeRecordEntity> list = new List<VipCardRechargeRecordEntity>();

            DataSet ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipCardRechargeRecordEntity>(ds.Tables[0]);
            }

            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(VipCardRechargeRecordEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region 充值
        /// <summary>
        /// 充值提交
        /// </summary>
        /// <param name="VipCardID">会员卡标识【必须】</param>
        /// <param name="RechargeAmount">充值金额【必须】</param>
        /// <param name="RechargeNo">小票号【必须】</param>
        /// <param name="PaymentTypeID">充值方式【必须】</param>
        /// <param name="UnitID">充值门店【必须】</param>
        /// <param name="strError">输出信息</param>
        /// <returns></returns>
        public bool SetVipCardRecjargeRpecord(string VipCardID
                                            , decimal RechargeAmount
                                            , string RechargeNo
                                            , string PaymentTypeID
                                            , string UnitID
                                            , out string strError)
        {
            try
            {
                #region 判断输入信息是否合法
                if (VipCardID == null || VipCardID.Trim().Equals(""))
                {
                    strError = "会员卡号标识不能为空.";
                    return false;
                }
                if (RechargeAmount.ToString().Trim().Equals(""))
                {
                    strError = "充值金额不能为空.";
                    return false;
                }
                if (RechargeNo == null || RechargeNo.ToString().Trim().Equals(""))
                {
                    strError = "小票号不能为空.";
                    return false;
                }
                if (PaymentTypeID == null || PaymentTypeID.ToString().Trim().Equals(""))
                {
                    strError = "支付方式不能为空.";
                    return false;
                }
                if (UnitID == null || UnitID.ToString().Trim().Equals(""))
                {
                    strError = "充值门店不能为空.";
                    return false;
                }
                #endregion
                //1.获取会员卡信息
                #region 获取会员卡信息
                VipCardBLL vipCardServer = new VipCardBLL(this.CurrentUserInfo);
                VipCardEntity vipCardInfo = new VipCardEntity();
                vipCardInfo = vipCardServer.GetByID(VipCardID);
                if (vipCardInfo == null)
                {
                    strError = "会员卡信息不存在.";
                    return false;
                }
                if (vipCardInfo.BalanceAmount == null || vipCardInfo.BalanceAmount.ToString().Equals(""))
                {
                    vipCardInfo.BalanceAmount = 0;
                }
                if (vipCardInfo.TotalAmount == null || vipCardInfo.TotalAmount.ToString().Equals(""))
                {
                    vipCardInfo.TotalAmount = 0;
                }
                #endregion
                //2.修改会员卡信息
                #region
                VipCardEntity vipCard = new VipCardEntity();
                vipCard.VipCardID = vipCardInfo.VipCardID;
                vipCard.BalanceAmount = vipCardInfo.BalanceAmount + RechargeAmount;
                vipCard.TotalAmount = vipCardInfo.TotalAmount + RechargeAmount;
                #endregion
                //3.插入充值记录
                #region
                VipCardRechargeRecordEntity vipCardRRInfo = new VipCardRechargeRecordEntity();
                vipCardRRInfo.RechargeRecordID = JIT.CPOS.BS.BLL.BaseService.NewGuidPub();
                vipCardRRInfo.RechargeNo = RechargeNo;
                vipCardRRInfo.PaymentTypeID = PaymentTypeID;
                vipCardRRInfo.RechargeAmount = RechargeAmount;
                vipCardRRInfo.RechargeTime = System.DateTime.Now;
                vipCardRRInfo.VipCardID = VipCardID;
                vipCardRRInfo.BalanceBeforeAmount = vipCardInfo.BalanceAmount;
                vipCardRRInfo.BalanceAfterAmount = vipCardInfo.BalanceAmount + RechargeAmount;
                vipCardRRInfo.RechargeUserID = this.CurrentUserInfo.UserID;
                vipCardRRInfo.UnitID = UnitID;
                vipCardRRInfo.CustomerID = this.CurrentUserInfo.CurrentUser.customer_id;
                #endregion
                //strError = "充值成功.";
                //return true;
                return _currentDAO.SetVipCardRecjargeRpecord(vipCard, vipCardRRInfo, out strError);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

    }
}