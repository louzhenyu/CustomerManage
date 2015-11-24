/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/31 15:58:37
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
    /// 表MHCategoryArea的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MHCategoryAreaDAO : BaseCPOSDAO, ICRUDable<MHCategoryAreaEntity>, IQueryable<MHCategoryAreaEntity>
    {
        public int GetMaxGroupId()
        {
            string sql =string.Format( "select max(groupId) groupId from MHCategoryArea a,mobileHome b where a.homeId = b.homeId " +
                         " and b.customerId ='{0}' ",this.CurrentUserInfo.ClientID);

            string result = this.SQLHelper.ExecuteScalar(sql).ToString();
            if (string.IsNullOrEmpty(result))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(result);
            }
        }
        public int GetMaxGroupId(string strHomeId)
        {
            string sql = string.Format("select max(groupId) groupId from MHCategoryArea a,mobileHome b where a.homeId = b.homeId " +
                         " and b.customerId ='{0}' and b.Homeid='{1}' ", this.CurrentUserInfo.ClientID,strHomeId);

            string result = this.SQLHelper.ExecuteScalar(sql).ToString();
            if (string.IsNullOrEmpty(result))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(result);
            }
        }
        /// <summary>
        /// 获取指定客户下的商品分类区域的项
        /// </summary>
        /// <returns></returns>
        public MHCategoryAreaEntity[] GetByCustomerID()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select b.* from dbo.MobileHome a left join dbo.MHCategoryArea b on a.homeid=b.homeid  where a.customerid='{0}' and  a.IsDelete=0 and b.IsDelete=0", this.CurrentUserInfo.ClientID);

            List<MHCategoryAreaEntity> list = new List<MHCategoryAreaEntity>();
            using (var rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MHCategoryAreaEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //
            return list.ToArray();
        }
    }
}
