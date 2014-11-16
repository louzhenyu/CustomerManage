/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:40
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
    /// 表EnterpriseMemberEmployee的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EnterpriseMemberEmployeeDAO : Base.BaseCPOSDAO, ICRUDable<EnterpriseMemberEmployeeEntity>, IQueryable<EnterpriseMemberEmployeeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EnterpriseMemberEmployeeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(EnterpriseMemberEmployeeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(EnterpriseMemberEmployeeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [EnterpriseMemberEmployee](");
            strSql.Append("[EnterpriseMemberStructureID],[EnterpriseMemberID],[MemberName],[MemberNameEn],[Gender],[Telephone],[Mobile],[Email],[ClientID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[EnterpriseMemberEmployeeID])");
            strSql.Append(" values (");
            strSql.Append("@EnterpriseMemberStructureID,@EnterpriseMemberID,@MemberName,@MemberNameEn,@Gender,@Telephone,@Mobile,@Email,@ClientID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@EnterpriseMemberEmployeeID)");            

			Guid? pkGuid;
			if (pEntity.EnterpriseMemberEmployeeID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.EnterpriseMemberEmployeeID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EnterpriseMemberStructureID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@EnterpriseMemberID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@MemberName",SqlDbType.NVarChar),
					new SqlParameter("@MemberNameEn",SqlDbType.NVarChar),
					new SqlParameter("@Gender",SqlDbType.Int),
					new SqlParameter("@Telephone",SqlDbType.NVarChar),
					new SqlParameter("@Mobile",SqlDbType.VarChar),
					new SqlParameter("@Email",SqlDbType.VarChar),
					new SqlParameter("@ClientID",SqlDbType.VarChar),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@EnterpriseMemberEmployeeID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.EnterpriseMemberStructureID;
			parameters[1].Value = pEntity.EnterpriseMemberID;
			parameters[2].Value = pEntity.MemberName;
			parameters[3].Value = pEntity.MemberNameEn;
			parameters[4].Value = pEntity.Gender;
			parameters[5].Value = pEntity.Telephone;
			parameters[6].Value = pEntity.Mobile;
			parameters[7].Value = pEntity.Email;
			parameters[8].Value = pEntity.ClientID;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.EnterpriseMemberEmployeeID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public EnterpriseMemberEmployeeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EnterpriseMemberEmployee] where EnterpriseMemberEmployeeID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            EnterpriseMemberEmployeeEntity m = null;
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
        public EnterpriseMemberEmployeeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EnterpriseMemberEmployee] where isdelete=0");
            //读取数据
            List<EnterpriseMemberEmployeeEntity> list = new List<EnterpriseMemberEmployeeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    EnterpriseMemberEmployeeEntity m;
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
        public void Update(EnterpriseMemberEmployeeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(EnterpriseMemberEmployeeEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EnterpriseMemberEmployeeID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [EnterpriseMemberEmployee] set ");
            if (pIsUpdateNullField || pEntity.EnterpriseMemberStructureID!=null)
                strSql.Append( "[EnterpriseMemberStructureID]=@EnterpriseMemberStructureID,");
            if (pIsUpdateNullField || pEntity.EnterpriseMemberID!=null)
                strSql.Append( "[EnterpriseMemberID]=@EnterpriseMemberID,");
            if (pIsUpdateNullField || pEntity.MemberName!=null)
                strSql.Append( "[MemberName]=@MemberName,");
            if (pIsUpdateNullField || pEntity.MemberNameEn!=null)
                strSql.Append( "[MemberNameEn]=@MemberNameEn,");
            if (pIsUpdateNullField || pEntity.Gender!=null)
                strSql.Append( "[Gender]=@Gender,");
            if (pIsUpdateNullField || pEntity.Telephone!=null)
                strSql.Append( "[Telephone]=@Telephone,");
            if (pIsUpdateNullField || pEntity.Mobile!=null)
                strSql.Append( "[Mobile]=@Mobile,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.ClientID!=null)
                strSql.Append( "[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where EnterpriseMemberEmployeeID=@EnterpriseMemberEmployeeID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@EnterpriseMemberStructureID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@EnterpriseMemberID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@MemberName",SqlDbType.NVarChar),
					new SqlParameter("@MemberNameEn",SqlDbType.NVarChar),
					new SqlParameter("@Gender",SqlDbType.Int),
					new SqlParameter("@Telephone",SqlDbType.NVarChar),
					new SqlParameter("@Mobile",SqlDbType.VarChar),
					new SqlParameter("@Email",SqlDbType.VarChar),
					new SqlParameter("@ClientID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@EnterpriseMemberEmployeeID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.EnterpriseMemberStructureID;
			parameters[1].Value = pEntity.EnterpriseMemberID;
			parameters[2].Value = pEntity.MemberName;
			parameters[3].Value = pEntity.MemberNameEn;
			parameters[4].Value = pEntity.Gender;
			parameters[5].Value = pEntity.Telephone;
			parameters[6].Value = pEntity.Mobile;
			parameters[7].Value = pEntity.Email;
			parameters[8].Value = pEntity.ClientID;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.EnterpriseMemberEmployeeID;

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
        public void Update(EnterpriseMemberEmployeeEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(EnterpriseMemberEmployeeEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(EnterpriseMemberEmployeeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(EnterpriseMemberEmployeeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EnterpriseMemberEmployeeID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.EnterpriseMemberEmployeeID, pTran);           
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
            sql.AppendLine("update [EnterpriseMemberEmployee] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where EnterpriseMemberEmployeeID=@EnterpriseMemberEmployeeID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=CurrentUserInfo.UserID},
                new SqlParameter{ParameterName="@EnterpriseMemberEmployeeID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(EnterpriseMemberEmployeeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.EnterpriseMemberEmployeeID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.EnterpriseMemberEmployeeID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(EnterpriseMemberEmployeeEntity[] pEntities)
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
            sql.AppendLine("update [EnterpriseMemberEmployee] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where EnterpriseMemberEmployeeID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public EnterpriseMemberEmployeeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EnterpriseMemberEmployee] where isdelete=0 ");
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
            List<EnterpriseMemberEmployeeEntity> list = new List<EnterpriseMemberEmployeeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    EnterpriseMemberEmployeeEntity m;
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
        public PagedQueryResult<EnterpriseMemberEmployeeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EnterpriseMemberEmployeeID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [EnterpriseMemberEmployee] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [EnterpriseMemberEmployee] where isdelete=0 ");
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
            PagedQueryResult<EnterpriseMemberEmployeeEntity> result = new PagedQueryResult<EnterpriseMemberEmployeeEntity>();
            List<EnterpriseMemberEmployeeEntity> list = new List<EnterpriseMemberEmployeeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    EnterpriseMemberEmployeeEntity m;
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
        public EnterpriseMemberEmployeeEntity[] QueryByEntity(EnterpriseMemberEmployeeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<EnterpriseMemberEmployeeEntity> PagedQueryByEntity(EnterpriseMemberEmployeeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(EnterpriseMemberEmployeeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EnterpriseMemberEmployeeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EnterpriseMemberEmployeeID", Value = pQueryEntity.EnterpriseMemberEmployeeID });
            if (pQueryEntity.EnterpriseMemberStructureID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EnterpriseMemberStructureID", Value = pQueryEntity.EnterpriseMemberStructureID });
            if (pQueryEntity.EnterpriseMemberID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EnterpriseMemberID", Value = pQueryEntity.EnterpriseMemberID });
            if (pQueryEntity.MemberName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MemberName", Value = pQueryEntity.MemberName });
            if (pQueryEntity.MemberNameEn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MemberNameEn", Value = pQueryEntity.MemberNameEn });
            if (pQueryEntity.Gender!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Gender", Value = pQueryEntity.Gender });
            if (pQueryEntity.Telephone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Telephone", Value = pQueryEntity.Telephone });
            if (pQueryEntity.Mobile!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Mobile", Value = pQueryEntity.Mobile });
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
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
        protected void Load(SqlDataReader pReader, out EnterpriseMemberEmployeeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new EnterpriseMemberEmployeeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["EnterpriseMemberEmployeeID"] != DBNull.Value)
			{
				pInstance.EnterpriseMemberEmployeeID =  (Guid)pReader["EnterpriseMemberEmployeeID"];
			}
			if (pReader["EnterpriseMemberStructureID"] != DBNull.Value)
			{
				pInstance.EnterpriseMemberStructureID =  (Guid)pReader["EnterpriseMemberStructureID"];
			}
			if (pReader["EnterpriseMemberID"] != DBNull.Value)
			{
				pInstance.EnterpriseMemberID =  (Guid)pReader["EnterpriseMemberID"];
			}
			if (pReader["MemberName"] != DBNull.Value)
			{
				pInstance.MemberName =  Convert.ToString(pReader["MemberName"]);
			}
			if (pReader["MemberNameEn"] != DBNull.Value)
			{
				pInstance.MemberNameEn =  Convert.ToString(pReader["MemberNameEn"]);
			}
			if (pReader["Gender"] != DBNull.Value)
			{
				pInstance.Gender =   Convert.ToInt32(pReader["Gender"]);
			}
			if (pReader["Telephone"] != DBNull.Value)
			{
				pInstance.Telephone =  Convert.ToString(pReader["Telephone"]);
			}
			if (pReader["Mobile"] != DBNull.Value)
			{
				pInstance.Mobile =  Convert.ToString(pReader["Mobile"]);
			}
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =  Convert.ToString(pReader["ClientID"]);
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
