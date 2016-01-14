/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:37
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
    /// 表T_QN_QuestionnaireOptionCount的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_QN_QuestionnaireOptionCountDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionnaireOptionCountEntity>, IQueryable<T_QN_QuestionnaireOptionCountEntity>
    {
        /// <summary>
        /// 根据选项id和活动id查询是否存在数据
        /// </summary>
        /// <param name="OptionID"></param>
        /// <param name="ActivityID"></param>
        public T_QN_QuestionnaireOptionCountEntity isExist(string OptionID, string ActivityID)
        {
            //参数检查
            if (OptionID == null || ActivityID==null)
                return null;
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_QuestionnaireOptionCount] where ActivityID='{0}' and OptionID='{1}'  and isdelete=0 ",ActivityID, OptionID);
            //读取数据
            T_QN_QuestionnaireOptionCountEntity m = null;
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

        /// <summary>
        /// 根据选项id和活动id查询数据
        /// </summary>
        /// <param name="OptionID"></param>
        /// <param name="ActivityID"></param>
        public T_QN_QuestionnaireOptionCountEntity[] GetList(string QuestionnaireID, string ActivityID)
        {
            //参数检查
            if (QuestionnaireID == null || ActivityID == null)
                return null;
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_QuestionnaireOptionCount] where ActivityID='{0}' and QuestionnaireID='{1}'  and isdelete=0 ", ActivityID, QuestionnaireID);
            //读取数据
            List<T_QN_QuestionnaireOptionCountEntity> list = new List<T_QN_QuestionnaireOptionCountEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireOptionCountEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }
    }
}
