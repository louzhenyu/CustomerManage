/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 17:12:31
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
    /// 表C_Activity的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class C_ActivityDAO : Base.BaseCPOSDAO, ICRUDable<C_ActivityEntity>, IQueryable<C_ActivityEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public C_ActivityDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(C_ActivityEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(C_ActivityEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [C_Activity](");
            strSql.Append("[ActivityType],[ActivityName],[StartTime],[EndTime],[IsLongTime],[PointsMultiple],[SendCouponQty],[Status],[IsAllVipCardType],[IsVipGrouping],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ActivityID])");
            strSql.Append(" values (");
            strSql.Append("@ActivityType,@ActivityName,@StartTime,@EndTime,@IsLongTime,@PointsMultiple,@SendCouponQty,@Status,@IsAllVipCardType,@IsVipGrouping,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ActivityID)");            

			Guid? pkGuid;
			if (pEntity.ActivityID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ActivityID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ActivityType",SqlDbType.Int),
					new SqlParameter("@ActivityName",SqlDbType.NVarChar),
					new SqlParameter("@StartTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@IsLongTime",SqlDbType.Int),
					new SqlParameter("@PointsMultiple",SqlDbType.Decimal),
					new SqlParameter("@SendCouponQty",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@IsAllVipCardType",SqlDbType.Int),
					new SqlParameter("@IsVipGrouping",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ActivityID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.ActivityType;
			parameters[1].Value = pEntity.ActivityName;
			parameters[2].Value = pEntity.StartTime;
			parameters[3].Value = pEntity.EndTime;
			parameters[4].Value = pEntity.IsLongTime;
			parameters[5].Value = pEntity.PointsMultiple;
			parameters[6].Value = pEntity.SendCouponQty;
			parameters[7].Value = pEntity.Status;
			parameters[8].Value = pEntity.IsAllVipCardType;
			parameters[9].Value = pEntity.IsVipGrouping;
			parameters[10].Value = pEntity.CustomerID;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ActivityID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public C_ActivityEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [C_Activity] where ActivityID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            C_ActivityEntity m = null;
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
        public C_ActivityEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [C_Activity] where 1=1  and isdelete=0");
            //读取数据
            List<C_ActivityEntity> list = new List<C_ActivityEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    C_ActivityEntity m;
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
        public void Update(C_ActivityEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(C_ActivityEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ActivityID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [C_Activity] set ");
                        if (pIsUpdateNullField || pEntity.ActivityType!=null)
                strSql.Append( "[ActivityType]=@ActivityType,");
            if (pIsUpdateNullField || pEntity.ActivityName!=null)
                strSql.Append( "[ActivityName]=@ActivityName,");
            if (pIsUpdateNullField || pEntity.StartTime!=null)
                strSql.Append( "[StartTime]=@StartTime,");
            if (pIsUpdateNullField || pEntity.EndTime!=null)
                strSql.Append( "[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.IsLongTime!=null)
                strSql.Append( "[IsLongTime]=@IsLongTime,");
            if (pIsUpdateNullField || pEntity.PointsMultiple!=null)
                strSql.Append( "[PointsMultiple]=@PointsMultiple,");
            if (pIsUpdateNullField || pEntity.SendCouponQty!=null)
                strSql.Append( "[SendCouponQty]=@SendCouponQty,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.IsAllVipCardType!=null)
                strSql.Append( "[IsAllVipCardType]=@IsAllVipCardType,");
            if (pIsUpdateNullField || pEntity.IsVipGrouping!=null)
                strSql.Append( "[IsVipGrouping]=@IsVipGrouping,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where ActivityID=@ActivityID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ActivityType",SqlDbType.Int),
					new SqlParameter("@ActivityName",SqlDbType.NVarChar),
					new SqlParameter("@StartTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@IsLongTime",SqlDbType.Int),
					new SqlParameter("@PointsMultiple",SqlDbType.Decimal),
					new SqlParameter("@SendCouponQty",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@IsAllVipCardType",SqlDbType.Int),
					new SqlParameter("@IsVipGrouping",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ActivityID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.ActivityType;
			parameters[1].Value = pEntity.ActivityName;
			parameters[2].Value = pEntity.StartTime;
			parameters[3].Value = pEntity.EndTime;
			parameters[4].Value = pEntity.IsLongTime;
			parameters[5].Value = pEntity.PointsMultiple;
			parameters[6].Value = pEntity.SendCouponQty;
			parameters[7].Value = pEntity.Status;
			parameters[8].Value = pEntity.IsAllVipCardType;
			parameters[9].Value = pEntity.IsVipGrouping;
			parameters[10].Value = pEntity.CustomerID;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.ActivityID;

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
        public void Update(C_ActivityEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(C_ActivityEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(C_ActivityEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ActivityID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ActivityID.Value, pTran);           
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
            sql.AppendLine("update [C_Activity] set  isdelete=1 where ActivityID=@ActivityID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ActivityID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(C_ActivityEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ActivityID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ActivityID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(C_ActivityEntity[] pEntities)
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
            sql.AppendLine("update [C_Activity] set  isdelete=1 where ActivityID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public C_ActivityEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [C_Activity] where 1=1  and isdelete=0 ");
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
            List<C_ActivityEntity> list = new List<C_ActivityEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    C_ActivityEntity m;
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
        public PagedQueryResult<C_ActivityEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ActivityID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [C_Activity] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [C_Activity] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<C_ActivityEntity> result = new PagedQueryResult<C_ActivityEntity>();
            List<C_ActivityEntity> list = new List<C_ActivityEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    C_ActivityEntity m;
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
        public C_ActivityEntity[] QueryByEntity(C_ActivityEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<C_ActivityEntity> PagedQueryByEntity(C_ActivityEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(C_ActivityEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ActivityID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityID", Value = pQueryEntity.ActivityID });
            if (pQueryEntity.ActivityType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityType", Value = pQueryEntity.ActivityType });
            if (pQueryEntity.ActivityName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityName", Value = pQueryEntity.ActivityName });
            if (pQueryEntity.StartTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartTime", Value = pQueryEntity.StartTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.IsLongTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsLongTime", Value = pQueryEntity.IsLongTime });
            if (pQueryEntity.PointsMultiple!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PointsMultiple", Value = pQueryEntity.PointsMultiple });
            if (pQueryEntity.SendCouponQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendCouponQty", Value = pQueryEntity.SendCouponQty });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.IsAllVipCardType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAllVipCardType", Value = pQueryEntity.IsAllVipCardType });
            if (pQueryEntity.IsVipGrouping!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsVipGrouping", Value = pQueryEntity.IsVipGrouping });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out C_ActivityEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new C_ActivityEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ActivityID"] != DBNull.Value)
			{
				pInstance.ActivityID =  (Guid)pReader["ActivityID"];
			}
			if (pReader["ActivityType"] != DBNull.Value)
			{
				pInstance.ActivityType =   Convert.ToInt32(pReader["ActivityType"]);
			}
			if (pReader["ActivityName"] != DBNull.Value)
			{
				pInstance.ActivityName =  Convert.ToString(pReader["ActivityName"]);
			}
			if (pReader["StartTime"] != DBNull.Value)
			{
				pInstance.StartTime =  Convert.ToDateTime(pReader["StartTime"]);
			}
			if (pReader["EndTime"] != DBNull.Value)
			{
				pInstance.EndTime =  Convert.ToDateTime(pReader["EndTime"]);
			}
			if (pReader["IsLongTime"] != DBNull.Value)
			{
				pInstance.IsLongTime =   Convert.ToInt32(pReader["IsLongTime"]);
			}
			if (pReader["PointsMultiple"] != DBNull.Value)
			{
				pInstance.PointsMultiple =  Convert.ToDecimal(pReader["PointsMultiple"]);
			}
			if (pReader["SendCouponQty"] != DBNull.Value)
			{
				pInstance.SendCouponQty =   Convert.ToInt32(pReader["SendCouponQty"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["IsAllVipCardType"] != DBNull.Value)
			{
				pInstance.IsAllVipCardType =   Convert.ToInt32(pReader["IsAllVipCardType"]);
			}
			if (pReader["IsVipGrouping"] != DBNull.Value)
			{
				pInstance.IsVipGrouping =   Convert.ToInt32(pReader["IsVipGrouping"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
