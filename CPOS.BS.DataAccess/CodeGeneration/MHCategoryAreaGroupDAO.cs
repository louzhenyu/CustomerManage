/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/11/2 21:32:01
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
    /// 表MHCategoryAreaGroup的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MHCategoryAreaGroupDAO : Base.BaseCPOSDAO, ICRUDable<MHCategoryAreaGroupEntity>, IQueryable<MHCategoryAreaGroupEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MHCategoryAreaGroupDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(MHCategoryAreaGroupEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(MHCategoryAreaGroupEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [MHCategoryAreaGroup](");
            strSql.Append("[ModelName],[ModelDesc],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ModelTypeId],[CustomerID],[GroupValue],[StyleType],[TitleName],[TitleStyle],[ShowCount],[ShowName],[ShowPrice],[ShowSalesPrice],[ShowDiscount],[ShowSalesQty],[DisplayIndex],[HomeId])");
            strSql.Append(" values (");
            strSql.Append("@ModelName,@ModelDesc,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ModelTypeId,@CustomerID,@GroupValue,@StyleType,@TitleName,@TitleStyle,@ShowCount,@ShowName,@ShowPrice,@ShowSalesPrice,@ShowDiscount,@ShowSalesQty,@DisplayIndex,@HomeId)");
            strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@ModelName",SqlDbType.NVarChar),
					new SqlParameter("@ModelDesc",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ModelTypeId",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@GroupValue",SqlDbType.Int),
					new SqlParameter("@StyleType",SqlDbType.VarChar),
					new SqlParameter("@TitleName",SqlDbType.VarChar),
					new SqlParameter("@TitleStyle",SqlDbType.VarChar),
					new SqlParameter("@ShowCount",SqlDbType.Int),
					new SqlParameter("@ShowName",SqlDbType.Int),
					new SqlParameter("@ShowPrice",SqlDbType.Int),
					new SqlParameter("@ShowSalesPrice",SqlDbType.Int),
					new SqlParameter("@ShowDiscount",SqlDbType.Int),
					new SqlParameter("@ShowSalesQty",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@HomeId",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.ModelName;
            parameters[1].Value = pEntity.ModelDesc;
            parameters[2].Value = pEntity.CreateTime;
            parameters[3].Value = pEntity.CreateBy;
            parameters[4].Value = pEntity.LastUpdateBy;
            parameters[5].Value = pEntity.LastUpdateTime;
            parameters[6].Value = pEntity.IsDelete;
            parameters[7].Value = pEntity.ModelTypeId;
            parameters[8].Value = pEntity.CustomerID;
            parameters[9].Value = pEntity.GroupValue;
            parameters[10].Value = pEntity.StyleType;
            parameters[11].Value = pEntity.TitleName;
            parameters[12].Value = pEntity.TitleStyle;
            parameters[13].Value = pEntity.ShowCount;
            parameters[14].Value = pEntity.ShowName;
            parameters[15].Value = pEntity.ShowPrice;
            parameters[16].Value = pEntity.ShowSalesPrice;
            parameters[17].Value = pEntity.ShowDiscount;
            parameters[18].Value = pEntity.ShowSalesQty;
            parameters[19].Value = pEntity.DisplayIndex;
            parameters[20].Value = pEntity.HomeId;

            //执行并将结果回写
            object result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.GroupId = Convert.ToInt32(result);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public MHCategoryAreaGroupEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MHCategoryAreaGroup] where GroupId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            MHCategoryAreaGroupEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public MHCategoryAreaGroupEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MHCategoryAreaGroup] where 1=1  and isdelete=0");
            //读取数据
            List<MHCategoryAreaGroupEntity> list = new List<MHCategoryAreaGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MHCategoryAreaGroupEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(MHCategoryAreaGroupEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, false);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(MHCategoryAreaGroupEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.GroupId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MHCategoryAreaGroup] set ");
            if (pIsUpdateNullField || pEntity.ModelName != null)
                strSql.Append("[ModelName]=@ModelName,");
            if (pIsUpdateNullField || pEntity.ModelDesc != null)
                strSql.Append("[ModelDesc]=@ModelDesc,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.ModelTypeId != null)
                strSql.Append("[ModelTypeId]=@ModelTypeId,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.GroupValue != null)
                strSql.Append("[GroupValue]=@GroupValue,");
            if (pIsUpdateNullField || pEntity.StyleType != null)
                strSql.Append("[StyleType]=@StyleType,");
            if (pIsUpdateNullField || pEntity.TitleName != null)
                strSql.Append("[TitleName]=@TitleName,");
            if (pIsUpdateNullField || pEntity.TitleStyle != null)
                strSql.Append("[TitleStyle]=@TitleStyle,");
            if (pIsUpdateNullField || pEntity.ShowCount != null)
                strSql.Append("[ShowCount]=@ShowCount,");
            if (pIsUpdateNullField || pEntity.ShowName != null)
                strSql.Append("[ShowName]=@ShowName,");
            if (pIsUpdateNullField || pEntity.ShowPrice != null)
                strSql.Append("[ShowPrice]=@ShowPrice,");
            if (pIsUpdateNullField || pEntity.ShowSalesPrice != null)
                strSql.Append("[ShowSalesPrice]=@ShowSalesPrice,");
            if (pIsUpdateNullField || pEntity.ShowDiscount != null)
                strSql.Append("[ShowDiscount]=@ShowDiscount,");
            if (pIsUpdateNullField || pEntity.ShowSalesQty != null)
                strSql.Append("[ShowSalesQty]=@ShowSalesQty,");
            if (pIsUpdateNullField || pEntity.DisplayIndex != null)
                strSql.Append("[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.HomeId != null)
                strSql.Append("[HomeId]=@HomeId");
            strSql.Append(" where GroupId=@GroupId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ModelName",SqlDbType.NVarChar),
					new SqlParameter("@ModelDesc",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ModelTypeId",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@GroupValue",SqlDbType.Int),
					new SqlParameter("@StyleType",SqlDbType.VarChar),
					new SqlParameter("@TitleName",SqlDbType.VarChar),
					new SqlParameter("@TitleStyle",SqlDbType.VarChar),
					new SqlParameter("@ShowCount",SqlDbType.Int),
					new SqlParameter("@ShowName",SqlDbType.Int),
					new SqlParameter("@ShowPrice",SqlDbType.Int),
					new SqlParameter("@ShowSalesPrice",SqlDbType.Int),
					new SqlParameter("@ShowDiscount",SqlDbType.Int),
					new SqlParameter("@ShowSalesQty",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@HomeId",SqlDbType.NVarChar),
					new SqlParameter("@GroupId",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.ModelName;
            parameters[1].Value = pEntity.ModelDesc;
            parameters[2].Value = pEntity.LastUpdateBy;
            parameters[3].Value = pEntity.LastUpdateTime;
            parameters[4].Value = pEntity.ModelTypeId;
            parameters[5].Value = pEntity.CustomerID;
            parameters[6].Value = pEntity.GroupValue;
            parameters[7].Value = pEntity.StyleType;
            parameters[8].Value = pEntity.TitleName;
            parameters[9].Value = pEntity.TitleStyle;
            parameters[10].Value = pEntity.ShowCount;
            parameters[11].Value = pEntity.ShowName;
            parameters[12].Value = pEntity.ShowPrice;
            parameters[13].Value = pEntity.ShowSalesPrice;
            parameters[14].Value = pEntity.ShowDiscount;
            parameters[15].Value = pEntity.ShowSalesQty;
            parameters[16].Value = pEntity.DisplayIndex;
            parameters[17].Value = pEntity.HomeId;
            parameters[18].Value = pEntity.GroupId;

            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(MHCategoryAreaGroupEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MHCategoryAreaGroupEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(MHCategoryAreaGroupEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.GroupId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.GroupId.Value, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [MHCategoryAreaGroup] set  isdelete=1 where GroupId=@GroupId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@GroupId",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(MHCategoryAreaGroupEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.GroupId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.GroupId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(MHCategoryAreaGroupEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("{0},", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [MHCategoryAreaGroup] set  isdelete=1 where GroupId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public MHCategoryAreaGroupEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MHCategoryAreaGroup] where 1=1  and isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //执行SQL
            List<MHCategoryAreaGroupEntity> list = new List<MHCategoryAreaGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MHCategoryAreaGroupEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<MHCategoryAreaGroupEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [GroupId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [MHCategoryAreaGroup] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [MHCategoryAreaGroup] where 1=1  and isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<MHCategoryAreaGroupEntity> result = new PagedQueryResult<MHCategoryAreaGroupEntity>();
            List<MHCategoryAreaGroupEntity> list = new List<MHCategoryAreaGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MHCategoryAreaGroupEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public MHCategoryAreaGroupEntity[] QueryByEntity(MHCategoryAreaGroupEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<MHCategoryAreaGroupEntity> PagedQueryByEntity(MHCategoryAreaGroupEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(MHCategoryAreaGroupEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.GroupId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupId", Value = pQueryEntity.GroupId });
            if (pQueryEntity.ModelName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModelName", Value = pQueryEntity.ModelName });
            if (pQueryEntity.ModelDesc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModelDesc", Value = pQueryEntity.ModelDesc });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.ModelTypeId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModelTypeId", Value = pQueryEntity.ModelTypeId });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.GroupValue != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupValue", Value = pQueryEntity.GroupValue });
            if (pQueryEntity.StyleType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StyleType", Value = pQueryEntity.StyleType });
            if (pQueryEntity.TitleName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TitleName", Value = pQueryEntity.TitleName });
            if (pQueryEntity.TitleStyle != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TitleStyle", Value = pQueryEntity.TitleStyle });
            if (pQueryEntity.ShowCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShowCount", Value = pQueryEntity.ShowCount });
            if (pQueryEntity.ShowName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShowName", Value = pQueryEntity.ShowName });
            if (pQueryEntity.ShowPrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShowPrice", Value = pQueryEntity.ShowPrice });
            if (pQueryEntity.ShowSalesPrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShowSalesPrice", Value = pQueryEntity.ShowSalesPrice });
            if (pQueryEntity.ShowDiscount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShowDiscount", Value = pQueryEntity.ShowDiscount });
            if (pQueryEntity.ShowSalesQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShowSalesQty", Value = pQueryEntity.ShowSalesQty });
            if (pQueryEntity.DisplayIndex != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.HomeId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HomeId", Value = pQueryEntity.HomeId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out MHCategoryAreaGroupEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new MHCategoryAreaGroupEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["GroupId"] != DBNull.Value)
            {
                pInstance.GroupId = Convert.ToInt32(pReader["GroupId"]);
            }
            if (pReader["ModelName"] != DBNull.Value)
            {
                pInstance.ModelName = Convert.ToString(pReader["ModelName"]);
            }
            if (pReader["ModelDesc"] != DBNull.Value)
            {
                pInstance.ModelDesc = Convert.ToString(pReader["ModelDesc"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["ModelTypeId"] != DBNull.Value)
            {
                pInstance.ModelTypeId = Convert.ToInt32(pReader["ModelTypeId"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }
            if (pReader["GroupValue"] != DBNull.Value)
            {
                pInstance.GroupValue = Convert.ToInt32(pReader["GroupValue"]);
            }
            if (pReader["StyleType"] != DBNull.Value)
            {
                pInstance.StyleType = Convert.ToString(pReader["StyleType"]);
            }
            if (pReader["TitleName"] != DBNull.Value)
            {
                pInstance.TitleName = Convert.ToString(pReader["TitleName"]);
            }
            if (pReader["TitleStyle"] != DBNull.Value)
            {
                pInstance.TitleStyle = Convert.ToString(pReader["TitleStyle"]);
            }
            if (pReader["ShowCount"] != DBNull.Value)
            {
                pInstance.ShowCount = Convert.ToInt32(pReader["ShowCount"]);
            }
            if (pReader["ShowName"] != DBNull.Value)
            {
                pInstance.ShowName = Convert.ToInt32(pReader["ShowName"]);
            }
            if (pReader["ShowPrice"] != DBNull.Value)
            {
                pInstance.ShowPrice = Convert.ToInt32(pReader["ShowPrice"]);
            }
            if (pReader["ShowSalesPrice"] != DBNull.Value)
            {
                pInstance.ShowSalesPrice = Convert.ToInt32(pReader["ShowSalesPrice"]);
            }
            if (pReader["ShowDiscount"] != DBNull.Value)
            {
                pInstance.ShowDiscount = Convert.ToInt32(pReader["ShowDiscount"]);
            }
            if (pReader["ShowSalesQty"] != DBNull.Value)
            {
                pInstance.ShowSalesQty = Convert.ToInt32(pReader["ShowSalesQty"]);
            }
            if (pReader["DisplayIndex"] != DBNull.Value)
            {
                pInstance.DisplayIndex = Convert.ToInt32(pReader["DisplayIndex"]);
            }
            if (pReader["HomeId"] != DBNull.Value)
            {
                pInstance.HomeId = Convert.ToString(pReader["HomeId"]);
            }

        }
        #endregion
    }
}
