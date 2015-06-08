/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-6-6 16:15:13
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
    /// 表X_Question的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class X_QuestionDAO : Base.BaseCPOSDAO, ICRUDable<X_QuestionEntity>, IQueryable<X_QuestionEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public X_QuestionDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(X_QuestionEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(X_QuestionEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [X_Question](");
            strSql.Append("[Name],[NameUrl],[Option1],[Option4],[Option3],[Option2],[Option1ImageUrl],[Option2ImageUrl],[Option3ImageUrl],[Option4ImageUrl],[Answer],[IsMultiple],[BeginTime],[EndTime],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[QuestionID])");
            strSql.Append(" values (");
            strSql.Append("@Name,@NameUrl,@Option1,@Option4,@Option3,@Option2,@Option1ImageUrl,@Option2ImageUrl,@Option3ImageUrl,@Option4ImageUrl,@Answer,@IsMultiple,@BeginTime,@EndTime,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@QuestionID)");            

			Guid? pkGuid;
			if (pEntity.QuestionID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.QuestionID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@NameUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option1",SqlDbType.NVarChar),
					new SqlParameter("@Option4",SqlDbType.NVarChar),
					new SqlParameter("@Option3",SqlDbType.NVarChar),
					new SqlParameter("@Option2",SqlDbType.NVarChar),
					new SqlParameter("@Option1ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option2ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option3ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option4ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Answer",SqlDbType.Int),
					new SqlParameter("@IsMultiple",SqlDbType.Int),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@QuestionID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.Name;
			parameters[1].Value = pEntity.NameUrl;
			parameters[2].Value = pEntity.Option1;
			parameters[3].Value = pEntity.Option4;
			parameters[4].Value = pEntity.Option3;
			parameters[5].Value = pEntity.Option2;
			parameters[6].Value = pEntity.Option1ImageUrl;
			parameters[7].Value = pEntity.Option2ImageUrl;
			parameters[8].Value = pEntity.Option3ImageUrl;
			parameters[9].Value = pEntity.Option4ImageUrl;
			parameters[10].Value = pEntity.Answer;
			parameters[11].Value = pEntity.IsMultiple;
			parameters[12].Value = pEntity.BeginTime;
			parameters[13].Value = pEntity.EndTime;
			parameters[14].Value = pEntity.CustomerID;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.IsDelete;
			parameters[20].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.QuestionID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public X_QuestionEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [X_Question] where QuestionID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            X_QuestionEntity m = null;
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
        public X_QuestionEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [X_Question] where 1=1  and isdelete=0");
            //读取数据
            List<X_QuestionEntity> list = new List<X_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    X_QuestionEntity m;
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
        public void Update(X_QuestionEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(X_QuestionEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.QuestionID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [X_Question] set ");
                        if (pIsUpdateNullField || pEntity.Name!=null)
                strSql.Append( "[Name]=@Name,");
            if (pIsUpdateNullField || pEntity.NameUrl!=null)
                strSql.Append( "[NameUrl]=@NameUrl,");
            if (pIsUpdateNullField || pEntity.Option1!=null)
                strSql.Append( "[Option1]=@Option1,");
            if (pIsUpdateNullField || pEntity.Option4!=null)
                strSql.Append( "[Option4]=@Option4,");
            if (pIsUpdateNullField || pEntity.Option3!=null)
                strSql.Append( "[Option3]=@Option3,");
            if (pIsUpdateNullField || pEntity.Option2!=null)
                strSql.Append( "[Option2]=@Option2,");
            if (pIsUpdateNullField || pEntity.Option1ImageUrl!=null)
                strSql.Append( "[Option1ImageUrl]=@Option1ImageUrl,");
            if (pIsUpdateNullField || pEntity.Option2ImageUrl!=null)
                strSql.Append( "[Option2ImageUrl]=@Option2ImageUrl,");
            if (pIsUpdateNullField || pEntity.Option3ImageUrl!=null)
                strSql.Append( "[Option3ImageUrl]=@Option3ImageUrl,");
            if (pIsUpdateNullField || pEntity.Option4ImageUrl!=null)
                strSql.Append( "[Option4ImageUrl]=@Option4ImageUrl,");
            if (pIsUpdateNullField || pEntity.Answer!=null)
                strSql.Append( "[Answer]=@Answer,");
            if (pIsUpdateNullField || pEntity.IsMultiple!=null)
                strSql.Append( "[IsMultiple]=@IsMultiple,");
            if (pIsUpdateNullField || pEntity.BeginTime!=null)
                strSql.Append( "[BeginTime]=@BeginTime,");
            if (pIsUpdateNullField || pEntity.EndTime!=null)
                strSql.Append( "[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where QuestionID=@QuestionID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@NameUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option1",SqlDbType.NVarChar),
					new SqlParameter("@Option4",SqlDbType.NVarChar),
					new SqlParameter("@Option3",SqlDbType.NVarChar),
					new SqlParameter("@Option2",SqlDbType.NVarChar),
					new SqlParameter("@Option1ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option2ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option3ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Option4ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Answer",SqlDbType.Int),
					new SqlParameter("@IsMultiple",SqlDbType.Int),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@QuestionID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.Name;
			parameters[1].Value = pEntity.NameUrl;
			parameters[2].Value = pEntity.Option1;
			parameters[3].Value = pEntity.Option4;
			parameters[4].Value = pEntity.Option3;
			parameters[5].Value = pEntity.Option2;
			parameters[6].Value = pEntity.Option1ImageUrl;
			parameters[7].Value = pEntity.Option2ImageUrl;
			parameters[8].Value = pEntity.Option3ImageUrl;
			parameters[9].Value = pEntity.Option4ImageUrl;
			parameters[10].Value = pEntity.Answer;
			parameters[11].Value = pEntity.IsMultiple;
			parameters[12].Value = pEntity.BeginTime;
			parameters[13].Value = pEntity.EndTime;
			parameters[14].Value = pEntity.CustomerID;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.QuestionID;

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
        public void Update(X_QuestionEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(X_QuestionEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(X_QuestionEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.QuestionID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.QuestionID.Value, pTran);           
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
            sql.AppendLine("update [X_Question] set  isdelete=1 where QuestionID=@QuestionID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@QuestionID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(X_QuestionEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.QuestionID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.QuestionID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(X_QuestionEntity[] pEntities)
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
            sql.AppendLine("update [X_Question] set  isdelete=1 where QuestionID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public X_QuestionEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [X_Question] where 1=1  and isdelete=0 ");
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
            List<X_QuestionEntity> list = new List<X_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    X_QuestionEntity m;
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
        public PagedQueryResult<X_QuestionEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [QuestionID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [X_Question] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [X_Question] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<X_QuestionEntity> result = new PagedQueryResult<X_QuestionEntity>();
            List<X_QuestionEntity> list = new List<X_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    X_QuestionEntity m;
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
        public X_QuestionEntity[] QueryByEntity(X_QuestionEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<X_QuestionEntity> PagedQueryByEntity(X_QuestionEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(X_QuestionEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.QuestionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionID", Value = pQueryEntity.QuestionID });
            if (pQueryEntity.Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Name", Value = pQueryEntity.Name });
            if (pQueryEntity.NameUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NameUrl", Value = pQueryEntity.NameUrl });
            if (pQueryEntity.Option1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option1", Value = pQueryEntity.Option1 });
            if (pQueryEntity.Option4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option4", Value = pQueryEntity.Option4 });
            if (pQueryEntity.Option3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option3", Value = pQueryEntity.Option3 });
            if (pQueryEntity.Option2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option2", Value = pQueryEntity.Option2 });
            if (pQueryEntity.Option1ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option1ImageUrl", Value = pQueryEntity.Option1ImageUrl });
            if (pQueryEntity.Option2ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option2ImageUrl", Value = pQueryEntity.Option2ImageUrl });
            if (pQueryEntity.Option3ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option3ImageUrl", Value = pQueryEntity.Option3ImageUrl });
            if (pQueryEntity.Option4ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Option4ImageUrl", Value = pQueryEntity.Option4ImageUrl });
            if (pQueryEntity.Answer!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Answer", Value = pQueryEntity.Answer });
            if (pQueryEntity.IsMultiple!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMultiple", Value = pQueryEntity.IsMultiple });
            if (pQueryEntity.BeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
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
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out X_QuestionEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new X_QuestionEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["QuestionID"] != DBNull.Value)
			{
				pInstance.QuestionID =  (Guid)pReader["QuestionID"];
			}
			if (pReader["Name"] != DBNull.Value)
			{
				pInstance.Name =  Convert.ToString(pReader["Name"]);
			}
			if (pReader["NameUrl"] != DBNull.Value)
			{
				pInstance.NameUrl =  Convert.ToString(pReader["NameUrl"]);
			}
			if (pReader["Option1"] != DBNull.Value)
			{
				pInstance.Option1 =  Convert.ToString(pReader["Option1"]);
			}
			if (pReader["Option4"] != DBNull.Value)
			{
				pInstance.Option4 =  Convert.ToString(pReader["Option4"]);
			}
			if (pReader["Option3"] != DBNull.Value)
			{
				pInstance.Option3 =  Convert.ToString(pReader["Option3"]);
			}
			if (pReader["Option2"] != DBNull.Value)
			{
				pInstance.Option2 =  Convert.ToString(pReader["Option2"]);
			}
			if (pReader["Option1ImageUrl"] != DBNull.Value)
			{
				pInstance.Option1ImageUrl =  Convert.ToString(pReader["Option1ImageUrl"]);
			}
			if (pReader["Option2ImageUrl"] != DBNull.Value)
			{
				pInstance.Option2ImageUrl =  Convert.ToString(pReader["Option2ImageUrl"]);
			}
			if (pReader["Option3ImageUrl"] != DBNull.Value)
			{
				pInstance.Option3ImageUrl =  Convert.ToString(pReader["Option3ImageUrl"]);
			}
			if (pReader["Option4ImageUrl"] != DBNull.Value)
			{
				pInstance.Option4ImageUrl =  Convert.ToString(pReader["Option4ImageUrl"]);
			}
			if (pReader["Answer"] != DBNull.Value)
			{
				pInstance.Answer =   Convert.ToInt32(pReader["Answer"]);
			}
			if (pReader["IsMultiple"] != DBNull.Value)
			{
				pInstance.IsMultiple =   Convert.ToInt32(pReader["IsMultiple"]);
			}
			if (pReader["BeginTime"] != DBNull.Value)
			{
				pInstance.BeginTime =  Convert.ToDateTime(pReader["BeginTime"]);
			}
			if (pReader["EndTime"] != DBNull.Value)
			{
				pInstance.EndTime =  Convert.ToDateTime(pReader["EndTime"]);
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
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
