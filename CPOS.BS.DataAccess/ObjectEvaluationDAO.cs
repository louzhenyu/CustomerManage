/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 17:54:09
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
using JIT.CPOS.Entity;

namespace JIT.CPOS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表ObjectEvaluation的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ObjectEvaluationDAO : JIT.CPOS.BS.DataAccess.Base.BaseCPOSDAO, ICRUDable<ObjectEvaluationEntity>, IQueryable<ObjectEvaluationEntity>
    {
        //pClientID是客户ID，pMemberID是指会员ID
        public ObjectEvaluationEntity[] GetByVIPAndObject(string pClientID,string pMemberID, string pObjectID, int page, int pagesize)
        {
            StringBuilder sub = new StringBuilder();
            if (!string.IsNullOrEmpty(pMemberID))
                sub.AppendLine(string.Format(" and a.ClientID='{0}'", pClientID));
            if (!string.IsNullOrEmpty(pMemberID))
                sub.AppendLine(string.Format(" and a.MemberID='{0}'", pMemberID));
            if (!string.IsNullOrEmpty(pObjectID))
                sub.AppendLine(string.Format(" and a.objectid='{0}'", pObjectID));
            List<ObjectEvaluationEntity> list = new List<ObjectEvaluationEntity> { };
            StringBuilder sql = new StringBuilder(string.Format(@"select row_number() over(order by createtime desc) _row, a.*
                                                                  from ObjectEvaluation a 
                                                                  where a.isdelete=0 {0}", sub));//创建row_number
            //下面用上面的sql来拼装
            sql = new StringBuilder(string.Format("select * from ({0}) t where t._row>{1}*{2} and t.row<=({1}+1)*{2}", sql, page, pagesize));
            sql.AppendLine(string.Format(@"select count(*) from from ObjectEvaluation a
                                            where a.isdelete=0 
                                            {0}", sub));//这一句话是计算总数量的
            DataSet ds;
            ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            var count = Convert.ToInt32(ds.Tables[1].Rows[0][0]);//第二个表里存的是总数量
            using (var rd = ds.Tables[0].CreateDataReader())//用reader读取，速度快
            {
                while (rd.Read())
                {
                    ObjectEvaluationEntity m;//输出m
                    this.Load(rd, out m);
                    m.MemberName = rd["vipname"].ToString();
                    m.Count = count;//每个里都赋值了属性
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
