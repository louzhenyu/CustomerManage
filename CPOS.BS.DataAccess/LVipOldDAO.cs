/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/16 10:28:54
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
    /// 表LVipOld的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LVipOldDAO : Base.BaseCPOSDAO, ICRUDable<LVipOldEntity>, IQueryable<LVipOldEntity>
    {
        #region GetVipListForMsgSend
        /// <summary>
        /// 获取要发送消息的用户列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet GetVipListForMsgSend(LVipOldEntity entity)
        {
            string sql = "select a.* ";
            sql += " from LVipOld a ";
            sql += " where a.isDelete='0' ";
            sql += " and (a.Mobile is not null and len(a.Mobile)>10) ";
            sql += " and (a.IsPush is null or a.IsPush=0) ";
            sql += " and (a.PushCount is null or a.PushCount<3) ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
