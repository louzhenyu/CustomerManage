
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

namespace JIT.CPOS.BS.DataAccess
{
    public partial class MobileHomeDAO : Base.BaseCPOSDAO, ICRUDable<MobileHomeEntity>, IQueryable<MobileHomeEntity>
    {
        /// <summary>
        /// 更新商户下所有主页的状态为非激活状态
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="pIsUpdateNullField"></param>
        public void UpdateIsActivate(MobileHomeEntity pEntity)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.HomeId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update [MobileHome] set ");
            strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            strSql.Append("[IsActivate]=@IsActivate");
            strSql.Append(" WHERE CustomerId=@CustomerId ");

            SqlParameter[] parameters = 
            {
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@IsActivate",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.LastUpdateBy;
            parameters[1].Value = pEntity.LastUpdateTime;
            parameters[2].Value = pEntity.CustomerId;
            parameters[3].Value = 0;
            //执行语句
            int result = 0;
            result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据模板Id生成新实例数据
        /// </summary>
        /// <param name="strHomeId"></param>
        public void CreateStoreDataFromTemplate(string strHomeId,string strTemplateId)
        {
            string strProc = "Proc_CreateStoreDataFromTemplate";
            SqlParameter[] parameters = 
            {
					new SqlParameter("@HomeId",SqlDbType.NVarChar),
					new SqlParameter("@TemplateId",SqlDbType.NVarChar)
            };
            parameters[0].Value = strHomeId;
            parameters[1].Value = strTemplateId;

            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, strProc, parameters);


        }
        /// <summary>
        /// 模版名称是否重复
        /// </summary>
        /// <param name="strCustomerId"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public int IsExistsTitle(string strCustomerId,string strHomeId, string strTitle)
        {
            string strSql = string.Format(@"SELECT COUNT(1) 
                                            FROM dbo.MobileHome
                                            WHERE IsDelete=0 
                                                AND CustomerId='{0}' AND HomeId<>'{1}'  AND Title='{2}'", strCustomerId, strHomeId, strTitle);

            return Convert.ToInt32(SQLHelper.ExecuteScalar(strSql));
        }
    }
}
