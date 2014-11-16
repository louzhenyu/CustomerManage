/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:57
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
    /// 表EclubVipClassMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubVipClassMappingDAO : Base.BaseCPOSDAO, ICRUDable<EclubVipClassMappingEntity>, IQueryable<EclubVipClassMappingEntity>
    {

        #region  获取实例
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public EclubVipClassMappingEntity GetModel(string strWhere)
        {
            //参数检查
            if (string.IsNullOrEmpty(strWhere))
                return null;
            //组织SQL
            string sql = string.Format(@"    
select M.* from EclubVipClassMapping M 
  inner join Vip V 
  on M.VipID=V.VIPID and M.IsDelete=V.IsDelete 
  where M.IsDelete=0 and M.CustomerId='{0}' {1} ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, strWhere);

            //读取数据
            EclubVipClassMappingEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public EclubVipClassMappingEntity GetModel_V1(string strWhere)
        {
            //参数检查
            if (string.IsNullOrEmpty(strWhere))
                return null;
            //组织SQL
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select M.VipID, M.ClassInfoID,V.Email from EclubVipClassMapping M ");
            sbSQL.Append("inner join EclubClassInfo ECl on ECl.IsDelete=0 and ECl.CustomerId=M.CustomerId and ECl.ClassInfoID=M.ClassInfoID ");
            sbSQL.Append("inner join EclubCourseInfo ECo on ECo.IsDelete=0 and ECo.CustomerId=ECl.CustomerId and ECo.CourseInfoID=ECl.CourseInfoID ");
            sbSQL.Append("inner join Vip V on M.VipID=V.VIPID and M.IsDelete=V.IsDelete and V.ClientID=M.CustomerId ");
            sbSQL.AppendFormat("where M.IsDelete=0 and M.CustomerId='{0}' ",CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            sbSQL.Append(strWhere);

            //读取数据
            EclubVipClassMappingEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sbSQL.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load_V1(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }
        #endregion

        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load_V1(SqlDataReader pReader, out EclubVipClassMappingEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new EclubVipClassMappingEntity();

            if (pReader["VipID"] != DBNull.Value)
            {
                pInstance.VipID = Convert.ToString(pReader["VipID"]);
            }
            if (pReader["ClassInfoID"] != DBNull.Value)
            {
                pInstance.ClassInfoID = Convert.ToString(pReader["ClassInfoID"]);
            }
            if (pReader["Email"] != DBNull.Value)
            {
               pInstance.Email = Convert.ToString(pReader["Email"]);
            }
        }

    }
}
