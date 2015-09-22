using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;


namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 组织数据方法集
    /// </summary>
    public class UnitService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public UnitService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 根据组织标识，获取组织详细信息
        /// <summary>
        /// 根据组织标识，获取组织详细信息
        /// </summary>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public DataSet GetUnitById(string unitId)
        {
            string sql = GetSql("")
                      + "  where   a.unit_code!=e.customer_code and a.unit_code!='ONLINE' and   a.unit_id= '" + unitId + "'";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region Jermyn20130913 根据类型获取相关门店
        /// <summary>
        /// 根据类型获取相关门店
        /// </summary>
        /// <param name="unitId">unitTypeId</param>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public DataSet GetUnitByUnitType(string unitTypeCode, string unitId)
        {
            string sql = GetSql("")
                      + " where 1=1 and a.unit_code!=e.customer_code and a.unit_code!='ONLINE' and a.customer_id = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (unitId != null && !unitId.Equals(""))
            {
                sql += "  and a.unit_id= '" + unitId + "'";
            }
            if (unitTypeCode != null && !unitTypeCode.Equals(""))
            {
                sql += "  and b.type_code = '" + unitTypeCode + "'";
            }
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion


        #region 单位树
        /// <summary>
        /// 获取某种关联模式下的根单位列表(只含已启用的)
        /// </summary>
        /// <param name="unitRelationModeId">关联模式Id</param>
        /// <returns></returns>
        public DataSet GetRootUnitsByUnitRelationMode()
        {

            DataSet ds = new DataSet();
            string sql = GetSql("")
                       + "  inner join (select distinct unit_id from t_user_role "
                       + "  where 1=1 and status='1' and user_id= '" + this.loggingSessionInfo.CurrentUser.User_Id + "' and role_id= '" + this.loggingSessionInfo.CurrentUserRole.RoleId + "' "
                       + "  ) d on a.unit_id=d.unit_id "
                       + "  where 1=1 and a.unit_code!=e.customer_code and a.unit_code!='ONLINE' and a.customer_id='" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' "
                       + " and a.status = '1' order by a.unit_code";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取某种关联模式下的下一级子单位列表(只含已启用的)
        /// </summary>
        /// <param name="unitRelationModeId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DataSet GetSubUnitsByUnitRelationMode(string unitId)
        {
            DataSet ds = new DataSet();
            string sql = "";
            sql = GetUserUntil();
            sql += "declare @T table(unit_id nvarchar(100)); "
                    + " insert into @T "
                    + " select dst_unit_id From T_Unit_Relation "
                    + " where status = '1' "
                    + " and src_unit_id= '" + unitId + "'; ";
            sql = GetSql(sql)
                + " inner join @T d "
                + " on(a.unit_id = d.unit_id) ";
            if (!GetSonUntil(unitId))
            {
                sql += "inner join @CategoryInfo as useruntil on useruntil.dst_unit_id=d.unit_id";
            }
            sql += "  where 1=1 and a.unit_code!=e.customer_code and a.unit_code!='ONLINE' and a.status = '1' and a.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' order by a.unit_code";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region GetUserUntil
        /// <summary>
        ///获取所有的父节点
        /// </summary>
        /// <returns></returns>
        public string GetUserUntil()
        {
            string sql = @"
            declare @CategoryInfo table(dst_unit_id nvarchar(100),src_unit_id nvarchar(100));
            WITH CategoryInfo as
                 (
            SELECT dst_unit_id,src_unit_id 
          FROM T_Unit_Relation  as t_u inner join 
         (
          select 
          a.unit_id UnitId
          from t_user_role a  
          inner join t_unit c on a.unit_id=c.unit_id  
          inner join t_user d on a.user_id=d.user_id  
          inner join t_role e on a.role_id=e.role_id  
          left join T_Def_App f  on e.def_app_id=f.def_app_id 
          where a.user_id='" + this.loggingSessionInfo.CurrentLoggingManager.User_Id + @"' 
          and e.def_app_id =case when ''='' then e.def_app_id else '' end  
          and  a.status = '1'
          ) as x on x.UnitId=t_u.dst_unit_id
         UNION ALL
         SELECT tu.dst_unit_id,tu.src_unit_id FROM T_Unit_Relation AS tu,CategoryInfo AS tup 
         WHERE tu.src_unit_id = tup.dst_unit_id and tu.status='1' 
          )
        insert into @CategoryInfo
       select distinct *  from CategoryInfo ";

            return sql;
        }

        #endregion

    
        /// <summary>
        /// 获取某个用户权限下的门店数据
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DataSet GetUnitByUser(string CustomerID,string loginUserID)
        { 
            DataSet ds = new DataSet();
            string sql = "";          
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@loginUserID", loginUserID));


            sql = @"----在这里要把用户的权限能看到的数据加上
DECLARE @AllUnit NVARCHAR(200)

CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
 INSERT #UnitSET (UnitID)                  
   SELECT DISTINCT R.UnitID                   
   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  (@CustomerId,UR.unit_id,205)  R                  
   WHERE user_id=@loginUserID          ---根据账户的角色去查角色对应的  unit_id


";

             sql = GetSql(sql)
                + " inner join #UnitSET  d on(a.unit_id = d.unitid)  where  1=1 and a.unit_code!=e.customer_code and a.unit_code!='ONLINE' and a.status = '1'  order by a.unit_code ";


               sql += @" drop table #UnitSET                ";
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text,  sql,ls.ToArray());
            return ds;
        }
        #region MyRegion
        public bool GetSonUntil(string unitId)
        {
            string sql = @"
            WITH CategoryInfo as
                 (
          SELECT dst_unit_id,src_unit_id 
          FROM T_Unit_Relation  as t_u inner join 
         (
          select 
          a.unit_id UnitId
          from t_user_role a  
          inner join t_unit c on a.unit_id=c.unit_id  
          inner join t_user d on a.user_id=d.user_id  
          inner join t_role e on a.role_id=e.role_id  
          left join T_Def_App f  on e.def_app_id=f.def_app_id 
          where a.user_id='" + this.loggingSessionInfo.CurrentLoggingManager.User_Id + @"' 
          and e.def_app_id =case when ''='' then e.def_app_id else '' end  
          and  a.status = '1'
          ) as x on x.UnitId=t_u.dst_unit_id
         UNION ALL
         SELECT tu.dst_unit_id,tu.src_unit_id FROM T_Unit_Relation AS tu,CategoryInfo AS tup 
         WHERE tu.src_unit_id = tup.dst_unit_id and tu.status='1' 
          )
         select distinct *  from CategoryInfo where src_unit_id='" + unitId + "'";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                return true;
            }
            return false;
        }
        #endregion


        #region 获取单个类型的组织（譬如：门店，供应商，客户，渠道，代理商.....)
        /// <summary>
        /// 根据组织类型，获取集合
        /// </summary>
        /// <param name="type_code">类型号码：（门店，供应商，客户，总部，代理商.）</param>
        /// <returns></returns>
        public DataSet GetUnitInfoListByTypeCode(string type_code)
        {
            DataSet ds = new DataSet();
            string sql = GetSql("")
                + "where 1=1  and a.unit_code!=e.customer_code and a.unit_code!='ONLINE' b.type_code = '" + type_code + "' and a.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' and a.status = '1'";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 公共Sql
        private string GetSql(string sql)
        {
            sql = sql + "select a.unit_id Id "
                + " ,a.type_id TypeId "
                + " ,a.unit_code Code "
                + " ,a.unit_name Name "
                + " ,a.unit_name_en EnglishName  "
                + " ,a.unit_name_short ShortName "
                + " ,a.unit_email Email "
                + " ,a.unit_tel Telephone "
                + " ,a.unit_fax Fax "
                + " ,a.unit_address Address "
                + " ,a.unit_contact Contact "
                + " ,a.unit_postcode Postcode "
                + " ,a.unit_city_id CityId "
                + " ,a.unit_remark Remark "
                + " ,a.Status "
                + " ,a.unit_flag Flag "
                + " ,a.customer_level CustomerLevel "
                + " ,a.status_desc Status_desc "
                + " ,c.city1_name + c.city2_name + c.city3_name  as CityName "
                + " ,(select top 1 city_code from t_city where city_id=a.unit_city_id) CityCode "
                + " ,b.type_name TypeName "
                + " ,(select src_unit_id From T_Unit_Relation where dst_unit_id = a.unit_id and status='1') Parent_unit_id "
                + " ,(select y.unit_name From T_Unit_Relation x inner join t_unit y on(x.src_unit_id = y.unit_id) where x.dst_unit_id = a.unit_id and y.status='1' and x.status='1') Parent_Unit_Name"
                + " ,a.dimension "
                + " ,a.longitude "
                + " ,a.create_user_id "
                + " ,a.create_time "
                + " ,ua.user_name create_user_name "
                + " ,a.modify_user_id "
                + " ,a.modify_time "
                + " ,ub.user_name modify_user_name "
                + " ,a.imageURL "
                + " ,a.ftpImagerURL "
                + " ,a.webserversURL "
                + " ,a.weiXinId "
                + " ,a.dimensionalCodeURL "
                + " From t_unit a "
                + " inner join T_Type b "
                + " on(a.type_id = b.type_id) "
                + " left join T_City c "
                + " on(a.unit_city_id = c.city_id)";
            sql += " left join t_user ua on a.create_user_id=ua.user_id ";
            sql += " left join t_user ub on a.modify_user_id=ub.user_id ";
            sql += @" inner join cpos_ap..t_customer  e on a.customer_id=e.customer_id   "//去除初始化的那个门店和电商门店
          + @" ";

            return sql;
        }
        #endregion

        #region 查询
        public int SearchCount(Hashtable _ht, string UserID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            string sql = SearchPublicSql(_ht);
            sql = sql + " select @iCount; ";//
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql, ls.ToArray()));
        }
        public DataSet SearchList(Hashtable _ht, string UserID, string CustomerID)
        {

            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));

            DataSet ds = new DataSet();
            string sql = SearchPublicSql(_ht);
            sql = sql + " select a.unit_id Id"
                      + " ,a.type_id TypeId"
                      + " ,a.unit_code Code"
                      + " ,a.unit_name Name"
                      + " ,a.unit_name_en EnglishName"
                      + " ,a.unit_name_short ShortName "
                      + " ,a.unit_city_id CityId"
                      + " ,a.unit_address Address "
                      + " ,a.unit_contact Contact"
                      + " ,a.unit_tel Telephone "
                      + " ,a.unit_fax Fax "
                      + " ,a.unit_email Email "
                      + " ,a.unit_postcode Postcode "
                      + " ,a.unit_remark Remark"
                      + " ,a.Status Status "
                      + " ,a.unit_flag Flag"
                      + " ,a.customer_level CustomerLevel"
                      + " ,a.create_user_id "
                      + " ,a.create_time "
                      + " ,a.modify_user_id "
                      + " ,a.modify_time "
                      + " ,a.status_desc "
                      + " ,@iCount icount "
                      + " ,c.type_name TypeName "
                      + " ,d.city1_name + d.city2_name + d.city3_name CityName "
                      + " ,e.user_name create_user_name "
                      + " ,f.user_name modify_user_name "
                      + " ,b.row_no "
                      + " ,a.longitude "
                      + " ,a.dimension "
                      + " From  t_unit a "
                      + " inner join @TmpTable b "
                      + " on(a.unit_id = b.unit_id) "
                      + " left join t_type c "
                      + " on(a.type_id = c.type_id) "
                      + " left join T_City d "
                      + " on(a.unit_city_id = d.city_id) "
                      + " left join t_user e "
                      + " on(a.create_user_id = e.user_id) "
                      + " left join t_user f "
                      + " on(a.modify_user_id = f.user_id) "
                      + " where b.row_no >= '" + _ht["StartRow"] + "' "
                      + " and b.row_no <= '" + _ht["EndRow"] + "' "
                + " order by a.customer_level desc, a.type_id, a.unit_code, a.unit_name ;";
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());
            return ds;
        }
        /// <summary>
        /// 查询公共部分
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public string SearchPublicSql(Hashtable _ht)
        {
            PublicService pService = new PublicService();

            #region

            string sql = @"----在这里要把用户的权限能看到的数据加上
DECLARE @AllUnit NVARCHAR(200)

CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
 INSERT #UnitSET (UnitID)                  
   SELECT DISTINCT R.UnitID                   
   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  (@CustomerId,UR.unit_id,205)  R                  
   WHERE user_id=@UserID          ---根据账户的角色去查角色对应的  所有门店unit_id
";

            sql += "Declare @TmpTable Table "
                      + " (unit_id nvarchar(100) "
                      + " ,row_no int "
                      + " ); "
                      + " Declare @iCount int; "
                      + " insert into @TmpTable(unit_id,row_no) "
                      + " select x.unit_id ,x.rownum_ From ( select rownum_=row_number() over(order by a.unit_code),unit_id "
                      + @" from t_unit a inner join  #UnitSET R  on a.unit_id=R.unitID inner join cpos_ap..t_customer  c  on a.customer_id=c.customer_id where 1=1 "
            +  @" and a.unit_code!=c.customer_code and unit_code!='ONLINE'";
            sql = pService.GetLinkSql(sql, "a.unit_code", _ht["unit_code"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.customer_id", _ht["CustomerId"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.unit_name", _ht["unit_name"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.type_id", _ht["unit_type_id"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.unit_tel", _ht["unit_tel"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.unit_city_id", _ht["unit_city_id"].ToString(), "=");
            sql = pService.GetLinkSql(sql, "a.status", _ht["unit_status"].ToString(), "=");

            sql = sql + " ) x";

            sql = sql + " select @iCount = COUNT(*) From @TmpTable; ";
            sql = sql + " drop table #UnitSET ";//删除零时表
            #endregion

            return sql;
        }
        #endregion

        #region 根据用户获取该用户的所有门店
        public DataSet GetUnitListByUserId(string userId)
        {
            DataSet ds = new DataSet();
            string sql = " select distinct a.* "
                      + " , '' as unit_city_name "
                      + " , '' as unit_type_name "
                      + " , (select src_unit_id From T_Unit_Relation where dst_unit_id = a.unit_id and status='1') parent_unit_id "
                      + " from t_unit a"
                      + " inner join T_User_Role b "
                      + " on(a.unit_id = b.unit_id) "
                      + " where b.user_id= '" + userId + "' "
                      + " order by a.unit_code ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 单位保存
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetUnitInfo(UnitInfo unitInfo, out string strError, bool IsTran = true)
        {
            IDbTransaction tran = null;
            if (IsTran)
            {
                tran = this.SQLHelper.CreateTransaction();
            }
            using (tran)
            {
                try
                {
                    //2.1更新门店信息
                    if (!SetUnitUpdate(unitInfo, tran))
                    {
                        strError = "修改门店信息失败";
                        throw (new System.Exception(strError));
                    }

                    //2.2插入门店信息
                    if (!SetUnitInsert(unitInfo, tran))
                    {
                        strError = "插入门店信息失败";
                        throw (new System.Exception(strError));
                    }
                    //3处理上下级关系失败
                    if (!SetUnitRelation(unitInfo, tran))
                    {
                        strError = "处理上下级关系失败";
                        throw (new System.Exception(strError));
                    }
                    //4.
                    if (unitInfo.PropertyList != null)
                    {
                        if (!SetUnitPropertyDelete(unitInfo, tran))
                        {
                            strError = "删除单位属性失败";
                            throw (new System.Exception(strError));
                        }
                        foreach (UnitPropertyInfo unitPropertyInfo in unitInfo.PropertyList)
                        {
                            if (!SetUnitPropertyInfo(unitInfo, unitPropertyInfo, tran))
                            {
                                strError = "处理单位属性失败";
                                break;
                                throw (new System.Exception(strError));
                            }
                        }
                    }
                    if (IsTran) tran.Commit();
                    strError = "成功";
                    return true;
                }
                catch (Exception ex)
                {
                    if (IsTran) tran.Rollback();
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="unit_code"></param>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public int IsExistUnitCode(string unit_code, string unit_id)
        {
            string sql = "select count(*) From t_unit where 1=1 and customer_id = '" + CurrentUserInfo.ClientID + "' and unit_code = '" + unit_code + "'";

            if (!string.IsNullOrEmpty(unit_id))
            {
                sql = sql + " and unit_id != '" + unit_id + "' ";
            }

            int count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count;
        }

        /// <summary>
        /// 更新单位信息
        /// </summary>
        /// <param name="unitInfo"></param>
        private bool SetUnitUpdate(UnitInfo unitInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "update t_unit set type_id = '" + unitInfo.TypeId + "' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_code", unitInfo.Code);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_name", unitInfo.Name);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_name_en", unitInfo.EnglishName);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_name_short", unitInfo.ShortName);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_city_id", unitInfo.CityId);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_address", unitInfo.Address);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_contact", unitInfo.Contact);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_tel", unitInfo.Telephone);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_fax", unitInfo.Fax);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_email", unitInfo.Email);
            sql = pService.GetIsNotNullUpdateSql(sql, "if_flag", "0");
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_postcode", unitInfo.Postcode);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_remark", unitInfo.Remark);
            sql = pService.GetIsNotNullUpdateSql(sql, "Status", unitInfo.Status);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_flag", unitInfo.Flag);
            sql = pService.GetIsNotNullUpdateSql(sql, "customer_level", unitInfo.CustomerLevel.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", unitInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "Modify_Time", unitInfo.Modify_Time);
            sql = pService.GetIsNotNullUpdateSql(sql, "Status_Desc", unitInfo.Status_Desc);
            sql = pService.GetIsNotNullUpdateSql(sql, "longitude", unitInfo.longitude);
            sql = pService.GetIsNotNullUpdateSql(sql, "dimension", unitInfo.dimension);
            sql = pService.GetIsNotNullUpdateSql(sql, "imageURL", unitInfo.imageURL);
            sql = pService.GetIsNotNullUpdateSql(sql, "ftpImagerURL", unitInfo.ftpImagerURL);
            sql = pService.GetIsNotNullUpdateSql(sql, "webserversURL", unitInfo.webserversURL);
            sql = pService.GetIsNotNullUpdateSql(sql, "weiXinId", unitInfo.weiXinId);
            sql = pService.GetIsNotNullUpdateSql(sql, "dimensionalCodeURL", unitInfo.dimensionalCodeURL);
            sql = sql + " where unit_id = '" + unitInfo.Id + "' ;";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 插入门店信息
        /// </summary>
        /// <param name="unitInfo"></param>
        private bool SetUnitInsert(UnitInfo unitInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_unit (unit_id "
                      + " ,type_id "
                      + " ,unit_code "
                      + " ,unit_name "
                      + " ,unit_name_en "
                      + " ,unit_name_short "
                      + " ,unit_city_id "
                      + " ,unit_address "
                      + " ,unit_contact "
                      + " ,unit_tel "
                      + " ,unit_fax "
                      + " ,unit_email "
                      + " ,unit_postcode "
                      + " ,unit_remark "
                      + " ,Status "
                      + " ,unit_flag "
                      + " ,CUSTOMER_LEVEL "
                      + " ,create_user_id "
                      + " ,create_time "
                      + " ,modify_user_id "
                      + " ,modify_time "
                      + " ,status_desc "
                      + " ,customer_id "
                      + " ,longitude "
                      + " ,imageURL "
                      + " ,ftpImagerURL "
                      + " ,webserversURL "
                      + " ,weiXinId "
                      + " ,dimensionalCodeURL "
                      + " ,dimension) "

                      + " select a.* From ( "
                      + " select '" + unitInfo.Id + "' unit_id "
                      + ", '" + unitInfo.TypeId + "' type_id "
                      + ", '" + unitInfo.Code + "' unit_code "
                      + ", '" + unitInfo.Name + "' unit_name "
                      + ", '" + unitInfo.EnglishName + "' unit_name_en "
                      + ", '" + unitInfo.ShortName + "' unit_name_short "
                      + ", '" + unitInfo.CityId + "' unit_city_id "
                      + ", '" + unitInfo.Address + "' unit_address "
                      + ", '" + unitInfo.Contact + "' unit_contact "
                      + ", '" + unitInfo.Telephone + "' unit_tel "
                      + ", '" + unitInfo.Fax + "' unit_fax "
                      + ", '" + unitInfo.Email + "' unit_email "
                      + ", '" + unitInfo.Postcode + "' unit_postcode "
                      + ", '" + unitInfo.Remark + "' unit_remark "
                      + ", '1' Status "
                      + ", '" + unitInfo.Flag + "' unit_flag "
                      + ", '" + unitInfo.CustomerLevel + "' customer_level "
                      + ", '" + unitInfo.Create_User_Id + "' create_user_id "
                      + ", '" + unitInfo.Create_Time + "' create_time "
                      + ", '" + unitInfo.Modify_User_Id + "' modify_user_id "
                      + ", '" + unitInfo.Modify_Time + "' modify_time "
                      + ", '" + unitInfo.Status_Desc + "' status_desc "
                      + ", '" + unitInfo.customer_id + "' customer_id "
                      + ", '" + unitInfo.longitude + "' longitude "
                      + ", '" + unitInfo.imageURL + "' imageURL "
                      + ", '" + unitInfo.ftpImagerURL + "' ftpImagerURL "
                      + ", '" + unitInfo.webserversURL + "' webserversURL "
                      + ", '" + unitInfo.weiXinId + "' weiXinId "
                      + ", '" + unitInfo.dimensionalCodeURL + "' dimensionalCodeURL "
                      + ", '" + unitInfo.dimension + "' dimension "
                      + " ) a "
                      + " left join T_Unit b "
                      + " on(a.unit_id = b.unit_id) "
                      + " where b.unit_id is null ; ";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 更新单位上下级关系
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public bool SetUnitRelation(UnitInfo unitInfo, IDbTransaction pTran)
        {

            this.SQLHelper.ExecuteNonQuery(" delete from T_Unit_Relation where dst_unit_id='" + unitInfo.Id + "' ");


            string sql = "update t_unit_relation set status = '-1' where dst_unit_id = '" + unitInfo.Id + "' and src_unit_id != '-99';";
            sql = sql + "update t_unit_relation set status = '1' where src_unit_id = '" + unitInfo.Parent_Unit_Id + "' and dst_unit_id = '" + unitInfo.Id + " ;";
            sql = "insert into t_unit_relation(unit_relation_id,src_unit_id,dst_unit_id,[status]) "
                + "select x.* From ( "
                + "select REPLACE(NEWID(),'-','') unit_relation_id,'" + unitInfo.Parent_Unit_Id + "' src_unit_id,'" + unitInfo.Id + "' dst_unit_id,'1' [status] ) x "
                + " left join t_unit_relation y "
                + " on(x.src_unit_id = y.src_unit_id "
                + " and x.dst_unit_id = y.dst_unit_id) "
                + " where y.unit_relation_id is null; "
                ;

            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 删除单位属性
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitPropertyDelete(UnitInfo unitInfo, IDbTransaction pTran)
        {
            string sql = "update t_unit_property set [status] = '-1' ,modify_time = '" + unitInfo.Create_Time + "' ,modify_user_id = '" + unitInfo.Create_User_Id + "' where unit_id = '" + unitInfo.Id + "'";
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 修改或者插入单位属性
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="unitPropertyInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitPropertyInfo(UnitInfo unitInfo, UnitPropertyInfo unitPropertyInfo, IDbTransaction pTran)
        {
            string sql = "update t_unit_property "
                        + " set [status] = '1' "
                        + " ,property_value = '" + unitPropertyInfo.PropertyDetailCode + "' "
                        + " ,modify_time = '" + unitPropertyInfo.Create_Time + "'"
                        + " ,modify_user_id = '" + unitPropertyInfo.Create_User_id + "'"
                        + " where unit_id = '" + unitInfo.Id + "' and unit_property_id = '" + unitPropertyInfo.Id + "' and property_id = '" + unitPropertyInfo.PropertyCodeId + "' ;";
            sql = sql + "insert into T_Unit_Property "
                        + " (unit_property_id "
                        + " ,unit_id "
                        + " ,property_id "
                        + " ,property_value "
                        + " ,status "
                        + " ,create_user_id "
                        + " ,create_time "
                        + " ,modify_user_id "
                        + " ,modify_time "
                        + " ) "
                        + "  SELECT P.unit_property_id "
                        + " ,P.unit_id "
                        + " ,P.property_id "
                        + " ,P.property_value "
                        + " ,P.status "
                        + " ,P.create_user_id "
                        + " ,P.create_time "
                        + " ,P.modify_user_id "
                        + " ,P.modify_time "
                        + " FROM ( "
                        + " SELECT "
                        + " '" + unitPropertyInfo.Id + "' unit_property_id "
                        + "   ,'" + unitPropertyInfo.UnitId + "' unit_id "
                        + "   ,'" + unitPropertyInfo.PropertyCodeId + "' property_id "
                        + "   ,'" + unitPropertyInfo.PropertyDetailCode + "' property_value "
                        + "   ,'1' status "
                        + "   ,'" + unitPropertyInfo.Create_User_id + "' create_user_id "
                        + "   ,'" + unitPropertyInfo.Create_Time + "' create_time "
                        + "   ,'" + unitPropertyInfo.Create_User_id + "' modify_user_id "
                        + "   ,'" + unitPropertyInfo.Create_Time + "' modify_time "
                        + "  ) P "
                        + " left join t_unit_property  b "
                        + " on(P.unit_property_id = b.unit_property_id) "
                        + " left join t_unit_property c "
                        + " on(P.unit_id = c.unit_id "
                        + " and P.property_id = c.property_id and c.status='0') "
                        + " where b.unit_property_id is null "
                        + " and c.unit_id is null; "
                        ;
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        #endregion

        #region 删除门店
        public bool SetUnitTableStatus(UnitInfo unitInfo)
        {
            string sql = " update t_unit "
                      + " set [status] = '" + unitInfo.Status + "' "
                      + " ,Status_Desc = '" + unitInfo.Status_Desc + "'"
                      + " ,Modify_Time = '" + unitInfo.Modify_Time + "' "
                      + " ,Modify_User_Id = '" + unitInfo.Modify_User_Id + "' "
                      + " ,if_flag = '0' "
                      + " where unit_id = '" + unitInfo.Id + "' ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region 获取总部门店信息
        /// <summary>
        /// 获取总部门店信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetHeadStoreId(string customerId)
        {
            string sql = "SELECT TOP 1 * FROM dbo.T_Unit WHERE type_id = '2F35F85CF7FF4DF087188A7FB05DED1D' AND customer_id = '" + customerId + "'";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取门店信息 2014-10-16
        /// <summary>
        /// 获取总部门店信息
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetAllStoreId(string customerId)
        {
            string sql = "SELECT unit_name,unit_id FROM dbo.T_Unit WHERE type_id = 'EB58F1B053694283B2B7610C9AAD2742' AND customer_id = '" + customerId + "'";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion


        public DataSet GetPosOrder(string unitId, int top)
        {
            DataSet ds = new DataSet();
            string sql = "";
            sql += "select top " + top + " * from vwVipPosOrder where salesUnitId='" + unitId + "' or purchaseUnitId='" + unitId + "'";
            sql += " order by createTime desc";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #region 获取在线商城门店信息
        public DataSet GetRecentlyUsedStore(string customerid, string userid, string openid)
        {
            string sql = "proc_GetRecentlyUsedStore";
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter(){ ParameterName="@customerid", DbType= DbType.String, Value=customerid},
                new SqlParameter(){ ParameterName="@userid", DbType= DbType.String,Value=userid},
                new SqlParameter(){ ParameterName="@openid",DbType= DbType.String,Value=openid}
            };
            var ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql, list.ToArray());
            return ds;
        }
        #endregion

        #region 俄丽亚
        public DataSet FuzzyQueryStores(string pLike, string pCustomerID, string pStoreID, string pDistrictID, bool pIncludeHQ, int pageindex, int pagesize)
        {
            StringBuilder temp = new StringBuilder();
            string type_id = "EB58F1B053694283B2B7610C9AAD2742"; //只取门店的
            if (!string.IsNullOrEmpty(pCustomerID))
                temp.AppendFormat(" and a.customer_id='{0}'", pCustomerID);
            if (!string.IsNullOrEmpty(pLike))
                temp.AppendFormat(" and (a.unit_name like '%{0}%' or a.unit_address like '%{0}%')", pLike);
            if (!string.IsNullOrEmpty(pStoreID))
                temp.AppendFormat(" and a.unit_id='{0}'", pStoreID);
            if (!string.IsNullOrEmpty(pDistrictID))
                temp.AppendFormat(" and a.unit_city_id='{0}'", pDistrictID);
            if (!pIncludeHQ)
                temp.AppendFormat(" and not exists(select 1 from t_type where type_id=a.type_id and type_name='总部')");
            if (!string.IsNullOrEmpty(type_id))  //只取门店数据
            {
                temp.AppendFormat(" or (a.type_id='{0}' and a.customer_id='{0}')", type_id);
            }
            StringBuilder sql = new StringBuilder(string.Format(@"select * into #temp from 
                                           (select row_number()over(order by create_time) _row, a.*,b.unitPT,b.unitRD,c.type_code from t_unit a
                                                left join VwUnitProperty b on a.unit_id=b.unitid
                                                join t_type c on a.type_id=c.type_id
                                                where 1=1 and a.Status=1 {2})
                                           t where t._row>{0}*{1} and t._row<= ({0}+1)*{1}", pageindex, pagesize, temp.ToString()));
            sql.AppendLine("select * from #temp");
            sql.AppendLine("select * from ObjectImages where IsDelete=0 and exists(select 1 from #temp where unit_id=ObjectImages.ObjectId)");
            sql.AppendLine("drop Table #temp");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        /// <summary>
        /// 搜索门店 全部 Add by Alex Tian 2014-04-14
        /// </summary>
        /// <param name="NameLike"></param>
        /// <param name="CityCode"></param>
        /// <param name="Position"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="StoreID"></param>
        /// <param name="IncludeHQ"></param>
        /// <returns></returns>
        public DataSet FuzzyQueryStoresALL(string NameLike, string CityCode, string Position, int pageindex, int pagesize, string StoreID, bool IncludeHQ, string pCustomerID)
        {
            StringBuilder temp = new StringBuilder();
            string type_id = "EB58F1B053694283B2B7610C9AAD2742"; //只取门店的
            //模糊查询用参数
            if (!string.IsNullOrEmpty(NameLike))
                temp.AppendFormat(" and (a.unit_name like '%{0}%' or a.unit_address like '%{0}%')", NameLike);
            //门店ID
            if (!string.IsNullOrEmpty(StoreID))
                temp.AppendFormat(" and a.unit_id='{0}'", StoreID);
            //总店/门店
            if (!IncludeHQ)
                temp.AppendFormat(" and not exists(select 1 from t_type where type_id=a.type_id and type_name='总部')");
            //客户ID
            if (!string.IsNullOrEmpty(pCustomerID))
                temp.AppendFormat(" and a.customer_id='{0}'", pCustomerID);
            if (!string.IsNullOrEmpty(type_id))  //只取门店数据
            {
                temp.AppendFormat(" and a.type_id='{0}'", type_id);
            }
            StringBuilder sql = new StringBuilder(string.Format(@"select * into #temp from 
                                           (select row_number()over(order by create_time) _row, a.*,b.unitPT,b.unitRD,c.type_code from t_unit a
                                                join VwUnitProperty b on a.unit_id=b.unitid
                                                join t_type c on a.type_id=c.type_id
                                                where 1=1 and a.Status=1 {2})
                                           t where t._row>{0}*{1} and t._row<= ({0}+1)*{1}", pageindex, pagesize, temp.ToString()));
            sql.AppendLine("select * from #temp");
            sql.AppendLine("select * from ObjectImages where IsDelete=0 and exists(select 1 from #temp where unit_id=ObjectImages.ObjectId) order by ObjectId, ObjectImages.DisplayIndex");
            sql.AppendLine("drop Table #temp");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 搜索门店 附近 Add by Alex Tian 2014-04-15
        /// </summary>
        /// <param name="NameLike"></param>
        /// <param name="CityCode"></param>
        /// <param name="Position"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="StoreID"></param>
        /// <param name="IncludeHQ"></param>
        /// <returns></returns>
        public DataSet FuzzyQueryStoresNearby(string NameLike, string CityCode, string Position, int pageindex, int pagesize, string StoreID, bool IncludeHQ, string pCustomerID, double RangeAccessoriesStores)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            string type_id = "EB58F1B053694283B2B7610C9AAD2742"; //只取门店的
            string strdistance = string.Empty;
            string strDistance = string.Empty;
            //模糊查询用参数
            if (!string.IsNullOrEmpty(NameLike))
                temp.AppendFormat(" and (a.unit_name like '%{0}%' or a.unit_address like '%{0}%')", NameLike);
            //门店ID
            if (!string.IsNullOrEmpty(StoreID))
                temp.AppendFormat(" and a.unit_id='{0}'", StoreID);
            //总店/门店
            if (!IncludeHQ)
                temp.AppendFormat(" and not exists(select 1 from t_type where type_id=a.type_id and type_name='总部')");
            //客户ID
            if (!string.IsNullOrEmpty(pCustomerID))
                temp.AppendFormat(" and a.customer_id='{0}'", pCustomerID);
            //城市代码等于-00时，搜索附近
            if (CityCode == "-00")
            {
                if (!string.IsNullOrEmpty(Position))//坐标不为''
                {
                    string[] pos = Position.Split(',');

                    string lng = pos[0].Trim();  //经度
                    string lat = pos[1].Trim();  //纬度
                    if (lng == "undefined" || lat == "undefined")
                    {
                        strdistance = (string.Format(" ,(0) as Distance ", lng, lat));
                        //try
                        //{
                        //    throw new Exception("无法获取到经纬度信息！");
                        //}
                        //catch { }
                    }
                    else
                        strdistance = (string.Format(" ,(GEOGRAPHY::STGeomFromText('POINT('+isnull(a.longitude,0)+' '+isnull(a.dimension,0)+')',4326).STDistance(GEOGRAPHY::STGeomFromText('POINT({0} {1})',4326))) as Distance ", lng, lat));
                    temp.AppendFormat(" and LTRIM(a.longitude)<>'' and LTRIM(a.dimension)<>'' ");
                }
            }
            if (!string.IsNullOrEmpty(type_id))  //只取门店数据
            {
                temp.AppendFormat(" and a.type_id='{0}'", type_id);
            }
            if (RangeAccessoriesStores > 0)
            {
                strDistance = (" and Distance between 0 and " + RangeAccessoriesStores * 1000 + "");
            }
            sql = new StringBuilder(string.Format("select * into #temp from (select  a.*,b.unitPT,b.unitRD,c.type_code " + strdistance + " from t_unit a  join VwUnitProperty b on a.unit_id=b.unitid join t_type c on a.type_id=c.type_id where 1=1 and a.Status=1 {0}) t where 1=1 " + strDistance + " ", temp.ToString()));

            //查询附件的时候 按距离排序
            sql.AppendFormat(" select * from  (select row_number()over( order by Distance) _row, * from #temp) t where t._row>{0}*{1} and t._row<= ({0}+1)*{1} ", pageindex, pagesize);
            sql.AppendLine("select * from ObjectImages where IsDelete=0 and exists( select 1 from #temp where unit_id=ObjectImages.ObjectId) order by ObjectId, ObjectImages.DisplayIndex");
            sql.AppendLine(" drop Table #temp");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 搜索门店 城市 Add by Alex Tian 2014-04-22
        /// </summary>
        /// <param name="NameLike"></param>
        /// <param name="CityCode"></param>
        /// <param name="Position"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="StoreID"></param>
        /// <param name="IncludeHQ"></param>
        /// <returns></returns>
        public DataSet FuzzyQueryStoresCity(string NameLike, string CityCode, string Position, int pageindex, int pagesize, string StoreID, bool IncludeHQ, string pCustomerID, double RangeAccessoriesStores)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            string type_id = "EB58F1B053694283B2B7610C9AAD2742"; //只取门店的
            string strdistance = string.Empty;
            //模糊查询用参数
            if (!string.IsNullOrEmpty(NameLike))
                temp.AppendFormat(" and (a.unit_name like '%{0}%' or a.unit_address like '%{0}%')", NameLike);
            //门店ID
            if (!string.IsNullOrEmpty(StoreID))
                temp.AppendFormat(" and a.unit_id='{0}'", StoreID);
            //总店/门店
            if (!IncludeHQ)
                temp.AppendFormat(" and not exists(select 1 from t_type where type_id=a.type_id and type_name='总部')");
            //客户ID
            if (!string.IsNullOrEmpty(pCustomerID))
                temp.AppendFormat(" and a.customer_id='{0}'", pCustomerID);
            if (!string.IsNullOrEmpty(CityCode))
            {
                strdistance = " ,0 as Distance ";
                temp.AppendFormat(" and  a.unit_city_id in (select unit_city_id from t_unit where  exists(select 1 from T_City where city_id=t_unit.unit_city_id and city_code like '{0}%'))", CityCode);
            }
            if (!string.IsNullOrEmpty(type_id))  //只取门店数据
            {
                temp.AppendFormat(" and a.type_id='{0}'", type_id);
            }
            sql = new StringBuilder(string.Format("select * into #temp from (select row_number()over(order by create_time) _row, a.*,b.unitPT,b.unitRD,c.type_code " + strdistance + " from t_unit a  join VwUnitProperty b on a.unit_id=b.unitid join t_type c on a.type_id=c.type_id where 1=1 and a.Status=1 {2}) t where t._row>{0}*{1} and t._row<= ({0}+1)*{1} order by 1", pageindex, pagesize, temp.ToString()));
            sql.AppendLine(" select * from #temp");
            sql.AppendLine("select * from ObjectImages where IsDelete=0 and exists( select 1 from #temp where unit_id=ObjectImages.ObjectId) order by ObjectId,ObjectImages.DisplayIndex ");
            sql.AppendLine(" drop Table #temp");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }

        #endregion

        #region 获取客户行业类型
        public DataSet GetCustomerDataDeploy(string pCustomerID)
        {
            string sql = string.Format(@"select b.DataDeployName,a.customer_id From cpos_ap..t_customer_connect a
inner join cpos_ap..TDataDeploy b
on(a.db_name = b.db_name)
where b.IsDelete = '0' and customer_id='{0}'", pCustomerID);
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
