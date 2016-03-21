/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/22 14:32:02
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
    /// 表VipAmount的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipAmountDAO : BaseCPOSDAO, ICRUDable<VipAmountEntity>, IQueryable<VipAmountEntity>
    {
        #region Jermyn20140710 积分变更
        public string SetVipAmountChange(string CustomerId
                                   , int AmountSourceId
                                   , string VipId
                                   , decimal Amount
                                   , string ObjectId
                                   , string Remark
                                   , SqlTransaction tran
                                   , string InOut
                                   , out string strError
                                   ) 
        {
            try
            {
                strError = "Data Success!";
                string bReturn = "0";
                string sql = "ProcSetVipAmountChange";

                List<SqlParameter> parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter() { ParameterName = "@VipId", Value = VipId });
                parameter.Add(new SqlParameter() { ParameterName = "@Amount", Value = Amount });
                parameter.Add(new SqlParameter() { ParameterName = "@AmountSourceId", Value = AmountSourceId });
                parameter.Add(new SqlParameter() { ParameterName = "@InOut", Value = InOut });
                parameter.Add(new SqlParameter() { ParameterName = "@ObjectId", Value = ObjectId });
                parameter.Add(new SqlParameter() { ParameterName = "@Remark", Value = Remark });
                parameter.Add(new SqlParameter() { ParameterName = "@CustomerID", Value = CustomerId });
                if (tran == null)
                    bReturn = this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, sql, parameter.ToArray()).ToString();
                else
                    bReturn = this.SQLHelper.ExecuteScalar(tran, CommandType.StoredProcedure, sql, parameter.ToArray()).ToString();

                return bReturn;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return "1";
            }
        }
        #endregion

        public decimal GetVipByEndAmount(string vipId, System.Data.SqlClient.SqlTransaction tran)
        {
            decimal endamount = 0;
            var sql = string.Format("select EndAmount from VipAmount where VipID='{0}' and isdelete=0 ", vipId);
            var res = SQLHelper.ExecuteScalar(tran,CommandType.Text, sql);
            if (res==null || string.IsNullOrWhiteSpace(res.ToString()))
            {
                return endamount;
            }
            return Convert.ToDecimal(res);
        }
        public decimal GetVipValidReturnAmountByID(string vipId, System.Data.SqlClient.SqlTransaction tran)
        {
            decimal validReturnAmount = 0;
            var sql = string.Format("select ValidReturnAmount from VipAmount where VipId='{0}' and isdelete = 0", vipId);
            var result = SQLHelper.ExecuteScalar(tran, CommandType.Text, sql);
            if (result == null || string.IsNullOrWhiteSpace(result.ToString()))
            {
                return validReturnAmount;
            }
            return Convert.ToDecimal(result);
        }
    }
}
