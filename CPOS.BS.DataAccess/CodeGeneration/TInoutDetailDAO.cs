/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 14:14:45
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表T_Inout_Detail的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TInoutDetailDAO : BaseCPOSDAO, ICRUDable<TInoutDetailEntity>, IQueryable<TInoutDetailEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInoutDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TInoutDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TInoutDetailEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Inout_Detail](");
            strSql.Append("[order_id],[ref_order_detail_id],[sku_id],[unit_id],[order_qty],[enter_qty],[enter_price],[enter_amount],[std_price],[discount_rate],[retail_price],[retail_amount],[plan_price],[receive_points],[pay_points],[remark],[pos_order_code],[order_detail_status],[display_index],[create_time],[create_user_id],[modify_time],[modify_user_id],[ref_order_id],[if_flag],[Field1],[Field2],[Field3],[Field4],[Field5],[Field6],[Field7],[Field8],[Field9],[Field10],[order_detail_id])");
            strSql.Append(" values (");
            strSql.Append("@OrderID,@RefOrderDetailID,@SkuID,@UnitID,@OrderQty,@EnterQty,@EnterPrice,@EnterAmount,@StdPrice,@DiscountRate,@RetailPrice,@RetailAmount,@PlanPrice,@ReceivePoints,@PayPoints,@Remark,@PosOrderCode,@OrderDetailStatus,@DisplayIndex,@CreateTime,@CreateUserID,@ModifyTime,@ModifyUserID,@RefOrderID,@IfFlag,@Field1,@Field2,@Field3,@Field4,@Field5,@Field6,@Field7,@Field8,@Field9,@Field10,@OrderDetailID)");            

			string pkString = pEntity.OrderDetailID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderDetailID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@OrderQty",SqlDbType.Decimal),
					new SqlParameter("@EnterQty",SqlDbType.Decimal),
					new SqlParameter("@EnterPrice",SqlDbType.Decimal),
					new SqlParameter("@EnterAmount",SqlDbType.Decimal),
					new SqlParameter("@StdPrice",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@RetailPrice",SqlDbType.Decimal),
					new SqlParameter("@RetailAmount",SqlDbType.Decimal),
					new SqlParameter("@PlanPrice",SqlDbType.Decimal),
					new SqlParameter("@ReceivePoints",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@PosOrderCode",SqlDbType.NVarChar),
					new SqlParameter("@OrderDetailStatus",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.NVarChar),
					new SqlParameter("@CreateUserID",SqlDbType.NVarChar),
					new SqlParameter("@ModifyTime",SqlDbType.NVarChar),
					new SqlParameter("@ModifyUserID",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderID",SqlDbType.NVarChar),
					new SqlParameter("@IfFlag",SqlDbType.Int),
					new SqlParameter("@Field1",SqlDbType.NVarChar),
					new SqlParameter("@Field2",SqlDbType.NVarChar),
					new SqlParameter("@Field3",SqlDbType.NVarChar),
					new SqlParameter("@Field4",SqlDbType.NVarChar),
					new SqlParameter("@Field5",SqlDbType.NVarChar),
					new SqlParameter("@Field6",SqlDbType.NVarChar),
					new SqlParameter("@Field7",SqlDbType.NVarChar),
					new SqlParameter("@Field8",SqlDbType.NVarChar),
					new SqlParameter("@Field9",SqlDbType.NVarChar),
					new SqlParameter("@Field10",SqlDbType.NVarChar),
					new SqlParameter("@OrderDetailID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderID;
			parameters[1].Value = pEntity.RefOrderDetailID;
			parameters[2].Value = pEntity.SkuID;
			parameters[3].Value = pEntity.UnitID;
			parameters[4].Value = pEntity.OrderQty;
			parameters[5].Value = pEntity.EnterQty;
			parameters[6].Value = pEntity.EnterPrice;
			parameters[7].Value = pEntity.EnterAmount;
			parameters[8].Value = pEntity.StdPrice;
			parameters[9].Value = pEntity.DiscountRate;
			parameters[10].Value = pEntity.RetailPrice;
			parameters[11].Value = pEntity.RetailAmount;
			parameters[12].Value = pEntity.PlanPrice;
			parameters[13].Value = pEntity.ReceivePoints;
			parameters[14].Value = pEntity.PayPoints;
			parameters[15].Value = pEntity.Remark;
			parameters[16].Value = pEntity.PosOrderCode;
			parameters[17].Value = pEntity.OrderDetailStatus;
			parameters[18].Value = pEntity.DisplayIndex;
			parameters[19].Value = pEntity.CreateTime;
			parameters[20].Value = pEntity.CreateUserID;
			parameters[21].Value = pEntity.ModifyTime;
			parameters[22].Value = pEntity.ModifyUserID;
			parameters[23].Value = pEntity.RefOrderID;
			parameters[24].Value = pEntity.IfFlag;
			parameters[25].Value = pEntity.Field1;
			parameters[26].Value = pEntity.Field2;
			parameters[27].Value = pEntity.Field3;
			parameters[28].Value = pEntity.Field4;
			parameters[29].Value = pEntity.Field5;
			parameters[30].Value = pEntity.Field6;
			parameters[31].Value = pEntity.Field7;
			parameters[32].Value = pEntity.Field8;
			parameters[33].Value = pEntity.Field9;
			parameters[34].Value = pEntity.Field10;
			parameters[35].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.OrderDetailID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TInoutDetailEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Detail] where order_detail_id='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            TInoutDetailEntity m = null;
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
        public TInoutDetailEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Detail] where isdelete=0");
            //读取数据
            List<TInoutDetailEntity> list = new List<TInoutDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutDetailEntity m;
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
        public void Update(TInoutDetailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(TInoutDetailEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderDetailID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Inout_Detail] set ");
                        if (pIsUpdateNullField || pEntity.OrderID!=null)
                strSql.Append( "[order_id]=@OrderID,");
            if (pIsUpdateNullField || pEntity.RefOrderDetailID!=null)
                strSql.Append( "[ref_order_detail_id]=@RefOrderDetailID,");
            if (pIsUpdateNullField || pEntity.SkuID!=null)
                strSql.Append( "[sku_id]=@SkuID,");
            if (pIsUpdateNullField || pEntity.UnitID!=null)
                strSql.Append( "[unit_id]=@UnitID,");
            if (pIsUpdateNullField || pEntity.OrderQty!=null)
                strSql.Append( "[order_qty]=@OrderQty,");
            if (pIsUpdateNullField || pEntity.EnterQty!=null)
                strSql.Append( "[enter_qty]=@EnterQty,");
            if (pIsUpdateNullField || pEntity.EnterPrice!=null)
                strSql.Append( "[enter_price]=@EnterPrice,");
            if (pIsUpdateNullField || pEntity.EnterAmount!=null)
                strSql.Append( "[enter_amount]=@EnterAmount,");
            if (pIsUpdateNullField || pEntity.StdPrice!=null)
                strSql.Append( "[std_price]=@StdPrice,");
            if (pIsUpdateNullField || pEntity.DiscountRate!=null)
                strSql.Append( "[discount_rate]=@DiscountRate,");
            if (pIsUpdateNullField || pEntity.RetailPrice!=null)
                strSql.Append( "[retail_price]=@RetailPrice,");
            if (pIsUpdateNullField || pEntity.RetailAmount!=null)
                strSql.Append( "[retail_amount]=@RetailAmount,");
            if (pIsUpdateNullField || pEntity.PlanPrice!=null)
                strSql.Append( "[plan_price]=@PlanPrice,");
            if (pIsUpdateNullField || pEntity.ReceivePoints!=null)
                strSql.Append( "[receive_points]=@ReceivePoints,");
            if (pIsUpdateNullField || pEntity.PayPoints!=null)
                strSql.Append( "[pay_points]=@PayPoints,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.PosOrderCode!=null)
                strSql.Append( "[pos_order_code]=@PosOrderCode,");
            if (pIsUpdateNullField || pEntity.OrderDetailStatus!=null)
                strSql.Append( "[order_detail_status]=@OrderDetailStatus,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[display_index]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.CreateUserID!=null)
                strSql.Append( "[create_user_id]=@CreateUserID,");
            if (pIsUpdateNullField || pEntity.ModifyTime!=null)
                strSql.Append( "[modify_time]=@ModifyTime,");
            if (pIsUpdateNullField || pEntity.ModifyUserID!=null)
                strSql.Append( "[modify_user_id]=@ModifyUserID,");
            if (pIsUpdateNullField || pEntity.RefOrderID!=null)
                strSql.Append( "[ref_order_id]=@RefOrderID,");
            if (pIsUpdateNullField || pEntity.IfFlag!=null)
                strSql.Append( "[if_flag]=@IfFlag,");
            if (pIsUpdateNullField || pEntity.Field1!=null)
                strSql.Append( "[Field1]=@Field1,");
            if (pIsUpdateNullField || pEntity.Field2!=null)
                strSql.Append( "[Field2]=@Field2,");
            if (pIsUpdateNullField || pEntity.Field3!=null)
                strSql.Append( "[Field3]=@Field3,");
            if (pIsUpdateNullField || pEntity.Field4!=null)
                strSql.Append( "[Field4]=@Field4,");
            if (pIsUpdateNullField || pEntity.Field5!=null)
                strSql.Append( "[Field5]=@Field5,");
            if (pIsUpdateNullField || pEntity.Field6!=null)
                strSql.Append( "[Field6]=@Field6,");
            if (pIsUpdateNullField || pEntity.Field7!=null)
                strSql.Append( "[Field7]=@Field7,");
            if (pIsUpdateNullField || pEntity.Field8!=null)
                strSql.Append( "[Field8]=@Field8,");
            if (pIsUpdateNullField || pEntity.Field9!=null)
                strSql.Append( "[Field9]=@Field9,");
            if (pIsUpdateNullField || pEntity.Field10!=null)
                strSql.Append( "[Field10]=@Field10");
            strSql.Append(" where order_detail_id=@OrderDetailID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderDetailID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@OrderQty",SqlDbType.Decimal),
					new SqlParameter("@EnterQty",SqlDbType.Decimal),
					new SqlParameter("@EnterPrice",SqlDbType.Decimal),
					new SqlParameter("@EnterAmount",SqlDbType.Decimal),
					new SqlParameter("@StdPrice",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@RetailPrice",SqlDbType.Decimal),
					new SqlParameter("@RetailAmount",SqlDbType.Decimal),
					new SqlParameter("@PlanPrice",SqlDbType.Decimal),
					new SqlParameter("@ReceivePoints",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@PosOrderCode",SqlDbType.NVarChar),
					new SqlParameter("@OrderDetailStatus",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@CreateUserID",SqlDbType.NVarChar),
					new SqlParameter("@ModifyTime",SqlDbType.NVarChar),
					new SqlParameter("@ModifyUserID",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderID",SqlDbType.NVarChar),
					new SqlParameter("@IfFlag",SqlDbType.Int),
					new SqlParameter("@Field1",SqlDbType.NVarChar),
					new SqlParameter("@Field2",SqlDbType.NVarChar),
					new SqlParameter("@Field3",SqlDbType.NVarChar),
					new SqlParameter("@Field4",SqlDbType.NVarChar),
					new SqlParameter("@Field5",SqlDbType.NVarChar),
					new SqlParameter("@Field6",SqlDbType.NVarChar),
					new SqlParameter("@Field7",SqlDbType.NVarChar),
					new SqlParameter("@Field8",SqlDbType.NVarChar),
					new SqlParameter("@Field9",SqlDbType.NVarChar),
					new SqlParameter("@Field10",SqlDbType.NVarChar),
					new SqlParameter("@OrderDetailID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderID;
			parameters[1].Value = pEntity.RefOrderDetailID;
			parameters[2].Value = pEntity.SkuID;
			parameters[3].Value = pEntity.UnitID;
			parameters[4].Value = pEntity.OrderQty;
			parameters[5].Value = pEntity.EnterQty;
			parameters[6].Value = pEntity.EnterPrice;
			parameters[7].Value = pEntity.EnterAmount;
			parameters[8].Value = pEntity.StdPrice;
			parameters[9].Value = pEntity.DiscountRate;
			parameters[10].Value = pEntity.RetailPrice;
			parameters[11].Value = pEntity.RetailAmount;
			parameters[12].Value = pEntity.PlanPrice;
			parameters[13].Value = pEntity.ReceivePoints;
			parameters[14].Value = pEntity.PayPoints;
			parameters[15].Value = pEntity.Remark;
			parameters[16].Value = pEntity.PosOrderCode;
			parameters[17].Value = pEntity.OrderDetailStatus;
			parameters[18].Value = pEntity.DisplayIndex;
			parameters[19].Value = pEntity.CreateUserID;
			parameters[20].Value = pEntity.ModifyTime;
			parameters[21].Value = pEntity.ModifyUserID;
			parameters[22].Value = pEntity.RefOrderID;
			parameters[23].Value = pEntity.IfFlag;
			parameters[24].Value = pEntity.Field1;
			parameters[25].Value = pEntity.Field2;
			parameters[26].Value = pEntity.Field3;
			parameters[27].Value = pEntity.Field4;
			parameters[28].Value = pEntity.Field5;
			parameters[29].Value = pEntity.Field6;
			parameters[30].Value = pEntity.Field7;
			parameters[31].Value = pEntity.Field8;
			parameters[32].Value = pEntity.Field9;
			parameters[33].Value = pEntity.Field10;
			parameters[34].Value = pEntity.OrderDetailID;

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
        public void Update(TInoutDetailEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TInoutDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TInoutDetailEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderDetailID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.OrderDetailID, pTran);           
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
            sql.AppendLine("update [T_Inout_Detail] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where order_detail_id=@OrderDetailID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.Int,Value=Convert.ToInt32(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderDetailID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(TInoutDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.OrderDetailID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.OrderDetailID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TInoutDetailEntity[] pEntities)
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
            sql.AppendLine("update [T_Inout_Detail] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy="+CurrentUserInfo.UserID+",IsDelete=1 where order_detail_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TInoutDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Detail] where 1=1 ");
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
            List<TInoutDetailEntity> list = new List<TInoutDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutDetailEntity m;
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
        public PagedQueryResult<TInoutDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderDetailID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [TInoutDetail] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [TInoutDetail] where isdelete=0 ");
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
            PagedQueryResult<TInoutDetailEntity> result = new PagedQueryResult<TInoutDetailEntity>();
            List<TInoutDetailEntity> list = new List<TInoutDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutDetailEntity m;
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
        public TInoutDetailEntity[] QueryByEntity(TInoutDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TInoutDetailEntity> PagedQueryByEntity(TInoutDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TInoutDetailEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderDetailID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDetailID", Value = pQueryEntity.OrderDetailID });
            if (pQueryEntity.OrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.order_id != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_id", Value = pQueryEntity.order_id });
            if (pQueryEntity.RefOrderDetailID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefOrderDetailID", Value = pQueryEntity.RefOrderDetailID });
            if (pQueryEntity.SkuID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuID", Value = pQueryEntity.SkuID });
            if (pQueryEntity.UnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.OrderQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderQty", Value = pQueryEntity.OrderQty });
            if (pQueryEntity.EnterQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EnterQty", Value = pQueryEntity.EnterQty });
            if (pQueryEntity.EnterPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EnterPrice", Value = pQueryEntity.EnterPrice });
            if (pQueryEntity.EnterAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EnterAmount", Value = pQueryEntity.EnterAmount });
            if (pQueryEntity.StdPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StdPrice", Value = pQueryEntity.StdPrice });
            if (pQueryEntity.DiscountRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscountRate", Value = pQueryEntity.DiscountRate });
            if (pQueryEntity.RetailPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailPrice", Value = pQueryEntity.RetailPrice });
            if (pQueryEntity.RetailAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailAmount", Value = pQueryEntity.RetailAmount });
            if (pQueryEntity.PlanPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PlanPrice", Value = pQueryEntity.PlanPrice });
            if (pQueryEntity.ReceivePoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceivePoints", Value = pQueryEntity.ReceivePoints });
            if (pQueryEntity.PayPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayPoints", Value = pQueryEntity.PayPoints });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.PosOrderCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PosOrderCode", Value = pQueryEntity.PosOrderCode });
            if (pQueryEntity.OrderDetailStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDetailStatus", Value = pQueryEntity.OrderDetailStatus });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateUserID", Value = pQueryEntity.CreateUserID });
            if (pQueryEntity.ModifyTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModifyTime", Value = pQueryEntity.ModifyTime });
            if (pQueryEntity.ModifyUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModifyUserID", Value = pQueryEntity.ModifyUserID });
            if (pQueryEntity.RefOrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefOrderID", Value = pQueryEntity.RefOrderID });
            if (pQueryEntity.IfFlag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IfFlag", Value = pQueryEntity.IfFlag });
            if (pQueryEntity.Field1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field1", Value = pQueryEntity.Field1 });
            if (pQueryEntity.Field2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field2", Value = pQueryEntity.Field2 });
            if (pQueryEntity.Field3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field3", Value = pQueryEntity.Field3 });
            if (pQueryEntity.Field4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field4", Value = pQueryEntity.Field4 });
            if (pQueryEntity.Field5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field5", Value = pQueryEntity.Field5 });
            if (pQueryEntity.Field6!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field6", Value = pQueryEntity.Field6 });
            if (pQueryEntity.Field7!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field7", Value = pQueryEntity.Field7 });
            if (pQueryEntity.Field8!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field8", Value = pQueryEntity.Field8 });
            if (pQueryEntity.Field9!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field9", Value = pQueryEntity.Field9 });
            if (pQueryEntity.Field10!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field10", Value = pQueryEntity.Field10 });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out TInoutDetailEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new TInoutDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["order_detail_id"] != DBNull.Value)
			{
				pInstance.OrderDetailID =  Convert.ToString(pReader["order_detail_id"]);
			}
			if (pReader["order_id"] != DBNull.Value)
			{
				pInstance.OrderID =  Convert.ToString(pReader["order_id"]);
			}
			if (pReader["ref_order_detail_id"] != DBNull.Value)
			{
				pInstance.RefOrderDetailID =  Convert.ToString(pReader["ref_order_detail_id"]);
			}
			if (pReader["sku_id"] != DBNull.Value)
			{
				pInstance.SkuID =  Convert.ToString(pReader["sku_id"]);
			}
			if (pReader["unit_id"] != DBNull.Value)
			{
				pInstance.UnitID =  Convert.ToString(pReader["unit_id"]);
			}
			if (pReader["order_qty"] != DBNull.Value)
			{
				pInstance.OrderQty =  Convert.ToDecimal(pReader["order_qty"]);
			}
			if (pReader["enter_qty"] != DBNull.Value)
			{
				pInstance.EnterQty =  Convert.ToDecimal(pReader["enter_qty"]);
			}
			if (pReader["enter_price"] != DBNull.Value)
			{
				pInstance.EnterPrice =  Convert.ToDecimal(pReader["enter_price"]);
			}
			if (pReader["enter_amount"] != DBNull.Value)
			{
				pInstance.EnterAmount =  Convert.ToDecimal(pReader["enter_amount"]);
			}
			if (pReader["std_price"] != DBNull.Value)
			{
				pInstance.StdPrice =  Convert.ToDecimal(pReader["std_price"]);
			}
			if (pReader["discount_rate"] != DBNull.Value)
			{
				pInstance.DiscountRate =  Convert.ToDecimal(pReader["discount_rate"]);
			}
			if (pReader["retail_price"] != DBNull.Value)
			{
				pInstance.RetailPrice =  Convert.ToDecimal(pReader["retail_price"]);
			}
			if (pReader["retail_amount"] != DBNull.Value)
			{
				pInstance.RetailAmount =  Convert.ToDecimal(pReader["retail_amount"]);
			}
			if (pReader["plan_price"] != DBNull.Value)
			{
				pInstance.PlanPrice =  Convert.ToDecimal(pReader["plan_price"]);
			}
			if (pReader["receive_points"] != DBNull.Value)
			{
				pInstance.ReceivePoints =  Convert.ToDecimal(pReader["receive_points"]);
			}
			if (pReader["pay_points"] != DBNull.Value)
			{
				pInstance.PayPoints =  Convert.ToDecimal(pReader["pay_points"]);
			}
			if (pReader["remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["remark"]);
			}
			if (pReader["pos_order_code"] != DBNull.Value)
			{
				pInstance.PosOrderCode =  Convert.ToString(pReader["pos_order_code"]);
			}
			if (pReader["order_detail_status"] != DBNull.Value)
			{
				pInstance.OrderDetailStatus =  Convert.ToString(pReader["order_detail_status"]);
			}
			if (pReader["display_index"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["display_index"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.CreateUserID =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.ModifyTime =  Convert.ToString(pReader["modify_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.ModifyUserID =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["ref_order_id"] != DBNull.Value)
			{
				pInstance.RefOrderID =  Convert.ToString(pReader["ref_order_id"]);
			}
			if (pReader["if_flag"] != DBNull.Value)
			{
				pInstance.IfFlag =   Convert.ToInt32(pReader["if_flag"]);
			}
			if (pReader["Field1"] != DBNull.Value)
			{
				pInstance.Field1 =  Convert.ToString(pReader["Field1"]);
			}
			if (pReader["Field2"] != DBNull.Value)
			{
				pInstance.Field2 =  Convert.ToString(pReader["Field2"]);
			}
			if (pReader["Field3"] != DBNull.Value)
			{
				pInstance.Field3 =  Convert.ToString(pReader["Field3"]);
			}
			if (pReader["Field4"] != DBNull.Value)
			{
				pInstance.Field4 =  Convert.ToString(pReader["Field4"]);
			}
			if (pReader["Field5"] != DBNull.Value)
			{
				pInstance.Field5 =  Convert.ToString(pReader["Field5"]);
			}
			if (pReader["Field6"] != DBNull.Value)
			{
				pInstance.Field6 =  Convert.ToString(pReader["Field6"]);
			}
			if (pReader["Field7"] != DBNull.Value)
			{
				pInstance.Field7 =  Convert.ToString(pReader["Field7"]);
			}
			if (pReader["Field8"] != DBNull.Value)
			{
				pInstance.Field8 =  Convert.ToString(pReader["Field8"]);
			}
			if (pReader["Field9"] != DBNull.Value)
			{
				pInstance.Field9 =  Convert.ToString(pReader["Field9"]);
			}
			if (pReader["Field10"] != DBNull.Value)
			{
				pInstance.Field10 =  Convert.ToString(pReader["Field10"]);
			}

        }
        #endregion
    }
}
