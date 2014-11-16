/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:48
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 拜访数据明细(参数定义) 
    /// 表VisitingParameter的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VisitingParameterDAO : BaseCPOSDAO, ICRUDable<VisitingParameterEntity>, IQueryable<VisitingParameterEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingParameterDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VisitingParameterEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VisitingParameterEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VisitingParameter](");
            strSql.Append("[ParameterType],[ParameterName],[ParameterNameEn],[ControlType],[ControlName],[MaxValue],[MinValue],[DefaultValue],[Scale],[Unit],[Weight],[IsMustDo],[IsRemember],[IsVerify],[ClientID],[ClientDistributorID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[VisitingParameterID])");
            strSql.Append(" values (");
            strSql.Append("@ParameterType,@ParameterName,@ParameterNameEn,@ControlType,@ControlName,@MaxValue,@MinValue,@DefaultValue,@Scale,@Unit,@Weight,@IsMustDo,@IsRemember,@IsVerify,@ClientID,@ClientDistributorID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@VisitingParameterID)");            

			Guid? pkGuid;
			if (pEntity.VisitingParameterID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.VisitingParameterID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ParameterType",SqlDbType.Int),
					new SqlParameter("@ParameterName",SqlDbType.NVarChar,100),
					new SqlParameter("@ParameterNameEn",SqlDbType.NVarChar,100),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@ControlName",SqlDbType.NVarChar,100),
					new SqlParameter("@MaxValue",SqlDbType.Decimal),
					new SqlParameter("@MinValue",SqlDbType.Decimal),
					new SqlParameter("@DefaultValue",SqlDbType.NVarChar,100),
					new SqlParameter("@Scale",SqlDbType.Int),
					new SqlParameter("@Unit",SqlDbType.NVarChar,100),
					new SqlParameter("@Weight",SqlDbType.Decimal),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@IsRemember",SqlDbType.Int),
					new SqlParameter("@IsVerify",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VisitingParameterID",SqlDbType.UniqueIdentifier,16)
            };
			parameters[0].Value = pEntity.ParameterType;
			parameters[1].Value = pEntity.ParameterName;
			parameters[2].Value = pEntity.ParameterNameEn;
			parameters[3].Value = pEntity.ControlType;
			parameters[4].Value = pEntity.ControlName;
			parameters[5].Value = pEntity.MaxValue;
			parameters[6].Value = pEntity.MinValue;
			parameters[7].Value = pEntity.DefaultValue;
			parameters[8].Value = pEntity.Scale;
			parameters[9].Value = pEntity.Unit;
			parameters[10].Value = pEntity.Weight;
			parameters[11].Value = pEntity.IsMustDo;
			parameters[12].Value = pEntity.IsRemember;
			parameters[13].Value = pEntity.IsVerify;
			parameters[14].Value = pEntity.ClientID;
			parameters[15].Value = pEntity.ClientDistributorID;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.CreateTime;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.IsDelete;
			parameters[21].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VisitingParameterID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VisitingParameterEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingParameter] where VisitingParameterID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            VisitingParameterEntity m = null;
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
        public VisitingParameterEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingParameter] where isdelete=0");
            //读取数据
            List<VisitingParameterEntity> list = new List<VisitingParameterEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingParameterEntity m;
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
        public void Update(VisitingParameterEntity pEntity , IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingParameterID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VisitingParameter] set ");
            strSql.Append("[ParameterType]=@ParameterType,[ParameterName]=@ParameterName,[ParameterNameEn]=@ParameterNameEn,[ControlType]=@ControlType,[ControlName]=@ControlName,[MaxValue]=@MaxValue,[MinValue]=@MinValue,[DefaultValue]=@DefaultValue,[Scale]=@Scale,[Unit]=@Unit,[Weight]=@Weight,[IsMustDo]=@IsMustDo,[IsRemember]=@IsRemember,[IsVerify]=@IsVerify,[ClientID]=@ClientID,[ClientDistributorID]=@ClientDistributorID,[LastUpdateBy]=@LastUpdateBy,[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where VisitingParameterID=@VisitingParameterID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ParameterType",SqlDbType.Int),
					new SqlParameter("@ParameterName",SqlDbType.NVarChar,100),
					new SqlParameter("@ParameterNameEn",SqlDbType.NVarChar,100),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@ControlName",SqlDbType.NVarChar,100),
					new SqlParameter("@MaxValue",SqlDbType.Decimal),
					new SqlParameter("@MinValue",SqlDbType.Decimal),
					new SqlParameter("@DefaultValue",SqlDbType.NVarChar,100),
					new SqlParameter("@Scale",SqlDbType.Int),
					new SqlParameter("@Unit",SqlDbType.NVarChar,100),
					new SqlParameter("@Weight",SqlDbType.Decimal),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@IsRemember",SqlDbType.Int),
					new SqlParameter("@IsVerify",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@VisitingParameterID",SqlDbType.UniqueIdentifier,16)
            };
			parameters[0].Value = pEntity.ParameterType;
			parameters[1].Value = pEntity.ParameterName;
			parameters[2].Value = pEntity.ParameterNameEn;
			parameters[3].Value = pEntity.ControlType;
			parameters[4].Value = pEntity.ControlName;
			parameters[5].Value = pEntity.MaxValue;
			parameters[6].Value = pEntity.MinValue;
			parameters[7].Value = pEntity.DefaultValue;
			parameters[8].Value = pEntity.Scale;
			parameters[9].Value = pEntity.Unit;
			parameters[10].Value = pEntity.Weight;
			parameters[11].Value = pEntity.IsMustDo;
			parameters[12].Value = pEntity.IsRemember;
			parameters[13].Value = pEntity.IsVerify;
			parameters[14].Value = pEntity.ClientID;
			parameters[15].Value = pEntity.ClientDistributorID;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.VisitingParameterID;

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
        public void Update(VisitingParameterEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VisitingParameterEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VisitingParameterEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingParameterID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VisitingParameterID.Value, pTran);           
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
            sql.AppendLine("update [VisitingParameter] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VisitingParameterID=@VisitingParameterID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=CurrentUserInfo.UserID},
                new SqlParameter{ParameterName="@VisitingParameterID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VisitingParameterEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.VisitingParameterID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.VisitingParameterID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VisitingParameterEntity[] pEntities)
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
            sql.AppendLine("update [VisitingParameter] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where VisitingParameterID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VisitingParameterEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingParameter] where isdelete=0 ");
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
            List<VisitingParameterEntity> list = new List<VisitingParameterEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingParameterEntity m;
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
        public PagedQueryResult<VisitingParameterEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VisitingParameterID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VisitingParameter] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VisitingParameter] where isdelete=0 ");
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
            PagedQueryResult<VisitingParameterEntity> result = new PagedQueryResult<VisitingParameterEntity>();
            List<VisitingParameterEntity> list = new List<VisitingParameterEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingParameterEntity m;
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
        public VisitingParameterEntity[] QueryByEntity(VisitingParameterEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VisitingParameterEntity> PagedQueryByEntity(VisitingParameterEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VisitingParameterEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VisitingParameterID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingParameterID", Value = pQueryEntity.VisitingParameterID });
            if (pQueryEntity.ParameterType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParameterType", Value = pQueryEntity.ParameterType });
            if (pQueryEntity.ParameterName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParameterName", Value = pQueryEntity.ParameterName });
            if (pQueryEntity.ParameterNameEn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParameterNameEn", Value = pQueryEntity.ParameterNameEn });
            if (pQueryEntity.ControlType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ControlType", Value = pQueryEntity.ControlType });
            if (pQueryEntity.ControlName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ControlName", Value = pQueryEntity.ControlName });
            if (pQueryEntity.MaxValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxValue", Value = pQueryEntity.MaxValue });
            if (pQueryEntity.MinValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinValue", Value = pQueryEntity.MinValue });
            if (pQueryEntity.DefaultValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DefaultValue", Value = pQueryEntity.DefaultValue });
            if (pQueryEntity.Scale!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Scale", Value = pQueryEntity.Scale });
            if (pQueryEntity.Unit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Unit", Value = pQueryEntity.Unit });
            if (pQueryEntity.Weight!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Weight", Value = pQueryEntity.Weight });
            if (pQueryEntity.IsMustDo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMustDo", Value = pQueryEntity.IsMustDo });
            if (pQueryEntity.IsRemember!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRemember", Value = pQueryEntity.IsRemember });
            if (pQueryEntity.IsVerify!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsVerify", Value = pQueryEntity.IsVerify });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.ClientDistributorID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientDistributorID", Value = pQueryEntity.ClientDistributorID });
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
        protected void Load(SqlDataReader pReader, out VisitingParameterEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VisitingParameterEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VisitingParameterID"] != DBNull.Value)
			{
				pInstance.VisitingParameterID =  (Guid)pReader["VisitingParameterID"];
			}
			if (pReader["ParameterType"] != DBNull.Value)
			{
				pInstance.ParameterType =   Convert.ToInt32(pReader["ParameterType"]);
			}
			if (pReader["ParameterName"] != DBNull.Value)
			{
				pInstance.ParameterName =  Convert.ToString(pReader["ParameterName"]);
			}
			if (pReader["ParameterNameEn"] != DBNull.Value)
			{
				pInstance.ParameterNameEn =  Convert.ToString(pReader["ParameterNameEn"]);
			}
			if (pReader["ControlType"] != DBNull.Value)
			{
				pInstance.ControlType =   Convert.ToInt32(pReader["ControlType"]);
			}
			if (pReader["ControlName"] != DBNull.Value)
			{
				pInstance.ControlName =  Convert.ToString(pReader["ControlName"]);
			}
			if (pReader["MaxValue"] != DBNull.Value)
			{
				pInstance.MaxValue =  Convert.ToDecimal(pReader["MaxValue"]);
			}
			if (pReader["MinValue"] != DBNull.Value)
			{
				pInstance.MinValue =  Convert.ToDecimal(pReader["MinValue"]);
			}
			if (pReader["DefaultValue"] != DBNull.Value)
			{
				pInstance.DefaultValue =  Convert.ToString(pReader["DefaultValue"]);
			}
			if (pReader["Scale"] != DBNull.Value)
			{
				pInstance.Scale =   Convert.ToInt32(pReader["Scale"]);
			}
			if (pReader["Unit"] != DBNull.Value)
			{
                pInstance.Unit = Convert.ToString(pReader["Unit"]);
			}
			if (pReader["Weight"] != DBNull.Value)
			{
				pInstance.Weight =  Convert.ToDecimal(pReader["Weight"]);
			}
			if (pReader["IsMustDo"] != DBNull.Value)
			{
				pInstance.IsMustDo =   Convert.ToInt32(pReader["IsMustDo"]);
			}
			if (pReader["IsRemember"] != DBNull.Value)
			{
				pInstance.IsRemember =   Convert.ToInt32(pReader["IsRemember"]);
			}
			if (pReader["IsVerify"] != DBNull.Value)
			{
				pInstance.IsVerify =   Convert.ToInt32(pReader["IsVerify"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =   pReader["ClientID"].ToString();
			}
			if (pReader["ClientDistributorID"] != DBNull.Value)
			{
				pInstance.ClientDistributorID =   Convert.ToInt32(pReader["ClientDistributorID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =   pReader["CreateBy"].ToString();
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =   pReader["LastUpdateBy"].ToString();
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
