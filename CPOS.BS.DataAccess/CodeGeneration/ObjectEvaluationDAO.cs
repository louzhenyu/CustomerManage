/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 17:54:09
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
    /// 表ObjectEvaluation的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ObjectEvaluationDAO : Base.BaseCPOSDAO, ICRUDable<ObjectEvaluationEntity>, IQueryable<ObjectEvaluationEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ObjectEvaluationDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ObjectEvaluationEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ObjectEvaluationEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ObjectEvaluation](");
            strSql.Append("[ObjectID],[Type],[VipID],[CustomerID],[Content],[StarLevel],[StarLevel1],[StarLevel2],[StarLevel3],[StarLevel4],[StarLevel5],[Platform],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[OrderID],[Remark],[IsAnonymity],[EvaluationID])");
            strSql.Append(" values (");
            strSql.Append("@ObjectID,@Type,@VipID,@CustomerID,@Content,@StarLevel,@StarLevel1,@StarLevel2,@StarLevel3,@StarLevel4,@StarLevel5,@Platform,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@OrderID,@Remark,@IsAnonymity,@EvaluationID)");            

			string pkString = pEntity.EvaluationID;
            if (string.IsNullOrEmpty(pkString))
                pkString = Guid.NewGuid().ToString();

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ObjectID",SqlDbType.NVarChar),
					new SqlParameter("@Type",SqlDbType.Int),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@StarLevel",SqlDbType.Int),
					new SqlParameter("@StarLevel1",SqlDbType.Int),
					new SqlParameter("@StarLevel2",SqlDbType.Int),
					new SqlParameter("@StarLevel3",SqlDbType.Int),
					new SqlParameter("@StarLevel4",SqlDbType.Int),
					new SqlParameter("@StarLevel5",SqlDbType.Int),
					new SqlParameter("@Platform",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@IsAnonymity",SqlDbType.Int),
					new SqlParameter("@EvaluationID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ObjectID;
			parameters[1].Value = pEntity.Type;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.CustomerID;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.StarLevel;
			parameters[6].Value = pEntity.StarLevel1;
			parameters[7].Value = pEntity.StarLevel2;
			parameters[8].Value = pEntity.StarLevel3;
			parameters[9].Value = pEntity.StarLevel4;
			parameters[10].Value = pEntity.StarLevel5;
			parameters[11].Value = pEntity.Platform;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.CreateBy;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pEntity.OrderID;
			parameters[18].Value = pEntity.Remark;
			parameters[19].Value = pEntity.IsAnonymity;
			parameters[20].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.EvaluationID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ObjectEvaluationEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ObjectEvaluation] where EvaluationID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            ObjectEvaluationEntity m = null;
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
        public ObjectEvaluationEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ObjectEvaluation] where 1=1  and isdelete=0");
            //读取数据
            List<ObjectEvaluationEntity> list = new List<ObjectEvaluationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ObjectEvaluationEntity m;
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
        public void Update(ObjectEvaluationEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(ObjectEvaluationEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EvaluationID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ObjectEvaluation] set ");
                        if (pIsUpdateNullField || pEntity.ObjectID!=null)
                strSql.Append( "[ObjectID]=@ObjectID,");
            if (pIsUpdateNullField || pEntity.Type!=null)
                strSql.Append( "[Type]=@Type,");
            if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.StarLevel!=null)
                strSql.Append( "[StarLevel]=@StarLevel,");
            if (pIsUpdateNullField || pEntity.StarLevel1!=null)
                strSql.Append( "[StarLevel1]=@StarLevel1,");
            if (pIsUpdateNullField || pEntity.StarLevel2!=null)
                strSql.Append( "[StarLevel2]=@StarLevel2,");
            if (pIsUpdateNullField || pEntity.StarLevel3!=null)
                strSql.Append( "[StarLevel3]=@StarLevel3,");
            if (pIsUpdateNullField || pEntity.StarLevel4!=null)
                strSql.Append( "[StarLevel4]=@StarLevel4,");
            if (pIsUpdateNullField || pEntity.StarLevel5!=null)
                strSql.Append( "[StarLevel5]=@StarLevel5,");
            if (pIsUpdateNullField || pEntity.Platform!=null)
                strSql.Append( "[Platform]=@Platform,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.OrderID!=null)
                strSql.Append( "[OrderID]=@OrderID,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.IsAnonymity!=null)
                strSql.Append( "[IsAnonymity]=@IsAnonymity");
            strSql.Append(" where EvaluationID=@EvaluationID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ObjectID",SqlDbType.NVarChar),
					new SqlParameter("@Type",SqlDbType.Int),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@StarLevel",SqlDbType.Int),
					new SqlParameter("@StarLevel1",SqlDbType.Int),
					new SqlParameter("@StarLevel2",SqlDbType.Int),
					new SqlParameter("@StarLevel3",SqlDbType.Int),
					new SqlParameter("@StarLevel4",SqlDbType.Int),
					new SqlParameter("@StarLevel5",SqlDbType.Int),
					new SqlParameter("@Platform",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@IsAnonymity",SqlDbType.Int),
					new SqlParameter("@EvaluationID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ObjectID;
			parameters[1].Value = pEntity.Type;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.CustomerID;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.StarLevel;
			parameters[6].Value = pEntity.StarLevel1;
			parameters[7].Value = pEntity.StarLevel2;
			parameters[8].Value = pEntity.StarLevel3;
			parameters[9].Value = pEntity.StarLevel4;
			parameters[10].Value = pEntity.StarLevel5;
			parameters[11].Value = pEntity.Platform;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.OrderID;
			parameters[15].Value = pEntity.Remark;
			parameters[16].Value = pEntity.IsAnonymity;
			parameters[17].Value = pEntity.EvaluationID;

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
        public void Update(ObjectEvaluationEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ObjectEvaluationEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ObjectEvaluationEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EvaluationID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.EvaluationID, pTran);           
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
            sql.AppendLine("update [ObjectEvaluation] set  isdelete=1 where EvaluationID=@EvaluationID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@EvaluationID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ObjectEvaluationEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.EvaluationID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.EvaluationID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(ObjectEvaluationEntity[] pEntities)
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
            sql.AppendLine("update [ObjectEvaluation] set  isdelete=1 where EvaluationID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ObjectEvaluationEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ObjectEvaluation] where 1=1  and isdelete=0 ");
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
            List<ObjectEvaluationEntity> list = new List<ObjectEvaluationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ObjectEvaluationEntity m;
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
        public PagedQueryResult<ObjectEvaluationEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EvaluationID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [ObjectEvaluation] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [ObjectEvaluation] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<ObjectEvaluationEntity> result = new PagedQueryResult<ObjectEvaluationEntity>();
            List<ObjectEvaluationEntity> list = new List<ObjectEvaluationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ObjectEvaluationEntity m;
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
        public ObjectEvaluationEntity[] QueryByEntity(ObjectEvaluationEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ObjectEvaluationEntity> PagedQueryByEntity(ObjectEvaluationEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ObjectEvaluationEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EvaluationID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EvaluationID", Value = pQueryEntity.EvaluationID });
            if (pQueryEntity.ObjectID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectID", Value = pQueryEntity.ObjectID });
            if (pQueryEntity.Type!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Type", Value = pQueryEntity.Type });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.StarLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StarLevel", Value = pQueryEntity.StarLevel });
            if (pQueryEntity.StarLevel1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StarLevel1", Value = pQueryEntity.StarLevel1 });
            if (pQueryEntity.StarLevel2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StarLevel2", Value = pQueryEntity.StarLevel2 });
            if (pQueryEntity.StarLevel3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StarLevel3", Value = pQueryEntity.StarLevel3 });
            if (pQueryEntity.StarLevel4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StarLevel4", Value = pQueryEntity.StarLevel4 });
            if (pQueryEntity.StarLevel5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StarLevel5", Value = pQueryEntity.StarLevel5 });
            if (pQueryEntity.Platform!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Platform", Value = pQueryEntity.Platform });
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
            if (pQueryEntity.OrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.IsAnonymity!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAnonymity", Value = pQueryEntity.IsAnonymity });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out ObjectEvaluationEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new ObjectEvaluationEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["EvaluationID"] != DBNull.Value)
			{
				pInstance.EvaluationID =  Convert.ToString(pReader["EvaluationID"]);
			}
			if (pReader["ObjectID"] != DBNull.Value)
			{
				pInstance.ObjectID =  Convert.ToString(pReader["ObjectID"]);
			}
			if (pReader["Type"] != DBNull.Value)
			{
				pInstance.Type =   Convert.ToInt32(pReader["Type"]);
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
                var vipDao = new VipDAO(CurrentUserInfo);
                var vipInfo = vipDao.GetByID(pInstance.VipID);
                if (vipInfo != null)
                {
                    pInstance.VipName = vipInfo.VipName;
                    pInstance.HeadImgUrl = vipInfo.HeadImgUrl;
                }

			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["StarLevel"] != DBNull.Value)
			{
				pInstance.StarLevel =   Convert.ToInt32(pReader["StarLevel"]);
			}
			if (pReader["StarLevel1"] != DBNull.Value)
			{
				pInstance.StarLevel1 =   Convert.ToInt32(pReader["StarLevel1"]);
			}
			if (pReader["StarLevel2"] != DBNull.Value)
			{
				pInstance.StarLevel2 =   Convert.ToInt32(pReader["StarLevel2"]);
			}
			if (pReader["StarLevel3"] != DBNull.Value)
			{
				pInstance.StarLevel3 =   Convert.ToInt32(pReader["StarLevel3"]);
			}
			if (pReader["StarLevel4"] != DBNull.Value)
			{
				pInstance.StarLevel4 =   Convert.ToInt32(pReader["StarLevel4"]);
			}
			if (pReader["StarLevel5"] != DBNull.Value)
			{
				pInstance.StarLevel5 =   Convert.ToInt32(pReader["StarLevel5"]);
			}
			if (pReader["Platform"] != DBNull.Value)
			{
				pInstance.Platform =  Convert.ToString(pReader["Platform"]);
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
			if (pReader["OrderID"] != DBNull.Value)
			{
				pInstance.OrderID =  Convert.ToString(pReader["OrderID"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["IsAnonymity"] != DBNull.Value)
			{
				pInstance.IsAnonymity =   Convert.ToInt32(pReader["IsAnonymity"]);
			}

        }
        #endregion
    }
}
