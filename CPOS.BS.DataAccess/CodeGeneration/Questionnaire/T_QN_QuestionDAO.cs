/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:36
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
    /// 表T_QN_Question的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_QN_QuestionDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionEntity>, IQueryable<T_QN_QuestionEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_QN_QuestionDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_QN_QuestionEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_QN_QuestionEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_QN_Question](");
            strSql.Append("[Name],[QuestionidType],[DefaultValue],[ScoreStyle],[MinScore],[MaxScore],[IsRequired],[IsValidateMinChar],[MinChar],[IsValidateMaxChar],[MaxChar],[IsShowProvince],[IsShowCity],[IsShowCounty],[IsShowAddress],[NoRepeat],[IsValidateStartDate],[StartDate],[IsValidateEndDate],[EndDate],[Isphone],[Sort],[Status],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[Questionid])");
            strSql.Append(" values (");
            strSql.Append("@Name,@QuestionidType,@DefaultValue,@ScoreStyle,@MinScore,@MaxScore,@IsRequired,@IsValidateMinChar,@MinChar,@IsValidateMaxChar,@MaxChar,@IsShowProvince,@IsShowCity,@IsShowCounty,@IsShowAddress,@NoRepeat,@IsValidateStartDate,@StartDate,@IsValidateEndDate,@EndDate,@Isphone,@Sort,@Status,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@Questionid)");            

			Guid? pkGuid;
			if (pEntity.Questionid == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.Questionid;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@QuestionidType",SqlDbType.Int),
					new SqlParameter("@DefaultValue",SqlDbType.NVarChar),
					new SqlParameter("@ScoreStyle",SqlDbType.Int),
					new SqlParameter("@MinScore",SqlDbType.Int),
					new SqlParameter("@MaxScore",SqlDbType.Int),
					new SqlParameter("@IsRequired",SqlDbType.Int),
					new SqlParameter("@IsValidateMinChar",SqlDbType.Int),
					new SqlParameter("@MinChar",SqlDbType.Int),
					new SqlParameter("@IsValidateMaxChar",SqlDbType.Int),
					new SqlParameter("@MaxChar",SqlDbType.Int),
					new SqlParameter("@IsShowProvince",SqlDbType.Int),
					new SqlParameter("@IsShowCity",SqlDbType.Int),
					new SqlParameter("@IsShowCounty",SqlDbType.Int),
					new SqlParameter("@IsShowAddress",SqlDbType.Int),
					new SqlParameter("@NoRepeat",SqlDbType.Int),
					new SqlParameter("@IsValidateStartDate",SqlDbType.Int),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@IsValidateEndDate",SqlDbType.Int),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@Isphone",SqlDbType.Int),
					new SqlParameter("@Sort",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@Questionid",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.Name;
			parameters[1].Value = pEntity.QuestionidType;
			parameters[2].Value = pEntity.DefaultValue;
			parameters[3].Value = pEntity.ScoreStyle;
			parameters[4].Value = pEntity.MinScore;
			parameters[5].Value = pEntity.MaxScore;
			parameters[6].Value = pEntity.IsRequired;
			parameters[7].Value = pEntity.IsValidateMinChar;
			parameters[8].Value = pEntity.MinChar;
			parameters[9].Value = pEntity.IsValidateMaxChar;
			parameters[10].Value = pEntity.MaxChar;
			parameters[11].Value = pEntity.IsShowProvince;
			parameters[12].Value = pEntity.IsShowCity;
			parameters[13].Value = pEntity.IsShowCounty;
			parameters[14].Value = pEntity.IsShowAddress;
			parameters[15].Value = pEntity.NoRepeat;
			parameters[16].Value = pEntity.IsValidateStartDate;
			parameters[17].Value = pEntity.StartDate;
			parameters[18].Value = pEntity.IsValidateEndDate;
			parameters[19].Value = pEntity.EndDate;
			parameters[20].Value = pEntity.Isphone;
			parameters[21].Value = pEntity.Sort;
			parameters[22].Value = pEntity.Status;
			parameters[23].Value = pEntity.CustomerID;
			parameters[24].Value = pEntity.CreateTime;
			parameters[25].Value = pEntity.CreateBy;
			parameters[26].Value = pEntity.LastUpdateTime;
			parameters[27].Value = pEntity.LastUpdateBy;
			parameters[28].Value = pEntity.IsDelete;
			parameters[29].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.Questionid = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_QN_QuestionEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_Question] where Questionid='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            T_QN_QuestionEntity m = null;
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
        public T_QN_QuestionEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_Question] where 1=1  and isdelete=0");
            //读取数据
            List<T_QN_QuestionEntity> list = new List<T_QN_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionEntity m;
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
        public void Update(T_QN_QuestionEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_QN_QuestionEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.Questionid.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_QN_Question] set ");
                        if (pIsUpdateNullField || pEntity.Name!=null)
                strSql.Append( "[Name]=@Name,");
            if (pIsUpdateNullField || pEntity.QuestionidType!=null)
                strSql.Append( "[QuestionidType]=@QuestionidType,");
            if (pIsUpdateNullField || pEntity.DefaultValue!=null)
                strSql.Append( "[DefaultValue]=@DefaultValue,");
            if (pIsUpdateNullField || pEntity.ScoreStyle!=null)
                strSql.Append( "[ScoreStyle]=@ScoreStyle,");
            if (pIsUpdateNullField || pEntity.MinScore!=null)
                strSql.Append( "[MinScore]=@MinScore,");
            if (pIsUpdateNullField || pEntity.MaxScore!=null)
                strSql.Append( "[MaxScore]=@MaxScore,");
            if (pIsUpdateNullField || pEntity.IsRequired!=null)
                strSql.Append( "[IsRequired]=@IsRequired,");
            if (pIsUpdateNullField || pEntity.IsValidateMinChar!=null)
                strSql.Append( "[IsValidateMinChar]=@IsValidateMinChar,");
            if (pIsUpdateNullField || pEntity.MinChar!=null)
                strSql.Append( "[MinChar]=@MinChar,");
            if (pIsUpdateNullField || pEntity.IsValidateMaxChar!=null)
                strSql.Append( "[IsValidateMaxChar]=@IsValidateMaxChar,");
            if (pIsUpdateNullField || pEntity.MaxChar!=null)
                strSql.Append( "[MaxChar]=@MaxChar,");
            if (pIsUpdateNullField || pEntity.IsShowProvince!=null)
                strSql.Append( "[IsShowProvince]=@IsShowProvince,");
            if (pIsUpdateNullField || pEntity.IsShowCity!=null)
                strSql.Append( "[IsShowCity]=@IsShowCity,");
            if (pIsUpdateNullField || pEntity.IsShowCounty!=null)
                strSql.Append( "[IsShowCounty]=@IsShowCounty,");
            if (pIsUpdateNullField || pEntity.IsShowAddress!=null)
                strSql.Append( "[IsShowAddress]=@IsShowAddress,");
            if (pIsUpdateNullField || pEntity.NoRepeat!=null)
                strSql.Append( "[NoRepeat]=@NoRepeat,");
            if (pIsUpdateNullField || pEntity.IsValidateStartDate!=null)
                strSql.Append( "[IsValidateStartDate]=@IsValidateStartDate,");
            if (pIsUpdateNullField || pEntity.StartDate!=null)
                strSql.Append( "[StartDate]=@StartDate,");
            if (pIsUpdateNullField || pEntity.IsValidateEndDate!=null)
                strSql.Append( "[IsValidateEndDate]=@IsValidateEndDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.Isphone!=null)
                strSql.Append( "[Isphone]=@Isphone,");
            if (pIsUpdateNullField || pEntity.Sort!=null)
                strSql.Append( "[Sort]=@Sort,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where Questionid=@Questionid ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@QuestionidType",SqlDbType.Int),
					new SqlParameter("@DefaultValue",SqlDbType.NVarChar),
					new SqlParameter("@ScoreStyle",SqlDbType.Int),
					new SqlParameter("@MinScore",SqlDbType.Int),
					new SqlParameter("@MaxScore",SqlDbType.Int),
					new SqlParameter("@IsRequired",SqlDbType.Int),
					new SqlParameter("@IsValidateMinChar",SqlDbType.Int),
					new SqlParameter("@MinChar",SqlDbType.Int),
					new SqlParameter("@IsValidateMaxChar",SqlDbType.Int),
					new SqlParameter("@MaxChar",SqlDbType.Int),
					new SqlParameter("@IsShowProvince",SqlDbType.Int),
					new SqlParameter("@IsShowCity",SqlDbType.Int),
					new SqlParameter("@IsShowCounty",SqlDbType.Int),
					new SqlParameter("@IsShowAddress",SqlDbType.Int),
					new SqlParameter("@NoRepeat",SqlDbType.Int),
					new SqlParameter("@IsValidateStartDate",SqlDbType.Int),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@IsValidateEndDate",SqlDbType.Int),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@Isphone",SqlDbType.Int),
					new SqlParameter("@Sort",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@Questionid",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.Name;
			parameters[1].Value = pEntity.QuestionidType;
			parameters[2].Value = pEntity.DefaultValue;
			parameters[3].Value = pEntity.ScoreStyle;
			parameters[4].Value = pEntity.MinScore;
			parameters[5].Value = pEntity.MaxScore;
			parameters[6].Value = pEntity.IsRequired;
			parameters[7].Value = pEntity.IsValidateMinChar;
			parameters[8].Value = pEntity.MinChar;
			parameters[9].Value = pEntity.IsValidateMaxChar;
			parameters[10].Value = pEntity.MaxChar;
			parameters[11].Value = pEntity.IsShowProvince;
			parameters[12].Value = pEntity.IsShowCity;
			parameters[13].Value = pEntity.IsShowCounty;
			parameters[14].Value = pEntity.IsShowAddress;
			parameters[15].Value = pEntity.NoRepeat;
			parameters[16].Value = pEntity.IsValidateStartDate;
			parameters[17].Value = pEntity.StartDate;
			parameters[18].Value = pEntity.IsValidateEndDate;
			parameters[19].Value = pEntity.EndDate;
			parameters[20].Value = pEntity.Isphone;
			parameters[21].Value = pEntity.Sort;
			parameters[22].Value = pEntity.Status;
			parameters[23].Value = pEntity.CustomerID;
			parameters[24].Value = pEntity.LastUpdateTime;
			parameters[25].Value = pEntity.LastUpdateBy;
			parameters[26].Value = pEntity.Questionid;

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
        public void Update(T_QN_QuestionEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_QN_QuestionEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_QN_QuestionEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.Questionid.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.Questionid.Value, pTran);           
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
            sql.AppendLine("update [T_QN_Question] set  isdelete=1 where Questionid=@Questionid;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@Questionid",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_QN_QuestionEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.Questionid.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.Questionid;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_QN_QuestionEntity[] pEntities)
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
            sql.AppendLine("update [T_QN_Question] set  isdelete=1 where Questionid in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_QN_QuestionEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_Question] where 1=1  and isdelete=0 ");
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
            List<T_QN_QuestionEntity> list = new List<T_QN_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionEntity m;
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
        public PagedQueryResult<T_QN_QuestionEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [Questionid] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_QN_Question] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_QN_Question] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_QN_QuestionEntity> result = new PagedQueryResult<T_QN_QuestionEntity>();
            List<T_QN_QuestionEntity> list = new List<T_QN_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionEntity m;
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
        public T_QN_QuestionEntity[] QueryByEntity(T_QN_QuestionEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_QN_QuestionEntity> PagedQueryByEntity(T_QN_QuestionEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_QN_QuestionEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.Questionid!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Questionid", Value = pQueryEntity.Questionid });
            if (pQueryEntity.Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Name", Value = pQueryEntity.Name });
            if (pQueryEntity.QuestionidType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionidType", Value = pQueryEntity.QuestionidType });
            if (pQueryEntity.DefaultValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DefaultValue", Value = pQueryEntity.DefaultValue });
            if (pQueryEntity.ScoreStyle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ScoreStyle", Value = pQueryEntity.ScoreStyle });
            if (pQueryEntity.MinScore!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinScore", Value = pQueryEntity.MinScore });
            if (pQueryEntity.MaxScore!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxScore", Value = pQueryEntity.MaxScore });
            if (pQueryEntity.IsRequired!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRequired", Value = pQueryEntity.IsRequired });
            if (pQueryEntity.IsValidateMinChar!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsValidateMinChar", Value = pQueryEntity.IsValidateMinChar });
            if (pQueryEntity.MinChar!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinChar", Value = pQueryEntity.MinChar });
            if (pQueryEntity.IsValidateMaxChar!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsValidateMaxChar", Value = pQueryEntity.IsValidateMaxChar });
            if (pQueryEntity.MaxChar!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxChar", Value = pQueryEntity.MaxChar });
            if (pQueryEntity.IsShowProvince!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsShowProvince", Value = pQueryEntity.IsShowProvince });
            if (pQueryEntity.IsShowCity!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsShowCity", Value = pQueryEntity.IsShowCity });
            if (pQueryEntity.IsShowCounty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsShowCounty", Value = pQueryEntity.IsShowCounty });
            if (pQueryEntity.IsShowAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsShowAddress", Value = pQueryEntity.IsShowAddress });
            if (pQueryEntity.NoRepeat!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NoRepeat", Value = pQueryEntity.NoRepeat });
            if (pQueryEntity.IsValidateStartDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsValidateStartDate", Value = pQueryEntity.IsValidateStartDate });
            if (pQueryEntity.StartDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartDate", Value = pQueryEntity.StartDate });
            if (pQueryEntity.IsValidateEndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsValidateEndDate", Value = pQueryEntity.IsValidateEndDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.Isphone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Isphone", Value = pQueryEntity.Isphone });
            if (pQueryEntity.Sort!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sort", Value = pQueryEntity.Sort });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
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
        protected void Load(IDataReader pReader, out T_QN_QuestionEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_QN_QuestionEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["Questionid"] != DBNull.Value)
			{
				pInstance.Questionid =  (Guid)pReader["Questionid"];
			}
			if (pReader["Name"] != DBNull.Value)
			{
				pInstance.Name =  Convert.ToString(pReader["Name"]);
			}
			if (pReader["QuestionidType"] != DBNull.Value)
			{
				pInstance.QuestionidType =   Convert.ToInt32(pReader["QuestionidType"]);
			}
			if (pReader["DefaultValue"] != DBNull.Value)
			{
				pInstance.DefaultValue =  Convert.ToString(pReader["DefaultValue"]);
			}
			if (pReader["ScoreStyle"] != DBNull.Value)
			{
				pInstance.ScoreStyle =   Convert.ToInt32(pReader["ScoreStyle"]);
			}
			if (pReader["MinScore"] != DBNull.Value)
			{
				pInstance.MinScore =   Convert.ToInt32(pReader["MinScore"]);
			}
			if (pReader["MaxScore"] != DBNull.Value)
			{
				pInstance.MaxScore =   Convert.ToInt32(pReader["MaxScore"]);
			}
			if (pReader["IsRequired"] != DBNull.Value)
			{
				pInstance.IsRequired =   Convert.ToInt32(pReader["IsRequired"]);
			}
			if (pReader["IsValidateMinChar"] != DBNull.Value)
			{
				pInstance.IsValidateMinChar =   Convert.ToInt32(pReader["IsValidateMinChar"]);
			}
			if (pReader["MinChar"] != DBNull.Value)
			{
				pInstance.MinChar =   Convert.ToInt32(pReader["MinChar"]);
			}
			if (pReader["IsValidateMaxChar"] != DBNull.Value)
			{
				pInstance.IsValidateMaxChar =   Convert.ToInt32(pReader["IsValidateMaxChar"]);
			}
			if (pReader["MaxChar"] != DBNull.Value)
			{
				pInstance.MaxChar =   Convert.ToInt32(pReader["MaxChar"]);
			}
			if (pReader["IsShowProvince"] != DBNull.Value)
			{
				pInstance.IsShowProvince =   Convert.ToInt32(pReader["IsShowProvince"]);
			}
			if (pReader["IsShowCity"] != DBNull.Value)
			{
				pInstance.IsShowCity =   Convert.ToInt32(pReader["IsShowCity"]);
			}
			if (pReader["IsShowCounty"] != DBNull.Value)
			{
				pInstance.IsShowCounty =   Convert.ToInt32(pReader["IsShowCounty"]);
			}
			if (pReader["IsShowAddress"] != DBNull.Value)
			{
				pInstance.IsShowAddress =   Convert.ToInt32(pReader["IsShowAddress"]);
			}
			if (pReader["NoRepeat"] != DBNull.Value)
			{
				pInstance.NoRepeat =   Convert.ToInt32(pReader["NoRepeat"]);
			}
			if (pReader["IsValidateStartDate"] != DBNull.Value)
			{
				pInstance.IsValidateStartDate =   Convert.ToInt32(pReader["IsValidateStartDate"]);
			}
			if (pReader["StartDate"] != DBNull.Value)
			{
				pInstance.StartDate =  Convert.ToDateTime(pReader["StartDate"]);
			}
			if (pReader["IsValidateEndDate"] != DBNull.Value)
			{
				pInstance.IsValidateEndDate =   Convert.ToInt32(pReader["IsValidateEndDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["Isphone"] != DBNull.Value)
			{
				pInstance.Isphone =   Convert.ToInt32(pReader["Isphone"]);
			}
			if (pReader["Sort"] != DBNull.Value)
			{
				pInstance.Sort =   Convert.ToInt32(pReader["Sort"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
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
