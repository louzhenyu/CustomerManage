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
    /// 表WQRCodeManager的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WQRCodeManagerDAO : Base.BaseCPOSDAO, ICRUDable<WQRCodeManagerEntity>, IQueryable<WQRCodeManagerEntity>
    {

        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(WQRCodeManagerEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(WQRCodeManagerEntity entity, int Page, int PageSize)
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
        private string GetListSql(WQRCodeManagerEntity entity)
        {
            string OrderBy = "a.createTime desc";
            if (entity.OrderBy != null && entity.OrderBy.Length > 0) 
                OrderBy = entity.OrderBy;

            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,b.TypeName QRCodeTypeName ";
            sql += " ,DisplayIndex = row_number() over(order by " + OrderBy + " ) ";
            sql += " into #tmp ";
            sql += " from WQRCodeManager a ";
            sql += " left join WQRCodeType b on (a.QRCodeTypeId=b.QRCodeTypeId and b.isDelete='0') ";
            sql += " where a.IsDelete='0' ";
            sql += " and a.customerId='"+CurrentUserInfo.CurrentUser.customer_id+"' ";
            if (entity.QRCodeId != null)
            {
                sql += " and a.QRCodeId = '" + entity.QRCodeId + "' ";
            }
            if (entity.QRCodeTypeId != null)
            {
                sql += " and a.QRCodeTypeId = '" + entity.QRCodeTypeId + "' ";
            }
            if (entity.QRCode != null && entity.QRCode.Trim().Length > 0)
            {
                sql += " and a.QRCode like '%" + entity.QRCode + "%' ";
            }
            if (entity.ApplicationId != null)
            {
                sql += " and a.ApplicationId = '" + entity.ApplicationId + "' ";
            }
            if (entity.IsUse != null)
            {
                sql += " and a.IsUse = '" + entity.IsUse + "' ";
            }
            if (entity.ObjectId != null && entity.ObjectId.Length > 0)
            {
                sql += " and a.ObjectId = '" + entity.ObjectId + "' ";
            }
            return sql;
        }
        #endregion


        #region 获取当前二维码 最大值

        public int GetMaxWQRCod()
        {
            var param = new List<SqlParameter>{
            new SqlParameter{ParameterName="@CustomerId", Value=this.CurrentUserInfo.ClientID}
            };

            StringBuilder strb = new StringBuilder(@"
            select max(convert(int,isnull(qrcode,0))) qrcode from WQRCodeManager where isdelete=0 and CustomerId=@CustomerId");
           object obj=  this.SQLHelper.ExecuteScalar(CommandType.Text, strb.ToString(), param.ToArray());
           if (obj == null || obj is DBNull || obj.ToString() == "")
           {
               return 0;
           }
           return (int)obj;
        }
        #endregion


    }
}
