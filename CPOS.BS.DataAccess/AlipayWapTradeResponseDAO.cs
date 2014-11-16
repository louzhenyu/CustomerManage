/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-05-31 20:42
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表AlipayWapTradeResponse的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class AlipayWapTradeResponseDAO : BaseDAO<BasicUserInfo>, ICRUDable<AlipayWapTradeResponseEntity>, IQueryable<AlipayWapTradeResponseEntity>
    {
        /// <summary>
        /// 根据商户网站唯一订单号更新支付宝交易记录
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="pIsUpdateNullField"></param>
        public void UpdateAlipayWapTrade(AlipayWapTradeResponseEntity pEntity, bool pIsUpdateNullField = true)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [AlipayWapTradeResponse] set ");
            if (pIsUpdateNullField || pEntity.Subject != null)
                strSql.Append("[subject]=@Subject,");
            if (pIsUpdateNullField || pEntity.TotalFee != null)
                strSql.Append("[total_fee]=@TotalFee,");
            if (pIsUpdateNullField || pEntity.PaymentType != null)
                strSql.Append("[payment_type]=@PaymentType,");
            if (pIsUpdateNullField || pEntity.TradeNo != null)
                strSql.Append("[trade_no]=@TradeNo,");
            if (pIsUpdateNullField || pEntity.BuyerEmail != null)
                strSql.Append("[buyer_email]=@BuyerEmail,");
            if (pIsUpdateNullField || pEntity.GmtCreate != null)
                strSql.Append("[gmt_create]=@GmtCreate,");
            if (pIsUpdateNullField || pEntity.NotifyType != null)
                strSql.Append("[notify_type]=@NotifyType,");
            if (pIsUpdateNullField || pEntity.Quantity != null)
                strSql.Append("[quantity]=@Quantity,");
            if (pIsUpdateNullField || pEntity.NotifyTime != null)
                strSql.Append("[notify_time]=@NotifyTime,");
            if (pIsUpdateNullField || pEntity.SellerID != null)
                strSql.Append("[seller_id]=@SellerID,");
            if (pIsUpdateNullField || pEntity.TradeStatus != null)
                strSql.Append("[trade_status]=@TradeStatus,");
            if (pIsUpdateNullField || pEntity.IsTotalFeeAdjust != null)
                strSql.Append("[is_total_fee_adjust]=@IsTotalFeeAdjust,");
            if (pIsUpdateNullField || pEntity.GmtPayment != null)
                strSql.Append("[gmt_payment]=@GmtPayment,");
            if (pIsUpdateNullField || pEntity.SellerEmail != null)
                strSql.Append("[seller_email]=@SellerEmail,");
            if (pIsUpdateNullField || pEntity.GmtClose != null)
                strSql.Append("[gmt_close]=@GmtClose,");
            if (pIsUpdateNullField || pEntity.Price != null)
                strSql.Append("[price]=@Price,");
            if (pIsUpdateNullField || pEntity.BuyerID != null)
                strSql.Append("[buyer_id]=@BuyerID,");
            if (pIsUpdateNullField || pEntity.NotifyID != null)
                strSql.Append("[notify_id]=@NotifyID,");
            if (pIsUpdateNullField || pEntity.UseCoupon != null)
                strSql.Append("[use_coupon]=@UseCoupon,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where out_trade_no=@OutTradeNo ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@OutTradeNo",SqlDbType.NVarChar),
					new SqlParameter("@Subject",SqlDbType.NVarChar),
					new SqlParameter("@TotalFee",SqlDbType.NVarChar),
					new SqlParameter("@PaymentType",SqlDbType.NVarChar),
					new SqlParameter("@TradeNo",SqlDbType.NVarChar),
					new SqlParameter("@BuyerEmail",SqlDbType.NVarChar),
					new SqlParameter("@GmtCreate",SqlDbType.NVarChar),
					new SqlParameter("@NotifyType",SqlDbType.NVarChar),
					new SqlParameter("@Quantity",SqlDbType.NVarChar),
					new SqlParameter("@NotifyTime",SqlDbType.NVarChar),
					new SqlParameter("@SellerID",SqlDbType.NVarChar),
					new SqlParameter("@TradeStatus",SqlDbType.NVarChar),
					new SqlParameter("@IsTotalFeeAdjust",SqlDbType.NVarChar),
					new SqlParameter("@GmtPayment",SqlDbType.NVarChar),
					new SqlParameter("@SellerEmail",SqlDbType.NVarChar),
					new SqlParameter("@GmtClose",SqlDbType.NVarChar),
					new SqlParameter("@Price",SqlDbType.NVarChar),
					new SqlParameter("@BuyerID",SqlDbType.NVarChar),
					new SqlParameter("@NotifyID",SqlDbType.NVarChar),
					new SqlParameter("@UseCoupon",SqlDbType.NVarChar),
					new SqlParameter("@MerchantUrl",SqlDbType.NVarChar),
					new SqlParameter("@CallBackUrl",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@ResponseID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.OrderID;
            parameters[1].Value = pEntity.OutTradeNo;
            parameters[2].Value = pEntity.Subject;
            parameters[3].Value = pEntity.TotalFee;
            parameters[4].Value = pEntity.PaymentType;
            parameters[5].Value = pEntity.TradeNo;
            parameters[6].Value = pEntity.BuyerEmail;
            parameters[7].Value = pEntity.GmtCreate;
            parameters[8].Value = pEntity.NotifyType;
            parameters[9].Value = pEntity.Quantity;
            parameters[10].Value = pEntity.NotifyTime;
            parameters[11].Value = pEntity.SellerID;
            parameters[12].Value = pEntity.TradeStatus;
            parameters[13].Value = pEntity.IsTotalFeeAdjust;
            parameters[14].Value = pEntity.GmtPayment;
            parameters[15].Value = pEntity.SellerEmail;
            parameters[16].Value = pEntity.GmtClose;
            parameters[17].Value = pEntity.Price;
            parameters[18].Value = pEntity.BuyerID;
            parameters[19].Value = pEntity.NotifyID;
            parameters[20].Value = pEntity.UseCoupon;
            parameters[21].Value = pEntity.MerchantUrl;
            parameters[22].Value = pEntity.CallBackUrl;
            parameters[23].Value = pEntity.Status;
            parameters[24].Value = pEntity.LastUpdateTime;
            parameters[25].Value = pEntity.LastUpdateBy;
            parameters[26].Value = pEntity.ResponseID;

            //执行语句
            int result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据商户网站唯一订单号更新支付宝交易状态
        /// </summary>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="status">状态值 1：创建交易  2：交易成功  3：交易失败</param>
        public void UpdateAlipayWapTradeStatus(string outTradeNo, string status)
        {
            string sql = "UPDATE dbo.AlipayWapTradeResponse "
                + " SET status = '" + status + "', LastUpdateBy = '" + CurrentUserInfo.UserID + "', LastUpdateTime = '" + DateTime.Now + "' "
                + " WHERE out_trade_no = '" + outTradeNo + "'";

            //执行语句
            int result = this.SQLHelper.ExecuteNonQuery(sql);
        }
    }
}
