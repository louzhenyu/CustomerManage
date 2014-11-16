/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:10:21
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
    /// 表CustomerOrderPay的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CustomerOrderPayDAO : Base.BaseCPOSDAO, ICRUDable<CustomerOrderPayEntity>, IQueryable<CustomerOrderPayEntity>
    {
        #region MyRegion
        public DataSet GetCustomerOrderPayPage(CustomerOrderPayEntity entity, int pageIndex, int pageSize)
        {

            //            string sql = "select COUNT(1) from cpos_ap..CustomerPlot where IsDelete=0 and CustomerId=" + this.CurrentUserInfo.ClientID + "";
            //            int obj = (int)this.SQLHelper.ExecuteScalar(sql);

            //            sql = @"SELECT cop.*,tinout.order_no OrderNo
            //                       ,tp.Payment_Type_Name PaymentName,ss.VipSourceName
            //                        ,(case when OrderPayStatus=10 then ''未到帐'' when OrderPayStatus=20 then ''已到帐'' when OrderPayStatus=30 
            //                        then ''已提现'' when OrderPayStatus=40 then ''出帐中'' when OrderPayStatus=50 then ''提现成功'' when OrderPayStatus=0 then ''提现失败'' end)OrderPayStatusName
            //                       ,(select AladingRate from  cpos_ap..CustomerPlot where";

            //            if (obj > 0)
            //            {
            //                sql += " CustomerId='+this.CurrentUserInfo.ClientID+ ') AladingRate";
            //            }
            //            else
            //            {
            //                sql += " CustomerId='' or CustomerId is null) AladingRate";
            //            }
            //            sql += @",ROW_NUMBER() over(order by cop.SerialPay DESC) rownumber 
            //                     into #tmp
            //                     FROM CustomerOrderPay cop
            //                     left join TPaymentTypeCustomerMapping  tpm on cop.ChannelId=tpm.ChannelId
            //                     left join T_Payment_Type tp on tp.Payment_Type_Id=tpm.PaymentTypeID
            //                     left join T_Inout tinout on tinout.order_id=cop.OrderId
            //                     left join SysVipSource ss on ss.VipSourceID=cop.OrderSource
            //                     left join CustomerWithdrawal ww on ww.WithdrawalId=cop.WithdrawalId
            //                     where  1=1 ";
            //            if (!string.IsNullOrEmpty(entity.OrderNo))
            //            {
            //                sql += " and tinout.order_no like ''%'+entity.OrderNo+'%''";
            //            }
            //            if (entity.OrderSource != null)
            //            {
            //                sql += " and cop.OrderSource=" + entity.OrderSource;
            //            }
            //            if (entity.OrderPayStatus != null)
            //            {
            //                sql += " and cop.OrderPayStatus=" + entity.OrderPayStatus;
            //            }
            //            if (!string.IsNullOrEmpty(entity.WithdrawalId.ToString()))
            //            {
            //                sql += " and ww.WithdrawalId='" + entity.WithdrawalId + "'";
            //            }

            //            sql += @"  select COUNT(1) RowsCount from #tmp
            //                       select * from #tmp where rownumber between (" + pageSize * (pageIndex - 1) + ")  and  (" + pageSize * pageIndex + ")";

            //            DataSet dataSet = this.SQLHelper.ExecuteDataset(sql.ToString());
            //            sql += @" declare @RowsCount int
            //                      declare @PageCount int";

            string sql = "GetCustomerOrderPay";
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@OrderNo", entity.OrderNo));
            parameter.Add(new SqlParameter("@OrderSource", entity.OrderSource));
            //parameter.Add(new SqlParameter("@PayTimeBegin", entity.PayTimeBegin.ToString() == "0001/1/1 0:00:00" ? "" : entity.PayTimeBegin.ToString()));
            //parameter.Add(new SqlParameter("@PayTimeEnd", entity.PayTimeEnd.ToString() == "0001/1/1 0:00:00" ? "" : entity.PayTimeEnd.ToString()));
            parameter.Add(new SqlParameter("@OrderPayStatus", entity.OrderPayStatus));
            parameter.Add(new SqlParameter("@CustomerId", this.CurrentUserInfo.ClientID));
            parameter.Add(new SqlParameter("@pageIndex", pageIndex));
            parameter.Add(new SqlParameter("@pageSize", pageSize));
            parameter.Add(new SqlParameter("@WithdrawalId", entity.WithdrawalId));
            DataSet dataSet = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql, parameter.ToArray());
            return dataSet;
        }
        #endregion

        /// <summary>
        /// 根据提现明细表中的支付明细ID 订单支付明细数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrderPayList(string pCustomerID, string pOrderPayId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(string.Format("select * from CustomerOrderPay where CustomerId='{0}' and OrderPayId='{1}' and IsDelete=0 ",pCustomerID,pOrderPayId));
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// 根据提现明细表中的支付明细ID 更新订单支付明细
        /// </summary>
        /// <returns></returns>
        public int UpdateOrderPayList(string pWithdrawalld,string pCustomerID,int OrderPayStatus,string pUserID)
        {
             StringBuilder sbSQL = new StringBuilder();
             sbSQL.Append(string.Format("UPDATE CustomerOrderPay  set OrderPayStatus='{0}',LastUpdateBy='{1}',WithdrawalTime=getdate(),LastUpdateTime=getdate() from	 CustomerOrderPay x inner join CustomerWithdrawalDetail y on(x.OrderPayId = y.OrderPayId) where x.IsDelete = '0' and y.IsDelete = '0' and x.CustomerId='{2}' and y.WithdrawalId ='{3}' and x.IsDelete=0 ", OrderPayStatus, pUserID, pCustomerID, pWithdrawalld));
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }
    }
}
