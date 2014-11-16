/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 10:37:49
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
    /// 表t_unit的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TUnitDAO : Base.BaseCPOSDAO, ICRUDable<TUnitEntity>, IQueryable<TUnitEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TUnitDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TUnitEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TUnitEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            ////初始化固定字段
            //pEntity.CreateTime = DateTime.Now;
            //pEntity.CreateBy = CurrentUserInfo.UserID;
            //pEntity.LastUpdateTime = pEntity.CreateTime;
            //pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            //pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [t_unit](");
            strSql.Append("[type_id],[unit_code],[unit_name],[unit_name_en],[unit_name_short],[unit_city_id],[unit_address],[unit_contact],[unit_tel],[unit_fax],[unit_email],[unit_postcode],[unit_remark],[Status],[unit_flag],[CUSTOMER_LEVEL],[create_user_id],[create_time],[modify_user_id],[modify_time],[status_desc],[bat_id],[if_flag],[customer_id],[longitude],[dimension],[imageURL],[ftpImagerURL],[webserversURL],[weiXinId],[dimensionalCodeURL],[unit_id])");//,[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete]
            strSql.Append(" values (");
            strSql.Append("@TypeID,@UnitCode,@UnitName,@UnitNameEn,@UnitNameShort,@UnitCityID,@UnitAddress,@UnitContact,@UnitTel,@UnitFax,@UnitEmail,@UnitPostcode,@UnitRemark,@Status,@UnitFlag,@CUSTOMERLEVEL,@CreateUserID,@CreateTime,@ModifyUserID,@ModifyTime,@StatusDesc,@BatID,@IfFlag,@CustomerID,@Longitude,@Dimension,@ImageURL,@FtpImagerURL,@WebserversURL,@WeiXinId,@DimensionalCodeURL,@UnitID)");//,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete            

            string pkString = pEntity.UnitID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TypeID",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitNameEn",SqlDbType.NVarChar),
					new SqlParameter("@UnitNameShort",SqlDbType.NVarChar),
					new SqlParameter("@UnitCityID",SqlDbType.NVarChar),
					new SqlParameter("@UnitAddress",SqlDbType.NVarChar),
					new SqlParameter("@UnitContact",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@UnitFax",SqlDbType.NVarChar),
					new SqlParameter("@UnitEmail",SqlDbType.NVarChar),
					new SqlParameter("@UnitPostcode",SqlDbType.NVarChar),
					new SqlParameter("@UnitRemark",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@UnitFlag",SqlDbType.NVarChar),
					new SqlParameter("@CUSTOMERLEVEL",SqlDbType.Int),
					new SqlParameter("@CreateUserID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.NVarChar),
					new SqlParameter("@ModifyUserID",SqlDbType.NVarChar),
					new SqlParameter("@ModifyTime",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@BatID",SqlDbType.NVarChar),
					new SqlParameter("@IfFlag",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@Longitude",SqlDbType.NVarChar),
					new SqlParameter("@Dimension",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@FtpImagerURL",SqlDbType.NVarChar),
					new SqlParameter("@WebserversURL",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinId",SqlDbType.NVarChar),
					new SqlParameter("@DimensionalCodeURL",SqlDbType.NVarChar),
                    //new SqlParameter("@CreateBy",SqlDbType.NVarChar),
                    //new SqlParameter("@CreateTime",SqlDbType.DateTime),
                    //new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
                    //new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
                    //new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@UnitID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.TypeID;
            parameters[1].Value = pEntity.UnitCode;
            parameters[2].Value = pEntity.UnitName;
            parameters[3].Value = pEntity.UnitNameEn;
            parameters[4].Value = pEntity.UnitNameShort;
            parameters[5].Value = pEntity.UnitCityID;
            parameters[6].Value = pEntity.UnitAddress;
            parameters[7].Value = pEntity.UnitContact;
            parameters[8].Value = pEntity.UnitTel;
            parameters[9].Value = pEntity.UnitFax;
            parameters[10].Value = pEntity.UnitEmail;
            parameters[11].Value = pEntity.UnitPostcode;
            parameters[12].Value = pEntity.UnitRemark;
            parameters[13].Value = pEntity.Status;
            parameters[14].Value = pEntity.UnitFlag;
            parameters[15].Value = pEntity.CUSTOMERLEVEL;
            parameters[16].Value = pEntity.CreateUserID;
            parameters[17].Value = pEntity.CreateTime;
            parameters[18].Value = pEntity.ModifyUserID;
            parameters[19].Value = pEntity.ModifyTime;
            parameters[20].Value = pEntity.StatusDesc;
            parameters[21].Value = pEntity.BatID;
            parameters[22].Value = pEntity.IfFlag;
            parameters[23].Value = pEntity.CustomerID;
            parameters[24].Value = pEntity.Longitude;
            parameters[25].Value = pEntity.Dimension;
            parameters[26].Value = pEntity.ImageURL;
            parameters[27].Value = pEntity.FtpImagerURL;
            parameters[28].Value = pEntity.WebserversURL;
            parameters[29].Value = pEntity.WeiXinId;
            parameters[30].Value = pEntity.DimensionalCodeURL;
            //parameters[31].Value = pEntity.CreateBy;
            //parameters[32].Value = pEntity.CreateTime;
            //parameters[33].Value = pEntity.LastUpdateBy;
            //parameters[34].Value = pEntity.LastUpdateTime;
            //parameters[35].Value = pEntity.IsDelete;
            parameters[31].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.UnitID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TUnitEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_unit] where unit_id='{0}'  ", id.ToString());//and IsDelete=0
            //读取数据
            TUnitEntity m = null;
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
        public TUnitEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_unit] where 1=1 ");//isdelete=0
            //读取数据
            List<TUnitEntity> list = new List<TUnitEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TUnitEntity m;
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
        public void Update(TUnitEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(TUnitEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UnitID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            // //初始化固定字段
            //pEntity.LastUpdateTime = DateTime.Now;
            //pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [t_unit] set ");
            if (pIsUpdateNullField || pEntity.TypeID != null)
                strSql.Append("[type_id]=@TypeID,");
            if (pIsUpdateNullField || pEntity.UnitCode != null)
                strSql.Append("[unit_code]=@UnitCode,");
            if (pIsUpdateNullField || pEntity.UnitName != null)
                strSql.Append("[unit_name]=@UnitName,");
            if (pIsUpdateNullField || pEntity.UnitNameEn != null)
                strSql.Append("[unit_name_en]=@UnitNameEn,");
            if (pIsUpdateNullField || pEntity.UnitNameShort != null)
                strSql.Append("[unit_name_short]=@UnitNameShort,");
            if (pIsUpdateNullField || pEntity.UnitCityID != null)
                strSql.Append("[unit_city_id]=@UnitCityID,");
            if (pIsUpdateNullField || pEntity.UnitAddress != null)
                strSql.Append("[unit_address]=@UnitAddress,");
            if (pIsUpdateNullField || pEntity.UnitContact != null)
                strSql.Append("[unit_contact]=@UnitContact,");
            if (pIsUpdateNullField || pEntity.UnitTel != null)
                strSql.Append("[unit_tel]=@UnitTel,");
            if (pIsUpdateNullField || pEntity.UnitFax != null)
                strSql.Append("[unit_fax]=@UnitFax,");
            if (pIsUpdateNullField || pEntity.UnitEmail != null)
                strSql.Append("[unit_email]=@UnitEmail,");
            if (pIsUpdateNullField || pEntity.UnitPostcode != null)
                strSql.Append("[unit_postcode]=@UnitPostcode,");
            if (pIsUpdateNullField || pEntity.UnitRemark != null)
                strSql.Append("[unit_remark]=@UnitRemark,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.UnitFlag != null)
                strSql.Append("[unit_flag]=@UnitFlag,");
            if (pIsUpdateNullField || pEntity.CUSTOMERLEVEL != null)
                strSql.Append("[CUSTOMER_LEVEL]=@CUSTOMERLEVEL,");
            if (pIsUpdateNullField || pEntity.CreateUserID != null)
                strSql.Append("[create_user_id]=@CreateUserID,");
            if (pIsUpdateNullField || pEntity.ModifyUserID != null)
                strSql.Append("[modify_user_id]=@ModifyUserID,");
            if (pIsUpdateNullField || pEntity.ModifyTime != null)
                strSql.Append("[modify_time]=@ModifyTime,");
            if (pIsUpdateNullField || pEntity.StatusDesc != null)
                strSql.Append("[status_desc]=@StatusDesc,");
            if (pIsUpdateNullField || pEntity.BatID != null)
                strSql.Append("[bat_id]=@BatID,");
            if (pIsUpdateNullField || pEntity.IfFlag != null)
                strSql.Append("[if_flag]=@IfFlag,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[customer_id]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.Longitude != null)
                strSql.Append("[longitude]=@Longitude,");
            if (pIsUpdateNullField || pEntity.Dimension != null)
                strSql.Append("[dimension]=@Dimension,");
            if (pIsUpdateNullField || pEntity.ImageURL != null)
                strSql.Append("[imageURL]=@ImageURL,");
            if (pIsUpdateNullField || pEntity.FtpImagerURL != null)
                strSql.Append("[ftpImagerURL]=@FtpImagerURL,");
            if (pIsUpdateNullField || pEntity.WebserversURL != null)
                strSql.Append("[webserversURL]=@WebserversURL,");
            if (pIsUpdateNullField || pEntity.WeiXinId != null)
                strSql.Append("[weiXinId]=@WeiXinId,");
            if (pIsUpdateNullField || pEntity.DimensionalCodeURL != null)
                strSql.Append("[dimensionalCodeURL]=@DimensionalCodeURL,");
            //if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
            //    strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            //if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
            //    strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where unit_id=@UnitID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TypeID",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitNameEn",SqlDbType.NVarChar),
					new SqlParameter("@UnitNameShort",SqlDbType.NVarChar),
					new SqlParameter("@UnitCityID",SqlDbType.NVarChar),
					new SqlParameter("@UnitAddress",SqlDbType.NVarChar),
					new SqlParameter("@UnitContact",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@UnitFax",SqlDbType.NVarChar),
					new SqlParameter("@UnitEmail",SqlDbType.NVarChar),
					new SqlParameter("@UnitPostcode",SqlDbType.NVarChar),
					new SqlParameter("@UnitRemark",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@UnitFlag",SqlDbType.NVarChar),
					new SqlParameter("@CUSTOMERLEVEL",SqlDbType.Int),
					new SqlParameter("@CreateUserID",SqlDbType.NVarChar),
					new SqlParameter("@ModifyUserID",SqlDbType.NVarChar),
					new SqlParameter("@ModifyTime",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@BatID",SqlDbType.NVarChar),
					new SqlParameter("@IfFlag",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@Longitude",SqlDbType.NVarChar),
					new SqlParameter("@Dimension",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@FtpImagerURL",SqlDbType.NVarChar),
					new SqlParameter("@WebserversURL",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinId",SqlDbType.NVarChar),
					new SqlParameter("@DimensionalCodeURL",SqlDbType.NVarChar),
                    //new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
                    //new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@UnitID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.TypeID;
            parameters[1].Value = pEntity.UnitCode;
            parameters[2].Value = pEntity.UnitName;
            parameters[3].Value = pEntity.UnitNameEn;
            parameters[4].Value = pEntity.UnitNameShort;
            parameters[5].Value = pEntity.UnitCityID;
            parameters[6].Value = pEntity.UnitAddress;
            parameters[7].Value = pEntity.UnitContact;
            parameters[8].Value = pEntity.UnitTel;
            parameters[9].Value = pEntity.UnitFax;
            parameters[10].Value = pEntity.UnitEmail;
            parameters[11].Value = pEntity.UnitPostcode;
            parameters[12].Value = pEntity.UnitRemark;
            parameters[13].Value = pEntity.Status;
            parameters[14].Value = pEntity.UnitFlag;
            parameters[15].Value = pEntity.CUSTOMERLEVEL;
            parameters[16].Value = pEntity.CreateUserID;
            parameters[17].Value = pEntity.ModifyUserID;
            parameters[18].Value = pEntity.ModifyTime;
            parameters[19].Value = pEntity.StatusDesc;
            parameters[20].Value = pEntity.BatID;
            parameters[21].Value = pEntity.IfFlag;
            parameters[22].Value = pEntity.CustomerID;
            parameters[23].Value = pEntity.Longitude;
            parameters[24].Value = pEntity.Dimension;
            parameters[25].Value = pEntity.ImageURL;
            parameters[26].Value = pEntity.FtpImagerURL;
            parameters[27].Value = pEntity.WebserversURL;
            parameters[28].Value = pEntity.WeiXinId;
            parameters[29].Value = pEntity.DimensionalCodeURL;
            //parameters[30].Value = pEntity.LastUpdateBy;
            //parameters[31].Value = pEntity.LastUpdateTime;
            parameters[30].Value = pEntity.UnitID;

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
        public void Update(TUnitEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(TUnitEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TUnitEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TUnitEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UnitID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.UnitID, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine("update [t_unit] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where unit_id=@UnitID;");
            sql.AppendLine("update [t_unit] set modify_time=@LastUpdateTime,modify_user_id=@LastUpdateBy,Status=0 where unit_id=@UnitID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                //new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.NVarChar,Value=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@UnitID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TUnitEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.UnitID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.UnitID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TUnitEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine("update [t_unit] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where unit_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            sql.AppendLine("update [t_unit] set modify_time='" + DateTime.Now.ToString() + "',modify_user_id='" + CurrentUserInfo.UserID + "',Status=0 where unit_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public TUnitEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_unit] where 1=1 ");//isdelete=0
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
            List<TUnitEntity> list = new List<TUnitEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TUnitEntity m;
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
        public PagedQueryResult<TUnitEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [UnitID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Unit] where 1=1 ");//isdelete=0
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_Unit] where 1=1 ");//isdelete=0
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<TUnitEntity> result = new PagedQueryResult<TUnitEntity>();
            List<TUnitEntity> list = new List<TUnitEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TUnitEntity m;
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
        public TUnitEntity[] QueryByEntity(TUnitEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<TUnitEntity> PagedQueryByEntity(TUnitEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(TUnitEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.UnitID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.TypeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TypeID", Value = pQueryEntity.TypeID });
            if (pQueryEntity.UnitCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCode", Value = pQueryEntity.UnitCode });
            if (pQueryEntity.UnitName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.UnitNameEn != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitNameEn", Value = pQueryEntity.UnitNameEn });
            if (pQueryEntity.UnitNameShort != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitNameShort", Value = pQueryEntity.UnitNameShort });
            if (pQueryEntity.UnitCityID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCityID", Value = pQueryEntity.UnitCityID });
            if (pQueryEntity.UnitAddress != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitAddress", Value = pQueryEntity.UnitAddress });
            if (pQueryEntity.UnitContact != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitContact", Value = pQueryEntity.UnitContact });
            if (pQueryEntity.UnitTel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitTel", Value = pQueryEntity.UnitTel });
            if (pQueryEntity.UnitFax != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitFax", Value = pQueryEntity.UnitFax });
            if (pQueryEntity.UnitEmail != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitEmail", Value = pQueryEntity.UnitEmail });
            if (pQueryEntity.UnitPostcode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitPostcode", Value = pQueryEntity.UnitPostcode });
            if (pQueryEntity.UnitRemark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitRemark", Value = pQueryEntity.UnitRemark });
            if (pQueryEntity.Status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.UnitFlag != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitFlag", Value = pQueryEntity.UnitFlag });
            if (pQueryEntity.CUSTOMERLEVEL != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CUSTOMERLEVEL", Value = pQueryEntity.CUSTOMERLEVEL });
            if (pQueryEntity.CreateUserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateUserID", Value = pQueryEntity.CreateUserID });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.ModifyUserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModifyUserID", Value = pQueryEntity.ModifyUserID });
            if (pQueryEntity.ModifyTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModifyTime", Value = pQueryEntity.ModifyTime });
            if (pQueryEntity.StatusDesc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusDesc", Value = pQueryEntity.StatusDesc });
            if (pQueryEntity.BatID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BatID", Value = pQueryEntity.BatID });
            if (pQueryEntity.IfFlag != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IfFlag", Value = pQueryEntity.IfFlag });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.Longitude != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Longitude", Value = pQueryEntity.Longitude });
            if (pQueryEntity.Dimension != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Dimension", Value = pQueryEntity.Dimension });
            if (pQueryEntity.ImageURL != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageURL", Value = pQueryEntity.ImageURL });
            if (pQueryEntity.FtpImagerURL != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FtpImagerURL", Value = pQueryEntity.FtpImagerURL });
            if (pQueryEntity.WebserversURL != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WebserversURL", Value = pQueryEntity.WebserversURL });
            if (pQueryEntity.WeiXinId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinId", Value = pQueryEntity.WeiXinId });
            if (pQueryEntity.DimensionalCodeURL != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DimensionalCodeURL", Value = pQueryEntity.DimensionalCodeURL });
            //if (pQueryEntity.CreateBy!=null)
            //    lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            //if (pQueryEntity.CreateTime!=null)
            //    lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            //if (pQueryEntity.LastUpdateBy!=null)
            //    lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            //if (pQueryEntity.LastUpdateTime!=null)
            //    lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            //if (pQueryEntity.IsDelete!=null)
            //    lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out TUnitEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new TUnitEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["unit_id"] != DBNull.Value)
            {
                pInstance.UnitID = Convert.ToString(pReader["unit_id"]);
            }
            if (pReader["type_id"] != DBNull.Value)
            {
                pInstance.TypeID = Convert.ToString(pReader["type_id"]);
            }
            if (pReader["unit_code"] != DBNull.Value)
            {
                pInstance.UnitCode = Convert.ToString(pReader["unit_code"]);
            }
            if (pReader["unit_name"] != DBNull.Value)
            {
                pInstance.UnitName = Convert.ToString(pReader["unit_name"]);
            }
            if (pReader["unit_name_en"] != DBNull.Value)
            {
                pInstance.UnitNameEn = Convert.ToString(pReader["unit_name_en"]);
            }
            if (pReader["unit_name_short"] != DBNull.Value)
            {
                pInstance.UnitNameShort = Convert.ToString(pReader["unit_name_short"]);
            }
            if (pReader["unit_city_id"] != DBNull.Value)
            {
                pInstance.UnitCityID = Convert.ToString(pReader["unit_city_id"]);
            }
            if (pReader["unit_address"] != DBNull.Value)
            {
                pInstance.UnitAddress = Convert.ToString(pReader["unit_address"]);
            }
            if (pReader["unit_contact"] != DBNull.Value)
            {
                pInstance.UnitContact = Convert.ToString(pReader["unit_contact"]);
            }
            if (pReader["unit_tel"] != DBNull.Value)
            {
                pInstance.UnitTel = Convert.ToString(pReader["unit_tel"]);
            }
            if (pReader["unit_fax"] != DBNull.Value)
            {
                pInstance.UnitFax = Convert.ToString(pReader["unit_fax"]);
            }
            if (pReader["unit_email"] != DBNull.Value)
            {
                pInstance.UnitEmail = Convert.ToString(pReader["unit_email"]);
            }
            if (pReader["unit_postcode"] != DBNull.Value)
            {
                pInstance.UnitPostcode = Convert.ToString(pReader["unit_postcode"]);
            }
            if (pReader["unit_remark"] != DBNull.Value)
            {
                pInstance.UnitRemark = Convert.ToString(pReader["unit_remark"]);
            }
            if (pReader["Status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToString(pReader["Status"]);
            }
            if (pReader["unit_flag"] != DBNull.Value)
            {
                pInstance.UnitFlag = Convert.ToString(pReader["unit_flag"]);
            }
            if (pReader["CUSTOMER_LEVEL"] != DBNull.Value)
            {
                pInstance.CUSTOMERLEVEL = Convert.ToInt32(pReader["CUSTOMER_LEVEL"]);
            }
            if (pReader["create_user_id"] != DBNull.Value)
            {
                pInstance.CreateUserID = Convert.ToString(pReader["create_user_id"]);
            }
            if (pReader["create_time"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToString(pReader["create_time"]);
            }
            if (pReader["modify_user_id"] != DBNull.Value)
            {
                pInstance.ModifyUserID = Convert.ToString(pReader["modify_user_id"]);
            }
            if (pReader["modify_time"] != DBNull.Value)
            {
                pInstance.ModifyTime = Convert.ToString(pReader["modify_time"]);
            }
            if (pReader["status_desc"] != DBNull.Value)
            {
                pInstance.StatusDesc = Convert.ToString(pReader["status_desc"]);
            }
            if (pReader["bat_id"] != DBNull.Value)
            {
                pInstance.BatID = Convert.ToString(pReader["bat_id"]);
            }
            if (pReader["if_flag"] != DBNull.Value)
            {
                pInstance.IfFlag = Convert.ToString(pReader["if_flag"]);
            }
            if (pReader["customer_id"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["customer_id"]);
            }
            if (pReader["longitude"] != DBNull.Value)
            {
                pInstance.Longitude = Convert.ToString(pReader["longitude"]);
            }
            if (pReader["dimension"] != DBNull.Value)
            {
                pInstance.Dimension = Convert.ToString(pReader["dimension"]);
            }
            if (pReader["imageURL"] != DBNull.Value)
            {
                pInstance.ImageURL = Convert.ToString(pReader["imageURL"]);
            }
            if (pReader["ftpImagerURL"] != DBNull.Value)
            {
                pInstance.FtpImagerURL = Convert.ToString(pReader["ftpImagerURL"]);
            }
            if (pReader["webserversURL"] != DBNull.Value)
            {
                pInstance.WebserversURL = Convert.ToString(pReader["webserversURL"]);
            }
            if (pReader["weiXinId"] != DBNull.Value)
            {
                pInstance.WeiXinId = Convert.ToString(pReader["weiXinId"]);
            }
            if (pReader["dimensionalCodeURL"] != DBNull.Value)
            {
                pInstance.DimensionalCodeURL = Convert.ToString(pReader["dimensionalCodeURL"]);
            }
            //if (pReader["CreateBy"] != DBNull.Value)
            //{
            //    pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
            //}
            //if (pReader["CreateTime"] != DBNull.Value)
            //{
            //    pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
            //}
            //if (pReader["LastUpdateBy"] != DBNull.Value)
            //{
            //    pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
            //}
            //if (pReader["LastUpdateTime"] != DBNull.Value)
            //{
            //    pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
            //}
            //if (pReader["IsDelete"] != DBNull.Value)
            //{
            //    pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
            //}

        }
        #endregion
    }
}
