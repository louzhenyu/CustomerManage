/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/15 19:16:26
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
    /// 表T_QN_QuestionnaireAnswerRecord的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_QN_QuestionnaireAnswerRecordDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionnaireAnswerRecordEntity>, IQueryable<T_QN_QuestionnaireAnswerRecordEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_QN_QuestionnaireAnswerRecordDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_QN_QuestionnaireAnswerRecordEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_QN_QuestionnaireAnswerRecordEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_QN_QuestionnaireAnswerRecord](");
            strSql.Append("[VipID],[QuestionnaireID],[QuestionnaireName],[QuestionID],[QuestionName],[ActivityID],[ActivityName],[QuestionidType],[AnswerText],[AnswerOptionId],[AnswerOption],[AnswerDate],[AnswerProvince],[AnswerCity],[AnswerCounty],[AnswerAddress],[QuestionScore],[CustomerID],[Status],[CreateTime],[QuestionnaireAnswerRecordID])");
            strSql.Append(" values (");
            strSql.Append("@VipID,@QuestionnaireID,@QuestionnaireName,@QuestionID,@QuestionName,@ActivityID,@ActivityName,@QuestionidType,@AnswerText,@AnswerOptionId,@AnswerOption,@AnswerDate,@AnswerProvince,@AnswerCity,@AnswerCounty,@AnswerAddress,@QuestionScore,@CustomerID,@Status,@CreateTime,@QuestionnaireAnswerRecordID)");            

			Guid? pkGuid;
			if (pEntity.QuestionnaireAnswerRecordID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.QuestionnaireAnswerRecordID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@QuestionnaireID",SqlDbType.VarChar),
					new SqlParameter("@QuestionnaireName",SqlDbType.NVarChar),
					new SqlParameter("@QuestionID",SqlDbType.VarChar),
					new SqlParameter("@QuestionName",SqlDbType.NVarChar),
					new SqlParameter("@ActivityID",SqlDbType.VarChar),
					new SqlParameter("@ActivityName",SqlDbType.NVarChar),
					new SqlParameter("@QuestionidType",SqlDbType.Int),
					new SqlParameter("@AnswerText",SqlDbType.NVarChar),
					new SqlParameter("@AnswerOptionId",SqlDbType.VarChar),
					new SqlParameter("@AnswerOption",SqlDbType.NVarChar),
					new SqlParameter("@AnswerDate",SqlDbType.NVarChar),
					new SqlParameter("@AnswerProvince",SqlDbType.NVarChar),
					new SqlParameter("@AnswerCity",SqlDbType.NVarChar),
					new SqlParameter("@AnswerCounty",SqlDbType.NVarChar),
					new SqlParameter("@AnswerAddress",SqlDbType.NVarChar),
					new SqlParameter("@QuestionScore",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@QuestionnaireAnswerRecordID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipID;
			parameters[1].Value = pEntity.QuestionnaireID;
			parameters[2].Value = pEntity.QuestionnaireName;
			parameters[3].Value = pEntity.QuestionID;
			parameters[4].Value = pEntity.QuestionName;
			parameters[5].Value = pEntity.ActivityID;
			parameters[6].Value = pEntity.ActivityName;
			parameters[7].Value = pEntity.QuestionidType;
			parameters[8].Value = pEntity.AnswerText;
			parameters[9].Value = pEntity.AnswerOptionId;
			parameters[10].Value = pEntity.AnswerOption;
			parameters[11].Value = pEntity.AnswerDate;
			parameters[12].Value = pEntity.AnswerProvince;
			parameters[13].Value = pEntity.AnswerCity;
			parameters[14].Value = pEntity.AnswerCounty;
			parameters[15].Value = pEntity.AnswerAddress;
			parameters[16].Value = pEntity.QuestionScore;
			parameters[17].Value = pEntity.CustomerID;
			parameters[18].Value = pEntity.Status;
			parameters[19].Value = pEntity.CreateTime;
			parameters[20].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.QuestionnaireAnswerRecordID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_QN_QuestionnaireAnswerRecordEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_QuestionnaireAnswerRecord] where QuestionnaireAnswerRecordID='{0}'  and status<>'-1' ", id.ToString());
            //读取数据
            T_QN_QuestionnaireAnswerRecordEntity m = null;
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
        public T_QN_QuestionnaireAnswerRecordEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_QuestionnaireAnswerRecord] where 1=1  and status<>'-1'");
            //读取数据
            List<T_QN_QuestionnaireAnswerRecordEntity> list = new List<T_QN_QuestionnaireAnswerRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireAnswerRecordEntity m;
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
        public void Update(T_QN_QuestionnaireAnswerRecordEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_QN_QuestionnaireAnswerRecordEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.QuestionnaireAnswerRecordID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_QN_QuestionnaireAnswerRecord] set ");
                        if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
            if (pIsUpdateNullField || pEntity.QuestionnaireID!=null)
                strSql.Append( "[QuestionnaireID]=@QuestionnaireID,");
            if (pIsUpdateNullField || pEntity.QuestionnaireName!=null)
                strSql.Append( "[QuestionnaireName]=@QuestionnaireName,");
            if (pIsUpdateNullField || pEntity.QuestionID!=null)
                strSql.Append( "[QuestionID]=@QuestionID,");
            if (pIsUpdateNullField || pEntity.QuestionName!=null)
                strSql.Append( "[QuestionName]=@QuestionName,");
            if (pIsUpdateNullField || pEntity.ActivityID!=null)
                strSql.Append( "[ActivityID]=@ActivityID,");
            if (pIsUpdateNullField || pEntity.ActivityName!=null)
                strSql.Append( "[ActivityName]=@ActivityName,");
            if (pIsUpdateNullField || pEntity.QuestionidType!=null)
                strSql.Append( "[QuestionidType]=@QuestionidType,");
            if (pIsUpdateNullField || pEntity.AnswerText!=null)
                strSql.Append( "[AnswerText]=@AnswerText,");
            if (pIsUpdateNullField || pEntity.AnswerOptionId!=null)
                strSql.Append( "[AnswerOptionId]=@AnswerOptionId,");
            if (pIsUpdateNullField || pEntity.AnswerOption!=null)
                strSql.Append( "[AnswerOption]=@AnswerOption,");
            if (pIsUpdateNullField || pEntity.AnswerDate!=null)
                strSql.Append( "[AnswerDate]=@AnswerDate,");
            if (pIsUpdateNullField || pEntity.AnswerProvince!=null)
                strSql.Append( "[AnswerProvince]=@AnswerProvince,");
            if (pIsUpdateNullField || pEntity.AnswerCity!=null)
                strSql.Append( "[AnswerCity]=@AnswerCity,");
            if (pIsUpdateNullField || pEntity.AnswerCounty!=null)
                strSql.Append( "[AnswerCounty]=@AnswerCounty,");
            if (pIsUpdateNullField || pEntity.AnswerAddress!=null)
                strSql.Append( "[AnswerAddress]=@AnswerAddress,");
            if (pIsUpdateNullField || pEntity.QuestionScore!=null)
                strSql.Append( "[QuestionScore]=@QuestionScore,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status");
            strSql.Append(" where QuestionnaireAnswerRecordID=@QuestionnaireAnswerRecordID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@QuestionnaireID",SqlDbType.VarChar),
					new SqlParameter("@QuestionnaireName",SqlDbType.NVarChar),
					new SqlParameter("@QuestionID",SqlDbType.VarChar),
					new SqlParameter("@QuestionName",SqlDbType.NVarChar),
					new SqlParameter("@ActivityID",SqlDbType.VarChar),
					new SqlParameter("@ActivityName",SqlDbType.NVarChar),
					new SqlParameter("@QuestionidType",SqlDbType.Int),
					new SqlParameter("@AnswerText",SqlDbType.NVarChar),
					new SqlParameter("@AnswerOptionId",SqlDbType.VarChar),
					new SqlParameter("@AnswerOption",SqlDbType.NVarChar),
					new SqlParameter("@AnswerDate",SqlDbType.NVarChar),
					new SqlParameter("@AnswerProvince",SqlDbType.NVarChar),
					new SqlParameter("@AnswerCity",SqlDbType.NVarChar),
					new SqlParameter("@AnswerCounty",SqlDbType.NVarChar),
					new SqlParameter("@AnswerAddress",SqlDbType.NVarChar),
					new SqlParameter("@QuestionScore",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@QuestionnaireAnswerRecordID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipID;
			parameters[1].Value = pEntity.QuestionnaireID;
			parameters[2].Value = pEntity.QuestionnaireName;
			parameters[3].Value = pEntity.QuestionID;
			parameters[4].Value = pEntity.QuestionName;
			parameters[5].Value = pEntity.ActivityID;
			parameters[6].Value = pEntity.ActivityName;
			parameters[7].Value = pEntity.QuestionidType;
			parameters[8].Value = pEntity.AnswerText;
			parameters[9].Value = pEntity.AnswerOptionId;
			parameters[10].Value = pEntity.AnswerOption;
			parameters[11].Value = pEntity.AnswerDate;
			parameters[12].Value = pEntity.AnswerProvince;
			parameters[13].Value = pEntity.AnswerCity;
			parameters[14].Value = pEntity.AnswerCounty;
			parameters[15].Value = pEntity.AnswerAddress;
			parameters[16].Value = pEntity.QuestionScore;
			parameters[17].Value = pEntity.CustomerID;
			parameters[18].Value = pEntity.Status;
			parameters[19].Value = pEntity.QuestionnaireAnswerRecordID;

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
        public void Update(T_QN_QuestionnaireAnswerRecordEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_QN_QuestionnaireAnswerRecordEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_QN_QuestionnaireAnswerRecordEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.QuestionnaireAnswerRecordID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.QuestionnaireAnswerRecordID.Value, pTran);           
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
            sql.AppendLine("update [T_QN_QuestionnaireAnswerRecord] set status='-1' where QuestionnaireAnswerRecordID=@QuestionnaireAnswerRecordID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@QuestionnaireAnswerRecordID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_QN_QuestionnaireAnswerRecordEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.QuestionnaireAnswerRecordID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.QuestionnaireAnswerRecordID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_QN_QuestionnaireAnswerRecordEntity[] pEntities)
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
            sql.AppendLine("update [T_QN_QuestionnaireAnswerRecord] set status='-1' where QuestionnaireAnswerRecordID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_QN_QuestionnaireAnswerRecordEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_QuestionnaireAnswerRecord] where 1=1  and status<>'-1' ");
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
            List<T_QN_QuestionnaireAnswerRecordEntity> list = new List<T_QN_QuestionnaireAnswerRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireAnswerRecordEntity m;
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
        public PagedQueryResult<T_QN_QuestionnaireAnswerRecordEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [QuestionnaireAnswerRecordID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_QN_QuestionnaireAnswerRecord] where 1=1  and status<>'-1' ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_QN_QuestionnaireAnswerRecord] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<T_QN_QuestionnaireAnswerRecordEntity> result = new PagedQueryResult<T_QN_QuestionnaireAnswerRecordEntity>();
            List<T_QN_QuestionnaireAnswerRecordEntity> list = new List<T_QN_QuestionnaireAnswerRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireAnswerRecordEntity m;
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
        public T_QN_QuestionnaireAnswerRecordEntity[] QueryByEntity(T_QN_QuestionnaireAnswerRecordEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_QN_QuestionnaireAnswerRecordEntity> PagedQueryByEntity(T_QN_QuestionnaireAnswerRecordEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_QN_QuestionnaireAnswerRecordEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.QuestionnaireAnswerRecordID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireAnswerRecordID", Value = pQueryEntity.QuestionnaireAnswerRecordID });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.QuestionnaireID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireID", Value = pQueryEntity.QuestionnaireID });
            if (pQueryEntity.QuestionnaireName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireName", Value = pQueryEntity.QuestionnaireName });
            if (pQueryEntity.QuestionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionID", Value = pQueryEntity.QuestionID });
            if (pQueryEntity.QuestionName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionName", Value = pQueryEntity.QuestionName });
            if (pQueryEntity.ActivityID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityID", Value = pQueryEntity.ActivityID });
            if (pQueryEntity.ActivityName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityName", Value = pQueryEntity.ActivityName });
            if (pQueryEntity.QuestionidType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionidType", Value = pQueryEntity.QuestionidType });
            if (pQueryEntity.AnswerText!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerText", Value = pQueryEntity.AnswerText });
            if (pQueryEntity.AnswerOptionId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerOptionId", Value = pQueryEntity.AnswerOptionId });
            if (pQueryEntity.AnswerOption!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerOption", Value = pQueryEntity.AnswerOption });
            if (pQueryEntity.AnswerDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerDate", Value = pQueryEntity.AnswerDate });
            if (pQueryEntity.AnswerProvince!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerProvince", Value = pQueryEntity.AnswerProvince });
            if (pQueryEntity.AnswerCity!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerCity", Value = pQueryEntity.AnswerCity });
            if (pQueryEntity.AnswerCounty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerCounty", Value = pQueryEntity.AnswerCounty });
            if (pQueryEntity.AnswerAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerAddress", Value = pQueryEntity.AnswerAddress });
            if (pQueryEntity.QuestionScore!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionScore", Value = pQueryEntity.QuestionScore });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_QN_QuestionnaireAnswerRecordEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_QN_QuestionnaireAnswerRecordEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["QuestionnaireAnswerRecordID"] != DBNull.Value)
			{
				pInstance.QuestionnaireAnswerRecordID =  (Guid)pReader["QuestionnaireAnswerRecordID"];
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
			}
			if (pReader["QuestionnaireID"] != DBNull.Value)
			{
				pInstance.QuestionnaireID =  Convert.ToString(pReader["QuestionnaireID"]);
			}
			if (pReader["QuestionnaireName"] != DBNull.Value)
			{
				pInstance.QuestionnaireName =  Convert.ToString(pReader["QuestionnaireName"]);
			}
			if (pReader["QuestionID"] != DBNull.Value)
			{
				pInstance.QuestionID =  Convert.ToString(pReader["QuestionID"]);
			}
			if (pReader["QuestionName"] != DBNull.Value)
			{
				pInstance.QuestionName =  Convert.ToString(pReader["QuestionName"]);
			}
			if (pReader["ActivityID"] != DBNull.Value)
			{
				pInstance.ActivityID =  Convert.ToString(pReader["ActivityID"]);
			}
			if (pReader["ActivityName"] != DBNull.Value)
			{
				pInstance.ActivityName =  Convert.ToString(pReader["ActivityName"]);
			}
			if (pReader["QuestionidType"] != DBNull.Value)
			{
				pInstance.QuestionidType =   Convert.ToInt32(pReader["QuestionidType"]);
			}
			if (pReader["AnswerText"] != DBNull.Value)
			{
				pInstance.AnswerText =  Convert.ToString(pReader["AnswerText"]);
			}
			if (pReader["AnswerOptionId"] != DBNull.Value)
			{
				pInstance.AnswerOptionId =  Convert.ToString(pReader["AnswerOptionId"]);
			}
			if (pReader["AnswerOption"] != DBNull.Value)
			{
				pInstance.AnswerOption =  Convert.ToString(pReader["AnswerOption"]);
			}
			if (pReader["AnswerDate"] != DBNull.Value)
			{
				pInstance.AnswerDate =  Convert.ToString(pReader["AnswerDate"]);
			}
			if (pReader["AnswerProvince"] != DBNull.Value)
			{
				pInstance.AnswerProvince =  Convert.ToString(pReader["AnswerProvince"]);
			}
			if (pReader["AnswerCity"] != DBNull.Value)
			{
				pInstance.AnswerCity =  Convert.ToString(pReader["AnswerCity"]);
			}
			if (pReader["AnswerCounty"] != DBNull.Value)
			{
				pInstance.AnswerCounty =  Convert.ToString(pReader["AnswerCounty"]);
			}
			if (pReader["AnswerAddress"] != DBNull.Value)
			{
				pInstance.AnswerAddress =  Convert.ToString(pReader["AnswerAddress"]);
			}
			if (pReader["QuestionScore"] != DBNull.Value)
			{
				pInstance.QuestionScore =   Convert.ToInt32(pReader["QuestionScore"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}

        }
        #endregion
    }
}
