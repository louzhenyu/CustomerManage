/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 20:05:31
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
    /// 数据访问： 批量制卡 
    /// 表VipCardBatch的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardBatchDAO : Base.BaseCPOSDAO, ICRUDable<VipCardBatchEntity>, IQueryable<VipCardBatchEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardBatchDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipCardBatchEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipCardBatchEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.IsDelete=0;
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;
			pEntity.CreateBy=CurrentUserInfo.UserID;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [VipCardBatch](");
            strSql.Append("[BatchNo],[CardMedium],[RegionNumber],[CardPrefix],[VipCardTypeCode],[StartCardNo],[EndCardNo],[Qty],[OutliersQty],[CostPrice],[ExportCount],[Remark],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[ImageUrl],[ImportQty],[BatchID])");
            strSql.Append(" values (");
            strSql.Append("@BatchNo,@CardMedium,@RegionNumber,@CardPrefix,@VipCardTypeCode,@StartCardNo,@EndCardNo,@Qty,@OutliersQty,@CostPrice,@ExportCount,@Remark,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@ImageUrl,@ImportQty,@BatchID)");            

			Guid? pkGuid;
			if (pEntity.BatchID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.BatchID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@BatchNo",SqlDbType.Int),
					new SqlParameter("@CardMedium",SqlDbType.VarChar),
					new SqlParameter("@RegionNumber",SqlDbType.VarChar),
					new SqlParameter("@CardPrefix",SqlDbType.NVarChar),
					new SqlParameter("@VipCardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@StartCardNo",SqlDbType.VarChar),
					new SqlParameter("@EndCardNo",SqlDbType.VarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@OutliersQty",SqlDbType.Int),
					new SqlParameter("@CostPrice",SqlDbType.Decimal),
					new SqlParameter("@ExportCount",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImportQty",SqlDbType.Int),
					new SqlParameter("@BatchID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.BatchNo;
			parameters[1].Value = pEntity.CardMedium;
			parameters[2].Value = pEntity.RegionNumber;
			parameters[3].Value = pEntity.CardPrefix;
			parameters[4].Value = pEntity.VipCardTypeCode;
			parameters[5].Value = pEntity.StartCardNo;
			parameters[6].Value = pEntity.EndCardNo;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.OutliersQty;
			parameters[9].Value = pEntity.CostPrice;
			parameters[10].Value = pEntity.ExportCount;
			parameters[11].Value = pEntity.Remark;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.CreateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pEntity.CustomerID;
			parameters[18].Value = pEntity.ImageUrl;
			parameters[19].Value = pEntity.ImportQty;
			parameters[20].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.BatchID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipCardBatchEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardBatch] where BatchID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipCardBatchEntity m = null;
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
        public VipCardBatchEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardBatch] where 1=1  and isdelete=0");
            //读取数据
            List<VipCardBatchEntity> list = new List<VipCardBatchEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardBatchEntity m;
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
        public void Update(VipCardBatchEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(VipCardBatchEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.BatchID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardBatch] set ");
                        if (pIsUpdateNullField || pEntity.BatchNo!=null)
                strSql.Append( "[BatchNo]=@BatchNo,");
            if (pIsUpdateNullField || pEntity.CardMedium!=null)
                strSql.Append( "[CardMedium]=@CardMedium,");
            if (pIsUpdateNullField || pEntity.RegionNumber!=null)
                strSql.Append( "[RegionNumber]=@RegionNumber,");
            if (pIsUpdateNullField || pEntity.CardPrefix!=null)
                strSql.Append( "[CardPrefix]=@CardPrefix,");
            if (pIsUpdateNullField || pEntity.VipCardTypeCode!=null)
                strSql.Append( "[VipCardTypeCode]=@VipCardTypeCode,");
            if (pIsUpdateNullField || pEntity.StartCardNo!=null)
                strSql.Append( "[StartCardNo]=@StartCardNo,");
            if (pIsUpdateNullField || pEntity.EndCardNo!=null)
                strSql.Append( "[EndCardNo]=@EndCardNo,");
            if (pIsUpdateNullField || pEntity.Qty!=null)
                strSql.Append( "[Qty]=@Qty,");
            if (pIsUpdateNullField || pEntity.OutliersQty!=null)
                strSql.Append( "[OutliersQty]=@OutliersQty,");
            if (pIsUpdateNullField || pEntity.CostPrice!=null)
                strSql.Append( "[CostPrice]=@CostPrice,");
            if (pIsUpdateNullField || pEntity.ExportCount!=null)
                strSql.Append( "[ExportCount]=@ExportCount,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.ImportQty!=null)
                strSql.Append( "[ImportQty]=@ImportQty");
            strSql.Append(" where BatchID=@BatchID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@BatchNo",SqlDbType.Int),
					new SqlParameter("@CardMedium",SqlDbType.VarChar),
					new SqlParameter("@RegionNumber",SqlDbType.VarChar),
					new SqlParameter("@CardPrefix",SqlDbType.NVarChar),
					new SqlParameter("@VipCardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@StartCardNo",SqlDbType.VarChar),
					new SqlParameter("@EndCardNo",SqlDbType.VarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@OutliersQty",SqlDbType.Int),
					new SqlParameter("@CostPrice",SqlDbType.Decimal),
					new SqlParameter("@ExportCount",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImportQty",SqlDbType.Int),
					new SqlParameter("@BatchID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.BatchNo;
			parameters[1].Value = pEntity.CardMedium;
			parameters[2].Value = pEntity.RegionNumber;
			parameters[3].Value = pEntity.CardPrefix;
			parameters[4].Value = pEntity.VipCardTypeCode;
			parameters[5].Value = pEntity.StartCardNo;
			parameters[6].Value = pEntity.EndCardNo;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.OutliersQty;
			parameters[9].Value = pEntity.CostPrice;
			parameters[10].Value = pEntity.ExportCount;
			parameters[11].Value = pEntity.Remark;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.CustomerID;
			parameters[15].Value = pEntity.ImageUrl;
			parameters[16].Value = pEntity.ImportQty;
			parameters[17].Value = pEntity.BatchID;

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
        public void Update(VipCardBatchEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardBatchEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipCardBatchEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.BatchID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.BatchID.Value, pTran);           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [VipCardBatch] set  isdelete=1 where BatchID=@BatchID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@BatchID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipCardBatchEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.BatchID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.BatchID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipCardBatchEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [VipCardBatch] set  isdelete=1 where BatchID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public VipCardBatchEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardBatch] where 1=1  and isdelete=0 ");
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
            List<VipCardBatchEntity> list = new List<VipCardBatchEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardBatchEntity m;
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
        public PagedQueryResult<VipCardBatchEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [BatchID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardBatch] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardBatch] where 1=1  and isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<VipCardBatchEntity> result = new PagedQueryResult<VipCardBatchEntity>();
            List<VipCardBatchEntity> list = new List<VipCardBatchEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardBatchEntity m;
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
        public VipCardBatchEntity[] QueryByEntity(VipCardBatchEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<VipCardBatchEntity> PagedQueryByEntity(VipCardBatchEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardBatchEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.BatchID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BatchID", Value = pQueryEntity.BatchID });
            if (pQueryEntity.BatchNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BatchNo", Value = pQueryEntity.BatchNo });
            if (pQueryEntity.CardMedium!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardMedium", Value = pQueryEntity.CardMedium });
            if (pQueryEntity.RegionNumber!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RegionNumber", Value = pQueryEntity.RegionNumber });
            if (pQueryEntity.CardPrefix!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardPrefix", Value = pQueryEntity.CardPrefix });
            if (pQueryEntity.VipCardTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeCode", Value = pQueryEntity.VipCardTypeCode });
            if (pQueryEntity.StartCardNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartCardNo", Value = pQueryEntity.StartCardNo });
            if (pQueryEntity.EndCardNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndCardNo", Value = pQueryEntity.EndCardNo });
            if (pQueryEntity.Qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.OutliersQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutliersQty", Value = pQueryEntity.OutliersQty });
            if (pQueryEntity.CostPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CostPrice", Value = pQueryEntity.CostPrice });
            if (pQueryEntity.ExportCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExportCount", Value = pQueryEntity.ExportCount });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.ImportQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImportQty", Value = pQueryEntity.ImportQty });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out VipCardBatchEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipCardBatchEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["BatchID"] != DBNull.Value)
			{
				pInstance.BatchID =  (Guid)pReader["BatchID"];
			}
			if (pReader["BatchNo"] != DBNull.Value)
			{
				pInstance.BatchNo =   Convert.ToInt32(pReader["BatchNo"]);
			}
			if (pReader["CardMedium"] != DBNull.Value)
			{
				pInstance.CardMedium =  Convert.ToString(pReader["CardMedium"]);
			}
			if (pReader["RegionNumber"] != DBNull.Value)
			{
				pInstance.RegionNumber =  Convert.ToString(pReader["RegionNumber"]);
			}
			if (pReader["CardPrefix"] != DBNull.Value)
			{
				pInstance.CardPrefix =  Convert.ToString(pReader["CardPrefix"]);
			}
			if (pReader["VipCardTypeCode"] != DBNull.Value)
			{
				pInstance.VipCardTypeCode =  Convert.ToString(pReader["VipCardTypeCode"]);
			}
			if (pReader["StartCardNo"] != DBNull.Value)
			{
				pInstance.StartCardNo =  Convert.ToString(pReader["StartCardNo"]);
			}
			if (pReader["EndCardNo"] != DBNull.Value)
			{
				pInstance.EndCardNo =  Convert.ToString(pReader["EndCardNo"]);
			}
			if (pReader["Qty"] != DBNull.Value)
			{
				pInstance.Qty =   Convert.ToInt32(pReader["Qty"]);
			}
			if (pReader["OutliersQty"] != DBNull.Value)
			{
				pInstance.OutliersQty =   Convert.ToInt32(pReader["OutliersQty"]);
			}
			if (pReader["CostPrice"] != DBNull.Value)
			{
				pInstance.CostPrice =  Convert.ToDecimal(pReader["CostPrice"]);
			}
			if (pReader["ExportCount"] != DBNull.Value)
			{
				pInstance.ExportCount =   Convert.ToInt32(pReader["ExportCount"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["ImportQty"] != DBNull.Value)
			{
				pInstance.ImportQty =   Convert.ToInt32(pReader["ImportQty"]);
			}

        }
        #endregion
    }
}
