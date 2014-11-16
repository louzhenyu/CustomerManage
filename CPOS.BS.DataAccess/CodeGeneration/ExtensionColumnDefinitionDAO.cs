/*
 * Author		:yong.liu
 * EMail		:yong.liu@jitmarketing.cn
 * Company		:JIT
 * Create On	:10/30/2012 5:12:26 PM
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
using System.Text;
using JIT.ManagementPlatform.DataAccess.Base;
using JIT.Utility.DataAccess;
using JIT.ManagementPlatform.Entity;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess.Query;
using JIT.Utility;

namespace JIT.ManagementPlatform.DataAccess
{
    /// <summary>
    /// 表Extension_column_definition的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ExtensionColumnDefinitionDAO : BaseManagementPlatformDAO, ICRUDable<ExtensionColumnDefinitionEntity>, IQueryable<ExtensionColumnDefinitionEntity>
    {
        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ExtensionColumnDefinitionEntity pEntity)
        {
            this.Create(pEntity, null);
        }
        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ExtensionColumnDefinitionEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Extension_column_definition(");
            strSql.Append("Table_definition_id,Column_name,Column_type,Dictionary_name,Client_id,Creater_id,Create_time,Last_updater_id,Last_update_time,Is_delete)");
            strSql.Append(" values (");
            strSql.Append("@Table_definition_id,@Column_name,@Column_type,@Dictionary_name,@Client_id,@Creater_id,@Create_time,@Last_updater_id,@Last_update_time,@Is_delete)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Table_definition_id", SqlDbType.Int,4),
					new SqlParameter("@Column_name", SqlDbType.NVarChar,50),
					new SqlParameter("@Column_type", SqlDbType.Int,4),
					new SqlParameter("@Dictionary_name", SqlDbType.NVarChar,50),
					new SqlParameter("@Client_id", SqlDbType.Int,4),
					new SqlParameter("@Creater_id", SqlDbType.Int,4),
					new SqlParameter("@Create_time", SqlDbType.DateTime),
					new SqlParameter("@Last_updater_id", SqlDbType.Int,4),
					new SqlParameter("@Last_update_time", SqlDbType.DateTime),
					new SqlParameter("@Is_delete", SqlDbType.Bit,1)};
            parameters[0].Value = pEntity.TableDefinitionID;
            parameters[1].Value = pEntity.ColumnName;
            parameters[2].Value = pEntity.ColumnType;
            parameters[3].Value = pEntity.DictionaryName;
            parameters[4].Value = pEntity.ClientID;
            parameters[5].Value = pEntity.CreaterID;
            parameters[6].Value = pEntity.CreateTime;
            parameters[7].Value = pEntity.LastUpdaterID;
            parameters[8].Value = pEntity.LastUpdateTime;
            parameters[9].Value = pEntity.IsDelete;

            //执行并将结果回写
            object result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.ExtensionColumnDefinitionID = Convert.ToInt32(result );
        }
        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ExtensionColumnDefinitionEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            int id = (int)pID;
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from Extension_column_definition where Extension_column_definition_id={0}", id);
            //读取数据
            ExtensionColumnDefinitionEntity m = null;
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
        public ExtensionColumnDefinitionEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from Extension_column_definition where is_delete=0");
            //读取数据
            List<ExtensionColumnDefinitionEntity> list = new List<ExtensionColumnDefinitionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ExtensionColumnDefinitionEntity m;
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
        public void Update(ExtensionColumnDefinitionEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
           
            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Extension_column_definition set ");
            strSql.Append("Table_definition_id=@Table_definition_id,");
            strSql.Append("Column_name=@Column_name,");
            strSql.Append("Column_type=@Column_type,");
            strSql.Append("Dictionary_name=@Dictionary_name,");
            strSql.Append("Client_id=@Client_id,");
            strSql.Append("Creater_id=@Creater_id,");
            strSql.Append("Create_time=@Create_time,");
            strSql.Append("Last_updater_id=@Last_updater_id,");
            strSql.Append("Last_update_time=@Last_update_time,");
            strSql.Append("Is_delete=@Is_delete");
            strSql.Append(" where Extension_column_definition_id=@Extension_column_definition_id");
            SqlParameter[] parameters = {
					new SqlParameter("@Table_definition_id", SqlDbType.Int,4),
					new SqlParameter("@Column_name", SqlDbType.NVarChar,50),
					new SqlParameter("@Column_type", SqlDbType.Int,4),
					new SqlParameter("@Dictionary_name", SqlDbType.NVarChar,50),
					new SqlParameter("@Client_id", SqlDbType.Int,4),
					new SqlParameter("@Creater_id", SqlDbType.Int,4),
					new SqlParameter("@Create_time", SqlDbType.DateTime),
					new SqlParameter("@Last_updater_id", SqlDbType.Int,4),
					new SqlParameter("@Last_update_time", SqlDbType.DateTime),
					new SqlParameter("@Is_delete", SqlDbType.Bit,1),
					new SqlParameter("@Extension_column_definition_id", SqlDbType.Int,4)};
            parameters[0].Value = pEntity.TableDefinitionID;
            parameters[1].Value = pEntity.ColumnName;
            parameters[2].Value = pEntity.ColumnType;
            parameters[3].Value = pEntity.DictionaryName;
            parameters[4].Value = pEntity.ClientID;
            parameters[5].Value = pEntity.CreaterID;
            parameters[6].Value = pEntity.CreateTime;
            parameters[7].Value = pEntity.LastUpdaterID;
            parameters[8].Value = pEntity.LastUpdateTime;
            parameters[9].Value = pEntity.IsDelete;
            parameters[10].Value = pEntity.ExtensionColumnDefinitionID;

            //执行语句
            if (pTran != null)
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(ExtensionColumnDefinitionEntity pEntity)
        {
            this.Update(pEntity, null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ExtensionColumnDefinitionEntity pEntity)
        {
            this.Delete(pEntity, null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ExtensionColumnDefinitionEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
          
            //执行
            this.Delete(pEntity.ExtensionColumnDefinitionID, pTran);
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
            sql.AppendLine("update Extension_column_definition  set Is_delete=1 where Extension_column_definition_id=@Extension_column_definition_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ ParameterName="@Extension_column_definition_id",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            if (pTran != null)
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
        }

        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public ExtensionColumnDefinitionEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from Extension_column_definition where is_delete=0");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat("and {0}", item.GetExpression());
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
            List<ExtensionColumnDefinitionEntity> list = new List<ExtensionColumnDefinitionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ExtensionColumnDefinitionEntity m;
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
        public PagedQueryResult<ExtensionColumnDefinitionEntity> PagedQuery(Utility.DataAccess.Query.IWhereCondition[] pWhereConditions, Utility.DataAccess.Query.OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select row_number()over(");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" Extension_column_definition_id desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from Extension_column_definition where is_delete=0");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from Extension_column_definition where is_delete=0");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    pagedSql.AppendFormat("and {0}", item.GetExpression());
                    totalCountSql.AppendFormat("and {0}", item.GetExpression());
                }
            }
            //取指定页的数据
            pagedSql.AppendFormat(" and ___rn >=0 {0} and ___rn <{1}", pPageSize * pCurrentPageIndex, pPageSize * (pCurrentPageIndex + 1));
            //执行语句并返回结果
            PagedQueryResult<ExtensionColumnDefinitionEntity> result = new PagedQueryResult<ExtensionColumnDefinitionEntity>();
            List<ExtensionColumnDefinitionEntity> list = new List<ExtensionColumnDefinitionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ExtensionColumnDefinitionEntity m;
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

        #endregion

        #region 装载实体
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out ExtensionColumnDefinitionEntity pInstance)
        {
            //TODO:根据如下方式将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new ExtensionColumnDefinitionEntity();

            if (pReader["Extension_column_definition_id"] != null && pReader["Extension_column_definition_id"].ToString() != "")
            {
                pInstance.ExtensionColumnDefinitionID = int.Parse(pReader["Extension_column_definition_id"].ToString());
            }
            if (pReader["Table_definition_id"] != null && pReader["Table_definition_id"].ToString() != "")
            {
                pInstance.TableDefinitionID = int.Parse(pReader["Table_definition_id"].ToString());
            }
            if (pReader["Column_name"] != null && pReader["Column_name"].ToString() != "")
            {
                pInstance.ColumnName = pReader["Column_name"].ToString();
            }
            if (pReader["Column_type"] != null && pReader["Column_type"].ToString() != "")
            {
                pInstance.ColumnType = int.Parse(pReader["Column_type"].ToString());
            }
            if (pReader["Dictionary_name"] != null && pReader["Dictionary_name"].ToString() != "")
            {
                pInstance.DictionaryName = pReader["Dictionary_name"].ToString();
            }
            if (pReader["Client_id"] != null && pReader["Client_id"].ToString() != "")
            {
                pInstance.ClientID = int.Parse(pReader["Client_id"].ToString());
            }
            if (pReader["Creater_id"] != null && pReader["Creater_id"].ToString() != "")
            {
                pInstance.CreaterID = int.Parse(pReader["Creater_id"].ToString());
            }
            if (pReader["Create_time"] != null && pReader["Create_time"].ToString() != "")
            {
                pInstance.CreateTime = DateTime.Parse(pReader["Create_time"].ToString());
            }
            if (pReader["Last_updater_id"] != null && pReader["Last_updater_id"].ToString() != "")
            {
                pInstance.LastUpdaterID = int.Parse(pReader["Last_updater_id"].ToString());
            }
            if (pReader["Last_update_time"] != null && pReader["Last_update_time"].ToString() != "")
            {
                pInstance.LastUpdateTime = DateTime.Parse(pReader["Last_update_time"].ToString());
            }
            if (pReader["Is_delete"] != null && pReader["Is_delete"].ToString() != "")
            {
                if ((pReader["Is_delete"].ToString() == "1") || (pReader["Is_delete"].ToString().ToLower() == "true"))
                {
                    pInstance.IsDelete = true;
                }
                else
                {
                    pInstance.IsDelete = false;
                }
            }
        }
        #endregion
    }
}
