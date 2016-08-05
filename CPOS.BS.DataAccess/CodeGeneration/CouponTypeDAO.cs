/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/22 11:41:32
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
    /// 表CouponType的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CouponTypeDAO : Base.BaseCPOSDAO, ICRUDable<CouponTypeEntity>, IQueryable<CouponTypeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CouponTypeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(CouponTypeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(CouponTypeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CouponType](");
            strSql.Append("[CouponTypeName],[CouponTypeCode],[CouponCategory],[ParValue],[Discount],[ConditionValue],[IsRepeatable],[IsMixable],[CouponSourceID],[ValidPeriod],[LastUpdateTime],[LastUpdateBy],[CreateTime],[CreateBy],[IsDelete],[CustomerId],[IssuedQty],[IsVoucher],[UsableRange],[ServiceLife],[SuitableForStore],[BeginTime],[EndTime],[CouponTypeDesc],[IsNotLimitQty],[CouponTypeID])");
            strSql.Append(" values (");
            strSql.Append("@CouponTypeName,@CouponTypeCode,@CouponCategory,@ParValue,@Discount,@ConditionValue,@IsRepeatable,@IsMixable,@CouponSourceID,@ValidPeriod,@LastUpdateTime,@LastUpdateBy,@CreateTime,@CreateBy,@IsDelete,@CustomerId,@IssuedQty,@IsVoucher,@UsableRange,@ServiceLife,@SuitableForStore,@BeginTime,@EndTime,@CouponTypeDesc,@IsNotLimitQty,@CouponTypeID)");

            Guid? pkGuid;
            if (pEntity.CouponTypeID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.CouponTypeID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouponTypeName",SqlDbType.NVarChar),
					new SqlParameter("@CouponTypeCode",SqlDbType.VarChar),
					new SqlParameter("@CouponCategory",SqlDbType.VarChar),
					new SqlParameter("@ParValue",SqlDbType.Decimal),
					new SqlParameter("@Discount",SqlDbType.Decimal),
					new SqlParameter("@ConditionValue",SqlDbType.Decimal),
					new SqlParameter("@IsRepeatable",SqlDbType.Int),
					new SqlParameter("@IsMixable",SqlDbType.Int),
					new SqlParameter("@CouponSourceID",SqlDbType.NVarChar),
					new SqlParameter("@ValidPeriod",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@IssuedQty",SqlDbType.Int),
					new SqlParameter("@IsVoucher",SqlDbType.Int),
					new SqlParameter("@UsableRange",SqlDbType.Int),
					new SqlParameter("@ServiceLife",SqlDbType.Int),
					new SqlParameter("@SuitableForStore",SqlDbType.Int),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@CouponTypeDesc",SqlDbType.NVarChar),
					new SqlParameter("@IsNotLimitQty",SqlDbType.Int),
					new SqlParameter("@CouponTypeID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CouponTypeName;
            parameters[1].Value = pEntity.CouponTypeCode;
            parameters[2].Value = pEntity.CouponCategory;
            parameters[3].Value = pEntity.ParValue;
            parameters[4].Value = pEntity.Discount;
            parameters[5].Value = pEntity.ConditionValue;
            parameters[6].Value = pEntity.IsRepeatable;
            parameters[7].Value = pEntity.IsMixable;
            parameters[8].Value = pEntity.CouponSourceID;
            parameters[9].Value = pEntity.ValidPeriod;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.LastUpdateBy;
            parameters[12].Value = pEntity.CreateTime;
            parameters[13].Value = pEntity.CreateBy;
            parameters[14].Value = pEntity.IsDelete;
            parameters[15].Value = pEntity.CustomerId;
            parameters[16].Value = pEntity.IssuedQty;
            parameters[17].Value = pEntity.IsVoucher;
            parameters[18].Value = pEntity.UsableRange;
            parameters[19].Value = pEntity.ServiceLife;
            parameters[20].Value = pEntity.SuitableForStore;
            parameters[21].Value = pEntity.BeginTime;
            parameters[22].Value = pEntity.EndTime;
            parameters[23].Value = pEntity.CouponTypeDesc;
            parameters[24].Value = pEntity.IsNotLimitQty;
            parameters[25].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.CouponTypeID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public CouponTypeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CouponType] where CouponTypeID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            CouponTypeEntity m = null;
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
        public CouponTypeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CouponType] where 1=1  and isdelete=0");
            //读取数据
            List<CouponTypeEntity> list = new List<CouponTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CouponTypeEntity m;
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
        public void Update(CouponTypeEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(CouponTypeEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CouponTypeID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [CouponType] set ");
            if (pIsUpdateNullField || pEntity.CouponTypeName != null)
                strSql.Append("[CouponTypeName]=@CouponTypeName,");
            if (pIsUpdateNullField || pEntity.CouponTypeCode != null)
                strSql.Append("[CouponTypeCode]=@CouponTypeCode,");
            if (pIsUpdateNullField || pEntity.CouponCategory != null)
                strSql.Append("[CouponCategory]=@CouponCategory,");
            if (pIsUpdateNullField || pEntity.ParValue != null)
                strSql.Append("[ParValue]=@ParValue,");
            if (pIsUpdateNullField || pEntity.Discount != null)
                strSql.Append("[Discount]=@Discount,");
            if (pIsUpdateNullField || pEntity.ConditionValue != null)
                strSql.Append("[ConditionValue]=@ConditionValue,");
            if (pIsUpdateNullField || pEntity.IsRepeatable != null)
                strSql.Append("[IsRepeatable]=@IsRepeatable,");
            if (pIsUpdateNullField || pEntity.IsMixable != null)
                strSql.Append("[IsMixable]=@IsMixable,");
            if (pIsUpdateNullField || pEntity.CouponSourceID != null)
                strSql.Append("[CouponSourceID]=@CouponSourceID,");
            if (pIsUpdateNullField || pEntity.ValidPeriod != null)
                strSql.Append("[ValidPeriod]=@ValidPeriod,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.IssuedQty != null)
                strSql.Append("[IssuedQty]=@IssuedQty,");
            if (pIsUpdateNullField || pEntity.IsVoucher != null)
                strSql.Append("[IsVoucher]=@IsVoucher,");
            if (pIsUpdateNullField || pEntity.UsableRange != null)
                strSql.Append("[UsableRange]=@UsableRange,");
            if (pIsUpdateNullField || pEntity.ServiceLife != null)
                strSql.Append("[ServiceLife]=@ServiceLife,");
            if (pIsUpdateNullField || pEntity.SuitableForStore != null)
                strSql.Append("[SuitableForStore]=@SuitableForStore,");
            if (pIsUpdateNullField || pEntity.BeginTime != null)
                strSql.Append("[BeginTime]=@BeginTime,");
            if (pIsUpdateNullField || pEntity.EndTime != null)
                strSql.Append("[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.CouponTypeDesc != null)
                strSql.Append("[CouponTypeDesc]=@CouponTypeDesc,");
            if (pIsUpdateNullField || pEntity.IsNotLimitQty != null)
                strSql.Append("[IsNotLimitQty]=@IsNotLimitQty");
            strSql.Append(" where CouponTypeID=@CouponTypeID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouponTypeName",SqlDbType.NVarChar),
					new SqlParameter("@CouponTypeCode",SqlDbType.VarChar),
					new SqlParameter("@CouponCategory",SqlDbType.VarChar),
					new SqlParameter("@ParValue",SqlDbType.Decimal),
					new SqlParameter("@Discount",SqlDbType.Decimal),
					new SqlParameter("@ConditionValue",SqlDbType.Decimal),
					new SqlParameter("@IsRepeatable",SqlDbType.Int),
					new SqlParameter("@IsMixable",SqlDbType.Int),
					new SqlParameter("@CouponSourceID",SqlDbType.NVarChar),
					new SqlParameter("@ValidPeriod",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@IssuedQty",SqlDbType.Int),
					new SqlParameter("@IsVoucher",SqlDbType.Int),
					new SqlParameter("@UsableRange",SqlDbType.Int),
					new SqlParameter("@ServiceLife",SqlDbType.Int),
					new SqlParameter("@SuitableForStore",SqlDbType.Int),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@CouponTypeDesc",SqlDbType.NVarChar),
					new SqlParameter("@IsNotLimitQty",SqlDbType.Int),
					new SqlParameter("@CouponTypeID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CouponTypeName;
            parameters[1].Value = pEntity.CouponTypeCode;
            parameters[2].Value = pEntity.CouponCategory;
            parameters[3].Value = pEntity.ParValue;
            parameters[4].Value = pEntity.Discount;
            parameters[5].Value = pEntity.ConditionValue;
            parameters[6].Value = pEntity.IsRepeatable;
            parameters[7].Value = pEntity.IsMixable;
            parameters[8].Value = pEntity.CouponSourceID;
            parameters[9].Value = pEntity.ValidPeriod;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.LastUpdateBy;
            parameters[12].Value = pEntity.CustomerId;
            parameters[13].Value = pEntity.IssuedQty;
            parameters[14].Value = pEntity.IsVoucher;
            parameters[15].Value = pEntity.UsableRange;
            parameters[16].Value = pEntity.ServiceLife;
            parameters[17].Value = pEntity.SuitableForStore;
            parameters[18].Value = pEntity.BeginTime;
            parameters[19].Value = pEntity.EndTime;
            parameters[20].Value = pEntity.CouponTypeDesc;
            parameters[21].Value = pEntity.IsNotLimitQty;
            parameters[22].Value = pEntity.CouponTypeID;

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
        public void Update(CouponTypeEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CouponTypeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(CouponTypeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CouponTypeID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.CouponTypeID.Value, pTran);
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
            sql.AppendLine("update [CouponType] set  isdelete=1 where CouponTypeID=@CouponTypeID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@CouponTypeID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(CouponTypeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.CouponTypeID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.CouponTypeID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(CouponTypeEntity[] pEntities)
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
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [CouponType] set  isdelete=1 where CouponTypeID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CouponTypeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CouponType] where 1=1  and isdelete=0 ");
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
            List<CouponTypeEntity> list = new List<CouponTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CouponTypeEntity m;
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
        public PagedQueryResult<CouponTypeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CouponTypeID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [CouponType] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [CouponType] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<CouponTypeEntity> result = new PagedQueryResult<CouponTypeEntity>();
            List<CouponTypeEntity> list = new List<CouponTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CouponTypeEntity m;
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
        public CouponTypeEntity[] QueryByEntity(CouponTypeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CouponTypeEntity> PagedQueryByEntity(CouponTypeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CouponTypeEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CouponTypeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponTypeID", Value = pQueryEntity.CouponTypeID });
            if (pQueryEntity.CouponTypeName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponTypeName", Value = pQueryEntity.CouponTypeName });
            if (pQueryEntity.CouponTypeCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponTypeCode", Value = pQueryEntity.CouponTypeCode });
            if (pQueryEntity.CouponCategory != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponCategory", Value = pQueryEntity.CouponCategory });
            if (pQueryEntity.ParValue != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParValue", Value = pQueryEntity.ParValue });
            if (pQueryEntity.Discount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Discount", Value = pQueryEntity.Discount });
            if (pQueryEntity.ConditionValue != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConditionValue", Value = pQueryEntity.ConditionValue });
            if (pQueryEntity.IsRepeatable != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRepeatable", Value = pQueryEntity.IsRepeatable });
            if (pQueryEntity.IsMixable != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMixable", Value = pQueryEntity.IsMixable });
            if (pQueryEntity.CouponSourceID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponSourceID", Value = pQueryEntity.CouponSourceID });
            if (pQueryEntity.ValidPeriod != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ValidPeriod", Value = pQueryEntity.ValidPeriod });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IssuedQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IssuedQty", Value = pQueryEntity.IssuedQty });
            if (pQueryEntity.IsVoucher != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsVoucher", Value = pQueryEntity.IsVoucher });
            if (pQueryEntity.UsableRange != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UsableRange", Value = pQueryEntity.UsableRange });
            if (pQueryEntity.ServiceLife != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceLife", Value = pQueryEntity.ServiceLife });
            if (pQueryEntity.SuitableForStore != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuitableForStore", Value = pQueryEntity.SuitableForStore });
            if (pQueryEntity.BeginTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.CouponTypeDesc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponTypeDesc", Value = pQueryEntity.CouponTypeDesc });
            if (pQueryEntity.IsNotLimitQty != 0)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsNotLimitQty", Value = pQueryEntity.IsNotLimitQty });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out CouponTypeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new CouponTypeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["CouponTypeID"] != DBNull.Value)
            {
                pInstance.CouponTypeID = (Guid)pReader["CouponTypeID"];
            }
            if (pReader["CouponTypeName"] != DBNull.Value)
            {
                pInstance.CouponTypeName = Convert.ToString(pReader["CouponTypeName"]);
            }
            if (pReader["CouponTypeCode"] != DBNull.Value)
            {
                pInstance.CouponTypeCode = Convert.ToString(pReader["CouponTypeCode"]);
            }
            if (pReader["CouponCategory"] != DBNull.Value)
            {
                pInstance.CouponCategory = Convert.ToString(pReader["CouponCategory"]);
            }
            if (pReader["ParValue"] != DBNull.Value)
            {
                pInstance.ParValue = Convert.ToDecimal(pReader["ParValue"]);
            }
            if (pReader["Discount"] != DBNull.Value)
            {
                pInstance.Discount = Convert.ToDecimal(pReader["Discount"]);
            }
            if (pReader["ConditionValue"] != DBNull.Value)
            {
                pInstance.ConditionValue = Convert.ToDecimal(pReader["ConditionValue"]);
            }
            if (pReader["IsRepeatable"] != DBNull.Value)
            {
                pInstance.IsRepeatable = Convert.ToInt32(pReader["IsRepeatable"]);
            }
            if (pReader["IsMixable"] != DBNull.Value)
            {
                pInstance.IsMixable = Convert.ToInt32(pReader["IsMixable"]);
            }
            if (pReader["CouponSourceID"] != DBNull.Value)
            {
                pInstance.CouponSourceID = Convert.ToString(pReader["CouponSourceID"]);
            }
            if (pReader["ValidPeriod"] != DBNull.Value)
            {
                pInstance.ValidPeriod = Convert.ToInt32(pReader["ValidPeriod"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["CustomerId"] != DBNull.Value)
            {
                pInstance.CustomerId = Convert.ToString(pReader["CustomerId"]);
            }
            if (pReader["IssuedQty"] != DBNull.Value)
            {
                pInstance.IssuedQty = Convert.ToInt32(pReader["IssuedQty"]);
            }
            if (pReader["IsVoucher"] != DBNull.Value)
            {
                pInstance.IsVoucher = Convert.ToInt32(pReader["IsVoucher"]);
            }
            if (pReader["UsableRange"] != DBNull.Value)
            {
                pInstance.UsableRange = Convert.ToInt32(pReader["UsableRange"]);
            }
            if (pReader["ServiceLife"] != DBNull.Value)
            {
                pInstance.ServiceLife = Convert.ToInt32(pReader["ServiceLife"]);
            }
            if (pReader["SuitableForStore"] != DBNull.Value)
            {
                pInstance.SuitableForStore = Convert.ToInt32(pReader["SuitableForStore"]);
            }
            if (pReader["BeginTime"] != DBNull.Value)
            {
                pInstance.BeginTime = Convert.ToDateTime(pReader["BeginTime"]);
            }
            if (pReader["EndTime"] != DBNull.Value)
            {
                pInstance.EndTime = Convert.ToDateTime(pReader["EndTime"]);
            }
            if (pReader["CouponTypeDesc"] != DBNull.Value)
            {
                pInstance.CouponTypeDesc = Convert.ToString(pReader["CouponTypeDesc"]);
            }
            if (pReader["IsNotLimitQty"] != DBNull.Value)
            {
                pInstance.IsNotLimitQty = Convert.ToInt32(pReader["IsNotLimitQty"]);
            }

        }
        #endregion
    }
}
