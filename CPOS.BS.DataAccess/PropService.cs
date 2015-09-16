using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.CPOS.BS.Entity.Pos;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{
    public class PropService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public PropService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region

        public DataSet GetPropListFirstByDomain(string propDomain)
        {
            DataSet ds = new DataSet();
            string sql = "select Prop_Id "
                          + " ,Prop_Code "
                          + " ,Prop_Name "
                          + " ,Prop_Eng_Name "
                          + " ,Prop_Type "
                          + " ,Parent_Prop_id "
                          + " ,Prop_Level "
                          + " ,Prop_Domain "
                          + " ,Prop_Input_Flag "
                          + " ,a.prop_max_lenth "
                          + " ,Prop_Default_Value "
                          + " ,status "
                          + " ,Display_Index "
                          + " ,Create_User_Id "
                          + " ,Create_Time "
                          + " ,Modify_User_Id "
                          + " ,Modify_Time "
                          + " ,case when status = '1' then '正常' else '删除' end Prop_Status_Desc "
                          + " ,(select user_name From t_user x where x.user_id = a.create_user_id) Create_User_Name "
                          + " ,(select user_name From t_user x where x.user_id = a.modify_user_id) Modify_User_Name "
                          + " From t_prop a "

                          + " where 1=1 "
                          + " and a.status = '1'  and a.Prop_Type = '1' "
                          + " and a.Prop_Domain = '" + propDomain + "' "
                          + " order by a.display_index ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetPropListByParentId(string propDomain, string parentPropId)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (parentPropId.Equals("F8823C2EBACF4965BA134D3B10BD0B9F"))
            {
                sql = "select Prop_Id "
                              + " ,Prop_Code "
                              + " ,Prop_Name "
                              + " ,Prop_Eng_Name "
                              + " ,Prop_Type "
                              + " ,Parent_Prop_id "
                              + " ,Prop_Level "
                              + " ,Prop_Domain "
                              + " ,Prop_Input_Flag "
                              + " ,a.prop_max_lenth Prop_Max_Length"
                              + " ,Prop_Default_Value "
                              + " ,status "
                              + " ,Display_Index "
                              + " ,Create_User_Id "
                              + " ,Create_Time "
                              + " ,Modify_User_Id "
                              + " ,Modify_Time "
                              + " ,case when status = '1' then '正常' else '删除' end Prop_Status_Desc "
                              + " ,(select user_name From t_user x where x.user_id = a.create_user_id) Create_User_Name "
                              + " ,(select user_name From t_user x where x.user_id = a.modify_user_id) Modify_User_Name "
                              + " From t_prop a inner join BrandDetail b on(a.prop_id = b.BrandId and b.customerId='" + this.CurrentUserInfo.CurrentUser.customer_id + "')"

                              + " where 1=1 "
                              + " and a.status = '1' "
                              + " and a.Prop_Domain = '" + propDomain + "' and a.Parent_Prop_id = '" + parentPropId + "'"
                              + " order by a.display_index ";
            }
            else
            {
                sql = "select Prop_Id "
                              + " ,Prop_Code "
                              + " ,Prop_Name "
                              + " ,Prop_Eng_Name "
                              + " ,Prop_Type "
                              + " ,Parent_Prop_id "
                              + " ,Prop_Level "
                              + " ,Prop_Domain "
                              + " ,Prop_Input_Flag "
                              + " ,a.prop_max_lenth Prop_Max_Length"
                              + " ,Prop_Default_Value "
                              + " ,status "
                              + " ,Display_Index "
                              + " ,Create_User_Id "
                              + " ,Create_Time "
                              + " ,Modify_User_Id "
                              + " ,Modify_Time "
                              + " ,case when status = '1' then '正常' else '删除' end Prop_Status_Desc "
                              + " ,(select user_name From t_user x where x.user_id = a.create_user_id) Create_User_Name "
                              + " ,(select user_name From t_user x where x.user_id = a.modify_user_id) Modify_User_Name "
                              + " From t_prop a "

                              + " where 1=1 "
                              + " and a.status = '1' "
                              + " and a.Prop_Domain = '" + propDomain + "' and a.Parent_Prop_id = '" + parentPropId + "'"
                              + " order by a.display_index ";
            }
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetPropInfoById(string propId)
        {
            DataSet ds = new DataSet();
            string sql = "select Prop_Id "
                          + " ,Prop_Code "
                          + " ,Prop_Name "
                          + " ,Prop_Eng_Name "
                          + " ,Prop_Type "
                          + " ,Parent_Prop_id "
                          + " ,Parent_Prop_Name=(select top 1 prop_name from t_prop where prop_id=a.parent_prop_id) "
                          + " ,Prop_Level "
                          + " ,Prop_Domain "
                          + " ,Prop_Input_Flag "
                          + " ,a.prop_max_lenth Prop_Max_Length "
                          + " ,Prop_Default_Value "
                          + " ,status "
                          + " ,Display_Index "
                          + " ,Create_User_Id "
                          + " ,Create_Time "
                          + " ,Modify_User_Id "
                          + " ,Modify_Time "
                          + " ,case when status = '1' then '正常' else '删除' end Prop_Status_Desc "
                          + " ,(select user_name From t_user x where x.user_id = a.create_user_id) Create_User_Name "
                          + " ,(select user_name From t_user x where x.user_id = a.modify_user_id) Modify_User_Name "
                          + " From t_prop a "

                          + " where 1=1 "
                //+ " and a.status = '1'  and a.Prop_Type = '1' "
                          + " and a.Prop_id = '" + propId + "'"
                          + " order by a.display_index ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 后台Web获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebPropCount(PropInfo entity)
        {
            string sql = GetWebPropSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebProp(PropInfo entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebPropSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebPropSql(PropInfo entity)
        {
            var orderBy = "a.parent_prop_id, a.display_index asc";
            if (entity.OrderBy != null && entity.OrderBy.Length > 0)
            {
                orderBy = entity.OrderBy;
            }
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by " + orderBy + ") ";
            sql += " ,b.User_Name CreateByName ";
            sql += " ,c.Prop_Name Parent_Prop_Name ";
            sql += " ,(case when a.Prop_Type='1' then '属性组' when a.Prop_Type='2' then '属性' when a.Prop_Type='3' then '属性明细' else '' end) Prop_Type_Name ";
            sql += " into #tmp ";
            sql += " from [t_prop] a ";
            sql += " left join [t_user] b on a.create_user_id=b.user_id ";
            sql += " left join [t_prop] c on a.parent_prop_id=c.prop_id ";
            sql += " left join [T_Sku_Property] s on a.prop_id=s.prop_id and s.status='1' ";
            sql += " where a.status='1' ";
            if (entity.Prop_Name != null && entity.Prop_Name.Trim().Length > 0)
            {
                sql += " and a.Prop_Name like '%" + entity.Prop_Name.Trim() + "%' ";
            }
            if (entity.Prop_Code != null && entity.Prop_Code.Trim().Length > 0)
            {
                sql += " and a.Prop_Code like '%" + entity.Prop_Code.Trim() + "%' ";
            }
            if (entity.Prop_Type != null && entity.Prop_Type.Trim().Length > 0)
            {
                sql += " and a.Prop_Type = '" + entity.Prop_Type.Trim() + "' ";
            }

            //jifeng.cao20140220
            if (entity.CustomerId != null && entity.CustomerId.Trim().Length > 0)
            {
                sql += " and s.CustomerId = '" + entity.CustomerId.Trim() + "' ";
            }

            if (entity.Prop_Domain != null && entity.Prop_Domain.Trim().Length > 0)
            {
                //////Jermyn20131121///////////////////////////////////////////////////////////////////////////
                //if (entity.Prop_Domain.Equals("SKU") && !entity.Parent_Prop_id.Equals("-99"))
                //{
                //    sql += " and (a.Prop_Domain = 'ITEM' or a.Prop_Domain='SKU') ";
                //}
                //else
                //{
                sql += " and a.Prop_Domain = '" + entity.Prop_Domain.Trim() + "' ";
                //}
                /////////////////////////////////////////////////////////////////////////////////
            }

            if (entity.Parent_Prop_id != null)
            {
                if (entity.Prop_Domain != null && entity.Prop_Domain.Trim().ToUpper() == "SKU"&&entity.Parent_Prop_id=="-88")
                {
                    sql += " and a.Prop_Type=2 ";  
                }
                else
                {
                    sql += " and isnull(a.Parent_Prop_id,'-99') = '" + entity.Parent_Prop_id.Trim() + "' ";
                }                
            }
            //sql += " order by a.[Prop_Name] asc ";
            return sql;
        }
        #endregion

        #region CretaeIndex
        /// <summary>
        /// 根据父ID生成序号
        /// </summary>
        /// <param name="partentPropID"></param>
        /// <returns></returns>
        public int CreateIndex(string partentPropID, string propID)
        {
            StringBuilder strb = new StringBuilder();
            if (string.IsNullOrEmpty(propID))
            {
                strb.AppendFormat(@"
            declare @index int
            set @index=0
            select  top 1 @index=display_index from  t_prop 
            where prop_domain='SKU' and parent_prop_id='{0}' and status='1'
            order by display_index desc 
            if @index<=0
            begin
            select 1
            end
            else
            begin
            select @index+1 
            end
             ", partentPropID);
            }
            else
            {
                strb.AppendFormat(@"
                declare @partentPropID nvarchar(50)
                select @partentPropID=parent_prop_id from t_prop  where prop_id='{0}'
                if @partentPropID='{1}'
                begin
                  select display_index from t_prop  where prop_id='{0}'
                end
                else

                begin                    
                declare @index int
                set @index=0
                select  top 1 @index=display_index from  t_prop where prop_domain='SKU' and parent_prop_id='{1}' and status='1'
                order by display_index desc 
                if @index<=0
                begin
                select 1
                end
                else
                begin
                select @index+1 
                end 
                end
                  ", propID, partentPropID);

            }
            object obj = this.SQLHelper.ExecuteScalar(strb.ToString());
            return (int)obj;
        }
        #endregion

        #region CheckSkuLast
        /// <summary>
        /// 判断SKU最高长度
        /// </summary>
        /// <param name="partentID"></param>
        /// <returns></returns>
        public DataSet CheckSkuLast(string partentID)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"
             select count(1)  from  t_prop  as a
             left join t_prop as b on a.prop_id=b.parent_prop_id 
             left join [T_Sku_Property] s on b.prop_id=s.prop_id 
             where  a.parent_prop_id='-99' and  a.prop_domain='SKU'  and a.status='1' and b.status='1' and b.prop_domain='SKU'
             and s.CustomerId='{1}' 
             and a.prop_id='{0}'
             ", partentID,this.CurrentUserInfo.ClientID);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;

        }
        #endregion


        #region SaveProp
        /// <summary>
        /// SaveProp
        /// </summary>
        /// <returns></returns>
        public bool SaveProp(PropInfo propInfo, ref string error)
        {
            if (CheckProp(propInfo.Prop_Id))
            {
                UpdateProp(propInfo);
                return true;
            }
            else
            {
                if (CheckProp(propInfo))//判断在同一个父类下面，是不是有这个属性代码了。
                {
                    error = "属性代码已存在";
                    return false;
                }
                AddProp(propInfo);
                return true;
            }
        }
        #endregion

        #region AddProp
        /// <summary>
        /// AddProp
        /// </summary>
        /// <returns></returns>
        public bool AddProp(PropInfo propInfo)
        {
            string sql = "insert into t_prop  ";
            sql += " (prop_id, prop_code, prop_name, prop_eng_name, prop_type, parent_prop_id, prop_level, prop_domain, prop_input_flag, prop_max_lenth, prop_default_value, status, display_index, create_user_id, create_time, modify_user_id, modify_time) ";
            sql += " values ('" + propInfo.Prop_Id + "' ";
            sql += " ,'" + propInfo.Prop_Code + "' ";
            sql += " ,'" + propInfo.Prop_Name + "' ";
            sql += " ,'" + propInfo.Prop_Eng_Name + "' ";
            sql += " ,'" + propInfo.Prop_Type + "' ";
            sql += " ,'" + propInfo.Parent_Prop_id + "' ";
            sql += " ,'" + propInfo.Prop_Level + "' ";
            sql += " ,'" + propInfo.Prop_Domain + "' ";
            sql += " ,'" + propInfo.Prop_Input_Flag + "' ";
            sql += " ,'" + propInfo.Prop_Max_Length + "' ";
            sql += " ,'" + propInfo.Prop_Default_Value + "' ";
            sql += " ,'" + propInfo.Prop_Status + "' ";
            sql += " ,'" + propInfo.Display_Index + "' ";
            sql += " ,'" + propInfo.Create_User_Id + "' ";
            sql += " ,'" + propInfo.Create_Time + "' ";
            sql += " ,'" + propInfo.Modify_User_Id + "' ";
            sql += " ,'" + propInfo.Modify_Time + "' ";
            sql += " ) ";

            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region UpdateProp
        /// <summary>
        /// UpdateProp
        /// </summary>
        /// <returns></returns>
        public bool UpdateProp(PropInfo propInfo)
        {
            string sql = "update t_prop set ";
            sql += " prop_name='" + propInfo.Prop_Name + "' ";
            sql += " ,prop_code='" + propInfo.Prop_Code + "' ";
            sql += " ,prop_eng_name='" + propInfo.Prop_Eng_Name + "' ";
            sql += " ,prop_type='" + propInfo.Prop_Type + "' ";
            sql += " ,parent_prop_id='" + propInfo.Parent_Prop_id + "' ";
            sql += " ,prop_level='" + propInfo.Prop_Level + "' ";
            sql += " ,prop_domain='" + propInfo.Prop_Domain + "' ";
            sql += " ,prop_input_flag='" + propInfo.Prop_Input_Flag + "' ";
            sql += " ,prop_max_lenth='" + propInfo.Prop_Max_Length + "' ";
            sql += " ,prop_default_value='" + propInfo.Prop_Default_Value + "' ";
            sql += " ,status='" + propInfo.Prop_Status + "' ";
            sql += " ,display_index='" + propInfo.Display_Index + "' ";
            sql += " ,modify_user_id='" + propInfo.Modify_User_Id + "' ";
            sql += " ,modify_time='" + propInfo.Modify_Time + "' ";
            sql += " where prop_id='" + propInfo.Prop_Id + "'";

            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region CheckProp
        /// <summary>
        /// CheckProp
        /// </summary>
        /// <returns></returns>
        public bool CheckProp(PropInfo propInfo)
        {
            if (CheckProp(propInfo.Prop_Id))//这个是判断数据库里是否有这条记录
            {
                string sql = "select count(*) from t_prop a inner join [T_Sku_Property] b on a.prop_id=b.prop_id ";
                sql += " where a.status='1' and prop_code='" + propInfo.Prop_Code + "'";
                sql += " and parent_prop_id='" + propInfo.Parent_Prop_id + "'";
                sql += " and a.prop_id<>'" + propInfo.Prop_Id + "'";
                sql += " and b.CustomerID='" + loggingSessionInfo.ClientID + "'";//根据商户判断了
                var obj = this.SQLHelper.ExecuteScalar(sql);
                if (obj == DBNull.Value) return false;
                var count = Convert.ToInt32(obj);
                return count > 0 ? true : false;
            }
            else
            {
                string sql = "select count(*) from t_prop  a inner join [T_Sku_Property] b on a.prop_id=b.prop_id ";
                sql += " where a.status='1' and prop_code='" + propInfo.Prop_Code + "'";
                sql += " and parent_prop_id='" + propInfo.Parent_Prop_id + "'";
                sql += " and b.CustomerID='" + loggingSessionInfo.ClientID + "'";//根据商户判断了
                var obj = this.SQLHelper.ExecuteScalar(sql);
                if (obj == DBNull.Value) return false;
                var count = Convert.ToInt32(obj);
                return count > 0 ? true : false;
            }
        }
        public bool CheckProp(string propId)
        {
            string sql = "select count(*) from t_prop ";
            sql += " where prop_id='" + propId + "'";
            var obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj == DBNull.Value) return false;
            var count = Convert.ToInt32(obj);
            return count > 0 ? true : false;
        }
        #endregion

        #region DeleteProp
        /// <summary>
        /// DeleteProp
        /// </summary>
        /// <returns></returns>
        public bool DeleteProp(PropInfo propInfo)
        {
            string sql = "update t_prop set ";
            sql += " status='-1' ";
            sql += " ,modify_user_id='" + propInfo.Modify_User_Id + "' ";
            sql += " ,modify_time='" + propInfo.Modify_Time + "' ";
            sql += " where prop_id='" + propInfo.Prop_Id + "'";
          

            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }

        public bool DeletePropByIds(string propIds, PropInfo propInfo)
        {
            string sql = "update t_prop set ";
            sql += " status='-1' ";
            sql += " ,modify_user_id='" + propInfo.Modify_User_Id + "' ";
            sql += " ,modify_time='" + propInfo.Modify_Time + "' ";
            sql += " where parent_prop_id='" + propInfo.Prop_Id + "'";
            sql += " and prop_id not in (" + propIds + ") ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region 获取web业务系统的主界面图标 Jermyn20131202
        public string GetWebLogo()
        {
            string sql = "SELECT b.property_value FROM t_prop a "
                    + " INNER JOIN dbo.T_Unit_Property b ON(a.prop_id = b.property_id) "
                    + " INNER JOIN dbo.t_unit c ON(b.unit_id = c.unit_id) "
                    + " WHERE a.prop_domain = 'unit' "
                    + " AND a.prop_code = 'webLogo' "
                    + " AND a.status = '1' "
                    + " AND b.status = '1' "
                    + " AND c.Status = '1' "
                    + " AND c.type_id = '2F35F85CF7FF4DF087188A7FB05DED1D' "
                    + " AND c.customer_id = '" + this.CurrentUserInfo.CurrentUser.customer_id + "'";
            string strLogo = Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
            return strLogo;
        }
        #endregion

      
    }
}
