/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
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
using System.Configuration;
using JIT.Utility.Web.ComponentModel.ExtJS.Menu;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表WMenu的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WMenuDAO : Base.BaseCPOSDAO, ICRUDable<WMenuEntity>, IQueryable<WMenuEntity>
    {
        #region 后台Web获取列表

        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebWMenuCount(WMenuEntity entity)
        {
            string sql = GetWebWMenuSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebWMenu(WMenuEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebWMenuSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                   beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetWebWMenuSql(WMenuEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.[Key] asc) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " ,c.Name ParentName ";
            sql += " ,e.ModelName ModelName ";
            sql += " ,f.MaterialTypeName MaterialTypeName ";
            sql += " into #tmp ";
            sql += " from [WMenu] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " left join [wmenu] c on a.parentId=c.Id ";
            sql += " left join [WApplicationInterface] d on a.WeiXinID=d.WeiXinID ";
            sql += " left join [WModel] e on a.ModelId=e.ModelId ";
            sql += " left join [WMaterialType] f on e.MaterialTypeId=f.MaterialTypeId ";
            sql += " where a.IsDelete='0' ";
            if (entity.Name != null && entity.Name.Trim().Length > 0)
            {
                sql += " and a.Name like '%" + entity.Name.Trim() + "%' ";
            }
            if (entity.WeiXinID != null && entity.WeiXinID.Trim().Length > 0)
            {
                sql += " and a.WeiXinID like '%" + entity.WeiXinID.Trim() + "%' ";
            }
            if (entity.Key != null && entity.Key.Trim().Length > 0)
            {
                sql += " and a.[Key] like '%" + entity.Key.Trim() + "%' ";
            }
            if (entity.Type != null && entity.Type.Trim().Length > 0)
            {
                sql += " and a.Type = '" + entity.Type.Trim() + "' ";
            }
            if (entity.Level != null && entity.Level.Trim().Length > 0)
            {
                sql += " and a.Level = '" + entity.Level.Trim() + "' ";
            }
            if (entity.ApplicationId != null && entity.ApplicationId.Trim().Length > 0)
            {
                sql += " and d.ApplicationId = '" + entity.ApplicationId.Trim() + "' ";
            }
            if (entity.ParentId != null)
            {
                sql += " and isnull(a.ParentId,'') = '" + entity.ParentId.Trim() + "' ";
            }
            sql += " order by a.[Key] asc ";
            return sql;
        }

        #endregion

        #region 后台Web获取列表

        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebWMenuTreeCount(WMenuEntity entity)
        {
            string sql = GetWebWMenuTreeSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebWMenuTree(WMenuEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebWMenuSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                   beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetWebWMenuTreeSql(WMenuEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.[Key] asc) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " ,c.Name ParentName ";
            sql += " into #tmp ";
            sql += " from [WMenu] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " left join [wmenu] c on a.parentId=c.Id ";
            sql += " left join [WApplicationInterface] d on a.WeiXinID=d.WeiXinID ";
            sql += " where a.IsDelete='0' ";
            if (entity.Name != null && entity.Name.Trim().Length > 0)
            {
                sql += " and a.Name like '%" + entity.Name.Trim() + "%' ";
            }
            if (entity.WeiXinID != null && entity.WeiXinID.Trim().Length > 0)
            {
                sql += " and a.WeiXinID like '%" + entity.WeiXinID.Trim() + "%' ";
            }
            if (entity.Key != null && entity.Key.Trim().Length > 0)
            {
                sql += " and a.[Key] like '%" + entity.Key.Trim() + "%' ";
            }
            if (entity.Type != null && entity.Type.Trim().Length > 0)
            {
                sql += " and a.Type = '" + entity.Type.Trim() + "' ";
            }
            if (entity.Level != null && entity.Level.Trim().Length > 0)
            {
                sql += " and a.Level = '" + entity.Level.Trim() + "' ";
            }
            if (entity.ApplicationId != null && entity.ApplicationId.Trim().Length > 0)
            {
                sql += " and d.ApplicationId = '" + entity.ApplicationId.Trim() + "' ";
            }
            if (entity.ParentId != null)
            {
                sql += " and isnull(a.ParentId,'') = '" + entity.ParentId.Trim() + "' ";
            }
            sql += " order by a.[Key] asc ";
            return sql;
        }

        #endregion

        #region 获取微信菜单

        /// <summary>
        /// 获取微信第一级菜单
        /// </summary>
        /// <param name="weixinID">公众平台账号</param>
        /// <returns></returns>
        public DataSet GetFirstMenus(string weixinID)
        {
            string sql = " SELECT * FROM dbo.WMenu "
                         + " WHERE WeiXinID = '" + weixinID + "' AND Level = 1 AND IsDelete = 0 and status = '1'"
                         + " ORDER BY   CONVERT(int, DisplayColumn ) ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取微信第二级菜单
        /// </summary>
        /// <param name="weixinID">公众平台账号</param>
        /// <param name="parentID">上级菜单ID</param>
        /// <returns></returns>
        public DataSet GetSecondMenus(string weixinID, string parentID)
        {
            string sql = " SELECT Name, [Type], [Key], [MenuURL], sub_button = '[]' FROM dbo.WMenu "
                         + " WHERE WeiXinID = '" + weixinID + "' AND Level = 2 AND IsDelete = 0 and status = '1' "
                         + " AND ParentId = '" + parentID + "' "
                         + " ORDER BY CONVERT(int, DisplayColumn ) ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 通过KEY值获取素材类型和素材ID

        /// <summary>
        /// 通过KEY值获取素材类型和素材ID
        /// </summary>
        /// <param name="weixinID">公众平台账号</param>
        /// <param name="key">菜单KEY值</param>
        /// <returns></returns>
        public DataSet GetMenusByKey(string weixinID, string eventKey)
        {
            string sql = " SELECT a.ModelId, b.MaterialTypeId, b.MaterialId FROM dbo.WMenu a "
                         + " INNER JOIN dbo.WModel b ON a.ModelId = b.ModelId "
                         + " WHERE a.IsDelete = 0 "
                         + " AND a.WeiXinID = '" + weixinID + "' AND a.[Key] = '" + eventKey + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetMenusByKeyJermyn(string weixinID, string eventKey)
        {
            string sql = " select a.* From WMenu a "
                        + " where a.IsDelete = '0' "
                        + " and a.WeiXinID = '" + weixinID + "' "
                        + " and a.[Key] = '" + eventKey + "' "
                        + " and a.[type] = 'click' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 根据微信公众账号获取数据库连接字符串

        /// <summary>
        /// 根据微信公众账号获取数据库连接字符串
        /// </summary>
        /// <param name="weixinID"></param>
        /// <returns></returns>
        public string GetCustomerConn(string weixinID)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn "
                + " from t_customer_connect a where a.customer_id = "
                + " (SELECT CustomerId FROM dbo.TCustomerWeiXinMapping WHERE WeiXinId = '{0}' AND IsDelete = 0) ",
                weixinID);

            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        #endregion

        #region 根据微信公众账号获取CustomerID

        /// <summary>
        /// 根据微信公众账号获取CustomerID
        /// </summary>
        /// <param name="weixinID"></param>
        /// <returns></returns>
        public string GetCustomerID(string weixinID)
        {
            string sql =
                string.Format(
                    "SELECT CustomerId FROM dbo.TCustomerWeiXinMapping WHERE WeiXinId = '{0}' AND IsDelete = 0",
                    weixinID);

            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        #endregion

        #region 根据customerCode获取CustomerID

        /// <summary>
        /// 根据微信公众账号获取CustomerID
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public string GetCustomerIDByCustomerCode(string customerCode)
        {
            string sql = string.Format("SELECT customer_id FROM dbo.t_customer WHERE customer_code = '{0}'",
                customerCode);

            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        #endregion

        #region Jermyn 20131107 根据微信号码获取客户标识

        public string GetCustomerIdByWx(string weixinID)
        {
            string sql = string.Format(
                "select CustomerId FROM dbo.TCustomerWeiXinMapping WHERE WeiXinId = '{0}' AND IsDelete = 0  ", weixinID);

            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        #endregion

        #region 获取微信菜单

        public DataSet GetMenuList(string customerId, string applicationId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter() {ParameterName = "@pApplicationId", Value = applicationId}
            };

            var sql = new StringBuilder();

            sql.Append(" select a.ID,isnull(a.WeiXinID,'')WeiXinID ,isnull(Name,'') Name,");
            sql.Append(" isnull(a.Level,0) Level,");
            sql.Append(" isnull(a.ParentId,'')ParentId,isnull(a.Status,0) Status,");
            sql.Append(
                " isnull(a.DisplayColumn,0) DisplayColumn, ");
            sql.Append(
                " a.CreateTime");
            sql.Append("  from WMenu a,WApplicationInterface b");
            sql.Append(" where a.WeiXinID = b.WeiXinID ");
            sql.Append(" and a.IsDelete = 0 and b.IsDelete = 0 and isnull(a.status,-1) <> -1 ");
            sql.Append(" and b.applicationId = @pApplicationId");
            sql.Append(" and b.CustomerId = @pCustomerId");
            sql.Append(" order by  CONVERT(int, a.DisplayColumn )");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public DataSet GetMenuDetail(string menuId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pMenuId", Value = menuId}
            };

            var sql = new StringBuilder();

            sql.Append(" select a.ID,isnull(a.WeiXinID,'')WeiXinID ,isnull(Name,'') Name,");
            sql.Append(" isnull(a.Type,'') Type,isnull(a.Level,0) Level,a.PageId,");
            sql.Append(
                " isnull(a.ParentId,'')ParentId,isnull(a.Status,0) Status,isnull(materialTypeId,0) materialTypeId,");
            sql.Append(
                " isnull(a.Text,'') Text,isnull(a.DisplayColumn,0) DisplayColumn,isnull(a.BeLinkedType,0) BeLinkedType, ");
            sql.Append(" isnull(a.MenuURL,'') MenuUrl,isnull(a.ImageId,'') ImageId,isnull(c.imageUrl,'') imageUrl,");
            sql.Append(
                " isnull(a.PageParamJson,'') PageParamJson,isnull(a.PageUrlJson,'') PageUrlJson from WMenu a left join WmaterialImage c");
            sql.Append(" on a.imageId = c.imageId");
            sql.Append(" where a.IsDelete = 0 ");
            sql.Append(" and a.Id = @pMenuId");
            sql.Append(" order by a.DisplayColumn");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        #endregion

        public DataSet GetMenuTextIdList(string customerId, string weiXinId, string menuList)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter() {ParameterName = "@pWeiXinId", Value = weiXinId}
            };

            var sql = new StringBuilder();
            sql.Append("select a.TextId,b.ID MenuId from WMenu b left join WMenuMTextMapping c on c.MenuId = b.ID ");
            sql.Append(" left join WMaterialText a on a.TextId = c.TextId");
            sql.Append("  where  ");
            sql.Append(" c.customerId = @pCustomerId and b.WeiXinID = @pWeiXinId");
            sql.Append(" and a.IsDelete = 0 and b.IsDelete = 0 and c.isdelete = 0");
            sql.AppendFormat(" and b.ID in ({0})", menuList);

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }


        public DataSet GetMenuTextIdListByMenuId(string customerId, string menuId)
        {
            var paras = new List<SqlParameter>
            {
            //    new SqlParameter() {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter() {ParameterName = "@pMenuId", Value = menuId}
            };

            var sql = new StringBuilder();

            sql.Append(" select c.ID MenuId,a.TextId,a.Title,a.CoverImageUrl,b.DisplayIndex,a.Text,a.OriginalUrl,a.Author");
            sql.Append(" from WMenu c left join WMenuMTextMapping b on b.MenuId = c.id and b.isdelete = 0 left join WMaterialText a on a.TextId = b.TextId and a.isdelete = 0");
            sql.Append(" where  ");
            sql.Append(" c.id =  @pMenuId   ");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        #region 根据一级节点获取该节点下状态为启用的二级节点数量

        public int GetLevel2CountByMenuId(string menuId, string applicationId, string customerid)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pMenuId", Value = menuId},
                     new SqlParameter() {ParameterName = "@pApplicationId", Value = applicationId},
                          new SqlParameter() {ParameterName = "@pCustomerId", Value = customerid},
            };
            var sql = new StringBuilder();
            sql.Append(@"select isnull(count(1),0) from wmenu  a inner join wapplicationinterface b on a.weixinid=b.weixinid
                                     where a.isdelete = 0 and a.parentid = @pMenuId and a.status = 1
                                                and b.applicationId = @pApplicationId
				                        and b.CustomerId = @pCustomerId");
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));
        }

        public int GetLevel2CountByDisplayColumn(string parentid, string menuId, int DisplayColumn, string applicationId, string customerid)
        {
            var paras = new List<SqlParameter>
            {
               
                     new SqlParameter() {ParameterName = "@pApplicationId", Value = applicationId},
                          new SqlParameter() {ParameterName = "@pCustomerId", Value = customerid},
                              new SqlParameter() {ParameterName = "@parentid", Value = parentid},
                                     new SqlParameter() {ParameterName = "@DisplayColumn", Value = DisplayColumn}
            };
       
            var sql = new StringBuilder();
            sql.Append(@"select isnull(count(1),0) from wmenu  a inner join wapplicationinterface b on a.weixinid=b.weixinid
                                     where a.isdelete = 0 
                                        and a.parentid = @parentid 
                                                and a.status = 1    ---还是加上status=1，不算没启用的
                                                 and a.DisplayColumn=@DisplayColumn
                                                and b.applicationId = @pApplicationId
                                            
				                        and b.CustomerId = @pCustomerId");
            if (!string.IsNullOrEmpty(menuId))
            {
                paras.Add(new SqlParameter() { ParameterName = "@menuId", Value = menuId });
                sql.Append(" and  a.Id!=@menuId");
            }
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));
        }

        #endregion

        public void UpdateMenuData(string menuId, int status, Guid? pageid, string pageParamJson, string pageUrlJson,
            int unionTypeId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pMenuId", Value = menuId},
                new SqlParameter() {ParameterName = "@pStatus", Value = status},
                new SqlParameter() {ParameterName = "@pPageId", Value = pageid},
                new SqlParameter() {ParameterName = "@pPageParamJson", Value = pageParamJson},
                new SqlParameter() {ParameterName = "@pPageUrlJson", Value = pageUrlJson},
                new SqlParameter() {ParameterName = "@pBeLinkedType", Value = unionTypeId}
            };
            var sql = new StringBuilder();

            sql.Append(
                "update wmenu set status =@pStatus,pageid = @pPageId,pageParamJson = @pPageParamJson,BeLinkedType = @pBeLinkedType,");
            sql.Append("pageUrlJson = @pPageUrlJson where id = @pMenuId");

            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        #region 检查要删除的模板
        /// <summary>
        /// 检查删除的模板是否关联
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool CheckDelete(string ids)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"select count(1) from WModelTextMapping 
      left join WMaterialText on WModelTextMapping.TextId=WMaterialText.TextId where WModelTextMapping.ModelId in ( {0} ) and WMaterialText.IsDelete='0'", ids);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            if (ds != null && ds.Tables[0] != null && ((int)ds.Tables[0].Rows[0][0]) > 0)
            {
                return true;
            }
            return false;
        }

        #endregion


        public bool CheckExistLevel2Menu(string menuId)
        {
            var paras = new List<SqlParameter> {new SqlParameter() {ParameterName = "@pMenuId", Value = menuId}};

            var sql = new StringBuilder();
            sql.Append("if exists(select 1 from WMenu where ParentId = @pMenuId and IsDelete = 0 and Status = 1)");
            sql.Append("begin select 1 end else begin select 0 end ");

            int result = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));

            return result == 1;
        }
    }
}
