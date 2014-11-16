/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/3 10:50:49
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
    /// 表Knowledge的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class KnowledgeDAO : BaseCPOSDAO, ICRUDable<KnowledgeEntity>, IQueryable<KnowledgeEntity>
    {

        public KnowledgeEntity[] GetByParaAndUpdateTagSearchTimes(string pKey, int pPageIndex, int pPageSize, Guid? pCategoryID)
        {
            List<KnowledgeEntity> list = new List<KnowledgeEntity> { };
            StringBuilder sub = new StringBuilder();
            if (!string.IsNullOrEmpty(pKey))
                sub.AppendFormat(@" and exists(select 1 from KnowledgeTagMapping a 
                                    join KnowledgeTag b on a.KnowledgeTagId=b.KnowledgeTagId
                                    where b.TagName like '%{0}%' and a.KnowIedgeId=Knowledge.KnowIedgeId)", pKey);
            if (pCategoryID != null)
                sub.AppendFormat(" and KnowledgeTypeId='{0}'", pCategoryID);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(string.Format(@"select * from (select row_number()over(order by PraiseCount desc,ClickCount desc) _row,* from Knowledge where 1=1 {0}) t
                                       where t._row>{1}*{2} and t._row<=({1}+1)*{2}", sub, pPageIndex, pPageSize));
            sql.AppendLine(string.Format("update KnowledgeTag set SearchTimes=isnull(SearchTimes,0)+1 where TagName like '%{0}%'", pKey));
            using (var rd = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rd.Read())
                {
                    KnowledgeEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
                return list.ToArray();
            }
        }
    }
}
