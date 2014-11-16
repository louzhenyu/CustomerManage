/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/21 13:28:20
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
    /// 表PushAndroidMessage的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PushAndroidMessageDAO : Base.BaseCPOSDAO, ICRUDable<PushAndroidMessageEntity>, IQueryable<PushAndroidMessageEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PushAndroidMessageDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(PushAndroidMessageEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(PushAndroidMessageEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [PushAndroidMessage](");
            strSql.Append("[UserID],[ConnUserID],[ChannelIDBaiDu],[UserIDBaiDu],[PushType],[DeviceType],[Message],[MessageKey],[MessageExpires],[TagName],[ItemType],[ItemID],[SendCount],[BaiduPushAppID],[Status],[FailureReason],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[MessageType],[MessagePushType],[CustomerId],[AndroidMessageID])");
            strSql.Append(" values (");
            strSql.Append("@UserID,@ConnUserID,@ChannelIDBaiDu,@UserIDBaiDu,@PushType,@DeviceType,@Message,@MessageKey,@MessageExpires,@TagName,@ItemType,@ItemID,@SendCount,@BaiduPushAppID,@Status,@FailureReason,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@MessageType,@MessagePushType,@CustomerId,@AndroidMessageID)");            

			string pkString = pEntity.AndroidMessageID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@ConnUserID",SqlDbType.NVarChar),
					new SqlParameter("@ChannelIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@UserIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@PushType",SqlDbType.Int),
					new SqlParameter("@DeviceType",SqlDbType.Int),
					new SqlParameter("@Message",SqlDbType.NVarChar),
					new SqlParameter("@MessageKey",SqlDbType.NVarChar),
					new SqlParameter("@MessageExpires",SqlDbType.Int),
					new SqlParameter("@TagName",SqlDbType.NVarChar),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SendCount",SqlDbType.Int),
					new SqlParameter("@BaiduPushAppID",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@MessageType",SqlDbType.Int),
					new SqlParameter("@MessagePushType",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@AndroidMessageID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UserID;
			parameters[1].Value = pEntity.ConnUserID;
			parameters[2].Value = pEntity.ChannelIDBaiDu;
			parameters[3].Value = pEntity.UserIDBaiDu;
			parameters[4].Value = pEntity.PushType;
			parameters[5].Value = pEntity.DeviceType;
			parameters[6].Value = pEntity.Message;
			parameters[7].Value = pEntity.MessageKey;
			parameters[8].Value = pEntity.MessageExpires;
			parameters[9].Value = pEntity.TagName;
			parameters[10].Value = pEntity.ItemType;
			parameters[11].Value = pEntity.ItemID;
			parameters[12].Value = pEntity.SendCount;
			parameters[13].Value = pEntity.BaiduPushAppID;
			parameters[14].Value = pEntity.Status;
			parameters[15].Value = pEntity.FailureReason;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.IsDelete;
			parameters[21].Value = pEntity.MessageType;
			parameters[22].Value = pEntity.MessagePushType;
			parameters[23].Value = pEntity.CustomerId;
			parameters[24].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.AndroidMessageID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public PushAndroidMessageEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushAndroidMessage] where AndroidMessageID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            PushAndroidMessageEntity m = null;
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
        public PushAndroidMessageEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushAndroidMessage] where isdelete=0");
            //读取数据
            List<PushAndroidMessageEntity> list = new List<PushAndroidMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PushAndroidMessageEntity m;
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
        public void Update(PushAndroidMessageEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(PushAndroidMessageEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AndroidMessageID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PushAndroidMessage] set ");
            if (pIsUpdateNullField || pEntity.UserID!=null)
                strSql.Append( "[UserID]=@UserID,");
            if (pIsUpdateNullField || pEntity.ConnUserID!=null)
                strSql.Append( "[ConnUserID]=@ConnUserID,");
            if (pIsUpdateNullField || pEntity.ChannelIDBaiDu!=null)
                strSql.Append( "[ChannelIDBaiDu]=@ChannelIDBaiDu,");
            if (pIsUpdateNullField || pEntity.UserIDBaiDu!=null)
                strSql.Append( "[UserIDBaiDu]=@UserIDBaiDu,");
            if (pIsUpdateNullField || pEntity.PushType!=null)
                strSql.Append( "[PushType]=@PushType,");
            if (pIsUpdateNullField || pEntity.DeviceType!=null)
                strSql.Append( "[DeviceType]=@DeviceType,");
            if (pIsUpdateNullField || pEntity.Message!=null)
                strSql.Append( "[Message]=@Message,");
            if (pIsUpdateNullField || pEntity.MessageKey!=null)
                strSql.Append( "[MessageKey]=@MessageKey,");
            if (pIsUpdateNullField || pEntity.MessageExpires!=null)
                strSql.Append( "[MessageExpires]=@MessageExpires,");
            if (pIsUpdateNullField || pEntity.TagName!=null)
                strSql.Append( "[TagName]=@TagName,");
            if (pIsUpdateNullField || pEntity.ItemType!=null)
                strSql.Append( "[ItemType]=@ItemType,");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.SendCount!=null)
                strSql.Append( "[SendCount]=@SendCount,");
            if (pIsUpdateNullField || pEntity.BaiduPushAppID!=null)
                strSql.Append( "[BaiduPushAppID]=@BaiduPushAppID,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.FailureReason!=null)
                strSql.Append( "[FailureReason]=@FailureReason,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.MessageType!=null)
                strSql.Append( "[MessageType]=@MessageType,");
            if (pIsUpdateNullField || pEntity.MessagePushType!=null)
                strSql.Append( "[MessagePushType]=@MessagePushType,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where AndroidMessageID=@AndroidMessageID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@ConnUserID",SqlDbType.NVarChar),
					new SqlParameter("@ChannelIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@UserIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@PushType",SqlDbType.Int),
					new SqlParameter("@DeviceType",SqlDbType.Int),
					new SqlParameter("@Message",SqlDbType.NVarChar),
					new SqlParameter("@MessageKey",SqlDbType.NVarChar),
					new SqlParameter("@MessageExpires",SqlDbType.Int),
					new SqlParameter("@TagName",SqlDbType.NVarChar),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SendCount",SqlDbType.Int),
					new SqlParameter("@BaiduPushAppID",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@MessageType",SqlDbType.Int),
					new SqlParameter("@MessagePushType",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@AndroidMessageID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UserID;
			parameters[1].Value = pEntity.ConnUserID;
			parameters[2].Value = pEntity.ChannelIDBaiDu;
			parameters[3].Value = pEntity.UserIDBaiDu;
			parameters[4].Value = pEntity.PushType;
			parameters[5].Value = pEntity.DeviceType;
			parameters[6].Value = pEntity.Message;
			parameters[7].Value = pEntity.MessageKey;
			parameters[8].Value = pEntity.MessageExpires;
			parameters[9].Value = pEntity.TagName;
			parameters[10].Value = pEntity.ItemType;
			parameters[11].Value = pEntity.ItemID;
			parameters[12].Value = pEntity.SendCount;
			parameters[13].Value = pEntity.BaiduPushAppID;
			parameters[14].Value = pEntity.Status;
			parameters[15].Value = pEntity.FailureReason;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.MessageType;
			parameters[19].Value = pEntity.MessagePushType;
			parameters[20].Value = pEntity.CustomerId;
			parameters[21].Value = pEntity.AndroidMessageID;

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
        public void Update(PushAndroidMessageEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(PushAndroidMessageEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PushAndroidMessageEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(PushAndroidMessageEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AndroidMessageID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.AndroidMessageID, pTran);           
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
            sql.AppendLine("update [PushAndroidMessage] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where AndroidMessageID=@AndroidMessageID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@AndroidMessageID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(PushAndroidMessageEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.AndroidMessageID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.AndroidMessageID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(PushAndroidMessageEntity[] pEntities)
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
            sql.AppendLine("update [PushAndroidMessage] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where AndroidMessageID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PushAndroidMessageEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushAndroidMessage] where isdelete=0 ");
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
            List<PushAndroidMessageEntity> list = new List<PushAndroidMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PushAndroidMessageEntity m;
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
        public PagedQueryResult<PushAndroidMessageEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [AndroidMessageID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [PushAndroidMessage] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [PushAndroidMessage] where isdelete=0 ");
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
            PagedQueryResult<PushAndroidMessageEntity> result = new PagedQueryResult<PushAndroidMessageEntity>();
            List<PushAndroidMessageEntity> list = new List<PushAndroidMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PushAndroidMessageEntity m;
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
        public PushAndroidMessageEntity[] QueryByEntity(PushAndroidMessageEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PushAndroidMessageEntity> PagedQueryByEntity(PushAndroidMessageEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PushAndroidMessageEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.AndroidMessageID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AndroidMessageID", Value = pQueryEntity.AndroidMessageID });
            if (pQueryEntity.UserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
            if (pQueryEntity.ConnUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConnUserID", Value = pQueryEntity.ConnUserID });
            if (pQueryEntity.ChannelIDBaiDu!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelIDBaiDu", Value = pQueryEntity.ChannelIDBaiDu });
            if (pQueryEntity.UserIDBaiDu!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserIDBaiDu", Value = pQueryEntity.UserIDBaiDu });
            if (pQueryEntity.PushType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PushType", Value = pQueryEntity.PushType });
            if (pQueryEntity.DeviceType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeviceType", Value = pQueryEntity.DeviceType });
            if (pQueryEntity.Message!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Message", Value = pQueryEntity.Message });
            if (pQueryEntity.MessageKey!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageKey", Value = pQueryEntity.MessageKey });
            if (pQueryEntity.MessageExpires!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageExpires", Value = pQueryEntity.MessageExpires });
            if (pQueryEntity.TagName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TagName", Value = pQueryEntity.TagName });
            if (pQueryEntity.ItemType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemType", Value = pQueryEntity.ItemType });
            if (pQueryEntity.ItemID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.SendCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendCount", Value = pQueryEntity.SendCount });
            if (pQueryEntity.BaiduPushAppID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BaiduPushAppID", Value = pQueryEntity.BaiduPushAppID });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.FailureReason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FailureReason", Value = pQueryEntity.FailureReason });
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
            if (pQueryEntity.MessageType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageType", Value = pQueryEntity.MessageType });
            if (pQueryEntity.MessagePushType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessagePushType", Value = pQueryEntity.MessagePushType });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out PushAndroidMessageEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new PushAndroidMessageEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["AndroidMessageID"] != DBNull.Value)
			{
				pInstance.AndroidMessageID =  Convert.ToString(pReader["AndroidMessageID"]);
			}
			if (pReader["UserID"] != DBNull.Value)
			{
				pInstance.UserID =  Convert.ToString(pReader["UserID"]);
			}
			if (pReader["ConnUserID"] != DBNull.Value)
			{
				pInstance.ConnUserID =  Convert.ToString(pReader["ConnUserID"]);
			}
			if (pReader["ChannelIDBaiDu"] != DBNull.Value)
			{
				pInstance.ChannelIDBaiDu =  Convert.ToString(pReader["ChannelIDBaiDu"]);
			}
			if (pReader["UserIDBaiDu"] != DBNull.Value)
			{
				pInstance.UserIDBaiDu =  Convert.ToString(pReader["UserIDBaiDu"]);
			}
			if (pReader["PushType"] != DBNull.Value)
			{
				pInstance.PushType =   Convert.ToInt32(pReader["PushType"]);
			}
			if (pReader["DeviceType"] != DBNull.Value)
			{
				pInstance.DeviceType =   Convert.ToInt32(pReader["DeviceType"]);
			}
			if (pReader["Message"] != DBNull.Value)
			{
				pInstance.Message =  Convert.ToString(pReader["Message"]);
			}
			if (pReader["MessageKey"] != DBNull.Value)
			{
				pInstance.MessageKey =  Convert.ToString(pReader["MessageKey"]);
			}
			if (pReader["MessageExpires"] != DBNull.Value)
			{
				pInstance.MessageExpires =   Convert.ToInt32(pReader["MessageExpires"]);
			}
			if (pReader["TagName"] != DBNull.Value)
			{
				pInstance.TagName =  Convert.ToString(pReader["TagName"]);
			}
			if (pReader["ItemType"] != DBNull.Value)
			{
				pInstance.ItemType =   Convert.ToInt32(pReader["ItemType"]);
			}
			if (pReader["ItemID"] != DBNull.Value)
			{
				pInstance.ItemID =  Convert.ToString(pReader["ItemID"]);
			}
			if (pReader["SendCount"] != DBNull.Value)
			{
				pInstance.SendCount =   Convert.ToInt32(pReader["SendCount"]);
			}
			if (pReader["BaiduPushAppID"] != DBNull.Value)
			{
				pInstance.BaiduPushAppID =  Convert.ToString(pReader["BaiduPushAppID"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["FailureReason"] != DBNull.Value)
			{
				pInstance.FailureReason =  Convert.ToString(pReader["FailureReason"]);
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
			if (pReader["MessageType"] != DBNull.Value)
			{
				pInstance.MessageType =   Convert.ToInt32(pReader["MessageType"]);
			}
			if (pReader["MessagePushType"] != DBNull.Value)
			{
				pInstance.MessagePushType =   Convert.ToInt32(pReader["MessagePushType"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
