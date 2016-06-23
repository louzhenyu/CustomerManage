/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 14:12:49
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
    /// 表R_SRT_Home的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_SRT_HomeDAO : Base.BaseCPOSDAO, ICRUDable<R_SRT_HomeEntity>, IQueryable<R_SRT_HomeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_SRT_HomeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(R_SRT_HomeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(R_SRT_HomeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_SRT_Home](");
            strSql.Append("[DateCode],[CustomerId],[RTTotalSalesAmount],[DayUserRTSalesAmount],[DayVipRTSalesAmount],[RTDay7SalesAmount],[RTLastDay7SalesAmount],[RTDay30SalesAmount],[RTLastDay30SalesAmount],[RTTotalCount],[DayAddUserRTCount],[DayAddVipRTCount],[Day7RTOrderCount],[LastDay7RTOrderCount],[Day7RTOrderCountW2W],[Day30RTOrderCount],[LastDay30RTOrderCount],[Day30RTOrderCountM2M],[Day7RTAC],[LastDay7RTAC],[Day7RTACW2W],[Day30RTAC],[LastDay30RTAC],[Day30RTACM2M],[Day7ActiveRTCount],[LastDay7ActiveRTCount],[Day7ActiveRTCountW2W],[Day30ActiveRTCount],[LastDay30ActiveRTCount],[Day30ActiveRTCountM2M],[Day7RTShareCount],[LastDay7RTShareCount],[Day7RTShareCountW2W],[Day30RTShareCount],[LastDay30RTShareCount],[Day30RTShareCountM2M],[Day7AddRTCount],[LastDay7AddRTCount],[Day7AddRTCountW2W],[Day30AddRTCount],[LastDay30AddRTCount],[Day30AddRTCountM2M],[CreateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@DateCode,@CustomerId,@RTTotalSalesAmount,@DayUserRTSalesAmount,@DayVipRTSalesAmount,@RTDay7SalesAmount,@RTLastDay7SalesAmount,@RTDay30SalesAmount,@RTLastDay30SalesAmount,@RTTotalCount,@DayAddUserRTCount,@DayAddVipRTCount,@Day7RTOrderCount,@LastDay7RTOrderCount,@Day7RTOrderCountW2W,@Day30RTOrderCount,@LastDay30RTOrderCount,@Day30RTOrderCountM2M,@Day7RTAC,@LastDay7RTAC,@Day7RTACW2W,@Day30RTAC,@LastDay30RTAC,@Day30RTACM2M,@Day7ActiveRTCount,@LastDay7ActiveRTCount,@Day7ActiveRTCountW2W,@Day30ActiveRTCount,@LastDay30ActiveRTCount,@Day30ActiveRTCountM2M,@Day7RTShareCount,@LastDay7RTShareCount,@Day7RTShareCountW2W,@Day30RTShareCount,@LastDay30RTShareCount,@Day30RTShareCountM2M,@Day7AddRTCount,@LastDay7AddRTCount,@Day7AddRTCountW2W,@Day30AddRTCount,@LastDay30AddRTCount,@Day30AddRTCountM2M,@CreateTime,@ID)");            

			Guid? pkGuid;
			if (pEntity.ID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@RTTotalSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@DayUserRTSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@DayVipRTSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTDay7SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTLastDay7SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTDay30SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTLastDay30SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTTotalCount",SqlDbType.Int),
					new SqlParameter("@DayAddUserRTCount",SqlDbType.Int),
					new SqlParameter("@DayAddVipRTCount",SqlDbType.Int),
					new SqlParameter("@Day7RTOrderCount",SqlDbType.Int),
					new SqlParameter("@LastDay7RTOrderCount",SqlDbType.Int),
					new SqlParameter("@Day7RTOrderCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30RTOrderCount",SqlDbType.Int),
					new SqlParameter("@LastDay30RTOrderCount",SqlDbType.Int),
					new SqlParameter("@Day30RTOrderCountM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7RTAC",SqlDbType.Decimal),
					new SqlParameter("@LastDay7RTAC",SqlDbType.Decimal),
					new SqlParameter("@Day7RTACW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30RTAC",SqlDbType.Decimal),
					new SqlParameter("@LastDay30RTAC",SqlDbType.Decimal),
					new SqlParameter("@Day30RTACM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay7ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day7ActiveRTCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay30ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day30ActiveRTCountM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7RTShareCount",SqlDbType.Int),
					new SqlParameter("@LastDay7RTShareCount",SqlDbType.Int),
					new SqlParameter("@Day7RTShareCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30RTShareCount",SqlDbType.Int),
					new SqlParameter("@LastDay30RTShareCount",SqlDbType.Int),
					new SqlParameter("@Day30RTShareCountM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7AddRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay7AddRTCount",SqlDbType.Int),
					new SqlParameter("@Day7AddRTCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30AddRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay30AddRTCount",SqlDbType.Int),
					new SqlParameter("@Day30AddRTCountM2M",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.RTTotalSalesAmount;
			parameters[3].Value = pEntity.DayUserRTSalesAmount;
			parameters[4].Value = pEntity.DayVipRTSalesAmount;
			parameters[5].Value = pEntity.RTDay7SalesAmount;
			parameters[6].Value = pEntity.RTLastDay7SalesAmount;
			parameters[7].Value = pEntity.RTDay30SalesAmount;
			parameters[8].Value = pEntity.RTLastDay30SalesAmount;
			parameters[9].Value = pEntity.RTTotalCount;
			parameters[10].Value = pEntity.DayAddUserRTCount;
			parameters[11].Value = pEntity.DayAddVipRTCount;
			parameters[12].Value = pEntity.Day7RTOrderCount;
			parameters[13].Value = pEntity.LastDay7RTOrderCount;
			parameters[14].Value = pEntity.Day7RTOrderCountW2W;
			parameters[15].Value = pEntity.Day30RTOrderCount;
			parameters[16].Value = pEntity.LastDay30RTOrderCount;
			parameters[17].Value = pEntity.Day30RTOrderCountM2M;
			parameters[18].Value = pEntity.Day7RTAC;
			parameters[19].Value = pEntity.LastDay7RTAC;
			parameters[20].Value = pEntity.Day7RTACW2W;
			parameters[21].Value = pEntity.Day30RTAC;
			parameters[22].Value = pEntity.LastDay30RTAC;
			parameters[23].Value = pEntity.Day30RTACM2M;
			parameters[24].Value = pEntity.Day7ActiveRTCount;
			parameters[25].Value = pEntity.LastDay7ActiveRTCount;
			parameters[26].Value = pEntity.Day7ActiveRTCountW2W;
			parameters[27].Value = pEntity.Day30ActiveRTCount;
			parameters[28].Value = pEntity.LastDay30ActiveRTCount;
			parameters[29].Value = pEntity.Day30ActiveRTCountM2M;
			parameters[30].Value = pEntity.Day7RTShareCount;
			parameters[31].Value = pEntity.LastDay7RTShareCount;
			parameters[32].Value = pEntity.Day7RTShareCountW2W;
			parameters[33].Value = pEntity.Day30RTShareCount;
			parameters[34].Value = pEntity.LastDay30RTShareCount;
			parameters[35].Value = pEntity.Day30RTShareCountM2M;
			parameters[36].Value = pEntity.Day7AddRTCount;
			parameters[37].Value = pEntity.LastDay7AddRTCount;
			parameters[38].Value = pEntity.Day7AddRTCountW2W;
			parameters[39].Value = pEntity.Day30AddRTCount;
			parameters[40].Value = pEntity.LastDay30AddRTCount;
			parameters[41].Value = pEntity.Day30AddRTCountM2M;
			parameters[42].Value = pEntity.CreateTime;
			parameters[43].Value = pkGuid;

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
        public R_SRT_HomeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_Home] where ID='{0}'  ", id.ToString());
            //读取数据
            R_SRT_HomeEntity m = null;
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
        public R_SRT_HomeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_Home] where 1=1 ");
            //读取数据
            List<R_SRT_HomeEntity> list = new List<R_SRT_HomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_HomeEntity m;
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
        public void Update(R_SRT_HomeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(R_SRT_HomeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
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
            strSql.Append("update [R_SRT_Home] set ");
                        if (pIsUpdateNullField || pEntity.DateCode!=null)
                strSql.Append( "[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.RTTotalSalesAmount!=null)
                strSql.Append( "[RTTotalSalesAmount]=@RTTotalSalesAmount,");
            if (pIsUpdateNullField || pEntity.DayUserRTSalesAmount!=null)
                strSql.Append( "[DayUserRTSalesAmount]=@DayUserRTSalesAmount,");
            if (pIsUpdateNullField || pEntity.DayVipRTSalesAmount!=null)
                strSql.Append( "[DayVipRTSalesAmount]=@DayVipRTSalesAmount,");
            if (pIsUpdateNullField || pEntity.RTDay7SalesAmount!=null)
                strSql.Append( "[RTDay7SalesAmount]=@RTDay7SalesAmount,");
            if (pIsUpdateNullField || pEntity.RTLastDay7SalesAmount!=null)
                strSql.Append( "[RTLastDay7SalesAmount]=@RTLastDay7SalesAmount,");
            if (pIsUpdateNullField || pEntity.RTDay30SalesAmount!=null)
                strSql.Append( "[RTDay30SalesAmount]=@RTDay30SalesAmount,");
            if (pIsUpdateNullField || pEntity.RTLastDay30SalesAmount!=null)
                strSql.Append( "[RTLastDay30SalesAmount]=@RTLastDay30SalesAmount,");
            if (pIsUpdateNullField || pEntity.RTTotalCount!=null)
                strSql.Append( "[RTTotalCount]=@RTTotalCount,");
            if (pIsUpdateNullField || pEntity.DayAddUserRTCount!=null)
                strSql.Append( "[DayAddUserRTCount]=@DayAddUserRTCount,");
            if (pIsUpdateNullField || pEntity.DayAddVipRTCount!=null)
                strSql.Append( "[DayAddVipRTCount]=@DayAddVipRTCount,");
            if (pIsUpdateNullField || pEntity.Day7RTOrderCount!=null)
                strSql.Append( "[Day7RTOrderCount]=@Day7RTOrderCount,");
            if (pIsUpdateNullField || pEntity.LastDay7RTOrderCount!=null)
                strSql.Append( "[LastDay7RTOrderCount]=@LastDay7RTOrderCount,");
            if (pIsUpdateNullField || pEntity.Day7RTOrderCountW2W!=null)
                strSql.Append( "[Day7RTOrderCountW2W]=@Day7RTOrderCountW2W,");
            if (pIsUpdateNullField || pEntity.Day30RTOrderCount!=null)
                strSql.Append( "[Day30RTOrderCount]=@Day30RTOrderCount,");
            if (pIsUpdateNullField || pEntity.LastDay30RTOrderCount!=null)
                strSql.Append( "[LastDay30RTOrderCount]=@LastDay30RTOrderCount,");
            if (pIsUpdateNullField || pEntity.Day30RTOrderCountM2M!=null)
                strSql.Append( "[Day30RTOrderCountM2M]=@Day30RTOrderCountM2M,");
            if (pIsUpdateNullField || pEntity.Day7RTAC!=null)
                strSql.Append( "[Day7RTAC]=@Day7RTAC,");
            if (pIsUpdateNullField || pEntity.LastDay7RTAC!=null)
                strSql.Append( "[LastDay7RTAC]=@LastDay7RTAC,");
            if (pIsUpdateNullField || pEntity.Day7RTACW2W!=null)
                strSql.Append( "[Day7RTACW2W]=@Day7RTACW2W,");
            if (pIsUpdateNullField || pEntity.Day30RTAC!=null)
                strSql.Append( "[Day30RTAC]=@Day30RTAC,");
            if (pIsUpdateNullField || pEntity.LastDay30RTAC!=null)
                strSql.Append( "[LastDay30RTAC]=@LastDay30RTAC,");
            if (pIsUpdateNullField || pEntity.Day30RTACM2M!=null)
                strSql.Append( "[Day30RTACM2M]=@Day30RTACM2M,");
            if (pIsUpdateNullField || pEntity.Day7ActiveRTCount!=null)
                strSql.Append( "[Day7ActiveRTCount]=@Day7ActiveRTCount,");
            if (pIsUpdateNullField || pEntity.LastDay7ActiveRTCount!=null)
                strSql.Append( "[LastDay7ActiveRTCount]=@LastDay7ActiveRTCount,");
            if (pIsUpdateNullField || pEntity.Day7ActiveRTCountW2W!=null)
                strSql.Append( "[Day7ActiveRTCountW2W]=@Day7ActiveRTCountW2W,");
            if (pIsUpdateNullField || pEntity.Day30ActiveRTCount!=null)
                strSql.Append( "[Day30ActiveRTCount]=@Day30ActiveRTCount,");
            if (pIsUpdateNullField || pEntity.LastDay30ActiveRTCount!=null)
                strSql.Append( "[LastDay30ActiveRTCount]=@LastDay30ActiveRTCount,");
            if (pIsUpdateNullField || pEntity.Day30ActiveRTCountM2M!=null)
                strSql.Append( "[Day30ActiveRTCountM2M]=@Day30ActiveRTCountM2M,");
            if (pIsUpdateNullField || pEntity.Day7RTShareCount!=null)
                strSql.Append( "[Day7RTShareCount]=@Day7RTShareCount,");
            if (pIsUpdateNullField || pEntity.LastDay7RTShareCount!=null)
                strSql.Append( "[LastDay7RTShareCount]=@LastDay7RTShareCount,");
            if (pIsUpdateNullField || pEntity.Day7RTShareCountW2W!=null)
                strSql.Append( "[Day7RTShareCountW2W]=@Day7RTShareCountW2W,");
            if (pIsUpdateNullField || pEntity.Day30RTShareCount!=null)
                strSql.Append( "[Day30RTShareCount]=@Day30RTShareCount,");
            if (pIsUpdateNullField || pEntity.LastDay30RTShareCount!=null)
                strSql.Append( "[LastDay30RTShareCount]=@LastDay30RTShareCount,");
            if (pIsUpdateNullField || pEntity.Day30RTShareCountM2M!=null)
                strSql.Append( "[Day30RTShareCountM2M]=@Day30RTShareCountM2M,");
            if (pIsUpdateNullField || pEntity.Day7AddRTCount!=null)
                strSql.Append( "[Day7AddRTCount]=@Day7AddRTCount,");
            if (pIsUpdateNullField || pEntity.LastDay7AddRTCount!=null)
                strSql.Append( "[LastDay7AddRTCount]=@LastDay7AddRTCount,");
            if (pIsUpdateNullField || pEntity.Day7AddRTCountW2W!=null)
                strSql.Append( "[Day7AddRTCountW2W]=@Day7AddRTCountW2W,");
            if (pIsUpdateNullField || pEntity.Day30AddRTCount!=null)
                strSql.Append( "[Day30AddRTCount]=@Day30AddRTCount,");
            if (pIsUpdateNullField || pEntity.LastDay30AddRTCount!=null)
                strSql.Append( "[LastDay30AddRTCount]=@LastDay30AddRTCount,");
            if (pIsUpdateNullField || pEntity.Day30AddRTCountM2M!=null)
                strSql.Append( "[Day30AddRTCountM2M]=@Day30AddRTCountM2M");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@RTTotalSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@DayUserRTSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@DayVipRTSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTDay7SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTLastDay7SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTDay30SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTLastDay30SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@RTTotalCount",SqlDbType.Int),
					new SqlParameter("@DayAddUserRTCount",SqlDbType.Int),
					new SqlParameter("@DayAddVipRTCount",SqlDbType.Int),
					new SqlParameter("@Day7RTOrderCount",SqlDbType.Int),
					new SqlParameter("@LastDay7RTOrderCount",SqlDbType.Int),
					new SqlParameter("@Day7RTOrderCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30RTOrderCount",SqlDbType.Int),
					new SqlParameter("@LastDay30RTOrderCount",SqlDbType.Int),
					new SqlParameter("@Day30RTOrderCountM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7RTAC",SqlDbType.Decimal),
					new SqlParameter("@LastDay7RTAC",SqlDbType.Decimal),
					new SqlParameter("@Day7RTACW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30RTAC",SqlDbType.Decimal),
					new SqlParameter("@LastDay30RTAC",SqlDbType.Decimal),
					new SqlParameter("@Day30RTACM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay7ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day7ActiveRTCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay30ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day30ActiveRTCountM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7RTShareCount",SqlDbType.Int),
					new SqlParameter("@LastDay7RTShareCount",SqlDbType.Int),
					new SqlParameter("@Day7RTShareCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30RTShareCount",SqlDbType.Int),
					new SqlParameter("@LastDay30RTShareCount",SqlDbType.Int),
					new SqlParameter("@Day30RTShareCountM2M",SqlDbType.Decimal),
					new SqlParameter("@Day7AddRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay7AddRTCount",SqlDbType.Int),
					new SqlParameter("@Day7AddRTCountW2W",SqlDbType.Decimal),
					new SqlParameter("@Day30AddRTCount",SqlDbType.Int),
					new SqlParameter("@LastDay30AddRTCount",SqlDbType.Int),
					new SqlParameter("@Day30AddRTCountM2M",SqlDbType.Decimal),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.RTTotalSalesAmount;
			parameters[3].Value = pEntity.DayUserRTSalesAmount;
			parameters[4].Value = pEntity.DayVipRTSalesAmount;
			parameters[5].Value = pEntity.RTDay7SalesAmount;
			parameters[6].Value = pEntity.RTLastDay7SalesAmount;
			parameters[7].Value = pEntity.RTDay30SalesAmount;
			parameters[8].Value = pEntity.RTLastDay30SalesAmount;
			parameters[9].Value = pEntity.RTTotalCount;
			parameters[10].Value = pEntity.DayAddUserRTCount;
			parameters[11].Value = pEntity.DayAddVipRTCount;
			parameters[12].Value = pEntity.Day7RTOrderCount;
			parameters[13].Value = pEntity.LastDay7RTOrderCount;
			parameters[14].Value = pEntity.Day7RTOrderCountW2W;
			parameters[15].Value = pEntity.Day30RTOrderCount;
			parameters[16].Value = pEntity.LastDay30RTOrderCount;
			parameters[17].Value = pEntity.Day30RTOrderCountM2M;
			parameters[18].Value = pEntity.Day7RTAC;
			parameters[19].Value = pEntity.LastDay7RTAC;
			parameters[20].Value = pEntity.Day7RTACW2W;
			parameters[21].Value = pEntity.Day30RTAC;
			parameters[22].Value = pEntity.LastDay30RTAC;
			parameters[23].Value = pEntity.Day30RTACM2M;
			parameters[24].Value = pEntity.Day7ActiveRTCount;
			parameters[25].Value = pEntity.LastDay7ActiveRTCount;
			parameters[26].Value = pEntity.Day7ActiveRTCountW2W;
			parameters[27].Value = pEntity.Day30ActiveRTCount;
			parameters[28].Value = pEntity.LastDay30ActiveRTCount;
			parameters[29].Value = pEntity.Day30ActiveRTCountM2M;
			parameters[30].Value = pEntity.Day7RTShareCount;
			parameters[31].Value = pEntity.LastDay7RTShareCount;
			parameters[32].Value = pEntity.Day7RTShareCountW2W;
			parameters[33].Value = pEntity.Day30RTShareCount;
			parameters[34].Value = pEntity.LastDay30RTShareCount;
			parameters[35].Value = pEntity.Day30RTShareCountM2M;
			parameters[36].Value = pEntity.Day7AddRTCount;
			parameters[37].Value = pEntity.LastDay7AddRTCount;
			parameters[38].Value = pEntity.Day7AddRTCountW2W;
			parameters[39].Value = pEntity.Day30AddRTCount;
			parameters[40].Value = pEntity.LastDay30AddRTCount;
			parameters[41].Value = pEntity.Day30AddRTCountM2M;
			parameters[42].Value = pEntity.ID;

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
        public void Update(R_SRT_HomeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_SRT_HomeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(R_SRT_HomeEntity pEntity, IDbTransaction pTran)
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
            sql.AppendLine("update [R_SRT_Home] set  where ID=@ID;");
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
        public void Delete(R_SRT_HomeEntity[] pEntities, IDbTransaction pTran)
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
        public void Delete(R_SRT_HomeEntity[] pEntities)
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
            sql.AppendLine("update [R_SRT_Home] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_SRT_HomeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_Home] where 1=1  ");
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
            List<R_SRT_HomeEntity> list = new List<R_SRT_HomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_HomeEntity m;
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
        public PagedQueryResult<R_SRT_HomeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
            pagedSql.AppendFormat(") as ___rn,* from [R_SRT_Home] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [R_SRT_Home] where 1=1  ");
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
            PagedQueryResult<R_SRT_HomeEntity> result = new PagedQueryResult<R_SRT_HomeEntity>();
            List<R_SRT_HomeEntity> list = new List<R_SRT_HomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_HomeEntity m;
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
        public R_SRT_HomeEntity[] QueryByEntity(R_SRT_HomeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_SRT_HomeEntity> PagedQueryByEntity(R_SRT_HomeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_SRT_HomeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.DateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.RTTotalSalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RTTotalSalesAmount", Value = pQueryEntity.RTTotalSalesAmount });
            if (pQueryEntity.DayUserRTSalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DayUserRTSalesAmount", Value = pQueryEntity.DayUserRTSalesAmount });
            if (pQueryEntity.DayVipRTSalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DayVipRTSalesAmount", Value = pQueryEntity.DayVipRTSalesAmount });
            if (pQueryEntity.RTDay7SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RTDay7SalesAmount", Value = pQueryEntity.RTDay7SalesAmount });
            if (pQueryEntity.RTLastDay7SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RTLastDay7SalesAmount", Value = pQueryEntity.RTLastDay7SalesAmount });
            if (pQueryEntity.RTDay30SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RTDay30SalesAmount", Value = pQueryEntity.RTDay30SalesAmount });
            if (pQueryEntity.RTLastDay30SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RTLastDay30SalesAmount", Value = pQueryEntity.RTLastDay30SalesAmount });
            if (pQueryEntity.RTTotalCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RTTotalCount", Value = pQueryEntity.RTTotalCount });
            if (pQueryEntity.DayAddUserRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DayAddUserRTCount", Value = pQueryEntity.DayAddUserRTCount });
            if (pQueryEntity.DayAddVipRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DayAddVipRTCount", Value = pQueryEntity.DayAddVipRTCount });
            if (pQueryEntity.Day7RTOrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7RTOrderCount", Value = pQueryEntity.Day7RTOrderCount });
            if (pQueryEntity.LastDay7RTOrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay7RTOrderCount", Value = pQueryEntity.LastDay7RTOrderCount });
            if (pQueryEntity.Day7RTOrderCountW2W!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7RTOrderCountW2W", Value = pQueryEntity.Day7RTOrderCountW2W });
            if (pQueryEntity.Day30RTOrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30RTOrderCount", Value = pQueryEntity.Day30RTOrderCount });
            if (pQueryEntity.LastDay30RTOrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay30RTOrderCount", Value = pQueryEntity.LastDay30RTOrderCount });
            if (pQueryEntity.Day30RTOrderCountM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30RTOrderCountM2M", Value = pQueryEntity.Day30RTOrderCountM2M });
            if (pQueryEntity.Day7RTAC!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7RTAC", Value = pQueryEntity.Day7RTAC });
            if (pQueryEntity.LastDay7RTAC!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay7RTAC", Value = pQueryEntity.LastDay7RTAC });
            if (pQueryEntity.Day7RTACW2W!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7RTACW2W", Value = pQueryEntity.Day7RTACW2W });
            if (pQueryEntity.Day30RTAC!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30RTAC", Value = pQueryEntity.Day30RTAC });
            if (pQueryEntity.LastDay30RTAC!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay30RTAC", Value = pQueryEntity.LastDay30RTAC });
            if (pQueryEntity.Day30RTACM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30RTACM2M", Value = pQueryEntity.Day30RTACM2M });
            if (pQueryEntity.Day7ActiveRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7ActiveRTCount", Value = pQueryEntity.Day7ActiveRTCount });
            if (pQueryEntity.LastDay7ActiveRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay7ActiveRTCount", Value = pQueryEntity.LastDay7ActiveRTCount });
            if (pQueryEntity.Day7ActiveRTCountW2W!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7ActiveRTCountW2W", Value = pQueryEntity.Day7ActiveRTCountW2W });
            if (pQueryEntity.Day30ActiveRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30ActiveRTCount", Value = pQueryEntity.Day30ActiveRTCount });
            if (pQueryEntity.LastDay30ActiveRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay30ActiveRTCount", Value = pQueryEntity.LastDay30ActiveRTCount });
            if (pQueryEntity.Day30ActiveRTCountM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30ActiveRTCountM2M", Value = pQueryEntity.Day30ActiveRTCountM2M });
            if (pQueryEntity.Day7RTShareCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7RTShareCount", Value = pQueryEntity.Day7RTShareCount });
            if (pQueryEntity.LastDay7RTShareCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay7RTShareCount", Value = pQueryEntity.LastDay7RTShareCount });
            if (pQueryEntity.Day7RTShareCountW2W!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7RTShareCountW2W", Value = pQueryEntity.Day7RTShareCountW2W });
            if (pQueryEntity.Day30RTShareCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30RTShareCount", Value = pQueryEntity.Day30RTShareCount });
            if (pQueryEntity.LastDay30RTShareCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay30RTShareCount", Value = pQueryEntity.LastDay30RTShareCount });
            if (pQueryEntity.Day30RTShareCountM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30RTShareCountM2M", Value = pQueryEntity.Day30RTShareCountM2M });
            if (pQueryEntity.Day7AddRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7AddRTCount", Value = pQueryEntity.Day7AddRTCount });
            if (pQueryEntity.LastDay7AddRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay7AddRTCount", Value = pQueryEntity.LastDay7AddRTCount });
            if (pQueryEntity.Day7AddRTCountW2W!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day7AddRTCountW2W", Value = pQueryEntity.Day7AddRTCountW2W });
            if (pQueryEntity.Day30AddRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30AddRTCount", Value = pQueryEntity.Day30AddRTCount });
            if (pQueryEntity.LastDay30AddRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDay30AddRTCount", Value = pQueryEntity.LastDay30AddRTCount });
            if (pQueryEntity.Day30AddRTCountM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30AddRTCountM2M", Value = pQueryEntity.Day30AddRTCountM2M });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out R_SRT_HomeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new R_SRT_HomeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  (Guid)pReader["ID"];
			}
			if (pReader["DateCode"] != DBNull.Value)
			{
				pInstance.DateCode = Convert.ToDateTime(pReader["DateCode"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["RTTotalSalesAmount"] != DBNull.Value)
			{
				pInstance.RTTotalSalesAmount =  Convert.ToDecimal(pReader["RTTotalSalesAmount"]);
			}
			if (pReader["DayUserRTSalesAmount"] != DBNull.Value)
			{
				pInstance.DayUserRTSalesAmount =  Convert.ToDecimal(pReader["DayUserRTSalesAmount"]);
			}
			if (pReader["DayVipRTSalesAmount"] != DBNull.Value)
			{
				pInstance.DayVipRTSalesAmount =  Convert.ToDecimal(pReader["DayVipRTSalesAmount"]);
			}
			if (pReader["RTDay7SalesAmount"] != DBNull.Value)
			{
				pInstance.RTDay7SalesAmount =  Convert.ToDecimal(pReader["RTDay7SalesAmount"]);
			}
			if (pReader["RTLastDay7SalesAmount"] != DBNull.Value)
			{
				pInstance.RTLastDay7SalesAmount =  Convert.ToDecimal(pReader["RTLastDay7SalesAmount"]);
			}
			if (pReader["RTDay30SalesAmount"] != DBNull.Value)
			{
				pInstance.RTDay30SalesAmount =  Convert.ToDecimal(pReader["RTDay30SalesAmount"]);
			}
			if (pReader["RTLastDay30SalesAmount"] != DBNull.Value)
			{
				pInstance.RTLastDay30SalesAmount =  Convert.ToDecimal(pReader["RTLastDay30SalesAmount"]);
			}
			if (pReader["RTTotalCount"] != DBNull.Value)
			{
				pInstance.RTTotalCount =   Convert.ToInt32(pReader["RTTotalCount"]);
			}
			if (pReader["DayAddUserRTCount"] != DBNull.Value)
			{
				pInstance.DayAddUserRTCount =   Convert.ToInt32(pReader["DayAddUserRTCount"]);
			}
			if (pReader["DayAddVipRTCount"] != DBNull.Value)
			{
				pInstance.DayAddVipRTCount =   Convert.ToInt32(pReader["DayAddVipRTCount"]);
			}
			if (pReader["Day7RTOrderCount"] != DBNull.Value)
			{
				pInstance.Day7RTOrderCount =   Convert.ToInt32(pReader["Day7RTOrderCount"]);
			}
			if (pReader["LastDay7RTOrderCount"] != DBNull.Value)
			{
				pInstance.LastDay7RTOrderCount =   Convert.ToInt32(pReader["LastDay7RTOrderCount"]);
			}
			if (pReader["Day7RTOrderCountW2W"] != DBNull.Value)
			{
				pInstance.Day7RTOrderCountW2W =  Convert.ToDecimal(pReader["Day7RTOrderCountW2W"]);
			}
			if (pReader["Day30RTOrderCount"] != DBNull.Value)
			{
				pInstance.Day30RTOrderCount =   Convert.ToInt32(pReader["Day30RTOrderCount"]);
			}
			if (pReader["LastDay30RTOrderCount"] != DBNull.Value)
			{
				pInstance.LastDay30RTOrderCount =   Convert.ToInt32(pReader["LastDay30RTOrderCount"]);
			}
			if (pReader["Day30RTOrderCountM2M"] != DBNull.Value)
			{
				pInstance.Day30RTOrderCountM2M =  Convert.ToDecimal(pReader["Day30RTOrderCountM2M"]);
			}
			if (pReader["Day7RTAC"] != DBNull.Value)
			{
				pInstance.Day7RTAC =  Convert.ToDecimal(pReader["Day7RTAC"]);
			}
			if (pReader["LastDay7RTAC"] != DBNull.Value)
			{
				pInstance.LastDay7RTAC =  Convert.ToDecimal(pReader["LastDay7RTAC"]);
			}
			if (pReader["Day7RTACW2W"] != DBNull.Value)
			{
				pInstance.Day7RTACW2W =  Convert.ToDecimal(pReader["Day7RTACW2W"]);
			}
			if (pReader["Day30RTAC"] != DBNull.Value)
			{
				pInstance.Day30RTAC =  Convert.ToDecimal(pReader["Day30RTAC"]);
			}
			if (pReader["LastDay30RTAC"] != DBNull.Value)
			{
				pInstance.LastDay30RTAC =  Convert.ToDecimal(pReader["LastDay30RTAC"]);
			}
			if (pReader["Day30RTACM2M"] != DBNull.Value)
			{
				pInstance.Day30RTACM2M =  Convert.ToDecimal(pReader["Day30RTACM2M"]);
			}
			if (pReader["Day7ActiveRTCount"] != DBNull.Value)
			{
				pInstance.Day7ActiveRTCount =   Convert.ToInt32(pReader["Day7ActiveRTCount"]);
			}
			if (pReader["LastDay7ActiveRTCount"] != DBNull.Value)
			{
				pInstance.LastDay7ActiveRTCount =   Convert.ToInt32(pReader["LastDay7ActiveRTCount"]);
			}
			if (pReader["Day7ActiveRTCountW2W"] != DBNull.Value)
			{
				pInstance.Day7ActiveRTCountW2W =  Convert.ToDecimal(pReader["Day7ActiveRTCountW2W"]);
			}
			if (pReader["Day30ActiveRTCount"] != DBNull.Value)
			{
				pInstance.Day30ActiveRTCount =   Convert.ToInt32(pReader["Day30ActiveRTCount"]);
			}
			if (pReader["LastDay30ActiveRTCount"] != DBNull.Value)
			{
				pInstance.LastDay30ActiveRTCount =   Convert.ToInt32(pReader["LastDay30ActiveRTCount"]);
			}
			if (pReader["Day30ActiveRTCountM2M"] != DBNull.Value)
			{
				pInstance.Day30ActiveRTCountM2M =  Convert.ToDecimal(pReader["Day30ActiveRTCountM2M"]);
			}
			if (pReader["Day7RTShareCount"] != DBNull.Value)
			{
				pInstance.Day7RTShareCount =   Convert.ToInt32(pReader["Day7RTShareCount"]);
			}
			if (pReader["LastDay7RTShareCount"] != DBNull.Value)
			{
				pInstance.LastDay7RTShareCount =   Convert.ToInt32(pReader["LastDay7RTShareCount"]);
			}
			if (pReader["Day7RTShareCountW2W"] != DBNull.Value)
			{
				pInstance.Day7RTShareCountW2W =  Convert.ToDecimal(pReader["Day7RTShareCountW2W"]);
			}
			if (pReader["Day30RTShareCount"] != DBNull.Value)
			{
				pInstance.Day30RTShareCount =   Convert.ToInt32(pReader["Day30RTShareCount"]);
			}
			if (pReader["LastDay30RTShareCount"] != DBNull.Value)
			{
				pInstance.LastDay30RTShareCount =   Convert.ToInt32(pReader["LastDay30RTShareCount"]);
			}
			if (pReader["Day30RTShareCountM2M"] != DBNull.Value)
			{
				pInstance.Day30RTShareCountM2M =  Convert.ToDecimal(pReader["Day30RTShareCountM2M"]);
			}
			if (pReader["Day7AddRTCount"] != DBNull.Value)
			{
				pInstance.Day7AddRTCount =   Convert.ToInt32(pReader["Day7AddRTCount"]);
			}
			if (pReader["LastDay7AddRTCount"] != DBNull.Value)
			{
				pInstance.LastDay7AddRTCount =   Convert.ToInt32(pReader["LastDay7AddRTCount"]);
			}
			if (pReader["Day7AddRTCountW2W"] != DBNull.Value)
			{
				pInstance.Day7AddRTCountW2W =  Convert.ToDecimal(pReader["Day7AddRTCountW2W"]);
			}
			if (pReader["Day30AddRTCount"] != DBNull.Value)
			{
				pInstance.Day30AddRTCount =   Convert.ToInt32(pReader["Day30AddRTCount"]);
			}
			if (pReader["LastDay30AddRTCount"] != DBNull.Value)
			{
				pInstance.LastDay30AddRTCount =   Convert.ToInt32(pReader["LastDay30AddRTCount"]);
			}
			if (pReader["Day30AddRTCountM2M"] != DBNull.Value)
			{
				pInstance.Day30AddRTCountM2M =  Convert.ToDecimal(pReader["Day30AddRTCountM2M"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}

        }
        #endregion
    }
}
