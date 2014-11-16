/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/16 18:04:02
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
    /// 表WApplicationInterface的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WApplicationInterfaceDAO : Base.BaseCPOSDAO, ICRUDable<WApplicationInterfaceEntity>, IQueryable<WApplicationInterfaceEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WApplicationInterfaceDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WApplicationInterfaceEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WApplicationInterfaceEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WApplicationInterface](");
            strSql.Append(@"[WeiXinName],[WeiXinID],[URL],[Token],[AppID],[AppSecret],[ServerIP],[FileAddress],[IsHeight],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[LoginUser],[LoginPass],[CustomerId],[WeiXinTypeId],[AuthUrl],[RequestToken],[ExpirationTime],[IsMoreCS],[ApplicationId]
                    ,[PrevEncodingAESKey],[CurrentEncodingAESKey],[EncryptType])");
            strSql.Append(" values (");
            strSql.Append(@"@WeiXinName,@WeiXinID,@URL,@Token,@AppID,@AppSecret,@ServerIP,@FileAddress,@IsHeight,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@LoginUser,@LoginPass,@CustomerId,@WeiXinTypeId,@AuthUrl,@RequestToken,@ExpirationTime,@IsMoreCS,@ApplicationId
                        ,@PrevEncodingAESKey,@CurrentEncodingAESKey,@EncryptType)");            

			string pkString = pEntity.ApplicationId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinName",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@Token",SqlDbType.NVarChar),
					new SqlParameter("@AppID",SqlDbType.NVarChar),
					new SqlParameter("@AppSecret",SqlDbType.NVarChar),
					new SqlParameter("@ServerIP",SqlDbType.NVarChar),
					new SqlParameter("@FileAddress",SqlDbType.NVarChar),
					new SqlParameter("@IsHeight",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@LoginUser",SqlDbType.NVarChar),
					new SqlParameter("@LoginPass",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinTypeId",SqlDbType.NVarChar),
					new SqlParameter("@AuthUrl",SqlDbType.NVarChar),
					new SqlParameter("@RequestToken",SqlDbType.NVarChar),
					new SqlParameter("@ExpirationTime",SqlDbType.DateTime),
					new SqlParameter("@IsMoreCS",SqlDbType.Int),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
                    //加密所需字段，2014-10-21 zoukun
                    new SqlParameter("@PrevEncodingAESKey",SqlDbType.VarChar),
                    new SqlParameter("@CurrentEncodingAESKey",SqlDbType.VarChar),
                    new SqlParameter("@EncryptType",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.WeiXinName;
			parameters[1].Value = pEntity.WeiXinID;
			parameters[2].Value = pEntity.URL;
			parameters[3].Value = pEntity.Token;
			parameters[4].Value = pEntity.AppID;
			parameters[5].Value = pEntity.AppSecret;
			parameters[6].Value = pEntity.ServerIP;
			parameters[7].Value = pEntity.FileAddress;
			parameters[8].Value = pEntity.IsHeight;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.LoginUser;
			parameters[15].Value = pEntity.LoginPass;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pEntity.WeiXinTypeId;
			parameters[18].Value = pEntity.AuthUrl;
			parameters[19].Value = pEntity.RequestToken;
			parameters[20].Value = pEntity.ExpirationTime;
			parameters[21].Value = pEntity.IsMoreCS;
			parameters[22].Value = pkString;
            parameters[23].Value = pEntity.PrevEncodingAESKey;
            parameters[24].Value = pEntity.CurrentEncodingAESKey;
            parameters[25].Value = pEntity.EncryptType;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ApplicationId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WApplicationInterfaceEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WApplicationInterface] where ApplicationId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            WApplicationInterfaceEntity m = null;
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
        public WApplicationInterfaceEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WApplicationInterface] where isdelete=0");
            //读取数据
            List<WApplicationInterfaceEntity> list = new List<WApplicationInterfaceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WApplicationInterfaceEntity m;
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
        public void Update(WApplicationInterfaceEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WApplicationInterfaceEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ApplicationId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WApplicationInterface] set ");
            if (pIsUpdateNullField || pEntity.WeiXinName!=null)
                strSql.Append( "[WeiXinName]=@WeiXinName,");
            if (pIsUpdateNullField || pEntity.WeiXinID!=null)
                strSql.Append( "[WeiXinID]=@WeiXinID,");
            if (pIsUpdateNullField || pEntity.URL!=null)
                strSql.Append( "[URL]=@URL,");
            if (pIsUpdateNullField || pEntity.Token!=null)
                strSql.Append( "[Token]=@Token,");
            if (pIsUpdateNullField || pEntity.AppID!=null)
                strSql.Append( "[AppID]=@AppID,");
            if (pIsUpdateNullField || pEntity.AppSecret!=null)
                strSql.Append( "[AppSecret]=@AppSecret,");
            if (pIsUpdateNullField || pEntity.ServerIP!=null)
                strSql.Append( "[ServerIP]=@ServerIP,");
            if (pIsUpdateNullField || pEntity.FileAddress!=null)
                strSql.Append( "[FileAddress]=@FileAddress,");
            if (pIsUpdateNullField || pEntity.IsHeight!=null)
                strSql.Append( "[IsHeight]=@IsHeight,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LoginUser!=null)
                strSql.Append( "[LoginUser]=@LoginUser,");
            if (pIsUpdateNullField || pEntity.LoginPass!=null)
                strSql.Append( "[LoginPass]=@LoginPass,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.WeiXinTypeId!=null)
                strSql.Append( "[WeiXinTypeId]=@WeiXinTypeId,");
            if (pIsUpdateNullField || pEntity.AuthUrl!=null)
                strSql.Append( "[AuthUrl]=@AuthUrl,");
            if (pIsUpdateNullField || pEntity.RequestToken!=null)
                strSql.Append( "[RequestToken]=@RequestToken,");
            if (pIsUpdateNullField || pEntity.ExpirationTime!=null)
                strSql.Append( "[ExpirationTime]=@ExpirationTime,");
            if (pIsUpdateNullField || pEntity.IsMoreCS!=null)
                strSql.Append( "[IsMoreCS]=@IsMoreCS,");
            if (pIsUpdateNullField || pEntity.PrevEncodingAESKey != null)
                strSql.Append("[PrevEncodingAESKey]=@PrevEncodingAESKey,");
            if (pIsUpdateNullField || pEntity.CurrentEncodingAESKey != null)
                strSql.Append("[CurrentEncodingAESKey]=@CurrentEncodingAESKey,");
            if (pIsUpdateNullField || pEntity.EncryptType != null)
                strSql.Append("[EncryptType]=@EncryptType");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ApplicationId=@ApplicationId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinName",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@Token",SqlDbType.NVarChar),
					new SqlParameter("@AppID",SqlDbType.NVarChar),
					new SqlParameter("@AppSecret",SqlDbType.NVarChar),
					new SqlParameter("@ServerIP",SqlDbType.NVarChar),
					new SqlParameter("@FileAddress",SqlDbType.NVarChar),
					new SqlParameter("@IsHeight",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LoginUser",SqlDbType.NVarChar),
					new SqlParameter("@LoginPass",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinTypeId",SqlDbType.NVarChar),
					new SqlParameter("@AuthUrl",SqlDbType.NVarChar),
					new SqlParameter("@RequestToken",SqlDbType.NVarChar),
					new SqlParameter("@ExpirationTime",SqlDbType.DateTime),
					new SqlParameter("@IsMoreCS",SqlDbType.Int),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
                    //加密字段 2014-10-21 zoukun
                    new SqlParameter("@PrevEncodingAESKey",SqlDbType.VarChar),
                    new SqlParameter("@CurrentEncodingAESKey",SqlDbType.VarChar),
                    new SqlParameter("@EncryptType",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.WeiXinName;
			parameters[1].Value = pEntity.WeiXinID;
			parameters[2].Value = pEntity.URL;
			parameters[3].Value = pEntity.Token;
			parameters[4].Value = pEntity.AppID;
			parameters[5].Value = pEntity.AppSecret;
			parameters[6].Value = pEntity.ServerIP;
			parameters[7].Value = pEntity.FileAddress;
			parameters[8].Value = pEntity.IsHeight;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.LoginUser;
			parameters[12].Value = pEntity.LoginPass;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.WeiXinTypeId;
			parameters[15].Value = pEntity.AuthUrl;
			parameters[16].Value = pEntity.RequestToken;
			parameters[17].Value = pEntity.ExpirationTime;
			parameters[18].Value = pEntity.IsMoreCS;
			parameters[19].Value = pEntity.ApplicationId;
            parameters[20].Value = pEntity.PrevEncodingAESKey;
            parameters[21].Value = pEntity.CurrentEncodingAESKey;
            parameters[22].Value = pEntity.EncryptType;

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
        public void Update(WApplicationInterfaceEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WApplicationInterfaceEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WApplicationInterfaceEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WApplicationInterfaceEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ApplicationId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ApplicationId, pTran);           
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
            sql.AppendLine("update [WApplicationInterface] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ApplicationId=@ApplicationId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ApplicationId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WApplicationInterfaceEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ApplicationId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.ApplicationId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WApplicationInterfaceEntity[] pEntities)
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
            sql.AppendLine("update [WApplicationInterface] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ApplicationId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WApplicationInterfaceEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WApplicationInterface] where isdelete=0 ");
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
            List<WApplicationInterfaceEntity> list = new List<WApplicationInterfaceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WApplicationInterfaceEntity m;
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
        public PagedQueryResult<WApplicationInterfaceEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ApplicationId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WApplicationInterface] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WApplicationInterface] where isdelete=0 ");
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
            PagedQueryResult<WApplicationInterfaceEntity> result = new PagedQueryResult<WApplicationInterfaceEntity>();
            List<WApplicationInterfaceEntity> list = new List<WApplicationInterfaceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WApplicationInterfaceEntity m;
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
        public WApplicationInterfaceEntity[] QueryByEntity(WApplicationInterfaceEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WApplicationInterfaceEntity> PagedQueryByEntity(WApplicationInterfaceEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WApplicationInterfaceEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ApplicationId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApplicationId", Value = pQueryEntity.ApplicationId });
            if (pQueryEntity.WeiXinName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinName", Value = pQueryEntity.WeiXinName });
            if (pQueryEntity.WeiXinID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinID", Value = pQueryEntity.WeiXinID });
            if (pQueryEntity.URL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "URL", Value = pQueryEntity.URL });
            if (pQueryEntity.Token!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Token", Value = pQueryEntity.Token });
            if (pQueryEntity.AppID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppID", Value = pQueryEntity.AppID });
            if (pQueryEntity.AppSecret!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppSecret", Value = pQueryEntity.AppSecret });
            if (pQueryEntity.ServerIP!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServerIP", Value = pQueryEntity.ServerIP });
            if (pQueryEntity.FileAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FileAddress", Value = pQueryEntity.FileAddress });
            if (pQueryEntity.IsHeight!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsHeight", Value = pQueryEntity.IsHeight });
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
            if (pQueryEntity.LoginUser!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LoginUser", Value = pQueryEntity.LoginUser });
            if (pQueryEntity.LoginPass!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LoginPass", Value = pQueryEntity.LoginPass });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.WeiXinTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinTypeId", Value = pQueryEntity.WeiXinTypeId });
            if (pQueryEntity.AuthUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AuthUrl", Value = pQueryEntity.AuthUrl });
            if (pQueryEntity.RequestToken!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RequestToken", Value = pQueryEntity.RequestToken });
            if (pQueryEntity.ExpirationTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExpirationTime", Value = pQueryEntity.ExpirationTime });
            if (pQueryEntity.IsMoreCS!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMoreCS", Value = pQueryEntity.IsMoreCS });
            if (pQueryEntity.PrevEncodingAESKey != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrevEncodingAESKey", Value = pQueryEntity.PrevEncodingAESKey });
            if (pQueryEntity.CurrentEncodingAESKey != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentEncodingAESKey", Value = pQueryEntity.CurrentEncodingAESKey });
            if (pQueryEntity.EncryptType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EncryptType", Value = pQueryEntity.EncryptType });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out WApplicationInterfaceEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WApplicationInterfaceEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ApplicationId"] != DBNull.Value)
			{
				pInstance.ApplicationId =  Convert.ToString(pReader["ApplicationId"]);
			}
			if (pReader["WeiXinName"] != DBNull.Value)
			{
				pInstance.WeiXinName =  Convert.ToString(pReader["WeiXinName"]);
			}
			if (pReader["WeiXinID"] != DBNull.Value)
			{
				pInstance.WeiXinID =  Convert.ToString(pReader["WeiXinID"]);
			}
			if (pReader["URL"] != DBNull.Value)
			{
				pInstance.URL =  Convert.ToString(pReader["URL"]);
			}
			if (pReader["Token"] != DBNull.Value)
			{
				pInstance.Token =  Convert.ToString(pReader["Token"]);
			}
			if (pReader["AppID"] != DBNull.Value)
			{
				pInstance.AppID =  Convert.ToString(pReader["AppID"]);
			}
			if (pReader["AppSecret"] != DBNull.Value)
			{
				pInstance.AppSecret =  Convert.ToString(pReader["AppSecret"]);
			}
			if (pReader["ServerIP"] != DBNull.Value)
			{
				pInstance.ServerIP =  Convert.ToString(pReader["ServerIP"]);
			}
			if (pReader["FileAddress"] != DBNull.Value)
			{
				pInstance.FileAddress =  Convert.ToString(pReader["FileAddress"]);
			}
			if (pReader["IsHeight"] != DBNull.Value)
			{
				pInstance.IsHeight =   Convert.ToInt32(pReader["IsHeight"]);
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
			if (pReader["LoginUser"] != DBNull.Value)
			{
				pInstance.LoginUser =  Convert.ToString(pReader["LoginUser"]);
			}
			if (pReader["LoginPass"] != DBNull.Value)
			{
				pInstance.LoginPass =  Convert.ToString(pReader["LoginPass"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["WeiXinTypeId"] != DBNull.Value)
			{
				pInstance.WeiXinTypeId =  Convert.ToString(pReader["WeiXinTypeId"]);
			}
			if (pReader["AuthUrl"] != DBNull.Value)
			{
				pInstance.AuthUrl =  Convert.ToString(pReader["AuthUrl"]);
			}
			if (pReader["RequestToken"] != DBNull.Value)
			{
				pInstance.RequestToken =  Convert.ToString(pReader["RequestToken"]);
			}
			if (pReader["ExpirationTime"] != DBNull.Value)
			{
				pInstance.ExpirationTime =  Convert.ToDateTime(pReader["ExpirationTime"]);
			}
			if (pReader["IsMoreCS"] != DBNull.Value)
			{
				pInstance.IsMoreCS =   Convert.ToInt32(pReader["IsMoreCS"]);
			}
            //if (pReader["PrevEncodingAESKey"] != DBNull.Value)
            //{
            //    pInstance.PrevEncodingAESKey = Convert.ToString(pReader["PrevEncodingAESKey"]);
            //}
            //if (pReader["CurrentEncodingAESKey"] != DBNull.Value)
            //{
            //    pInstance.CurrentEncodingAESKey = Convert.ToString(pReader["CurrentEncodingAESKey"]);
            //}
            //if (pReader["EncryptType"] != DBNull.Value)
            //{
            //    pInstance.EncryptType = Convert.ToInt32(pReader["EncryptType"]);
            //}

        }
        #endregion
    }
}
