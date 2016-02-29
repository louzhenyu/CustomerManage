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
    /// 表T_QN_QuestionnaireAnswerRecord的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_QN_QuestionnaireAnswerRecordDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionnaireAnswerRecordEntity>, IQueryable<T_QN_QuestionnaireAnswerRecordEntity>
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="AID">活动id</param>
        /// <param name="QNID">问卷id</param>
        /// <returns></returns>
        public T_QN_QuestionnaireAnswerRecordEntity[] GetModelList(object AID, object QNID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(string.Format("select * from [T_QN_QuestionnaireAnswerRecord] where 1=1  and QuestionnaireID='{0}' and  ActivityID='{1}' order by CreateTime desc ", QNID, AID));
            //读取数据
            List<T_QN_QuestionnaireAnswerRecordEntity> list = new List<T_QN_QuestionnaireAnswerRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireAnswerRecordEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }

        /// <summary>
        /// 获取所有用户数据
        /// </summary>
        /// <param name="AID">活动id</param>
        /// <param name="QNID">问卷id</param>
        /// <returns></returns>
        public string[] GetUserModelList(object AID, object QNID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(string.Format("SELECT VipID,max(CreateTime) as CreateTime  from T_QN_QuestionnaireAnswerRecord   where QuestionnaireID='{0}' and  ActivityID='{1}' and  Status=1  GROUP BY VipID ORDER BY CreateTime desc   ", QNID, AID));
            //读取数据

            List<string> list = new List<string>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    list.Add(rdr[0].ToString());
                }
            }
            //返回
            return list.ToArray();
        }


        /// <summary>
        /// 按照每个用户答题标识批量删除
        /// </summary>
        /// <param name="vipIDs">用户答题标识数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void DeletevipIDs(object[] vipIDs, IDbTransaction pTran) 
        {
            if (vipIDs == null || vipIDs.Length==0)
                return ;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in vipIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [T_QN_QuestionnaireAnswerRecord] set status='-1' where VipID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
    }
}
