/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:15
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
    /// 表T_Inout_Detail的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_Inout_DetailDAO : Base.BaseCPOSDAO, ICRUDable<T_Inout_DetailEntity>, IQueryable<T_Inout_DetailEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Inout_DetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_Inout_DetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_Inout_DetailEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Inout_Detail](");
            strSql.Append("[order_id],[ref_order_detail_id],[sku_id],[unit_id],[order_qty],[enter_qty],[enter_price],[enter_amount],[std_price],[discount_rate],[retail_price],[retail_amount],[plan_price],[receive_points],[pay_points],[remark],[pos_order_code],[order_detail_status],[display_index],[create_time],[create_user_id],[modify_time],[modify_user_id],[ref_order_id],[if_flag],[Field1],[Field2],[Field3],[Field4],[Field5],[Field6],[Field7],[Field8],[Field9],[Field10],[order_detail_id])");
            strSql.Append(" values (");
            strSql.Append("@order_id,@ref_order_detail_id,@sku_id,@unit_id,@order_qty,@enter_qty,@enter_price,@enter_amount,@std_price,@discount_rate,@retail_price,@retail_amount,@plan_price,@receive_points,@pay_points,@remark,@pos_order_code,@order_detail_status,@display_index,@create_time,@create_user_id,@modify_time,@modify_user_id,@ref_order_id,@if_flag,@Field1,@Field2,@Field3,@Field4,@Field5,@Field6,@Field7,@Field8,@Field9,@Field10,@order_detail_id)");            

			string pkString = pEntity.order_detail_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@order_id",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_detail_id",SqlDbType.NVarChar),
					new SqlParameter("@sku_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_id",SqlDbType.NVarChar),
					new SqlParameter("@order_qty",SqlDbType.Decimal),
					new SqlParameter("@enter_qty",SqlDbType.Decimal),
					new SqlParameter("@enter_price",SqlDbType.Decimal),
					new SqlParameter("@enter_amount",SqlDbType.Decimal),
					new SqlParameter("@std_price",SqlDbType.Decimal),
					new SqlParameter("@discount_rate",SqlDbType.Decimal),
					new SqlParameter("@retail_price",SqlDbType.Decimal),
					new SqlParameter("@retail_amount",SqlDbType.Decimal),
					new SqlParameter("@plan_price",SqlDbType.Decimal),
					new SqlParameter("@receive_points",SqlDbType.Decimal),
					new SqlParameter("@pay_points",SqlDbType.Decimal),
					new SqlParameter("@remark",SqlDbType.NVarChar),
					new SqlParameter("@pos_order_code",SqlDbType.NVarChar),
					new SqlParameter("@order_detail_status",SqlDbType.NVarChar),
					new SqlParameter("@display_index",SqlDbType.Int),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.Int),
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
					new SqlParameter("@order_detail_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.order_id;
			parameters[1].Value = pEntity.ref_order_detail_id;
			parameters[2].Value = pEntity.sku_id;
			parameters[3].Value = pEntity.unit_id;
			parameters[4].Value = pEntity.order_qty;
			parameters[5].Value = pEntity.enter_qty;
			parameters[6].Value = pEntity.enter_price;
			parameters[7].Value = pEntity.enter_amount;
			parameters[8].Value = pEntity.std_price;
			parameters[9].Value = pEntity.discount_rate;
			parameters[10].Value = pEntity.retail_price;
			parameters[11].Value = pEntity.retail_amount;
			parameters[12].Value = pEntity.plan_price;
			parameters[13].Value = pEntity.receive_points;
			parameters[14].Value = pEntity.pay_points;
			parameters[15].Value = pEntity.remark;
			parameters[16].Value = pEntity.pos_order_code;
			parameters[17].Value = pEntity.order_detail_status;
			parameters[18].Value = pEntity.display_index;
			parameters[19].Value = pEntity.create_time;
			parameters[20].Value = pEntity.create_user_id;
			parameters[21].Value = pEntity.modify_time;
			parameters[22].Value = pEntity.modify_user_id;
			parameters[23].Value = pEntity.ref_order_id;
			parameters[24].Value = pEntity.if_flag;
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
            pEntity.order_detail_id = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_Inout_DetailEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Detail] where order_detail_id='{0}'  ", id.ToString());
            //读取数据
            T_Inout_DetailEntity m = null;
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
        public T_Inout_DetailEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Detail] where 1=1 ");
            //读取数据
            List<T_Inout_DetailEntity> list = new List<T_Inout_DetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Inout_DetailEntity m;
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
        public void Update(T_Inout_DetailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_Inout_DetailEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.order_detail_id == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Inout_Detail] set ");
                        if (pIsUpdateNullField || pEntity.order_id!=null)
                strSql.Append( "[order_id]=@order_id,");
            if (pIsUpdateNullField || pEntity.ref_order_detail_id!=null)
                strSql.Append( "[ref_order_detail_id]=@ref_order_detail_id,");
            if (pIsUpdateNullField || pEntity.sku_id!=null)
                strSql.Append( "[sku_id]=@sku_id,");
            if (pIsUpdateNullField || pEntity.unit_id!=null)
                strSql.Append( "[unit_id]=@unit_id,");
            if (pIsUpdateNullField || pEntity.order_qty!=null)
                strSql.Append( "[order_qty]=@order_qty,");
            if (pIsUpdateNullField || pEntity.enter_qty!=null)
                strSql.Append( "[enter_qty]=@enter_qty,");
            if (pIsUpdateNullField || pEntity.enter_price!=null)
                strSql.Append( "[enter_price]=@enter_price,");
            if (pIsUpdateNullField || pEntity.enter_amount!=null)
                strSql.Append( "[enter_amount]=@enter_amount,");
            if (pIsUpdateNullField || pEntity.std_price!=null)
                strSql.Append( "[std_price]=@std_price,");
            if (pIsUpdateNullField || pEntity.discount_rate!=null)
                strSql.Append( "[discount_rate]=@discount_rate,");
            if (pIsUpdateNullField || pEntity.retail_price!=null)
                strSql.Append( "[retail_price]=@retail_price,");
            if (pIsUpdateNullField || pEntity.retail_amount!=null)
                strSql.Append( "[retail_amount]=@retail_amount,");
            if (pIsUpdateNullField || pEntity.plan_price!=null)
                strSql.Append( "[plan_price]=@plan_price,");
            if (pIsUpdateNullField || pEntity.receive_points!=null)
                strSql.Append( "[receive_points]=@receive_points,");
            if (pIsUpdateNullField || pEntity.pay_points!=null)
                strSql.Append( "[pay_points]=@pay_points,");
            if (pIsUpdateNullField || pEntity.remark!=null)
                strSql.Append( "[remark]=@remark,");
            if (pIsUpdateNullField || pEntity.pos_order_code!=null)
                strSql.Append( "[pos_order_code]=@pos_order_code,");
            if (pIsUpdateNullField || pEntity.order_detail_status!=null)
                strSql.Append( "[order_detail_status]=@order_detail_status,");
            if (pIsUpdateNullField || pEntity.display_index!=null)
                strSql.Append( "[display_index]=@display_index,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.ref_order_id!=null)
                strSql.Append( "[ref_order_id]=@ref_order_id,");
            if (pIsUpdateNullField || pEntity.if_flag!=null)
                strSql.Append( "[if_flag]=@if_flag,");
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
            strSql.Append(" where order_detail_id=@order_detail_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@order_id",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_detail_id",SqlDbType.NVarChar),
					new SqlParameter("@sku_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_id",SqlDbType.NVarChar),
					new SqlParameter("@order_qty",SqlDbType.Decimal),
					new SqlParameter("@enter_qty",SqlDbType.Decimal),
					new SqlParameter("@enter_price",SqlDbType.Decimal),
					new SqlParameter("@enter_amount",SqlDbType.Decimal),
					new SqlParameter("@std_price",SqlDbType.Decimal),
					new SqlParameter("@discount_rate",SqlDbType.Decimal),
					new SqlParameter("@retail_price",SqlDbType.Decimal),
					new SqlParameter("@retail_amount",SqlDbType.Decimal),
					new SqlParameter("@plan_price",SqlDbType.Decimal),
					new SqlParameter("@receive_points",SqlDbType.Decimal),
					new SqlParameter("@pay_points",SqlDbType.Decimal),
					new SqlParameter("@remark",SqlDbType.NVarChar),
					new SqlParameter("@pos_order_code",SqlDbType.NVarChar),
					new SqlParameter("@order_detail_status",SqlDbType.NVarChar),
					new SqlParameter("@display_index",SqlDbType.Int),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.Int),
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
					new SqlParameter("@order_detail_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.order_id;
			parameters[1].Value = pEntity.ref_order_detail_id;
			parameters[2].Value = pEntity.sku_id;
			parameters[3].Value = pEntity.unit_id;
			parameters[4].Value = pEntity.order_qty;
			parameters[5].Value = pEntity.enter_qty;
			parameters[6].Value = pEntity.enter_price;
			parameters[7].Value = pEntity.enter_amount;
			parameters[8].Value = pEntity.std_price;
			parameters[9].Value = pEntity.discount_rate;
			parameters[10].Value = pEntity.retail_price;
			parameters[11].Value = pEntity.retail_amount;
			parameters[12].Value = pEntity.plan_price;
			parameters[13].Value = pEntity.receive_points;
			parameters[14].Value = pEntity.pay_points;
			parameters[15].Value = pEntity.remark;
			parameters[16].Value = pEntity.pos_order_code;
			parameters[17].Value = pEntity.order_detail_status;
			parameters[18].Value = pEntity.display_index;
			parameters[19].Value = pEntity.create_time;
			parameters[20].Value = pEntity.create_user_id;
			parameters[21].Value = pEntity.modify_time;
			parameters[22].Value = pEntity.modify_user_id;
			parameters[23].Value = pEntity.ref_order_id;
			parameters[24].Value = pEntity.if_flag;
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
			parameters[35].Value = pEntity.order_detail_id;

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
        public void Update(T_Inout_DetailEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_Inout_DetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_Inout_DetailEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.order_detail_id == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.order_detail_id, pTran);           
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
            sql.AppendLine("update [T_Inout_Detail] set {$deleteFeild$} where order_detail_id=@order_detail_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@order_detail_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_Inout_DetailEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.order_detail_id == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.order_detail_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_Inout_DetailEntity[] pEntities)
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
            sql.AppendLine("update [T_Inout_Detail] set  where order_detail_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_Inout_DetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Detail] where 1=1  ");
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
            List<T_Inout_DetailEntity> list = new List<T_Inout_DetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Inout_DetailEntity m;
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
        public PagedQueryResult<T_Inout_DetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [order_detail_id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Inout_Detail] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_Inout_Detail] where 1=1  ");
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
            PagedQueryResult<T_Inout_DetailEntity> result = new PagedQueryResult<T_Inout_DetailEntity>();
            List<T_Inout_DetailEntity> list = new List<T_Inout_DetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Inout_DetailEntity m;
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
        public T_Inout_DetailEntity[] QueryByEntity(T_Inout_DetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_Inout_DetailEntity> PagedQueryByEntity(T_Inout_DetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_Inout_DetailEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.order_detail_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_detail_id", Value = pQueryEntity.order_detail_id });
            if (pQueryEntity.order_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_id", Value = pQueryEntity.order_id });
            if (pQueryEntity.ref_order_detail_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ref_order_detail_id", Value = pQueryEntity.ref_order_detail_id });
            if (pQueryEntity.sku_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sku_id", Value = pQueryEntity.sku_id });
            if (pQueryEntity.unit_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_id", Value = pQueryEntity.unit_id });
            if (pQueryEntity.order_qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_qty", Value = pQueryEntity.order_qty });
            if (pQueryEntity.enter_qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "enter_qty", Value = pQueryEntity.enter_qty });
            if (pQueryEntity.enter_price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "enter_price", Value = pQueryEntity.enter_price });
            if (pQueryEntity.enter_amount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "enter_amount", Value = pQueryEntity.enter_amount });
            if (pQueryEntity.std_price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "std_price", Value = pQueryEntity.std_price });
            if (pQueryEntity.discount_rate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "discount_rate", Value = pQueryEntity.discount_rate });
            if (pQueryEntity.retail_price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "retail_price", Value = pQueryEntity.retail_price });
            if (pQueryEntity.retail_amount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "retail_amount", Value = pQueryEntity.retail_amount });
            if (pQueryEntity.plan_price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "plan_price", Value = pQueryEntity.plan_price });
            if (pQueryEntity.receive_points!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "receive_points", Value = pQueryEntity.receive_points });
            if (pQueryEntity.pay_points!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "pay_points", Value = pQueryEntity.pay_points });
            if (pQueryEntity.remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "remark", Value = pQueryEntity.remark });
            if (pQueryEntity.pos_order_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "pos_order_code", Value = pQueryEntity.pos_order_code });
            if (pQueryEntity.order_detail_status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_detail_status", Value = pQueryEntity.order_detail_status });
            if (pQueryEntity.display_index!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "display_index", Value = pQueryEntity.display_index });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.ref_order_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ref_order_id", Value = pQueryEntity.ref_order_id });
            if (pQueryEntity.if_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "if_flag", Value = pQueryEntity.if_flag });
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
        protected void Load(IDataReader pReader, out T_Inout_DetailEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_Inout_DetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["order_detail_id"] != DBNull.Value)
			{
				pInstance.order_detail_id =  Convert.ToString(pReader["order_detail_id"]);
			}
			if (pReader["order_id"] != DBNull.Value)
			{
				pInstance.order_id =  Convert.ToString(pReader["order_id"]);
			}
			if (pReader["ref_order_detail_id"] != DBNull.Value)
			{
				pInstance.ref_order_detail_id =  Convert.ToString(pReader["ref_order_detail_id"]);
			}
			if (pReader["sku_id"] != DBNull.Value)
			{
				pInstance.sku_id =  Convert.ToString(pReader["sku_id"]);
			}
			if (pReader["unit_id"] != DBNull.Value)
			{
				pInstance.unit_id =  Convert.ToString(pReader["unit_id"]);
			}
			if (pReader["order_qty"] != DBNull.Value)
			{
				pInstance.order_qty =  Convert.ToDecimal(pReader["order_qty"]);
			}
			if (pReader["enter_qty"] != DBNull.Value)
			{
				pInstance.enter_qty =  Convert.ToDecimal(pReader["enter_qty"]);
			}
			if (pReader["enter_price"] != DBNull.Value)
			{
				pInstance.enter_price =  Convert.ToDecimal(pReader["enter_price"]);
			}
			if (pReader["enter_amount"] != DBNull.Value)
			{
				pInstance.enter_amount =  Convert.ToDecimal(pReader["enter_amount"]);
			}
			if (pReader["std_price"] != DBNull.Value)
			{
				pInstance.std_price =  Convert.ToDecimal(pReader["std_price"]);
			}
			if (pReader["discount_rate"] != DBNull.Value)
			{
				pInstance.discount_rate =  Convert.ToDecimal(pReader["discount_rate"]);
			}
			if (pReader["retail_price"] != DBNull.Value)
			{
				pInstance.retail_price =  Convert.ToDecimal(pReader["retail_price"]);
			}
			if (pReader["retail_amount"] != DBNull.Value)
			{
				pInstance.retail_amount =  Convert.ToDecimal(pReader["retail_amount"]);
			}
			if (pReader["plan_price"] != DBNull.Value)
			{
				pInstance.plan_price =  Convert.ToDecimal(pReader["plan_price"]);
			}
			if (pReader["receive_points"] != DBNull.Value)
			{
				pInstance.receive_points =  Convert.ToDecimal(pReader["receive_points"]);
			}
			if (pReader["pay_points"] != DBNull.Value)
			{
				pInstance.pay_points =  Convert.ToDecimal(pReader["pay_points"]);
			}
			if (pReader["remark"] != DBNull.Value)
			{
				pInstance.remark =  Convert.ToString(pReader["remark"]);
			}
			if (pReader["pos_order_code"] != DBNull.Value)
			{
				pInstance.pos_order_code =  Convert.ToString(pReader["pos_order_code"]);
			}
			if (pReader["order_detail_status"] != DBNull.Value)
			{
				pInstance.order_detail_status =  Convert.ToString(pReader["order_detail_status"]);
			}
			if (pReader["display_index"] != DBNull.Value)
			{
				pInstance.display_index =   Convert.ToInt32(pReader["display_index"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToString(pReader["modify_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["ref_order_id"] != DBNull.Value)
			{
				pInstance.ref_order_id =  Convert.ToString(pReader["ref_order_id"]);
			}
			if (pReader["if_flag"] != DBNull.Value)
			{
				pInstance.if_flag =   Convert.ToInt32(pReader["if_flag"]);
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
