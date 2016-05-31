/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 18:30:11
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
    /// 表R_WxO2OPanel_ItemTopTen的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_WxO2OPanel_ItemTopTenDAO : Base.BaseCPOSDAO, ICRUDable<R_WxO2OPanel_ItemTopTenEntity>, IQueryable<R_WxO2OPanel_ItemTopTenEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_WxO2OPanel_ItemTopTenDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(R_WxO2OPanel_ItemTopTenEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(R_WxO2OPanel_ItemTopTenEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_WxO2OPanel_ItemTopTen](");
            strSql.Append("[CustomerId],[DateCode],[ItemType],[ItemID],[ItemName],[SortIndex],[ItemSoldCount],[ItemSoldAmount],[ItemUV],[ItemPV],[CreateTime],[LogIDs],[ID])");
            strSql.Append(" values (");
            strSql.Append("@CustomerId,@DateCode,@ItemType,@ItemID,@ItemName,@SortIndex,@ItemSoldCount,@ItemSoldAmount,@ItemUV,@ItemPV,@CreateTime,@LogIDs,@ID)");

            Guid? pkGuid;
            if (pEntity.ID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@ItemName",SqlDbType.NVarChar),
					new SqlParameter("@SortIndex",SqlDbType.Int),
					new SqlParameter("@ItemSoldCount",SqlDbType.Int),
					new SqlParameter("@ItemSoldAmount",SqlDbType.Decimal),
					new SqlParameter("@ItemUV",SqlDbType.Int),
					new SqlParameter("@ItemPV",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LogIDs",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CustomerId;
            parameters[1].Value = pEntity.DateCode;
            parameters[2].Value = pEntity.ItemType;
            parameters[3].Value = pEntity.ItemID;
            parameters[4].Value = pEntity.ItemName;
            parameters[5].Value = pEntity.SortIndex;
            parameters[6].Value = pEntity.ItemSoldCount;
            parameters[7].Value = pEntity.ItemSoldAmount;
            parameters[8].Value = pEntity.ItemUV;
            parameters[9].Value = pEntity.ItemPV;
            parameters[10].Value = pEntity.CreateTime;
            parameters[11].Value = pEntity.LogIDs;
            parameters[12].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.ID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public R_WxO2OPanel_ItemTopTenEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_ItemTopTen] where ID='{0}'  ", id.ToString());
            //读取数据
            R_WxO2OPanel_ItemTopTenEntity m = null;
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
        public R_WxO2OPanel_ItemTopTenEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_ItemTopTen] where 1=1 ");
            //读取数据
            List<R_WxO2OPanel_ItemTopTenEntity> list = new List<R_WxO2OPanel_ItemTopTenEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_ItemTopTenEntity m;
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
        public void Update(R_WxO2OPanel_ItemTopTenEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(R_WxO2OPanel_ItemTopTenEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [R_WxO2OPanel_ItemTopTen] set ");
            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.DateCode != null)
                strSql.Append("[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.ItemType != null)
                strSql.Append("[ItemType]=@ItemType,");
            if (pIsUpdateNullField || pEntity.ItemID != null)
                strSql.Append("[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.ItemName != null)
                strSql.Append("[ItemName]=@ItemName,");
            if (pIsUpdateNullField || pEntity.SortIndex != null)
                strSql.Append("[SortIndex]=@SortIndex,");
            if (pIsUpdateNullField || pEntity.ItemSoldCount != null)
                strSql.Append("[ItemSoldCount]=@ItemSoldCount,");
            if (pIsUpdateNullField || pEntity.ItemSoldAmount != null)
                strSql.Append("[ItemSoldAmount]=@ItemSoldAmount,");
            if (pIsUpdateNullField || pEntity.ItemUV != null)
                strSql.Append("[ItemUV]=@ItemUV,");
            if (pIsUpdateNullField || pEntity.ItemPV != null)
                strSql.Append("[ItemPV]=@ItemPV,");
            if (pIsUpdateNullField || pEntity.LogIDs != null)
                strSql.Append("[LogIDs]=@LogIDs");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@ItemName",SqlDbType.NVarChar),
					new SqlParameter("@SortIndex",SqlDbType.Int),
					new SqlParameter("@ItemSoldCount",SqlDbType.Int),
					new SqlParameter("@ItemSoldAmount",SqlDbType.Decimal),
					new SqlParameter("@ItemUV",SqlDbType.Int),
					new SqlParameter("@ItemPV",SqlDbType.Int),
					new SqlParameter("@LogIDs",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CustomerId;
            parameters[1].Value = pEntity.DateCode;
            parameters[2].Value = pEntity.ItemType;
            parameters[3].Value = pEntity.ItemID;
            parameters[4].Value = pEntity.ItemName;
            parameters[5].Value = pEntity.SortIndex;
            parameters[6].Value = pEntity.ItemSoldCount;
            parameters[7].Value = pEntity.ItemSoldAmount;
            parameters[8].Value = pEntity.ItemUV;
            parameters[9].Value = pEntity.ItemPV;
            parameters[10].Value = pEntity.LogIDs;
            parameters[11].Value = pEntity.ID;

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
        public void Update(R_WxO2OPanel_ItemTopTenEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_WxO2OPanel_ItemTopTenEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(R_WxO2OPanel_ItemTopTenEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ID.Value, pTran);
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
            sql.AppendLine("update [R_WxO2OPanel_ItemTopTen] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(R_WxO2OPanel_ItemTopTenEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(R_WxO2OPanel_ItemTopTenEntity[] pEntities)
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
            sql.AppendLine("update [R_WxO2OPanel_ItemTopTen] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_WxO2OPanel_ItemTopTenEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_ItemTopTen] where 1=1  ");
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
            List<R_WxO2OPanel_ItemTopTenEntity> list = new List<R_WxO2OPanel_ItemTopTenEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_ItemTopTenEntity m;
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
        public PagedQueryResult<R_WxO2OPanel_ItemTopTenEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [R_WxO2OPanel_ItemTopTen] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [R_WxO2OPanel_ItemTopTen] where 1=1  ");
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
            PagedQueryResult<R_WxO2OPanel_ItemTopTenEntity> result = new PagedQueryResult<R_WxO2OPanel_ItemTopTenEntity>();
            List<R_WxO2OPanel_ItemTopTenEntity> list = new List<R_WxO2OPanel_ItemTopTenEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_ItemTopTenEntity m;
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
        public R_WxO2OPanel_ItemTopTenEntity[] QueryByEntity(R_WxO2OPanel_ItemTopTenEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_WxO2OPanel_ItemTopTenEntity> PagedQueryByEntity(R_WxO2OPanel_ItemTopTenEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_WxO2OPanel_ItemTopTenEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.CustomerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.DateCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.ItemType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemType", Value = pQueryEntity.ItemType });
            if (pQueryEntity.ItemID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.ItemName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemName", Value = pQueryEntity.ItemName });
            if (pQueryEntity.SortIndex != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SortIndex", Value = pQueryEntity.SortIndex });
            if (pQueryEntity.ItemSoldCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemSoldCount", Value = pQueryEntity.ItemSoldCount });
            if (pQueryEntity.ItemSoldAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemSoldAmount", Value = pQueryEntity.ItemSoldAmount });
            if (pQueryEntity.ItemUV != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemUV", Value = pQueryEntity.ItemUV });
            if (pQueryEntity.ItemPV != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemPV", Value = pQueryEntity.ItemPV });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LogIDs != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogIDs", Value = pQueryEntity.LogIDs });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out R_WxO2OPanel_ItemTopTenEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new R_WxO2OPanel_ItemTopTenEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["ID"] != DBNull.Value)
            {
                pInstance.ID = (Guid)pReader["ID"];
            }
            if (pReader["CustomerId"] != DBNull.Value)
            {
                pInstance.CustomerId = Convert.ToString(pReader["CustomerId"]);
            }
            if (pReader["DateCode"] != DBNull.Value)
            {
                pInstance.DateCode = (DateTime?)pReader["DateCode"];
            }
            if (pReader["ItemType"] != DBNull.Value)
            {
                pInstance.ItemType = Convert.ToInt32(pReader["ItemType"]);
            }
            if (pReader["ItemID"] != DBNull.Value)
            {
                pInstance.ItemID = Convert.ToString(pReader["ItemID"]);
            }
            if (pReader["ItemName"] != DBNull.Value)
            {
                pInstance.ItemName = Convert.ToString(pReader["ItemName"]);
            }
            if (pReader["SortIndex"] != DBNull.Value)
            {
                pInstance.SortIndex = Convert.ToInt32(pReader["SortIndex"]);
            }
            if (pReader["ItemSoldCount"] != DBNull.Value)
            {
                pInstance.ItemSoldCount = Convert.ToInt32(pReader["ItemSoldCount"]);
            }
            if (pReader["ItemSoldAmount"] != DBNull.Value)
            {
                pInstance.ItemSoldAmount = Convert.ToDecimal(pReader["ItemSoldAmount"]);
            }
            if (pReader["ItemUV"] != DBNull.Value)
            {
                pInstance.ItemUV = Convert.ToInt32(pReader["ItemUV"]);
            }
            if (pReader["ItemPV"] != DBNull.Value)
            {
                pInstance.ItemPV = Convert.ToInt32(pReader["ItemPV"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LogIDs"] != DBNull.Value)
            {
                pInstance.LogIDs = Convert.ToString(pReader["LogIDs"]);
            }

        }
        #endregion
    }
}
