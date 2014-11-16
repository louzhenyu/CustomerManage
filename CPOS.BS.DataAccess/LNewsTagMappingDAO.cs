/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/25 14:38:12
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表LNewsTagMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LNewsTagMappingDAO : Base.BaseCPOSDAO, ICRUDable<LNewsTagMappingEntity>, IQueryable<LNewsTagMappingEntity>
    {
        #region
        public bool DeleteByNewsId(string NewsId)
        {
            string sql = "Update LNewsTagMapping set IsDelete = '1',LastUpdateTime=GETDATE(),LastUpdateBy='"+this.CurrentUserInfo.CurrentUser.User_Id+"' where newsid = '" + NewsId + "'";
            this.SQLHelper.ExecuteScalar(sql);
            return true;
        }
        #endregion
    }
}
