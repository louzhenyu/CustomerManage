/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/21 16:06:37
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
    /// 表SysModulePageMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SysModulePageMappingDAO : BaseCPOSDAO, ICRUDable<SysModulePageMappingEntity>, IQueryable<SysModulePageMappingEntity>
    {
         /// <summary>
        /// 查询数据库表SysModulePageMapping 中是否存在该MappingId和PageId的数据 Add By changjian.tian 2014-05-26
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public DataSet GetExistsVocaVerMappingIDandPageId(string MappingId,string PageId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine(string.Format("select * from SysModulePageMapping where VocaVerMappingID='{0}' and PageId='{1}'", MappingId,PageId));
            DataSet ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }

        /// <summary>
        /// 查询数据库表SysModulePageMapping 返回次序
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public object GetModulePageMappingBySequence(string MappingId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine(string.Format("select Max(Sequence) as Sequence from SysModulePageMapping where VocaVerMappingID='{0}'", MappingId));
            object Sequence= this.SQLHelper.ExecuteScalar(sbSQL.ToString());
            return Sequence;
        }

        public bool DeleteMappingByIds(string propIds, string PageID)
        {
            string sql = "delete from  SysModulePageMapping  ";
           // sql += " isdelete=1 ";
        //    sql += " ,Last='" +  + "' ";
           // sql += " ,LastUpdateTime='" + propInfo.Modify_Time + "' ";
            sql += " where PageID='" + PageID + "'";
            sql += " and VocaVerMappingID not in (" + propIds + ") ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
    }
}
