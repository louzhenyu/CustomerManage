/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/17 14:26:19
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
    /// 表t_customer的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class t_customerDAO : Base.BaseCPOSDAO, ICRUDable<t_customerEntity>, IQueryable<t_customerEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public t_customerDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(t_customerEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(t_customerEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [t_customer](");
            strSql.Append("[customer_code],[customer_name],[customer_address],[customer_post_code],[customer_contacter],[customer_tel],[customer_fax],[customer_email],[customer_cell],[customer_memo],[customer_status],[create_user_id],[create_time],[modify_user_id],[modify_time],[sys_modify_stamp],[customer_name_en],[create_user_name],[modify_user_name],[customer_start_date],[is_approve],[customer_id])");
            strSql.Append(" values (");
            strSql.Append("@customer_code,@customer_name,@customer_address,@customer_post_code,@customer_contacter,@customer_tel,@customer_fax,@customer_email,@customer_cell,@customer_memo,@customer_status,@create_user_id,@create_time,@modify_user_id,@modify_time,@sys_modify_stamp,@customer_name_en,@create_user_name,@modify_user_name,@customer_start_date,@is_approve,@customer_id)");            

			string pkString = pEntity.customer_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@customer_code",SqlDbType.VarChar),
					new SqlParameter("@customer_name",SqlDbType.VarChar),
					new SqlParameter("@customer_address",SqlDbType.VarChar),
					new SqlParameter("@customer_post_code",SqlDbType.VarChar),
					new SqlParameter("@customer_contacter",SqlDbType.VarChar),
					new SqlParameter("@customer_tel",SqlDbType.VarChar),
					new SqlParameter("@customer_fax",SqlDbType.VarChar),
					new SqlParameter("@customer_email",SqlDbType.VarChar),
					new SqlParameter("@customer_cell",SqlDbType.VarChar),
					new SqlParameter("@customer_memo",SqlDbType.VarChar),
					new SqlParameter("@customer_status",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.VarChar),
					new SqlParameter("@create_time",SqlDbType.DateTime),
					new SqlParameter("@modify_user_id",SqlDbType.VarChar),
					new SqlParameter("@modify_time",SqlDbType.DateTime),
					new SqlParameter("@sys_modify_stamp",SqlDbType.DateTime),
					new SqlParameter("@customer_name_en",SqlDbType.VarChar),
					new SqlParameter("@create_user_name",SqlDbType.VarChar),
					new SqlParameter("@modify_user_name",SqlDbType.VarChar),
					new SqlParameter("@customer_start_date",SqlDbType.VarChar),
					new SqlParameter("@is_approve",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.customer_code;
			parameters[1].Value = pEntity.customer_name;
			parameters[2].Value = pEntity.customer_address;
			parameters[3].Value = pEntity.customer_post_code;
			parameters[4].Value = pEntity.customer_contacter;
			parameters[5].Value = pEntity.customer_tel;
			parameters[6].Value = pEntity.customer_fax;
			parameters[7].Value = pEntity.customer_email;
			parameters[8].Value = pEntity.customer_cell;
			parameters[9].Value = pEntity.customer_memo;
			parameters[10].Value = pEntity.customer_status;
			parameters[11].Value = pEntity.create_user_id;
			parameters[12].Value = pEntity.create_time;
			parameters[13].Value = pEntity.modify_user_id;
			parameters[14].Value = pEntity.modify_time;
			parameters[15].Value = pEntity.sys_modify_stamp;
			parameters[16].Value = pEntity.customer_name_en;
			parameters[17].Value = pEntity.create_user_name;
			parameters[18].Value = pEntity.modify_user_name;
			parameters[19].Value = pEntity.customer_start_date;
			parameters[20].Value = pEntity.is_approve;
			parameters[21].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.customer_id = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public t_customerEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where customer_id='{0}'  ", id.ToString());
            //读取数据
            t_customerEntity m = null;
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
        public t_customerEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where 1=1 ");
            //读取数据
            List<t_customerEntity> list = new List<t_customerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    t_customerEntity m;
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
        public void Update(t_customerEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(t_customerEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.customer_id == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [t_customer] set ");
                        if (pIsUpdateNullField || pEntity.customer_code!=null)
                strSql.Append( "[customer_code]=@customer_code,");
            if (pIsUpdateNullField || pEntity.customer_name!=null)
                strSql.Append( "[customer_name]=@customer_name,");
            if (pIsUpdateNullField || pEntity.customer_address!=null)
                strSql.Append( "[customer_address]=@customer_address,");
            if (pIsUpdateNullField || pEntity.customer_post_code!=null)
                strSql.Append( "[customer_post_code]=@customer_post_code,");
            if (pIsUpdateNullField || pEntity.customer_contacter!=null)
                strSql.Append( "[customer_contacter]=@customer_contacter,");
            if (pIsUpdateNullField || pEntity.customer_tel!=null)
                strSql.Append( "[customer_tel]=@customer_tel,");
            if (pIsUpdateNullField || pEntity.customer_fax!=null)
                strSql.Append( "[customer_fax]=@customer_fax,");
            if (pIsUpdateNullField || pEntity.customer_email!=null)
                strSql.Append( "[customer_email]=@customer_email,");
            if (pIsUpdateNullField || pEntity.customer_cell!=null)
                strSql.Append( "[customer_cell]=@customer_cell,");
            if (pIsUpdateNullField || pEntity.customer_memo!=null)
                strSql.Append( "[customer_memo]=@customer_memo,");
            if (pIsUpdateNullField || pEntity.customer_status!=null)
                strSql.Append( "[customer_status]=@customer_status,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.sys_modify_stamp!=null)
                strSql.Append( "[sys_modify_stamp]=@sys_modify_stamp,");
            if (pIsUpdateNullField || pEntity.customer_name_en!=null)
                strSql.Append( "[customer_name_en]=@customer_name_en,");
            if (pIsUpdateNullField || pEntity.create_user_name!=null)
                strSql.Append( "[create_user_name]=@create_user_name,");
            if (pIsUpdateNullField || pEntity.modify_user_name!=null)
                strSql.Append( "[modify_user_name]=@modify_user_name,");
            if (pIsUpdateNullField || pEntity.customer_start_date!=null)
                strSql.Append( "[customer_start_date]=@customer_start_date,");
            if (pIsUpdateNullField || pEntity.is_approve!=null)
                strSql.Append( "[is_approve]=@is_approve");
            strSql.Append(" where customer_id=@customer_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@customer_code",SqlDbType.VarChar),
					new SqlParameter("@customer_name",SqlDbType.VarChar),
					new SqlParameter("@customer_address",SqlDbType.VarChar),
					new SqlParameter("@customer_post_code",SqlDbType.VarChar),
					new SqlParameter("@customer_contacter",SqlDbType.VarChar),
					new SqlParameter("@customer_tel",SqlDbType.VarChar),
					new SqlParameter("@customer_fax",SqlDbType.VarChar),
					new SqlParameter("@customer_email",SqlDbType.VarChar),
					new SqlParameter("@customer_cell",SqlDbType.VarChar),
					new SqlParameter("@customer_memo",SqlDbType.VarChar),
					new SqlParameter("@customer_status",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.VarChar),
					new SqlParameter("@create_time",SqlDbType.DateTime),
					new SqlParameter("@modify_user_id",SqlDbType.VarChar),
					new SqlParameter("@modify_time",SqlDbType.DateTime),
					new SqlParameter("@sys_modify_stamp",SqlDbType.DateTime),
					new SqlParameter("@customer_name_en",SqlDbType.VarChar),
					new SqlParameter("@create_user_name",SqlDbType.VarChar),
					new SqlParameter("@modify_user_name",SqlDbType.VarChar),
					new SqlParameter("@customer_start_date",SqlDbType.VarChar),
					new SqlParameter("@is_approve",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.customer_code;
			parameters[1].Value = pEntity.customer_name;
			parameters[2].Value = pEntity.customer_address;
			parameters[3].Value = pEntity.customer_post_code;
			parameters[4].Value = pEntity.customer_contacter;
			parameters[5].Value = pEntity.customer_tel;
			parameters[6].Value = pEntity.customer_fax;
			parameters[7].Value = pEntity.customer_email;
			parameters[8].Value = pEntity.customer_cell;
			parameters[9].Value = pEntity.customer_memo;
			parameters[10].Value = pEntity.customer_status;
			parameters[11].Value = pEntity.create_user_id;
			parameters[12].Value = pEntity.create_time;
			parameters[13].Value = pEntity.modify_user_id;
			parameters[14].Value = pEntity.modify_time;
			parameters[15].Value = pEntity.sys_modify_stamp;
			parameters[16].Value = pEntity.customer_name_en;
			parameters[17].Value = pEntity.create_user_name;
			parameters[18].Value = pEntity.modify_user_name;
			parameters[19].Value = pEntity.customer_start_date;
			parameters[20].Value = pEntity.is_approve;
			parameters[21].Value = pEntity.customer_id;

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
        public void Update(t_customerEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(t_customerEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(t_customerEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.customer_id == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.customer_id, pTran);           
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
            sql.AppendLine("update [t_customer] set  where customer_id=@customer_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@customer_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(t_customerEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.customer_id == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.customer_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(t_customerEntity[] pEntities)
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
            sql.AppendLine("update [t_customer] set  where customer_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public t_customerEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where 1=1  ");
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
            List<t_customerEntity> list = new List<t_customerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    t_customerEntity m;
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
        public PagedQueryResult<t_customerEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [customer_id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [t_customer] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [t_customer] where 1=1  ");
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
            PagedQueryResult<t_customerEntity> result = new PagedQueryResult<t_customerEntity>();
            List<t_customerEntity> list = new List<t_customerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    t_customerEntity m;
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
        public t_customerEntity[] QueryByEntity(t_customerEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<t_customerEntity> PagedQueryByEntity(t_customerEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(t_customerEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.customer_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_id", Value = pQueryEntity.customer_id });
            if (pQueryEntity.customer_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_code", Value = pQueryEntity.customer_code });
            if (pQueryEntity.customer_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_name", Value = pQueryEntity.customer_name });
            if (pQueryEntity.customer_address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_address", Value = pQueryEntity.customer_address });
            if (pQueryEntity.customer_post_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_post_code", Value = pQueryEntity.customer_post_code });
            if (pQueryEntity.customer_contacter!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_contacter", Value = pQueryEntity.customer_contacter });
            if (pQueryEntity.customer_tel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_tel", Value = pQueryEntity.customer_tel });
            if (pQueryEntity.customer_fax!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_fax", Value = pQueryEntity.customer_fax });
            if (pQueryEntity.customer_email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_email", Value = pQueryEntity.customer_email });
            if (pQueryEntity.customer_cell!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_cell", Value = pQueryEntity.customer_cell });
            if (pQueryEntity.customer_memo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_memo", Value = pQueryEntity.customer_memo });
            if (pQueryEntity.customer_status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_status", Value = pQueryEntity.customer_status });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.sys_modify_stamp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sys_modify_stamp", Value = pQueryEntity.sys_modify_stamp });
            if (pQueryEntity.customer_name_en!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_name_en", Value = pQueryEntity.customer_name_en });
            if (pQueryEntity.create_user_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_name", Value = pQueryEntity.create_user_name });
            if (pQueryEntity.modify_user_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_name", Value = pQueryEntity.modify_user_name });
            if (pQueryEntity.customer_start_date!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_start_date", Value = pQueryEntity.customer_start_date });
            if (pQueryEntity.is_approve!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "is_approve", Value = pQueryEntity.is_approve });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out t_customerEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new t_customerEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["customer_id"] != DBNull.Value)
			{
				pInstance.customer_id =  Convert.ToString(pReader["customer_id"]);
			}
			if (pReader["customer_code"] != DBNull.Value)
			{
				pInstance.customer_code =  Convert.ToString(pReader["customer_code"]);
			}
			if (pReader["customer_name"] != DBNull.Value)
			{
				pInstance.customer_name =  Convert.ToString(pReader["customer_name"]);
			}
			if (pReader["customer_address"] != DBNull.Value)
			{
				pInstance.customer_address =  Convert.ToString(pReader["customer_address"]);
			}
			if (pReader["customer_post_code"] != DBNull.Value)
			{
				pInstance.customer_post_code =  Convert.ToString(pReader["customer_post_code"]);
			}
			if (pReader["customer_contacter"] != DBNull.Value)
			{
				pInstance.customer_contacter =  Convert.ToString(pReader["customer_contacter"]);
			}
			if (pReader["customer_tel"] != DBNull.Value)
			{
				pInstance.customer_tel =  Convert.ToString(pReader["customer_tel"]);
			}
			if (pReader["customer_fax"] != DBNull.Value)
			{
				pInstance.customer_fax =  Convert.ToString(pReader["customer_fax"]);
			}
			if (pReader["customer_email"] != DBNull.Value)
			{
				pInstance.customer_email =  Convert.ToString(pReader["customer_email"]);
			}
			if (pReader["customer_cell"] != DBNull.Value)
			{
				pInstance.customer_cell =  Convert.ToString(pReader["customer_cell"]);
			}
			if (pReader["customer_memo"] != DBNull.Value)
			{
				pInstance.customer_memo =  Convert.ToString(pReader["customer_memo"]);
			}
			if (pReader["customer_status"] != DBNull.Value)
			{
				pInstance.customer_status =   Convert.ToInt32(pReader["customer_status"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToDateTime(pReader["create_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToDateTime(pReader["modify_time"]);
			}
			if (pReader["sys_modify_stamp"] != DBNull.Value)
			{
				pInstance.sys_modify_stamp =  Convert.ToDateTime(pReader["sys_modify_stamp"]);
			}
			if (pReader["customer_name_en"] != DBNull.Value)
			{
				pInstance.customer_name_en =  Convert.ToString(pReader["customer_name_en"]);
			}
			if (pReader["create_user_name"] != DBNull.Value)
			{
				pInstance.create_user_name =  Convert.ToString(pReader["create_user_name"]);
			}
			if (pReader["modify_user_name"] != DBNull.Value)
			{
				pInstance.modify_user_name =  Convert.ToString(pReader["modify_user_name"]);
			}
			if (pReader["customer_start_date"] != DBNull.Value)
			{
				pInstance.customer_start_date =  Convert.ToString(pReader["customer_start_date"]);
			}
			if (pReader["is_approve"] != DBNull.Value)
			{
				pInstance.is_approve =  Convert.ToString(pReader["is_approve"]);
			}

        }
        #endregion
    }
}
