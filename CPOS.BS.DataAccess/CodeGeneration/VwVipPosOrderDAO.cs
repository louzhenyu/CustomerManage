/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/9 16:52:55
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
    /// 表vwVipPosOrder的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VwVipPosOrderDAO : Base.BaseCPOSDAO, ICRUDable<VwVipPosOrderEntity>, IQueryable<VwVipPosOrderEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VwVipPosOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VwVipPosOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VwVipPosOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [vwVipPosOrder](");
            strSql.Append("[OrderNo],[Phone],[DeliveryAddress],[DeliveryTime],[VipName],[TotalAmount],[RewardAmount],[DeliveryName],[FansAwards],[TransactionAwards],[RewardTotalAmount],[SalesUnitId],[PurchaseUnitId],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[StatusId],[StatusDesc],[SalesUnitName],[PurchaseUnitName],[TotalQty],[OrderId])");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@Phone,@DeliveryAddress,@DeliveryTime,@VipName,@TotalAmount,@RewardAmount,@DeliveryName,@FansAwards,@TransactionAwards,@RewardTotalAmount,@SalesUnitId,@PurchaseUnitId,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@StatusId,@StatusDesc,@SalesUnitName,@PurchaseUnitName,@TotalQty,@OrderId)");            

			string pkString = pEntity.OrderId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryAddress",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryTime",SqlDbType.NVarChar),
					new SqlParameter("@VipName",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@RewardAmount",SqlDbType.Decimal),
					new SqlParameter("@DeliveryName",SqlDbType.NVarChar),
					new SqlParameter("@FansAwards",SqlDbType.NVarChar),
					new SqlParameter("@TransactionAwards",SqlDbType.NVarChar),
					new SqlParameter("@RewardTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesUnitId",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseUnitId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@StatusId",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@SalesUnitName",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseUnitName",SqlDbType.NVarChar),
					new SqlParameter("@TotalQty",SqlDbType.Int),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.Phone;
			parameters[2].Value = pEntity.DeliveryAddress;
			parameters[3].Value = pEntity.DeliveryTime;
			parameters[4].Value = pEntity.VipName;
			parameters[5].Value = pEntity.TotalAmount;
			parameters[6].Value = pEntity.RewardAmount;
			parameters[7].Value = pEntity.DeliveryName;
			parameters[8].Value = pEntity.FansAwards;
			parameters[9].Value = pEntity.TransactionAwards;
			parameters[10].Value = pEntity.RewardTotalAmount;
			parameters[11].Value = pEntity.SalesUnitId;
			parameters[12].Value = pEntity.PurchaseUnitId;
			parameters[13].Value = pEntity.CreateTime;
			parameters[14].Value = pEntity.CreateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.IsDelete;
			parameters[18].Value = pEntity.StatusId;
			parameters[19].Value = pEntity.StatusDesc;
			parameters[20].Value = pEntity.SalesUnitName;
			parameters[21].Value = pEntity.PurchaseUnitName;
			parameters[22].Value = pEntity.TotalQty;
			parameters[23].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VwVipPosOrderEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwVipPosOrder] where OrderId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            VwVipPosOrderEntity m = null;
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
        public VwVipPosOrderEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwVipPosOrder] where isdelete=0");
            //读取数据
            List<VwVipPosOrderEntity> list = new List<VwVipPosOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwVipPosOrderEntity m;
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
        public void Update(VwVipPosOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VwVipPosOrderEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [vwVipPosOrder] set ");
            if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.Phone!=null)
                strSql.Append( "[Phone]=@Phone,");
            if (pIsUpdateNullField || pEntity.DeliveryAddress!=null)
                strSql.Append( "[DeliveryAddress]=@DeliveryAddress,");
            if (pIsUpdateNullField || pEntity.DeliveryTime!=null)
                strSql.Append( "[DeliveryTime]=@DeliveryTime,");
            if (pIsUpdateNullField || pEntity.VipName!=null)
                strSql.Append( "[VipName]=@VipName,");
            if (pIsUpdateNullField || pEntity.TotalAmount!=null)
                strSql.Append( "[TotalAmount]=@TotalAmount,");
            if (pIsUpdateNullField || pEntity.RewardAmount!=null)
                strSql.Append( "[RewardAmount]=@RewardAmount,");
            if (pIsUpdateNullField || pEntity.DeliveryName!=null)
                strSql.Append( "[DeliveryName]=@DeliveryName,");
            if (pIsUpdateNullField || pEntity.FansAwards!=null)
                strSql.Append( "[FansAwards]=@FansAwards,");
            if (pIsUpdateNullField || pEntity.TransactionAwards!=null)
                strSql.Append( "[TransactionAwards]=@TransactionAwards,");
            if (pIsUpdateNullField || pEntity.RewardTotalAmount!=null)
                strSql.Append( "[RewardTotalAmount]=@RewardTotalAmount,");
            if (pIsUpdateNullField || pEntity.SalesUnitId!=null)
                strSql.Append( "[SalesUnitId]=@SalesUnitId,");
            if (pIsUpdateNullField || pEntity.PurchaseUnitId!=null)
                strSql.Append( "[PurchaseUnitId]=@PurchaseUnitId,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.StatusId!=null)
                strSql.Append( "[StatusId]=@StatusId,");
            if (pIsUpdateNullField || pEntity.StatusDesc!=null)
                strSql.Append( "[StatusDesc]=@StatusDesc,");
            if (pIsUpdateNullField || pEntity.SalesUnitName!=null)
                strSql.Append( "[SalesUnitName]=@SalesUnitName,");
            if (pIsUpdateNullField || pEntity.PurchaseUnitName!=null)
                strSql.Append( "[PurchaseUnitName]=@PurchaseUnitName,");
            if (pIsUpdateNullField || pEntity.TotalQty!=null)
                strSql.Append( "[TotalQty]=@TotalQty");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryAddress",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryTime",SqlDbType.NVarChar),
					new SqlParameter("@VipName",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@RewardAmount",SqlDbType.Decimal),
					new SqlParameter("@DeliveryName",SqlDbType.NVarChar),
					new SqlParameter("@FansAwards",SqlDbType.NVarChar),
					new SqlParameter("@TransactionAwards",SqlDbType.NVarChar),
					new SqlParameter("@RewardTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesUnitId",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseUnitId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@StatusId",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@SalesUnitName",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseUnitName",SqlDbType.NVarChar),
					new SqlParameter("@TotalQty",SqlDbType.Int),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.Phone;
			parameters[2].Value = pEntity.DeliveryAddress;
			parameters[3].Value = pEntity.DeliveryTime;
			parameters[4].Value = pEntity.VipName;
			parameters[5].Value = pEntity.TotalAmount;
			parameters[6].Value = pEntity.RewardAmount;
			parameters[7].Value = pEntity.DeliveryName;
			parameters[8].Value = pEntity.FansAwards;
			parameters[9].Value = pEntity.TransactionAwards;
			parameters[10].Value = pEntity.RewardTotalAmount;
			parameters[11].Value = pEntity.SalesUnitId;
			parameters[12].Value = pEntity.PurchaseUnitId;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.StatusId;
			parameters[16].Value = pEntity.StatusDesc;
			parameters[17].Value = pEntity.SalesUnitName;
			parameters[18].Value = pEntity.PurchaseUnitName;
			parameters[19].Value = pEntity.TotalQty;
			parameters[20].Value = pEntity.OrderId;

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
        public void Update(VwVipPosOrderEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VwVipPosOrderEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VwVipPosOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VwVipPosOrderEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.OrderId, pTran);           
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
            sql.AppendLine("update [vwVipPosOrder] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderId=@OrderId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VwVipPosOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.OrderId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.OrderId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VwVipPosOrderEntity[] pEntities)
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
            sql.AppendLine("update [vwVipPosOrder] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where OrderId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VwVipPosOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwVipPosOrder] where isdelete=0 ");
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
            List<VwVipPosOrderEntity> list = new List<VwVipPosOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwVipPosOrderEntity m;
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
        public PagedQueryResult<VwVipPosOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VwVipPosOrder] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VwVipPosOrder] where isdelete=0 ");
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
            PagedQueryResult<VwVipPosOrderEntity> result = new PagedQueryResult<VwVipPosOrderEntity>();
            List<VwVipPosOrderEntity> list = new List<VwVipPosOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VwVipPosOrderEntity m;
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
        public VwVipPosOrderEntity[] QueryByEntity(VwVipPosOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VwVipPosOrderEntity> PagedQueryByEntity(VwVipPosOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VwVipPosOrderEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.Phone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Phone", Value = pQueryEntity.Phone });
            if (pQueryEntity.DeliveryAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryAddress", Value = pQueryEntity.DeliveryAddress });
            if (pQueryEntity.DeliveryTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryTime", Value = pQueryEntity.DeliveryTime });
            if (pQueryEntity.VipName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipName", Value = pQueryEntity.VipName });
            if (pQueryEntity.TotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAmount", Value = pQueryEntity.TotalAmount });
            if (pQueryEntity.RewardAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardAmount", Value = pQueryEntity.RewardAmount });
            if (pQueryEntity.DeliveryName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryName", Value = pQueryEntity.DeliveryName });
            if (pQueryEntity.FansAwards!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FansAwards", Value = pQueryEntity.FansAwards });
            if (pQueryEntity.TransactionAwards!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransactionAwards", Value = pQueryEntity.TransactionAwards });
            if (pQueryEntity.RewardTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardTotalAmount", Value = pQueryEntity.RewardTotalAmount });
            if (pQueryEntity.SalesUnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesUnitId", Value = pQueryEntity.SalesUnitId });
            if (pQueryEntity.PurchaseUnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseUnitId", Value = pQueryEntity.PurchaseUnitId });
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
            if (pQueryEntity.StatusId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusId", Value = pQueryEntity.StatusId });
            if (pQueryEntity.StatusDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusDesc", Value = pQueryEntity.StatusDesc });
            if (pQueryEntity.SalesUnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesUnitName", Value = pQueryEntity.SalesUnitName });
            if (pQueryEntity.PurchaseUnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseUnitName", Value = pQueryEntity.PurchaseUnitName });
            if (pQueryEntity.TotalQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalQty", Value = pQueryEntity.TotalQty });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out VwVipPosOrderEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VwVipPosOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["Phone"] != DBNull.Value)
			{
				pInstance.Phone =  Convert.ToString(pReader["Phone"]);
			}
			if (pReader["DeliveryAddress"] != DBNull.Value)
			{
				pInstance.DeliveryAddress =  Convert.ToString(pReader["DeliveryAddress"]);
			}
			if (pReader["DeliveryTime"] != DBNull.Value)
			{
				pInstance.DeliveryTime =  Convert.ToString(pReader["DeliveryTime"]);
			}
			if (pReader["VipName"] != DBNull.Value)
			{
				pInstance.VipName =  Convert.ToString(pReader["VipName"]);
			}
			if (pReader["TotalAmount"] != DBNull.Value)
			{
				pInstance.TotalAmount =  Convert.ToDecimal(pReader["TotalAmount"]);
			}
			if (pReader["RewardAmount"] != DBNull.Value)
			{
				pInstance.RewardAmount =  Convert.ToDecimal(pReader["RewardAmount"]);
			}
			if (pReader["DeliveryName"] != DBNull.Value)
			{
				pInstance.DeliveryName =  Convert.ToString(pReader["DeliveryName"]);
			}
			if (pReader["FansAwards"] != DBNull.Value)
			{
				pInstance.FansAwards =  Convert.ToString(pReader["FansAwards"]);
			}
			if (pReader["TransactionAwards"] != DBNull.Value)
			{
				pInstance.TransactionAwards =  Convert.ToString(pReader["TransactionAwards"]);
			}
			if (pReader["RewardTotalAmount"] != DBNull.Value)
			{
				pInstance.RewardTotalAmount =  Convert.ToDecimal(pReader["RewardTotalAmount"]);
			}
			if (pReader["SalesUnitId"] != DBNull.Value)
			{
				pInstance.SalesUnitId =  Convert.ToString(pReader["SalesUnitId"]);
			}
			if (pReader["PurchaseUnitId"] != DBNull.Value)
			{
				pInstance.PurchaseUnitId =  Convert.ToString(pReader["PurchaseUnitId"]);
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
			if (pReader["StatusId"] != DBNull.Value)
			{
				pInstance.StatusId =  Convert.ToString(pReader["StatusId"]);
			}
			if (pReader["StatusDesc"] != DBNull.Value)
			{
				pInstance.StatusDesc =  Convert.ToString(pReader["StatusDesc"]);
			}
			if (pReader["SalesUnitName"] != DBNull.Value)
			{
				pInstance.SalesUnitName =  Convert.ToString(pReader["SalesUnitName"]);
			}
			if (pReader["PurchaseUnitName"] != DBNull.Value)
			{
				pInstance.PurchaseUnitName =  Convert.ToString(pReader["PurchaseUnitName"]);
			}
			if (pReader["TotalQty"] != DBNull.Value)
			{
				pInstance.TotalQty =   Convert.ToInt32(pReader["TotalQty"]);
			}

        }
        #endregion
    }
}
