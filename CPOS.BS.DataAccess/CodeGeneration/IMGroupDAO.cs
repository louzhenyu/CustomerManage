/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/26 14:59:01
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
    /// 表IMGroup的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class IMGroupDAO : Base.BaseCPOSDAO, ICRUDable<IMGroupEntity>, IQueryable<IMGroupEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public IMGroupDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(IMGroupEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(IMGroupEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [IMGroup](");
            strSql.Append("[GroupName],[LogoUrl],[Description],[CustomerID],[Telephone],[UserCount],[GroupLevel],[IsPublic],[ApproveNeededLevel],[InvitationLevel],[ChatLevel],[QuitLevel],[BindGroupID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[ChatGroupID])");
            strSql.Append(" values (");
            strSql.Append("@GroupName,@LogoUrl,@Description,@CustomerID,@Telephone,@UserCount,@GroupLevel,@IsPublic,@ApproveNeededLevel,@InvitationLevel,@ChatLevel,@QuitLevel,@BindGroupID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@ChatGroupID)");

            Guid? pkGuid;
            if (pEntity.ChatGroupID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.ChatGroupID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@GroupName",SqlDbType.NVarChar),
					new SqlParameter("@LogoUrl",SqlDbType.VarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@Telephone",SqlDbType.Char),
					new SqlParameter("@UserCount",SqlDbType.Int),
					new SqlParameter("@GroupLevel",SqlDbType.Int),
					new SqlParameter("@IsPublic",SqlDbType.Int),
					new SqlParameter("@ApproveNeededLevel",SqlDbType.Int),
					new SqlParameter("@InvitationLevel",SqlDbType.Int),
					new SqlParameter("@ChatLevel",SqlDbType.Int),
					new SqlParameter("@QuitLevel",SqlDbType.Int),
					new SqlParameter("@BindGroupID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ChatGroupID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.GroupName;
            parameters[1].Value = pEntity.LogoUrl;
            parameters[2].Value = pEntity.Description;
            parameters[3].Value = pEntity.CustomerID;
            parameters[4].Value = pEntity.Telephone;
            parameters[5].Value = pEntity.UserCount;
            parameters[6].Value = pEntity.GroupLevel;
            parameters[7].Value = pEntity.IsPublic;
            parameters[8].Value = pEntity.ApproveNeededLevel;
            parameters[9].Value = pEntity.InvitationLevel;
            parameters[10].Value = pEntity.ChatLevel;
            parameters[11].Value = pEntity.QuitLevel;
            parameters[12].Value = pEntity.BindGroupID;
            parameters[13].Value = pEntity.CreateTime;
            parameters[14].Value = pEntity.CreateBy;
            parameters[15].Value = pEntity.LastUpdateTime;
            parameters[16].Value = pEntity.LastUpdateBy;
            parameters[17].Value = pEntity.IsDelete;
            parameters[18].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.ChatGroupID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public IMGroupEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [IMGroup] where ChatGroupID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            IMGroupEntity m = null;
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
        public IMGroupEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [IMGroup] where isdelete=0");
            //读取数据
            List<IMGroupEntity> list = new List<IMGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    IMGroupEntity m;
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
        public void Update(IMGroupEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(IMGroupEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ChatGroupID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [IMGroup] set ");
            if (pIsUpdateNullField || pEntity.GroupName != null)
                strSql.Append("[GroupName]=@GroupName,");
            if (pIsUpdateNullField || pEntity.LogoUrl != null)
                strSql.Append("[LogoUrl]=@LogoUrl,");
            if (pIsUpdateNullField || pEntity.Description != null)
                strSql.Append("[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.Telephone != null)
                strSql.Append("[Telephone]=@Telephone,");
            if (pIsUpdateNullField || pEntity.UserCount != null)
                strSql.Append("[UserCount]=@UserCount,");
            if (pIsUpdateNullField || pEntity.GroupLevel != null)
                strSql.Append("[GroupLevel]=@GroupLevel,");
            if (pIsUpdateNullField || pEntity.IsPublic != null)
                strSql.Append("[IsPublic]=@IsPublic,");
            if (pIsUpdateNullField || pEntity.ApproveNeededLevel != null)
                strSql.Append("[ApproveNeededLevel]=@ApproveNeededLevel,");
            if (pIsUpdateNullField || pEntity.InvitationLevel != null)
                strSql.Append("[InvitationLevel]=@InvitationLevel,");
            if (pIsUpdateNullField || pEntity.ChatLevel != null)
                strSql.Append("[ChatLevel]=@ChatLevel,");
            if (pIsUpdateNullField || pEntity.QuitLevel != null)
                strSql.Append("[QuitLevel]=@QuitLevel,");
            if (pIsUpdateNullField || pEntity.BindGroupID != null)
                strSql.Append("[BindGroupID]=@BindGroupID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ChatGroupID=@ChatGroupID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@GroupName",SqlDbType.NVarChar),
					new SqlParameter("@LogoUrl",SqlDbType.VarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@Telephone",SqlDbType.Char),
					new SqlParameter("@UserCount",SqlDbType.Int),
					new SqlParameter("@GroupLevel",SqlDbType.Int),
					new SqlParameter("@IsPublic",SqlDbType.Int),
					new SqlParameter("@ApproveNeededLevel",SqlDbType.Int),
					new SqlParameter("@InvitationLevel",SqlDbType.Int),
					new SqlParameter("@ChatLevel",SqlDbType.Int),
					new SqlParameter("@QuitLevel",SqlDbType.Int),
					new SqlParameter("@BindGroupID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@ChatGroupID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.GroupName;
            parameters[1].Value = pEntity.LogoUrl;
            parameters[2].Value = pEntity.Description;
            parameters[3].Value = pEntity.CustomerID;
            parameters[4].Value = pEntity.Telephone;
            parameters[5].Value = pEntity.UserCount;
            parameters[6].Value = pEntity.GroupLevel;
            parameters[7].Value = pEntity.IsPublic;
            parameters[8].Value = pEntity.ApproveNeededLevel;
            parameters[9].Value = pEntity.InvitationLevel;
            parameters[10].Value = pEntity.ChatLevel;
            parameters[11].Value = pEntity.QuitLevel;
            parameters[12].Value = pEntity.BindGroupID;
            parameters[13].Value = pEntity.LastUpdateTime;
            parameters[14].Value = pEntity.LastUpdateBy;
            parameters[15].Value = pEntity.ChatGroupID;

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
        public void Update(IMGroupEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(IMGroupEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(IMGroupEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(IMGroupEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ChatGroupID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ChatGroupID, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [IMGroup] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ChatGroupID=@ChatGroupID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ChatGroupID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(IMGroupEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ChatGroupID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.ChatGroupID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(IMGroupEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [IMGroup] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where ChatGroupID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public IMGroupEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [IMGroup] where isdelete=0 ");
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
            List<IMGroupEntity> list = new List<IMGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    IMGroupEntity m;
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
        public PagedQueryResult<IMGroupEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [ChatGroupID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [IMGroup] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [IMGroup] where isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<IMGroupEntity> result = new PagedQueryResult<IMGroupEntity>();
            List<IMGroupEntity> list = new List<IMGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    IMGroupEntity m;
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
        public IMGroupEntity[] QueryByEntity(IMGroupEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<IMGroupEntity> PagedQueryByEntity(IMGroupEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(IMGroupEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ChatGroupID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChatGroupID", Value = pQueryEntity.ChatGroupID });
            if (pQueryEntity.GroupName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupName", Value = pQueryEntity.GroupName });
            if (pQueryEntity.LogoUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogoUrl", Value = pQueryEntity.LogoUrl });
            if (pQueryEntity.Description != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.Telephone != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Telephone", Value = pQueryEntity.Telephone });
            if (pQueryEntity.UserCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserCount", Value = pQueryEntity.UserCount });
            if (pQueryEntity.GroupLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupLevel", Value = pQueryEntity.GroupLevel });
            if (pQueryEntity.IsPublic != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPublic", Value = pQueryEntity.IsPublic });
            if (pQueryEntity.ApproveNeededLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApproveNeededLevel", Value = pQueryEntity.ApproveNeededLevel });
            if (pQueryEntity.InvitationLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InvitationLevel", Value = pQueryEntity.InvitationLevel });
            if (pQueryEntity.ChatLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChatLevel", Value = pQueryEntity.ChatLevel });
            if (pQueryEntity.QuitLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuitLevel", Value = pQueryEntity.QuitLevel });
            if (pQueryEntity.BindGroupID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BindGroupID", Value = pQueryEntity.BindGroupID });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out IMGroupEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new IMGroupEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["ChatGroupID"] != DBNull.Value)
            {
                pInstance.ChatGroupID = (Guid)pReader["ChatGroupID"];
            }
            if (pReader["GroupName"] != DBNull.Value)
            {
                pInstance.GroupName = Convert.ToString(pReader["GroupName"]);
            }
            if (pReader["LogoUrl"] != DBNull.Value)
            {
                pInstance.LogoUrl = Convert.ToString(pReader["LogoUrl"]);
            }
            if (pReader["Description"] != DBNull.Value)
            {
                pInstance.Description = Convert.ToString(pReader["Description"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }
            if (pReader["Telephone"] != DBNull.Value)
            {
                pInstance.Telephone = Convert.ToString(pReader["Telephone"]);
            }
            if (pReader["UserCount"] != DBNull.Value)
            {
                pInstance.UserCount = Convert.ToInt32(pReader["UserCount"]);
            }
            if (pReader["GroupLevel"] != DBNull.Value)
            {
                pInstance.GroupLevel = Convert.ToInt32(pReader["GroupLevel"]);
            }
            if (pReader["IsPublic"] != DBNull.Value)
            {
                pInstance.IsPublic = Convert.ToInt32(pReader["IsPublic"]);
            }
            if (pReader["ApproveNeededLevel"] != DBNull.Value)
            {
                pInstance.ApproveNeededLevel = Convert.ToInt32(pReader["ApproveNeededLevel"]);
            }
            if (pReader["InvitationLevel"] != DBNull.Value)
            {
                pInstance.InvitationLevel = Convert.ToInt32(pReader["InvitationLevel"]);
            }
            if (pReader["ChatLevel"] != DBNull.Value)
            {
                pInstance.ChatLevel = Convert.ToInt32(pReader["ChatLevel"]);
            }
            if (pReader["QuitLevel"] != DBNull.Value)
            {
                pInstance.QuitLevel = Convert.ToInt32(pReader["QuitLevel"]);
            }
            if (pReader["BindGroupID"] != DBNull.Value)
            {
                pInstance.BindGroupID = pReader["BindGroupID"].ToString();
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }

        }
        #endregion
    }
}
