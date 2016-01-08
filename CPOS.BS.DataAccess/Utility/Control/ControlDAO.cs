/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2012/12/5 13:56:28
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.DAL
{
    /// <summary>
    /// 表OperationLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ControlDAO : BaseCPOSDAO
    {           
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ControlDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        
        }
        #endregion               

        
        #region GetOptionsByClientID
        public DataSet GetOptionsByClientID(string pOptionType,string customer_id)
        {
          
            //原来的，有重复数据
            string pSql = string.Format(@"declare @cnt int
select @cnt=count(*)
            from Options 
            where  IsDelete=0 
                    and OptionName='YES/NO' 
                     and CustomerID='e703dbedadd943abacf864531decdac1'
               
   if(@cnt!=0)
      select OptionName,OptionValue,OptionText
            from Options 
            where  IsDelete=0 
                and OptionName='{0}'   and CustomerID='{1}'
            order by Sequence,OptionValue
     else
		     select OptionName,OptionValue,OptionText
            from Options 
            where  IsDelete=0 
              and OptionName='{0}' 
            order by Sequence,OptionValue", pOptionType, customer_id);
            //   
//            string pSql = string.Format(@"select distinct OptionValue,OptionText,OptionName
//                        from Options 
//                        where  IsDelete=0 
//                                and OptionName='{0}'  
//                       ", pOptionType);//order by Sequence,OptionValue
            return GetAll(pSql);
        }
        #endregion

        #region GetLEventsType
        public DataSet GetLEventsType()
        {
            //为什么取Lower的
            string pSql = string.Format(@"select Lower(EventTypeID)as EventTypeID,Title from LEventsType where ClientID='{0}'
                            and IsDelete=0 order by CreateTime ", CurrentUserInfo.ClientID);
            return GetAll(pSql);
        }
        public DataSet GetLEventsType2()
        {
            //为什么取Lower的
            string pSql = string.Format(@"select * from LEventsType where ClientID='{0}'
                            and IsDelete=0 order by CreateTime ", CurrentUserInfo.ClientID);
            return GetAll(pSql);
        }
        #endregion

        #region GetMobileModule
        public DataSet GetMobileModule()
        {
            string pSql = string.Format(@"select Lower(MobileModuleID) as ID,ModuleName as Title
                                            from dbo.MobileModule
                                            where CustomerID='{0}'
                                            and IsDelete=0 and Status=1 order by CreateTime ",
                                           CurrentUserInfo.ClientID);
            return GetAll(pSql);
        }
        #endregion

        #region 自定义SQL查询
        private DataSet GetAll(string pSql)
        {
            //执行语句并返回结果    
            using (DataSet ds = this.SQLHelper.ExecuteDataset(pSql))
            {
                return ds;
            }
        }      
        #endregion

        #region 自定义判断
        private int GetScalar(string pSql) 
        {
            return (int)this.SQLHelper.ExecuteScalar(pSql);
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 进行判断guid型数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <param name="pParentID"></param>
        /// <returns></returns>
        private DataSet GetAllByClientID(string pSql, string pParentID)
        {
            string pSqlWhere = " ";
            if (!string.IsNullOrEmpty(pParentID))
            {
                pSqlWhere = " and ParentID='" + pParentID + "'";
            }
            string sql = string.Format(pSql, CurrentUserInfo.ClientID, pSqlWhere);
            return GetAll(sql);
        }

        /// <summary>
        /// 进行判断int型数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <param name="pParentID"></param>
        /// <returns></returns>
        private DataSet GetAllByClientIDInt(string pSql, string pParentID)
        {
            string pSqlWhere = " ";
            if (!string.IsNullOrEmpty(pParentID))
            {
                pSqlWhere = " and ParentID=" + pParentID;
            }
            string sql = string.Format(pSql, CurrentUserInfo.ClientID, pSqlWhere);
            return GetAll(sql);
        }
        #endregion

        #region GetTreeByParentID
        public int GetTreeByParentID(string TableName, string pParentID)
        {
            string pSqlWhere = "";
            if (!string.IsNullOrEmpty(pParentID))
            {
                if (pParentID != "0")
                {
                    pSqlWhere = " and ParentID=" + pParentID;
                }
                else
                {
                    pSqlWhere = " and ParentID is null";
                }
            }
            string pSql = string.Format(ControlSqlMap.SQL_GETTREEBYPARENTID, TableName, CurrentUserInfo.ClientID, pSqlWhere);
            return GetScalar(pSql);
        }
        #endregion

        #region GetChannelByClientID
        public DataSet GetChannelByClientID(string pParentID)
        {   
            return GetAllByClientID(ControlSqlMap.SQL_GETCHANNELBYCLIENTID,pParentID);
        }
        #endregion

        #region GetChainByClientID
        public DataSet GetChainByClientID(string pParentID)
        {
            return GetAllByClientID(ControlSqlMap.SQL_GETCHAINBYCLIENTID,pParentID);
        }
        #endregion


        #region GetClientPositionByClientID
        public DataSet GetClientPositionByClientID(string pParentID)
        {
            return GetAllByClientIDInt(ControlSqlMap.SQL_GETCLIENTPOSITION, pParentID);
        }
        #endregion

        #region GetCategoryByClientID
        public DataSet GetCategoryByClientID(string pParentID)
        {
            return GetAllByClientIDInt(ControlSqlMap.SQL_GETCATEGORYBYCLIENTID, pParentID);
        }
        #endregion

        #region GetBrandByClientID
        public DataSet GetBrandByClientID(string pParentID)
        {
            return GetAllByClientIDInt(ControlSqlMap.SQL_GETBRANDBYCLIENTID, pParentID);
        }
        #endregion

        #region GetClientStructureByClientID
        public DataSet GetClientStructureByClientID(string pParentID)
        {
            return GetAllByClientID(ControlSqlMap.SQL_GETCLIENTSTRUCTUREBYCLIENTID,pParentID);
        }
        #endregion

        #region GetHierarchyByClientID
        public DataSet GetHierarchyByClientID(int  pHierarchyType)
        {
            string pSqlWhere = " ";
            if (pHierarchyType > 0)
            {
                pSqlWhere = " and HierarchyType=" + pHierarchyType;
            }
            string sql = string.Format(ControlSqlMap.SQL_GETHIERARCHY, CurrentUserInfo.ClientID, pSqlWhere);
            return GetAll(sql);    
        }
        #endregion

        #region GetHierarchyLevelByClientID
        public DataSet GetHierarchyLevelByClientID(string pHierarchyID)
        {
            string pSqlWhere = " ";
            if (!string.IsNullOrEmpty(pHierarchyID))
            {
                pSqlWhere = " and ClientHierarchyId='" + pHierarchyID+"'";
            }
            string sql = string.Format(ControlSqlMap.SQL_GETHIERARCHYLEVEL, CurrentUserInfo.ClientID, pSqlWhere);
            return GetAll(sql);
        }
        #endregion

        #region GetHierarchyItemByClientID
        public DataSet GetHierarchyItemByClientID(string pHierarchyID, string pParentID)
        {
            string pSqlWhere = " ";
            if (!string.IsNullOrEmpty(pParentID))
            {
                pSqlWhere = " and ParentID='" + pParentID+"'";
            }
            string sql = string.Format(ControlSqlMap.SQL_GETHIERARCHYITEM, CurrentUserInfo.ClientID, pHierarchyID, pSqlWhere);
            return GetAll(sql);
        }
        #endregion

        #region GetProvince
        public DataSet GetProvince()
        {
            return GetAll(ControlSqlMap.SQL_GETPROVINCE);
        }
        #endregion

        #region GetCityByProvinceID
        public DataSet GetCityByProvinceID(string pProvinceID)
        {
            string sql = string.Format(ControlSqlMap.SQL_GETCITY, pProvinceID);
            return GetAll(sql);
        }
        #endregion

        #region GetCityByCityID
        public DataSet GetCityByCityID(string pCityID)
        {
            string sql = string.Format(ControlSqlMap.SQL_GetCITYBYCITYID, pCityID);
            return GetAll(sql);
        }
        #endregion

        #region GetDistrictByCityID
        public DataSet GetDistrictByCityID(string pCityID)
        {
            string sql = string.Format(ControlSqlMap.SQL_GETDISTRICT, pCityID);
            return GetAll(sql);
        }
        #endregion        

        #region GetDistrictByDistrictID
        public DataSet GetDistrictByDistrictID(string pDistrictID)
        {
            string sql = string.Format(ControlSqlMap.SQL_GETDISTRICTBYDISTRICTYID, pDistrictID);
            return GetAll(sql);
        }
        #endregion
    }
}
