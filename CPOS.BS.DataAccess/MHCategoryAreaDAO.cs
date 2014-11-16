/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/4 20:29:56
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
using System.Text;

using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// MHCategoryAreaDAO 
    /// </summary>
    public partial class MHCategoryAreaDAO
    {
        /// <summary>
        /// 获取指定客户下的商品分类区域的项
        /// </summary>
        /// <returns></returns>
        public MHCategoryAreaEntity[] GetByCustomerID()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select b.* from dbo.MobileHome a left join dbo.MHCategoryArea b on a.homeid=b.homeid  where a.customerid='{0}'",this.CurrentUserInfo.ClientID);

            List<MHCategoryAreaEntity> list=new List<MHCategoryAreaEntity>();
            using(var rdr =this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while(rdr.Read())
                {
                    MHCategoryAreaEntity m;
                    this.Load(rdr,out m);
                    list.Add(m);
                }
            }
            //
            return list.ToArray();
        }
    }
}
