/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-6 16:02:58
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
    /// 表T_Item的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_ItemDAO : Base.BaseCPOSDAO, ICRUDable<T_ItemEntity>, IQueryable<T_ItemEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_ItemDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_ItemEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_ItemEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Item](");
            strSql.Append("[item_category_id],[item_code],[item_name],[item_name_en],[item_name_short],[pyzjm],[item_remark],[status],[status_desc],[create_user_id],[create_time],[modify_user_id],[modify_time],[bat_id],[if_flag],[ifgifts],[ifoften],[ifservice],[IsGB],[data_from],[display_index],[imageUrl],[CustomerId],[item_id])");
            strSql.Append(" values (");
            strSql.Append("@item_category_id,@item_code,@item_name,@item_name_en,@item_name_short,@pyzjm,@item_remark,@status,@status_desc,@create_user_id,@create_time,@modify_user_id,@modify_time,@bat_id,@if_flag,@ifgifts,@ifoften,@ifservice,@IsGB,@data_from,@display_index,@imageUrl,@CustomerId,@item_id)");            

			string pkString = pEntity.item_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@item_category_id",SqlDbType.NVarChar),
					new SqlParameter("@item_code",SqlDbType.NVarChar),
					new SqlParameter("@item_name",SqlDbType.NVarChar),
					new SqlParameter("@item_name_en",SqlDbType.NVarChar),
					new SqlParameter("@item_name_short",SqlDbType.NVarChar),
					new SqlParameter("@pyzjm",SqlDbType.NVarChar),
					new SqlParameter("@item_remark",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.NVarChar),
					new SqlParameter("@status_desc",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@ifgifts",SqlDbType.Int),
					new SqlParameter("@ifoften",SqlDbType.Int),
					new SqlParameter("@ifservice",SqlDbType.Int),
					new SqlParameter("@IsGB",SqlDbType.Int),
					new SqlParameter("@data_from",SqlDbType.NVarChar),
					new SqlParameter("@display_index",SqlDbType.Int),
					new SqlParameter("@imageUrl",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@item_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.item_category_id;
			parameters[1].Value = pEntity.item_code;
			parameters[2].Value = pEntity.item_name;
			parameters[3].Value = pEntity.item_name_en;
			parameters[4].Value = pEntity.item_name_short;
			parameters[5].Value = pEntity.pyzjm;
			parameters[6].Value = pEntity.item_remark;
			parameters[7].Value = pEntity.status;
			parameters[8].Value = pEntity.status_desc;
			parameters[9].Value = pEntity.create_user_id;
			parameters[10].Value = pEntity.create_time;
			parameters[11].Value = pEntity.modify_user_id;
			parameters[12].Value = pEntity.modify_time;
			parameters[13].Value = pEntity.bat_id;
			parameters[14].Value = pEntity.if_flag;
			parameters[15].Value = pEntity.ifgifts;
			parameters[16].Value = pEntity.ifoften;
			parameters[17].Value = pEntity.ifservice;
			parameters[18].Value = pEntity.IsGB;
			parameters[19].Value = pEntity.data_from;
			parameters[20].Value = pEntity.display_index;
			parameters[21].Value = pEntity.imageUrl;
			parameters[22].Value = pEntity.CustomerId;
			parameters[23].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.item_id = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_ItemEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Item] where item_id='{0}'  and status<>'-1' ", id.ToString());
            //读取数据
            T_ItemEntity m = null;
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
        public T_ItemEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Item] where 1=1  and status<>'-1'");
            //读取数据
            List<T_ItemEntity> list = new List<T_ItemEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_ItemEntity m;
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
        public void Update(T_ItemEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_ItemEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.item_id == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Item] set ");
                        if (pIsUpdateNullField || pEntity.item_category_id!=null)
                strSql.Append( "[item_category_id]=@item_category_id,");
            if (pIsUpdateNullField || pEntity.item_code!=null)
                strSql.Append( "[item_code]=@item_code,");
            if (pIsUpdateNullField || pEntity.item_name!=null)
                strSql.Append( "[item_name]=@item_name,");
            if (pIsUpdateNullField || pEntity.item_name_en!=null)
                strSql.Append( "[item_name_en]=@item_name_en,");
            if (pIsUpdateNullField || pEntity.item_name_short!=null)
                strSql.Append( "[item_name_short]=@item_name_short,");
            if (pIsUpdateNullField || pEntity.pyzjm!=null)
                strSql.Append( "[pyzjm]=@pyzjm,");
            if (pIsUpdateNullField || pEntity.item_remark!=null)
                strSql.Append( "[item_remark]=@item_remark,");
            if (pIsUpdateNullField || pEntity.status!=null)
                strSql.Append( "[status]=@status,");
            if (pIsUpdateNullField || pEntity.status_desc!=null)
                strSql.Append( "[status_desc]=@status_desc,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.bat_id!=null)
                strSql.Append( "[bat_id]=@bat_id,");
            if (pIsUpdateNullField || pEntity.if_flag!=null)
                strSql.Append( "[if_flag]=@if_flag,");
            if (pIsUpdateNullField || pEntity.ifgifts!=null)
                strSql.Append( "[ifgifts]=@ifgifts,");
            if (pIsUpdateNullField || pEntity.ifoften!=null)
                strSql.Append( "[ifoften]=@ifoften,");
            if (pIsUpdateNullField || pEntity.ifservice!=null)
                strSql.Append( "[ifservice]=@ifservice,");
            if (pIsUpdateNullField || pEntity.IsGB!=null)
                strSql.Append( "[IsGB]=@IsGB,");
            if (pIsUpdateNullField || pEntity.data_from!=null)
                strSql.Append( "[data_from]=@data_from,");
            if (pIsUpdateNullField || pEntity.display_index!=null)
                strSql.Append( "[display_index]=@display_index,");
            if (pIsUpdateNullField || pEntity.imageUrl!=null)
                strSql.Append( "[imageUrl]=@imageUrl,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where item_id=@item_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@item_category_id",SqlDbType.NVarChar),
					new SqlParameter("@item_code",SqlDbType.NVarChar),
					new SqlParameter("@item_name",SqlDbType.NVarChar),
					new SqlParameter("@item_name_en",SqlDbType.NVarChar),
					new SqlParameter("@item_name_short",SqlDbType.NVarChar),
					new SqlParameter("@pyzjm",SqlDbType.NVarChar),
					new SqlParameter("@item_remark",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.NVarChar),
					new SqlParameter("@status_desc",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@ifgifts",SqlDbType.Int),
					new SqlParameter("@ifoften",SqlDbType.Int),
					new SqlParameter("@ifservice",SqlDbType.Int),
					new SqlParameter("@IsGB",SqlDbType.Int),
					new SqlParameter("@data_from",SqlDbType.NVarChar),
					new SqlParameter("@display_index",SqlDbType.Int),
					new SqlParameter("@imageUrl",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@item_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.item_category_id;
			parameters[1].Value = pEntity.item_code;
			parameters[2].Value = pEntity.item_name;
			parameters[3].Value = pEntity.item_name_en;
			parameters[4].Value = pEntity.item_name_short;
			parameters[5].Value = pEntity.pyzjm;
			parameters[6].Value = pEntity.item_remark;
			parameters[7].Value = pEntity.status;
			parameters[8].Value = pEntity.status_desc;
			parameters[9].Value = pEntity.create_user_id;
			parameters[10].Value = pEntity.create_time;
			parameters[11].Value = pEntity.modify_user_id;
			parameters[12].Value = pEntity.modify_time;
			parameters[13].Value = pEntity.bat_id;
			parameters[14].Value = pEntity.if_flag;
			parameters[15].Value = pEntity.ifgifts;
			parameters[16].Value = pEntity.ifoften;
			parameters[17].Value = pEntity.ifservice;
			parameters[18].Value = pEntity.IsGB;
			parameters[19].Value = pEntity.data_from;
			parameters[20].Value = pEntity.display_index;
			parameters[21].Value = pEntity.imageUrl;
			parameters[22].Value = pEntity.CustomerId;
			parameters[23].Value = pEntity.item_id;

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
        public void Update(T_ItemEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_ItemEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_ItemEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.item_id == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.item_id, pTran);           
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
            sql.AppendLine("update [T_Item] set status='-1' where item_id=@item_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@item_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_ItemEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.item_id == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.item_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_ItemEntity[] pEntities)
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
            sql.AppendLine("update [T_Item] set status='-1' where item_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_ItemEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Item] where 1=1  and status<>'-1' ");
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
            List<T_ItemEntity> list = new List<T_ItemEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_ItemEntity m;
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
        public PagedQueryResult<T_ItemEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [item_id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Item] where 1=1  and status<>'-1' ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_Item] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<T_ItemEntity> result = new PagedQueryResult<T_ItemEntity>();
            List<T_ItemEntity> list = new List<T_ItemEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_ItemEntity m;
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
        public T_ItemEntity[] QueryByEntity(T_ItemEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_ItemEntity> PagedQueryByEntity(T_ItemEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_ItemEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.item_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_id", Value = pQueryEntity.item_id });
            if (pQueryEntity.item_category_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_category_id", Value = pQueryEntity.item_category_id });
            if (pQueryEntity.item_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_code", Value = pQueryEntity.item_code });
            if (pQueryEntity.item_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_name", Value = pQueryEntity.item_name });
            if (pQueryEntity.item_name_en!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_name_en", Value = pQueryEntity.item_name_en });
            if (pQueryEntity.item_name_short!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_name_short", Value = pQueryEntity.item_name_short });
            if (pQueryEntity.pyzjm!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "pyzjm", Value = pQueryEntity.pyzjm });
            if (pQueryEntity.item_remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_remark", Value = pQueryEntity.item_remark });
            if (pQueryEntity.status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status", Value = pQueryEntity.status });
            if (pQueryEntity.status_desc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status_desc", Value = pQueryEntity.status_desc });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.bat_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "bat_id", Value = pQueryEntity.bat_id });
            if (pQueryEntity.if_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "if_flag", Value = pQueryEntity.if_flag });
            if (pQueryEntity.ifgifts!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ifgifts", Value = pQueryEntity.ifgifts });
            if (pQueryEntity.ifoften!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ifoften", Value = pQueryEntity.ifoften });
            if (pQueryEntity.ifservice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ifservice", Value = pQueryEntity.ifservice });
            if (pQueryEntity.IsGB!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsGB", Value = pQueryEntity.IsGB });
            if (pQueryEntity.data_from!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "data_from", Value = pQueryEntity.data_from });
            if (pQueryEntity.display_index!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "display_index", Value = pQueryEntity.display_index });
            if (pQueryEntity.imageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "imageUrl", Value = pQueryEntity.imageUrl });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_ItemEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_ItemEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["item_id"] != DBNull.Value)
			{
				pInstance.item_id =  Convert.ToString(pReader["item_id"]);
			}
			if (pReader["item_category_id"] != DBNull.Value)
			{
				pInstance.item_category_id =  Convert.ToString(pReader["item_category_id"]);
			}
			if (pReader["item_code"] != DBNull.Value)
			{
				pInstance.item_code =  Convert.ToString(pReader["item_code"]);
			}
			if (pReader["item_name"] != DBNull.Value)
			{
				pInstance.item_name =  Convert.ToString(pReader["item_name"]);
			}
			if (pReader["item_name_en"] != DBNull.Value)
			{
				pInstance.item_name_en =  Convert.ToString(pReader["item_name_en"]);
			}
			if (pReader["item_name_short"] != DBNull.Value)
			{
				pInstance.item_name_short =  Convert.ToString(pReader["item_name_short"]);
			}
			if (pReader["pyzjm"] != DBNull.Value)
			{
				pInstance.pyzjm =  Convert.ToString(pReader["pyzjm"]);
			}
			if (pReader["item_remark"] != DBNull.Value)
			{
				pInstance.item_remark =  Convert.ToString(pReader["item_remark"]);
			}
			if (pReader["status"] != DBNull.Value)
			{
				pInstance.status =  Convert.ToString(pReader["status"]);
			}
			if (pReader["status_desc"] != DBNull.Value)
			{
				pInstance.status_desc =  Convert.ToString(pReader["status_desc"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToString(pReader["modify_time"]);
			}
			if (pReader["bat_id"] != DBNull.Value)
			{
				pInstance.bat_id =  Convert.ToString(pReader["bat_id"]);
			}
			if (pReader["if_flag"] != DBNull.Value)
			{
				pInstance.if_flag =  Convert.ToString(pReader["if_flag"]);
			}
			if (pReader["ifgifts"] != DBNull.Value)
			{
				pInstance.ifgifts =   Convert.ToInt32(pReader["ifgifts"]);
			}
			if (pReader["ifoften"] != DBNull.Value)
			{
				pInstance.ifoften =   Convert.ToInt32(pReader["ifoften"]);
			}
			if (pReader["ifservice"] != DBNull.Value)
			{
				pInstance.ifservice =   Convert.ToInt32(pReader["ifservice"]);
			}
			if (pReader["IsGB"] != DBNull.Value)
			{
				pInstance.IsGB =   Convert.ToInt32(pReader["IsGB"]);
			}
			if (pReader["data_from"] != DBNull.Value)
			{
				pInstance.data_from =  Convert.ToString(pReader["data_from"]);
			}
			if (pReader["display_index"] != DBNull.Value)
			{
				pInstance.display_index =   Convert.ToInt32(pReader["display_index"]);
			}
			if (pReader["imageUrl"] != DBNull.Value)
			{
				pInstance.imageUrl =  Convert.ToString(pReader["imageUrl"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
