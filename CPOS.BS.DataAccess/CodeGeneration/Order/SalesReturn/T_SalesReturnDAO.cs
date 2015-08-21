/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-3 10:30:22
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
    /// 表T_SalesReturn的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SalesReturnDAO : Base.BaseCPOSDAO, ICRUDable<T_SalesReturnEntity>, IQueryable<T_SalesReturnEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SalesReturnDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_SalesReturnEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_SalesReturnEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_SalesReturn](");
            strSql.Append("[SalesReturnNo],[VipID],[ServicesType],[DeliveryType],[OrderID],[ItemID],[SkuID],[Qty],[ActualQty],[RefundAmount],[ConfirmAmount],[UnitID],[UnitName],[UnitTel],[Address],[Contacts],[Phone],[Reason],[Status],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[SalesReturnID])");
            strSql.Append(" values (");
            strSql.Append("@SalesReturnNo,@VipID,@ServicesType,@DeliveryType,@OrderID,@ItemID,@SkuID,@Qty,@ActualQty,@RefundAmount,@ConfirmAmount,@UnitID,@UnitName,@UnitTel,@Address,@Contacts,@Phone,@Reason,@Status,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@SalesReturnID)");            

			Guid? pkGuid;
			if (pEntity.SalesReturnID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.SalesReturnID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesReturnNo",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@ServicesType",SqlDbType.Int),
					new SqlParameter("@DeliveryType",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@ActualQty",SqlDbType.Int),
					new SqlParameter("@RefundAmount",SqlDbType.Decimal),
					new SqlParameter("@ConfirmAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Contacts",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@SalesReturnID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SalesReturnNo;
			parameters[1].Value = pEntity.VipID;
			parameters[2].Value = pEntity.ServicesType;
			parameters[3].Value = pEntity.DeliveryType;
			parameters[4].Value = pEntity.OrderID;
			parameters[5].Value = pEntity.ItemID;
			parameters[6].Value = pEntity.SkuID;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.ActualQty;
			parameters[9].Value = pEntity.RefundAmount;
			parameters[10].Value = pEntity.ConfirmAmount;
			parameters[11].Value = pEntity.UnitID;
			parameters[12].Value = pEntity.UnitName;
			parameters[13].Value = pEntity.UnitTel;
			parameters[14].Value = pEntity.Address;
			parameters[15].Value = pEntity.Contacts;
			parameters[16].Value = pEntity.Phone;
			parameters[17].Value = pEntity.Reason;
			parameters[18].Value = pEntity.Status;
			parameters[19].Value = pEntity.CustomerID;
			parameters[20].Value = pEntity.CreateTime;
			parameters[21].Value = pEntity.CreateBy;
			parameters[22].Value = pEntity.LastUpdateTime;
			parameters[23].Value = pEntity.LastUpdateBy;
			parameters[24].Value = pEntity.IsDelete;
			parameters[25].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SalesReturnID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_SalesReturnEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SalesReturn] where SalesReturnID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            T_SalesReturnEntity m = null;
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
        public T_SalesReturnEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SalesReturn] where 1=1  and isdelete=0");
            //读取数据
            List<T_SalesReturnEntity> list = new List<T_SalesReturnEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SalesReturnEntity m;
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
        public void Update(T_SalesReturnEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_SalesReturnEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SalesReturnID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_SalesReturn] set ");
                        if (pIsUpdateNullField || pEntity.SalesReturnNo!=null)
                strSql.Append( "[SalesReturnNo]=@SalesReturnNo,");
            if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
            if (pIsUpdateNullField || pEntity.ServicesType!=null)
                strSql.Append( "[ServicesType]=@ServicesType,");
            if (pIsUpdateNullField || pEntity.DeliveryType!=null)
                strSql.Append( "[DeliveryType]=@DeliveryType,");
            if (pIsUpdateNullField || pEntity.OrderID!=null)
                strSql.Append( "[OrderID]=@OrderID,");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.SkuID!=null)
                strSql.Append( "[SkuID]=@SkuID,");
            if (pIsUpdateNullField || pEntity.Qty!=null)
                strSql.Append( "[Qty]=@Qty,");
            if (pIsUpdateNullField || pEntity.ActualQty!=null)
                strSql.Append( "[ActualQty]=@ActualQty,");
            if (pIsUpdateNullField || pEntity.RefundAmount!=null)
                strSql.Append( "[RefundAmount]=@RefundAmount,");
            if (pIsUpdateNullField || pEntity.ConfirmAmount!=null)
                strSql.Append( "[ConfirmAmount]=@ConfirmAmount,");
            if (pIsUpdateNullField || pEntity.UnitID!=null)
                strSql.Append( "[UnitID]=@UnitID,");
            if (pIsUpdateNullField || pEntity.UnitName!=null)
                strSql.Append( "[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.UnitTel!=null)
                strSql.Append( "[UnitTel]=@UnitTel,");
            if (pIsUpdateNullField || pEntity.Address!=null)
                strSql.Append( "[Address]=@Address,");
            if (pIsUpdateNullField || pEntity.Contacts!=null)
                strSql.Append( "[Contacts]=@Contacts,");
            if (pIsUpdateNullField || pEntity.Phone!=null)
                strSql.Append( "[Phone]=@Phone,");
            if (pIsUpdateNullField || pEntity.Reason!=null)
                strSql.Append( "[Reason]=@Reason,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where SalesReturnID=@SalesReturnID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesReturnNo",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@ServicesType",SqlDbType.Int),
					new SqlParameter("@DeliveryType",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@ActualQty",SqlDbType.Int),
					new SqlParameter("@RefundAmount",SqlDbType.Decimal),
					new SqlParameter("@ConfirmAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Contacts",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@SalesReturnID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SalesReturnNo;
			parameters[1].Value = pEntity.VipID;
			parameters[2].Value = pEntity.ServicesType;
			parameters[3].Value = pEntity.DeliveryType;
			parameters[4].Value = pEntity.OrderID;
			parameters[5].Value = pEntity.ItemID;
			parameters[6].Value = pEntity.SkuID;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.ActualQty;
			parameters[9].Value = pEntity.RefundAmount;
			parameters[10].Value = pEntity.ConfirmAmount;
			parameters[11].Value = pEntity.UnitID;
			parameters[12].Value = pEntity.UnitName;
			parameters[13].Value = pEntity.UnitTel;
			parameters[14].Value = pEntity.Address;
			parameters[15].Value = pEntity.Contacts;
			parameters[16].Value = pEntity.Phone;
			parameters[17].Value = pEntity.Reason;
			parameters[18].Value = pEntity.Status;
			parameters[19].Value = pEntity.CustomerID;
			parameters[20].Value = pEntity.LastUpdateTime;
			parameters[21].Value = pEntity.LastUpdateBy;
			parameters[22].Value = pEntity.SalesReturnID;

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
        public void Update(T_SalesReturnEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_SalesReturnEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_SalesReturnEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SalesReturnID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.SalesReturnID.Value, pTran);           
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
            sql.AppendLine("update [T_SalesReturn] set  isdelete=1 where SalesReturnID=@SalesReturnID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SalesReturnID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_SalesReturnEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.SalesReturnID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.SalesReturnID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_SalesReturnEntity[] pEntities)
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
            sql.AppendLine("update [T_SalesReturn] set  isdelete=1 where SalesReturnID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_SalesReturnEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SalesReturn] where 1=1  and isdelete=0 ");
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
            List<T_SalesReturnEntity> list = new List<T_SalesReturnEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SalesReturnEntity m;
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
        public PagedQueryResult<T_SalesReturnEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SalesReturnID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_SalesReturn] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_SalesReturn] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_SalesReturnEntity> result = new PagedQueryResult<T_SalesReturnEntity>();
            List<T_SalesReturnEntity> list = new List<T_SalesReturnEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SalesReturnEntity m;
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
        public T_SalesReturnEntity[] QueryByEntity(T_SalesReturnEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_SalesReturnEntity> PagedQueryByEntity(T_SalesReturnEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_SalesReturnEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SalesReturnID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesReturnID", Value = pQueryEntity.SalesReturnID });
            if (pQueryEntity.SalesReturnNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesReturnNo", Value = pQueryEntity.SalesReturnNo });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.ServicesType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServicesType", Value = pQueryEntity.ServicesType });
            if (pQueryEntity.DeliveryType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryType", Value = pQueryEntity.DeliveryType });
            if (pQueryEntity.OrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.ItemID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.SkuID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuID", Value = pQueryEntity.SkuID });
            if (pQueryEntity.Qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.ActualQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActualQty", Value = pQueryEntity.ActualQty });
            if (pQueryEntity.RefundAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefundAmount", Value = pQueryEntity.RefundAmount });
            if (pQueryEntity.ConfirmAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConfirmAmount", Value = pQueryEntity.ConfirmAmount });
            if (pQueryEntity.UnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.UnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.UnitTel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitTel", Value = pQueryEntity.UnitTel });
            if (pQueryEntity.Address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Address", Value = pQueryEntity.Address });
            if (pQueryEntity.Contacts!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Contacts", Value = pQueryEntity.Contacts });
            if (pQueryEntity.Phone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Phone", Value = pQueryEntity.Phone });
            if (pQueryEntity.Reason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Reason", Value = pQueryEntity.Reason });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_SalesReturnEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_SalesReturnEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SalesReturnID"] != DBNull.Value)
			{
				pInstance.SalesReturnID =  (Guid)pReader["SalesReturnID"];
			}
			if (pReader["SalesReturnNo"] != DBNull.Value)
			{
				pInstance.SalesReturnNo =  Convert.ToString(pReader["SalesReturnNo"]);
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
                var vipDao = new VipDAO(this.CurrentUserInfo);
                var vipInfo=vipDao.GetByID(pInstance.VipID);
                if (vipInfo != null)
                    pInstance.VipName = vipInfo.VipName;
			}
			if (pReader["ServicesType"] != DBNull.Value)
			{
				pInstance.ServicesType =   Convert.ToInt32(pReader["ServicesType"]);
			}
			if (pReader["DeliveryType"] != DBNull.Value)
			{
				pInstance.DeliveryType =   Convert.ToInt32(pReader["DeliveryType"]);
			}
			if (pReader["OrderID"] != DBNull.Value)
			{
				pInstance.OrderID =  Convert.ToString(pReader["OrderID"]);
                var inoutDAO = new T_InoutDAO(CurrentUserInfo);
                var inoutInfo = inoutDAO.GetByID(pInstance.OrderID);
                if (inoutInfo != null)
                {
                    var paymentTypeDAO = new TPaymentTypeDAO(CurrentUserInfo);
                    var paymentTypeInfo = paymentTypeDAO.GetByID(inoutInfo.pay_id);
                    if (paymentTypeInfo != null)
                    {
                        pInstance.PayTypeName = paymentTypeInfo.PaymentTypeName;//支付方式名称
                    }
                }
			}
			if (pReader["ItemID"] != DBNull.Value)
			{
				pInstance.ItemID =  Convert.ToString(pReader["ItemID"]);
			}
			if (pReader["SkuID"] != DBNull.Value)
			{
				pInstance.SkuID =  Convert.ToString(pReader["SkuID"]);
			}
			if (pReader["Qty"] != DBNull.Value)
			{
				pInstance.Qty =   Convert.ToInt32(pReader["Qty"]);
			}
			if (pReader["ActualQty"] != DBNull.Value)
			{
				pInstance.ActualQty =   Convert.ToInt32(pReader["ActualQty"]);
			}
			if (pReader["RefundAmount"] != DBNull.Value)
			{
				pInstance.RefundAmount =  Convert.ToDecimal(pReader["RefundAmount"]);
			}
			if (pReader["ConfirmAmount"] != DBNull.Value)
			{
				pInstance.ConfirmAmount =  Convert.ToDecimal(pReader["ConfirmAmount"]);
			}
			if (pReader["UnitID"] != DBNull.Value)
			{
				pInstance.UnitID =  Convert.ToString(pReader["UnitID"]);
			}
			if (pReader["UnitName"] != DBNull.Value)
			{
				pInstance.UnitName =  Convert.ToString(pReader["UnitName"]);
			}
			if (pReader["UnitTel"] != DBNull.Value)
			{
				pInstance.UnitTel =  Convert.ToString(pReader["UnitTel"]);
			}
			if (pReader["Address"] != DBNull.Value)
			{
				pInstance.Address =  Convert.ToString(pReader["Address"]);
			}
			if (pReader["Contacts"] != DBNull.Value)
			{
				pInstance.Contacts =  Convert.ToString(pReader["Contacts"]);
			}
			if (pReader["Phone"] != DBNull.Value)
			{
				pInstance.Phone =  Convert.ToString(pReader["Phone"]);
			}
			if (pReader["Reason"] != DBNull.Value)
			{
				pInstance.Reason =  Convert.ToString(pReader["Reason"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
