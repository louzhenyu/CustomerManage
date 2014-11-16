/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/2 16:56:07
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
    /// 表VipSnapshot的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipSnapshotDAO : Base.BaseCPOSDAO, ICRUDable<VipSnapshotEntity>, IQueryable<VipSnapshotEntity>
    {
        public void Create(VipEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [VipSnapshot](");
            strSql.Append("[VIPID],[VipName],[VipLevel],[VipCode],[WeiXin],[WeiXinUserId],[Gender],[Age],[Phone],[SinaMBlog],[TencentMBlog],[Birthday],[Qq],[Email],[Status],[VipSourceId],[Integration],[ClientID],[RecentlySalesTime],[RegistrationTime],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[APPID],[HigherVipID],[QRVipCode],[City],[CouponURL],[CouponInfo],[PurchaseAmount],[PurchaseCount],[DeliveryAddress],[Longitude],[Latitude],[VipPasswrod],[HeadImgUrl],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col14],[Col15],[Col16],[Col17],[Col18],[Col19],[Col20],[Col21],[Col22],[Col23],[Col24],[Col25],[Col26],[Col27],[Col28],[Col29],[Col30],[Col31],[Col32],[Col33],[Col34],[Col35],[Col36],[Col37],[Col38],[Col39],[Col40],[Col41],[Col42],[Col43],[Col44],[Col45],[Col46],[Col47],[Col48],[Col49],[Col50],[VipRealName],[isActivate],[VIPImportID],[ShareVipId],[SetoffUserId],[ShareUserId],[ShapShotID])");
            strSql.Append(" values (");
            strSql.Append("@VIPID,@VipName,@VipLevel,@VipCode,@WeiXin,@WeiXinUserId,@Gender,@Age,@Phone,@SinaMBlog,@TencentMBlog,@Birthday,@Qq,@Email,@Status,@VipSourceId,@Integration,@ClientID,@RecentlySalesTime,@RegistrationTime,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@APPID,@HigherVipID,@QRVipCode,@City,@CouponURL,@CouponInfo,@PurchaseAmount,@PurchaseCount,@DeliveryAddress,@Longitude,@Latitude,@VipPasswrod,@HeadImgUrl,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@Col11,@Col12,@Col13,@Col14,@Col15,@Col16,@Col17,@Col18,@Col19,@Col20,@Col21,@Col22,@Col23,@Col24,@Col25,@Col26,@Col27,@Col28,@Col29,@Col30,@Col31,@Col32,@Col33,@Col34,@Col35,@Col36,@Col37,@Col38,@Col39,@Col40,@Col41,@Col42,@Col43,@Col44,@Col45,@Col46,@Col47,@Col48,@Col49,@Col50,@VipRealName,@isActivate,@VIPImportID,@ShareVipId,@SetoffUserId,@ShareUserId,@ShapShotID)");

            Guid? pkGuid = Guid.NewGuid();

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VIPID",SqlDbType.NVarChar),
					new SqlParameter("@VipName",SqlDbType.NVarChar),
					new SqlParameter("@VipLevel",SqlDbType.Int),
					new SqlParameter("@VipCode",SqlDbType.NVarChar),
					new SqlParameter("@WeiXin",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinUserId",SqlDbType.NVarChar),
					new SqlParameter("@Gender",SqlDbType.Int),
					new SqlParameter("@Age",SqlDbType.Int),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@SinaMBlog",SqlDbType.NVarChar),
					new SqlParameter("@TencentMBlog",SqlDbType.NVarChar),
					new SqlParameter("@Birthday",SqlDbType.NVarChar),
					new SqlParameter("@Qq",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@VipSourceId",SqlDbType.NVarChar),
					new SqlParameter("@Integration",SqlDbType.Decimal),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@RecentlySalesTime",SqlDbType.DateTime),
					new SqlParameter("@RegistrationTime",SqlDbType.DateTime),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@APPID",SqlDbType.NVarChar),
					new SqlParameter("@HigherVipID",SqlDbType.NVarChar),
					new SqlParameter("@QRVipCode",SqlDbType.NVarChar),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@CouponURL",SqlDbType.NVarChar),
					new SqlParameter("@CouponInfo",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseAmount",SqlDbType.Decimal),
					new SqlParameter("@PurchaseCount",SqlDbType.Int),
					new SqlParameter("@DeliveryAddress",SqlDbType.NVarChar),
					new SqlParameter("@Longitude",SqlDbType.NVarChar),
					new SqlParameter("@Latitude",SqlDbType.NVarChar),
					new SqlParameter("@VipPasswrod",SqlDbType.NVarChar),
					new SqlParameter("@HeadImgUrl",SqlDbType.NVarChar),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@Col11",SqlDbType.NVarChar),
					new SqlParameter("@Col12",SqlDbType.NVarChar),
					new SqlParameter("@Col13",SqlDbType.NVarChar),
					new SqlParameter("@Col14",SqlDbType.NVarChar),
					new SqlParameter("@Col15",SqlDbType.NVarChar),
					new SqlParameter("@Col16",SqlDbType.NVarChar),
					new SqlParameter("@Col17",SqlDbType.NVarChar),
					new SqlParameter("@Col18",SqlDbType.NVarChar),
					new SqlParameter("@Col19",SqlDbType.NVarChar),
					new SqlParameter("@Col20",SqlDbType.NVarChar),
					new SqlParameter("@Col21",SqlDbType.NVarChar),
					new SqlParameter("@Col22",SqlDbType.NVarChar),
					new SqlParameter("@Col23",SqlDbType.NVarChar),
					new SqlParameter("@Col24",SqlDbType.NVarChar),
					new SqlParameter("@Col25",SqlDbType.NVarChar),
					new SqlParameter("@Col26",SqlDbType.NVarChar),
					new SqlParameter("@Col27",SqlDbType.NVarChar),
					new SqlParameter("@Col28",SqlDbType.NVarChar),
					new SqlParameter("@Col29",SqlDbType.NVarChar),
					new SqlParameter("@Col30",SqlDbType.NVarChar),
					new SqlParameter("@Col31",SqlDbType.NVarChar),
					new SqlParameter("@Col32",SqlDbType.NVarChar),
					new SqlParameter("@Col33",SqlDbType.NVarChar),
					new SqlParameter("@Col34",SqlDbType.NVarChar),
					new SqlParameter("@Col35",SqlDbType.NVarChar),
					new SqlParameter("@Col36",SqlDbType.NVarChar),
					new SqlParameter("@Col37",SqlDbType.NVarChar),
					new SqlParameter("@Col38",SqlDbType.NVarChar),
					new SqlParameter("@Col39",SqlDbType.NVarChar),
					new SqlParameter("@Col40",SqlDbType.NVarChar),
					new SqlParameter("@Col41",SqlDbType.NVarChar),
					new SqlParameter("@Col42",SqlDbType.NVarChar),
					new SqlParameter("@Col43",SqlDbType.NVarChar),
					new SqlParameter("@Col44",SqlDbType.NVarChar),
					new SqlParameter("@Col45",SqlDbType.NVarChar),
					new SqlParameter("@Col46",SqlDbType.NVarChar),
					new SqlParameter("@Col47",SqlDbType.NVarChar),
					new SqlParameter("@Col48",SqlDbType.NVarChar),
					new SqlParameter("@Col49",SqlDbType.NVarChar),
					new SqlParameter("@Col50",SqlDbType.NVarChar),
					new SqlParameter("@VipRealName",SqlDbType.NVarChar),
					new SqlParameter("@isActivate",SqlDbType.Int),
					new SqlParameter("@VIPImportID",SqlDbType.NVarChar),
					new SqlParameter("@ShareVipId",SqlDbType.NVarChar),
					new SqlParameter("@SetoffUserId",SqlDbType.NVarChar),
					new SqlParameter("@ShareUserId",SqlDbType.NVarChar),
					new SqlParameter("@ShapShotID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.VIPID;
            parameters[1].Value = pEntity.VipName;
            parameters[2].Value = pEntity.VipLevel;
            parameters[3].Value = pEntity.VipCode;
            parameters[4].Value = pEntity.WeiXin;
            parameters[5].Value = pEntity.WeiXinUserId;
            parameters[6].Value = pEntity.Gender;
            parameters[7].Value = pEntity.Age;
            parameters[8].Value = pEntity.Phone;
            parameters[9].Value = pEntity.SinaMBlog;
            parameters[10].Value = pEntity.TencentMBlog;
            parameters[11].Value = pEntity.Birthday;
            parameters[12].Value = pEntity.Qq;
            parameters[13].Value = pEntity.Email;
            parameters[14].Value = pEntity.Status;
            parameters[15].Value = pEntity.VipSourceId;
            parameters[16].Value = pEntity.Integration;
            parameters[17].Value = pEntity.ClientID;
            parameters[18].Value = pEntity.RecentlySalesTime;
            parameters[19].Value = pEntity.RegistrationTime;
            parameters[20].Value = pEntity.CreateTime;
            parameters[21].Value = pEntity.CreateBy;
            parameters[22].Value = pEntity.LastUpdateTime;
            parameters[23].Value = pEntity.LastUpdateBy;
            parameters[24].Value = pEntity.IsDelete;
            parameters[25].Value = pEntity.APPID;
            parameters[26].Value = pEntity.HigherVipID;
            parameters[27].Value = pEntity.QRVipCode;
            parameters[28].Value = pEntity.City;
            parameters[29].Value = pEntity.CouponURL;
            parameters[30].Value = pEntity.CouponInfo;
            parameters[31].Value = pEntity.PurchaseAmount;
            parameters[32].Value = pEntity.PurchaseCount;
            parameters[33].Value = pEntity.DeliveryAddress;
            parameters[34].Value = pEntity.Longitude;
            parameters[35].Value = pEntity.Latitude;
            parameters[36].Value = pEntity.VipPasswrod;
            parameters[37].Value = pEntity.HeadImgUrl;
            parameters[38].Value = pEntity.Col1;
            parameters[39].Value = pEntity.Col2;
            parameters[40].Value = pEntity.Col3;
            parameters[41].Value = pEntity.Col4;
            parameters[42].Value = pEntity.Col5;
            parameters[43].Value = pEntity.Col6;
            parameters[44].Value = pEntity.Col7;
            parameters[45].Value = pEntity.Col8;
            parameters[46].Value = pEntity.Col9;
            parameters[47].Value = pEntity.Col10;
            parameters[48].Value = pEntity.Col11;
            parameters[49].Value = pEntity.Col12;
            parameters[50].Value = pEntity.Col13;
            parameters[51].Value = pEntity.Col14;
            parameters[52].Value = pEntity.Col15;
            parameters[53].Value = pEntity.Col16;
            parameters[54].Value = pEntity.Col17;
            parameters[55].Value = pEntity.Col18;
            parameters[56].Value = pEntity.Col19;
            parameters[57].Value = pEntity.Col20;
            parameters[58].Value = pEntity.Col21;
            parameters[59].Value = pEntity.Col22;
            parameters[60].Value = pEntity.Col23;
            parameters[61].Value = pEntity.Col24;
            parameters[62].Value = pEntity.Col25;
            parameters[63].Value = pEntity.Col26;
            parameters[64].Value = pEntity.Col27;
            parameters[65].Value = pEntity.Col28;
            parameters[66].Value = pEntity.Col29;
            parameters[67].Value = pEntity.Col30;
            parameters[68].Value = pEntity.Col31;
            parameters[69].Value = pEntity.Col32;
            parameters[70].Value = pEntity.Col33;
            parameters[71].Value = pEntity.Col34;
            parameters[72].Value = pEntity.Col35;
            parameters[73].Value = pEntity.Col36;
            parameters[74].Value = pEntity.Col37;
            parameters[75].Value = pEntity.Col38;
            parameters[76].Value = pEntity.Col39;
            parameters[77].Value = pEntity.Col40;
            parameters[78].Value = pEntity.Col41;
            parameters[79].Value = pEntity.Col42;
            parameters[80].Value = pEntity.Col43;
            parameters[81].Value = pEntity.Col44;
            parameters[82].Value = pEntity.Col45;
            parameters[83].Value = pEntity.Col46;
            parameters[84].Value = pEntity.Col47;
            parameters[85].Value = pEntity.Col48;
            parameters[86].Value = pEntity.Col49;
            parameters[87].Value = pEntity.Col50;
            parameters[88].Value = pEntity.VipRealName;
            parameters[89].Value = pEntity.IsActivate;
            parameters[90].Value = pEntity.VIPImportID;
            parameters[91].Value = pEntity.ShareVipId;
            parameters[92].Value = pEntity.SetoffUserId;
            parameters[93].Value = pEntity.ShareUserId;
            parameters[94].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
