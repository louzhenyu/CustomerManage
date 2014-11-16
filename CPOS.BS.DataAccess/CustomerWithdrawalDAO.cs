/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:21:46
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表CustomerWithdrawal的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CustomerWithdrawalDAO : BaseCPOSDAO, ICRUDable<CustomerWithdrawalEntity>, IQueryable<CustomerWithdrawalEntity>
    {
        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }
        public DataSet GetCustomerWithdrawalList(string customerId, string serialNo, string beginDate, string endDate, int status, int pageIndex, int pageSize)
        {
            var para = new List<SqlParameter>();
            var sqlWhere = new StringBuilder();
            para.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });

            if (!string.IsNullOrEmpty(beginDate))
            {
                sqlWhere.Append(" and convert(nvarchar(10),a.WithdrawalTime,121) >=@pBeginDate ");
                para.Add(new SqlParameter() { ParameterName = "@pBeginDate", Value = beginDate });
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                sqlWhere.Append(" and convert(nvarchar(10),a.WithdrawalTime,121) <=@pEndDate ");
                para.Add(new SqlParameter() { ParameterName = "@pEndDate", Value = endDate });
            }

            if (serialNo != "" && !string.IsNullOrEmpty(serialNo))
            {
                sqlWhere.Append(" and a.SerialNo like '%'+@pSerialNo+'%'");
                para.Add(new SqlParameter() { ParameterName = "@pSerialNo", Value = serialNo });
            }

            if (status != -1)
            {
                sqlWhere.Append(" and a.WithdrawalStatus = @pStatus");
                para.Add(new SqlParameter() { ParameterName = "@pStatus", Value = status });
            }

            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.Append(" select row_number()over(order by a.WithdrawalTime desc) as _row,a.WithdrawalId,a.SerialNo,a.WithdrawalTime,");
            sql.Append(" b.CustomerName,b.ReceivingBank,BankAccount = '***************' + right(b.BankAccount,4),a.WithdrawalAmount,");
            sql.Append(" WithdrawalStatus = (select optiontext from options where optionname = 'customerWithdrawalStatus' and optionvalue =a.WithdrawalStatus ),");
            sql.Append(" a.FailureReason");
            sql.Append(" from CustomerWithdrawal a");
            sql.Append(" left join cpos_ap..CustomerBack b ");
            sql.Append(" on a.CustomerBackId = b.CustomerBackId");
            sql.Append(" where a.IsDelete = 0 and b.isdelete = 0 ");
            sql.Append(" and a.customerId = @pCustomerId");
            sql.Append(sqlWhere);
            sql.Append(") t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
               , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }
        public DataSet GetCustomerOrderPayStatus()
        {
            string sql = "select * from (select '-1' as OptionValue,'全部' as OptionText,0 as Sequence  "
                     + " union all select OptionValue,OptionText,Sequence from Options"
                     + " where optionname = 'CustomerOrderPayStatus' and IsDelete = 0) t order by Sequence ";
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        /// <summary>
        /// 获取客户提现基本信息 add by changjian.tian 2014-06-18
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerWithrawalInfo(string pCustomerId)
        {
            SqlParameter[] pars = new SqlParameter[] {
              new SqlParameter("@CustomerID",pCustomerId)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "sp_GetCustomerWithdrawalinfo", pars);
        }
        /// <summary>
        ///// 获取提现主标识
        ///// 客户ID。提现金额
        ///// </summary>
        ///// <returns></returns>
        //public string GetWithdrawalID(string pCustomerId, int pWithdrawalStatus)
        //{
        //    DataSet ds = new DataSet();
        //    string WithdrawalID = string.Empty;
        //    StringBuilder sbSQL = new StringBuilder();
        //    sbSQL.AppendFormat("select * from CustomerWithdrawal where CustomerId='{0}' and WithdrawalStatus='{1}' and IsDelete=0 ", pCustomerId, pWithdrawalStatus);
        //    ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        WithdrawalID = ds.Tables[0].Rows[0]["WithdrawalID"].ToString();
        //    }
        //    return WithdrawalID;
        //}
        ///// <summary>
        ///// 根据提现主标识跟新
        ///// </summary>
        ///// <param name="pWithdrawalld"></param>
        ///// <param name="pWithdrawalStatus"></param>
        ///// <returns></returns>
        //public int UpdateWithdrawalStatus(string pCustomerId, string pWithdrawalld, int pWithdrawalStatus, string pUserId)
        //{
        //    StringBuilder sbSQL = new StringBuilder();
        //    sbSQL.Append(string.Format("UPDATE CustomerWithdrawal set WithdrawalStatus='{0}',WithdrawalBy='" + pUserId + "',WithdrawalTime=getdate(), LastUpdateBy='{1}',LastUpdateTime=getdate() where CustomerId='{2}' and WithdrawalId='{3}'", pWithdrawalStatus, pUserId, pCustomerId, pWithdrawalld));
        //    return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        //}
        /// <summary>
        /// 判断客户提现时间
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <returns></returns>
        public bool GetWithdrawalDayByMaxPeriod(string pCustomerId)
        {
            string sql = "select dbo.fn_GetWithdrawalDayByMaxPeriod ('" + pCustomerId + "') as MaxTimes ";
            object obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj.ToString().Equals("1"))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 申请提现
        /// 客户ID。登录用户ID。提现金额
        /// </summary>
        /// <returns></returns>
        public int getApplyForWithdrawal(string pCustomerId, string pUserId, decimal pWithdrawalAmount)
        {
            SqlParameter[] pars = new SqlParameter[] {
              new SqlParameter("@pCustomerID",pCustomerId),
              new SqlParameter("@pUserId",pUserId),
              new SqlParameter("@WithdrawalAmount",pWithdrawalAmount)
            };
            object obj = this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_ApplyForWithdrawal", pars);
            return Convert.ToInt32(obj.ToString().Trim());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pWithdrawalId"></param>
        /// <returns></returns>
        public DataSet GetTradeCenterPay(string SerialNo)
        {
            string sql = string.Format(@"select * from cpos_ap.dbo.CustomerBack
	                        where CustomerId='{0}'
	                        and IsDelete=0", this.CurrentUserInfo.ClientID);
            sql += string.Format(@"    select  *  from  CustomerWithdrawal	a
	             left join CustomerAmount b on a.CustomerId=b.CustomerId
                 left join cpos_ap..CustomerBack c on a.CustomerBackId=c.CustomerBackId 
	             where WithdrawalStatus='30' and a.IsDelete=0  and b.IsDelete=0 and c.IsDelete=0 and a.CustomerId='{0}'", this.CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(SerialNo))
            {
                sql += "  and a.SerialNo ='" + SerialNo + "'";
            }
            return this.SQLHelper.ExecuteDataset(sql);

        }

        public void SetTradeCenterPay(string SerialNo)
        {
          SqlTransaction tra =this.SQLHelper.CreateTransaction();
            using (tra.Connection)
            {
             
              string sql = @" update CustomerOrderPay set OrderPayStatus='40'
                    where exists(
                    select OrderPayId from CustomerOrderPay a
                    inner join CustomerWithdrawal b on a.WithdrawalId=b.WithdrawalId 
                    where 1=1 ";
                if (!string.IsNullOrEmpty(SerialNo))
                {
                    sql += " And b.SerialNo='" + SerialNo + "'";
                }
                sql += @"  and a.IsDelete=0 and b.IsDelete=0 and  b.WithdrawalStatus='30' and a.CustomerId='" + this.CurrentUserInfo.ClientID + @"' and b.CustomerId='" + this.CurrentUserInfo.ClientID + @"'
                      and a.OrderPayId=CustomerOrderPay.OrderPayId)";

                 sql += " update CustomerWithdrawal set WithdrawalStatus='40' where IsDelete='0' and CustomerId='" + this.CurrentUserInfo.ClientID + "' and WithdrawalStatus='30'";

                if (!string.IsNullOrEmpty(SerialNo))
                {
                    sql += " And SerialNo='" + SerialNo + "'";
                }
                this.SQLHelper.ExecuteNonQuery(sql);
                tra.Commit();
            }
        }

        public void NotityTradeCenterPay(string SerialNo, string WithdrawalStatus)
        {
            //修改提现状态
            string sql = @" update CustomerWithdrawal set WithdrawalStatus='" + WithdrawalStatus + @"'
                            where exists(
                            select WithdrawalStatus  from CustomerWithdrawal w
                            inner join CustomerWithdrawalTransferMapping a on w.WithdrawalId=a.WithdrawalId 
                            inner join CustomerWithdrawalTransfer b on a.TransferId=b.TransferId and w.CustomerId=b.CustomerId
                            where b.SerialNo='" + SerialNo + @"' and w.IsDelete=0 and a.IsDelete=0 and b.IsDelete=0
                            and w.CustomerId='" + this.CurrentUserInfo.ClientID + "' and CustomerWithdrawal.WithdrawalId=w.WithdrawalId)";

            sql += "  update CustomerWithdrawalTransfer set TransferStatus='" + WithdrawalStatus + "' where SerialNo='" + SerialNo + "'";

            sql += @"       update CustomerOrderPay set OrderPayStatus='"+WithdrawalStatus+@"'
                            where exists(
                            select *  from CustomerOrderPay c
                            inner join CustomerWithdrawal w on c.WithdrawalId=w.WithdrawalId
                            inner join CustomerWithdrawalTransferMapping a on w.WithdrawalId=a.WithdrawalId 
                            inner join CustomerWithdrawalTransfer b on a.TransferId=b.TransferId and w.CustomerId=b.CustomerId
                            
                            where b.SerialNo='" + SerialNo + @"' and w.IsDelete=0 and a.IsDelete=0 and b.IsDelete=0
                            and w.CustomerId='"+this.CurrentUserInfo.ClientID+"' and CustomerOrderPay.OrderPayId=c.OrderPayId)";


            this.SQLHelper.ExecuteNonQuery(sql);

        }
    }
}
