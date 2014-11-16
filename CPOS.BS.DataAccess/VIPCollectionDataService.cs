using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.DataAccess
{
    public class VIPCollectionDataService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public VIPCollectionDataService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        public DataSet GetCollectionPropertyList(VIPCollectionDatEntity queryEntity, int pageIndex, int pageSize)
        {
            int beginSize = pageIndex *pageSize + 1;
            int endSize = pageIndex * pageSize + pageSize;
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from dbo.vw_VIPCollectionData a ";
            sql += " where a.IsDelete='0' ";
            if (queryEntity.VIPID != null && queryEntity.VIPID.Trim().Length > 0)
            {
                sql += " and a.VIPID = '" + queryEntity.VIPID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            DataSet ds = new DataSet();
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public int GetCollectionPropertyListCount(VIPCollectionDatEntity queryEntity)
        {
            string sql = "select count(*) from vw_VIPCollectionData where VIPID='" + queryEntity.VIPID + "'";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
    }
}
