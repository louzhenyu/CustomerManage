/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/5 15:57:10
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
    /// 表Vip的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipDAO : Base.BaseCPOSDAO, ICRUDable<VipEntity>, IQueryable<VipEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [Vip](");
            strSql.Append("[VipName],[VipLevel],[VipCode],[WeiXin],[WeiXinUserId],[Gender],[Age],[Phone],[SinaMBlog],[TencentMBlog],[Birthday],[Qq],[Email],[Status],[VipSourceId],[Integration],[ClientID],[RecentlySalesTime],[RegistrationTime],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[APPID],[HigherVipID],[QRVipCode],[City],[CouponURL],[CouponInfo],[PurchaseAmount],[PurchaseCount],[DeliveryAddress],[Longitude],[Latitude],[VipPasswrod],[HeadImgUrl],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col14],[Col15],[Col16],[Col17],[Col18],[Col19],[Col20],[Col21],[Col22],[Col23],[Col24],[Col25],[Col26],[Col27],[Col28],[Col29],[Col30],[Col31],[Col32],[Col33],[Col34],[Col35],[Col36],[Col37],[Col38],[Col39],[Col40],[Col41],[Col42],[Col43],[Col44],[Col45],[Col46],[Col47],[Col48],[Col49],[Col50],[VipRealName],[isActivate],[VIPImportID],[ShareVipId],[SetoffUserId],[ShareUserId],[VIPID])");
            strSql.Append(" values (");
            strSql.Append("@VipName,@VipLevel,@VipCode,@WeiXin,@WeiXinUserId,@Gender,@Age,@Phone,@SinaMBlog,@TencentMBlog,@Birthday,@Qq,@Email,@Status,@VipSourceId,@Integration,@ClientID,@RecentlySalesTime,@RegistrationTime,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@APPID,@HigherVipID,@QRVipCode,@City,@CouponURL,@CouponInfo,@PurchaseAmount,@PurchaseCount,@DeliveryAddress,@Longitude,@Latitude,@VipPasswrod,@HeadImgUrl,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@Col11,@Col12,@Col13,@Col14,@Col15,@Col16,@Col17,@Col18,@Col19,@Col20,@Col21,@Col22,@Col23,@Col24,@Col25,@Col26,@Col27,@Col28,@Col29,@Col30,@Col31,@Col32,@Col33,@Col34,@Col35,@Col36,@Col37,@Col38,@Col39,@Col40,@Col41,@Col42,@Col43,@Col44,@Col45,@Col46,@Col47,@Col48,@Col49,@Col50,@VipRealName,@IsActivate,@VIPImportID,@ShareVipId,@SetoffUserId,@ShareUserId,@VIPID)");            

			string pkString = pEntity.VIPID;

            SqlParameter[] parameters = 
            {
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
					new SqlParameter("@IsActivate",SqlDbType.Int),
					new SqlParameter("@VIPImportID",SqlDbType.NVarChar),
					new SqlParameter("@ShareVipId",SqlDbType.NVarChar),
					new SqlParameter("@SetoffUserId",SqlDbType.NVarChar),
					new SqlParameter("@ShareUserId",SqlDbType.NVarChar),
					new SqlParameter("@VIPID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipName;
			parameters[1].Value = pEntity.VipLevel;
			parameters[2].Value = pEntity.VipCode;
			parameters[3].Value = pEntity.WeiXin;
			parameters[4].Value = pEntity.WeiXinUserId;
			parameters[5].Value = pEntity.Gender;
			parameters[6].Value = pEntity.Age;
			parameters[7].Value = pEntity.Phone;
			parameters[8].Value = pEntity.SinaMBlog;
			parameters[9].Value = pEntity.TencentMBlog;
			parameters[10].Value = pEntity.Birthday;
			parameters[11].Value = pEntity.Qq;
			parameters[12].Value = pEntity.Email;
			parameters[13].Value = pEntity.Status;
			parameters[14].Value = pEntity.VipSourceId;
			parameters[15].Value = pEntity.Integration;
			parameters[16].Value = pEntity.ClientID;
			parameters[17].Value = pEntity.RecentlySalesTime;
			parameters[18].Value = pEntity.RegistrationTime;
			parameters[19].Value = pEntity.CreateTime;
			parameters[20].Value = pEntity.CreateBy;
			parameters[21].Value = pEntity.LastUpdateTime;
			parameters[22].Value = pEntity.LastUpdateBy;
			parameters[23].Value = pEntity.IsDelete;
			parameters[24].Value = pEntity.APPID;
			parameters[25].Value = pEntity.HigherVipID;
			parameters[26].Value = pEntity.QRVipCode;
			parameters[27].Value = pEntity.City;
			parameters[28].Value = pEntity.CouponURL;
			parameters[29].Value = pEntity.CouponInfo;
			parameters[30].Value = pEntity.PurchaseAmount;
			parameters[31].Value = pEntity.PurchaseCount;
			parameters[32].Value = pEntity.DeliveryAddress;
			parameters[33].Value = pEntity.Longitude;
			parameters[34].Value = pEntity.Latitude;
			parameters[35].Value = pEntity.VipPasswrod;
			parameters[36].Value = pEntity.HeadImgUrl;
			parameters[37].Value = pEntity.Col1;
			parameters[38].Value = pEntity.Col2;
			parameters[39].Value = pEntity.Col3;
			parameters[40].Value = pEntity.Col4;
			parameters[41].Value = pEntity.Col5;
			parameters[42].Value = pEntity.Col6;
			parameters[43].Value = pEntity.Col7;
			parameters[44].Value = pEntity.Col8;
			parameters[45].Value = pEntity.Col9;
			parameters[46].Value = pEntity.Col10;
			parameters[47].Value = pEntity.Col11;
			parameters[48].Value = pEntity.Col12;
			parameters[49].Value = pEntity.Col13;
			parameters[50].Value = pEntity.Col14;
			parameters[51].Value = pEntity.Col15;
			parameters[52].Value = pEntity.Col16;
			parameters[53].Value = pEntity.Col17;
			parameters[54].Value = pEntity.Col18;
			parameters[55].Value = pEntity.Col19;
			parameters[56].Value = pEntity.Col20;
			parameters[57].Value = pEntity.Col21;
			parameters[58].Value = pEntity.Col22;
			parameters[59].Value = pEntity.Col23;
			parameters[60].Value = pEntity.Col24;
			parameters[61].Value = pEntity.Col25;
			parameters[62].Value = pEntity.Col26;
			parameters[63].Value = pEntity.Col27;
			parameters[64].Value = pEntity.Col28;
			parameters[65].Value = pEntity.Col29;
			parameters[66].Value = pEntity.Col30;
			parameters[67].Value = pEntity.Col31;
			parameters[68].Value = pEntity.Col32;
			parameters[69].Value = pEntity.Col33;
			parameters[70].Value = pEntity.Col34;
			parameters[71].Value = pEntity.Col35;
			parameters[72].Value = pEntity.Col36;
			parameters[73].Value = pEntity.Col37;
			parameters[74].Value = pEntity.Col38;
			parameters[75].Value = pEntity.Col39;
			parameters[76].Value = pEntity.Col40;
			parameters[77].Value = pEntity.Col41;
			parameters[78].Value = pEntity.Col42;
			parameters[79].Value = pEntity.Col43;
			parameters[80].Value = pEntity.Col44;
			parameters[81].Value = pEntity.Col45;
			parameters[82].Value = pEntity.Col46;
			parameters[83].Value = pEntity.Col47;
			parameters[84].Value = pEntity.Col48;
			parameters[85].Value = pEntity.Col49;
			parameters[86].Value = pEntity.Col50;
			parameters[87].Value = pEntity.VipRealName;
			parameters[88].Value = pEntity.IsActivate;
			parameters[89].Value = pEntity.VIPImportID;
			parameters[90].Value = pEntity.ShareVipId;
			parameters[91].Value = pEntity.SetoffUserId;
			parameters[92].Value = pEntity.ShareUserId;
			parameters[93].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VIPID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Vip] where VIPID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            VipEntity m = null;
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
        public VipEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Vip] where isdelete=0");
            //读取数据
            List<VipEntity> list = new List<VipEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipEntity m;
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
        public void Update(VipEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VipEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VIPID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Vip] set ");
            if (pIsUpdateNullField || pEntity.VipName!=null)
                strSql.Append( "[VipName]=@VipName,");
            if (pIsUpdateNullField || pEntity.VipLevel!=null)
                strSql.Append( "[VipLevel]=@VipLevel,");
            if (pIsUpdateNullField || pEntity.VipCode!=null)
                strSql.Append( "[VipCode]=@VipCode,");
            if (pIsUpdateNullField || pEntity.WeiXin!=null)
                strSql.Append( "[WeiXin]=@WeiXin,");
            if (pIsUpdateNullField || pEntity.WeiXinUserId!=null)
                strSql.Append( "[WeiXinUserId]=@WeiXinUserId,");
            if (pIsUpdateNullField || pEntity.Gender!=null)
                strSql.Append( "[Gender]=@Gender,");
            if (pIsUpdateNullField || pEntity.Age!=null)
                strSql.Append( "[Age]=@Age,");
            if (pIsUpdateNullField || pEntity.Phone!=null)
                strSql.Append( "[Phone]=@Phone,");
            if (pIsUpdateNullField || pEntity.SinaMBlog!=null)
                strSql.Append( "[SinaMBlog]=@SinaMBlog,");
            if (pIsUpdateNullField || pEntity.TencentMBlog!=null)
                strSql.Append( "[TencentMBlog]=@TencentMBlog,");
            if (pIsUpdateNullField || pEntity.Birthday!=null)
                strSql.Append( "[Birthday]=@Birthday,");
            if (pIsUpdateNullField || pEntity.Qq!=null)
                strSql.Append( "[Qq]=@Qq,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.VipSourceId!=null)
                strSql.Append( "[VipSourceId]=@VipSourceId,");
            if (pIsUpdateNullField || pEntity.Integration!=null)
                strSql.Append( "[Integration]=@Integration,");
            if (pIsUpdateNullField || pEntity.ClientID!=null)
                strSql.Append( "[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.RecentlySalesTime!=null)
                strSql.Append( "[RecentlySalesTime]=@RecentlySalesTime,");
            if (pIsUpdateNullField || pEntity.RegistrationTime!=null)
                strSql.Append( "[RegistrationTime]=@RegistrationTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.APPID!=null)
                strSql.Append( "[APPID]=@APPID,");
            if (pIsUpdateNullField || pEntity.HigherVipID!=null)
                strSql.Append( "[HigherVipID]=@HigherVipID,");
            if (pIsUpdateNullField || pEntity.QRVipCode!=null)
                strSql.Append( "[QRVipCode]=@QRVipCode,");
            if (pIsUpdateNullField || pEntity.City!=null)
                strSql.Append( "[City]=@City,");
            if (pIsUpdateNullField || pEntity.CouponURL!=null)
                strSql.Append( "[CouponURL]=@CouponURL,");
            if (pIsUpdateNullField || pEntity.CouponInfo!=null)
                strSql.Append( "[CouponInfo]=@CouponInfo,");
            if (pIsUpdateNullField || pEntity.PurchaseAmount!=null)
                strSql.Append( "[PurchaseAmount]=@PurchaseAmount,");
            if (pIsUpdateNullField || pEntity.PurchaseCount!=null)
                strSql.Append( "[PurchaseCount]=@PurchaseCount,");
            if (pIsUpdateNullField || pEntity.DeliveryAddress!=null)
                strSql.Append( "[DeliveryAddress]=@DeliveryAddress,");
            if (pIsUpdateNullField || pEntity.Longitude!=null)
                strSql.Append( "[Longitude]=@Longitude,");
            if (pIsUpdateNullField || pEntity.Latitude!=null)
                strSql.Append( "[Latitude]=@Latitude,");
            if (pIsUpdateNullField || pEntity.VipPasswrod!=null)
                strSql.Append( "[VipPasswrod]=@VipPasswrod,");
            if (pIsUpdateNullField || pEntity.HeadImgUrl!=null)
                strSql.Append( "[HeadImgUrl]=@HeadImgUrl,");
            if (pIsUpdateNullField || pEntity.Col1!=null)
                strSql.Append( "[Col1]=@Col1,");
            if (pIsUpdateNullField || pEntity.Col2!=null)
                strSql.Append( "[Col2]=@Col2,");
            if (pIsUpdateNullField || pEntity.Col3!=null)
                strSql.Append( "[Col3]=@Col3,");
            if (pIsUpdateNullField || pEntity.Col4!=null)
                strSql.Append( "[Col4]=@Col4,");
            if (pIsUpdateNullField || pEntity.Col5!=null)
                strSql.Append( "[Col5]=@Col5,");
            if (pIsUpdateNullField || pEntity.Col6!=null)
                strSql.Append( "[Col6]=@Col6,");
            if (pIsUpdateNullField || pEntity.Col7!=null)
                strSql.Append( "[Col7]=@Col7,");
            if (pIsUpdateNullField || pEntity.Col8!=null)
                strSql.Append( "[Col8]=@Col8,");
            if (pIsUpdateNullField || pEntity.Col9!=null)
                strSql.Append( "[Col9]=@Col9,");
            if (pIsUpdateNullField || pEntity.Col10!=null)
                strSql.Append( "[Col10]=@Col10,");
            if (pIsUpdateNullField || pEntity.Col11!=null)
                strSql.Append( "[Col11]=@Col11,");
            if (pIsUpdateNullField || pEntity.Col12!=null)
                strSql.Append( "[Col12]=@Col12,");
            if (pIsUpdateNullField || pEntity.Col13!=null)
                strSql.Append( "[Col13]=@Col13,");
            if (pIsUpdateNullField || pEntity.Col14!=null)
                strSql.Append( "[Col14]=@Col14,");
            if (pIsUpdateNullField || pEntity.Col15!=null)
                strSql.Append( "[Col15]=@Col15,");
            if (pIsUpdateNullField || pEntity.Col16!=null)
                strSql.Append( "[Col16]=@Col16,");
            if (pIsUpdateNullField || pEntity.Col17!=null)
                strSql.Append( "[Col17]=@Col17,");
            if (pIsUpdateNullField || pEntity.Col18!=null)
                strSql.Append( "[Col18]=@Col18,");
            if (pIsUpdateNullField || pEntity.Col19!=null)
                strSql.Append( "[Col19]=@Col19,");
            if (pIsUpdateNullField || pEntity.Col20!=null)
                strSql.Append( "[Col20]=@Col20,");
            if (pIsUpdateNullField || pEntity.Col21!=null)
                strSql.Append( "[Col21]=@Col21,");
            if (pIsUpdateNullField || pEntity.Col22!=null)
                strSql.Append( "[Col22]=@Col22,");
            if (pIsUpdateNullField || pEntity.Col23!=null)
                strSql.Append( "[Col23]=@Col23,");
            if (pIsUpdateNullField || pEntity.Col24!=null)
                strSql.Append( "[Col24]=@Col24,");
            if (pIsUpdateNullField || pEntity.Col25!=null)
                strSql.Append( "[Col25]=@Col25,");
            if (pIsUpdateNullField || pEntity.Col26!=null)
                strSql.Append( "[Col26]=@Col26,");
            if (pIsUpdateNullField || pEntity.Col27!=null)
                strSql.Append( "[Col27]=@Col27,");
            if (pIsUpdateNullField || pEntity.Col28!=null)
                strSql.Append( "[Col28]=@Col28,");
            if (pIsUpdateNullField || pEntity.Col29!=null)
                strSql.Append( "[Col29]=@Col29,");
            if (pIsUpdateNullField || pEntity.Col30!=null)
                strSql.Append( "[Col30]=@Col30,");
            if (pIsUpdateNullField || pEntity.Col31!=null)
                strSql.Append( "[Col31]=@Col31,");
            if (pIsUpdateNullField || pEntity.Col32!=null)
                strSql.Append( "[Col32]=@Col32,");
            if (pIsUpdateNullField || pEntity.Col33!=null)
                strSql.Append( "[Col33]=@Col33,");
            if (pIsUpdateNullField || pEntity.Col34!=null)
                strSql.Append( "[Col34]=@Col34,");
            if (pIsUpdateNullField || pEntity.Col35!=null)
                strSql.Append( "[Col35]=@Col35,");
            if (pIsUpdateNullField || pEntity.Col36!=null)
                strSql.Append( "[Col36]=@Col36,");
            if (pIsUpdateNullField || pEntity.Col37!=null)
                strSql.Append( "[Col37]=@Col37,");
            if (pIsUpdateNullField || pEntity.Col38!=null)
                strSql.Append( "[Col38]=@Col38,");
            if (pIsUpdateNullField || pEntity.Col39!=null)
                strSql.Append( "[Col39]=@Col39,");
            if (pIsUpdateNullField || pEntity.Col40!=null)
                strSql.Append( "[Col40]=@Col40,");
            if (pIsUpdateNullField || pEntity.Col41!=null)
                strSql.Append( "[Col41]=@Col41,");
            if (pIsUpdateNullField || pEntity.Col42!=null)
                strSql.Append( "[Col42]=@Col42,");
            if (pIsUpdateNullField || pEntity.Col43!=null)
                strSql.Append( "[Col43]=@Col43,");
            if (pIsUpdateNullField || pEntity.Col44!=null)
                strSql.Append( "[Col44]=@Col44,");
            if (pIsUpdateNullField || pEntity.Col45!=null)
                strSql.Append( "[Col45]=@Col45,");
            if (pIsUpdateNullField || pEntity.Col46!=null)
                strSql.Append( "[Col46]=@Col46,");
            if (pIsUpdateNullField || pEntity.Col47!=null)
                strSql.Append( "[Col47]=@Col47,");
            if (pIsUpdateNullField || pEntity.Col48!=null)
                strSql.Append( "[Col48]=@Col48,");
            if (pIsUpdateNullField || pEntity.Col49!=null)
                strSql.Append( "[Col49]=@Col49,");
            if (pIsUpdateNullField || pEntity.Col50!=null)
                strSql.Append( "[Col50]=@Col50,");
            if (pIsUpdateNullField || pEntity.VipRealName!=null)
                strSql.Append( "[VipRealName]=@VipRealName,");
            if (pIsUpdateNullField || pEntity.IsActivate!=null)
                strSql.Append( "[isActivate]=@IsActivate,");
            if (pIsUpdateNullField || pEntity.VIPImportID!=null)
                strSql.Append( "[VIPImportID]=@VIPImportID,");
            if (pIsUpdateNullField || pEntity.ShareVipId!=null)
                strSql.Append( "[ShareVipId]=@ShareVipId,");
            if (pIsUpdateNullField || pEntity.SetoffUserId!=null)
                strSql.Append( "[SetoffUserId]=@SetoffUserId,");
            if (pIsUpdateNullField || pEntity.ShareUserId!=null)
                strSql.Append( "[ShareUserId]=@ShareUserId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where VIPID=@VIPID ");
            SqlParameter[] parameters = 
            {
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
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
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
					new SqlParameter("@IsActivate",SqlDbType.Int),
					new SqlParameter("@VIPImportID",SqlDbType.NVarChar),
					new SqlParameter("@ShareVipId",SqlDbType.NVarChar),
					new SqlParameter("@SetoffUserId",SqlDbType.NVarChar),
					new SqlParameter("@ShareUserId",SqlDbType.NVarChar),
					new SqlParameter("@VIPID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipName;
			parameters[1].Value = pEntity.VipLevel;
			parameters[2].Value = pEntity.VipCode;
			parameters[3].Value = pEntity.WeiXin;
			parameters[4].Value = pEntity.WeiXinUserId;
			parameters[5].Value = pEntity.Gender;
			parameters[6].Value = pEntity.Age;
			parameters[7].Value = pEntity.Phone;
			parameters[8].Value = pEntity.SinaMBlog;
			parameters[9].Value = pEntity.TencentMBlog;
			parameters[10].Value = pEntity.Birthday;
			parameters[11].Value = pEntity.Qq;
			parameters[12].Value = pEntity.Email;
			parameters[13].Value = pEntity.Status;
			parameters[14].Value = pEntity.VipSourceId;
			parameters[15].Value = pEntity.Integration;
			parameters[16].Value = pEntity.ClientID;
			parameters[17].Value = pEntity.RecentlySalesTime;
			parameters[18].Value = pEntity.RegistrationTime;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.LastUpdateBy;
			parameters[21].Value = pEntity.APPID;
			parameters[22].Value = pEntity.HigherVipID;
			parameters[23].Value = pEntity.QRVipCode;
			parameters[24].Value = pEntity.City;
			parameters[25].Value = pEntity.CouponURL;
			parameters[26].Value = pEntity.CouponInfo;
			parameters[27].Value = pEntity.PurchaseAmount;
			parameters[28].Value = pEntity.PurchaseCount;
			parameters[29].Value = pEntity.DeliveryAddress;
			parameters[30].Value = pEntity.Longitude;
			parameters[31].Value = pEntity.Latitude;
			parameters[32].Value = pEntity.VipPasswrod;
			parameters[33].Value = pEntity.HeadImgUrl;
			parameters[34].Value = pEntity.Col1;
			parameters[35].Value = pEntity.Col2;
			parameters[36].Value = pEntity.Col3;
			parameters[37].Value = pEntity.Col4;
			parameters[38].Value = pEntity.Col5;
			parameters[39].Value = pEntity.Col6;
			parameters[40].Value = pEntity.Col7;
			parameters[41].Value = pEntity.Col8;
			parameters[42].Value = pEntity.Col9;
			parameters[43].Value = pEntity.Col10;
			parameters[44].Value = pEntity.Col11;
			parameters[45].Value = pEntity.Col12;
			parameters[46].Value = pEntity.Col13;
			parameters[47].Value = pEntity.Col14;
			parameters[48].Value = pEntity.Col15;
			parameters[49].Value = pEntity.Col16;
			parameters[50].Value = pEntity.Col17;
			parameters[51].Value = pEntity.Col18;
			parameters[52].Value = pEntity.Col19;
			parameters[53].Value = pEntity.Col20;
			parameters[54].Value = pEntity.Col21;
			parameters[55].Value = pEntity.Col22;
			parameters[56].Value = pEntity.Col23;
			parameters[57].Value = pEntity.Col24;
			parameters[58].Value = pEntity.Col25;
			parameters[59].Value = pEntity.Col26;
			parameters[60].Value = pEntity.Col27;
			parameters[61].Value = pEntity.Col28;
			parameters[62].Value = pEntity.Col29;
			parameters[63].Value = pEntity.Col30;
			parameters[64].Value = pEntity.Col31;
			parameters[65].Value = pEntity.Col32;
			parameters[66].Value = pEntity.Col33;
			parameters[67].Value = pEntity.Col34;
			parameters[68].Value = pEntity.Col35;
			parameters[69].Value = pEntity.Col36;
			parameters[70].Value = pEntity.Col37;
			parameters[71].Value = pEntity.Col38;
			parameters[72].Value = pEntity.Col39;
			parameters[73].Value = pEntity.Col40;
			parameters[74].Value = pEntity.Col41;
			parameters[75].Value = pEntity.Col42;
			parameters[76].Value = pEntity.Col43;
			parameters[77].Value = pEntity.Col44;
			parameters[78].Value = pEntity.Col45;
			parameters[79].Value = pEntity.Col46;
			parameters[80].Value = pEntity.Col47;
			parameters[81].Value = pEntity.Col48;
			parameters[82].Value = pEntity.Col49;
			parameters[83].Value = pEntity.Col50;
			parameters[84].Value = pEntity.VipRealName;
			parameters[85].Value = pEntity.IsActivate;
			parameters[86].Value = pEntity.VIPImportID;
			parameters[87].Value = pEntity.ShareVipId;
			parameters[88].Value = pEntity.SetoffUserId;
			parameters[89].Value = pEntity.ShareUserId;
			parameters[90].Value = pEntity.VIPID;

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
        public void Update(VipEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VipEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VIPID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VIPID, pTran);           
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
            sql.AppendLine("update [Vip] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VIPID=@VIPID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@VIPID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.VIPID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.VIPID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipEntity[] pEntities)
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
            sql.AppendLine("update [Vip] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where VIPID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Vip] where isdelete=0 ");
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
            List<VipEntity> list = new List<VipEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipEntity m;
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
        public PagedQueryResult<VipEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VIPID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [Vip] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [Vip] where isdelete=0 ");
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
            PagedQueryResult<VipEntity> result = new PagedQueryResult<VipEntity>();
            List<VipEntity> list = new List<VipEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipEntity m;
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
        public VipEntity[] QueryByEntity(VipEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipEntity> PagedQueryByEntity(VipEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VIPID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VIPID", Value = pQueryEntity.VIPID });
            if (pQueryEntity.VipName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipName", Value = pQueryEntity.VipName });
            if (pQueryEntity.VipLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipLevel", Value = pQueryEntity.VipLevel });
            if (pQueryEntity.VipCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCode", Value = pQueryEntity.VipCode });
            if (pQueryEntity.WeiXin!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXin", Value = pQueryEntity.WeiXin });
            if (pQueryEntity.WeiXinUserId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinUserId", Value = pQueryEntity.WeiXinUserId });
            if (pQueryEntity.Gender!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Gender", Value = pQueryEntity.Gender });
            if (pQueryEntity.Age!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Age", Value = pQueryEntity.Age });
            if (pQueryEntity.Phone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Phone", Value = pQueryEntity.Phone });
            if (pQueryEntity.SinaMBlog!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SinaMBlog", Value = pQueryEntity.SinaMBlog });
            if (pQueryEntity.TencentMBlog!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TencentMBlog", Value = pQueryEntity.TencentMBlog });
            if (pQueryEntity.Birthday!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Birthday", Value = pQueryEntity.Birthday });
            if (pQueryEntity.Qq!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qq", Value = pQueryEntity.Qq });
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.VipSourceId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipSourceId", Value = pQueryEntity.VipSourceId });
            if (pQueryEntity.Integration!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Integration", Value = pQueryEntity.Integration });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.RecentlySalesTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RecentlySalesTime", Value = pQueryEntity.RecentlySalesTime });
            if (pQueryEntity.RegistrationTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RegistrationTime", Value = pQueryEntity.RegistrationTime });
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
            if (pQueryEntity.APPID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "APPID", Value = pQueryEntity.APPID });
            if (pQueryEntity.HigherVipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HigherVipID", Value = pQueryEntity.HigherVipID });
            if (pQueryEntity.QRVipCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QRVipCode", Value = pQueryEntity.QRVipCode });
            if (pQueryEntity.City!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "City", Value = pQueryEntity.City });
            if (pQueryEntity.CouponURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponURL", Value = pQueryEntity.CouponURL });
            if (pQueryEntity.CouponInfo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponInfo", Value = pQueryEntity.CouponInfo });
            if (pQueryEntity.PurchaseAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseAmount", Value = pQueryEntity.PurchaseAmount });
            if (pQueryEntity.PurchaseCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseCount", Value = pQueryEntity.PurchaseCount });
            if (pQueryEntity.DeliveryAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryAddress", Value = pQueryEntity.DeliveryAddress });
            if (pQueryEntity.Longitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Longitude", Value = pQueryEntity.Longitude });
            if (pQueryEntity.Latitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Latitude", Value = pQueryEntity.Latitude });
            if (pQueryEntity.VipPasswrod!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipPasswrod", Value = pQueryEntity.VipPasswrod });
            if (pQueryEntity.HeadImgUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HeadImgUrl", Value = pQueryEntity.HeadImgUrl });
            if (pQueryEntity.Col1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col1", Value = pQueryEntity.Col1 });
            if (pQueryEntity.Col2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col2", Value = pQueryEntity.Col2 });
            if (pQueryEntity.Col3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col3", Value = pQueryEntity.Col3 });
            if (pQueryEntity.Col4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col4", Value = pQueryEntity.Col4 });
            if (pQueryEntity.Col5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col5", Value = pQueryEntity.Col5 });
            if (pQueryEntity.Col6!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col6", Value = pQueryEntity.Col6 });
            if (pQueryEntity.Col7!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col7", Value = pQueryEntity.Col7 });
            if (pQueryEntity.Col8!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col8", Value = pQueryEntity.Col8 });
            if (pQueryEntity.Col9!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col9", Value = pQueryEntity.Col9 });
            if (pQueryEntity.Col10!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col10", Value = pQueryEntity.Col10 });
            if (pQueryEntity.Col11!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col11", Value = pQueryEntity.Col11 });
            if (pQueryEntity.Col12!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col12", Value = pQueryEntity.Col12 });
            if (pQueryEntity.Col13!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col13", Value = pQueryEntity.Col13 });
            if (pQueryEntity.Col14!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col14", Value = pQueryEntity.Col14 });
            if (pQueryEntity.Col15!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col15", Value = pQueryEntity.Col15 });
            if (pQueryEntity.Col16!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col16", Value = pQueryEntity.Col16 });
            if (pQueryEntity.Col17!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col17", Value = pQueryEntity.Col17 });
            if (pQueryEntity.Col18!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col18", Value = pQueryEntity.Col18 });
            if (pQueryEntity.Col19!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col19", Value = pQueryEntity.Col19 });
            if (pQueryEntity.Col20!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col20", Value = pQueryEntity.Col20 });
            if (pQueryEntity.Col21!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col21", Value = pQueryEntity.Col21 });
            if (pQueryEntity.Col22!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col22", Value = pQueryEntity.Col22 });
            if (pQueryEntity.Col23!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col23", Value = pQueryEntity.Col23 });
            if (pQueryEntity.Col24!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col24", Value = pQueryEntity.Col24 });
            if (pQueryEntity.Col25!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col25", Value = pQueryEntity.Col25 });
            if (pQueryEntity.Col26!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col26", Value = pQueryEntity.Col26 });
            if (pQueryEntity.Col27!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col27", Value = pQueryEntity.Col27 });
            if (pQueryEntity.Col28!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col28", Value = pQueryEntity.Col28 });
            if (pQueryEntity.Col29!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col29", Value = pQueryEntity.Col29 });
            if (pQueryEntity.Col30!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col30", Value = pQueryEntity.Col30 });
            if (pQueryEntity.Col31!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col31", Value = pQueryEntity.Col31 });
            if (pQueryEntity.Col32!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col32", Value = pQueryEntity.Col32 });
            if (pQueryEntity.Col33!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col33", Value = pQueryEntity.Col33 });
            if (pQueryEntity.Col34!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col34", Value = pQueryEntity.Col34 });
            if (pQueryEntity.Col35!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col35", Value = pQueryEntity.Col35 });
            if (pQueryEntity.Col36!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col36", Value = pQueryEntity.Col36 });
            if (pQueryEntity.Col37!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col37", Value = pQueryEntity.Col37 });
            if (pQueryEntity.Col38!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col38", Value = pQueryEntity.Col38 });
            if (pQueryEntity.Col39!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col39", Value = pQueryEntity.Col39 });
            if (pQueryEntity.Col40!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col40", Value = pQueryEntity.Col40 });
            if (pQueryEntity.Col41!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col41", Value = pQueryEntity.Col41 });
            if (pQueryEntity.Col42!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col42", Value = pQueryEntity.Col42 });
            if (pQueryEntity.Col43!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col43", Value = pQueryEntity.Col43 });
            if (pQueryEntity.Col44!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col44", Value = pQueryEntity.Col44 });
            if (pQueryEntity.Col45!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col45", Value = pQueryEntity.Col45 });
            if (pQueryEntity.Col46!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col46", Value = pQueryEntity.Col46 });
            if (pQueryEntity.Col47!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col47", Value = pQueryEntity.Col47 });
            if (pQueryEntity.Col48!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col48", Value = pQueryEntity.Col48 });
            if (pQueryEntity.Col49!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col49", Value = pQueryEntity.Col49 });
            if (pQueryEntity.Col50!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col50", Value = pQueryEntity.Col50 });
            if (pQueryEntity.VipRealName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipRealName", Value = pQueryEntity.VipRealName });
            if (pQueryEntity.IsActivate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsActivate", Value = pQueryEntity.IsActivate });
            if (pQueryEntity.VIPImportID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VIPImportID", Value = pQueryEntity.VIPImportID });
            if (pQueryEntity.ShareVipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareVipId", Value = pQueryEntity.ShareVipId });
            if (pQueryEntity.SetoffUserId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SetoffUserId", Value = pQueryEntity.SetoffUserId });
            if (pQueryEntity.ShareUserId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareUserId", Value = pQueryEntity.ShareUserId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out VipEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VIPID"] != DBNull.Value)
			{
				pInstance.VIPID =  Convert.ToString(pReader["VIPID"]);
			}
			if (pReader["VipName"] != DBNull.Value)
			{
				pInstance.VipName =  Convert.ToString(pReader["VipName"]);
			}
			if (pReader["VipLevel"] != DBNull.Value)
			{
				pInstance.VipLevel =   Convert.ToInt32(pReader["VipLevel"]);
			}
			if (pReader["VipCode"] != DBNull.Value)
			{
				pInstance.VipCode =  Convert.ToString(pReader["VipCode"]);
			}
			if (pReader["WeiXin"] != DBNull.Value)
			{
				pInstance.WeiXin =  Convert.ToString(pReader["WeiXin"]);
			}
			if (pReader["WeiXinUserId"] != DBNull.Value)
			{
				pInstance.WeiXinUserId =  Convert.ToString(pReader["WeiXinUserId"]);
			}
			if (pReader["Gender"] != DBNull.Value)
			{
				pInstance.Gender =   Convert.ToInt32(pReader["Gender"]);
			}
			if (pReader["Age"] != DBNull.Value)
			{
				pInstance.Age =   Convert.ToInt32(pReader["Age"]);
			}
			if (pReader["Phone"] != DBNull.Value)
			{
				pInstance.Phone =  Convert.ToString(pReader["Phone"]);
			}
			if (pReader["SinaMBlog"] != DBNull.Value)
			{
				pInstance.SinaMBlog =  Convert.ToString(pReader["SinaMBlog"]);
			}
			if (pReader["TencentMBlog"] != DBNull.Value)
			{
				pInstance.TencentMBlog =  Convert.ToString(pReader["TencentMBlog"]);
			}
			if (pReader["Birthday"] != DBNull.Value)
			{
				pInstance.Birthday =  Convert.ToString(pReader["Birthday"]);
			}
			if (pReader["Qq"] != DBNull.Value)
			{
				pInstance.Qq =  Convert.ToString(pReader["Qq"]);
			}
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["VipSourceId"] != DBNull.Value)
			{
				pInstance.VipSourceId =  Convert.ToString(pReader["VipSourceId"]);
			}
			if (pReader["Integration"] != DBNull.Value)
			{
				pInstance.Integration =  Convert.ToDecimal(pReader["Integration"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =  Convert.ToString(pReader["ClientID"]);
			}
			if (pReader["RecentlySalesTime"] != DBNull.Value)
			{
				pInstance.RecentlySalesTime =  Convert.ToDateTime(pReader["RecentlySalesTime"]);
			}
			if (pReader["RegistrationTime"] != DBNull.Value)
			{
				pInstance.RegistrationTime =  Convert.ToDateTime(pReader["RegistrationTime"]);
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
			if (pReader["APPID"] != DBNull.Value)
			{
				pInstance.APPID =  Convert.ToString(pReader["APPID"]);
			}
			if (pReader["HigherVipID"] != DBNull.Value)
			{
				pInstance.HigherVipID =  Convert.ToString(pReader["HigherVipID"]);
			}
			if (pReader["QRVipCode"] != DBNull.Value)
			{
				pInstance.QRVipCode =  Convert.ToString(pReader["QRVipCode"]);
			}
			if (pReader["City"] != DBNull.Value)
			{
				pInstance.City =  Convert.ToString(pReader["City"]);
			}
			if (pReader["CouponURL"] != DBNull.Value)
			{
				pInstance.CouponURL =  Convert.ToString(pReader["CouponURL"]);
			}
			if (pReader["CouponInfo"] != DBNull.Value)
			{
				pInstance.CouponInfo =  Convert.ToString(pReader["CouponInfo"]);
			}
			if (pReader["PurchaseAmount"] != DBNull.Value)
			{
				pInstance.PurchaseAmount =  Convert.ToDecimal(pReader["PurchaseAmount"]);
			}
			if (pReader["PurchaseCount"] != DBNull.Value)
			{
				pInstance.PurchaseCount =   Convert.ToInt32(pReader["PurchaseCount"]);
			}
			if (pReader["DeliveryAddress"] != DBNull.Value)
			{
				pInstance.DeliveryAddress =  Convert.ToString(pReader["DeliveryAddress"]);
			}
			if (pReader["Longitude"] != DBNull.Value)
			{
				pInstance.Longitude =  Convert.ToString(pReader["Longitude"]);
			}
			if (pReader["Latitude"] != DBNull.Value)
			{
				pInstance.Latitude =  Convert.ToString(pReader["Latitude"]);
			}
			if (pReader["VipPasswrod"] != DBNull.Value)
			{
				pInstance.VipPasswrod =  Convert.ToString(pReader["VipPasswrod"]);
			}
			if (pReader["HeadImgUrl"] != DBNull.Value)
			{
				pInstance.HeadImgUrl =  Convert.ToString(pReader["HeadImgUrl"]);
			}
			if (pReader["Col1"] != DBNull.Value)
			{
				pInstance.Col1 =  Convert.ToString(pReader["Col1"]);
			}
			if (pReader["Col2"] != DBNull.Value)
			{
				pInstance.Col2 =  Convert.ToString(pReader["Col2"]);
			}
			if (pReader["Col3"] != DBNull.Value)
			{
				pInstance.Col3 =  Convert.ToString(pReader["Col3"]);
			}
			if (pReader["Col4"] != DBNull.Value)
			{
				pInstance.Col4 =  Convert.ToString(pReader["Col4"]);
			}
			if (pReader["Col5"] != DBNull.Value)
			{
				pInstance.Col5 =  Convert.ToString(pReader["Col5"]);
			}
			if (pReader["Col6"] != DBNull.Value)
			{
				pInstance.Col6 =  Convert.ToString(pReader["Col6"]);
			}
			if (pReader["Col7"] != DBNull.Value)
			{
				pInstance.Col7 =  Convert.ToString(pReader["Col7"]);
			}
			if (pReader["Col8"] != DBNull.Value)
			{
				pInstance.Col8 =  Convert.ToString(pReader["Col8"]);
			}
			if (pReader["Col9"] != DBNull.Value)
			{
				pInstance.Col9 =  Convert.ToString(pReader["Col9"]);
			}
			if (pReader["Col10"] != DBNull.Value)
			{
				pInstance.Col10 =  Convert.ToString(pReader["Col10"]);
			}
			if (pReader["Col11"] != DBNull.Value)
			{
				pInstance.Col11 =  Convert.ToString(pReader["Col11"]);
			}
			if (pReader["Col12"] != DBNull.Value)
			{
				pInstance.Col12 =  Convert.ToString(pReader["Col12"]);
			}
			if (pReader["Col13"] != DBNull.Value)
			{
				pInstance.Col13 =  Convert.ToString(pReader["Col13"]);
			}
			if (pReader["Col14"] != DBNull.Value)
			{
				pInstance.Col14 =  Convert.ToString(pReader["Col14"]);
			}
			if (pReader["Col15"] != DBNull.Value)
			{
				pInstance.Col15 =  Convert.ToString(pReader["Col15"]);
			}
			if (pReader["Col16"] != DBNull.Value)
			{
				pInstance.Col16 =  Convert.ToString(pReader["Col16"]);
			}
			if (pReader["Col17"] != DBNull.Value)
			{
				pInstance.Col17 =  Convert.ToString(pReader["Col17"]);
			}
			if (pReader["Col18"] != DBNull.Value)
			{
				pInstance.Col18 =  Convert.ToString(pReader["Col18"]);
			}
			if (pReader["Col19"] != DBNull.Value)
			{
				pInstance.Col19 =  Convert.ToString(pReader["Col19"]);
			}
			if (pReader["Col20"] != DBNull.Value)
			{
				pInstance.Col20 =  Convert.ToString(pReader["Col20"]);
			}
			if (pReader["Col21"] != DBNull.Value)
			{
				pInstance.Col21 =  Convert.ToString(pReader["Col21"]);
			}
			if (pReader["Col22"] != DBNull.Value)
			{
				pInstance.Col22 =  Convert.ToString(pReader["Col22"]);
			}
			if (pReader["Col23"] != DBNull.Value)
			{
				pInstance.Col23 =  Convert.ToString(pReader["Col23"]);
			}
			if (pReader["Col24"] != DBNull.Value)
			{
				pInstance.Col24 =  Convert.ToString(pReader["Col24"]);
			}
			if (pReader["Col25"] != DBNull.Value)
			{
				pInstance.Col25 =  Convert.ToString(pReader["Col25"]);
			}
			if (pReader["Col26"] != DBNull.Value)
			{
				pInstance.Col26 =  Convert.ToString(pReader["Col26"]);
			}
			if (pReader["Col27"] != DBNull.Value)
			{
				pInstance.Col27 =  Convert.ToString(pReader["Col27"]);
			}
			if (pReader["Col28"] != DBNull.Value)
			{
				pInstance.Col28 =  Convert.ToString(pReader["Col28"]);
			}
			if (pReader["Col29"] != DBNull.Value)
			{
				pInstance.Col29 =  Convert.ToString(pReader["Col29"]);
			}
			if (pReader["Col30"] != DBNull.Value)
			{
				pInstance.Col30 =  Convert.ToString(pReader["Col30"]);
			}
			if (pReader["Col31"] != DBNull.Value)
			{
				pInstance.Col31 =  Convert.ToString(pReader["Col31"]);
			}
			if (pReader["Col32"] != DBNull.Value)
			{
				pInstance.Col32 =  Convert.ToString(pReader["Col32"]);
			}
			if (pReader["Col33"] != DBNull.Value)
			{
				pInstance.Col33 =  Convert.ToString(pReader["Col33"]);
			}
			if (pReader["Col34"] != DBNull.Value)
			{
				pInstance.Col34 =  Convert.ToString(pReader["Col34"]);
			}
			if (pReader["Col35"] != DBNull.Value)
			{
				pInstance.Col35 =  Convert.ToString(pReader["Col35"]);
			}
			if (pReader["Col36"] != DBNull.Value)
			{
				pInstance.Col36 =  Convert.ToString(pReader["Col36"]);
			}
			if (pReader["Col37"] != DBNull.Value)
			{
				pInstance.Col37 =  Convert.ToString(pReader["Col37"]);
			}
			if (pReader["Col38"] != DBNull.Value)
			{
				pInstance.Col38 =  Convert.ToString(pReader["Col38"]);
			}
			if (pReader["Col39"] != DBNull.Value)
			{
				pInstance.Col39 =  Convert.ToString(pReader["Col39"]);
			}
			if (pReader["Col40"] != DBNull.Value)
			{
				pInstance.Col40 =  Convert.ToString(pReader["Col40"]);
			}
			if (pReader["Col41"] != DBNull.Value)
			{
				pInstance.Col41 =  Convert.ToString(pReader["Col41"]);
			}
			if (pReader["Col42"] != DBNull.Value)
			{
				pInstance.Col42 =  Convert.ToString(pReader["Col42"]);
			}
			if (pReader["Col43"] != DBNull.Value)
			{
				pInstance.Col43 =  Convert.ToString(pReader["Col43"]);
			}
			if (pReader["Col44"] != DBNull.Value)
			{
				pInstance.Col44 =  Convert.ToString(pReader["Col44"]);
			}
			if (pReader["Col45"] != DBNull.Value)
			{
				pInstance.Col45 =  Convert.ToString(pReader["Col45"]);
			}
			if (pReader["Col46"] != DBNull.Value)
			{
				pInstance.Col46 =  Convert.ToString(pReader["Col46"]);
			}
			if (pReader["Col47"] != DBNull.Value)
			{
				pInstance.Col47 =  Convert.ToString(pReader["Col47"]);
			}
			if (pReader["Col48"] != DBNull.Value)
			{
				pInstance.Col48 =  Convert.ToString(pReader["Col48"]);
			}
			if (pReader["Col49"] != DBNull.Value)
			{
				pInstance.Col49 =  Convert.ToString(pReader["Col49"]);
			}
			if (pReader["Col50"] != DBNull.Value)
			{
				pInstance.Col50 =  Convert.ToString(pReader["Col50"]);
			}
			if (pReader["VipRealName"] != DBNull.Value)
			{
				pInstance.VipRealName =  Convert.ToString(pReader["VipRealName"]);
			}
			if (pReader["isActivate"] != DBNull.Value)
			{
				pInstance.IsActivate =   Convert.ToInt32(pReader["isActivate"]);
			}
			if (pReader["VIPImportID"] != DBNull.Value)
			{
				pInstance.VIPImportID =  Convert.ToString(pReader["VIPImportID"]);
			}
			if (pReader["ShareVipId"] != DBNull.Value)
			{
				pInstance.ShareVipId =  Convert.ToString(pReader["ShareVipId"]);
			}
			if (pReader["SetoffUserId"] != DBNull.Value)
			{
				pInstance.SetoffUserId =  Convert.ToString(pReader["SetoffUserId"]);
			}
			if (pReader["ShareUserId"] != DBNull.Value)
			{
				pInstance.ShareUserId =  Convert.ToString(pReader["ShareUserId"]);
			}

        }
        #endregion
    }
}
