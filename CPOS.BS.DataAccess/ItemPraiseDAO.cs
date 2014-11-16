/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/15 10:12:45
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
    /// 表ItemPraise的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ItemPraiseDAO : BaseCPOSDAO, ICRUDable<ItemPraiseEntity>, IQueryable<ItemPraiseEntity>
    {
        public bool JudgeItemPraiseByUser(string itemId, string userId)
        {
            bool b = false;
            string sql = string.Format("if exists(select 1 from itemPraise where itemId ='{0}' and vipId = '{1}') select 1 else select 0 ", itemId, userId);

            int count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));

            if (count == 0)
            {
                b = false;
            }
            else
            {
                b = true;
            }
            return b;
        }
    }
}
