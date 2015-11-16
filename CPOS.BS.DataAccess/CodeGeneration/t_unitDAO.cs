/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-07-01 11:14:15
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
    /// 表t_unit的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class t_unitDAO : Base.BaseCPOSDAO, ICRUDable<t_unitEntity>, IQueryable<t_unitEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public t_unitDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(t_unitEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(t_unitEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [t_unit](");
            strSql.Append("[type_id],[unit_code],[unit_name],[unit_name_en],[unit_name_short],[unit_city_id],[unit_address],[unit_contact],[unit_tel],[unit_fax],[unit_email],[unit_postcode],[unit_remark],[Status],[unit_flag],[CUSTOMER_LEVEL],[create_user_id],[create_time],[modify_user_id],[modify_time],[status_desc],[bat_id],[if_flag],[customer_id],[longitude],[dimension],[imageURL],[ftpImagerURL],[webserversURL],[weiXinId],[dimensionalCodeURL],[BizHoursStarttime],[BizHoursEndtime],[StoreType],[unit_id])");
            strSql.Append(" values (");
            strSql.Append("@type_id,@unit_code,@unit_name,@unit_name_en,@unit_name_short,@unit_city_id,@unit_address,@unit_contact,@unit_tel,@unit_fax,@unit_email,@unit_postcode,@unit_remark,@Status,@unit_flag,@CUSTOMER_LEVEL,@create_user_id,@create_time,@modify_user_id,@modify_time,@status_desc,@bat_id,@if_flag,@customer_id,@longitude,@dimension,@imageURL,@ftpImagerURL,@webserversURL,@weiXinId,@dimensionalCodeURL,@BizHoursStarttime,@BizHoursEndtime,@StoreType,@unit_id)");            

			string pkString = pEntity.unit_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@type_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_code",SqlDbType.NVarChar),
					new SqlParameter("@unit_name",SqlDbType.NVarChar),
					new SqlParameter("@unit_name_en",SqlDbType.NVarChar),
					new SqlParameter("@unit_name_short",SqlDbType.NVarChar),
					new SqlParameter("@unit_city_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_address",SqlDbType.NVarChar),
					new SqlParameter("@unit_contact",SqlDbType.NVarChar),
					new SqlParameter("@unit_tel",SqlDbType.NVarChar),
					new SqlParameter("@unit_fax",SqlDbType.NVarChar),
					new SqlParameter("@unit_email",SqlDbType.NVarChar),
					new SqlParameter("@unit_postcode",SqlDbType.NVarChar),
					new SqlParameter("@unit_remark",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@unit_flag",SqlDbType.NVarChar),
					new SqlParameter("@CUSTOMER_LEVEL",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@status_desc",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@longitude",SqlDbType.NVarChar),
					new SqlParameter("@dimension",SqlDbType.NVarChar),
					new SqlParameter("@imageURL",SqlDbType.NVarChar),
					new SqlParameter("@ftpImagerURL",SqlDbType.NVarChar),
					new SqlParameter("@webserversURL",SqlDbType.NVarChar),
					new SqlParameter("@weiXinId",SqlDbType.NVarChar),
					new SqlParameter("@dimensionalCodeURL",SqlDbType.NVarChar),
					new SqlParameter("@BizHoursStarttime",SqlDbType.NVarChar),
					new SqlParameter("@BizHoursEndtime",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@unit_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.type_id;
			parameters[1].Value = pEntity.unit_code;
			parameters[2].Value = pEntity.unit_name;
			parameters[3].Value = pEntity.unit_name_en;
			parameters[4].Value = pEntity.unit_name_short;
			parameters[5].Value = pEntity.unit_city_id;
			parameters[6].Value = pEntity.unit_address;
			parameters[7].Value = pEntity.unit_contact;
			parameters[8].Value = pEntity.unit_tel;
			parameters[9].Value = pEntity.unit_fax;
			parameters[10].Value = pEntity.unit_email;
			parameters[11].Value = pEntity.unit_postcode;
			parameters[12].Value = pEntity.unit_remark;
			parameters[13].Value = pEntity.Status;
			parameters[14].Value = pEntity.unit_flag;
			parameters[15].Value = pEntity.CUSTOMER_LEVEL;
			parameters[16].Value = pEntity.create_user_id;
			parameters[17].Value = pEntity.create_time;
			parameters[18].Value = pEntity.modify_user_id;
			parameters[19].Value = pEntity.modify_time;
			parameters[20].Value = pEntity.status_desc;
			parameters[21].Value = pEntity.bat_id;
			parameters[22].Value = pEntity.if_flag;
			parameters[23].Value = pEntity.customer_id;
			parameters[24].Value = pEntity.longitude;
			parameters[25].Value = pEntity.dimension;
			parameters[26].Value = pEntity.imageURL;
			parameters[27].Value = pEntity.ftpImagerURL;
			parameters[28].Value = pEntity.webserversURL;
			parameters[29].Value = pEntity.weiXinId;
			parameters[30].Value = pEntity.dimensionalCodeURL;
			parameters[31].Value = pEntity.BizHoursStarttime;
			parameters[32].Value = pEntity.BizHoursEndtime;
			parameters[33].Value = pEntity.StoreType;
			parameters[34].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.unit_id = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public t_unitEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_unit] where unit_id='{0}'  and status<>'-1' ", id.ToString());
            //读取数据
            t_unitEntity m = null;
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
        public t_unitEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_unit] where 1=1  and status<>'-1'");
            //读取数据
            List<t_unitEntity> list = new List<t_unitEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    t_unitEntity m;
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
        public void Update(t_unitEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(t_unitEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.unit_id == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [t_unit] set ");
                        if (pIsUpdateNullField || pEntity.type_id!=null)
                strSql.Append( "[type_id]=@type_id,");
            if (pIsUpdateNullField || pEntity.unit_code!=null)
                strSql.Append( "[unit_code]=@unit_code,");
            if (pIsUpdateNullField || pEntity.unit_name!=null)
                strSql.Append( "[unit_name]=@unit_name,");
            if (pIsUpdateNullField || pEntity.unit_name_en!=null)
                strSql.Append( "[unit_name_en]=@unit_name_en,");
            if (pIsUpdateNullField || pEntity.unit_name_short!=null)
                strSql.Append( "[unit_name_short]=@unit_name_short,");
            if (pIsUpdateNullField || pEntity.unit_city_id!=null)
                strSql.Append( "[unit_city_id]=@unit_city_id,");
            if (pIsUpdateNullField || pEntity.unit_address!=null)
                strSql.Append( "[unit_address]=@unit_address,");
            if (pIsUpdateNullField || pEntity.unit_contact!=null)
                strSql.Append( "[unit_contact]=@unit_contact,");
            if (pIsUpdateNullField || pEntity.unit_tel!=null)
                strSql.Append( "[unit_tel]=@unit_tel,");
            if (pIsUpdateNullField || pEntity.unit_fax!=null)
                strSql.Append( "[unit_fax]=@unit_fax,");
            if (pIsUpdateNullField || pEntity.unit_email!=null)
                strSql.Append( "[unit_email]=@unit_email,");
            if (pIsUpdateNullField || pEntity.unit_postcode!=null)
                strSql.Append( "[unit_postcode]=@unit_postcode,");
            if (pIsUpdateNullField || pEntity.unit_remark!=null)
                strSql.Append( "[unit_remark]=@unit_remark,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.unit_flag!=null)
                strSql.Append( "[unit_flag]=@unit_flag,");
            if (pIsUpdateNullField || pEntity.CUSTOMER_LEVEL!=null)
                strSql.Append( "[CUSTOMER_LEVEL]=@CUSTOMER_LEVEL,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.status_desc!=null)
                strSql.Append( "[status_desc]=@status_desc,");
            if (pIsUpdateNullField || pEntity.bat_id!=null)
                strSql.Append( "[bat_id]=@bat_id,");
            if (pIsUpdateNullField || pEntity.if_flag!=null)
                strSql.Append( "[if_flag]=@if_flag,");
            if (pIsUpdateNullField || pEntity.customer_id!=null)
                strSql.Append( "[customer_id]=@customer_id,");
            if (pIsUpdateNullField || pEntity.longitude!=null)
                strSql.Append( "[longitude]=@longitude,");
            if (pIsUpdateNullField || pEntity.dimension!=null)
                strSql.Append( "[dimension]=@dimension,");
            if (pIsUpdateNullField || pEntity.imageURL!=null)
                strSql.Append( "[imageURL]=@imageURL,");
            if (pIsUpdateNullField || pEntity.ftpImagerURL!=null)
                strSql.Append( "[ftpImagerURL]=@ftpImagerURL,");
            if (pIsUpdateNullField || pEntity.webserversURL!=null)
                strSql.Append( "[webserversURL]=@webserversURL,");
            if (pIsUpdateNullField || pEntity.weiXinId!=null)
                strSql.Append( "[weiXinId]=@weiXinId,");
            if (pIsUpdateNullField || pEntity.dimensionalCodeURL!=null)
                strSql.Append( "[dimensionalCodeURL]=@dimensionalCodeURL,");
            if (pIsUpdateNullField || pEntity.BizHoursStarttime!=null)
                strSql.Append( "[BizHoursStarttime]=@BizHoursStarttime,");
            if (pIsUpdateNullField || pEntity.BizHoursEndtime!=null)
                strSql.Append( "[BizHoursEndtime]=@BizHoursEndtime,");
            if (pIsUpdateNullField || pEntity.StoreType!=null)
                strSql.Append( "[StoreType]=@StoreType");
            strSql.Append(" where unit_id=@unit_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@type_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_code",SqlDbType.NVarChar),
					new SqlParameter("@unit_name",SqlDbType.NVarChar),
					new SqlParameter("@unit_name_en",SqlDbType.NVarChar),
					new SqlParameter("@unit_name_short",SqlDbType.NVarChar),
					new SqlParameter("@unit_city_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_address",SqlDbType.NVarChar),
					new SqlParameter("@unit_contact",SqlDbType.NVarChar),
					new SqlParameter("@unit_tel",SqlDbType.NVarChar),
					new SqlParameter("@unit_fax",SqlDbType.NVarChar),
					new SqlParameter("@unit_email",SqlDbType.NVarChar),
					new SqlParameter("@unit_postcode",SqlDbType.NVarChar),
					new SqlParameter("@unit_remark",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@unit_flag",SqlDbType.NVarChar),
					new SqlParameter("@CUSTOMER_LEVEL",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@status_desc",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@longitude",SqlDbType.NVarChar),
					new SqlParameter("@dimension",SqlDbType.NVarChar),
					new SqlParameter("@imageURL",SqlDbType.NVarChar),
					new SqlParameter("@ftpImagerURL",SqlDbType.NVarChar),
					new SqlParameter("@webserversURL",SqlDbType.NVarChar),
					new SqlParameter("@weiXinId",SqlDbType.NVarChar),
					new SqlParameter("@dimensionalCodeURL",SqlDbType.NVarChar),
					new SqlParameter("@BizHoursStarttime",SqlDbType.NVarChar),
					new SqlParameter("@BizHoursEndtime",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@unit_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.type_id;
			parameters[1].Value = pEntity.unit_code;
			parameters[2].Value = pEntity.unit_name;
			parameters[3].Value = pEntity.unit_name_en;
			parameters[4].Value = pEntity.unit_name_short;
			parameters[5].Value = pEntity.unit_city_id;
			parameters[6].Value = pEntity.unit_address;
			parameters[7].Value = pEntity.unit_contact;
			parameters[8].Value = pEntity.unit_tel;
			parameters[9].Value = pEntity.unit_fax;
			parameters[10].Value = pEntity.unit_email;
			parameters[11].Value = pEntity.unit_postcode;
			parameters[12].Value = pEntity.unit_remark;
			parameters[13].Value = pEntity.Status;
			parameters[14].Value = pEntity.unit_flag;
			parameters[15].Value = pEntity.CUSTOMER_LEVEL;
			parameters[16].Value = pEntity.create_user_id;
			parameters[17].Value = pEntity.create_time;
			parameters[18].Value = pEntity.modify_user_id;
			parameters[19].Value = pEntity.modify_time;
			parameters[20].Value = pEntity.status_desc;
			parameters[21].Value = pEntity.bat_id;
			parameters[22].Value = pEntity.if_flag;
			parameters[23].Value = pEntity.customer_id;
			parameters[24].Value = pEntity.longitude;
			parameters[25].Value = pEntity.dimension;
			parameters[26].Value = pEntity.imageURL;
			parameters[27].Value = pEntity.ftpImagerURL;
			parameters[28].Value = pEntity.webserversURL;
			parameters[29].Value = pEntity.weiXinId;
			parameters[30].Value = pEntity.dimensionalCodeURL;
			parameters[31].Value = pEntity.BizHoursStarttime;
			parameters[32].Value = pEntity.BizHoursEndtime;
			parameters[33].Value = pEntity.StoreType;
			parameters[34].Value = pEntity.unit_id;

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
        public void Update(t_unitEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(t_unitEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(t_unitEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.unit_id == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.unit_id, pTran);           
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
            sql.AppendLine("update [t_unit] set status='-1' where unit_id=@unit_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@unit_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(t_unitEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.unit_id == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.unit_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(t_unitEntity[] pEntities)
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
            sql.AppendLine("update [t_unit] set status='-1' where unit_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public t_unitEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_unit] where 1=1  and status<>'-1' ");
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
            List<t_unitEntity> list = new List<t_unitEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    t_unitEntity m;
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
        public PagedQueryResult<t_unitEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [unit_id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [t_unit] where 1=1  and status<>'-1' ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [t_unit] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<t_unitEntity> result = new PagedQueryResult<t_unitEntity>();
            List<t_unitEntity> list = new List<t_unitEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    t_unitEntity m;
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
        public t_unitEntity[] QueryByEntity(t_unitEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<t_unitEntity> PagedQueryByEntity(t_unitEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(t_unitEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.unit_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_id", Value = pQueryEntity.unit_id });
            if (pQueryEntity.type_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_id", Value = pQueryEntity.type_id });
            if (pQueryEntity.unit_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_code", Value = pQueryEntity.unit_code });
            if (pQueryEntity.unit_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_name", Value = pQueryEntity.unit_name });
            if (pQueryEntity.unit_name_en!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_name_en", Value = pQueryEntity.unit_name_en });
            if (pQueryEntity.unit_name_short!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_name_short", Value = pQueryEntity.unit_name_short });
            if (pQueryEntity.unit_city_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_city_id", Value = pQueryEntity.unit_city_id });
            if (pQueryEntity.unit_address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_address", Value = pQueryEntity.unit_address });
            if (pQueryEntity.unit_contact!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_contact", Value = pQueryEntity.unit_contact });
            if (pQueryEntity.unit_tel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_tel", Value = pQueryEntity.unit_tel });
            if (pQueryEntity.unit_fax!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_fax", Value = pQueryEntity.unit_fax });
            if (pQueryEntity.unit_email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_email", Value = pQueryEntity.unit_email });
            if (pQueryEntity.unit_postcode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_postcode", Value = pQueryEntity.unit_postcode });
            if (pQueryEntity.unit_remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_remark", Value = pQueryEntity.unit_remark });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.unit_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_flag", Value = pQueryEntity.unit_flag });
            if (pQueryEntity.CUSTOMER_LEVEL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CUSTOMER_LEVEL", Value = pQueryEntity.CUSTOMER_LEVEL });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.status_desc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status_desc", Value = pQueryEntity.status_desc });
            if (pQueryEntity.bat_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "bat_id", Value = pQueryEntity.bat_id });
            if (pQueryEntity.if_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "if_flag", Value = pQueryEntity.if_flag });
            if (pQueryEntity.customer_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_id", Value = pQueryEntity.customer_id });
            if (pQueryEntity.longitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "longitude", Value = pQueryEntity.longitude });
            if (pQueryEntity.dimension!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "dimension", Value = pQueryEntity.dimension });
            if (pQueryEntity.imageURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "imageURL", Value = pQueryEntity.imageURL });
            if (pQueryEntity.ftpImagerURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ftpImagerURL", Value = pQueryEntity.ftpImagerURL });
            if (pQueryEntity.webserversURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "webserversURL", Value = pQueryEntity.webserversURL });
            if (pQueryEntity.weiXinId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "weiXinId", Value = pQueryEntity.weiXinId });
            if (pQueryEntity.dimensionalCodeURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "dimensionalCodeURL", Value = pQueryEntity.dimensionalCodeURL });
            if (pQueryEntity.BizHoursStarttime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BizHoursStarttime", Value = pQueryEntity.BizHoursStarttime });
            if (pQueryEntity.BizHoursEndtime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BizHoursEndtime", Value = pQueryEntity.BizHoursEndtime });
            if (pQueryEntity.StoreType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreType", Value = pQueryEntity.StoreType });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out t_unitEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new t_unitEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["unit_id"] != DBNull.Value)
			{
				pInstance.unit_id =  Convert.ToString(pReader["unit_id"]);
			}
			if (pReader["type_id"] != DBNull.Value)
			{
				pInstance.type_id =  Convert.ToString(pReader["type_id"]);
			}
			if (pReader["unit_code"] != DBNull.Value)
			{
				pInstance.unit_code =  Convert.ToString(pReader["unit_code"]);
			}
			if (pReader["unit_name"] != DBNull.Value)
			{
				pInstance.unit_name =  Convert.ToString(pReader["unit_name"]);
			}
			if (pReader["unit_name_en"] != DBNull.Value)
			{
				pInstance.unit_name_en =  Convert.ToString(pReader["unit_name_en"]);
			}
			if (pReader["unit_name_short"] != DBNull.Value)
			{
				pInstance.unit_name_short =  Convert.ToString(pReader["unit_name_short"]);
			}
			if (pReader["unit_city_id"] != DBNull.Value)
			{
				pInstance.unit_city_id =  Convert.ToString(pReader["unit_city_id"]);
			}
			if (pReader["unit_address"] != DBNull.Value)
			{
				pInstance.unit_address =  Convert.ToString(pReader["unit_address"]);
			}
			if (pReader["unit_contact"] != DBNull.Value)
			{
				pInstance.unit_contact =  Convert.ToString(pReader["unit_contact"]);
			}
			if (pReader["unit_tel"] != DBNull.Value)
			{
				pInstance.unit_tel =  Convert.ToString(pReader["unit_tel"]);
			}
			if (pReader["unit_fax"] != DBNull.Value)
			{
				pInstance.unit_fax =  Convert.ToString(pReader["unit_fax"]);
			}
			if (pReader["unit_email"] != DBNull.Value)
			{
				pInstance.unit_email =  Convert.ToString(pReader["unit_email"]);
			}
			if (pReader["unit_postcode"] != DBNull.Value)
			{
				pInstance.unit_postcode =  Convert.ToString(pReader["unit_postcode"]);
			}
			if (pReader["unit_remark"] != DBNull.Value)
			{
				pInstance.unit_remark =  Convert.ToString(pReader["unit_remark"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =  Convert.ToString(pReader["Status"]);
			}
			if (pReader["unit_flag"] != DBNull.Value)
			{
				pInstance.unit_flag =  Convert.ToString(pReader["unit_flag"]);
			}
			if (pReader["CUSTOMER_LEVEL"] != DBNull.Value)
			{
				pInstance.CUSTOMER_LEVEL =   Convert.ToInt32(pReader["CUSTOMER_LEVEL"]);
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
			if (pReader["status_desc"] != DBNull.Value)
			{
				pInstance.status_desc =  Convert.ToString(pReader["status_desc"]);
			}
			if (pReader["bat_id"] != DBNull.Value)
			{
				pInstance.bat_id =  Convert.ToString(pReader["bat_id"]);
			}
			if (pReader["if_flag"] != DBNull.Value)
			{
				pInstance.if_flag =  Convert.ToString(pReader["if_flag"]);
			}
			if (pReader["customer_id"] != DBNull.Value)
			{
				pInstance.customer_id =  Convert.ToString(pReader["customer_id"]);
			}
			if (pReader["longitude"] != DBNull.Value)
			{
				pInstance.longitude =  Convert.ToString(pReader["longitude"]);
			}
			if (pReader["dimension"] != DBNull.Value)
			{
				pInstance.dimension =  Convert.ToString(pReader["dimension"]);
			}
			if (pReader["imageURL"] != DBNull.Value)
			{
				pInstance.imageURL =  Convert.ToString(pReader["imageURL"]);
			}
			if (pReader["ftpImagerURL"] != DBNull.Value)
			{
				pInstance.ftpImagerURL =  Convert.ToString(pReader["ftpImagerURL"]);
			}
			if (pReader["webserversURL"] != DBNull.Value)
			{
				pInstance.webserversURL =  Convert.ToString(pReader["webserversURL"]);
			}
			if (pReader["weiXinId"] != DBNull.Value)
			{
				pInstance.weiXinId =  Convert.ToString(pReader["weiXinId"]);
			}
			if (pReader["dimensionalCodeURL"] != DBNull.Value)
			{
				pInstance.dimensionalCodeURL =  Convert.ToString(pReader["dimensionalCodeURL"]);
			}
			if (pReader["BizHoursStarttime"] != DBNull.Value)
			{
				pInstance.BizHoursStarttime =  Convert.ToString(pReader["BizHoursStarttime"]);
			}
			if (pReader["BizHoursEndtime"] != DBNull.Value)
			{
				pInstance.BizHoursEndtime =  Convert.ToString(pReader["BizHoursEndtime"]);
			}
			if (pReader["StoreType"] != DBNull.Value)
			{
				pInstance.StoreType =  Convert.ToString(pReader["StoreType"]);
			}

        }
        #endregion
    }
}
