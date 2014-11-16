/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/19 10:39:35
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
    /// 表PgUser的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PgUserDAO : Base.BaseCPOSDAO, ICRUDable<PgUserEntity>, IQueryable<PgUserEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PgUserDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(PgUserEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(PgUserEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [PgUser](");
            strSql.Append("[ID],[MARKET],[LAST_NAME],[FIRST_NAME],[KNOWN_AS],[GENDER],[BIRTHDATE],[EMAIL],[MANAGER_EMAIL],[FUNC],[JOB_BAND],[JOB_BAND_DATE],[SERVICE_YEAR],[SUBORDINATE_COUNT],[LOCATION],[ON_BLACK_LIST],[BLACK_DATE],[BLACK_REASON],[ADMIN_UPDATED_MOBILE],[USER_POINT],[MOBILE],[channel_id],[group_id],[headphoto_id],[onlineCourseCommentCount],[POINT],[Type],[BRAND],[channel_group_id],[DCC_POINT],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[City],[SpecialTitle],[USER_ID])");
            strSql.Append(" values (");
            strSql.Append("@ID,@MARKET,@LASTNAME,@FIRSTNAME,@KNOWNAS,@GENDER,@BIRTHDATE,@EMAIL,@MANAGEREMAIL,@FUNC,@JOBBAND,@JOBBANDDATE,@SERVICEYEAR,@SUBORDINATECOUNT,@LOCATION,@ONBLACKLIST,@BLACKDATE,@BLACKREASON,@ADMINUPDATEDMOBILE,@USERPOINT,@MOBILE,@ChannelID,@GroupID,@HeadphotoID,@OnlineCourseCommentCount,@POINT,@Type,@BRAND,@ChannelGroupID,@DCCPOINT,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@City,@SpecialTitle,@USERID)");            

			string pkString = pEntity.USER_ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ID",SqlDbType.Int),
					new SqlParameter("@MARKET",SqlDbType.VarChar),
					new SqlParameter("@LASTNAME",SqlDbType.VarChar),
					new SqlParameter("@FIRSTNAME",SqlDbType.VarChar),
					new SqlParameter("@KNOWNAS",SqlDbType.VarChar),
					new SqlParameter("@GENDER",SqlDbType.VarChar),
					new SqlParameter("@BIRTHDATE",SqlDbType.DateTime),
					new SqlParameter("@EMAIL",SqlDbType.VarChar),
					new SqlParameter("@MANAGEREMAIL",SqlDbType.VarChar),
					new SqlParameter("@FUNC",SqlDbType.VarChar),
					new SqlParameter("@JOBBAND",SqlDbType.Int),
					new SqlParameter("@JOBBANDDATE",SqlDbType.DateTime),
					new SqlParameter("@SERVICEYEAR",SqlDbType.Int),
					new SqlParameter("@SUBORDINATECOUNT",SqlDbType.Int),
					new SqlParameter("@LOCATION",SqlDbType.VarChar),
					new SqlParameter("@ONBLACKLIST",SqlDbType.Int),
					new SqlParameter("@BLACKDATE",SqlDbType.DateTime),
					new SqlParameter("@BLACKREASON",SqlDbType.VarChar),
					new SqlParameter("@ADMINUPDATEDMOBILE",SqlDbType.VarChar),
					new SqlParameter("@USERPOINT",SqlDbType.Int),
					new SqlParameter("@MOBILE",SqlDbType.VarChar),
					new SqlParameter("@ChannelID",SqlDbType.Int),
					new SqlParameter("@GroupID",SqlDbType.Int),
					new SqlParameter("@HeadphotoID",SqlDbType.Int),
					new SqlParameter("@OnlineCourseCommentCount",SqlDbType.Int),
					new SqlParameter("@POINT",SqlDbType.Int),
					new SqlParameter("@Type",SqlDbType.VarChar),
					new SqlParameter("@BRAND",SqlDbType.VarChar),
					new SqlParameter("@ChannelGroupID",SqlDbType.Int),
					new SqlParameter("@DCCPOINT",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@SpecialTitle",SqlDbType.NVarChar),
					new SqlParameter("@USERID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ID;
			parameters[1].Value = pEntity.MARKET;
			parameters[2].Value = pEntity.LASTNAME;
			parameters[3].Value = pEntity.FIRSTNAME;
			parameters[4].Value = pEntity.KNOWNAS;
			parameters[5].Value = pEntity.GENDER;
			parameters[6].Value = pEntity.BIRTHDATE;
			parameters[7].Value = pEntity.EMAIL;
			parameters[8].Value = pEntity.MANAGEREMAIL;
			parameters[9].Value = pEntity.FUNC;
			parameters[10].Value = pEntity.JOBBAND;
			parameters[11].Value = pEntity.JOBBANDDATE;
			parameters[12].Value = pEntity.SERVICEYEAR;
			parameters[13].Value = pEntity.SUBORDINATECOUNT;
			parameters[14].Value = pEntity.LOCATION;
			parameters[15].Value = pEntity.ONBLACKLIST;
			parameters[16].Value = pEntity.BLACKDATE;
			parameters[17].Value = pEntity.BLACKREASON;
			parameters[18].Value = pEntity.ADMINUPDATEDMOBILE;
			parameters[19].Value = pEntity.USERPOINT;
			parameters[20].Value = pEntity.MOBILE;
			parameters[21].Value = pEntity.ChannelID;
			parameters[22].Value = pEntity.GroupID;
			parameters[23].Value = pEntity.HeadphotoID;
			parameters[24].Value = pEntity.OnlineCourseCommentCount;
			parameters[25].Value = pEntity.POINT;
			parameters[26].Value = pEntity.Type;
			parameters[27].Value = pEntity.BRAND;
			parameters[28].Value = pEntity.ChannelGroupID;
			parameters[29].Value = pEntity.DCCPOINT;
			parameters[30].Value = pEntity.CreateBy;
			parameters[31].Value = pEntity.CreateTime;
			parameters[32].Value = pEntity.LastUpdateBy;
			parameters[33].Value = pEntity.LastUpdateTime;
			parameters[34].Value = pEntity.IsDelete;
			parameters[35].Value = pEntity.City;
			parameters[36].Value = pEntity.SpecialTitle;
			parameters[37].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.USER_ID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public PgUserEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PgUser] where USER_ID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            PgUserEntity m = null;
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
        public PgUserEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PgUser] where isdelete=0");
            //读取数据
            List<PgUserEntity> list = new List<PgUserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PgUserEntity m;
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
        public void Update(PgUserEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(PgUserEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.USER_ID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PgUser] set ");
            if (pIsUpdateNullField || pEntity.ID!=null)
                strSql.Append( "[ID]=@ID,");
            if (pIsUpdateNullField || pEntity.MARKET!=null)
                strSql.Append( "[MARKET]=@MARKET,");
            if (pIsUpdateNullField || pEntity.LASTNAME!=null)
                strSql.Append( "[LAST_NAME]=@LASTNAME,");
            if (pIsUpdateNullField || pEntity.FIRSTNAME!=null)
                strSql.Append( "[FIRST_NAME]=@FIRSTNAME,");
            if (pIsUpdateNullField || pEntity.KNOWNAS!=null)
                strSql.Append( "[KNOWN_AS]=@KNOWNAS,");
            if (pIsUpdateNullField || pEntity.GENDER!=null)
                strSql.Append( "[GENDER]=@GENDER,");
            if (pIsUpdateNullField || pEntity.BIRTHDATE!=null)
                strSql.Append( "[BIRTHDATE]=@BIRTHDATE,");
            if (pIsUpdateNullField || pEntity.EMAIL!=null)
                strSql.Append( "[EMAIL]=@EMAIL,");
            if (pIsUpdateNullField || pEntity.MANAGEREMAIL!=null)
                strSql.Append( "[MANAGER_EMAIL]=@MANAGEREMAIL,");
            if (pIsUpdateNullField || pEntity.FUNC!=null)
                strSql.Append( "[FUNC]=@FUNC,");
            if (pIsUpdateNullField || pEntity.JOBBAND!=null)
                strSql.Append( "[JOB_BAND]=@JOBBAND,");
            if (pIsUpdateNullField || pEntity.JOBBANDDATE!=null)
                strSql.Append( "[JOB_BAND_DATE]=@JOBBANDDATE,");
            if (pIsUpdateNullField || pEntity.SERVICEYEAR!=null)
                strSql.Append( "[SERVICE_YEAR]=@SERVICEYEAR,");
            if (pIsUpdateNullField || pEntity.SUBORDINATECOUNT!=null)
                strSql.Append( "[SUBORDINATE_COUNT]=@SUBORDINATECOUNT,");
            if (pIsUpdateNullField || pEntity.LOCATION!=null)
                strSql.Append( "[LOCATION]=@LOCATION,");
            if (pIsUpdateNullField || pEntity.ONBLACKLIST!=null)
                strSql.Append( "[ON_BLACK_LIST]=@ONBLACKLIST,");
            if (pIsUpdateNullField || pEntity.BLACKDATE!=null)
                strSql.Append( "[BLACK_DATE]=@BLACKDATE,");
            if (pIsUpdateNullField || pEntity.BLACKREASON!=null)
                strSql.Append( "[BLACK_REASON]=@BLACKREASON,");
            if (pIsUpdateNullField || pEntity.ADMINUPDATEDMOBILE!=null)
                strSql.Append( "[ADMIN_UPDATED_MOBILE]=@ADMINUPDATEDMOBILE,");
            if (pIsUpdateNullField || pEntity.USERPOINT!=null)
                strSql.Append( "[USER_POINT]=@USERPOINT,");
            if (pIsUpdateNullField || pEntity.MOBILE!=null)
                strSql.Append( "[MOBILE]=@MOBILE,");
            if (pIsUpdateNullField || pEntity.ChannelID!=null)
                strSql.Append( "[channel_id]=@ChannelID,");
            if (pIsUpdateNullField || pEntity.GroupID!=null)
                strSql.Append( "[group_id]=@GroupID,");
            if (pIsUpdateNullField || pEntity.HeadphotoID!=null)
                strSql.Append( "[headphoto_id]=@HeadphotoID,");
            if (pIsUpdateNullField || pEntity.OnlineCourseCommentCount!=null)
                strSql.Append( "[onlineCourseCommentCount]=@OnlineCourseCommentCount,");
            if (pIsUpdateNullField || pEntity.POINT!=null)
                strSql.Append( "[POINT]=@POINT,");
            if (pIsUpdateNullField || pEntity.Type!=null)
                strSql.Append( "[Type]=@Type,");
            if (pIsUpdateNullField || pEntity.BRAND!=null)
                strSql.Append( "[BRAND]=@BRAND,");
            if (pIsUpdateNullField || pEntity.ChannelGroupID!=null)
                strSql.Append( "[channel_group_id]=@ChannelGroupID,");
            if (pIsUpdateNullField || pEntity.DCCPOINT!=null)
                strSql.Append( "[DCC_POINT]=@DCCPOINT,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.City!=null)
                strSql.Append( "[City]=@City,");
            if (pIsUpdateNullField || pEntity.SpecialTitle!=null)
                strSql.Append( "[SpecialTitle]=@SpecialTitle");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where USER_ID=@USERID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ID",SqlDbType.Int),
					new SqlParameter("@MARKET",SqlDbType.VarChar),
					new SqlParameter("@LASTNAME",SqlDbType.VarChar),
					new SqlParameter("@FIRSTNAME",SqlDbType.VarChar),
					new SqlParameter("@KNOWNAS",SqlDbType.VarChar),
					new SqlParameter("@GENDER",SqlDbType.VarChar),
					new SqlParameter("@BIRTHDATE",SqlDbType.DateTime),
					new SqlParameter("@EMAIL",SqlDbType.VarChar),
					new SqlParameter("@MANAGEREMAIL",SqlDbType.VarChar),
					new SqlParameter("@FUNC",SqlDbType.VarChar),
					new SqlParameter("@JOBBAND",SqlDbType.Int),
					new SqlParameter("@JOBBANDDATE",SqlDbType.DateTime),
					new SqlParameter("@SERVICEYEAR",SqlDbType.Int),
					new SqlParameter("@SUBORDINATECOUNT",SqlDbType.Int),
					new SqlParameter("@LOCATION",SqlDbType.VarChar),
					new SqlParameter("@ONBLACKLIST",SqlDbType.Int),
					new SqlParameter("@BLACKDATE",SqlDbType.DateTime),
					new SqlParameter("@BLACKREASON",SqlDbType.VarChar),
					new SqlParameter("@ADMINUPDATEDMOBILE",SqlDbType.VarChar),
					new SqlParameter("@USERPOINT",SqlDbType.Int),
					new SqlParameter("@MOBILE",SqlDbType.VarChar),
					new SqlParameter("@ChannelID",SqlDbType.Int),
					new SqlParameter("@GroupID",SqlDbType.Int),
					new SqlParameter("@HeadphotoID",SqlDbType.Int),
					new SqlParameter("@OnlineCourseCommentCount",SqlDbType.Int),
					new SqlParameter("@POINT",SqlDbType.Int),
					new SqlParameter("@Type",SqlDbType.VarChar),
					new SqlParameter("@BRAND",SqlDbType.VarChar),
					new SqlParameter("@ChannelGroupID",SqlDbType.Int),
					new SqlParameter("@DCCPOINT",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@SpecialTitle",SqlDbType.NVarChar),
					new SqlParameter("@USERID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ID;
			parameters[1].Value = pEntity.MARKET;
			parameters[2].Value = pEntity.LASTNAME;
			parameters[3].Value = pEntity.FIRSTNAME;
			parameters[4].Value = pEntity.KNOWNAS;
			parameters[5].Value = pEntity.GENDER;
			parameters[6].Value = pEntity.BIRTHDATE;
			parameters[7].Value = pEntity.EMAIL;
			parameters[8].Value = pEntity.MANAGEREMAIL;
			parameters[9].Value = pEntity.FUNC;
			parameters[10].Value = pEntity.JOBBAND;
			parameters[11].Value = pEntity.JOBBANDDATE;
			parameters[12].Value = pEntity.SERVICEYEAR;
			parameters[13].Value = pEntity.SUBORDINATECOUNT;
			parameters[14].Value = pEntity.LOCATION;
			parameters[15].Value = pEntity.ONBLACKLIST;
			parameters[16].Value = pEntity.BLACKDATE;
			parameters[17].Value = pEntity.BLACKREASON;
			parameters[18].Value = pEntity.ADMINUPDATEDMOBILE;
			parameters[19].Value = pEntity.USERPOINT;
			parameters[20].Value = pEntity.MOBILE;
			parameters[21].Value = pEntity.ChannelID;
			parameters[22].Value = pEntity.GroupID;
			parameters[23].Value = pEntity.HeadphotoID;
			parameters[24].Value = pEntity.OnlineCourseCommentCount;
			parameters[25].Value = pEntity.POINT;
			parameters[26].Value = pEntity.Type;
			parameters[27].Value = pEntity.BRAND;
			parameters[28].Value = pEntity.ChannelGroupID;
			parameters[29].Value = pEntity.DCCPOINT;
			parameters[30].Value = pEntity.LastUpdateBy;
			parameters[31].Value = pEntity.LastUpdateTime;
			parameters[32].Value = pEntity.City;
			parameters[33].Value = pEntity.SpecialTitle;
			parameters[34].Value = pEntity.USER_ID;

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
        public void Update(PgUserEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(PgUserEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PgUserEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(PgUserEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.USER_ID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.USER_ID, pTran);           
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
            sql.AppendLine("update [PgUser] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where USER_ID=@USERID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@USERID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(PgUserEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.USER_ID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.USER_ID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(PgUserEntity[] pEntities)
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
            sql.AppendLine("update [PgUser] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where USER_ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PgUserEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PgUser] where isdelete=0 ");
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
            List<PgUserEntity> list = new List<PgUserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PgUserEntity m;
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
        public PagedQueryResult<PgUserEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [USERID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [PgUser] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [PgUser] where isdelete=0 ");
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
            PagedQueryResult<PgUserEntity> result = new PagedQueryResult<PgUserEntity>();
            List<PgUserEntity> list = new List<PgUserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PgUserEntity m;
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
        public PgUserEntity[] QueryByEntity(PgUserEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PgUserEntity> PagedQueryByEntity(PgUserEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PgUserEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.USER_ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "USERID", Value = pQueryEntity.USER_ID });
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.MARKET!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MARKET", Value = pQueryEntity.MARKET });
            if (pQueryEntity.LASTNAME!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LASTNAME", Value = pQueryEntity.LASTNAME });
            if (pQueryEntity.FIRSTNAME!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FIRSTNAME", Value = pQueryEntity.FIRSTNAME });
            if (pQueryEntity.KNOWNAS!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KNOWNAS", Value = pQueryEntity.KNOWNAS });
            if (pQueryEntity.GENDER!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GENDER", Value = pQueryEntity.GENDER });
            if (pQueryEntity.BIRTHDATE!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BIRTHDATE", Value = pQueryEntity.BIRTHDATE });
            if (pQueryEntity.EMAIL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EMAIL", Value = pQueryEntity.EMAIL });
            if (pQueryEntity.MANAGEREMAIL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MANAGEREMAIL", Value = pQueryEntity.MANAGEREMAIL });
            if (pQueryEntity.FUNC!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FUNC", Value = pQueryEntity.FUNC });
            if (pQueryEntity.JOBBAND!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "JOBBAND", Value = pQueryEntity.JOBBAND });
            if (pQueryEntity.JOBBANDDATE!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "JOBBANDDATE", Value = pQueryEntity.JOBBANDDATE });
            if (pQueryEntity.SERVICEYEAR!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SERVICEYEAR", Value = pQueryEntity.SERVICEYEAR });
            if (pQueryEntity.SUBORDINATECOUNT!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SUBORDINATECOUNT", Value = pQueryEntity.SUBORDINATECOUNT });
            if (pQueryEntity.LOCATION!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LOCATION", Value = pQueryEntity.LOCATION });
            if (pQueryEntity.ONBLACKLIST!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ONBLACKLIST", Value = pQueryEntity.ONBLACKLIST });
            if (pQueryEntity.BLACKDATE!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BLACKDATE", Value = pQueryEntity.BLACKDATE });
            if (pQueryEntity.BLACKREASON!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BLACKREASON", Value = pQueryEntity.BLACKREASON });
            if (pQueryEntity.ADMINUPDATEDMOBILE!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ADMINUPDATEDMOBILE", Value = pQueryEntity.ADMINUPDATEDMOBILE });
            if (pQueryEntity.USERPOINT!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "USERPOINT", Value = pQueryEntity.USERPOINT });
            if (pQueryEntity.MOBILE!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MOBILE", Value = pQueryEntity.MOBILE });
            if (pQueryEntity.ChannelID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelID", Value = pQueryEntity.ChannelID });
            if (pQueryEntity.GroupID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupID", Value = pQueryEntity.GroupID });
            if (pQueryEntity.HeadphotoID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HeadphotoID", Value = pQueryEntity.HeadphotoID });
            if (pQueryEntity.OnlineCourseCommentCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineCourseCommentCount", Value = pQueryEntity.OnlineCourseCommentCount });
            if (pQueryEntity.POINT!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "POINT", Value = pQueryEntity.POINT });
            if (pQueryEntity.Type!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Type", Value = pQueryEntity.Type });
            if (pQueryEntity.BRAND!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BRAND", Value = pQueryEntity.BRAND });
            if (pQueryEntity.ChannelGroupID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelGroupID", Value = pQueryEntity.ChannelGroupID });
            if (pQueryEntity.DCCPOINT!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DCCPOINT", Value = pQueryEntity.DCCPOINT });
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
            if (pQueryEntity.City!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "City", Value = pQueryEntity.City });
            if (pQueryEntity.SpecialTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SpecialTitle", Value = pQueryEntity.SpecialTitle });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out PgUserEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new PgUserEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["USER_ID"] != DBNull.Value)
			{
				pInstance.USER_ID =  Convert.ToString(pReader["USER_ID"]);
			}
			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =   Convert.ToInt32(pReader["ID"]);
			}
			if (pReader["MARKET"] != DBNull.Value)
			{
				pInstance.MARKET =  Convert.ToString(pReader["MARKET"]);
			}
			if (pReader["LAST_NAME"] != DBNull.Value)
			{
				pInstance.LASTNAME =  Convert.ToString(pReader["LAST_NAME"]);
			}
			if (pReader["FIRST_NAME"] != DBNull.Value)
			{
				pInstance.FIRSTNAME =  Convert.ToString(pReader["FIRST_NAME"]);
			}
			if (pReader["KNOWN_AS"] != DBNull.Value)
			{
				pInstance.KNOWNAS =  Convert.ToString(pReader["KNOWN_AS"]);
			}
			if (pReader["GENDER"] != DBNull.Value)
			{
				pInstance.GENDER =  Convert.ToString(pReader["GENDER"]);
			}
			if (pReader["BIRTHDATE"] != DBNull.Value)
			{
				pInstance.BIRTHDATE =  Convert.ToDateTime(pReader["BIRTHDATE"]);
			}
			if (pReader["EMAIL"] != DBNull.Value)
			{
				pInstance.EMAIL =  Convert.ToString(pReader["EMAIL"]);
			}
			if (pReader["MANAGER_EMAIL"] != DBNull.Value)
			{
				pInstance.MANAGEREMAIL =  Convert.ToString(pReader["MANAGER_EMAIL"]);
			}
			if (pReader["FUNC"] != DBNull.Value)
			{
				pInstance.FUNC =  Convert.ToString(pReader["FUNC"]);
			}
			if (pReader["JOB_BAND"] != DBNull.Value)
			{
				pInstance.JOBBAND =   Convert.ToInt32(pReader["JOB_BAND"]);
			}
			if (pReader["JOB_BAND_DATE"] != DBNull.Value)
			{
				pInstance.JOBBANDDATE =  Convert.ToDateTime(pReader["JOB_BAND_DATE"]);
			}
			if (pReader["SERVICE_YEAR"] != DBNull.Value)
			{
				pInstance.SERVICEYEAR =   Convert.ToInt32(pReader["SERVICE_YEAR"]);
			}
			if (pReader["SUBORDINATE_COUNT"] != DBNull.Value)
			{
				pInstance.SUBORDINATECOUNT =   Convert.ToInt32(pReader["SUBORDINATE_COUNT"]);
			}
			if (pReader["LOCATION"] != DBNull.Value)
			{
				pInstance.LOCATION =  Convert.ToString(pReader["LOCATION"]);
			}
			if (pReader["ON_BLACK_LIST"] != DBNull.Value)
			{
				pInstance.ONBLACKLIST =   Convert.ToInt32(pReader["ON_BLACK_LIST"]);
			}
			if (pReader["BLACK_DATE"] != DBNull.Value)
			{
				pInstance.BLACKDATE =  Convert.ToDateTime(pReader["BLACK_DATE"]);
			}
			if (pReader["BLACK_REASON"] != DBNull.Value)
			{
				pInstance.BLACKREASON =  Convert.ToString(pReader["BLACK_REASON"]);
			}
			if (pReader["ADMIN_UPDATED_MOBILE"] != DBNull.Value)
			{
				pInstance.ADMINUPDATEDMOBILE =  Convert.ToString(pReader["ADMIN_UPDATED_MOBILE"]);
			}
			if (pReader["USER_POINT"] != DBNull.Value)
			{
				pInstance.USERPOINT =   Convert.ToInt32(pReader["USER_POINT"]);
			}
			if (pReader["MOBILE"] != DBNull.Value)
			{
				pInstance.MOBILE =  Convert.ToString(pReader["MOBILE"]);
			}
			if (pReader["channel_id"] != DBNull.Value)
			{
				pInstance.ChannelID =   Convert.ToInt32(pReader["channel_id"]);
			}
			if (pReader["group_id"] != DBNull.Value)
			{
				pInstance.GroupID =   Convert.ToInt32(pReader["group_id"]);
			}
			if (pReader["headphoto_id"] != DBNull.Value)
			{
				pInstance.HeadphotoID =   Convert.ToInt32(pReader["headphoto_id"]);
			}
			if (pReader["onlineCourseCommentCount"] != DBNull.Value)
			{
				pInstance.OnlineCourseCommentCount =   Convert.ToInt32(pReader["onlineCourseCommentCount"]);
			}
			if (pReader["POINT"] != DBNull.Value)
			{
				pInstance.POINT =   Convert.ToInt32(pReader["POINT"]);
			}
			if (pReader["Type"] != DBNull.Value)
			{
				pInstance.Type =  Convert.ToString(pReader["Type"]);
			}
			if (pReader["BRAND"] != DBNull.Value)
			{
				pInstance.BRAND =  Convert.ToString(pReader["BRAND"]);
			}
			if (pReader["channel_group_id"] != DBNull.Value)
			{
				pInstance.ChannelGroupID =   Convert.ToInt32(pReader["channel_group_id"]);
			}
			if (pReader["DCC_POINT"] != DBNull.Value)
			{
				pInstance.DCCPOINT =   Convert.ToInt32(pReader["DCC_POINT"]);
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
			if (pReader["City"] != DBNull.Value)
			{
				pInstance.City =  Convert.ToString(pReader["City"]);
			}
			if (pReader["SpecialTitle"] != DBNull.Value)
			{
				pInstance.SpecialTitle =  Convert.ToString(pReader["SpecialTitle"]);
			}

        }
        #endregion
    }
}
