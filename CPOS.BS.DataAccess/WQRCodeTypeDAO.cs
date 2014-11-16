/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/21 19:20:01
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
    /// 表WQRCodeType的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WQRCodeTypeDAO : Base.BaseCPOSDAO, ICRUDable<WQRCodeTypeEntity>, IQueryable<WQRCodeTypeEntity>
    {

        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(WQRCodeTypeEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(WQRCodeTypeEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(WQRCodeTypeEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.*,c.ModelId,d.ApplicationId  ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from WQRCodeType a ";
            sql += " left join WQRCodeTypeModelMapping b on (a.QRCodeTypeId=b.QRCodeTypeId and b.isDelete='0') ";
            sql += " left join WModel c on (b.ModelId=c.ModelId and c.isDelete='0') ";
            sql += " left join WApplicationInterface d on (d.ApplicationId=c.ApplicationId and d.isDelete='0') ";
            sql += " where a.IsDelete='0' ";
            if (entity.QRCodeTypeId != null)
            {
                sql += " and a.QRCodeTypeId = '" + entity.QRCodeTypeId + "' ";
            }
            if (entity.TypeCode != null && entity.TypeCode.Trim().Length > 0)
            {
                sql += " and a.TypeCode like '%" + entity.TypeCode + "%' ";
            }
            if (entity.TypeName != null && entity.TypeName.Trim().Length > 0)
            {
                sql += " and a.TypeName like '%" + entity.TypeName + "%' ";
            }
            if (entity.ApplicationId != null && entity.ApplicationId.Trim().Length > 0)
            {
                sql += " and d.ApplicationId = '" + entity.ApplicationId + "' ";
            }
            return sql;
        }
        #endregion

    }
}
