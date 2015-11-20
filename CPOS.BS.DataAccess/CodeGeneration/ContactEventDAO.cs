/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/11/11 17:58:00
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
    /// 表ContactEvent的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ContactEventDAO : Base.BaseCPOSDAO, ICRUDable<ContactEventEntity>, IQueryable<ContactEventEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ContactEventDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ContactEventEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ContactEventEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ContactEvent](");
            strSql.Append("[ContactTypeCode],[ContactEventName],[BeginDate],[EndDate],[PrizeType],[PrizeCount],[Integral],[CouponTypeID],[EventId],[ChanceCount],[ShareEventId],[RewardNumber],[Status],[IsDelete],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[ContactEventId])");
            strSql.Append(" values (");
            strSql.Append("@ContactTypeCode,@ContactEventName,@BeginDate,@EndDate,@PrizeType,@PrizeCount,@Integral,@CouponTypeID,@EventId,@ChanceCount,@ShareEventId,@RewardNumber,@Status,@IsDelete,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@ContactEventId)");            

			Guid? pkGuid;
			if (pEntity.ContactEventId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ContactEventId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ContactTypeCode",SqlDbType.VarChar),
					new SqlParameter("@ContactEventName",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@PrizeType",SqlDbType.VarChar),
					new SqlParameter("@PrizeCount",SqlDbType.Int),
					new SqlParameter("@Integral",SqlDbType.Int),
					new SqlParameter("@CouponTypeID",SqlDbType.NVarChar),
					new SqlParameter("@EventId",SqlDbType.NVarChar),
					new SqlParameter("@ChanceCount",SqlDbType.Int),
					new SqlParameter("@ShareEventId",SqlDbType.NVarChar),
					new SqlParameter("@RewardNumber",SqlDbType.Char),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@ContactEventId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.ContactTypeCode;
			parameters[1].Value = pEntity.ContactEventName;
			parameters[2].Value = pEntity.BeginDate;
			parameters[3].Value = pEntity.EndDate;
			parameters[4].Value = pEntity.PrizeType;
			parameters[5].Value = pEntity.PrizeCount;
			parameters[6].Value = pEntity.Integral;
			parameters[7].Value = pEntity.CouponTypeID;
			parameters[8].Value = pEntity.EventId;
			parameters[9].Value = pEntity.ChanceCount;
			parameters[10].Value = pEntity.ShareEventId;
			parameters[11].Value = pEntity.RewardNumber;
			parameters[12].Value = pEntity.Status;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.CustomerID;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ContactEventId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ContactEventEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ContactEvent] where ContactEventId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            ContactEventEntity m = null;
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
        public ContactEventEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ContactEvent] where 1=1  and isdelete=0");
            //读取数据
            List<ContactEventEntity> list = new List<ContactEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ContactEventEntity m;
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
        public void Update(ContactEventEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(ContactEventEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ContactEventId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ContactEvent] set ");
                        if (pIsUpdateNullField || pEntity.ContactTypeCode!=null)
                strSql.Append( "[ContactTypeCode]=@ContactTypeCode,");
            if (pIsUpdateNullField || pEntity.ContactEventName!=null)
                strSql.Append( "[ContactEventName]=@ContactEventName,");
            if (pIsUpdateNullField || pEntity.BeginDate!=null)
                strSql.Append( "[BeginDate]=@BeginDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.PrizeType!=null)
                strSql.Append( "[PrizeType]=@PrizeType,");
            if (pIsUpdateNullField || pEntity.PrizeCount!=null)
                strSql.Append( "[PrizeCount]=@PrizeCount,");
            if (pIsUpdateNullField || pEntity.Integral!=null)
                strSql.Append( "[Integral]=@Integral,");
            if (pIsUpdateNullField || pEntity.CouponTypeID!=null)
                strSql.Append( "[CouponTypeID]=@CouponTypeID,");
            if (pIsUpdateNullField || pEntity.EventId!=null)
                strSql.Append( "[EventId]=@EventId,");
            if (pIsUpdateNullField || pEntity.ChanceCount!=null)
                strSql.Append( "[ChanceCount]=@ChanceCount,");
            if (pIsUpdateNullField || pEntity.ShareEventId!=null)
                strSql.Append( "[ShareEventId]=@ShareEventId,");
            if (pIsUpdateNullField || pEntity.RewardNumber!=null)
                strSql.Append( "[RewardNumber]=@RewardNumber,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where ContactEventId=@ContactEventId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ContactTypeCode",SqlDbType.VarChar),
					new SqlParameter("@ContactEventName",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@PrizeType",SqlDbType.VarChar),
					new SqlParameter("@PrizeCount",SqlDbType.Int),
					new SqlParameter("@Integral",SqlDbType.Int),
					new SqlParameter("@CouponTypeID",SqlDbType.NVarChar),
					new SqlParameter("@EventId",SqlDbType.NVarChar),
					new SqlParameter("@ChanceCount",SqlDbType.Int),
					new SqlParameter("@ShareEventId",SqlDbType.NVarChar),
					new SqlParameter("@RewardNumber",SqlDbType.Char),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@ContactEventId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.ContactTypeCode;
			parameters[1].Value = pEntity.ContactEventName;
			parameters[2].Value = pEntity.BeginDate;
			parameters[3].Value = pEntity.EndDate;
			parameters[4].Value = pEntity.PrizeType;
			parameters[5].Value = pEntity.PrizeCount;
			parameters[6].Value = pEntity.Integral;
			parameters[7].Value = pEntity.CouponTypeID;
			parameters[8].Value = pEntity.EventId;
			parameters[9].Value = pEntity.ChanceCount;
			parameters[10].Value = pEntity.ShareEventId;
			parameters[11].Value = pEntity.RewardNumber;
			parameters[12].Value = pEntity.Status;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.ContactEventId;

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
        public void Update(ContactEventEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ContactEventEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ContactEventEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ContactEventId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ContactEventId.Value, pTran);           
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
            sql.AppendLine("update [ContactEvent] set  isdelete=1 where ContactEventId=@ContactEventId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ContactEventId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(ContactEventEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ContactEventId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ContactEventId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(ContactEventEntity[] pEntities)
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
            sql.AppendLine("update [ContactEvent] set  isdelete=1 where ContactEventId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ContactEventEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ContactEvent] where 1=1  and isdelete=0 ");
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
            List<ContactEventEntity> list = new List<ContactEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ContactEventEntity m;
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
        public PagedQueryResult<ContactEventEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ContactEventId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [ContactEvent] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [ContactEvent] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<ContactEventEntity> result = new PagedQueryResult<ContactEventEntity>();
            List<ContactEventEntity> list = new List<ContactEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ContactEventEntity m;
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
        public ContactEventEntity[] QueryByEntity(ContactEventEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ContactEventEntity> PagedQueryByEntity(ContactEventEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ContactEventEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ContactEventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContactEventId", Value = pQueryEntity.ContactEventId });
            if (pQueryEntity.ContactTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContactTypeCode", Value = pQueryEntity.ContactTypeCode });
            if (pQueryEntity.ContactEventName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContactEventName", Value = pQueryEntity.ContactEventName });
            if (pQueryEntity.BeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginDate", Value = pQueryEntity.BeginDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.PrizeType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizeType", Value = pQueryEntity.PrizeType });
            if (pQueryEntity.PrizeCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizeCount", Value = pQueryEntity.PrizeCount });
            if (pQueryEntity.Integral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Integral", Value = pQueryEntity.Integral });
            if (pQueryEntity.CouponTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponTypeID", Value = pQueryEntity.CouponTypeID });
            if (pQueryEntity.EventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
            if (pQueryEntity.ChanceCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChanceCount", Value = pQueryEntity.ChanceCount });
            if (pQueryEntity.ShareEventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareEventId", Value = pQueryEntity.ShareEventId });
            if (pQueryEntity.RewardNumber!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardNumber", Value = pQueryEntity.RewardNumber });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out ContactEventEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new ContactEventEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ContactEventId"] != DBNull.Value)
			{
				pInstance.ContactEventId =  (Guid)pReader["ContactEventId"];
			}
			if (pReader["ContactTypeCode"] != DBNull.Value)
			{
				pInstance.ContactTypeCode =  Convert.ToString(pReader["ContactTypeCode"]);
			}
			if (pReader["ContactEventName"] != DBNull.Value)
			{
				pInstance.ContactEventName =  Convert.ToString(pReader["ContactEventName"]);
			}
			if (pReader["BeginDate"] != DBNull.Value)
			{
				pInstance.BeginDate =  Convert.ToDateTime(pReader["BeginDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["PrizeType"] != DBNull.Value)
			{
				pInstance.PrizeType =  Convert.ToString(pReader["PrizeType"]);
			}
			if (pReader["PrizeCount"] != DBNull.Value)
			{
				pInstance.PrizeCount =   Convert.ToInt32(pReader["PrizeCount"]);
			}
			if (pReader["Integral"] != DBNull.Value)
			{
				pInstance.Integral =   Convert.ToInt32(pReader["Integral"]);
			}
			if (pReader["CouponTypeID"] != DBNull.Value)
			{
				pInstance.CouponTypeID =  Convert.ToString(pReader["CouponTypeID"]);
			}
			if (pReader["EventId"] != DBNull.Value)
			{
				pInstance.EventId =  Convert.ToString(pReader["EventId"]);
			}
			if (pReader["ChanceCount"] != DBNull.Value)
			{
				pInstance.ChanceCount =   Convert.ToInt32(pReader["ChanceCount"]);
			}
			if (pReader["ShareEventId"] != DBNull.Value)
			{
				pInstance.ShareEventId =  Convert.ToString(pReader["ShareEventId"]);
			}
			if (pReader["RewardNumber"] != DBNull.Value)
			{
				pInstance.RewardNumber =  Convert.ToString(pReader["RewardNumber"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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

        }
        #endregion
    }
}
