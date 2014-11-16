/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/24 17:30:30
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
    /// 表WXHouseReservationPay的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXHouseReservationPayDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseReservationPayEntity>, IQueryable<WXHouseReservationPayEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseReservationPayDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WXHouseReservationPayEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WXHouseReservationPayEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXHouseReservationPay](");
            strSql.Append("[PrePaymentID],[SeqNO],[Fundtype],[FundState],[Merchantdate],[Hatradedt],[HaorderNO],[Retcode],[Retmsg],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ReservationPayID])");
            strSql.Append(" values (");
            strSql.Append("@PrePaymentID,@SeqNO,@Fundtype,@FundState,@Merchantdate,@Hatradedt,@HaorderNO,@Retcode,@Retmsg,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ReservationPayID)");            

			Guid? pkGuid;
			if (pEntity.ReservationPayID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ReservationPayID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrePaymentID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SeqNO",SqlDbType.NVarChar),
					new SqlParameter("@Fundtype",SqlDbType.Int),
					new SqlParameter("@FundState",SqlDbType.Int),
					new SqlParameter("@Merchantdate",SqlDbType.VarChar),
					new SqlParameter("@Hatradedt",SqlDbType.Int),
					new SqlParameter("@HaorderNO",SqlDbType.NVarChar),
					new SqlParameter("@Retcode",SqlDbType.VarChar),
					new SqlParameter("@Retmsg",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ReservationPayID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.PrePaymentID;
			parameters[1].Value = pEntity.SeqNO;
			parameters[2].Value = pEntity.Fundtype;
			parameters[3].Value = pEntity.FundState;
			parameters[4].Value = pEntity.Merchantdate;
			parameters[5].Value = pEntity.Hatradedt;
			parameters[6].Value = pEntity.HaorderNO;
			parameters[7].Value = pEntity.Retcode;
			parameters[8].Value = pEntity.Retmsg;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ReservationPayID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WXHouseReservationPayEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseReservationPay] where ReservationPayID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            WXHouseReservationPayEntity m = null;
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
        public WXHouseReservationPayEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseReservationPay] where isdelete=0");
            //读取数据
            List<WXHouseReservationPayEntity> list = new List<WXHouseReservationPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseReservationPayEntity m;
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
        public void Update(WXHouseReservationPayEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WXHouseReservationPayEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReservationPayID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXHouseReservationPay] set ");
            if (pIsUpdateNullField || pEntity.PrePaymentID!=null)
                strSql.Append( "[PrePaymentID]=@PrePaymentID,");
            if (pIsUpdateNullField || pEntity.SeqNO!=null)
                strSql.Append( "[SeqNO]=@SeqNO,");
            if (pIsUpdateNullField || pEntity.Fundtype!=null)
                strSql.Append( "[Fundtype]=@Fundtype,");
            if (pIsUpdateNullField || pEntity.FundState!=null)
                strSql.Append( "[FundState]=@FundState,");
            if (pIsUpdateNullField || pEntity.Merchantdate!=null)
                strSql.Append( "[Merchantdate]=@Merchantdate,");
            if (pIsUpdateNullField || pEntity.Hatradedt!=null)
                strSql.Append( "[Hatradedt]=@Hatradedt,");
            if (pIsUpdateNullField || pEntity.HaorderNO!=null)
                strSql.Append( "[HaorderNO]=@HaorderNO,");
            if (pIsUpdateNullField || pEntity.Retcode!=null)
                strSql.Append( "[Retcode]=@Retcode,");
            if (pIsUpdateNullField || pEntity.Retmsg!=null)
                strSql.Append( "[Retmsg]=@Retmsg,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ReservationPayID=@ReservationPayID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrePaymentID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SeqNO",SqlDbType.NVarChar),
					new SqlParameter("@Fundtype",SqlDbType.Int),
					new SqlParameter("@FundState",SqlDbType.Int),
					new SqlParameter("@Merchantdate",SqlDbType.VarChar),
					new SqlParameter("@Hatradedt",SqlDbType.Int),
					new SqlParameter("@HaorderNO",SqlDbType.NVarChar),
					new SqlParameter("@Retcode",SqlDbType.VarChar),
					new SqlParameter("@Retmsg",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ReservationPayID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.PrePaymentID;
			parameters[1].Value = pEntity.SeqNO;
			parameters[2].Value = pEntity.Fundtype;
			parameters[3].Value = pEntity.FundState;
			parameters[4].Value = pEntity.Merchantdate;
			parameters[5].Value = pEntity.Hatradedt;
			parameters[6].Value = pEntity.HaorderNO;
			parameters[7].Value = pEntity.Retcode;
			parameters[8].Value = pEntity.Retmsg;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.ReservationPayID;

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
        public void Update(WXHouseReservationPayEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WXHouseReservationPayEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXHouseReservationPayEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WXHouseReservationPayEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReservationPayID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ReservationPayID, pTran);           
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
            sql.AppendLine("update [WXHouseReservationPay] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ReservationPayID=@ReservationPayID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ReservationPayID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXHouseReservationPayEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ReservationPayID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.ReservationPayID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WXHouseReservationPayEntity[] pEntities)
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
            sql.AppendLine("update [WXHouseReservationPay] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ReservationPayID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXHouseReservationPayEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseReservationPay] where isdelete=0 ");
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
            List<WXHouseReservationPayEntity> list = new List<WXHouseReservationPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseReservationPayEntity m;
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
        public PagedQueryResult<WXHouseReservationPayEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ReservationPayID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXHouseReservationPay] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WXHouseReservationPay] where isdelete=0 ");
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
            PagedQueryResult<WXHouseReservationPayEntity> result = new PagedQueryResult<WXHouseReservationPayEntity>();
            List<WXHouseReservationPayEntity> list = new List<WXHouseReservationPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseReservationPayEntity m;
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
        public WXHouseReservationPayEntity[] QueryByEntity(WXHouseReservationPayEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXHouseReservationPayEntity> PagedQueryByEntity(WXHouseReservationPayEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXHouseReservationPayEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ReservationPayID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationPayID", Value = pQueryEntity.ReservationPayID });
            if (pQueryEntity.PrePaymentID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrePaymentID", Value = pQueryEntity.PrePaymentID });
            if (pQueryEntity.SeqNO!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SeqNO", Value = pQueryEntity.SeqNO });
            if (pQueryEntity.Fundtype!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Fundtype", Value = pQueryEntity.Fundtype });
            if (pQueryEntity.FundState!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FundState", Value = pQueryEntity.FundState });
            if (pQueryEntity.Merchantdate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Merchantdate", Value = pQueryEntity.Merchantdate });
            if (pQueryEntity.Hatradedt!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Hatradedt", Value = pQueryEntity.Hatradedt });
            if (pQueryEntity.HaorderNO!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HaorderNO", Value = pQueryEntity.HaorderNO });
            if (pQueryEntity.Retcode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Retcode", Value = pQueryEntity.Retcode });
            if (pQueryEntity.Retmsg!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Retmsg", Value = pQueryEntity.Retmsg });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out WXHouseReservationPayEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WXHouseReservationPayEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ReservationPayID"] != DBNull.Value)
			{
				pInstance.ReservationPayID =  (Guid)pReader["ReservationPayID"];
			}
			if (pReader["PrePaymentID"] != DBNull.Value)
			{
				pInstance.PrePaymentID =  (Guid)pReader["PrePaymentID"];
			}
			if (pReader["SeqNO"] != DBNull.Value)
			{
				pInstance.SeqNO =  Convert.ToString(pReader["SeqNO"]);
			}
			if (pReader["Fundtype"] != DBNull.Value)
			{
				pInstance.Fundtype =   Convert.ToInt32(pReader["Fundtype"]);
			}
			if (pReader["FundState"] != DBNull.Value)
			{
				pInstance.FundState =   Convert.ToInt32(pReader["FundState"]);
			}
			if (pReader["Merchantdate"] != DBNull.Value)
			{
				pInstance.Merchantdate =  Convert.ToString(pReader["Merchantdate"]);
			}
			if (pReader["Hatradedt"] != DBNull.Value)
			{
				pInstance.Hatradedt =   Convert.ToInt32(pReader["Hatradedt"]);
			}
			if (pReader["HaorderNO"] != DBNull.Value)
			{
				pInstance.HaorderNO =  Convert.ToString(pReader["HaorderNO"]);
			}
			if (pReader["Retcode"] != DBNull.Value)
			{
				pInstance.Retcode =  Convert.ToString(pReader["Retcode"]);
			}
			if (pReader["Retmsg"] != DBNull.Value)
			{
				pInstance.Retmsg =  Convert.ToString(pReader["Retmsg"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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

        }
        #endregion
    }
}
