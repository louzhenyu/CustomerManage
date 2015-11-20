/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/2 15:39:11
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
    /// 表LEventsShare的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventsShareDAO : Base.BaseCPOSDAO, ICRUDable<LEventsShareEntity>, IQueryable<LEventsShareEntity>
    {

        public DataSet GetShareList(int pageIndex, int pageSize)
        {
            //组织SQL
            //string strSql = "SELECT A.ShareId,b.PrizesID,b.PrizeName,C.Title EventName,B.CountTotal,Case when [State]=1 then '启用'	when [State]=0 then '停用' end [State],count(d.VipID) WinnerCount";
            //strSql += " FROM [LEventsShare] a ";
            //strSql += " LEFT JOIN dbo.LPrizes b ON a.ShareId = b.EventId";
            //strSql += " LEFT join dbo.LEvents c ON A.EventId=C.EventID ";
            //strSql += " LEFT join LPrizeWinner d on b.PrizesID=d.PrizeID";
            //strSql += " WHERE   A.IsDelete = 0";
            //strSql += " GROUP BY A.ShareId,b.PrizesID,b.PrizeName,C.Title ,B.CountTotal,[State]";
            SqlParameter[] parameters = new SqlParameter[2] 
            { 
                 new SqlParameter{ParameterName="@PageIndex",SqlDbType=SqlDbType.Int,Value=pageIndex},
                new SqlParameter{ParameterName="@PageSize",SqlDbType=SqlDbType.Int,Value=pageSize}

            };

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_GetShareList", parameters);

        }


        /// <summary>
        /// 启用停用
        /// </summary>
        /// <param name="strEventId"></param>
        /// <param name="intEventStatus"></param>

        public void UpdateEventShareStatus(string strShareId, int intState)
        {
            var sql = string.Format("UPDATE dbo.LEventsShare SET State={0} WHERE ShareId='{1}'",intState, strShareId);
            this.SQLHelper.ExecuteNonQuery(sql.ToString());
        }

        #region 保存分享设置奖品
        /// <summary>
        /// 和活动共用一个奖品表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveSharePrize(LPrizesEntity entity)
        {

            SqlParameter[] parameters = new SqlParameter[7] 
            { 
                new SqlParameter{ParameterName="@EventId",SqlDbType=SqlDbType.NVarChar,Value=entity.EventId},
                new SqlParameter{ParameterName="@PrizeName",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizeName},
                new SqlParameter{ParameterName="@PrizeTypeId",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizeTypeId},
                new SqlParameter{ParameterName="@Point",SqlDbType=SqlDbType.Int,Value=entity.Point},
                new SqlParameter{ParameterName="@CouponTypeID",SqlDbType=SqlDbType.NVarChar,Value=entity.CouponTypeID},
                new SqlParameter{ParameterName="@CountTotal",SqlDbType=SqlDbType.Int,Value=entity.CountTotal},
                new SqlParameter{ParameterName="@Creator",SqlDbType=SqlDbType.NVarChar,Value=entity.CreateBy}
            };
            return this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_AddShare", parameters);
        }
        #endregion

        #region 追加分享设置奖品
        /// <summary>
        /// 追缴分享设置奖品的数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AppendSharePrize(LPrizesEntity entity)
        {

            SqlParameter[] parameters = new SqlParameter[4] 
            { 
                new SqlParameter{ParameterName="@EventId",SqlDbType=SqlDbType.NVarChar,Value=entity.EventId},
                new SqlParameter{ParameterName="@PrizesId",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizesID},
                new SqlParameter{ParameterName="@Qty",SqlDbType=SqlDbType.Int,Value=entity.CountTotal},
                new SqlParameter{ParameterName="@UpdateBy",SqlDbType=SqlDbType.NVarChar,Value=entity.CreateBy}
            };
            return this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[Proc_AppendShare]", parameters);
        }
        #endregion
        /// <summary>
        /// 是否已经设置了分享
        /// </summary>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        public int HasShare(string strEventId)
        {
            string strSql = "SELECT * FROM LEventsShare WHERE EventId ='" + strEventId + "' and IsDelete=0";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql));

        }
    }
}
