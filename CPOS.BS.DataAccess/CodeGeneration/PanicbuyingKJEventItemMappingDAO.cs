/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/27 20:28:30
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
    /// 表PanicbuyingKJEventItemMapping的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingKJEventItemMappingDAO : BaseCPOSDAO, ICRUDable<PanicbuyingKJEventItemMappingEntity>, IQueryable<PanicbuyingKJEventItemMappingEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PanicbuyingKJEventItemMappingDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(PanicbuyingKJEventItemMappingEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [PanicbuyingKJEventItemMapping](");
            strSql.Append("[EventId],[ItemID],[MinPrice],[MinBasePrice],[SoldQty],[Qty],[KeepQty],[SinglePurchaseQty],[DiscountRate],[PromotePersonCount],[BargainPersonCount],[PurchasePersonCount],[Status],[StatusReason],[DisplayIndex],[customerId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[BargaingingInterval],[EventItemMappingID])");
            strSql.Append(" values (");

            strSql.Append("@EventId,@ItemID,@MinPrice,@MinBasePrice,@SoldQty,@Qty,@KeepQty,@SinglePurchaseQty,@DiscountRate,@PromotePersonCount,@BargainPersonCount,@PurchasePersonCount,@Status,@StatusReason,@DisplayIndex,@customerId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@BargaingingInterval,@EventItemMappingID)");            


            Guid? pkGuid;
            if (pEntity.EventItemMappingID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.EventItemMappingID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemID",SqlDbType.VarChar),
					new SqlParameter("@MinPrice",SqlDbType.Decimal),
					new SqlParameter("@MinBasePrice",SqlDbType.Decimal),
					new SqlParameter("@SoldQty",SqlDbType.Int),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@KeepQty",SqlDbType.Int),
					new SqlParameter("@SinglePurchaseQty",SqlDbType.Int),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@PromotePersonCount",SqlDbType.Int),
					new SqlParameter("@BargainPersonCount",SqlDbType.Int),
					new SqlParameter("@PurchasePersonCount",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@StatusReason",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@customerId",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@BargaingingInterval",SqlDbType.Decimal),
					new SqlParameter("@EventItemMappingID",SqlDbType.UniqueIdentifier)
            };

			parameters[0].Value = pEntity.EventId;
			parameters[1].Value = pEntity.ItemID;
			parameters[2].Value = pEntity.MinPrice;
			parameters[3].Value = pEntity.MinBasePrice;
			parameters[4].Value = pEntity.SoldQty;
			parameters[5].Value = pEntity.Qty;
			parameters[6].Value = pEntity.KeepQty;
			parameters[7].Value = pEntity.SinglePurchaseQty;
			parameters[8].Value = pEntity.DiscountRate;
			parameters[9].Value = pEntity.PromotePersonCount;
			parameters[10].Value = pEntity.BargainPersonCount;
			parameters[11].Value = pEntity.PurchasePersonCount;
			parameters[12].Value = pEntity.Status;
			parameters[13].Value = pEntity.StatusReason;
			parameters[14].Value = pEntity.DisplayIndex;
			parameters[15].Value = pEntity.customerId;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.IsDelete;
			parameters[21].Value = pEntity.BargaingingInterval;
			parameters[22].Value = pkGuid;


            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.EventItemMappingID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public PanicbuyingKJEventItemMappingEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventItemMapping] where EventItemMappingID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            PanicbuyingKJEventItemMappingEntity m = null;
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
        public PanicbuyingKJEventItemMappingEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0");
            //读取数据
            List<PanicbuyingKJEventItemMappingEntity> list = new List<PanicbuyingKJEventItemMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventItemMappingEntity m;
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
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.EventItemMappingID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PanicbuyingKJEventItemMapping] set ");

                        if (pIsUpdateNullField || pEntity.EventId!=null)
                strSql.Append( "[EventId]=@EventId,");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.MinPrice!=null)
                strSql.Append( "[MinPrice]=@MinPrice,");
            if (pIsUpdateNullField || pEntity.MinBasePrice!=null)
                strSql.Append( "[MinBasePrice]=@MinBasePrice,");
            if (pIsUpdateNullField || pEntity.SoldQty!=null)
                strSql.Append( "[SoldQty]=@SoldQty,");
            if (pIsUpdateNullField || pEntity.Qty!=null)
                strSql.Append( "[Qty]=@Qty,");
            if (pIsUpdateNullField || pEntity.KeepQty!=null)
                strSql.Append( "[KeepQty]=@KeepQty,");
            if (pIsUpdateNullField || pEntity.SinglePurchaseQty!=null)
                strSql.Append( "[SinglePurchaseQty]=@SinglePurchaseQty,");
            if (pIsUpdateNullField || pEntity.DiscountRate!=null)
                strSql.Append( "[DiscountRate]=@DiscountRate,");
            if (pIsUpdateNullField || pEntity.PromotePersonCount!=null)
                strSql.Append( "[PromotePersonCount]=@PromotePersonCount,");
            if (pIsUpdateNullField || pEntity.BargainPersonCount!=null)
                strSql.Append( "[BargainPersonCount]=@BargainPersonCount,");
            if (pIsUpdateNullField || pEntity.PurchasePersonCount!=null)
                strSql.Append( "[PurchasePersonCount]=@PurchasePersonCount,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.StatusReason!=null)
                strSql.Append( "[StatusReason]=@StatusReason,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.customerId!=null)
                strSql.Append( "[customerId]=@customerId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.BargaingingInterval!=null)
                strSql.Append( "[BargaingingInterval]=@BargaingingInterval");

            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where EventItemMappingID=@EventItemMappingID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemID",SqlDbType.VarChar),
					new SqlParameter("@MinPrice",SqlDbType.Decimal),
					new SqlParameter("@MinBasePrice",SqlDbType.Decimal),
					new SqlParameter("@SoldQty",SqlDbType.Int),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@KeepQty",SqlDbType.Int),
					new SqlParameter("@SinglePurchaseQty",SqlDbType.Int),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@PromotePersonCount",SqlDbType.Int),
					new SqlParameter("@BargainPersonCount",SqlDbType.Int),
					new SqlParameter("@PurchasePersonCount",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@StatusReason",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@customerId",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@BargaingingInterval",SqlDbType.Decimal),
					new SqlParameter("@EventItemMappingID",SqlDbType.UniqueIdentifier)
            };

			parameters[0].Value = pEntity.EventId;
			parameters[1].Value = pEntity.ItemID;
			parameters[2].Value = pEntity.MinPrice;
			parameters[3].Value = pEntity.MinBasePrice;
			parameters[4].Value = pEntity.SoldQty;
			parameters[5].Value = pEntity.Qty;
			parameters[6].Value = pEntity.KeepQty;
			parameters[7].Value = pEntity.SinglePurchaseQty;
			parameters[8].Value = pEntity.DiscountRate;
			parameters[9].Value = pEntity.PromotePersonCount;
			parameters[10].Value = pEntity.BargainPersonCount;
			parameters[11].Value = pEntity.PurchasePersonCount;
			parameters[12].Value = pEntity.Status;
			parameters[13].Value = pEntity.StatusReason;
			parameters[14].Value = pEntity.DisplayIndex;
			parameters[15].Value = pEntity.customerId;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.BargaingingInterval;
			parameters[19].Value = pEntity.EventItemMappingID;


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
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PanicbuyingKJEventItemMappingEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.EventItemMappingID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.EventItemMappingID.Value, pTran);
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
            sql.AppendLine("update [PanicbuyingKJEventItemMapping] set  isdelete=1 where EventItemMappingID=@EventItemMappingID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@EventItemMappingID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(PanicbuyingKJEventItemMappingEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.EventItemMappingID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.EventItemMappingID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(PanicbuyingKJEventItemMappingEntity[] pEntities)
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
            sql.AppendLine("update [PanicbuyingKJEventItemMapping] set  isdelete=1 where EventItemMappingID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PanicbuyingKJEventItemMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
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
            List<PanicbuyingKJEventItemMappingEntity> list = new List<PanicbuyingKJEventItemMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventItemMappingEntity m;
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
        public PagedQueryResult<PanicbuyingKJEventItemMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EventItemMappingID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<PanicbuyingKJEventItemMappingEntity> result = new PagedQueryResult<PanicbuyingKJEventItemMappingEntity>();
            List<PanicbuyingKJEventItemMappingEntity> list = new List<PanicbuyingKJEventItemMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventItemMappingEntity m;
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
        public PanicbuyingKJEventItemMappingEntity[] QueryByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PanicbuyingKJEventItemMappingEntity> PagedQueryByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EventItemMappingID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventItemMappingID", Value = pQueryEntity.EventItemMappingID });
            if (pQueryEntity.EventId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
            if (pQueryEntity.ItemID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.MinPrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinPrice", Value = pQueryEntity.MinPrice });
            if (pQueryEntity.MinBasePrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinBasePrice", Value = pQueryEntity.MinBasePrice });
            if (pQueryEntity.SoldQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SoldQty", Value = pQueryEntity.SoldQty });
            if (pQueryEntity.Qty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.KeepQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KeepQty", Value = pQueryEntity.KeepQty });
            if (pQueryEntity.SinglePurchaseQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SinglePurchaseQty", Value = pQueryEntity.SinglePurchaseQty });
            if (pQueryEntity.DiscountRate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscountRate", Value = pQueryEntity.DiscountRate });
            if (pQueryEntity.PromotePersonCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PromotePersonCount", Value = pQueryEntity.PromotePersonCount });
            if (pQueryEntity.BargainPersonCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargainPersonCount", Value = pQueryEntity.BargainPersonCount });
            if (pQueryEntity.PurchasePersonCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchasePersonCount", Value = pQueryEntity.PurchasePersonCount });
            if (pQueryEntity.Status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.StatusReason != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusReason", Value = pQueryEntity.StatusReason });
            if (pQueryEntity.DisplayIndex != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.customerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customerId", Value = pQueryEntity.customerId });
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
            if (pQueryEntity.BargaingingInterval!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargaingingInterval", Value = pQueryEntity.BargaingingInterval });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out PanicbuyingKJEventItemMappingEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new PanicbuyingKJEventItemMappingEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["EventItemMappingID"] != DBNull.Value)
            {
                pInstance.EventItemMappingID = (Guid)pReader["EventItemMappingID"];
            }
            if (pReader["EventId"] != DBNull.Value)
            {
                pInstance.EventId = (Guid)pReader["EventId"];
            }
            if (pReader["ItemID"] != DBNull.Value)
            {
                pInstance.ItemID = Convert.ToString(pReader["ItemID"]);
                var ItemData = new T_ItemDAO(this.CurrentUserInfo).GetByID(pInstance.ItemID);
                if (ItemData != null)
                    pInstance.ItemName = ItemData.item_name;
            }
            if (pReader["MinPrice"] != DBNull.Value)
            {
                pInstance.MinPrice = Convert.ToDecimal(pReader["MinPrice"]);
            }
            if (pReader["MinBasePrice"] != DBNull.Value)
            {
                pInstance.MinBasePrice = Convert.ToDecimal(pReader["MinBasePrice"]);
            }
            if (pReader["SoldQty"] != DBNull.Value)
            {
                pInstance.SoldQty = Convert.ToInt32(pReader["SoldQty"]);
            }
            if (pReader["Qty"] != DBNull.Value)
            {
                pInstance.Qty = Convert.ToInt32(pReader["Qty"]);
            }
            if (pReader["KeepQty"] != DBNull.Value)
            {
                pInstance.KeepQty = Convert.ToInt32(pReader["KeepQty"]);
            }
            if (pReader["SinglePurchaseQty"] != DBNull.Value)
            {
                pInstance.SinglePurchaseQty = Convert.ToInt32(pReader["SinglePurchaseQty"]);
            }
            if (pReader["DiscountRate"] != DBNull.Value)
            {
                pInstance.DiscountRate = Convert.ToDecimal(pReader["DiscountRate"]);
            }
            if (pReader["PromotePersonCount"] != DBNull.Value)
            {
                pInstance.PromotePersonCount = Convert.ToInt32(pReader["PromotePersonCount"]);
            }
            if (pReader["BargainPersonCount"] != DBNull.Value)
            {
                pInstance.BargainPersonCount = Convert.ToInt32(pReader["BargainPersonCount"]);
            }
            if (pReader["PurchasePersonCount"] != DBNull.Value)
            {
                pInstance.PurchasePersonCount = Convert.ToInt32(pReader["PurchasePersonCount"]);
            }
            if (pReader["Status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToInt32(pReader["Status"]);
            }
            if (pReader["StatusReason"] != DBNull.Value)
            {
                pInstance.StatusReason = Convert.ToString(pReader["StatusReason"]);
            }
            if (pReader["DisplayIndex"] != DBNull.Value)
            {
                pInstance.DisplayIndex = Convert.ToInt32(pReader["DisplayIndex"]);
            }
            if (pReader["customerId"] != DBNull.Value)
            {
                pInstance.customerId = Convert.ToString(pReader["customerId"]);
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
			if (pReader["BargaingingInterval"] != DBNull.Value)
			{
				pInstance.BargaingingInterval =  Convert.ToDecimal(pReader["BargaingingInterval"]);
			}

        }
        #endregion
    }
}
