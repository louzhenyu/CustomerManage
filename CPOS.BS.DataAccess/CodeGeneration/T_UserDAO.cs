/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 14:33:03
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
    /// 表T_User的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_UserDAO : Base.BaseCPOSDAO, ICRUDable<T_UserEntity>, IQueryable<T_UserEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_UserDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_UserEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_UserEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            ////初始化固定字段
            //pEntity.CreateTime = DateTime.Now;
            //pEntity.CreateBy = CurrentUserInfo.UserID;
            //pEntity.LastUpdateTime = pEntity.CreateTime;
            //pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            //pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_User](");
            strSql.Append("[user_code],[user_name],[user_gender],[user_birthday],[user_password],[user_email],[user_identity],[user_telephone],[user_cellphone],[user_address],[user_postcode],[user_remark],[user_status],[qq],[msn],[blog],[create_user_id],[create_time],[modify_user_id],[modify_time],[user_status_desc],[fail_date],[user_name_en],[customer_id],[user_id])");
            strSql.Append(" values (");
            strSql.Append("@user_code,@user_name,@user_gender,@user_birthday,@user_password,@user_email,@user_identity,@user_telephone,@user_cellphone,@user_address,@user_postcode,@user_remark,@user_status,@qq,@msn,@blog,@create_user_id,@create_time,@modify_user_id,@modify_time,@user_status_desc,@fail_date,@user_name_en,@customer_id,@user_id)");

            string pkString = pEntity.user_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@user_code",SqlDbType.NVarChar),
					new SqlParameter("@user_name",SqlDbType.NVarChar),
					new SqlParameter("@user_gender",SqlDbType.NVarChar),
					new SqlParameter("@user_birthday",SqlDbType.NVarChar),
					new SqlParameter("@user_password",SqlDbType.NVarChar),
					new SqlParameter("@user_email",SqlDbType.NVarChar),
					new SqlParameter("@user_identity",SqlDbType.NVarChar),
					new SqlParameter("@user_telephone",SqlDbType.NVarChar),
					new SqlParameter("@user_cellphone",SqlDbType.NVarChar),
					new SqlParameter("@user_address",SqlDbType.NVarChar),
					new SqlParameter("@user_postcode",SqlDbType.NVarChar),
					new SqlParameter("@user_remark",SqlDbType.NVarChar),
					new SqlParameter("@user_status",SqlDbType.NVarChar),
					new SqlParameter("@qq",SqlDbType.NVarChar),
					new SqlParameter("@msn",SqlDbType.NVarChar),
					new SqlParameter("@blog",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@user_status_desc",SqlDbType.NVarChar),
					new SqlParameter("@fail_date",SqlDbType.NVarChar),
					new SqlParameter("@user_name_en",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@user_id",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.user_code;
            parameters[1].Value = pEntity.user_name;
            parameters[2].Value = pEntity.user_gender;
            parameters[3].Value = pEntity.user_birthday;
            parameters[4].Value = pEntity.user_password;
            parameters[5].Value = pEntity.user_email;
            parameters[6].Value = pEntity.user_identity;
            parameters[7].Value = pEntity.user_telephone;
            parameters[8].Value = pEntity.user_cellphone;
            parameters[9].Value = pEntity.user_address;
            parameters[10].Value = pEntity.user_postcode;
            parameters[11].Value = pEntity.user_remark;
            parameters[12].Value = pEntity.user_status;
            parameters[13].Value = pEntity.qq;
            parameters[14].Value = pEntity.msn;
            parameters[15].Value = pEntity.blog;
            parameters[16].Value = pEntity.create_user_id;
            parameters[17].Value = pEntity.create_time;
            parameters[18].Value = pEntity.modify_user_id;
            parameters[19].Value = pEntity.modify_time;
            parameters[20].Value = pEntity.user_status_desc;
            parameters[21].Value = pEntity.fail_date;
            parameters[22].Value = pEntity.user_name_en;
            parameters[23].Value = pEntity.customer_id;
            parameters[24].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.user_id = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_UserEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("select * from [T_User] where user_id='{0}' and IsDelete=0 ", id.ToString());
            sql.AppendFormat("select * from [T_User] where user_id='{0}' ", id.ToString());
            //读取数据
            T_UserEntity m = null;
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
        public T_UserEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_User] where isdelete=0");
            //读取数据
            List<T_UserEntity> list = new List<T_UserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_UserEntity m;
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
        public void Update(T_UserEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(T_UserEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.user_id == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            ////初始化固定字段
            //pEntity.LastUpdateTime = DateTime.Now;
            //pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            pEntity.modify_user_id = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_User] set ");
            if (pIsUpdateNullField || pEntity.user_code != null)
                strSql.Append("[user_code]=@user_code,");
            if (pIsUpdateNullField || pEntity.user_name != null)
                strSql.Append("[user_name]=@user_name,");
            if (pIsUpdateNullField || pEntity.user_gender != null)
                strSql.Append("[user_gender]=@user_gender,");
            if (pIsUpdateNullField || pEntity.user_birthday != null)
                strSql.Append("[user_birthday]=@user_birthday,");
            if (pIsUpdateNullField || pEntity.user_password != null)
                strSql.Append("[user_password]=@user_password,");
            if (pIsUpdateNullField || pEntity.user_email != null)
                strSql.Append("[user_email]=@user_email,");
            if (pIsUpdateNullField || pEntity.user_identity != null)
                strSql.Append("[user_identity]=@user_identity,");
            if (pIsUpdateNullField || pEntity.user_telephone != null)
                strSql.Append("[user_telephone]=@user_telephone,");
            if (pIsUpdateNullField || pEntity.user_cellphone != null)
                strSql.Append("[user_cellphone]=@user_cellphone,");
            if (pIsUpdateNullField || pEntity.user_address != null)
                strSql.Append("[user_address]=@user_address,");
            if (pIsUpdateNullField || pEntity.user_postcode != null)
                strSql.Append("[user_postcode]=@user_postcode,");
            if (pIsUpdateNullField || pEntity.user_remark != null)
                strSql.Append("[user_remark]=@user_remark,");
            if (pIsUpdateNullField || pEntity.user_status != null)
                strSql.Append("[user_status]=@user_status,");
            if (pIsUpdateNullField || pEntity.qq != null)
                strSql.Append("[qq]=@qq,");
            if (pIsUpdateNullField || pEntity.msn != null)
                strSql.Append("[msn]=@msn,");
            if (pIsUpdateNullField || pEntity.blog != null)
                strSql.Append("[blog]=@blog,");
            if (pIsUpdateNullField || pEntity.create_user_id != null)
                strSql.Append("[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.create_time != null)
                strSql.Append("[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id != null)
                strSql.Append("[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time != null)
                strSql.Append("[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.user_status_desc != null)
                strSql.Append("[user_status_desc]=@user_status_desc,");
            if (pIsUpdateNullField || pEntity.fail_date != null)
                strSql.Append("[fail_date]=@fail_date,");
            if (pIsUpdateNullField || pEntity.user_name_en != null)
                strSql.Append("[user_name_en]=@user_name_en,");
            if (pIsUpdateNullField || pEntity.customer_id != null)
                strSql.Append("[customer_id]=@customer_id");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where user_id=@user_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@user_code",SqlDbType.NVarChar),
					new SqlParameter("@user_name",SqlDbType.NVarChar),
					new SqlParameter("@user_gender",SqlDbType.NVarChar),
					new SqlParameter("@user_birthday",SqlDbType.NVarChar),
					new SqlParameter("@user_password",SqlDbType.NVarChar),
					new SqlParameter("@user_email",SqlDbType.NVarChar),
					new SqlParameter("@user_identity",SqlDbType.NVarChar),
					new SqlParameter("@user_telephone",SqlDbType.NVarChar),
					new SqlParameter("@user_cellphone",SqlDbType.NVarChar),
					new SqlParameter("@user_address",SqlDbType.NVarChar),
					new SqlParameter("@user_postcode",SqlDbType.NVarChar),
					new SqlParameter("@user_remark",SqlDbType.NVarChar),
					new SqlParameter("@user_status",SqlDbType.NVarChar),
					new SqlParameter("@qq",SqlDbType.NVarChar),
					new SqlParameter("@msn",SqlDbType.NVarChar),
					new SqlParameter("@blog",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@user_status_desc",SqlDbType.NVarChar),
					new SqlParameter("@fail_date",SqlDbType.NVarChar),
					new SqlParameter("@user_name_en",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@user_id",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.user_code;
            parameters[1].Value = pEntity.user_name;
            parameters[2].Value = pEntity.user_gender;
            parameters[3].Value = pEntity.user_birthday;
            parameters[4].Value = pEntity.user_password;
            parameters[5].Value = pEntity.user_email;
            parameters[6].Value = pEntity.user_identity;
            parameters[7].Value = pEntity.user_telephone;
            parameters[8].Value = pEntity.user_cellphone;
            parameters[9].Value = pEntity.user_address;
            parameters[10].Value = pEntity.user_postcode;
            parameters[11].Value = pEntity.user_remark;
            parameters[12].Value = pEntity.user_status;
            parameters[13].Value = pEntity.qq;
            parameters[14].Value = pEntity.msn;
            parameters[15].Value = pEntity.blog;
            parameters[16].Value = pEntity.create_user_id;
            parameters[17].Value = pEntity.create_time;
            parameters[18].Value = pEntity.modify_user_id;
            parameters[19].Value = pEntity.modify_time;
            parameters[20].Value = pEntity.user_status_desc;
            parameters[21].Value = pEntity.fail_date;
            parameters[22].Value = pEntity.user_name_en;
            parameters[23].Value = pEntity.customer_id;
            parameters[24].Value = pEntity.user_id;

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
        public void Update(T_UserEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(T_UserEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_UserEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_UserEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.user_id == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.user_id, pTran);
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
            sql.AppendLine("update [T_User] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where user_id=@user_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@user_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_UserEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.user_id == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.user_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_UserEntity[] pEntities)
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
            sql.AppendLine("update [T_User] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where user_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_UserEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("select * from [T_User] where isdelete=0 ");
            sql.AppendFormat("select * from [T_User] where 1=1 ");
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
            List<T_UserEntity> list = new List<T_UserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_UserEntity m;
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
        public PagedQueryResult<T_UserEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [user_id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_User] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_User] where isdelete=0 ");
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
            PagedQueryResult<T_UserEntity> result = new PagedQueryResult<T_UserEntity>();
            List<T_UserEntity> list = new List<T_UserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_UserEntity m;
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
        public T_UserEntity[] QueryByEntity(T_UserEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_UserEntity> PagedQueryByEntity(T_UserEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_UserEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.user_id != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_id", Value = pQueryEntity.user_id });
            if (pQueryEntity.user_code != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_code", Value = pQueryEntity.user_code });
            if (pQueryEntity.user_name != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_name", Value = pQueryEntity.user_name });
            if (pQueryEntity.user_gender != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_gender", Value = pQueryEntity.user_gender });
            if (pQueryEntity.user_birthday != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_birthday", Value = pQueryEntity.user_birthday });
            if (pQueryEntity.user_password != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_password", Value = pQueryEntity.user_password });
            if (pQueryEntity.user_email != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_email", Value = pQueryEntity.user_email });
            if (pQueryEntity.user_identity != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_identity", Value = pQueryEntity.user_identity });
            if (pQueryEntity.user_telephone != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_telephone", Value = pQueryEntity.user_telephone });
            if (pQueryEntity.user_cellphone != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_cellphone", Value = pQueryEntity.user_cellphone });
            if (pQueryEntity.user_address != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_address", Value = pQueryEntity.user_address });
            if (pQueryEntity.user_postcode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_postcode", Value = pQueryEntity.user_postcode });
            if (pQueryEntity.user_remark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_remark", Value = pQueryEntity.user_remark });
            if (pQueryEntity.user_status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_status", Value = pQueryEntity.user_status });
            if (pQueryEntity.qq != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "qq", Value = pQueryEntity.qq });
            if (pQueryEntity.msn != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "msn", Value = pQueryEntity.msn });
            if (pQueryEntity.blog != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "blog", Value = pQueryEntity.blog });
            if (pQueryEntity.create_user_id != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.create_time != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.modify_user_id != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.modify_time != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.user_status_desc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_status_desc", Value = pQueryEntity.user_status_desc });
            if (pQueryEntity.fail_date != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "fail_date", Value = pQueryEntity.fail_date });
            if (pQueryEntity.user_name_en != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "user_name_en", Value = pQueryEntity.user_name_en });
            if (pQueryEntity.customer_id != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_id", Value = pQueryEntity.customer_id });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out T_UserEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_UserEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["user_id"] != DBNull.Value)
            {
                pInstance.user_id = Convert.ToString(pReader["user_id"]);
            }
            if (pReader["user_code"] != DBNull.Value)
            {
                pInstance.user_code = Convert.ToString(pReader["user_code"]);
            }
            if (pReader["user_name"] != DBNull.Value)
            {
                pInstance.user_name = Convert.ToString(pReader["user_name"]);
            }
            if (pReader["user_gender"] != DBNull.Value)
            {
                pInstance.user_gender = Convert.ToString(pReader["user_gender"]);
            }
            if (pReader["user_birthday"] != DBNull.Value)
            {
                pInstance.user_birthday = Convert.ToString(pReader["user_birthday"]);
            }
            if (pReader["user_password"] != DBNull.Value)
            {
                pInstance.user_password = Convert.ToString(pReader["user_password"]);
            }
            if (pReader["user_email"] != DBNull.Value)
            {
                pInstance.user_email = Convert.ToString(pReader["user_email"]);
            }
            if (pReader["user_identity"] != DBNull.Value)
            {
                pInstance.user_identity = Convert.ToString(pReader["user_identity"]);
            }
            if (pReader["user_telephone"] != DBNull.Value)
            {
                pInstance.user_telephone = Convert.ToString(pReader["user_telephone"]);
            }
            if (pReader["user_cellphone"] != DBNull.Value)
            {
                pInstance.user_cellphone = Convert.ToString(pReader["user_cellphone"]);
            }
            if (pReader["user_address"] != DBNull.Value)
            {
                pInstance.user_address = Convert.ToString(pReader["user_address"]);
            }
            if (pReader["user_postcode"] != DBNull.Value)
            {
                pInstance.user_postcode = Convert.ToString(pReader["user_postcode"]);
            }
            if (pReader["user_remark"] != DBNull.Value)
            {
                pInstance.user_remark = Convert.ToString(pReader["user_remark"]);
            }
            if (pReader["user_status"] != DBNull.Value)
            {
                pInstance.user_status = Convert.ToString(pReader["user_status"]);
            }
            if (pReader["qq"] != DBNull.Value)
            {
                pInstance.qq = Convert.ToString(pReader["qq"]);
            }
            if (pReader["msn"] != DBNull.Value)
            {
                pInstance.msn = Convert.ToString(pReader["msn"]);
            }
            if (pReader["blog"] != DBNull.Value)
            {
                pInstance.blog = Convert.ToString(pReader["blog"]);
            }
            if (pReader["create_user_id"] != DBNull.Value)
            {
                pInstance.create_user_id = Convert.ToString(pReader["create_user_id"]);
            }
            if (pReader["create_time"] != DBNull.Value)
            {
                pInstance.create_time = Convert.ToString(pReader["create_time"]);
            }
            if (pReader["modify_user_id"] != DBNull.Value)
            {
                pInstance.modify_user_id = Convert.ToString(pReader["modify_user_id"]);
            }
            if (pReader["modify_time"] != DBNull.Value)
            {
                pInstance.modify_time = Convert.ToString(pReader["modify_time"]);
            }
            if (pReader["user_status_desc"] != DBNull.Value)
            {
                pInstance.user_status_desc = Convert.ToString(pReader["user_status_desc"]);
            }
            if (pReader["fail_date"] != DBNull.Value)
            {
                pInstance.fail_date = Convert.ToString(pReader["fail_date"]);
            }
            if (pReader["user_name_en"] != DBNull.Value)
            {
                pInstance.user_name_en = Convert.ToString(pReader["user_name_en"]);
            }
            if (pReader["customer_id"] != DBNull.Value)
            {
                pInstance.customer_id = Convert.ToString(pReader["customer_id"]);
            }

        }
        #endregion
    }
}
