/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/11/4 14:35:50
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
    /// 表MHFollow的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MHFollowDAO : Base.BaseCPOSDAO, ICRUDable<MHFollowEntity>, IQueryable<MHFollowEntity>
    {
        
        public DataSet GetFollowInfo(string strHomeId)
        {
            string strSql = "SELECT  a.FollowId,a.TextId,a.Url,a.TypeId,a.title,B.Author ,B.CoverImageUrl ,B.Text ,B.OriginalUrl,b.Author";
            strSql += " FROM    dbo.MHFollow A";
            strSql += " LEFT JOIN dbo.WMaterialText B ON A.TextId = B.TextId AND A.TypeId=35";
            strSql += " WHERE A.HomeId='" + strHomeId + "' and isDelete=0";
            return this.SQLHelper.ExecuteDataset(CommandType.Text, strSql);
        }
    }
}
