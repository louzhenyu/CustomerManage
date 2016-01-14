/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:36
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
    /// 表T_QN_ActivityQuestionnaireMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_QN_ActivityQuestionnaireMappingDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_ActivityQuestionnaireMappingEntity>, IQueryable<T_QN_ActivityQuestionnaireMappingEntity>
    {
        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="AID">标识符的值</param>
        public T_QN_ActivityQuestionnaireMappingEntity GetByAID(object AID)
        {
            //参数检查
            if (AID == null)
                return null;
            string id = AID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_ActivityQuestionnaireMapping] where  ActivityID='{0}'   and isdelete=0 ", id.ToString());
            //读取数据
            T_QN_ActivityQuestionnaireMappingEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
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
    }
}
