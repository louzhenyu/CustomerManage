/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/16 11:07:22
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
using System.Runtime.InteropServices;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Report;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表MobileBussinessDefined的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MobileBussinessDefinedDAO : BaseCPOSDAO, ICRUDable<MobileBussinessDefinedEntity>, IQueryable<MobileBussinessDefinedEntity>
    {
        public DataSet GetPagesInfo(string eventCode, string customerId)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });
            paras.Add(new SqlParameter() { ParameterName = "@pEventCode", Value = eventCode });

            StringBuilder sql = new StringBuilder();


            sql.Append(" create table #tmp1(id uniqueidentifier);");
            sql.Append(" if exists(select 1 from MobilePageBlock mpb ");
            sql.Append(" inner join MobileModule mm on mpb.MobileModuleID = mm.MobileModuleID");
            sql.Append(" inner join MobileModuleObjectMapping mmo on mm.MobileModuleID = mmo.MobileModuleID ");
            sql.Append(" where mpb.IsDelete = 0 and mm.IsDelete = 0 and mmo.IsDelete = 0 ");
            sql.Append(" and mmo.CustomerID =  @pCustomerId and mmo.ObjectID = '1')");
            sql.Append(" begin");
            sql.Append(" insert into #tmp1 ");
            sql.Append(" select top 1 mpb.MobilePageBlockID  ");
            sql.Append(" from ");
            sql.Append(" MobilePageBlock mpb ");
            sql.Append(" inner join MobileModule mm on mpb.MobileModuleID = mm.MobileModuleID");
            sql.Append(" inner join MobileModuleObjectMapping mmo on mm.MobileModuleID = mmo.MobileModuleID ");
            sql.Append(" where mpb.IsDelete = 0 and mm.IsDelete = 0 and mmo.IsDelete = 0 ");
            sql.Append(" and mmo.CustomerID =  @pCustomerId and mmo.ObjectID = '1' and mpb.[Type]='1'  order by mmo.lastupdatetime desc ");
            sql.Append(" end");
            sql.Append(" else begin");
            sql.Append(" insert into #tmp1 select top 1 mpb.MobilePageBlockID  from ");
            sql.Append(" MobilePageBlock mpb inner join MobileModule mm on mpb.MobileModuleID = mm.MobileModuleID");
            sql.Append(" inner join MobileModuleObjectMapping mmo on mm.MobileModuleID = mmo.MobileModuleID");
            sql.Append(" inner join LEvents l on l.EventID = mmo.objectid");
            sql.Append(" inner join LEventsGenre le on l.EventGenreId = le.EventGenreId");
            sql.Append(" where mpb.IsDelete = 0 and mm.isdelete =0 and mmo.isdelete = 0 ");
            sql.Append(" and l.IsDelete=0 and le.isdelete = 0 and mpb.[Type]='1' ");
            sql.Append(" and le.GenreCode = @pEventCode and (isnull(l.CustomerId,'') = '' or l.CustomerId = @pCustomerId)");
            sql.Append("  order by mmo.lastupdatetime desc end");

            //表单页信息
            sql.Append(" select b.ID ,isnull(a.sort,0) as DisplayIndex from MobilePageBlock a,#tmp1 b where a.MobilePageBlockID = b.id and type = 1;");

            sql.Append(" create table #tmp2(id uniqueidentifier);");
            sql.Append(" insert into #tmp2");
            sql.Append(" select distinct b.ID from MobilePageBlock a,#tmp1 b where a.MobilePageBlockID = b.id and type = 1;");

            //块信息
            sql.Append(" select a.MobilePageBlockID as ID,a.ParentID as PageId, isnull(a.sort,0) as DisplayIndex from MobilePageBlock a,#tmp2 b where a.ParentID = b.id and type = 2;");


            sql.Append(" create table #tmp3(id uniqueidentifier);");
            sql.Append(" insert into #tmp3");
            sql.Append(" select distinct a.MobilePageBlockID from MobilePageBlock a,#tmp1 b where a.ParentID = b.id and type = 2;");


            //扩展属性定义信息
            sql.Append(" select b.MobileBussinessDefinedID ,c.id as BlockId,a.Title,isnull(b.EditOrder,0) as DisplayIndex");
            sql.Append(" from MobilePageBlock a, MobileBussinessDefined b,#tmp3 c where a.MobilePageBlockID = c.id");
            sql.Append(" and a.MobilePageBlockID = b.MobilePageBlockID and a.isdelete = 0 and b.isdelete = 0;");

            //控件信息
            sql.Append(" select a.MobileBussinessDefinedID,a.DisplayType,a.ColumnDescEn, IsNull(a.IsMustDo, 0) as IsMustDo, ControlType, columnDesc, CorrelationValue from MobileBussinessDefined a, #tmp3 b where");
            sql.Append(" a.MobilePageBlockID = b.id and a.IsDelete = 0 order by a.EditOrder ");

            //值信息
            sql.Append(" select a.MobileBussinessDefinedID, c.OptionValue,c.OptionText,isnull(c.Sequence,0) Sequence from ");
            sql.Append(" MobileBussinessDefined a,#tmp3 b,Options c ");
            sql.Append(" where a.MobilePageBlockID = b.id and a.CorrelationValue = c.OptionName");
            sql.Append(" and (isnull(c.CustomerID,'') = '' or c.CustomerID = @pCustomerId) and a.IsDelete = 0 and a.DisplayType = 5 and c.isdelete = 0");


            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public string GetColumnName(string id)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pId", Value = id });

            string sql = "select columnName from MobileBussinessDefined where MobileBussinessDefinedID = @pId";

            return this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()).ToString();
        }

        public void UpdateDynamicColumnValue(string columnList, string userId, string tableName)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };

            //  paras.Add(new SqlParameter() { ParameterName = "@pColumnList", Value = columnList });
            paras.Add(new SqlParameter() { ParameterName = "@pUserId", Value = userId });

            string sql = "update " + tableName + " set " + columnList + " where vipId = @pUserId";


            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras.ToArray());

        }

        public void EditMobileBussinessDefined(MobileBussinessDefinedEntity[] addEntities, MobileBussinessDefinedEntity[] updatEntities, MobileBussinessDefinedEntity[] deletEntities)
        {
            using (var tran = SQLHelper.CreateTransaction())
            using (tran.Connection)
            {
                try
                {
                    if (deletEntities != null && deletEntities.Length > 0)
                    {
                        Delete(deletEntities, tran);
                    }
                    if (addEntities != null && addEntities.Length > 0)
                    {
                        foreach (var entity in addEntities)
                        {
                            Create(entity, tran);
                        }
                    }
                    if (updatEntities != null && updatEntities.Length > 0)
                    {
                        foreach (var entity in updatEntities)
                        {
                            Update(entity, tran);
                        }
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 获取一个MobilePageBlockID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetMobilePageBlockIDByMobileModuleID(object id)
        {
            if (id == null)
            {
                return null;
            }
            const string sql = "select top 1 MobilePageBlockID from MobilePageBlock where IsDelete = 0 and MobileModuleID = @MobileModuleID order by Sort";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MobileModuleID", SqlDbType.NVarChar) 
            };
            parameters[0].Value = id.ToString();
            var result = SQLHelper.ExecuteScalar(CommandType.Text, sql, parameters);
            if (result == null || result == DBNull.Value)
            {
                return null;
            }
            return result.ToString();
        }

        public DataSet GetObjectPagesInfo(string objectId, string customerId)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });
            paras.Add(new SqlParameter() { ParameterName = "@pObjectId", Value = objectId });

            StringBuilder sql = new StringBuilder();
            sql.Append(" create table #tmp1(id uniqueidentifier);");
            sql.Append(" insert into #tmp1 select distinct mpb.MobilePageBlockID  from ");
            sql.Append(" MobilePageBlock mpb inner join MobileModule mm on mpb.MobileModuleID = mm.MobileModuleID");
            sql.Append(" inner join MobileModuleObjectMapping mmo on mm.MobileModuleID = mmo.MobileModuleID");
            //sql.Append(" inner join LEvents l on l.EventID = mmo.objectid");
            //sql.Append(" inner join LEventsGenre le on l.EventGenreId = le.EventGenreId");
            sql.Append(" where mpb.IsDelete = 0 and mm.isdelete =0 and mmo.isdelete = 0 ");
            //  sql.Append(" and l.IsDelete=0 and le.isdelete = 0");
            sql.Append(" and mmo.objectId = @pObjectId and (isnull(mm.CustomerId,'') = '' or mm.CustomerId = @pCustomerId);");

            //表单页信息
            sql.Append(" select b.ID ,isnull(a.sort,0) as DisplayIndex,isnull(c.IsVerification,0) IsVerification from MobilePageBlock a,");
            sql.Append(" #tmp1 b,MobileModule c where a.MobilePageBlockID = b.id and a.MobileModuleID = c.MobileModuleID and type = 1;");

            sql.Append(" create table #tmp2(id uniqueidentifier);");
            sql.Append(" insert into #tmp2");
            sql.Append(" select distinct b.ID from MobilePageBlock a,#tmp1 b where a.MobilePageBlockID = b.id and type = 1;");

            //块信息
            sql.Append(" select a.MobilePageBlockID as ID,a.ParentID as PageId, isnull(a.sort,0) as DisplayIndex");
            sql.Append(" from MobilePageBlock a,#tmp2 b where a.ParentID = b.id and type = 2;");


            sql.Append(" create table #tmp3(id uniqueidentifier);");
            sql.Append(" insert into #tmp3");
            sql.Append(" select distinct a.MobilePageBlockID from MobilePageBlock a,#tmp1 b where a.ParentID = b.id and type = 2;");


            //扩展属性定义信息
            sql.Append(" select b.MobileBussinessDefinedID ,c.id as BlockId,a.Title,isnull(b.EditOrder,0) as DisplayIndex");
            sql.Append(" from MobilePageBlock a, MobileBussinessDefined b,#tmp3 c where a.MobilePageBlockID = c.id");
            sql.Append(" and a.MobilePageBlockID = b.MobilePageBlockID and a.isdelete = 0 and b.isdelete = 0;");

            //控件信息
            sql.Append(" select  a.MobileBussinessDefinedID, TableName, ControlType,ColumnDesc, ColumnName,CorrelationValue from MobileBussinessDefined a,#tmp3 b where");
            sql.Append(" a.MobilePageBlockID = b.id and a.IsDelete = 0");

            //值信息
            sql.Append(" select a.MobileBussinessDefinedID, c.OptionValue,c.OptionText,isnull(c.Sequence,0) Sequence from ");
            sql.Append(" MobileBussinessDefined a,#tmp3 b,Options c ");
            sql.Append(" where a.MobilePageBlockID = b.id and a.CorrelationValue = c.OptionName");
            sql.Append(" and (isnull(c.CustomerID,'') = '' or c.CustomerID = @pCustomerId) and a.IsDelete = 0 and a.DisplayType = 5 and c.isdelete = 0");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public string GetTableNameByObjectId(string objectId, string customerId)
        {
            string sqlWhere = "";
            if (!string.IsNullOrEmpty(customerId))
            {
                sqlWhere = "and a.customerid='" + customerId + "'";
            }
            string sql = "select distinct d.TableName from MobileModuleObjectMapping a,MobileModule b,MobilePageBlock c,"
                + " MobileBussinessDefined d where a.MobileModuleID = b.MobileModuleID"
                + " and b.MobileModuleID = c.MobileModuleID"
                + " and c.MobilePageBlockID = d.MobilePageBlockID"
                + " and a.IsDelete = 0 and b.IsDelete = 0"
                + " and c.IsDelete = 0 and d.IsDelete = 0"
                + " and a.objectId = '" + objectId + "' "
                + sqlWhere;

            var result = this.SQLHelper.ExecuteScalar(sql);
            if (result == null || result.ToString() == "" || string.IsNullOrEmpty(result.ToString()))
            {
                return "";
            }
            else
            {
                return result.ToString();
            }

        }

        public DataSet GetValue(string getValueSql)
        {
            return this.SQLHelper.ExecuteDataset(getValueSql);
        }
    }
}
