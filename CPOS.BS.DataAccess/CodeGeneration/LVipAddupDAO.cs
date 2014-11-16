/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 14:59:47
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表LVipAddup的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LVipAddupDAO : Base.BaseCPOSDAO, ICRUDable<LVipAddupEntity>, IQueryable<LVipAddupEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LVipAddupDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(LVipAddupEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(LVipAddupEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [LVipAddup](");
            strSql.Append("[YearMonth],[VipAddupCount],[VipMonthCount],[VipMonthMoM],[VipVisitantCount],[VipVisitantMonthCount],[VipVisitantMonthMoM],[VipWeiXinAddupCount],[VipWeiXinMonthCount],[VipWeiXinMonthMoM],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[AddupId])");
            strSql.Append(" values (");
            strSql.Append("@YearMonth,@VipAddupCount,@VipMonthCount,@VipMonthMoM,@VipVisitantCount,@VipVisitantMonthCount,@VipVisitantMonthMoM,@VipWeiXinAddupCount,@VipWeiXinMonthCount,@VipWeiXinMonthMoM,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@AddupId)");            

			string pkString = pEntity.AddupId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@YearMonth",SqlDbType.NVarChar),
					new SqlParameter("@VipAddupCount",SqlDbType.Int),
					new SqlParameter("@VipMonthCount",SqlDbType.Int),
					new SqlParameter("@VipMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipVisitantCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipWeiXinAddupCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@AddupId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.YearMonth;
			parameters[1].Value = pEntity.VipAddupCount;
			parameters[2].Value = pEntity.VipMonthCount;
			parameters[3].Value = pEntity.VipMonthMoM;
			parameters[4].Value = pEntity.VipVisitantCount;
			parameters[5].Value = pEntity.VipVisitantMonthCount;
			parameters[6].Value = pEntity.VipVisitantMonthMoM;
			parameters[7].Value = pEntity.VipWeiXinAddupCount;
			parameters[8].Value = pEntity.VipWeiXinMonthCount;
			parameters[9].Value = pEntity.VipWeiXinMonthMoM;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.AddupId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public LVipAddupEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LVipAddup] where AddupId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            LVipAddupEntity m = null;
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
        public LVipAddupEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LVipAddup] where isdelete=0");
            //读取数据
            List<LVipAddupEntity> list = new List<LVipAddupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LVipAddupEntity m;
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
        public void Update(LVipAddupEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LVipAddupEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AddupId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LVipAddup] set ");
            if (pIsUpdateNullField || pEntity.YearMonth!=null)
                strSql.Append( "[YearMonth]=@YearMonth,");
            if (pIsUpdateNullField || pEntity.VipAddupCount!=null)
                strSql.Append( "[VipAddupCount]=@VipAddupCount,");
            if (pIsUpdateNullField || pEntity.VipMonthCount!=null)
                strSql.Append( "[VipMonthCount]=@VipMonthCount,");
            if (pIsUpdateNullField || pEntity.VipMonthMoM!=null)
                strSql.Append( "[VipMonthMoM]=@VipMonthMoM,");
            if (pIsUpdateNullField || pEntity.VipVisitantCount!=null)
                strSql.Append( "[VipVisitantCount]=@VipVisitantCount,");
            if (pIsUpdateNullField || pEntity.VipVisitantMonthCount!=null)
                strSql.Append( "[VipVisitantMonthCount]=@VipVisitantMonthCount,");
            if (pIsUpdateNullField || pEntity.VipVisitantMonthMoM!=null)
                strSql.Append( "[VipVisitantMonthMoM]=@VipVisitantMonthMoM,");
            if (pIsUpdateNullField || pEntity.VipWeiXinAddupCount!=null)
                strSql.Append( "[VipWeiXinAddupCount]=@VipWeiXinAddupCount,");
            if (pIsUpdateNullField || pEntity.VipWeiXinMonthCount!=null)
                strSql.Append( "[VipWeiXinMonthCount]=@VipWeiXinMonthCount,");
            if (pIsUpdateNullField || pEntity.VipWeiXinMonthMoM!=null)
                strSql.Append( "[VipWeiXinMonthMoM]=@VipWeiXinMonthMoM,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where AddupId=@AddupId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@YearMonth",SqlDbType.NVarChar),
					new SqlParameter("@VipAddupCount",SqlDbType.Int),
					new SqlParameter("@VipMonthCount",SqlDbType.Int),
					new SqlParameter("@VipMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipVisitantCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipWeiXinAddupCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@AddupId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.YearMonth;
			parameters[1].Value = pEntity.VipAddupCount;
			parameters[2].Value = pEntity.VipMonthCount;
			parameters[3].Value = pEntity.VipMonthMoM;
			parameters[4].Value = pEntity.VipVisitantCount;
			parameters[5].Value = pEntity.VipVisitantMonthCount;
			parameters[6].Value = pEntity.VipVisitantMonthMoM;
			parameters[7].Value = pEntity.VipWeiXinAddupCount;
			parameters[8].Value = pEntity.VipWeiXinMonthCount;
			parameters[9].Value = pEntity.VipWeiXinMonthMoM;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.AddupId;

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
        public void Update(LVipAddupEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LVipAddupEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LVipAddupEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(LVipAddupEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AddupId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.AddupId, pTran);           
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
            sql.AppendLine("update [LVipAddup] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where AddupId=@AddupId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@AddupId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LVipAddupEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.AddupId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.AddupId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(LVipAddupEntity[] pEntities)
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
            sql.AppendLine("update [LVipAddup] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where AddupId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LVipAddupEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LVipAddup] where isdelete=0 ");
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
            List<LVipAddupEntity> list = new List<LVipAddupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LVipAddupEntity m;
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
        public PagedQueryResult<LVipAddupEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [AddupId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [LVipAddup] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [LVipAddup] where isdelete=0 ");
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
            PagedQueryResult<LVipAddupEntity> result = new PagedQueryResult<LVipAddupEntity>();
            List<LVipAddupEntity> list = new List<LVipAddupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LVipAddupEntity m;
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
        public LVipAddupEntity[] QueryByEntity(LVipAddupEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LVipAddupEntity> PagedQueryByEntity(LVipAddupEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LVipAddupEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.AddupId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AddupId", Value = pQueryEntity.AddupId });
            if (pQueryEntity.YearMonth!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "YearMonth", Value = pQueryEntity.YearMonth });
            if (pQueryEntity.VipAddupCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipAddupCount", Value = pQueryEntity.VipAddupCount });
            if (pQueryEntity.VipMonthCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipMonthCount", Value = pQueryEntity.VipMonthCount });
            if (pQueryEntity.VipMonthMoM!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipMonthMoM", Value = pQueryEntity.VipMonthMoM });
            if (pQueryEntity.VipVisitantCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipVisitantCount", Value = pQueryEntity.VipVisitantCount });
            if (pQueryEntity.VipVisitantMonthCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipVisitantMonthCount", Value = pQueryEntity.VipVisitantMonthCount });
            if (pQueryEntity.VipVisitantMonthMoM!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipVisitantMonthMoM", Value = pQueryEntity.VipVisitantMonthMoM });
            if (pQueryEntity.VipWeiXinAddupCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipWeiXinAddupCount", Value = pQueryEntity.VipWeiXinAddupCount });
            if (pQueryEntity.VipWeiXinMonthCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipWeiXinMonthCount", Value = pQueryEntity.VipWeiXinMonthCount });
            if (pQueryEntity.VipWeiXinMonthMoM!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipWeiXinMonthMoM", Value = pQueryEntity.VipWeiXinMonthMoM });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
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
        protected void Load(SqlDataReader pReader, out LVipAddupEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new LVipAddupEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["AddupId"] != DBNull.Value)
			{
				pInstance.AddupId =  Convert.ToString(pReader["AddupId"]);
			}
			if (pReader["YearMonth"] != DBNull.Value)
			{
				pInstance.YearMonth =  Convert.ToString(pReader["YearMonth"]);
			}
			if (pReader["VipAddupCount"] != DBNull.Value)
			{
				pInstance.VipAddupCount =   Convert.ToInt32(pReader["VipAddupCount"]);
			}
			if (pReader["VipMonthCount"] != DBNull.Value)
			{
				pInstance.VipMonthCount =   Convert.ToInt32(pReader["VipMonthCount"]);
			}
			if (pReader["VipMonthMoM"] != DBNull.Value)
			{
				pInstance.VipMonthMoM =  Convert.ToDecimal(pReader["VipMonthMoM"]);
			}
			if (pReader["VipVisitantCount"] != DBNull.Value)
			{
				pInstance.VipVisitantCount =   Convert.ToInt32(pReader["VipVisitantCount"]);
			}
			if (pReader["VipVisitantMonthCount"] != DBNull.Value)
			{
				pInstance.VipVisitantMonthCount =   Convert.ToInt32(pReader["VipVisitantMonthCount"]);
			}
			if (pReader["VipVisitantMonthMoM"] != DBNull.Value)
			{
				pInstance.VipVisitantMonthMoM =  Convert.ToDecimal(pReader["VipVisitantMonthMoM"]);
			}
			if (pReader["VipWeiXinAddupCount"] != DBNull.Value)
			{
				pInstance.VipWeiXinAddupCount =   Convert.ToInt32(pReader["VipWeiXinAddupCount"]);
			}
			if (pReader["VipWeiXinMonthCount"] != DBNull.Value)
			{
				pInstance.VipWeiXinMonthCount =   Convert.ToInt32(pReader["VipWeiXinMonthCount"]);
			}
			if (pReader["VipWeiXinMonthMoM"] != DBNull.Value)
			{
				pInstance.VipWeiXinMonthMoM =  Convert.ToDecimal(pReader["VipWeiXinMonthMoM"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
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
