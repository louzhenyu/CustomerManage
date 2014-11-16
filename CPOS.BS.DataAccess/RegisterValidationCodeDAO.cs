/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/25 11:53:15
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
    /// 表RegisterValidationCode的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class RegisterValidationCodeDAO : Base.BaseCPOSDAO, ICRUDable<RegisterValidationCodeEntity>, IQueryable<RegisterValidationCodeEntity>
    {
        public void InsertSMS(string mobile, string message, string sign)
        {
            SQLHelper.ExecuteNonQuery(string.Format("INSERT INTO SMS_send(Mobile_NO,SMS_content,Sign) values ('{0}','{1}','{2}')", mobile, message, sign));
        }

        #region HS_InsertSMS
        /// <summary>
        /// 华硕校园发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="msg"></param>
        public void HS_InsertSMS(string mobile, string msg)
        {
            SQLHelper.ExecuteNonQuery(string.Format("INSERT INTO SMS_send(Mobile_NO,SMS_content,Sign) values ('{0}','{1}','泸州老窖')", mobile, msg));
        }
        #endregion

        public void DeleteByMobile(string pMobile, int isSuccess, SqlTransaction tran)
        {
            string sql = string.Format("update RegisterValidationCode set isdelete=1, IsValidated ={0} where isdelete=0 and Mobile='{1}'", isSuccess, pMobile);
            if (tran == null)
                this.SQLHelper.ExecuteNonQuery(sql);
            else
                this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
        }

        public RegisterValidationCodeEntity[] GetByMobile(string pMobile, SqlTransaction tran)
        {
            List<RegisterValidationCodeEntity> list = new List<RegisterValidationCodeEntity> { };
            string sql = string.Format("select * from RegisterValidationCode where isdelete=0 and Mobile='{0}'", pMobile);
            DataSet ds;
            if (tran == null)
                ds = this.SQLHelper.ExecuteDataset(sql);
            else
                ds = this.SQLHelper.ExecuteDataset(tran, CommandType.Text, sql);
            using (var rd = ds.Tables[0].CreateDataReader())
            {
                while (rd.Read())
                {
                    RegisterValidationCodeEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
