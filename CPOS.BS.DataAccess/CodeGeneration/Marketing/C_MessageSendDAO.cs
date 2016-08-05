/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/7/5 16:49:40
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
    /// 表C_MessageSend的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class C_MessageSendDAO : Base.BaseCPOSDAO, ICRUDable<C_MessageSendEntity>, IQueryable<C_MessageSendEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public C_MessageSendDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(C_MessageSendEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(C_MessageSendEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.IsDelete=0;
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;
			pEntity.CreateBy=CurrentUserInfo.UserID;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [C_MessageSend](");
            strSql.Append("[ActivityID],[MessageID],[VipID],[MessageType],[Content],[OpenID],[Phone],[Email],[SendTime],[ActualSendTime],[Priority],[SendNumber],[CustomerID],[IsSend],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CouponCode],[CouponTypeName],[DateDiffDesc],[CouponCategory],[SendID])");
            strSql.Append(" values (");
            strSql.Append("@ActivityID,@MessageID,@VipID,@MessageType,@Content,@OpenID,@Phone,@Email,@SendTime,@ActualSendTime,@Priority,@SendNumber,@CustomerID,@IsSend,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CouponCode,@CouponTypeName,@DateDiffDesc,@CouponCategory,@SendID)");            

			Guid? pkGuid;
			if (pEntity.SendID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.SendID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ActivityID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@MessageID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@MessageType",SqlDbType.Char),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@OpenID",SqlDbType.VarChar),
					new SqlParameter("@Phone",SqlDbType.VarChar),
					new SqlParameter("@Email",SqlDbType.VarChar),
					new SqlParameter("@SendTime",SqlDbType.DateTime),
					new SqlParameter("@ActualSendTime",SqlDbType.DateTime),
					new SqlParameter("@Priority",SqlDbType.Int),
					new SqlParameter("@SendNumber",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@IsSend",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CouponCode",SqlDbType.VarChar),
					new SqlParameter("@CouponTypeName",SqlDbType.NVarChar),
					new SqlParameter("@DateDiffDesc",SqlDbType.VarChar),
					new SqlParameter("@CouponCategory",SqlDbType.VarChar),
					new SqlParameter("@SendID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.ActivityID;
			parameters[1].Value = pEntity.MessageID;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.MessageType;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.OpenID;
			parameters[6].Value = pEntity.Phone;
			parameters[7].Value = pEntity.Email;
			parameters[8].Value = pEntity.SendTime;
			parameters[9].Value = pEntity.ActualSendTime;
			parameters[10].Value = pEntity.Priority;
			parameters[11].Value = pEntity.SendNumber;
			parameters[12].Value = pEntity.CustomerID;
			parameters[13].Value = pEntity.IsSend;
			parameters[14].Value = pEntity.CreateBy;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pEntity.CouponCode;
			parameters[20].Value = pEntity.CouponTypeName;
			parameters[21].Value = pEntity.DateDiffDesc;
			parameters[22].Value = pEntity.CouponCategory;
			parameters[23].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SendID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public C_MessageSendEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [C_MessageSend] where SendID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            C_MessageSendEntity m = null;
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
        public C_MessageSendEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [C_MessageSend] where 1=1  and isdelete=0");
            //读取数据
            List<C_MessageSendEntity> list = new List<C_MessageSendEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    C_MessageSendEntity m;
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
        public void Update(C_MessageSendEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(C_MessageSendEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SendID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [C_MessageSend] set ");
                        if (pIsUpdateNullField || pEntity.ActivityID!=null)
                strSql.Append( "[ActivityID]=@ActivityID,");
            if (pIsUpdateNullField || pEntity.MessageID!=null)
                strSql.Append( "[MessageID]=@MessageID,");
            if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
            if (pIsUpdateNullField || pEntity.MessageType!=null)
                strSql.Append( "[MessageType]=@MessageType,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.OpenID!=null)
                strSql.Append( "[OpenID]=@OpenID,");
            if (pIsUpdateNullField || pEntity.Phone!=null)
                strSql.Append( "[Phone]=@Phone,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.SendTime!=null)
                strSql.Append( "[SendTime]=@SendTime,");
            if (pIsUpdateNullField || pEntity.ActualSendTime!=null)
                strSql.Append( "[ActualSendTime]=@ActualSendTime,");
            if (pIsUpdateNullField || pEntity.Priority!=null)
                strSql.Append( "[Priority]=@Priority,");
            if (pIsUpdateNullField || pEntity.SendNumber!=null)
                strSql.Append( "[SendNumber]=@SendNumber,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.IsSend!=null)
                strSql.Append( "[IsSend]=@IsSend,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CouponCode!=null)
                strSql.Append( "[CouponCode]=@CouponCode,");
            if (pIsUpdateNullField || pEntity.CouponTypeName!=null)
                strSql.Append( "[CouponTypeName]=@CouponTypeName,");
            if (pIsUpdateNullField || pEntity.DateDiffDesc!=null)
                strSql.Append( "[DateDiffDesc]=@DateDiffDesc,");
            if (pIsUpdateNullField || pEntity.CouponCategory!=null)
                strSql.Append( "[CouponCategory]=@CouponCategory");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where SendID=@SendID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ActivityID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@MessageID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@MessageType",SqlDbType.Char),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@OpenID",SqlDbType.VarChar),
					new SqlParameter("@Phone",SqlDbType.VarChar),
					new SqlParameter("@Email",SqlDbType.VarChar),
					new SqlParameter("@SendTime",SqlDbType.DateTime),
					new SqlParameter("@ActualSendTime",SqlDbType.DateTime),
					new SqlParameter("@Priority",SqlDbType.Int),
					new SqlParameter("@SendNumber",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@IsSend",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CouponCode",SqlDbType.VarChar),
					new SqlParameter("@CouponTypeName",SqlDbType.NVarChar),
					new SqlParameter("@DateDiffDesc",SqlDbType.VarChar),
					new SqlParameter("@CouponCategory",SqlDbType.VarChar),
					new SqlParameter("@SendID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.ActivityID;
			parameters[1].Value = pEntity.MessageID;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.MessageType;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.OpenID;
			parameters[6].Value = pEntity.Phone;
			parameters[7].Value = pEntity.Email;
			parameters[8].Value = pEntity.SendTime;
			parameters[9].Value = pEntity.ActualSendTime;
			parameters[10].Value = pEntity.Priority;
			parameters[11].Value = pEntity.SendNumber;
			parameters[12].Value = pEntity.CustomerID;
			parameters[13].Value = pEntity.IsSend;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.CouponCode;
			parameters[17].Value = pEntity.CouponTypeName;
			parameters[18].Value = pEntity.DateDiffDesc;
			parameters[19].Value = pEntity.CouponCategory;
			parameters[20].Value = pEntity.SendID;

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
        public void Update(C_MessageSendEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(C_MessageSendEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(C_MessageSendEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SendID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.SendID.Value, pTran);           
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
            sql.AppendLine("update [C_MessageSend] set  isdelete=1 where SendID=@SendID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SendID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(C_MessageSendEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.SendID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.SendID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(C_MessageSendEntity[] pEntities)
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
            sql.AppendLine("update [C_MessageSend] set  isdelete=1 where SendID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public C_MessageSendEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [C_MessageSend] where 1=1  and isdelete=0 ");
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
            List<C_MessageSendEntity> list = new List<C_MessageSendEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    C_MessageSendEntity m;
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
        public PagedQueryResult<C_MessageSendEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SendID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [C_MessageSend] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [C_MessageSend] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<C_MessageSendEntity> result = new PagedQueryResult<C_MessageSendEntity>();
            List<C_MessageSendEntity> list = new List<C_MessageSendEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    C_MessageSendEntity m;
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
        public C_MessageSendEntity[] QueryByEntity(C_MessageSendEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<C_MessageSendEntity> PagedQueryByEntity(C_MessageSendEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(C_MessageSendEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SendID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendID", Value = pQueryEntity.SendID });
            if (pQueryEntity.ActivityID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityID", Value = pQueryEntity.ActivityID });
            if (pQueryEntity.MessageID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageID", Value = pQueryEntity.MessageID });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.MessageType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageType", Value = pQueryEntity.MessageType });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.OpenID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenID", Value = pQueryEntity.OpenID });
            if (pQueryEntity.Phone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Phone", Value = pQueryEntity.Phone });
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.SendTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendTime", Value = pQueryEntity.SendTime });
            if (pQueryEntity.ActualSendTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActualSendTime", Value = pQueryEntity.ActualSendTime });
            if (pQueryEntity.Priority!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Priority", Value = pQueryEntity.Priority });
            if (pQueryEntity.SendNumber!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendNumber", Value = pQueryEntity.SendNumber });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.IsSend!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSend", Value = pQueryEntity.IsSend });
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
            if (pQueryEntity.CouponCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponCode", Value = pQueryEntity.CouponCode });
            if (pQueryEntity.CouponTypeName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponTypeName", Value = pQueryEntity.CouponTypeName });
            if (pQueryEntity.DateDiffDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateDiffDesc", Value = pQueryEntity.DateDiffDesc });
            if (pQueryEntity.CouponCategory!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponCategory", Value = pQueryEntity.CouponCategory });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out C_MessageSendEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new C_MessageSendEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SendID"] != DBNull.Value)
			{
				pInstance.SendID =  (Guid)pReader["SendID"];
			}
			if (pReader["ActivityID"] != DBNull.Value)
			{
				pInstance.ActivityID =  (Guid)pReader["ActivityID"];
			}
			if (pReader["MessageID"] != DBNull.Value)
			{
				pInstance.MessageID =  (Guid)pReader["MessageID"];
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
			}
			if (pReader["MessageType"] != DBNull.Value)
			{
				pInstance.MessageType =Convert.ToString(pReader["MessageType"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["OpenID"] != DBNull.Value)
			{
				pInstance.OpenID =  Convert.ToString(pReader["OpenID"]);
			}
			if (pReader["Phone"] != DBNull.Value)
			{
				pInstance.Phone =  Convert.ToString(pReader["Phone"]);
			}
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["SendTime"] != DBNull.Value)
			{
				pInstance.SendTime =  Convert.ToDateTime(pReader["SendTime"]);
			}
			if (pReader["ActualSendTime"] != DBNull.Value)
			{
				pInstance.ActualSendTime =  Convert.ToDateTime(pReader["ActualSendTime"]);
			}
			if (pReader["Priority"] != DBNull.Value)
			{
				pInstance.Priority =   Convert.ToInt32(pReader["Priority"]);
			}
			if (pReader["SendNumber"] != DBNull.Value)
			{
				pInstance.SendNumber =   Convert.ToInt32(pReader["SendNumber"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["IsSend"] != DBNull.Value)
			{
				pInstance.IsSend =   Convert.ToInt32(pReader["IsSend"]);
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
			if (pReader["CouponCode"] != DBNull.Value)
			{
				pInstance.CouponCode =  Convert.ToString(pReader["CouponCode"]);
			}
			if (pReader["CouponTypeName"] != DBNull.Value)
			{
				pInstance.CouponTypeName =  Convert.ToString(pReader["CouponTypeName"]);
			}
			if (pReader["DateDiffDesc"] != DBNull.Value)
			{
				pInstance.DateDiffDesc =  Convert.ToString(pReader["DateDiffDesc"]);
			}
			if (pReader["CouponCategory"] != DBNull.Value)
			{
				pInstance.CouponCategory =  Convert.ToString(pReader["CouponCategory"]);
			}

        }
        #endregion
    }
}
