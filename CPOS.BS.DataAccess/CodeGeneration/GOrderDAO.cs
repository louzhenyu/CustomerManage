/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/27 14:39:44
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
    /// 表GOrder的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class GOrderDAO : Base.BaseCPOSDAO, ICRUDable<GOrderEntity>, IQueryable<GOrderEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(GOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(GOrderEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;
            if (pEntity.CreateBy == null)
            {
                pEntity.CreateBy = CurrentUserInfo.UserID;
            }
            
            pEntity.LastUpdateTime = pEntity.CreateTime;
            if (pEntity.LastUpdateBy == null)
            {
                pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            }
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [GOrder](");
            strSql.Append("[OrderCode],[VipId],[OpenId],[Lng],[Lat],[Address],[Qty],[Phone],[Status],[StatusDesc],[ReceiptVipId],[ReceiptOpenId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[FirstPushTime],[SecondPushTime],[ReceiptOrderTime],[AcquiringTime],[UnitId],[OrderId])");
            strSql.Append(" values (");
            strSql.Append("@OrderCode,@VipId,@OpenId,@Lng,@Lat,@Address,@Qty,@Phone,@Status,@StatusDesc,@ReceiptVipId,@ReceiptOpenId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@FirstPushTime,@SecondPushTime,@ReceiptOrderTime,@AcquiringTime,@UnitId,@OrderId)");            

			string pkString = pEntity.OrderId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderCode",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@Lng",SqlDbType.NVarChar),
					new SqlParameter("@Lat",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@ReceiptVipId",SqlDbType.NVarChar),
					new SqlParameter("@ReceiptOpenId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@FirstPushTime",SqlDbType.DateTime),
					new SqlParameter("@SecondPushTime",SqlDbType.DateTime),
					new SqlParameter("@ReceiptOrderTime",SqlDbType.DateTime),
					new SqlParameter("@AcquiringTime",SqlDbType.DateTime),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderCode;
			parameters[1].Value = pEntity.VipId;
			parameters[2].Value = pEntity.OpenId;
			parameters[3].Value = pEntity.Lng;
			parameters[4].Value = pEntity.Lat;
			parameters[5].Value = pEntity.Address;
			parameters[6].Value = pEntity.Qty;
			parameters[7].Value = pEntity.Phone;
			parameters[8].Value = pEntity.Status;
			parameters[9].Value = pEntity.StatusDesc;
			parameters[10].Value = pEntity.ReceiptVipId;
			parameters[11].Value = pEntity.ReceiptOpenId;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.CreateBy;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pEntity.FirstPushTime;
			parameters[18].Value = pEntity.SecondPushTime;
			parameters[19].Value = pEntity.ReceiptOrderTime;
			parameters[20].Value = pEntity.AcquiringTime;
			parameters[21].Value = pEntity.UnitId;
			parameters[22].Value = pkString;

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
        public GOrderEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [GOrder] where OrderId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            GOrderEntity m = null;
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
        public GOrderEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [GOrder] where isdelete=0");
            //读取数据
            List<GOrderEntity> list = new List<GOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    GOrderEntity m;
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
        public void Update(GOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(GOrderEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
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
            strSql.Append("update [GOrder] set ");
            if (pIsUpdateNullField || pEntity.OrderCode!=null)
                strSql.Append( "[OrderCode]=@OrderCode,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.OpenId!=null)
                strSql.Append( "[OpenId]=@OpenId,");
            if (pIsUpdateNullField || pEntity.Lng!=null)
                strSql.Append( "[Lng]=@Lng,");
            if (pIsUpdateNullField || pEntity.Lat!=null)
                strSql.Append( "[Lat]=@Lat,");
            if (pIsUpdateNullField || pEntity.Address!=null)
                strSql.Append( "[Address]=@Address,");
            if (pIsUpdateNullField || pEntity.Qty!=null)
                strSql.Append( "[Qty]=@Qty,");
            if (pIsUpdateNullField || pEntity.Phone!=null)
                strSql.Append( "[Phone]=@Phone,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.StatusDesc!=null)
                strSql.Append( "[StatusDesc]=@StatusDesc,");
            if (pIsUpdateNullField || pEntity.ReceiptVipId!=null)
                strSql.Append( "[ReceiptVipId]=@ReceiptVipId,");
            if (pIsUpdateNullField || pEntity.ReceiptOpenId!=null)
                strSql.Append( "[ReceiptOpenId]=@ReceiptOpenId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.FirstPushTime!=null)
                strSql.Append( "[FirstPushTime]=@FirstPushTime,");
            if (pIsUpdateNullField || pEntity.SecondPushTime!=null)
                strSql.Append( "[SecondPushTime]=@SecondPushTime,");
            if (pIsUpdateNullField || pEntity.ReceiptOrderTime!=null)
                strSql.Append( "[ReceiptOrderTime]=@ReceiptOrderTime,");
            if (pIsUpdateNullField || pEntity.AcquiringTime!=null)
                strSql.Append( "[AcquiringTime]=@AcquiringTime,");
            if (pIsUpdateNullField || pEntity.UnitId!=null)
                strSql.Append( "[UnitId]=@UnitId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderCode",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@Lng",SqlDbType.NVarChar),
					new SqlParameter("@Lat",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@ReceiptVipId",SqlDbType.NVarChar),
					new SqlParameter("@ReceiptOpenId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@FirstPushTime",SqlDbType.DateTime),
					new SqlParameter("@SecondPushTime",SqlDbType.DateTime),
					new SqlParameter("@ReceiptOrderTime",SqlDbType.DateTime),
					new SqlParameter("@AcquiringTime",SqlDbType.DateTime),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderCode;
			parameters[1].Value = pEntity.VipId;
			parameters[2].Value = pEntity.OpenId;
			parameters[3].Value = pEntity.Lng;
			parameters[4].Value = pEntity.Lat;
			parameters[5].Value = pEntity.Address;
			parameters[6].Value = pEntity.Qty;
			parameters[7].Value = pEntity.Phone;
			parameters[8].Value = pEntity.Status;
			parameters[9].Value = pEntity.StatusDesc;
			parameters[10].Value = pEntity.ReceiptVipId;
			parameters[11].Value = pEntity.ReceiptOpenId;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.FirstPushTime;
			parameters[15].Value = pEntity.SecondPushTime;
			parameters[16].Value = pEntity.ReceiptOrderTime;
			parameters[17].Value = pEntity.AcquiringTime;
			parameters[18].Value = pEntity.UnitId;
			parameters[19].Value = pEntity.OrderId;

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
        public void Update(GOrderEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(GOrderEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(GOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(GOrderEntity pEntity, IDbTransaction pTran)
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
            sql.AppendLine("update [GOrder] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderId=@OrderId;");
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
        public void Delete(GOrderEntity[] pEntities, IDbTransaction pTran)
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
        public void Delete(GOrderEntity[] pEntities)
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
            sql.AppendLine("update [GOrder] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where OrderId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public GOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [GOrder] where isdelete=0 ");
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
            List<GOrderEntity> list = new List<GOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    GOrderEntity m;
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
        public PagedQueryResult<GOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
            pagedSql.AppendFormat(") as ___rn,* from [GOrder] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [GOrder] where isdelete=0 ");
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
            PagedQueryResult<GOrderEntity> result = new PagedQueryResult<GOrderEntity>();
            List<GOrderEntity> list = new List<GOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    GOrderEntity m;
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
        public GOrderEntity[] QueryByEntity(GOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<GOrderEntity> PagedQueryByEntity(GOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(GOrderEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.OrderCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderCode", Value = pQueryEntity.OrderCode });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });
            if (pQueryEntity.Lng!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Lng", Value = pQueryEntity.Lng });
            if (pQueryEntity.Lat!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Lat", Value = pQueryEntity.Lat });
            if (pQueryEntity.Address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Address", Value = pQueryEntity.Address });
            if (pQueryEntity.Qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.Phone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Phone", Value = pQueryEntity.Phone });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.StatusDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusDesc", Value = pQueryEntity.StatusDesc });
            if (pQueryEntity.ReceiptVipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceiptVipId", Value = pQueryEntity.ReceiptVipId });
            if (pQueryEntity.ReceiptOpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceiptOpenId", Value = pQueryEntity.ReceiptOpenId });
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
            if (pQueryEntity.FirstPushTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstPushTime", Value = pQueryEntity.FirstPushTime });
            if (pQueryEntity.SecondPushTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SecondPushTime", Value = pQueryEntity.SecondPushTime });
            if (pQueryEntity.ReceiptOrderTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceiptOrderTime", Value = pQueryEntity.ReceiptOrderTime });
            if (pQueryEntity.AcquiringTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AcquiringTime", Value = pQueryEntity.AcquiringTime });
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out GOrderEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new GOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["OrderCode"] != DBNull.Value)
			{
				pInstance.OrderCode =  Convert.ToString(pReader["OrderCode"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}
			if (pReader["Lng"] != DBNull.Value)
			{
				pInstance.Lng =  Convert.ToString(pReader["Lng"]);
			}
			if (pReader["Lat"] != DBNull.Value)
			{
				pInstance.Lat =  Convert.ToString(pReader["Lat"]);
			}
			if (pReader["Address"] != DBNull.Value)
			{
				pInstance.Address =  Convert.ToString(pReader["Address"]);
			}
			if (pReader["Qty"] != DBNull.Value)
			{
				pInstance.Qty =  Convert.ToString(pReader["Qty"]);
			}
			if (pReader["Phone"] != DBNull.Value)
			{
				pInstance.Phone =  Convert.ToString(pReader["Phone"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =  Convert.ToString(pReader["Status"]);
			}
			if (pReader["StatusDesc"] != DBNull.Value)
			{
				pInstance.StatusDesc =  Convert.ToString(pReader["StatusDesc"]);
			}
			if (pReader["ReceiptVipId"] != DBNull.Value)
			{
				pInstance.ReceiptVipId =  Convert.ToString(pReader["ReceiptVipId"]);
			}
			if (pReader["ReceiptOpenId"] != DBNull.Value)
			{
				pInstance.ReceiptOpenId =  Convert.ToString(pReader["ReceiptOpenId"]);
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
			if (pReader["FirstPushTime"] != DBNull.Value)
			{
				pInstance.FirstPushTime =  Convert.ToDateTime(pReader["FirstPushTime"]);
			}
			if (pReader["SecondPushTime"] != DBNull.Value)
			{
				pInstance.SecondPushTime =  Convert.ToDateTime(pReader["SecondPushTime"]);
			}
			if (pReader["ReceiptOrderTime"] != DBNull.Value)
			{
				pInstance.ReceiptOrderTime =  Convert.ToDateTime(pReader["ReceiptOrderTime"]);
			}
			if (pReader["AcquiringTime"] != DBNull.Value)
			{
				pInstance.AcquiringTime =  Convert.ToDateTime(pReader["AcquiringTime"]);
			}
			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}

        }
        #endregion
    }
}
