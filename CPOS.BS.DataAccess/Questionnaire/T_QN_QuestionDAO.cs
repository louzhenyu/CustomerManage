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
    /// 表T_QN_Question的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_QN_QuestionDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionEntity>, IQueryable<T_QN_QuestionEntity>
    {
        #region 更新订单表数据

        /// <summary>
        /// 根据问卷id获取数据集合
        /// </summary>
        /// <param name="QuestionnaireID">问卷id</param>
        /// <returns></returns>
        public T_QN_QuestionEntity[] getList(string QuestionnaireID)
        {
            string sql = string.Empty;
            sql += " select * from [T_QN_Question] where 1=1  and isdelete=0  ";
            sql += " and   CONVERT(VARCHAR(500), Questionid) in (select QuestionID from T_QN_QuestionNaireQuestionMapping where  isdelete=0  and QuestionnaireID = '" + QuestionnaireID.Replace("\"", "\'") + "' ) order by sort ";

            //执行SQL
            List<T_QN_QuestionEntity> list = new List<T_QN_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }

        /// <summary>
        /// 根据问卷id获取单选和复选数据集合
        /// </summary>
        /// <param name="QuestionnaireID">问卷id</param>
        /// <returns></returns>
        public T_QN_QuestionEntity[] getOptionQuestionList(string QuestionnaireID)
        {
            string sql = string.Empty;
            sql += " select * from [T_QN_Question] where 1=1  and isdelete=0 and (QuestionidType=3 or QuestionidType=4 or QuestionidType=9 or QuestionidType=10 ) ";
            sql += " and   CONVERT(VARCHAR(500), Questionid) in (select QuestionID from T_QN_QuestionNaireQuestionMapping where  isdelete=0  and QuestionnaireID = '" + QuestionnaireID.Replace("\"", "\'") + "' ) order by sort ";

            //执行SQL
            List<T_QN_QuestionEntity> list = new List<T_QN_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }

        #endregion   
    }
}
