/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/28 12:56:52
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.VIP.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.VIP.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表ItemKnowledge的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ItemKnowledgeDAO : BaseDAO<BasicUserInfo>, ICRUDable<ItemKnowledgeEntity>, IQueryable<ItemKnowledgeEntity>
    {
        /// <summary>
        /// 根据用户的微信公众平台唯一码获取知识
        /// </summary>
        /// <param name="WeiXinUserId"></param>
        /// <returns></returns>
        public ItemKnowledgeEntity GetByWeiXinUserId(object WeiXinUserId)
        {
            //参数检查
            if (WeiXinUserId == null)
                return null;
            string id = WeiXinUserId.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select top 1 a.* from ItemKnowledge a inner join VipItemMapping b on(a.Item = b.Item) where b.WeiXinUserId = '{0}' and a.IsDelete=0 order by b.lastUpdateTime desc  ", id.ToString());
            //读取数据
            ItemKnowledgeEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }

            if (m == null) {
                sql = new StringBuilder();
                sql.AppendFormat("select top 1 a.* from ItemKnowledge a  where item = '公共' and a.IsDelete=0  ", "");

                using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
                {
                    while (rdr.Read())
                    {
                        this.Load(rdr, out m);
                        break;
                    }
                }
            }
            //返回
            return m;
        }

    }
}
