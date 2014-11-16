/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/3 10:59:10
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
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表LNewsType的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LNewsTypeDAO : Base.BaseCPOSDAO, ICRUDable<LNewsTypeEntity>, IQueryable<LNewsTypeEntity>
    {
        protected string _pTableName = "LNewsType";

        #region 构造函数

        /// <summary>
        /// 构造函数 
        /// </summary>
        public LNewsTypeDAO(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo)
        {
            this._pTableName = pTableName;
        }

        #endregion

        #region DelLNewsTypeByID

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="lNewsTypeID">类别ID</param>
        /// <returns></returns>
        public int DelLNewsTypeByID(string lNewsTypeID)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"
               declare @rowcount int
               select @rowcount=count(1) from LNews where NewsType='{0}' and IsDelete='0' and CustomerId='{1}' 
               if @rowcount=0
               begin 
               declare @pareentrowcount int
               select  @pareentrowcount=count(1) from LNewsType where  IsDelete='0' and ParentTypeId='{0}' and CustomerId='{1}'
                  if @pareentrowcount=0
                   begin
                   update LNewsType set IsDelete='1' where NewsTypeId='{0}'  
                   end 
               end", lNewsTypeID, CurrentUserInfo.ClientID);

            int i = this.SQLHelper.ExecuteNonQuery(strb.ToString());
            return i;
        }

        #endregion

        #region GetLNewsTypeList

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<LNewsTypeEntity> GetLNewsTypeList(IWhereCondition[] pWhereConditions, int pageIndex,
            int pageSize)
        {
            StringBuilder strbWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    strbWhere.AppendFormat(" AND {0}", item.GetExpression());
                }
            }
            UtilityEntity model = new UtilityEntity();
            string sql = @"select lt.NewsTypeId,lt.NewsTypeName,lt.ParentTypeId,ltp.NewsTypeName as ParentTypeName,lt.CreateTime,lt.isdelete,lt.CustomerId,lt.TypeLevel
                         from LNewsType as lt 
                         left join LNewsType as ltp on ltp.NewsTypeId=lt.ParentTypeId and ltp.IsDelete='0'
                         where lt.IsDelete='0' and lt.CustomerId='" + this.CurrentUserInfo.ClientID + "'";
            model.TableName = "(" + string.Format(sql, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID) +
                              strbWhere.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = " CreateTime desc";
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<LNewsTypeEntity> pEntity = new PagedQueryResult<LNewsTypeEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<LNewsTypeEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }

        #endregion



        public void Update(LNewsTypeEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, null, pIsUpdateNullField);
        }

        #region GetPartentNewsType

        /// <summary>
        /// 查询所有当前类别
        /// </summary>
        /// <returns></returns>
        public DataSet GetPartentNewsType()
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(
                @" select NewsTypeId,NewsTypeName,ParentTypeId from LNewsType where IsDelete='0' and CustomerId='{0}'",
                CurrentUserInfo.ClientID);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;
        }

        #endregion

        #region MyRegion

        /// <summary>
        /// 判断是否同名
        /// </summary>
        /// <param name="lNewsTypeID"></param>
        /// <param name="newsTypeName"></param>
        /// <returns></returns>
        public bool IsSameName(string lNewsTypeID, string newsTypeName)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(
                " select NewsTypeId from LNewsType where NewsTypeName='{0}' and NewsTypeId<>'{1}' and IsDelete='0' and CustomerId='{2}' ",
                newsTypeName, lNewsTypeID, CurrentUserInfo.ClientID);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        public DataSet GetNewsTypeList(string customerId, int pageIndex, int pageSize)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@pCustomerId", Value = customerId},
            };

            var sql = new StringBuilder();

            sql.Append("select * from (");
            sql.Append(
                " select  row_number()over(order by createTime desc) as _row, ISNULL(NewsTypeId,'') NewsTypeId,ISNULL(NewsTypeName,'') NewsTypeName from LNewsType");
            sql.Append(" where IsDelete = 0 and CustomerId = @pCustomerId");
            sql.Append(") t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||【新版资讯管理】Alan |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
        /// <summary>
        /// 查询所有当前类别
        /// </summary>
        /// <param name="parentID">分类父ID</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetNewsTypes(string parentID)
        {
            //Instance Obj
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" select NewsTypeId,NewsTypeName,ParentTypeId,TypeLevel from LNewsType ");
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' ", CurrentUserInfo.ClientID);

            if (!string.IsNullOrEmpty(parentID))
            {
                sbSQL.AppendFormat("and ParentTypeId = '{0}' ", parentID);
            }

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        /// <summary>
        /// 获取分类列表：分页
        /// </summary>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortOrder">排序方式：0 升序 1 降序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>数据集</returns>
        public DataSet GetTypeListPage(string sortField, int sortOrder, int pageIndex, int pageSize)
        {
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }

            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select NewsTypeId, NewsTypeName, ParentTypeId, TypeLevel, IsDelete, LastUpdateTime, LastUpdateBy, CreateBy, CreateTime, CustomerId, IsVisble,ParentTypeName from(");
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by C.{0} {1}) rowNum ,C.NewsTypeId, C.NewsTypeName, C.ParentTypeId, C.TypeLevel, C.IsDelete, C.LastUpdateTime, C.LastUpdateBy, C.CreateBy, C.CreateTime, C.CustomerId, C.IsVisble,P.NewsTypeName ParentTypeName from LNewsType C ", sortField, sort);
            sbSQL.Append("left join LNewsType P on P.IsDelete=0 and P.CustomerId=C.CustomerId and P.NewsTypeId=C.ParentTypeId ");
            sbSQL.AppendFormat("where C.IsDelete = 0 and C.CustomerId = '{0}') as Res ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("where rowNum between {0} and {1} ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.AppendFormat("select COUNT(NewsTypeId) from LNewsType where IsDelete = 0 and CustomerId = '{0}' ;", CurrentUserInfo.ClientID);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
    }
}
