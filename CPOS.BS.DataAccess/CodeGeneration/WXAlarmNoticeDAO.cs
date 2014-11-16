/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/8 15:53:25
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
    /// 表WXAlarmNotice的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXAlarmNoticeDAO : BaseCPOSDAO, ICRUDable<WXAlarmNoticeEntity>, IQueryable<WXAlarmNoticeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXAlarmNoticeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WXAlarmNoticeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WXAlarmNoticeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXAlarmNotice](");
            strSql.Append("[AlarmNoticeCode],[AlarmNoticeRemark],[AlarmNoticeDesc],[AlarmNoticeStatus],[Priority],[RequestBy],[FactBy],[ProposalPlan],[FactPlan],[RequestTime],[FactTime],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[AlarmNoticeId])");
            strSql.Append(" values (");
            strSql.Append("@AlarmNoticeCode,@AlarmNoticeRemark,@AlarmNoticeDesc,@AlarmNoticeStatus,@Priority,@RequestBy,@FactBy,@ProposalPlan,@FactPlan,@RequestTime,@FactTime,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@AlarmNoticeId)");            

			Guid? pkGuid;
			if (pEntity.AlarmNoticeId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.AlarmNoticeId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@AlarmNoticeCode",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeRemark",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeDesc",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeStatus",SqlDbType.Int),
					new SqlParameter("@Priority",SqlDbType.Int),
					new SqlParameter("@RequestBy",SqlDbType.NVarChar),
					new SqlParameter("@FactBy",SqlDbType.NVarChar),
					new SqlParameter("@ProposalPlan",SqlDbType.NVarChar),
					new SqlParameter("@FactPlan",SqlDbType.NVarChar),
					new SqlParameter("@RequestTime",SqlDbType.DateTime),
					new SqlParameter("@FactTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.AlarmNoticeCode;
			parameters[1].Value = pEntity.AlarmNoticeRemark;
			parameters[2].Value = pEntity.AlarmNoticeDesc;
			parameters[3].Value = pEntity.AlarmNoticeStatus;
			parameters[4].Value = pEntity.Priority;
			parameters[5].Value = pEntity.RequestBy;
			parameters[6].Value = pEntity.FactBy;
			parameters[7].Value = pEntity.ProposalPlan;
			parameters[8].Value = pEntity.FactPlan;
			parameters[9].Value = pEntity.RequestTime;
			parameters[10].Value = pEntity.FactTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.AlarmNoticeId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WXAlarmNoticeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXAlarmNotice] where AlarmNoticeId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            WXAlarmNoticeEntity m = null;
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
        public WXAlarmNoticeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXAlarmNotice] where 1=1  and isdelete=0");
            //读取数据
            List<WXAlarmNoticeEntity> list = new List<WXAlarmNoticeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXAlarmNoticeEntity m;
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
        public void Update(WXAlarmNoticeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(WXAlarmNoticeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.AlarmNoticeId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXAlarmNotice] set ");
                        if (pIsUpdateNullField || pEntity.AlarmNoticeCode!=null)
                strSql.Append( "[AlarmNoticeCode]=@AlarmNoticeCode,");
            if (pIsUpdateNullField || pEntity.AlarmNoticeRemark!=null)
                strSql.Append( "[AlarmNoticeRemark]=@AlarmNoticeRemark,");
            if (pIsUpdateNullField || pEntity.AlarmNoticeDesc!=null)
                strSql.Append( "[AlarmNoticeDesc]=@AlarmNoticeDesc,");
            if (pIsUpdateNullField || pEntity.AlarmNoticeStatus!=null)
                strSql.Append( "[AlarmNoticeStatus]=@AlarmNoticeStatus,");
            if (pIsUpdateNullField || pEntity.Priority!=null)
                strSql.Append( "[Priority]=@Priority,");
            if (pIsUpdateNullField || pEntity.RequestBy!=null)
                strSql.Append( "[RequestBy]=@RequestBy,");
            if (pIsUpdateNullField || pEntity.FactBy!=null)
                strSql.Append( "[FactBy]=@FactBy,");
            if (pIsUpdateNullField || pEntity.ProposalPlan!=null)
                strSql.Append( "[ProposalPlan]=@ProposalPlan,");
            if (pIsUpdateNullField || pEntity.FactPlan!=null)
                strSql.Append( "[FactPlan]=@FactPlan,");
            if (pIsUpdateNullField || pEntity.RequestTime!=null)
                strSql.Append( "[RequestTime]=@RequestTime,");
            if (pIsUpdateNullField || pEntity.FactTime!=null)
                strSql.Append( "[FactTime]=@FactTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where AlarmNoticeId=@AlarmNoticeId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AlarmNoticeCode",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeRemark",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeDesc",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeStatus",SqlDbType.Int),
					new SqlParameter("@Priority",SqlDbType.Int),
					new SqlParameter("@RequestBy",SqlDbType.NVarChar),
					new SqlParameter("@FactBy",SqlDbType.NVarChar),
					new SqlParameter("@ProposalPlan",SqlDbType.NVarChar),
					new SqlParameter("@FactPlan",SqlDbType.NVarChar),
					new SqlParameter("@RequestTime",SqlDbType.DateTime),
					new SqlParameter("@FactTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@AlarmNoticeId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.AlarmNoticeCode;
			parameters[1].Value = pEntity.AlarmNoticeRemark;
			parameters[2].Value = pEntity.AlarmNoticeDesc;
			parameters[3].Value = pEntity.AlarmNoticeStatus;
			parameters[4].Value = pEntity.Priority;
			parameters[5].Value = pEntity.RequestBy;
			parameters[6].Value = pEntity.FactBy;
			parameters[7].Value = pEntity.ProposalPlan;
			parameters[8].Value = pEntity.FactPlan;
			parameters[9].Value = pEntity.RequestTime;
			parameters[10].Value = pEntity.FactTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.AlarmNoticeId;

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
        public void Update(WXAlarmNoticeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXAlarmNoticeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WXAlarmNoticeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.AlarmNoticeId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.AlarmNoticeId.Value, pTran);           
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
            sql.AppendLine("update [WXAlarmNotice] set  isdelete=1 where AlarmNoticeId=@AlarmNoticeId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@AlarmNoticeId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXAlarmNoticeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.AlarmNoticeId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.AlarmNoticeId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WXAlarmNoticeEntity[] pEntities)
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
            sql.AppendLine("update [WXAlarmNotice] set  isdelete=1 where AlarmNoticeId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXAlarmNoticeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXAlarmNotice] where 1=1  and isdelete=0 ");
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
            List<WXAlarmNoticeEntity> list = new List<WXAlarmNoticeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXAlarmNoticeEntity m;
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
        public PagedQueryResult<WXAlarmNoticeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [AlarmNoticeId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXAlarmNotice] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WXAlarmNotice] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<WXAlarmNoticeEntity> result = new PagedQueryResult<WXAlarmNoticeEntity>();
            List<WXAlarmNoticeEntity> list = new List<WXAlarmNoticeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXAlarmNoticeEntity m;
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
        public WXAlarmNoticeEntity[] QueryByEntity(WXAlarmNoticeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXAlarmNoticeEntity> PagedQueryByEntity(WXAlarmNoticeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXAlarmNoticeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.AlarmNoticeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AlarmNoticeId", Value = pQueryEntity.AlarmNoticeId });
            if (pQueryEntity.AlarmNoticeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AlarmNoticeCode", Value = pQueryEntity.AlarmNoticeCode });
            if (pQueryEntity.AlarmNoticeRemark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AlarmNoticeRemark", Value = pQueryEntity.AlarmNoticeRemark });
            if (pQueryEntity.AlarmNoticeDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AlarmNoticeDesc", Value = pQueryEntity.AlarmNoticeDesc });
            if (pQueryEntity.AlarmNoticeStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AlarmNoticeStatus", Value = pQueryEntity.AlarmNoticeStatus });
            if (pQueryEntity.Priority!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Priority", Value = pQueryEntity.Priority });
            if (pQueryEntity.RequestBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RequestBy", Value = pQueryEntity.RequestBy });
            if (pQueryEntity.FactBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FactBy", Value = pQueryEntity.FactBy });
            if (pQueryEntity.ProposalPlan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProposalPlan", Value = pQueryEntity.ProposalPlan });
            if (pQueryEntity.FactPlan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FactPlan", Value = pQueryEntity.FactPlan });
            if (pQueryEntity.RequestTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RequestTime", Value = pQueryEntity.RequestTime });
            if (pQueryEntity.FactTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FactTime", Value = pQueryEntity.FactTime });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out WXAlarmNoticeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WXAlarmNoticeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["AlarmNoticeId"] != DBNull.Value)
			{
				pInstance.AlarmNoticeId =  (Guid)pReader["AlarmNoticeId"];
			}
			if (pReader["AlarmNoticeCode"] != DBNull.Value)
			{
				pInstance.AlarmNoticeCode =  Convert.ToString(pReader["AlarmNoticeCode"]);
			}
			if (pReader["AlarmNoticeRemark"] != DBNull.Value)
			{
				pInstance.AlarmNoticeRemark =  Convert.ToString(pReader["AlarmNoticeRemark"]);
			}
			if (pReader["AlarmNoticeDesc"] != DBNull.Value)
			{
				pInstance.AlarmNoticeDesc =  Convert.ToString(pReader["AlarmNoticeDesc"]);
			}
			if (pReader["AlarmNoticeStatus"] != DBNull.Value)
			{
				pInstance.AlarmNoticeStatus =   Convert.ToInt32(pReader["AlarmNoticeStatus"]);
			}
			if (pReader["Priority"] != DBNull.Value)
			{
				pInstance.Priority =   Convert.ToInt32(pReader["Priority"]);
			}
			if (pReader["RequestBy"] != DBNull.Value)
			{
				pInstance.RequestBy =  Convert.ToString(pReader["RequestBy"]);
			}
			if (pReader["FactBy"] != DBNull.Value)
			{
				pInstance.FactBy =  Convert.ToString(pReader["FactBy"]);
			}
			if (pReader["ProposalPlan"] != DBNull.Value)
			{
				pInstance.ProposalPlan =  Convert.ToString(pReader["ProposalPlan"]);
			}
			if (pReader["FactPlan"] != DBNull.Value)
			{
				pInstance.FactPlan =  Convert.ToString(pReader["FactPlan"]);
			}
			if (pReader["RequestTime"] != DBNull.Value)
			{
				pInstance.RequestTime =  Convert.ToDateTime(pReader["RequestTime"]);
			}
			if (pReader["FactTime"] != DBNull.Value)
			{
				pInstance.FactTime =  Convert.ToDateTime(pReader["FactTime"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
