/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/19 15:40:35
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
    /// 表ZCourse的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ZCourseDAO : Base.BaseCPOSDAO, ICRUDable<ZCourseEntity>, IQueryable<ZCourseEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ZCourseDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ZCourseEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ZCourseEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ZCourse](");
            strSql.Append("[CouseDesc],[CourseTypeId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CourseName],[CourseSummary],[CourseFee],[CourseStartTime],[CouseCapital],[CouseContact],[Email],[EmailTitle],[ParentId],[CourseLevel],[CourseId])");
            strSql.Append(" values (");
            strSql.Append("@CouseDesc,@CourseTypeId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CourseName,@CourseSummary,@CourseFee,@CourseStartTime,@CouseCapital,@CouseContact,@Email,@EmailTitle,@ParentId,@CourseLevel,@CourseId)");            

			string pkString = pEntity.CourseId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouseDesc",SqlDbType.NVarChar),
					new SqlParameter("@CourseTypeId",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CourseName",SqlDbType.NVarChar),
					new SqlParameter("@CourseSummary",SqlDbType.NVarChar),
					new SqlParameter("@CourseFee",SqlDbType.NVarChar),
					new SqlParameter("@CourseStartTime",SqlDbType.NVarChar),
					new SqlParameter("@CouseCapital",SqlDbType.NVarChar),
					new SqlParameter("@CouseContact",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@EmailTitle",SqlDbType.NVarChar),
					new SqlParameter("@ParentId",SqlDbType.NVarChar),
					new SqlParameter("@CourseLevel",SqlDbType.Int),
					new SqlParameter("@CourseId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.CouseDesc;
			parameters[1].Value = pEntity.CourseTypeId;
			parameters[2].Value = pEntity.CreateTime;
			parameters[3].Value = pEntity.CreateBy;
			parameters[4].Value = pEntity.LastUpdateBy;
			parameters[5].Value = pEntity.LastUpdateTime;
			parameters[6].Value = pEntity.IsDelete;
			parameters[7].Value = pEntity.CourseName;
			parameters[8].Value = pEntity.CourseSummary;
			parameters[9].Value = pEntity.CourseFee;
			parameters[10].Value = pEntity.CourseStartTime;
			parameters[11].Value = pEntity.CouseCapital;
			parameters[12].Value = pEntity.CouseContact;
			parameters[13].Value = pEntity.Email;
			parameters[14].Value = pEntity.EmailTitle;
			parameters[15].Value = pEntity.ParentId;
			parameters[16].Value = pEntity.CourseLevel;
			parameters[17].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CourseId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ZCourseEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZCourse] where CourseId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            ZCourseEntity m = null;
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
        public ZCourseEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZCourse] where isdelete=0");
            //读取数据
            List<ZCourseEntity> list = new List<ZCourseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ZCourseEntity m;
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
        public void Update(ZCourseEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(ZCourseEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CourseId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ZCourse] set ");
            if (pIsUpdateNullField || pEntity.CouseDesc!=null)
                strSql.Append( "[CouseDesc]=@CouseDesc,");
            if (pIsUpdateNullField || pEntity.CourseTypeId!=null)
                strSql.Append( "[CourseTypeId]=@CourseTypeId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CourseName!=null)
                strSql.Append( "[CourseName]=@CourseName,");
            if (pIsUpdateNullField || pEntity.CourseSummary!=null)
                strSql.Append( "[CourseSummary]=@CourseSummary,");
            if (pIsUpdateNullField || pEntity.CourseFee!=null)
                strSql.Append( "[CourseFee]=@CourseFee,");
            if (pIsUpdateNullField || pEntity.CourseStartTime!=null)
                strSql.Append( "[CourseStartTime]=@CourseStartTime,");
            if (pIsUpdateNullField || pEntity.CouseCapital!=null)
                strSql.Append( "[CouseCapital]=@CouseCapital,");
            if (pIsUpdateNullField || pEntity.CouseContact!=null)
                strSql.Append( "[CouseContact]=@CouseContact,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.EmailTitle!=null)
                strSql.Append( "[EmailTitle]=@EmailTitle,");
            if (pIsUpdateNullField || pEntity.ParentId!=null)
                strSql.Append( "[ParentId]=@ParentId,");
            if (pIsUpdateNullField || pEntity.CourseLevel!=null)
                strSql.Append( "[CourseLevel]=@CourseLevel");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CourseId=@CourseId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouseDesc",SqlDbType.NVarChar),
					new SqlParameter("@CourseTypeId",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CourseName",SqlDbType.NVarChar),
					new SqlParameter("@CourseSummary",SqlDbType.NVarChar),
					new SqlParameter("@CourseFee",SqlDbType.NVarChar),
					new SqlParameter("@CourseStartTime",SqlDbType.NVarChar),
					new SqlParameter("@CouseCapital",SqlDbType.NVarChar),
					new SqlParameter("@CouseContact",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@EmailTitle",SqlDbType.NVarChar),
					new SqlParameter("@ParentId",SqlDbType.NVarChar),
					new SqlParameter("@CourseLevel",SqlDbType.Int),
					new SqlParameter("@CourseId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.CouseDesc;
			parameters[1].Value = pEntity.CourseTypeId;
			parameters[2].Value = pEntity.LastUpdateBy;
			parameters[3].Value = pEntity.LastUpdateTime;
			parameters[4].Value = pEntity.CourseName;
			parameters[5].Value = pEntity.CourseSummary;
			parameters[6].Value = pEntity.CourseFee;
			parameters[7].Value = pEntity.CourseStartTime;
			parameters[8].Value = pEntity.CouseCapital;
			parameters[9].Value = pEntity.CouseContact;
			parameters[10].Value = pEntity.Email;
			parameters[11].Value = pEntity.EmailTitle;
			parameters[12].Value = pEntity.ParentId;
			parameters[13].Value = pEntity.CourseLevel;
			parameters[14].Value = pEntity.CourseId;

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
        public void Update(ZCourseEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(ZCourseEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ZCourseEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ZCourseEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CourseId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.CourseId, pTran);           
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
            sql.AppendLine("update [ZCourse] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where CourseId=@CourseId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@CourseId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ZCourseEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.CourseId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.CourseId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(ZCourseEntity[] pEntities)
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
            sql.AppendLine("update [ZCourse] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where CourseId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ZCourseEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZCourse] where isdelete=0 ");
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
            List<ZCourseEntity> list = new List<ZCourseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ZCourseEntity m;
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
        public PagedQueryResult<ZCourseEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CourseId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [ZCourse] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [ZCourse] where isdelete=0 ");
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
            PagedQueryResult<ZCourseEntity> result = new PagedQueryResult<ZCourseEntity>();
            List<ZCourseEntity> list = new List<ZCourseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ZCourseEntity m;
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
        public ZCourseEntity[] QueryByEntity(ZCourseEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ZCourseEntity> PagedQueryByEntity(ZCourseEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ZCourseEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CourseId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseId", Value = pQueryEntity.CourseId });
            if (pQueryEntity.CouseDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouseDesc", Value = pQueryEntity.CouseDesc });
            if (pQueryEntity.CourseTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseTypeId", Value = pQueryEntity.CourseTypeId });
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
            if (pQueryEntity.CourseName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseName", Value = pQueryEntity.CourseName });
            if (pQueryEntity.CourseSummary!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseSummary", Value = pQueryEntity.CourseSummary });
            if (pQueryEntity.CourseFee!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseFee", Value = pQueryEntity.CourseFee });
            if (pQueryEntity.CourseStartTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseStartTime", Value = pQueryEntity.CourseStartTime });
            if (pQueryEntity.CouseCapital!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouseCapital", Value = pQueryEntity.CouseCapital });
            if (pQueryEntity.CouseContact!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouseContact", Value = pQueryEntity.CouseContact });
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.EmailTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EmailTitle", Value = pQueryEntity.EmailTitle });
            if (pQueryEntity.ParentId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentId", Value = pQueryEntity.ParentId });
            if (pQueryEntity.CourseLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseLevel", Value = pQueryEntity.CourseLevel });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out ZCourseEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new ZCourseEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CourseId"] != DBNull.Value)
			{
				pInstance.CourseId =  Convert.ToString(pReader["CourseId"]);
			}
			if (pReader["CouseDesc"] != DBNull.Value)
			{
				pInstance.CouseDesc =  Convert.ToString(pReader["CouseDesc"]);
			}
			if (pReader["CourseTypeId"] != DBNull.Value)
			{
				pInstance.CourseTypeId =   Convert.ToInt32(pReader["CourseTypeId"]);
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
			if (pReader["CourseName"] != DBNull.Value)
			{
				pInstance.CourseName =  Convert.ToString(pReader["CourseName"]);
			}
			if (pReader["CourseSummary"] != DBNull.Value)
			{
				pInstance.CourseSummary =  Convert.ToString(pReader["CourseSummary"]);
			}
			if (pReader["CourseFee"] != DBNull.Value)
			{
				pInstance.CourseFee =  Convert.ToString(pReader["CourseFee"]);
			}
			if (pReader["CourseStartTime"] != DBNull.Value)
			{
				pInstance.CourseStartTime =  Convert.ToString(pReader["CourseStartTime"]);
			}
			if (pReader["CouseCapital"] != DBNull.Value)
			{
				pInstance.CouseCapital =  Convert.ToString(pReader["CouseCapital"]);
			}
			if (pReader["CouseContact"] != DBNull.Value)
			{
				pInstance.CouseContact =  Convert.ToString(pReader["CouseContact"]);
			}
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["EmailTitle"] != DBNull.Value)
			{
				pInstance.EmailTitle =  Convert.ToString(pReader["EmailTitle"]);
			}
			if (pReader["ParentId"] != DBNull.Value)
			{
				pInstance.ParentId =  Convert.ToString(pReader["ParentId"]);
			}
			if (pReader["CourseLevel"] != DBNull.Value)
			{
				pInstance.CourseLevel =   Convert.ToInt32(pReader["CourseLevel"]);
			}

        }
        #endregion
    }
}
