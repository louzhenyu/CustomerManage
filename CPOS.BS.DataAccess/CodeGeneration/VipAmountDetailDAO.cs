/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/30 15:25:27
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
    /// 表VipAmountDetail的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipAmountDetailDAO : Base.BaseCPOSDAO, ICRUDable<VipAmountDetailEntity>, IQueryable<VipAmountDetailEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipAmountDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipAmountDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipAmountDetailEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipAmountDetail](");
            strSql.Append("[VipId],[VipCardCode],[UnitID],[UnitName],[SalesAmount],[Amount],[UsedReturnAmount],[Reason],[EffectiveDate],[DeadlineDate],[AmountSourceId],[ObjectId],[Remark],[IsValid],[IsWithdrawCash],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[VipAmountDetailId])");
            strSql.Append(" values (");
            strSql.Append("@VipId,@VipCardCode,@UnitID,@UnitName,@SalesAmount,@Amount,@UsedReturnAmount,@Reason,@EffectiveDate,@DeadlineDate,@AmountSourceId,@ObjectId,@Remark,@IsValid,@IsWithdrawCash,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@VipAmountDetailId)");

            Guid? pkGuid;
            if (pEntity.VipAmountDetailId == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.VipAmountDetailId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.VarChar),
					new SqlParameter("@VipCardCode",SqlDbType.VarChar),
					new SqlParameter("@UnitID",SqlDbType.VarChar),
					new SqlParameter("@UnitName",SqlDbType.VarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@UsedReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@EffectiveDate",SqlDbType.DateTime),
					new SqlParameter("@DeadlineDate",SqlDbType.DateTime),
					new SqlParameter("@AmountSourceId",SqlDbType.NVarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@IsValid",SqlDbType.Int),
					new SqlParameter("@IsWithdrawCash",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VipAmountDetailId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.VipId;
            parameters[1].Value = pEntity.VipCardCode;
            parameters[2].Value = pEntity.UnitID;
            parameters[3].Value = pEntity.UnitName;
            parameters[4].Value = pEntity.SalesAmount;
            parameters[5].Value = pEntity.Amount;
            parameters[6].Value = pEntity.UsedReturnAmount;
            parameters[7].Value = pEntity.Reason;
            parameters[8].Value = pEntity.EffectiveDate;
            parameters[9].Value = pEntity.DeadlineDate;
            parameters[10].Value = pEntity.AmountSourceId;
            parameters[11].Value = pEntity.ObjectId;
            parameters[12].Value = pEntity.Remark;
            parameters[13].Value = pEntity.IsValid;
            parameters[14].Value = pEntity.IsWithdrawCash;
            parameters[15].Value = pEntity.CustomerID;
            parameters[16].Value = pEntity.CreateTime;
            parameters[17].Value = pEntity.CreateBy;
            parameters[18].Value = pEntity.LastUpdateTime;
            parameters[19].Value = pEntity.LastUpdateBy;
            parameters[20].Value = pEntity.IsDelete;
            parameters[21].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.VipAmountDetailId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipAmountDetailEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipAmountDetail] where VipAmountDetailId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipAmountDetailEntity m = null;
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
        public VipAmountDetailEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipAmountDetail] where 1=1  and isdelete=0");
            //读取数据
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipAmountDetailEntity m;
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
        public void Update(VipAmountDetailEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(VipAmountDetailEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipAmountDetailId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipAmountDetail] set ");
            if (pIsUpdateNullField || pEntity.VipId != null)
                strSql.Append("[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.VipCardCode != null)
                strSql.Append("[VipCardCode]=@VipCardCode,");
            if (pIsUpdateNullField || pEntity.UnitID != null)
                strSql.Append("[UnitID]=@UnitID,");
            if (pIsUpdateNullField || pEntity.UnitName != null)
                strSql.Append("[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.SalesAmount != null)
                strSql.Append("[SalesAmount]=@SalesAmount,");
            if (pIsUpdateNullField || pEntity.Amount != null)
                strSql.Append("[Amount]=@Amount,");
            if (pIsUpdateNullField || pEntity.UsedReturnAmount != null)
                strSql.Append("[UsedReturnAmount]=@UsedReturnAmount,");
            if (pIsUpdateNullField || pEntity.Reason != null)
                strSql.Append("[Reason]=@Reason,");
            if (pIsUpdateNullField || pEntity.EffectiveDate != null)
                strSql.Append("[EffectiveDate]=@EffectiveDate,");
            if (pIsUpdateNullField || pEntity.DeadlineDate != null)
                strSql.Append("[DeadlineDate]=@DeadlineDate,");
            if (pIsUpdateNullField || pEntity.AmountSourceId != null)
                strSql.Append("[AmountSourceId]=@AmountSourceId,");
            if (pIsUpdateNullField || pEntity.ObjectId != null)
                strSql.Append("[ObjectId]=@ObjectId,");
            if (pIsUpdateNullField || pEntity.Remark != null)
                strSql.Append("[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.IsValid != null)
                strSql.Append("[IsValid]=@IsValid,");
            if (pIsUpdateNullField || pEntity.IsWithdrawCash != null)
                strSql.Append("[IsWithdrawCash]=@IsWithdrawCash,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where VipAmountDetailId=@VipAmountDetailId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.VarChar),
					new SqlParameter("@VipCardCode",SqlDbType.VarChar),
					new SqlParameter("@UnitID",SqlDbType.VarChar),
					new SqlParameter("@UnitName",SqlDbType.VarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@UsedReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@EffectiveDate",SqlDbType.DateTime),
					new SqlParameter("@DeadlineDate",SqlDbType.DateTime),
					new SqlParameter("@AmountSourceId",SqlDbType.NVarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@IsValid",SqlDbType.Int),
					new SqlParameter("@IsWithdrawCash",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@VipAmountDetailId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.VipId;
            parameters[1].Value = pEntity.VipCardCode;
            parameters[2].Value = pEntity.UnitID;
            parameters[3].Value = pEntity.UnitName;
            parameters[4].Value = pEntity.SalesAmount;
            parameters[5].Value = pEntity.Amount;
            parameters[6].Value = pEntity.UsedReturnAmount;
            parameters[7].Value = pEntity.Reason;
            parameters[8].Value = pEntity.EffectiveDate;
            parameters[9].Value = pEntity.DeadlineDate;
            parameters[10].Value = pEntity.AmountSourceId;
            parameters[11].Value = pEntity.ObjectId;
            parameters[12].Value = pEntity.Remark;
            parameters[13].Value = pEntity.IsValid;
            parameters[14].Value = pEntity.IsWithdrawCash;
            parameters[15].Value = pEntity.CustomerID;
            parameters[16].Value = pEntity.LastUpdateTime;
            parameters[17].Value = pEntity.LastUpdateBy;
            parameters[18].Value = pEntity.VipAmountDetailId;

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
        public void Update(VipAmountDetailEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipAmountDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipAmountDetailEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipAmountDetailId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VipAmountDetailId.Value, pTran);
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
            sql.AppendLine("update [VipAmountDetail] set  isdelete=1 where VipAmountDetailId=@VipAmountDetailId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@VipAmountDetailId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VipAmountDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.VipAmountDetailId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.VipAmountDetailId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipAmountDetailEntity[] pEntities)
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
            sql.AppendLine("update [VipAmountDetail] set  isdelete=1 where VipAmountDetailId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipAmountDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipAmountDetail] where 1=1  and isdelete=0 ");
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
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipAmountDetailEntity m;
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
        public PagedQueryResult<VipAmountDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipAmountDetailId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipAmountDetail] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VipAmountDetail] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipAmountDetailEntity> result = new PagedQueryResult<VipAmountDetailEntity>();
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipAmountDetailEntity m;
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
        public VipAmountDetailEntity[] QueryByEntity(VipAmountDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipAmountDetailEntity> PagedQueryByEntity(VipAmountDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipAmountDetailEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipAmountDetailId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipAmountDetailId", Value = pQueryEntity.VipAmountDetailId });
            if (pQueryEntity.VipId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.VipCardCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardCode", Value = pQueryEntity.VipCardCode });
            if (pQueryEntity.UnitID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.UnitName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.SalesAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.Amount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Amount", Value = pQueryEntity.Amount });
            if (pQueryEntity.UsedReturnAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UsedReturnAmount", Value = pQueryEntity.UsedReturnAmount });
            if (pQueryEntity.Reason != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Reason", Value = pQueryEntity.Reason });
            if (pQueryEntity.EffectiveDate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveDate", Value = pQueryEntity.EffectiveDate });
            if (pQueryEntity.DeadlineDate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeadlineDate", Value = pQueryEntity.DeadlineDate });
            if (pQueryEntity.AmountSourceId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AmountSourceId", Value = pQueryEntity.AmountSourceId });
            if (pQueryEntity.ObjectId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = pQueryEntity.ObjectId });
            if (pQueryEntity.Remark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.IsValid != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsValid", Value = pQueryEntity.IsValid });
            if (pQueryEntity.IsWithdrawCash != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsWithdrawCash", Value = pQueryEntity.IsWithdrawCash });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out VipAmountDetailEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipAmountDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["VipAmountDetailId"] != DBNull.Value)
            {
                pInstance.VipAmountDetailId = (Guid)pReader["VipAmountDetailId"];
            }
            if (pReader["VipId"] != DBNull.Value)
            {
                pInstance.VipId = Convert.ToString(pReader["VipId"]);
            }
            if (pReader["VipCardCode"] != DBNull.Value)
            {
                pInstance.VipCardCode = Convert.ToString(pReader["VipCardCode"]);
            }
            if (pReader["UnitID"] != DBNull.Value)
            {
                pInstance.UnitID = Convert.ToString(pReader["UnitID"]);
            }
            if (pReader["UnitName"] != DBNull.Value)
            {
                pInstance.UnitName = Convert.ToString(pReader["UnitName"]);
            }
            if (pReader["SalesAmount"] != DBNull.Value)
            {
                pInstance.SalesAmount = Convert.ToDecimal(pReader["SalesAmount"]);
            }
            if (pReader["Amount"] != DBNull.Value)
            {
                pInstance.Amount = Convert.ToDecimal(pReader["Amount"]);
            }
            if (pReader["UsedReturnAmount"] != DBNull.Value)
            {
                pInstance.UsedReturnAmount = Convert.ToDecimal(pReader["UsedReturnAmount"]);
            }
            if (pReader["Reason"] != DBNull.Value)
            {
                pInstance.Reason = Convert.ToString(pReader["Reason"]);
            }
            if (pReader["EffectiveDate"] != DBNull.Value)
            {
                pInstance.EffectiveDate = Convert.ToDateTime(pReader["EffectiveDate"]);
            }
            if (pReader["DeadlineDate"] != DBNull.Value)
            {
                pInstance.DeadlineDate = Convert.ToDateTime(pReader["DeadlineDate"]);
            }
            if (pReader["AmountSourceId"] != DBNull.Value)
            {
                pInstance.AmountSourceId = Convert.ToString(pReader["AmountSourceId"]);
            }
            if (pReader["ObjectId"] != DBNull.Value)
            {
                pInstance.ObjectId = Convert.ToString(pReader["ObjectId"]);
            }
            if (pReader["Remark"] != DBNull.Value)
            {
                pInstance.Remark = Convert.ToString(pReader["Remark"]);
            }
            if (pReader["IsValid"] != DBNull.Value)
            {
                pInstance.IsValid = Convert.ToInt32(pReader["IsValid"]);
            }
            if (pReader["IsWithdrawCash"] != DBNull.Value)
            {
                pInstance.IsWithdrawCash = Convert.ToInt32(pReader["IsWithdrawCash"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
                if (pInstance.AmountSourceId == "23" || pInstance.AmountSourceId == "24")
                {
                    var userDao = new T_UserDAO(CurrentUserInfo);
                    var userInfo = userDao.GetByID(pInstance.CreateBy);
                    pInstance.CreateByName = userInfo != null ? userInfo.user_name : "";
                }
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }

        }
        #endregion
    }
}
