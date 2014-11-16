/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
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
    /// 表LEvents的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WLEventsDAO : Base.BaseCPOSDAO, ICRUDable<LEventsEntity>, IQueryable<LEventsEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WLEventsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion


        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(LEventsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(LEventsEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [LEvents](");
            strSql.Append("[Title],[EventLevel],[ParentEventID],[BeginTime],[EndTime],[WeiXinID],[IsDefault],[IsTop],[Organizer],[Address],[CityID],[Description],[ImageURL],[URL],[Content],[PhoneNumber],[Email],[ApplyQuesID],[PollQuesID],[IsSubEvent],[Longitude],[Latitude],[EventStatus],[DisplayIndex],[PersonCount],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[ModelId],[EventManagerUserId],[EventID])");
            strSql.Append(" values (");
            strSql.Append("@Title,@EventLevel,@ParentEventID,@BeginTime,@EndTime,@WeiXinID,@IsDefault,@IsTop,@Organizer,@Address,@CityID,@Description,@ImageURL,@URL,@Content,@PhoneNumber,@Email,@ApplyQuesID,@PollQuesID,@IsSubEvent,@Longitude,@Latitude,@EventStatus,@DisplayIndex,@PersonCount,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@ModelId,@EventManagerUserId,@EventID)");

            string pkString = pEntity.EventID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@EventLevel",SqlDbType.Int),
					new SqlParameter("@ParentEventID",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
                    new SqlParameter("@IsDefault",SqlDbType.Int),
                    new SqlParameter("@IsTop",SqlDbType.Int),
                    new SqlParameter("@Organizer",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@CityID",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PhoneNumber",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@ApplyQuesID",SqlDbType.NVarChar),
					new SqlParameter("@PollQuesID",SqlDbType.NVarChar),
					new SqlParameter("@IsSubEvent",SqlDbType.Int),
					new SqlParameter("@Longitude",SqlDbType.NVarChar),
					new SqlParameter("@Latitude",SqlDbType.NVarChar),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@PersonCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@ModelId",SqlDbType.NVarChar),
					new SqlParameter("@EventManagerUserId",SqlDbType.NVarChar),
					new SqlParameter("@EventID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.Title;
            parameters[1].Value = pEntity.EventLevel;
            parameters[2].Value = pEntity.ParentEventID;
            parameters[3].Value = pEntity.BeginTime;
            parameters[4].Value = pEntity.EndTime;
            parameters[5].Value = pEntity.WeiXinID;
            parameters[6].Value = pEntity.IsDefault;
            parameters[7].Value = pEntity.IsTop;
            parameters[8].Value = pEntity.Organizer;
            parameters[9].Value = pEntity.Address;
            parameters[10].Value = pEntity.CityID;
            parameters[11].Value = pEntity.Description;
            parameters[12].Value = pEntity.ImageURL;
            parameters[13].Value = pEntity.URL;
            parameters[14].Value = pEntity.Content;
            parameters[15].Value = pEntity.PhoneNumber;
            parameters[16].Value = pEntity.Email;
            parameters[17].Value = pEntity.ApplyQuesID;
            parameters[18].Value = pEntity.PollQuesID;
            parameters[19].Value = pEntity.IsSubEvent;
            parameters[20].Value = pEntity.Longitude;
            parameters[21].Value = pEntity.Latitude;
            parameters[22].Value = pEntity.EventStatus;
            parameters[23].Value = pEntity.DisplayIndex;
            parameters[24].Value = pEntity.PersonCount;
            parameters[25].Value = pEntity.CreateTime;
            parameters[26].Value = pEntity.CreateBy;
            parameters[27].Value = pEntity.LastUpdateBy;
            parameters[28].Value = pEntity.LastUpdateTime;
            parameters[29].Value = pEntity.IsDelete;
            parameters[30].Value = pEntity.CustomerId;
            parameters[31].Value = pEntity.ModelId;
            parameters[32].Value = pEntity.EventManagerUserId;
            parameters[33].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.EventID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public LEventsEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LEvents] where EventID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            LEventsEntity m = null;
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
        public LEventsEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LEvents] where isdelete=0");
            //读取数据
            List<LEventsEntity> list = new List<LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LEventsEntity m;
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
        public void Update(LEventsEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(LEventsEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EventID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LEvents] set ");
            if (pIsUpdateNullField || pEntity.Title != null)
                strSql.Append("[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.EventLevel != null)
                strSql.Append("[EventLevel]=@EventLevel,");
            if (pIsUpdateNullField || pEntity.ParentEventID != null)
                strSql.Append("[ParentEventID]=@ParentEventID,");
            if (pIsUpdateNullField || pEntity.BeginTime != null)
                strSql.Append("[BeginTime]=@BeginTime,");
            if (pIsUpdateNullField || pEntity.EndTime != null)
                strSql.Append("[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.WeiXinID != null)
                strSql.Append("[WeiXinID]=@WeiXinID,");
            if (pIsUpdateNullField || pEntity.IsDefault != null)
                strSql.Append("[IsDefault]=@IsDefault,");
            if (pIsUpdateNullField || pEntity.IsTop != null)
                strSql.Append("[IsTop]=@IsTop,");
            if (pIsUpdateNullField || pEntity.Organizer != null)
                strSql.Append("[Organizer]=@Organizer,");
            if (pIsUpdateNullField || pEntity.Address != null)
                strSql.Append("[Address]=@Address,");
            if (pIsUpdateNullField || pEntity.CityID != null)
                strSql.Append("[CityID]=@CityID,");
            if (pIsUpdateNullField || pEntity.Description != null)
                strSql.Append("[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.ImageURL != null)
                strSql.Append("[ImageURL]=@ImageURL,");
            if (pIsUpdateNullField || pEntity.URL != null)
                strSql.Append("[URL]=@URL,");
            if (pIsUpdateNullField || pEntity.Content != null)
                strSql.Append("[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.PhoneNumber != null)
                strSql.Append("[PhoneNumber]=@PhoneNumber,");
            if (pIsUpdateNullField || pEntity.Email != null)
                strSql.Append("[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.ApplyQuesID != null)
                strSql.Append("[ApplyQuesID]=@ApplyQuesID,");
            if (pIsUpdateNullField || pEntity.PollQuesID != null)
                strSql.Append("[PollQuesID]=@PollQuesID,");
            if (pIsUpdateNullField || pEntity.IsSubEvent != null)
                strSql.Append("[IsSubEvent]=@IsSubEvent,");
            if (pIsUpdateNullField || pEntity.Longitude != null)
                strSql.Append("[Longitude]=@Longitude,");
            if (pIsUpdateNullField || pEntity.Latitude != null)
                strSql.Append("[Latitude]=@Latitude,");
            if (pIsUpdateNullField || pEntity.EventStatus != null)
                strSql.Append("[EventStatus]=@EventStatus,");
            if (pIsUpdateNullField || pEntity.DisplayIndex != null)
                strSql.Append("[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.PersonCount != null)
                strSql.Append("[PersonCount]=@PersonCount,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.ModelId != null)
                strSql.Append("[ModelId]=@ModelId,");
            if (pIsUpdateNullField || pEntity.EventManagerUserId != null)
                strSql.Append("[EventManagerUserId]=@EventManagerUserId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where EventID=@EventID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@EventLevel",SqlDbType.Int),
					new SqlParameter("@ParentEventID",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
                    new SqlParameter("@IsDefault",SqlDbType.Int),
                    new SqlParameter("@IsTop",SqlDbType.Int),
                    new SqlParameter("@Organizer",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@CityID",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PhoneNumber",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@ApplyQuesID",SqlDbType.NVarChar),
					new SqlParameter("@PollQuesID",SqlDbType.NVarChar),
					new SqlParameter("@IsSubEvent",SqlDbType.Int),
					new SqlParameter("@Longitude",SqlDbType.NVarChar),
					new SqlParameter("@Latitude",SqlDbType.NVarChar),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@PersonCount",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@ModelId",SqlDbType.NVarChar),
					new SqlParameter("@EventManagerUserId",SqlDbType.NVarChar),
					new SqlParameter("@EventID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.Title;
            parameters[1].Value = pEntity.EventLevel;
            parameters[2].Value = pEntity.ParentEventID;
            parameters[3].Value = pEntity.BeginTime;
            parameters[4].Value = pEntity.EndTime;
            parameters[5].Value = pEntity.WeiXinID;
            parameters[6].Value = pEntity.IsDefault;
            parameters[7].Value = pEntity.IsTop;
            parameters[8].Value = pEntity.Organizer;
            parameters[9].Value = pEntity.Address;
            parameters[10].Value = pEntity.CityID;
            parameters[11].Value = pEntity.Description;
            parameters[12].Value = pEntity.ImageURL;
            parameters[13].Value = pEntity.URL;
            parameters[14].Value = pEntity.Content;
            parameters[15].Value = pEntity.PhoneNumber;
            parameters[16].Value = pEntity.Email;
            parameters[17].Value = pEntity.ApplyQuesID;
            parameters[18].Value = pEntity.PollQuesID;
            parameters[19].Value = pEntity.IsSubEvent;
            parameters[20].Value = pEntity.Longitude;
            parameters[21].Value = pEntity.Latitude;
            parameters[22].Value = pEntity.EventStatus;
            parameters[23].Value = pEntity.DisplayIndex;
            parameters[24].Value = pEntity.PersonCount;
            parameters[25].Value = pEntity.LastUpdateBy;
            parameters[26].Value = pEntity.LastUpdateTime;
            parameters[27].Value = pEntity.CustomerId;
            parameters[28].Value = pEntity.ModelId;
            parameters[29].Value = pEntity.EventManagerUserId;
            parameters[30].Value = pEntity.EventID;

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
        public void Update(LEventsEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(LEventsEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LEventsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(LEventsEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EventID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.EventID, pTran);
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
            sql.AppendLine("update [LEvents] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where EventID=@EventID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@EventID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LEventsEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.EventID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.EventID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(LEventsEntity[] pEntities)
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
            sql.AppendLine("update [LEvents] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where EventID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LEventsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LEvents] where isdelete=0 ");
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
            List<LEventsEntity> list = new List<LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LEventsEntity m;
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
        public PagedQueryResult<LEventsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EventID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [LEvents] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [LEvents] where isdelete=0 ");
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
            PagedQueryResult<LEventsEntity> result = new PagedQueryResult<LEventsEntity>();
            List<LEventsEntity> list = new List<LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LEventsEntity m;
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
        public LEventsEntity[] QueryByEntity(LEventsEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LEventsEntity> PagedQueryByEntity(LEventsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LEventsEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EventID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventID", Value = pQueryEntity.EventID });
            if (pQueryEntity.Title != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.EventLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventLevel", Value = pQueryEntity.EventLevel });
            if (pQueryEntity.ParentEventID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentEventID", Value = pQueryEntity.ParentEventID });
            if (pQueryEntity.BeginTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.WeiXinID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinID", Value = pQueryEntity.WeiXinID });
            if (pQueryEntity.IsDefault != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDefault", Value = pQueryEntity.IsDefault });
            if (pQueryEntity.IsTop != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTop", Value = pQueryEntity.IsTop });
            if (pQueryEntity.Organizer != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Organizer", Value = pQueryEntity.Organizer });
            if (pQueryEntity.Address != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Address", Value = pQueryEntity.Address });
            if (pQueryEntity.CityID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CityID", Value = pQueryEntity.CityID });
            if (pQueryEntity.Description != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.ImageURL != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageURL", Value = pQueryEntity.ImageURL });
            if (pQueryEntity.URL != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "URL", Value = pQueryEntity.URL });
            if (pQueryEntity.Content != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.PhoneNumber != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PhoneNumber", Value = pQueryEntity.PhoneNumber });
            if (pQueryEntity.Email != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.ApplyQuesID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApplyQuesID", Value = pQueryEntity.ApplyQuesID });
            if (pQueryEntity.PollQuesID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PollQuesID", Value = pQueryEntity.PollQuesID });
            if (pQueryEntity.IsSubEvent != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSubEvent", Value = pQueryEntity.IsSubEvent });
            if (pQueryEntity.Longitude != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Longitude", Value = pQueryEntity.Longitude });
            if (pQueryEntity.Latitude != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Latitude", Value = pQueryEntity.Latitude });
            if (pQueryEntity.EventStatus != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventStatus", Value = pQueryEntity.EventStatus });
            if (pQueryEntity.DisplayIndex != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.PersonCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PersonCount", Value = pQueryEntity.PersonCount });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.ModelId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModelId", Value = pQueryEntity.ModelId });
            if (pQueryEntity.EventManagerUserId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventManagerUserId", Value = pQueryEntity.EventManagerUserId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out LEventsEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new LEventsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["EventID"] != DBNull.Value)
            {
                pInstance.EventID = Convert.ToString(pReader["EventID"]);
            }
            if (pReader["Title"] != DBNull.Value)
            {
                pInstance.Title = Convert.ToString(pReader["Title"]);
            }
            if (pReader["EventLevel"] != DBNull.Value)
            {
                pInstance.EventLevel = Convert.ToInt32(pReader["EventLevel"]);
            }
            if (pReader["ParentEventID"] != DBNull.Value)
            {
                pInstance.ParentEventID = Convert.ToString(pReader["ParentEventID"]);
            }
            if (pReader["BeginTime"] != DBNull.Value)
            {
                pInstance.BeginTime = Convert.ToString(pReader["BeginTime"]);
            }
            if (pReader["EndTime"] != DBNull.Value)
            {
                pInstance.EndTime = Convert.ToString(pReader["EndTime"]);
            }
            if (pReader["WeiXinID"] != DBNull.Value)
            {
                pInstance.WeiXinID = Convert.ToString(pReader["WeiXinID"]);
            }
            if (pReader["IsDefault"] != DBNull.Value)
            {
                pInstance.IsDefault = Convert.ToInt32(pReader["IsDefault"]);
            }
            if (pReader["IsTop"] != DBNull.Value)
            {
                pInstance.IsTop = Convert.ToInt32(pReader["IsTop"]);
            }
            if (pReader["Organizer"] != DBNull.Value)
            {
                pInstance.Organizer = Convert.ToString(pReader["Organizer"]);
            }
            if (pReader["Address"] != DBNull.Value)
            {
                pInstance.Address = Convert.ToString(pReader["Address"]);
            }
            if (pReader["CityID"] != DBNull.Value)
            {
                pInstance.CityID = Convert.ToString(pReader["CityID"]);
            }
            if (pReader["Description"] != DBNull.Value)
            {
                pInstance.Description = Convert.ToString(pReader["Description"]);
            }
            if (pReader["ImageURL"] != DBNull.Value)
            {
                pInstance.ImageURL = Convert.ToString(pReader["ImageURL"]);
            }
            if (pReader["URL"] != DBNull.Value)
            {
                pInstance.URL = Convert.ToString(pReader["URL"]);
            }
            if (pReader["Content"] != DBNull.Value)
            {
                pInstance.Content = Convert.ToString(pReader["Content"]);
            }
            if (pReader["PhoneNumber"] != DBNull.Value)
            {
                pInstance.PhoneNumber = Convert.ToString(pReader["PhoneNumber"]);
            }
            if (pReader["Email"] != DBNull.Value)
            {
                pInstance.Email = Convert.ToString(pReader["Email"]);
            }
            if (pReader["ApplyQuesID"] != DBNull.Value)
            {
                pInstance.ApplyQuesID = Convert.ToString(pReader["ApplyQuesID"]);
            }
            if (pReader["PollQuesID"] != DBNull.Value)
            {
                pInstance.PollQuesID = Convert.ToString(pReader["PollQuesID"]);
            }
            if (pReader["IsSubEvent"] != DBNull.Value)
            {
                pInstance.IsSubEvent = Convert.ToInt32(pReader["IsSubEvent"]);
            }
            if (pReader["Longitude"] != DBNull.Value)
            {
                pInstance.Longitude = Convert.ToString(pReader["Longitude"]);
            }
            if (pReader["Latitude"] != DBNull.Value)
            {
                pInstance.Latitude = Convert.ToString(pReader["Latitude"]);
            }
            if (pReader["EventStatus"] != DBNull.Value)
            {
                pInstance.EventStatus = Convert.ToInt32(pReader["EventStatus"]);
            }
            if (pReader["DisplayIndex"] != DBNull.Value)
            {
                pInstance.DisplayIndex = Convert.ToInt32(pReader["DisplayIndex"]);
            }
            if (pReader["PersonCount"] != DBNull.Value)
            {
                pInstance.PersonCount = Convert.ToInt32(pReader["PersonCount"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["CustomerId"] != DBNull.Value)
            {
                pInstance.CustomerId = Convert.ToString(pReader["CustomerId"]);
            }
            if (pReader["ModelId"] != DBNull.Value)
            {
                pInstance.ModelId = Convert.ToString(pReader["ModelId"]);
            }
            if (pReader["EventManagerUserId"] != DBNull.Value)
            {
                pInstance.EventManagerUserId = Convert.ToString(pReader["EventManagerUserId"]);
            }

        }
        #endregion
        #region 获取定制酒介绍
        /// <summary>
        /// 获取定制酒介绍
        /// </summary>
        /// <returns></returns>
        public DataSet GetSkus()
        {
            string sql = "  "
                + " SELECT skuId = s.sku_id, imageURL = i.imageUrl, gg = s.sku_prop_id1,  "
                + "        degree = s.sku_prop_id2, baseWineYear = s.sku_prop_id3,  "
                + "        itemName = i.item_name, agePitPits = s.sku_prop_id5, "
                + "        itemId = i.item_id "
                + " FROM dbo.T_Sku s  "
                + " LEFT JOIN dbo.T_Item i ON s.item_id = i.item_id "
                + " WHERE s.status = 1 "
                + " AND s.sku_id = (SELECT TOP 1 sku_id FROM dbo.T_Sku s1 WHERE s1.status = 1 AND s1.item_id = i.item_id) ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取定制酒详情
        /// <summary>
        /// 获取定制酒详情
        /// </summary>
        /// <returns></returns>
        public DataSet GetSkuDetail(string skuId)
        {
            string sql = "  "
                + " SELECT skuId = s.sku_id, imageURL = i.imageUrl, gg = s.sku_prop_id1,  "
                + "        degree = s.sku_prop_id2, baseWineYear = s.sku_prop_id3,   "
                + "        itemName = i.item_name, agePitPits = s.sku_prop_id5, "
                + "        salesPrice = CONVERT(VARCHAR, CONVERT(INT, vsp1.price)), "
                + "        purchasePrice = CONVERT(VARCHAR, CONVERT(INT, vsp2.price)), "
                + "        itemRemark = i.item_remark, itemId=i.item_Id, "
                + "        unit = '元/'+(SELECT x.prop_value FROM dbo.T_Item_Property x WHERE x.item_id = i.item_id AND x.prop_id = '4F18DDB482654F4A9C01E607AFE0A236')"
                + " FROM dbo.T_Sku s  "
                + " LEFT JOIN dbo.T_Item i ON s.item_id = i.item_id "
                + " LEFT JOIN vw_sku_price vsp1 ON vsp1.sku_id = s.sku_id AND vsp1.item_price_type_id = '8A327C5BB7A44FC6B96AB1A4A70EEAC9' "
                + " LEFT JOIN vw_sku_price vsp2 ON vsp2.sku_id = s.sku_id AND vsp2.item_price_type_id = 'F52356521CEC4194872784F8E26C69DE' "
                + " WHERE s.status = 1 AND s.sku_id = '" + skuId + "' ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取活动详情(活动介绍)
        /// <summary>
        /// 获取活动详情(活动介绍)
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventInfo(string eventId)
        {
            string sql = "  "
                + " SELECT l1.EventID, l2.ImageURL, l1.Title,  "
                + "        l1.BeginTime, l1.EndTime, l1.Address,   "
                + "        l2.Description, l1.CityID "
                + " FROM dbo.LEvents l1 "
                + " LEFT JOIN dbo.LEvents l2 ON l2.EventID = l1.ParentEventID  "
                + " WHERE l1.EventID = '" + eventId + "'";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 是否在现场
        /// <summary>
        /// 是否在现场
        /// </summary>
        /// <param name="eventId">活动ID</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <returns></returns>
        public int IsSite(string eventId, string latitude, string longitude, float distance)
        {
            string sql = "  "
                + " SELECT COUNT(*) FROM dbo.LEvents "
                + " WHERE EventID = '87747791A95442F5B8D5AC205D51BDC3' "
                + " AND dbo.DISTANCE_TWO_POINTS('" + latitude + "','" + longitude + "',Latitude,Longitude) < " + distance;

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 获取列表信息
        public DataSet GetMessageEventList(LEventsEntity entity)
        {
            string sql = "select a.*  ";
            sql += " ,(select count(EventLevel) from LEvents where EventID=a.EventID) signUpCount ";
            sql += " ,(SELECT SUM(total_amount) FROM dbo.T_Inout WHERE Field18 =a.EventID) salesAmount ";
            sql += " ,DATEDIFF(dd,GETDATE(),a.BeginTime) distanceDays ";
            sql += " from LEvents a ";
            sql += " where a.IsDelete='0' ";
            if (entity.EventID != null && entity.EventID.Trim().Length > 0)
            {
                sql += " and a.EventID='" + entity.EventID + "' ";
            }
            else {
                sql += " and DATEDIFF(dd,GETDATE(),a.BeginTime) > 0";
            }
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 活动相册集合
        /// <summary>
        /// 活动相册集合
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getEventAlbums(ActivityMediaEntity entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getEventAlbumsSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int getEventAlbumsCount(ActivityMediaEntity entity)
        {
            string sql = getEventAlbumsSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string getEventAlbumsSql(ActivityMediaEntity entity)
        {
            string sql = "select a.* ";
            sql += " ,b.FileName,b.Path ";
            sql += " ,displayIndex=row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp from ActivityMedia a ";
            sql += " left join Attachment b on b.AttachmentID=a.AttachmentID ";
            sql += " where a.isDelete='0' ";
            sql += " and a.ActivityID='" + entity.ActivityID.ToString() + "' ";
            sql += " order by displayIndex ";
            return sql;
        }
        #endregion

        #region 活动订单集合
        /// <summary>
        /// 活动订单集合
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getEventOrders(InoutInfo entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getEventOrdersSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int getEventOrdersCount(InoutInfo entity)
        {
            string sql = getEventOrdersSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string getEventOrdersSql(InoutInfo entity)
        {
            string sql = "select a.* ";
            sql += "  ";
            sql += " ,displayIndex=row_number() over(order by a.Create_Time desc) ";
            sql += " into #tmp from t_inout a ";
            sql += " where a.status<>'-1' ";
            sql += " and a.Field18='" + entity.Field18.ToString() + "' ";
            sql += " order by displayIndex ";
            return sql;
        }
        #endregion

        #region 产品销量汇总
        /// <summary>
        /// 产品销量汇总
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getEventItemSales(InoutInfo entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getEventItemSalesSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int getEventItemSalesCount(InoutInfo entity)
        {
            string sql = getEventItemSalesSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string getEventItemSalesSql(InoutInfo entity)
        {
            string sql = "select a.*,c.item_id,d.imageUrl ";
            sql += " ,displayIndex=row_number() over(order by a.Create_Time desc) ";
            sql += " into #tmp from t_inout_detail a ";
            sql += " left join t_inout b on a.order_id=b.order_id ";
            sql += " left join vw_sku c on a.sku_id=c.sku_id ";
            sql += " left join t_item d on d.item_id=c.item_id ";
            sql += " where a.order_detail_status<>'-1' and b.status<>'-1' ";
            sql += " and b.Field18='" + entity.Field18.ToString() + "' ";
            sql += " order by displayIndex ";
            return sql;
        }
        #endregion

        #region 按照年月与活动状态统计活动信息
        /// <summary>
        /// 统计活动信息数量
        /// </summary>
        /// <param name="yearMonth">活动年月(格式：2013-07)</param>
        /// <param name="eventStatus">活动状态(1=已结束，0=未结束)</param>
        /// <returns></returns>
        public int GetEventListCount(string yearMonth, string eventStatus)
        {
            string sql = GetEventListSql(yearMonth, eventStatus);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取统计活动信息列表
        /// </summary>
        /// <param name="yearMonth">活动年月(格式：2013-07)</param>
        /// <param name="eventStatus">活动状态(1=已结束，0=未结束)</param>
        /// <param name="Page">页码</param>
        /// <param name="PageSize">页面数量</param>
        /// <returns></returns>
        public DataSet GetEventList(string yearMonth, string eventStatus, int Page, int PageSize)
        {
            if (PageSize == 0) PageSize = 15;
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventListSql(yearMonth, eventStatus);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 公共查询
        /// </summary>
        /// <param name="yearMonth">活动年月(格式：2013-07)</param>
        /// <param name="eventStatus">活动状态(1=已结束，0=未结束)</param>
        /// <returns></returns>
        private string GetEventListSql(string yearMonth, string eventStatus)
        {
            string sql = string.Empty;
            sql += " SELECT eventId = e.EventID, eventTitle = e.Title, e.BeginTime ";
            sql += " ,EndTime = CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END ";
            sql += " ,salesTotal = ISNULL((SELECT SUM(i1.total_amount) FROM dbo.T_Inout i1 WHERE i1.Field18 = e.EventID),0) ";
            sql += " ,signUpCount = (SELECT COUNT(*) FROM dbo.LEventSignUp s WHERE s.EventID = e.EventID AND s.IsDelete = '0') ";
            sql += " ,salesCount = (SELECT COUNT(DISTINCT ISNULL(vip_no, '')) FROM dbo.T_Inout i2 WHERE i2.Field18 = e.EventID) ";
            sql += " ,eventStartTime = CONVERT(NVARCHAR(20), CONVERT(DATETIME, ISNULL(e.BeginTime,GETDATE())),120) ";
            sql += " ,imageURL = ImageURL ";
            sql += " ,displayIndex = row_number() over(order by e.BeginTime ) ";
            sql += " INTO #tmp ";
            sql += " FROM dbo.LEvents e ";
            sql += " WHERE 1 = 1 AND e.IsDelete = '0' ";

            if (!string.IsNullOrEmpty(yearMonth))
            {
                sql += " AND CONVERT(VARCHAR(7), BeginTime, 120) = '" + yearMonth + "' ";
            }

            if (!string.IsNullOrEmpty(eventStatus))
            {
                if (eventStatus.Equals("1"))    //已结束
                {
                    sql += " AND (BeginTime < GETDATE() AND CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END < GETDATE()) ";
                }
                else    //未结束
                {
                    sql += " AND (BeginTime > GETDATE() OR CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END > GETDATE()) ";
                }
            }

            return sql;
        }
        #endregion

        #region 获取活动详细信息
        /// <summary>
        /// 获取活动详细信息
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <returns></returns>
        public DataSet GetEventDetail(string eventId)
        {
            string sql = "  "
                + " SELECT eventId = e.EventID "
                + " ,eventTitle = e.Title "
                + " ,e.BeginTime ,e.description ,e.imageURL ,e.address"
                + " ,EndTime = CASE ISNULL(e.EndTime, '') WHEN '' THEN CONVERT(VARCHAR(16), DATEADD(DAY, 1, CONVERT(VARCHAR(10), BeginTime, 120)), 120) ELSE EndTime END "
                + " ,salesCount = (SELECT COUNT(DISTINCT ISNULL(vip_no, '')) FROM dbo.T_Inout i2 WHERE i2.Field18 = e.EventID) "
                + " ,totalAmount = ISNULL((SELECT SUM(i1.total_amount) FROM dbo.T_Inout i1 WHERE i1.Field18 = e.EventID),0) "
                + " ,signUpCount = (SELECT COUNT(*) FROM dbo.LEventSignUp s WHERE s.EventID = e.EventID AND s.IsDelete = '0') "
                + " FROM dbo.LEvents e "
                + " WHERE 1 = 1 AND e.IsDelete = '0' "
                + " AND e.EventID = '" + eventId + "' ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取活动报名人员详细信息
        /// <summary>
        /// 统计活动报名人员数量
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <returns></returns>
        public int GetEventSignUpUserInfoCount(string eventId)
        {
            string sql = GetEventSignUpUserInfoSql(eventId);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取活动报名人员列表
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <param name="Page">页码</param>
        /// <param name="PageSize">页面数量</param>
        /// <returns></returns>
        public DataSet GetEventSignUpUserInfo(string eventId, int Page, int PageSize)
        {
            if (PageSize == 0) PageSize = 15;
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventSignUpUserInfoSql(eventId);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 公共查询
        /// </summary>
        /// <param name="eventId">活动标识</param>
        /// <returns></returns>
        private string GetEventSignUpUserInfoSql(string eventId)
        {
            string sql = string.Empty;
            sql += " SELECT vipId = e.VipID ";
            sql += " ,vipName = v.VipName ";
            sql += " ,signUpTime = CONVERT(VARCHAR, e.CreateTime, 120) ";
            sql += " ,displayIndex = row_number() over(order by e.CreateTime DESC ) ";
            sql += " INTO #tmp ";
            sql += " FROM LEventSignUp e ";
            sql += " LEFT JOIN dbo.Vip v ON v.VIPID = e.VipID ";
            sql += " WHERE e.IsDelete = '0' AND EventID = '" + eventId + "' ";

            return sql;
        }
        #endregion

        #region 后台Web获取活动列表
        /// <summary>
        /// 获取活动列表数量
        /// </summary>
        public int WEventGetWebEventsCount(LEventsEntity eventsEntity)
        {
            string sql = WEventGetWebEventsSql(eventsEntity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取活动列表
        /// </summary>
        public DataSet WEventGetWebEvents(LEventsEntity eventsEntity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string pWhere = "";
            if (eventsEntity.ParentEventID != null && eventsEntity.ParentEventID != "")
            {
                pWhere += " and l.ParentEventID = '" + eventsEntity.ParentEventID + "' ";
            }
            if (eventsEntity.Title != null && eventsEntity.Title.Trim().Length > 0)
            {
                pWhere += " and l.Title like '%" + eventsEntity.Title + "%' ";
            }
            if (eventsEntity.StartTimeText != null && eventsEntity.StartTimeText.Trim().Length > 0)
            {
                pWhere += " and convert(datetime, l.beginTime, 120)>=convert(datetime, '" + eventsEntity.StartTimeText + "', 120) ";
            }
            if (eventsEntity.EndTimeText != null && eventsEntity.EndTimeText.Trim().Length > 0)
            {
                pWhere += " and convert(datetime, l.endTime, 120)<=convert(datetime, '" + eventsEntity.EndTimeText + "', 120) ";
            }
            string sql = @"select EventID,COUNT(*)as EventCount into #Temp_A from EventVipTicket 
                    where CustomerID='{2}' and IsDelete=0
                    group by EventID
                    select * from (
                    select  row_number()over(order by createTime desc) as ID,* from(
                    select a.EventCount,l.* from LEvents  as l 
                    left join #Temp_A as a
                    on a.EventID=l.EventID
                    where l.CustomerId='{2}' and l.IsDelete=0
                      {3}
                     ) as ABCD ) as BDAS where ID>={0} and ID<={1}
                     select COUNT(*) from  LEvents as l where l.CustomerId='{2}' and l.IsDelete=0 {3}
                    drop table #Temp_A";
            sql = string.Format(sql, beginSize, endSize,this.CurrentUserInfo.CurrentUser.customer_id,pWhere);  
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取活动列表
        /// </summary>
        public DataSet GetEventsVipData(string pEventID, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = @"select * from (
                            select  row_number()over(order by createTime desc) as ID,* from(
                            select v.*,t.TicketName,t.TicketPrice,evt.Seat,evt.IsSignIn from EventVipTicket as evt
                            left join Vip as v
                            on v.ClientID=evt.CustomerID and v.IsDelete=evt.IsDelete 
                            and v.VIPID=evt.VipID
                            left join Ticket as t
                            on t.CustomerID=evt.CustomerID and evt.TicketID=t.TicketID
                            where evt.CustomerID='{2}'
                            and evt.EventID='{3}'
                            and evt.IsDelete=0)as ABCS) as DSS
                            where ID>={0} and ID<={1}

                            select COUNT(*) from EventVipTicket as evt
                             where evt.CustomerID='{2}'
                            and evt.EventID='{3}'
                            and evt.IsDelete=0
                            ";
            sql = string.Format(sql, beginSize, endSize, this.CurrentUserInfo.CurrentUser.customer_id, pEventID);
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }


        private string WEventGetWebEventsSql(LEventsEntity eventsEntity)
        {
            //var projectName = ConfigurationManager.AppSettings["ProjectName"];
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex asc) ";         

            sql += " ,(select count(*) from WEventUserMapping where IsDelete='0' and eventId=a.eventId) AppliesCount ";
            sql += " ,(select count(*) from LPrizes where IsDelete='0' and eventId=a.eventId) PrizesCount ";
            sql += " ,(select count(*) from LEventRound where IsDelete='0' and eventId=a.eventId) RoundCount ";

            sql += " into #tmp ";
            sql += " from [LEvents] a ";
     
            sql += " where a.IsDelete='0' and a.customerId= '"+this.CurrentUserInfo.CurrentUser.customer_id+"' ";
          
            if (eventsEntity.ParentEventID != null && eventsEntity.ParentEventID != "")
            {
                sql += " and a.ParentEventID = '" + eventsEntity.ParentEventID + "' ";
            }
            if (eventsEntity.Title != null && eventsEntity.Title.Trim().Length > 0)
            {
                sql += " and a.Title like '%" + eventsEntity.Title + "%' ";
            }
            if (eventsEntity.StartTimeText != null && eventsEntity.StartTimeText.Trim().Length > 0)
            {
                sql += " and convert(datetime, a.beginTime, 120)>=convert(datetime, '" + eventsEntity.StartTimeText + "', 120) ";
            }
            if (eventsEntity.EndTimeText != null && eventsEntity.EndTimeText.Trim().Length > 0)
            {
                sql += " and convert(datetime, a.endTime, 120)<=convert(datetime, '" + eventsEntity.EndTimeText + "', 120) ";
            }
            //sql += " order by a.DisplayIndexLast desc ";
            return sql;
        }
        #endregion

        #region 活动报名人员列表
        /// <summary>
        /// 根据标识获取活动人员数量
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int WEventGetEventAppliesCount(string EventID)
        {
            string sql = WEventGetEventAppliesSql(EventID);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 根据标识获取活动人员信息
        /// </summary>
        public DataSet WEventGetEventAppliesList(string EventID, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = WEventGetEventAppliesSql(EventID);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string WEventGetEventAppliesSql(string EventID)
        {
            //BasicUserInfo pUserInfo = new BasicUserInfo();
            //UsersDAO userDAO = new UsersDAO(pUserInfo);

            string sql = "";
            sql += "SELECT a.* "
                + " ,DisplayIndex = row_number() over(order by a.UserName ) "
                + " into #tmp FROM WEventUserMapping a "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            return sql;
        }
        #endregion


        #region 获取我的认购数量
        public int GetMyOrderCount(string eventId, string openId)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_Inout a "
                        + " INNER JOIN dbo.Vip b ON(a.vip_no = b.VIPID) "
                        + " WHERE a.Field18 = '" + eventId + "' "
                        + " AND b.WeiXinUserId = '" + openId + "' ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region Jermyn20130813 微活动移植
        /// <summary>
        /// 微活动移植
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetEventDetailById(string EventID)
        {
            string sql = " select * From LEvents where eventId = '" + EventID + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取返校日活动
        /// <summary>
        /// 获取返校日活动主信息
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetEventFXInfo(string eventId, string userId)
        {
            string sql = "SELECT a.EventID,a.Title,a.Description "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x "
                        + " INNER JOIN dbo.LEvents y ON(x.ObjectId = y.EventID ) "
                        + " WHERE x.OpenId = '"+userId+"'  "
                        + " AND x.IsDelete='0' AND y.EventLevel='2' AND y.IsDelete='0')  haveSignedCount "
                        + " FROM dbo.LEvents a "
                        + " WHERE EventID = '"+eventId+"';";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet getSchoolEventList(string eventId, string userId)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.EventID ,a.Title ,a.Description,a.EventLevel,a.ParentEventID, AllowCount,a.DisplayIndex,a.HaveCount AppliesCount,a.IsSignUp "
                        + " ,CASE WHEN a.AllowCount > a.HaveCount THEN a.AllowCount - a.HaveCount ELSE 0 END OverCount "
                        + " FROM ( "
                        + " SELECT *,CASE WHEN a.PersonCount IS NULL OR a.PersonCount = '' THEN 0 ELSE a.PersonCount END  AllowCount "
                        + " , (SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x WHERE x.ObjectId = a.eventid AND x.IsDelete='0') HaveCount "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM dbo.ZCourseApply x WHERE x.ObjectId = a.eventid AND x.OpenId = '" + userId + "' AND x.IsDelete = '0') IsSignUp "
                        + " FROM dbo.LEvents a WHERE a.ParentEventID = '" + eventId + "' AND a.IsDelete = '0' "
                        + " UNION ALL "
                        + " SELECT * ,CASE WHEN a.PersonCount IS NULL OR a.PersonCount = '' THEN 0 ELSE a.PersonCount END  AllowCount "
                        + " , (SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x WHERE x.ObjectId = a.eventid AND x.IsDelete='0') HaveCount "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM dbo.ZCourseApply x WHERE x.ObjectId = a.eventid AND x.OpenId = '" + userId + "' AND x.IsDelete = '0') IsSignUp "
                        + " FROM dbo.LEvents a "
                        + " WHERE a.ParentEventID IN (SELECT EventID FROM dbo.LEvents WHERE ParentEventID = '" + eventId + "') "
                        + " UNION ALL "
                        + " SELECT * ,CASE WHEN a.PersonCount IS NULL OR a.PersonCount = '' THEN 0 ELSE a.PersonCount END  AllowCount "
                        + " , (SELECT ISNULL(COUNT(*),0) FROM ZCourseApply x WHERE x.ObjectId = a.eventid AND x.IsDelete='0') HaveCount "
                        + " ,(SELECT ISNULL(COUNT(*),0) FROM dbo.ZCourseApply x WHERE x.ObjectId = a.eventid AND x.OpenId = '" + userId + "' AND x.IsDelete = '0') IsSignUp "
                        + " FROM dbo.LEvents a "
                        + " WHERE a.ParentEventID IN (SELECT EventID FROM dbo.LEvents x WHERE x.ParentEventID IN (SELECT EventID FROM dbo.LEvents  WHERE ParentEventID = '" + eventId + "'))"
                        + " AND IsDelete = '0') a ORDER BY a.EventLevel ,a.DisplayIndex";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public bool SetApplyEventDelete(string userId)
        {
            string sql = "UPDATE dbo.ZCourseApply SET IsDelete = '1' WHERE OpenId ='"+userId+"'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region 根据微信的固定二维码获取活动信息
        /// <summary>
        /// 根据微信的固定二维码获取活动信息
        /// </summary>
        /// <param name="wxCode"></param>
        /// <returns></returns>
        public DataSet GetEventInfoByWX(string wxCode)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT TOP 1 a.* FROM dbo.LEvents a "
                    + " INNER JOIN LEventsWX b ON(a.EventID = b.EventID) "
                    + " WHERE b.IsDelete = '0' "
                    + " AND b.wxcode = '"+wxCode+"' "
                    + " AND a.CustomerId = '"+this.CurrentUserInfo.CurrentUser.customer_id+"'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

    }
}
