/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 15:54:11
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
    /// 表R_WxO2OPanel_30Days的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_WxO2OPanel_30DaysDAO : Base.BaseCPOSDAO, ICRUDable<R_WxO2OPanel_30DaysEntity>, IQueryable<R_WxO2OPanel_30DaysEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_WxO2OPanel_30DaysDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(R_WxO2OPanel_30DaysEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(R_WxO2OPanel_30DaysEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_WxO2OPanel_30Days](");
            strSql.Append("[CustomerId],[DateCode],[WxUV],[WxOrderVipCount],[WxOrderVipPayCount],[WxPV],[WxOrderCount],[WxOrderPayCount],[WxOrderMoney],[WxOrderPayMoney],[WxOrderAVG],[LastWxUV],[LastWxOrderVipCount],[LastWxOrderVipPayCount],[LastWxPV],[LastWxOrderCount],[LastWxOrderPayCount],[LastWxOrderMoney],[LastWxOrderPayMoney],[LastWxOrderAVG],[Rate_OrderVipCount_UV],[Rate_OrderVipPayCount_OrderVipCount],[Rate_OrderVipPayCount_UV],[Rate_UV_Last],[Rate_PV_Last],[Rate_OrderVipCount_Last],[Rate_OrderCount_Last],[Rate_OrderMoney_Last],[Rate_OrderVipPayCount_Last],[Rate_OrderPayCount_Last],[Rate_OrderPayMoney_Last],[Rate_OrderAVG_Last],[CreateTime],[LogIDs],[ID])");
            strSql.Append(" values (");
            strSql.Append("@CustomerId,@DateCode,@WxUV,@WxOrderVipCount,@WxOrderVipPayCount,@WxPV,@WxOrderCount,@WxOrderPayCount,@WxOrderMoney,@WxOrderPayMoney,@WxOrderAVG,@LastWxUV,@LastWxOrderVipCount,@LastWxOrderVipPayCount,@LastWxPV,@LastWxOrderCount,@LastWxOrderPayCount,@LastWxOrderMoney,@LastWxOrderPayMoney,@LastWxOrderAVG,@Rate_OrderVipCount_UV,@Rate_OrderVipPayCount_OrderVipCount,@Rate_OrderVipPayCount_UV,@Rate_UV_Last,@Rate_PV_Last,@Rate_OrderVipCount_Last,@Rate_OrderCount_Last,@Rate_OrderMoney_Last,@Rate_OrderVipPayCount_Last,@Rate_OrderPayCount_Last,@Rate_OrderPayMoney_Last,@Rate_OrderAVG_Last,@CreateTime,@LogIDs,@ID)");            

			Guid? pkGuid;
			if (pEntity.ID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@WxUV",SqlDbType.Int),
					new SqlParameter("@WxOrderVipCount",SqlDbType.Int),
					new SqlParameter("@WxOrderVipPayCount",SqlDbType.Int),
					new SqlParameter("@WxPV",SqlDbType.Int),
					new SqlParameter("@WxOrderCount",SqlDbType.Int),
					new SqlParameter("@WxOrderPayCount",SqlDbType.Int),
					new SqlParameter("@WxOrderMoney",SqlDbType.Decimal),
					new SqlParameter("@WxOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@WxOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@LastWxUV",SqlDbType.Int),
					new SqlParameter("@LastWxOrderVipCount",SqlDbType.Int),
					new SqlParameter("@LastWxOrderVipPayCount",SqlDbType.Int),
					new SqlParameter("@LastWxPV",SqlDbType.Int),
					new SqlParameter("@LastWxOrderCount",SqlDbType.Int),
					new SqlParameter("@LastWxOrderPayCount",SqlDbType.Int),
					new SqlParameter("@LastWxOrderMoney",SqlDbType.Decimal),
					new SqlParameter("@LastWxOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@LastWxOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@Rate_OrderVipCount_UV",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipPayCount_OrderVipCount",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipPayCount_UV",SqlDbType.NVarChar),
					new SqlParameter("@Rate_UV_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_PV_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderMoney_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipPayCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderPayCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderPayMoney_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderAVG_Last",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LogIDs",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerId;
			parameters[1].Value = pEntity.DateCode;
			parameters[2].Value = pEntity.WxUV;
			parameters[3].Value = pEntity.WxOrderVipCount;
			parameters[4].Value = pEntity.WxOrderVipPayCount;
			parameters[5].Value = pEntity.WxPV;
			parameters[6].Value = pEntity.WxOrderCount;
			parameters[7].Value = pEntity.WxOrderPayCount;
			parameters[8].Value = pEntity.WxOrderMoney;
			parameters[9].Value = pEntity.WxOrderPayMoney;
			parameters[10].Value = pEntity.WxOrderAVG;
			parameters[11].Value = pEntity.LastWxUV;
			parameters[12].Value = pEntity.LastWxOrderVipCount;
			parameters[13].Value = pEntity.LastWxOrderVipPayCount;
			parameters[14].Value = pEntity.LastWxPV;
			parameters[15].Value = pEntity.LastWxOrderCount;
			parameters[16].Value = pEntity.LastWxOrderPayCount;
			parameters[17].Value = pEntity.LastWxOrderMoney;
			parameters[18].Value = pEntity.LastWxOrderPayMoney;
			parameters[19].Value = pEntity.LastWxOrderAVG;
			parameters[20].Value = pEntity.Rate_OrderVipCount_UV;
			parameters[21].Value = pEntity.Rate_OrderVipPayCount_OrderVipCount;
			parameters[22].Value = pEntity.Rate_OrderVipPayCount_UV;
			parameters[23].Value = pEntity.Rate_UV_Last;
			parameters[24].Value = pEntity.Rate_PV_Last;
			parameters[25].Value = pEntity.Rate_OrderVipCount_Last;
			parameters[26].Value = pEntity.Rate_OrderCount_Last;
			parameters[27].Value = pEntity.Rate_OrderMoney_Last;
			parameters[28].Value = pEntity.Rate_OrderVipPayCount_Last;
			parameters[29].Value = pEntity.Rate_OrderPayCount_Last;
			parameters[30].Value = pEntity.Rate_OrderPayMoney_Last;
			parameters[31].Value = pEntity.Rate_OrderAVG_Last;
			parameters[32].Value = pEntity.CreateTime;
			parameters[33].Value = pEntity.LogIDs;
			parameters[34].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public R_WxO2OPanel_30DaysEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_30Days] where ID='{0}'  ", id.ToString());
            //读取数据
            R_WxO2OPanel_30DaysEntity m = null;
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
        public R_WxO2OPanel_30DaysEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_30Days] where 1=1 ");
            //读取数据
            List<R_WxO2OPanel_30DaysEntity> list = new List<R_WxO2OPanel_30DaysEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_30DaysEntity m;
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
        public void Update(R_WxO2OPanel_30DaysEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(R_WxO2OPanel_30DaysEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [R_WxO2OPanel_30Days] set ");
                        if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.DateCode!=null)
                strSql.Append( "[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.WxUV!=null)
                strSql.Append( "[WxUV]=@WxUV,");
            if (pIsUpdateNullField || pEntity.WxOrderVipCount!=null)
                strSql.Append( "[WxOrderVipCount]=@WxOrderVipCount,");
            if (pIsUpdateNullField || pEntity.WxOrderVipPayCount!=null)
                strSql.Append( "[WxOrderVipPayCount]=@WxOrderVipPayCount,");
            if (pIsUpdateNullField || pEntity.WxPV!=null)
                strSql.Append( "[WxPV]=@WxPV,");
            if (pIsUpdateNullField || pEntity.WxOrderCount!=null)
                strSql.Append( "[WxOrderCount]=@WxOrderCount,");
            if (pIsUpdateNullField || pEntity.WxOrderPayCount!=null)
                strSql.Append( "[WxOrderPayCount]=@WxOrderPayCount,");
            if (pIsUpdateNullField || pEntity.WxOrderMoney!=null)
                strSql.Append( "[WxOrderMoney]=@WxOrderMoney,");
            if (pIsUpdateNullField || pEntity.WxOrderPayMoney!=null)
                strSql.Append( "[WxOrderPayMoney]=@WxOrderPayMoney,");
            if (pIsUpdateNullField || pEntity.WxOrderAVG!=null)
                strSql.Append( "[WxOrderAVG]=@WxOrderAVG,");
            if (pIsUpdateNullField || pEntity.LastWxUV!=null)
                strSql.Append( "[LastWxUV]=@LastWxUV,");
            if (pIsUpdateNullField || pEntity.LastWxOrderVipCount!=null)
                strSql.Append( "[LastWxOrderVipCount]=@LastWxOrderVipCount,");
            if (pIsUpdateNullField || pEntity.LastWxOrderVipPayCount!=null)
                strSql.Append( "[LastWxOrderVipPayCount]=@LastWxOrderVipPayCount,");
            if (pIsUpdateNullField || pEntity.LastWxPV!=null)
                strSql.Append( "[LastWxPV]=@LastWxPV,");
            if (pIsUpdateNullField || pEntity.LastWxOrderCount!=null)
                strSql.Append( "[LastWxOrderCount]=@LastWxOrderCount,");
            if (pIsUpdateNullField || pEntity.LastWxOrderPayCount!=null)
                strSql.Append( "[LastWxOrderPayCount]=@LastWxOrderPayCount,");
            if (pIsUpdateNullField || pEntity.LastWxOrderMoney!=null)
                strSql.Append( "[LastWxOrderMoney]=@LastWxOrderMoney,");
            if (pIsUpdateNullField || pEntity.LastWxOrderPayMoney!=null)
                strSql.Append( "[LastWxOrderPayMoney]=@LastWxOrderPayMoney,");
            if (pIsUpdateNullField || pEntity.LastWxOrderAVG!=null)
                strSql.Append( "[LastWxOrderAVG]=@LastWxOrderAVG,");
            if (pIsUpdateNullField || pEntity.Rate_OrderVipCount_UV!=null)
                strSql.Append( "[Rate_OrderVipCount_UV]=@Rate_OrderVipCount_UV,");
            if (pIsUpdateNullField || pEntity.Rate_OrderVipPayCount_OrderVipCount!=null)
                strSql.Append( "[Rate_OrderVipPayCount_OrderVipCount]=@Rate_OrderVipPayCount_OrderVipCount,");
            if (pIsUpdateNullField || pEntity.Rate_OrderVipPayCount_UV!=null)
                strSql.Append( "[Rate_OrderVipPayCount_UV]=@Rate_OrderVipPayCount_UV,");
            if (pIsUpdateNullField || pEntity.Rate_UV_Last!=null)
                strSql.Append( "[Rate_UV_Last]=@Rate_UV_Last,");
            if (pIsUpdateNullField || pEntity.Rate_PV_Last!=null)
                strSql.Append( "[Rate_PV_Last]=@Rate_PV_Last,");
            if (pIsUpdateNullField || pEntity.Rate_OrderVipCount_Last!=null)
                strSql.Append( "[Rate_OrderVipCount_Last]=@Rate_OrderVipCount_Last,");
            if (pIsUpdateNullField || pEntity.Rate_OrderCount_Last!=null)
                strSql.Append( "[Rate_OrderCount_Last]=@Rate_OrderCount_Last,");
            if (pIsUpdateNullField || pEntity.Rate_OrderMoney_Last!=null)
                strSql.Append( "[Rate_OrderMoney_Last]=@Rate_OrderMoney_Last,");
            if (pIsUpdateNullField || pEntity.Rate_OrderVipPayCount_Last!=null)
                strSql.Append( "[Rate_OrderVipPayCount_Last]=@Rate_OrderVipPayCount_Last,");
            if (pIsUpdateNullField || pEntity.Rate_OrderPayCount_Last!=null)
                strSql.Append( "[Rate_OrderPayCount_Last]=@Rate_OrderPayCount_Last,");
            if (pIsUpdateNullField || pEntity.Rate_OrderPayMoney_Last!=null)
                strSql.Append( "[Rate_OrderPayMoney_Last]=@Rate_OrderPayMoney_Last,");
            if (pIsUpdateNullField || pEntity.Rate_OrderAVG_Last!=null)
                strSql.Append( "[Rate_OrderAVG_Last]=@Rate_OrderAVG_Last,");
            if (pIsUpdateNullField || pEntity.LogIDs!=null)
                strSql.Append( "[LogIDs]=@LogIDs");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@WxUV",SqlDbType.Int),
					new SqlParameter("@WxOrderVipCount",SqlDbType.Int),
					new SqlParameter("@WxOrderVipPayCount",SqlDbType.Int),
					new SqlParameter("@WxPV",SqlDbType.Int),
					new SqlParameter("@WxOrderCount",SqlDbType.Int),
					new SqlParameter("@WxOrderPayCount",SqlDbType.Int),
					new SqlParameter("@WxOrderMoney",SqlDbType.Decimal),
					new SqlParameter("@WxOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@WxOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@LastWxUV",SqlDbType.Int),
					new SqlParameter("@LastWxOrderVipCount",SqlDbType.Int),
					new SqlParameter("@LastWxOrderVipPayCount",SqlDbType.Int),
					new SqlParameter("@LastWxPV",SqlDbType.Int),
					new SqlParameter("@LastWxOrderCount",SqlDbType.Int),
					new SqlParameter("@LastWxOrderPayCount",SqlDbType.Int),
					new SqlParameter("@LastWxOrderMoney",SqlDbType.Decimal),
					new SqlParameter("@LastWxOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@LastWxOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@Rate_OrderVipCount_UV",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipPayCount_OrderVipCount",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipPayCount_UV",SqlDbType.NVarChar),
					new SqlParameter("@Rate_UV_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_PV_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderMoney_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderVipPayCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderPayCount_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderPayMoney_Last",SqlDbType.NVarChar),
					new SqlParameter("@Rate_OrderAVG_Last",SqlDbType.NVarChar),
					new SqlParameter("@LogIDs",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerId;
			parameters[1].Value = pEntity.DateCode;
			parameters[2].Value = pEntity.WxUV;
			parameters[3].Value = pEntity.WxOrderVipCount;
			parameters[4].Value = pEntity.WxOrderVipPayCount;
			parameters[5].Value = pEntity.WxPV;
			parameters[6].Value = pEntity.WxOrderCount;
			parameters[7].Value = pEntity.WxOrderPayCount;
			parameters[8].Value = pEntity.WxOrderMoney;
			parameters[9].Value = pEntity.WxOrderPayMoney;
			parameters[10].Value = pEntity.WxOrderAVG;
			parameters[11].Value = pEntity.LastWxUV;
			parameters[12].Value = pEntity.LastWxOrderVipCount;
			parameters[13].Value = pEntity.LastWxOrderVipPayCount;
			parameters[14].Value = pEntity.LastWxPV;
			parameters[15].Value = pEntity.LastWxOrderCount;
			parameters[16].Value = pEntity.LastWxOrderPayCount;
			parameters[17].Value = pEntity.LastWxOrderMoney;
			parameters[18].Value = pEntity.LastWxOrderPayMoney;
			parameters[19].Value = pEntity.LastWxOrderAVG;
			parameters[20].Value = pEntity.Rate_OrderVipCount_UV;
			parameters[21].Value = pEntity.Rate_OrderVipPayCount_OrderVipCount;
			parameters[22].Value = pEntity.Rate_OrderVipPayCount_UV;
			parameters[23].Value = pEntity.Rate_UV_Last;
			parameters[24].Value = pEntity.Rate_PV_Last;
			parameters[25].Value = pEntity.Rate_OrderVipCount_Last;
			parameters[26].Value = pEntity.Rate_OrderCount_Last;
			parameters[27].Value = pEntity.Rate_OrderMoney_Last;
			parameters[28].Value = pEntity.Rate_OrderVipPayCount_Last;
			parameters[29].Value = pEntity.Rate_OrderPayCount_Last;
			parameters[30].Value = pEntity.Rate_OrderPayMoney_Last;
			parameters[31].Value = pEntity.Rate_OrderAVG_Last;
			parameters[32].Value = pEntity.LogIDs;
			parameters[33].Value = pEntity.ID;

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
        public void Update(R_WxO2OPanel_30DaysEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_WxO2OPanel_30DaysEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(R_WxO2OPanel_30DaysEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ID.Value, pTran);           
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
            sql.AppendLine("update [R_WxO2OPanel_30Days] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(R_WxO2OPanel_30DaysEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(R_WxO2OPanel_30DaysEntity[] pEntities)
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
            sql.AppendLine("update [R_WxO2OPanel_30Days] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_WxO2OPanel_30DaysEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_30Days] where 1=1  ");
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
            List<R_WxO2OPanel_30DaysEntity> list = new List<R_WxO2OPanel_30DaysEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_30DaysEntity m;
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
        public PagedQueryResult<R_WxO2OPanel_30DaysEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [R_WxO2OPanel_30Days] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [R_WxO2OPanel_30Days] where 1=1  ");
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
            PagedQueryResult<R_WxO2OPanel_30DaysEntity> result = new PagedQueryResult<R_WxO2OPanel_30DaysEntity>();
            List<R_WxO2OPanel_30DaysEntity> list = new List<R_WxO2OPanel_30DaysEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_30DaysEntity m;
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
        public R_WxO2OPanel_30DaysEntity[] QueryByEntity(R_WxO2OPanel_30DaysEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_WxO2OPanel_30DaysEntity> PagedQueryByEntity(R_WxO2OPanel_30DaysEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_WxO2OPanel_30DaysEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.DateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.WxUV!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxUV", Value = pQueryEntity.WxUV });
            if (pQueryEntity.WxOrderVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderVipCount", Value = pQueryEntity.WxOrderVipCount });
            if (pQueryEntity.WxOrderVipPayCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderVipPayCount", Value = pQueryEntity.WxOrderVipPayCount });
            if (pQueryEntity.WxPV!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxPV", Value = pQueryEntity.WxPV });
            if (pQueryEntity.WxOrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderCount", Value = pQueryEntity.WxOrderCount });
            if (pQueryEntity.WxOrderPayCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderPayCount", Value = pQueryEntity.WxOrderPayCount });
            if (pQueryEntity.WxOrderMoney!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderMoney", Value = pQueryEntity.WxOrderMoney });
            if (pQueryEntity.WxOrderPayMoney!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderPayMoney", Value = pQueryEntity.WxOrderPayMoney });
            if (pQueryEntity.WxOrderAVG!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderAVG", Value = pQueryEntity.WxOrderAVG });
            if (pQueryEntity.LastWxUV!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxUV", Value = pQueryEntity.LastWxUV });
            if (pQueryEntity.LastWxOrderVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxOrderVipCount", Value = pQueryEntity.LastWxOrderVipCount });
            if (pQueryEntity.LastWxOrderVipPayCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxOrderVipPayCount", Value = pQueryEntity.LastWxOrderVipPayCount });
            if (pQueryEntity.LastWxPV!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxPV", Value = pQueryEntity.LastWxPV });
            if (pQueryEntity.LastWxOrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxOrderCount", Value = pQueryEntity.LastWxOrderCount });
            if (pQueryEntity.LastWxOrderPayCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxOrderPayCount", Value = pQueryEntity.LastWxOrderPayCount });
            if (pQueryEntity.LastWxOrderMoney!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxOrderMoney", Value = pQueryEntity.LastWxOrderMoney });
            if (pQueryEntity.LastWxOrderPayMoney!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxOrderPayMoney", Value = pQueryEntity.LastWxOrderPayMoney });
            if (pQueryEntity.LastWxOrderAVG!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastWxOrderAVG", Value = pQueryEntity.LastWxOrderAVG });
            if (pQueryEntity.Rate_OrderVipCount_UV!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderVipCount_UV", Value = pQueryEntity.Rate_OrderVipCount_UV });
            if (pQueryEntity.Rate_OrderVipPayCount_OrderVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderVipPayCount_OrderVipCount", Value = pQueryEntity.Rate_OrderVipPayCount_OrderVipCount });
            if (pQueryEntity.Rate_OrderVipPayCount_UV!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderVipPayCount_UV", Value = pQueryEntity.Rate_OrderVipPayCount_UV });
            if (pQueryEntity.Rate_UV_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_UV_Last", Value = pQueryEntity.Rate_UV_Last });
            if (pQueryEntity.Rate_PV_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_PV_Last", Value = pQueryEntity.Rate_PV_Last });
            if (pQueryEntity.Rate_OrderVipCount_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderVipCount_Last", Value = pQueryEntity.Rate_OrderVipCount_Last });
            if (pQueryEntity.Rate_OrderCount_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderCount_Last", Value = pQueryEntity.Rate_OrderCount_Last });
            if (pQueryEntity.Rate_OrderMoney_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderMoney_Last", Value = pQueryEntity.Rate_OrderMoney_Last });
            if (pQueryEntity.Rate_OrderVipPayCount_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderVipPayCount_Last", Value = pQueryEntity.Rate_OrderVipPayCount_Last });
            if (pQueryEntity.Rate_OrderPayCount_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderPayCount_Last", Value = pQueryEntity.Rate_OrderPayCount_Last });
            if (pQueryEntity.Rate_OrderPayMoney_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderPayMoney_Last", Value = pQueryEntity.Rate_OrderPayMoney_Last });
            if (pQueryEntity.Rate_OrderAVG_Last!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rate_OrderAVG_Last", Value = pQueryEntity.Rate_OrderAVG_Last });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LogIDs!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogIDs", Value = pQueryEntity.LogIDs });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out R_WxO2OPanel_30DaysEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new R_WxO2OPanel_30DaysEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  (Guid)pReader["ID"];
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["DateCode"] != DBNull.Value)
			{
				pInstance.DateCode = (DateTime?)pReader["DateCode"];
			}
			if (pReader["WxUV"] != DBNull.Value)
			{
				pInstance.WxUV =   Convert.ToInt32(pReader["WxUV"]);
			}
			if (pReader["WxOrderVipCount"] != DBNull.Value)
			{
				pInstance.WxOrderVipCount =   Convert.ToInt32(pReader["WxOrderVipCount"]);
			}
			if (pReader["WxOrderVipPayCount"] != DBNull.Value)
			{
				pInstance.WxOrderVipPayCount =   Convert.ToInt32(pReader["WxOrderVipPayCount"]);
			}
			if (pReader["WxPV"] != DBNull.Value)
			{
				pInstance.WxPV =   Convert.ToInt32(pReader["WxPV"]);
			}
			if (pReader["WxOrderCount"] != DBNull.Value)
			{
				pInstance.WxOrderCount =   Convert.ToInt32(pReader["WxOrderCount"]);
			}
			if (pReader["WxOrderPayCount"] != DBNull.Value)
			{
				pInstance.WxOrderPayCount =   Convert.ToInt32(pReader["WxOrderPayCount"]);
			}
			if (pReader["WxOrderMoney"] != DBNull.Value)
			{
				pInstance.WxOrderMoney =  Convert.ToDecimal(pReader["WxOrderMoney"]);
			}
			if (pReader["WxOrderPayMoney"] != DBNull.Value)
			{
				pInstance.WxOrderPayMoney =  Convert.ToDecimal(pReader["WxOrderPayMoney"]);
			}
			if (pReader["WxOrderAVG"] != DBNull.Value)
			{
				pInstance.WxOrderAVG =  Convert.ToDecimal(pReader["WxOrderAVG"]);
			}
			if (pReader["LastWxUV"] != DBNull.Value)
			{
				pInstance.LastWxUV =   Convert.ToInt32(pReader["LastWxUV"]);
			}
			if (pReader["LastWxOrderVipCount"] != DBNull.Value)
			{
				pInstance.LastWxOrderVipCount =   Convert.ToInt32(pReader["LastWxOrderVipCount"]);
			}
			if (pReader["LastWxOrderVipPayCount"] != DBNull.Value)
			{
				pInstance.LastWxOrderVipPayCount =   Convert.ToInt32(pReader["LastWxOrderVipPayCount"]);
			}
			if (pReader["LastWxPV"] != DBNull.Value)
			{
				pInstance.LastWxPV =   Convert.ToInt32(pReader["LastWxPV"]);
			}
			if (pReader["LastWxOrderCount"] != DBNull.Value)
			{
				pInstance.LastWxOrderCount =   Convert.ToInt32(pReader["LastWxOrderCount"]);
			}
			if (pReader["LastWxOrderPayCount"] != DBNull.Value)
			{
				pInstance.LastWxOrderPayCount =   Convert.ToInt32(pReader["LastWxOrderPayCount"]);
			}
			if (pReader["LastWxOrderMoney"] != DBNull.Value)
			{
				pInstance.LastWxOrderMoney =  Convert.ToDecimal(pReader["LastWxOrderMoney"]);
			}
			if (pReader["LastWxOrderPayMoney"] != DBNull.Value)
			{
				pInstance.LastWxOrderPayMoney =  Convert.ToDecimal(pReader["LastWxOrderPayMoney"]);
			}
			if (pReader["LastWxOrderAVG"] != DBNull.Value)
			{
				pInstance.LastWxOrderAVG =  Convert.ToDecimal(pReader["LastWxOrderAVG"]);
			}
			if (pReader["Rate_OrderVipCount_UV"] != DBNull.Value)
			{
				pInstance.Rate_OrderVipCount_UV =  Convert.ToString(pReader["Rate_OrderVipCount_UV"]);
			}
			if (pReader["Rate_OrderVipPayCount_OrderVipCount"] != DBNull.Value)
			{
				pInstance.Rate_OrderVipPayCount_OrderVipCount =  Convert.ToString(pReader["Rate_OrderVipPayCount_OrderVipCount"]);
			}
			if (pReader["Rate_OrderVipPayCount_UV"] != DBNull.Value)
			{
				pInstance.Rate_OrderVipPayCount_UV =  Convert.ToString(pReader["Rate_OrderVipPayCount_UV"]);
			}
			if (pReader["Rate_UV_Last"] != DBNull.Value)
			{
				pInstance.Rate_UV_Last =  Convert.ToString(pReader["Rate_UV_Last"]);
			}
			if (pReader["Rate_PV_Last"] != DBNull.Value)
			{
				pInstance.Rate_PV_Last =  Convert.ToString(pReader["Rate_PV_Last"]);
			}
			if (pReader["Rate_OrderVipCount_Last"] != DBNull.Value)
			{
				pInstance.Rate_OrderVipCount_Last =  Convert.ToString(pReader["Rate_OrderVipCount_Last"]);
			}
			if (pReader["Rate_OrderCount_Last"] != DBNull.Value)
			{
				pInstance.Rate_OrderCount_Last =  Convert.ToString(pReader["Rate_OrderCount_Last"]);
			}
			if (pReader["Rate_OrderMoney_Last"] != DBNull.Value)
			{
				pInstance.Rate_OrderMoney_Last =  Convert.ToString(pReader["Rate_OrderMoney_Last"]);
			}
			if (pReader["Rate_OrderVipPayCount_Last"] != DBNull.Value)
			{
				pInstance.Rate_OrderVipPayCount_Last =  Convert.ToString(pReader["Rate_OrderVipPayCount_Last"]);
			}
			if (pReader["Rate_OrderPayCount_Last"] != DBNull.Value)
			{
				pInstance.Rate_OrderPayCount_Last =  Convert.ToString(pReader["Rate_OrderPayCount_Last"]);
			}
			if (pReader["Rate_OrderPayMoney_Last"] != DBNull.Value)
			{
				pInstance.Rate_OrderPayMoney_Last =  Convert.ToString(pReader["Rate_OrderPayMoney_Last"]);
			}
			if (pReader["Rate_OrderAVG_Last"] != DBNull.Value)
			{
				pInstance.Rate_OrderAVG_Last =  Convert.ToString(pReader["Rate_OrderAVG_Last"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LogIDs"] != DBNull.Value)
			{
				pInstance.LogIDs =  Convert.ToString(pReader["LogIDs"]);
			}

        }
        #endregion
    }
}
