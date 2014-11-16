/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/13 10:51:23
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
    /// 表vw_VipCenterInfo的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VwVipCenterInfoDAO : Base.BaseCPOSDAO, ICRUDable<VwVipCenterInfoEntity>, IQueryable<VwVipCenterInfoEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VwVipCenterInfoDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VwVipCenterInfoEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VwVipCenterInfoEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [vw_VipCenterInfo](");
            strSql.Append("[VipName],[VipLevel],[VipCode],[WeiXin],[WeiXinUserId],[Gender],[Age],[Phone],[SinaMBlog],[TencentMBlog],[Birthday],[Qq],[Email],[Status],[VipSourceId],[Integration],[ClientID],[RecentlySalesTime],[RegistrationTime],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[APPID],[HigherVipID],[QRVipCode],[City],[CouponURL],[CouponInfo],[PurchaseAmount],[PurchaseCount],[DeliveryAddress],[Longitude],[Latitude],[VipPasswrod],[HeadImgUrl],[BeginIntegral],[InIntegral],[EndIntegral],[OutIntegral],[ValidIntegral],[InvalidIntegral],[ImageUrl],[ItemKeepCount],[couponCount],[ShoppingCartCount],[LotteryCount],[IsWXPush],[IsSMSPush],[IsAppPush],[VIPID])");
            strSql.Append(" values (");
            strSql.Append("@VipName,@VipLevel,@VipCode,@WeiXin,@WeiXinUserId,@Gender,@Age,@Phone,@SinaMBlog,@TencentMBlog,@Birthday,@Qq,@Email,@Status,@VipSourceId,@Integration,@ClientID,@RecentlySalesTime,@RegistrationTime,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@APPID,@HigherVipID,@QRVipCode,@City,@CouponURL,@CouponInfo,@PurchaseAmount,@PurchaseCount,@DeliveryAddress,@Longitude,@Latitude,@VipPasswrod,@HeadImgUrl,@BeginIntegral,@InIntegral,@EndIntegral,@OutIntegral,@ValidIntegral,@InvalidIntegral,@ImageUrl,@ItemKeepCount,@CouponCount,@ShoppingCartCount,@LotteryCount,@IsWXPush,@IsSMSPush,@IsAppPush,@VIPID)");            

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
					new SqlParameter("@BeginIntegral",SqlDbType.Decimal),
					new SqlParameter("@InIntegral",SqlDbType.Decimal),
					new SqlParameter("@EndIntegral",SqlDbType.Decimal),
					new SqlParameter("@OutIntegral",SqlDbType.Decimal),
					new SqlParameter("@ValidIntegral",SqlDbType.Decimal),
					new SqlParameter("@InvalidIntegral",SqlDbType.Decimal),
					new SqlParameter("@ImageUrl",SqlDbType.VarChar),
					new SqlParameter("@ItemKeepCount",SqlDbType.NVarChar),
					new SqlParameter("@CouponCount",SqlDbType.Int),
					new SqlParameter("@ShoppingCartCount",SqlDbType.Int),
					new SqlParameter("@LotteryCount",SqlDbType.Int),
					new SqlParameter("@IsWXPush",SqlDbType.Int),
					new SqlParameter("@IsSMSPush",SqlDbType.Int),
					new SqlParameter("@IsAppPush",SqlDbType.Int),
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
			parameters[37].Value = pEntity.BeginIntegral;
			parameters[38].Value = pEntity.InIntegral;
			parameters[39].Value = pEntity.EndIntegral;
			parameters[40].Value = pEntity.OutIntegral;
			parameters[41].Value = pEntity.ValidIntegral;
			parameters[42].Value = pEntity.InvalidIntegral;
			parameters[43].Value = pEntity.ImageUrl;
			parameters[44].Value = pEntity.ItemKeepCount;
			parameters[45].Value = pEntity.CouponCount;
			parameters[46].Value = pEntity.ShoppingCartCount;
			parameters[47].Value = pEntity.LotteryCount;
			parameters[48].Value = pEntity.IsWXPush;
			parameters[49].Value = pEntity.IsSMSPush;
			parameters[50].Value = pEntity.IsAppPush;
			parameters[51].Value = pkString;

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
        public VwVipCenterInfoEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vw_VipCenterInfo] where VIPID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            VwVipCenterInfoEntity m = null;
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
        public VwVipCenterInfoEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vw_VipCenterInfo] where isdelete=0");
            //读取数据
            List<VwVipCenterInfoEntity> list = new List<VwVipCenterInfoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwVipCenterInfoEntity m;
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
        public void Update(VwVipCenterInfoEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VwVipCenterInfoEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
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
            strSql.Append("update [vw_VipCenterInfo] set ");
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
            if (pIsUpdateNullField || pEntity.BeginIntegral!=null)
                strSql.Append( "[BeginIntegral]=@BeginIntegral,");
            if (pIsUpdateNullField || pEntity.InIntegral!=null)
                strSql.Append( "[InIntegral]=@InIntegral,");
            if (pIsUpdateNullField || pEntity.EndIntegral!=null)
                strSql.Append( "[EndIntegral]=@EndIntegral,");
            if (pIsUpdateNullField || pEntity.OutIntegral!=null)
                strSql.Append( "[OutIntegral]=@OutIntegral,");
            if (pIsUpdateNullField || pEntity.ValidIntegral!=null)
                strSql.Append( "[ValidIntegral]=@ValidIntegral,");
            if (pIsUpdateNullField || pEntity.InvalidIntegral!=null)
                strSql.Append( "[InvalidIntegral]=@InvalidIntegral,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.ItemKeepCount!=null)
                strSql.Append( "[ItemKeepCount]=@ItemKeepCount,");
            if (pIsUpdateNullField || pEntity.CouponCount!=null)
                strSql.Append( "[couponCount]=@CouponCount,");
            if (pIsUpdateNullField || pEntity.ShoppingCartCount!=null)
                strSql.Append( "[ShoppingCartCount]=@ShoppingCartCount,");
            if (pIsUpdateNullField || pEntity.LotteryCount!=null)
                strSql.Append( "[LotteryCount]=@LotteryCount,");
            if (pIsUpdateNullField || pEntity.IsWXPush!=null)
                strSql.Append( "[IsWXPush]=@IsWXPush,");
            if (pIsUpdateNullField || pEntity.IsSMSPush!=null)
                strSql.Append( "[IsSMSPush]=@IsSMSPush,");
            if (pIsUpdateNullField || pEntity.IsAppPush!=null)
                strSql.Append( "[IsAppPush]=@IsAppPush");
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
					new SqlParameter("@BeginIntegral",SqlDbType.Decimal),
					new SqlParameter("@InIntegral",SqlDbType.Decimal),
					new SqlParameter("@EndIntegral",SqlDbType.Decimal),
					new SqlParameter("@OutIntegral",SqlDbType.Decimal),
					new SqlParameter("@ValidIntegral",SqlDbType.Decimal),
					new SqlParameter("@InvalidIntegral",SqlDbType.Decimal),
					new SqlParameter("@ImageUrl",SqlDbType.VarChar),
					new SqlParameter("@ItemKeepCount",SqlDbType.NVarChar),
					new SqlParameter("@CouponCount",SqlDbType.Int),
					new SqlParameter("@ShoppingCartCount",SqlDbType.Int),
					new SqlParameter("@LotteryCount",SqlDbType.Int),
					new SqlParameter("@IsWXPush",SqlDbType.Int),
					new SqlParameter("@IsSMSPush",SqlDbType.Int),
					new SqlParameter("@IsAppPush",SqlDbType.Int),
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
			parameters[34].Value = pEntity.BeginIntegral;
			parameters[35].Value = pEntity.InIntegral;
			parameters[36].Value = pEntity.EndIntegral;
			parameters[37].Value = pEntity.OutIntegral;
			parameters[38].Value = pEntity.ValidIntegral;
			parameters[39].Value = pEntity.InvalidIntegral;
			parameters[40].Value = pEntity.ImageUrl;
			parameters[41].Value = pEntity.ItemKeepCount;
			parameters[42].Value = pEntity.CouponCount;
			parameters[43].Value = pEntity.ShoppingCartCount;
			parameters[44].Value = pEntity.LotteryCount;
			parameters[45].Value = pEntity.IsWXPush;
			parameters[46].Value = pEntity.IsSMSPush;
			parameters[47].Value = pEntity.IsAppPush;
			parameters[48].Value = pEntity.VIPID;

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
        public void Update(VwVipCenterInfoEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VwVipCenterInfoEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VwVipCenterInfoEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VwVipCenterInfoEntity pEntity, IDbTransaction pTran)
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
            sql.AppendLine("update [vw_VipCenterInfo] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VIPID=@VIPID;");
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
        public void Delete(VwVipCenterInfoEntity[] pEntities, IDbTransaction pTran)
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
        public void Delete(VwVipCenterInfoEntity[] pEntities)
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
            sql.AppendLine("update [vw_VipCenterInfo] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where VIPID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VwVipCenterInfoEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vw_VipCenterInfo] where isdelete=0 ");
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
            List<VwVipCenterInfoEntity> list = new List<VwVipCenterInfoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwVipCenterInfoEntity m;
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
        public PagedQueryResult<VwVipCenterInfoEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
            pagedSql.AppendFormat(") as ___rn,* from [VwVipCenterInfo] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VwVipCenterInfo] where isdelete=0 ");
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
            PagedQueryResult<VwVipCenterInfoEntity> result = new PagedQueryResult<VwVipCenterInfoEntity>();
            List<VwVipCenterInfoEntity> list = new List<VwVipCenterInfoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VwVipCenterInfoEntity m;
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
        public VwVipCenterInfoEntity[] QueryByEntity(VwVipCenterInfoEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VwVipCenterInfoEntity> PagedQueryByEntity(VwVipCenterInfoEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VwVipCenterInfoEntity pQueryEntity)
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
            if (pQueryEntity.BeginIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginIntegral", Value = pQueryEntity.BeginIntegral });
            if (pQueryEntity.InIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InIntegral", Value = pQueryEntity.InIntegral });
            if (pQueryEntity.EndIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndIntegral", Value = pQueryEntity.EndIntegral });
            if (pQueryEntity.OutIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutIntegral", Value = pQueryEntity.OutIntegral });
            if (pQueryEntity.ValidIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ValidIntegral", Value = pQueryEntity.ValidIntegral });
            if (pQueryEntity.InvalidIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InvalidIntegral", Value = pQueryEntity.InvalidIntegral });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.ItemKeepCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemKeepCount", Value = pQueryEntity.ItemKeepCount });
            if (pQueryEntity.CouponCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponCount", Value = pQueryEntity.CouponCount });
            if (pQueryEntity.ShoppingCartCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShoppingCartCount", Value = pQueryEntity.ShoppingCartCount });
            if (pQueryEntity.LotteryCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LotteryCount", Value = pQueryEntity.LotteryCount });
            if (pQueryEntity.IsWXPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsWXPush", Value = pQueryEntity.IsWXPush });
            if (pQueryEntity.IsSMSPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSMSPush", Value = pQueryEntity.IsSMSPush });
            if (pQueryEntity.IsAppPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAppPush", Value = pQueryEntity.IsAppPush });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out VwVipCenterInfoEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VwVipCenterInfoEntity();
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
			if (pReader["BeginIntegral"] != DBNull.Value)
			{
				pInstance.BeginIntegral =  Convert.ToDecimal(pReader["BeginIntegral"]);
			}
			if (pReader["InIntegral"] != DBNull.Value)
			{
				pInstance.InIntegral =  Convert.ToDecimal(pReader["InIntegral"]);
			}
			if (pReader["EndIntegral"] != DBNull.Value)
			{
				pInstance.EndIntegral =  Convert.ToDecimal(pReader["EndIntegral"]);
			}
			if (pReader["OutIntegral"] != DBNull.Value)
			{
				pInstance.OutIntegral =  Convert.ToDecimal(pReader["OutIntegral"]);
			}
			if (pReader["ValidIntegral"] != DBNull.Value)
			{
				pInstance.ValidIntegral =  Convert.ToDecimal(pReader["ValidIntegral"]);
			}
			if (pReader["InvalidIntegral"] != DBNull.Value)
			{
				pInstance.InvalidIntegral =  Convert.ToDecimal(pReader["InvalidIntegral"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["ItemKeepCount"] != DBNull.Value)
			{
				pInstance.ItemKeepCount =  Convert.ToString(pReader["ItemKeepCount"]);
			}
			if (pReader["couponCount"] != DBNull.Value)
			{
				pInstance.CouponCount =   Convert.ToInt32(pReader["couponCount"]);
			}
			if (pReader["ShoppingCartCount"] != DBNull.Value)
			{
				pInstance.ShoppingCartCount =   Convert.ToInt32(pReader["ShoppingCartCount"]);
			}
			if (pReader["LotteryCount"] != DBNull.Value)
			{
				pInstance.LotteryCount =   Convert.ToInt32(pReader["LotteryCount"]);
			}
			if (pReader["IsWXPush"] != DBNull.Value)
			{
				pInstance.IsWXPush =   Convert.ToInt32(pReader["IsWXPush"]);
			}
			if (pReader["IsSMSPush"] != DBNull.Value)
			{
				pInstance.IsSMSPush =   Convert.ToInt32(pReader["IsSMSPush"]);
			}
			if (pReader["IsAppPush"] != DBNull.Value)
			{
				pInstance.IsAppPush =   Convert.ToInt32(pReader["IsAppPush"]);
			}

        }
        #endregion
    }
}
