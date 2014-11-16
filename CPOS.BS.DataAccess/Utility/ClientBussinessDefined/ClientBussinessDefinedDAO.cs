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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
namespace JIT.CPOS.BS.DataAccess
{
   public partial class ClientBussinessDefinedDAO : BaseCPOSDAO
    {
           #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
       public ClientBussinessDefinedDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion
        /// <summary>
        /// 得到配置信息的数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public DataSet GetGridData(string pSql)
        {
            return this.SQLHelper.ExecuteDataset(pSql);

        }
        /// <summary>
        /// 得到配置生成Grid的数据源数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public DataSet GetDefind(string pSql)
        {
            return this.SQLHelper.ExecuteDataset(pSql);
        }

        public void ICRUDable(string pSql, IDbTransaction pTran)
        {
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, pSql);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, pSql);
            return;
        
        }

        #region 获取客户是否配置了会籍店
        public int GetVipUnitCnt()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            declare @cnt int
            set @cnt=(
            select
	            COUNT(*)
            from ClientBussinessDefined
            where ControlType='205' and ClientID='{0}' and IsDelete=0)
            select @cnt", CurrentUserInfo.ClientID);
            return int.Parse(this.SQLHelper.ExecuteScalar(strSql.ToString()).ToString());
        }
        #endregion
    }
}
