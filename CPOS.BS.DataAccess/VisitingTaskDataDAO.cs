/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:45:48
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

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 拜访数据表 
    /// 表VisitingTaskData的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VisitingTaskDataDAO : BaseCPOSDAO, ICRUDable<VisitingTaskDataEntity>, IQueryable<VisitingTaskDataEntity>
    {
        #region GetVisitingTaskDataList
        public List<VisitingTaskDataViewEntity> GetVisitingTaskDataList(Dictionary<string, object> pQueryParams, OrderBy[] pOrderBys)
        {
            //组织SQL

            StringBuilder queryBody = new StringBuilder();
            StringBuilder queryWhere = new StringBuilder();

            #region 查询条件
            if (pQueryParams.ContainsKey("ExecutionTime"))
                queryWhere.Append("and datediff(day,vd.InTime,'" + pQueryParams["ExecutionTime"].ToString() + "')=0 ");

            if (pQueryParams.ContainsKey("POPID"))
                queryWhere.Append(" and vd.POPID='" + pQueryParams["POPID"].ToString() + "' ");

            if (pQueryParams.ContainsKey("ClientUserID"))
                queryWhere.Append(" and vd.ClientUserID='" + pQueryParams["ClientUserID"].ToString() + "' ");

            if (pQueryParams.ContainsKey("ClientPositionID"))
                queryWhere.Append(" and P.ClientPositionID='" + pQueryParams["ClientPositionID"].ToString() + "' ");

            if (pQueryParams.ContainsKey("ClientStructureID"))
                queryWhere.Append(" and CS.ClientStructureID in (SELECT StructureID FROM   fnGetStructure('" + pQueryParams["ClientStructureID"].ToString() + "',1)) ");

            #endregion
            #region 主体查询语句
            queryBody.AppendFormat(@" 
select * from(
    SELECT  vd.ClientUserID ,
            tuser.[user_name] AS ClientUserName,
            vd.VisitingTaskDataID ,
            vd.POPID ,
            POPName = tunit.unit_name,
            vd.InTime ,
            vd.OutTime ,
            vd.InCoordinate,
            vd.OutCoordinate,
            vd.InPic ,
            vd.OutPic,
            vd.InTime as ExecutionTime
    FROM   
    VisitingTaskData vd 
    LEFT JOIN T_User tuser ON vd.ClientUserID=tuser.[user_id]
    LEFT JOIN t_unit tunit ON vd.POPID=tunit.unit_id    
    where vd.IsDelete=0 and isnull(vd.POPType,1)=1 and  vd.ClientID='{0}'",
    this.CurrentUserInfo.ClientID);
            queryBody.Append(queryWhere);
            queryBody.Append(@" 
) A
            ORDER BY isnull(A.InTime,'2999-9-9') asc");
            #endregion

            //执行语句并返回结果
            List<VisitingTaskDataViewEntity> list = new List<VisitingTaskDataViewEntity>();

            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(queryBody.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskDataViewEntity m = new VisitingTaskDataViewEntity();
                    
                    if (rdr["ClientUserID"] != DBNull.Value)
                    {
                        m.ClientUserID = rdr["ClientUserID"].ToString();
                    }
                    if (rdr["ClientUserName"] != DBNull.Value)
                    {
                        m.ClientUserName = rdr["ClientUserName"].ToString();
                    }
                    if (rdr["VisitingTaskDataID"] != DBNull.Value)
                    {
                        m.VisitingTaskDataID = Convert.ToString(rdr["VisitingTaskDataID"]);
                    }
                    if (rdr["POPID"] != DBNull.Value)
                    {
                        m.POPID = Convert.ToString(rdr["POPID"]);
                    }
                    if (rdr["POPName"] != DBNull.Value)
                    {
                        m.POPName = Convert.ToString(rdr["POPName"]);
                    }
                    if (rdr["InTime"] != DBNull.Value)
                    {
                        m.InTime = Convert.ToDateTime(rdr["InTime"]).Date;
                    }
                    if (rdr["OutTime"] != DBNull.Value)
                    {
                        m.OutTime = Convert.ToDateTime(rdr["OutTime"]);
                    }
                    if (rdr["InCoordinate"] != DBNull.Value)
                    {
                        m.InCoordinate = Convert.ToString(rdr["InCoordinate"]);
                    }
                    if (rdr["OutCoordinate"] != DBNull.Value)
                    {
                        m.OutCoordinate = Convert.ToString(rdr["OutCoordinate"]);
                    }
                    if (rdr["ExecutionTime"] != DBNull.Value)
                    {
                        m.ExecutionTime = Convert.ToDateTime(rdr["ExecutionTime"]);
                    }
                    list.Add(m);
                }
            }
            return list;
        }
        #endregion

        #region GetVisitingTaskPlanList
        public List<VisitingTaskPlanViewEntity> GetVisitingTaskPlanList(Dictionary<string, object> pQueryParams)
        {
            List<VisitingTaskPlanViewEntity> result = new List<VisitingTaskPlanViewEntity>();

            StringBuilder queryBody = new StringBuilder();
            queryBody.AppendFormat(@" 
SELECT  VisitingTaskID = CDP.RouteID ,
        VisitingTaskName = 'RouteName' ,
        POPID = CDP.POPID ,
        CDP.POPType,
        POPName = 'popname' ,
        Coordinate = ''
FROM    [CallDayPlanning] CDP       
WHERE   CDP.IsDelete = 0
        AND CDP.ClientID = '12'",
                this.CurrentUserInfo.ClientID,
                this.CurrentUserInfo.ClientDistributorID);

            //if (pQueryParams.ContainsKey("ClientUserID"))
            //    queryBody.Append(" and ClientUserID='" + pQueryParams["ClientUserID"].ToString() + "' ");
            if (pQueryParams.ContainsKey("ExecutionTime"))
                queryBody.Append(" and DATEDIFF(day,calldate,'" + pQueryParams["ExecutionTime"].ToString() + "')=0  ");

            queryBody.Append(" order by CDP.sequence asc,CDP.Createtime asc");

            //执行语句并返回结果
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(queryBody.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskPlanViewEntity m = new VisitingTaskPlanViewEntity();
                    //数据转换成ViewEntity
                    if (rdr["Coordinate"] != DBNull.Value)
                    {
                        m.Coordinate = Convert.ToString(rdr["Coordinate"]);
                    }
                    if (rdr["POPID"] != DBNull.Value)
                    {
                        m.POPID = Convert.ToString(rdr["POPID"]);
                    }
                    if (rdr["POPType"] != DBNull.Value)
                    {
                        m.POPType = Convert.ToInt32(rdr["POPType"]);
                    }
                    if (rdr["POPName"] != DBNull.Value)
                    {
                        m.POPName = Convert.ToString(rdr["POPName"]);
                    }
                    if (rdr["VisitingTaskID"] != DBNull.Value)
                    {
                        m.VisitingTaskID = Convert.ToString(rdr["VisitingTaskID"]);
                    }
                    if (rdr["VisitingTaskName"] != DBNull.Value)
                    {
                        m.VisitingTaskName = Convert.ToString(rdr["VisitingTaskName"]);
                    }
                    result.Add(m);
                }
            }
            return result;
        }
        #endregion

        #region GetVisitingTaskDetailData
        public DataSet GetVisitingTaskDetailData(Dictionary<string, object> pQueryParams)
        {
            //组织SQL
            StringBuilder queryBody = new StringBuilder();
            StringBuilder commonSelectBody = new StringBuilder();
            StringBuilder commonFromBody = new StringBuilder();
            //中英文字段映射
            Dictionary<string, string> dictColumnNameMapping = new Dictionary<string, string>();
            dictColumnNameMapping.Add("@OptionText", "OptionText");
            dictColumnNameMapping.Add("@SKUName", "SKUName");
            dictColumnNameMapping.Add("@BrandName", "BrandName");
            dictColumnNameMapping.Add("@CategoryName", "CategoryName");
            dictColumnNameMapping.Add("@PositionName", "PositionName");
            dictColumnNameMapping.Add("@ParameterName", "ParameterName");
            dictColumnNameMapping = GetColumnNameByLang(dictColumnNameMapping);

            #region 主体查询语句
            queryBody.Append(@" select distinct VD.VisitingTaskDataID, VD.POPID,VD.InTime,  
             VSS.VisitingTaskStepID 
             into #tempVisitingTaskStepDetailViewData from  VisitingTaskDetailData VDD   
             inner join VisitingTaskData VD on VDD.VisitingTaskDataID = VD.VisitingTaskDataID and VD.IsDelete=0  
              inner join VisitingTaskStep VS on VDD.VisitingTaskStepID=VS.VisitingTaskStepID 
              RIGHT JOIN VisitingTaskStep VSS ON VSS.VisitingTaskID = VS.VisitingTaskID AND (VSS.IsDelete=0 or VDD.VisitingTaskStepID=VSS.VisitingTaskStepID)          
             where VDD.IsDelete=0  and  VDD.ClientID='" + this.CurrentUserInfo.ClientID + "' and isnull   (VDD.ClientDistributorID,0)=" + this.CurrentUserInfo.ClientDistributorID + " ");
            if (pQueryParams.ContainsKey("ExecutionTime"))
            {
                queryBody.Append("and datediff(day,vd.InTime,'" + pQueryParams["ExecutionTime"].ToString() + "')=0 ");
                Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "[某人某天在某个店的拜访步骤及参数值明细]接收参数:ExecutionTime:[" + pQueryParams["ExecutionTime"].ToString() + "]" });
            }
            else
            {
                Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "[某人某天在某个店的拜访步骤及参数值明细]接收参数:ExecutionTime:【无】" });
            }

            if (pQueryParams.ContainsKey("ClientUserID"))
            {
                queryBody.Append(" and vd.ClientUserID='" + pQueryParams["ClientUserID"].ToString() + "' ");
                Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "[某人某天在某个店的拜访步骤及参数值明细]接收参数:ClientUserID:[" + pQueryParams["ClientUserID"].ToString() + "]" });
            }
            else
            {
                Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "[某人某天在某个店的拜访步骤及参数值明细]接收参数:ClientUserID:【无】" });
            }

            if (pQueryParams.ContainsKey("POPID"))
            {
                queryBody.Append(" and VD.POPID='" + pQueryParams["POPID"].ToString() + "' ");
                Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "[某人某天在某个店的拜访步骤及参数值明细]接收参数:POPID:[" + pQueryParams["POPID"].ToString() + "]" });
            }
            else
            {
                Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "[某人某天在某个店的拜访步骤及参数值明细]接收参数:POPID:【无】" });
            }

            #endregion

            #region 取出详细数据
            commonSelectBody.Append(@" SELECT DISTINCT 
                 VD.VisitingTaskDataID ,VD.POPID ,VDD.VisitingTaskDetailDataID ,VD.ClientUserID  
                 ,VD.InTime ,temp.VisitingTaskStepID ,  VS.StepType ,VS.StepName ,VS.StepNo   
                 ,VS.StepPriority ,VP.VisitingParameterID ,VP.ParameterType
                 ,ParameterName = VP.ParameterName , VP.ControlType ,VP.ControlName 
                  ,VPM.ParameterOrder,VDD.ObjectID  ,VDD.Target1ID ,VDD.Target2ID , 
                 Value = CASE WHEN (RTRIM(VP.ControlName) = ''  or VP.ControlType=6 ) THEN VDD.Value WHEN VP.ControlName IS NULL THEN VDD.Value ELSE OPT.OptionText    END ");
            //通用表联接语句
            commonFromBody.Append(@" FROM #tempVisitingTaskStepDetailViewData temp  
                 INNER JOIN VisitingTaskData VD  
                 ON temp.VisitingTaskDataID = VD.VisitingTaskDataID AND VD.IsDelete = 0  
                  INNER JOIN VisitingTaskStep VS   
                 ON temp.VisitingTaskStepID = VS.VisitingTaskStepID 
                 INNER JOIN VisitingTaskParameterMapping VPM 
                 ON VPM.VisitingTaskStepID = temp.VisitingTaskStepID 
                   INNER JOIN VisitingParameter VP  
                 ON VPM.VisitingParameterID = VP.VisitingParameterID 
                  LEFT JOIN VisitingTaskDetailData VDD  
                 ON VDD.VisitingTaskDataID=temp.VisitingTaskDataID  AND VDD.VisitingTaskStepID=temp.VisitingTaskStepID AND VDD.VisitingParameterID=VP.VisitingParameterID AND VDD.IsDelete = 0  
                  LEFT JOIN VisitingParameterOptions OPT   
                  ON VDD.Value = CAST(OPT.OptionValue AS VARCHAR(50)) AND VP.ControlName = opt.OptionName AND VP.ControlType!=6 
                  AND ( RTRIM(vp.ControlName) != '' AND NOT vp.ControlName IS NULL )  and OPT.ClientID='" + this.CurrentUserInfo.ClientID + "' and isnull(OPT.ClientDistributorID,-1)=" + (string.IsNullOrWhiteSpace(this.CurrentUserInfo.ClientDistributorID) ? "-1" : this.CurrentUserInfo.ClientDistributorID));

            queryBody.AppendFormat(" select distinct * from ( ");
            /*StepType	1	产品相关*/
            queryBody.AppendFormat(commonSelectBody.ToString());
            queryBody.AppendFormat(" ,ObjectName1= item.item_name+' '+sku.sku_prop_id1,ObjectName2='' ,ObjectOrder=0  ");
            queryBody.Append(commonFromBody.ToString());
            queryBody.AppendFormat(@" left join T_sku sku on VDD.Target1ID=sku.sku_id and VS.StepType=1
                                      left join T_item item on sku.item_id=item.item_id
             where  VS.StepType=1 
            union all ");
//            /*StepType	2	品牌相关*/
//            queryBody.AppendFormat(commonSelectBody.ToString());
//            queryBody.AppendFormat(" , ObjectName1= b1.BrandName  ,ObjectName2=  C1.CategoryName ,ObjectOrder=0  ");
//            queryBody.Append(commonFromBody.ToString());
//            queryBody.AppendFormat(@" left join Brand B1 on VDD.Target1ID=B1.BrandID and VS.StepType=2     
//                   left join Category C1 on VDD.Target2ID=C1.CategoryID and VS.StepType=2
//                where VS.StepType=2  
//                union all ");
//            /*StepType	3	品类相关*/
//            queryBody.AppendFormat(commonSelectBody.ToString());
//            queryBody.AppendFormat(" , ObjectName1=C2.CategoryName  ,ObjectName2= b2.BrandName ,ObjectOrder=0 ");
//            queryBody.Append(commonFromBody.ToString());
//            queryBody.AppendFormat(@" left join Category C2 on VDD.Target1ID=C2.CategoryID and VS.StepType=3
//                    left join Brand B2 on VDD.Target2ID=B2.BrandID and VS.StepType=3
//                 where VS.StepType=3  
//                union all ");
            /*StepType	4	人员考评*/
//            queryBody.AppendFormat(commonSelectBody.ToString());
//            queryBody.AppendFormat(" , ObjectName1= cp.PositionName  ,ObjectName2=   CU.Name   ,ObjectOrder=0   ");
//            queryBody.Append(commonFromBody.ToString());
//            queryBody.AppendFormat(@" left join ClientPosition CP on VDD.Target1ID=CP.ClientPositionID and VS.StepType=4 
//                left join ClientUser CU on VDD.Target2ID=cu.ClientUserID and VS.StepType=4
//                where VS.StepType=4 
//                 union all ");

            /*StepType	5	问卷*/
            queryBody.AppendFormat(commonSelectBody.ToString());
            queryBody.AppendFormat(" , ObjectName1= ''   ,ObjectName2=   ''   ,ObjectOrder=0   ");
            queryBody.Append(commonFromBody.ToString());
            queryBody.AppendFormat(@" where VS.StepType=5 
             union all ");

            /*StepType	7	自定义对象*/
            //查询主体
            queryBody.AppendFormat(@" SELECT DISTINCT 
                 VD.VisitingTaskDataID ,VD.POPID ,VDD.VisitingTaskDetailDataID ,VD.ClientUserID  
                 ,VD.InTime ,temp.VisitingTaskStepID ,  VS.StepType ,VS.StepName ,VS.StepNo   
                 ,VS.StepPriority ,VP.VisitingParameterID ,VP.ParameterType
                 ,ParameterName = VP.ParameterName , VP.ControlType ,VP.ControlName 
                  ,VPM.ParameterOrder,ObjectID =VO.VisitingObjectID ,VSO.Target1ID ,VSO.Target2ID , 
                 Value = CASE WHEN (RTRIM(VP.ControlName) = ''  or VP.ControlType=6 ) THEN VDD.Value WHEN VP.ControlName IS NULL THEN VDD.Value ELSE OPT.OptionText    END
             , ObjectName1=VO.ObjectName  ,ObjectName2 = '',VO.Sequence AS ObjectOrder ");
            //表联接
            queryBody.AppendFormat(@" FROM #tempVisitingTaskStepDetailViewData temp  
                 INNER JOIN VisitingTaskData VD  
                 ON temp.VisitingTaskDataID = VD.VisitingTaskDataID AND VD.IsDelete = 0 
                  INNER JOIN VisitingTaskStep VS  
                 ON temp.VisitingTaskStepID = VS.VisitingTaskStepID 
                 LEFT JOIN VisitingTaskStepObject VSO ON VS.VisitingTaskStepID=VSO.VisitingTaskStepID
                 LEFT JOIN VisitingObject VO ON  VO.ObjectGroup=VSO.Target1ID 
AND VO.ClientID='" + this.CurrentUserInfo.ClientID + "' and isnull(VO.ClientDistributorID,-1)=" +  this.CurrentUserInfo.ClientDistributorID +
             @" INNER JOIN VisitingObjectVisitingParameterMapping VPM  
                 ON VPM.VisitingObjectID = VO.VisitingObjectID  
                  left JOIN VisitingParameter VP  
                ON VPM.VisitingParameterID = VP.VisitingParameterID 
                 LEFT JOIN VisitingTaskDetailData VDD  
                ON VDD.VisitingTaskDataID=temp.VisitingTaskDataID  AND VDD.VisitingTaskStepID=temp.VisitingTaskStepID and 
                VDD.ObjectID=VPM.VisitingObjectID  and VDD.VisitingParameterID=VP.VisitingParameterID and 
                VSO.Target1ID=VDD.Target1ID  AND VDD.IsDelete = 0 
                 LEFT JOIN VisitingParameterOptions OPT 
                 ON VDD.Value = CAST(OPT.OptionValue AS VARCHAR(50)) AND VP.ControlName = opt.OptionName AND VP.ControlType!=6 
                 AND ( RTRIM(vp.ControlName) != '' AND NOT vp.ControlName IS NULL )  and OPT.ClientID='" + this.CurrentUserInfo.ClientID + "' and isnull(OPT.ClientDistributorID,0)=" + this.CurrentUserInfo.ClientDistributorID + " where  VS.StepType=7 ) DetailData order by StepPriority ,ObjectOrder , ParameterOrder ");
            #endregion
            //中英文字段名称替换
            string pagedQueryString = queryBody.ToString();
            foreach (var item in dictColumnNameMapping)
            {
                pagedQueryString = pagedQueryString.Replace(item.Key, item.Value);
            }

            //执行语句并返回结果
            return this.SQLHelper.ExecuteDataset(pagedQueryString);
        }
        #endregion

        #region GetVisitingTaskPictures
        public PagedQueryResult<VisitingTaskPicturesViewEntity> GetVisitingTaskPictures(Dictionary<string, object> pQueryParams, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            StringBuilder queryBody = new StringBuilder();
            //中英文字段映射
            Dictionary<string, string> dictColumnNameMapping = new Dictionary<string, string>();
            dictColumnNameMapping.Add("@PositionName", "PositionName");
            dictColumnNameMapping.Add("@StructureName", "StructureName");
            dictColumnNameMapping.Add("@VisitingTaskName", "VisitingTaskName");
            dictColumnNameMapping.Add("@StepName", "StepName");
            dictColumnNameMapping.Add("@ParameterName", "ParameterName");
            dictColumnNameMapping = GetColumnNameByLang(dictColumnNameMapping);

            #region 查询条件
            StringBuilder inPicWhere = new StringBuilder();
            StringBuilder outPicWhere = new StringBuilder();
            StringBuilder parameterPicWhere = new StringBuilder();
            //部门查询条件
            if (pQueryParams.ContainsKey("ClientStructureID"))
            {
                inPicWhere.Append(" and CS.ClientStructureID in (SELECT StructureID FROM   fnGetStructure('" + pQueryParams["ClientStructureID"].ToString() + "',1)) ");
                outPicWhere.Append(" and CS.ClientStructureID in (SELECT StructureID FROM   fnGetStructure('" + pQueryParams["ClientStructureID"].ToString() + "',1)) ");
                parameterPicWhere.Append(" and CS.ClientStructureID in (SELECT StructureID FROM   fnGetStructure('" + pQueryParams["ClientStructureID"].ToString() + "',1)) ");
                //inPicWhere.Append(" and CS.ClientStructureID='" + pQueryParams["ClientStructureID"].ToString() + "' ");
                //outPicWhere.Append(" and CS.ClientStructureID='" + pQueryParams["ClientStructureID"].ToString() + "' ");
                //parameterPicWhere.Append(" and CS.ClientStructureID='" + pQueryParams["ClientStructureID"].ToString() + "' ");
            }
            //职位查询条件
            if (pQueryParams.ContainsKey("ClientPositionID"))
            {
                inPicWhere.Append(" and P.ClientPositionID='" + pQueryParams["ClientPositionID"].ToString() + "' ");
                outPicWhere.Append(" and P.ClientPositionID='" + pQueryParams["ClientPositionID"].ToString() + "' ");
                parameterPicWhere.Append(" and P.ClientPositionID='" + pQueryParams["ClientPositionID"].ToString() + "' ");
            }
            //执行人员查询条件
            if (pQueryParams.ContainsKey("ClientUserID"))
            {
                inPicWhere.Append(" and VD.ClientUserID='" + pQueryParams["ClientUserID"].ToString() + "' ");
                outPicWhere.Append(" and VD.ClientUserID='" + pQueryParams["ClientUserID"].ToString() + "' ");
                parameterPicWhere.Append(" and VD.ClientUserID='" + pQueryParams["ClientUserID"].ToString() + "' ");
            }

            //拜访任务查询条件
            if (pQueryParams.ContainsKey("VisitingTaskDataID"))
            {
                inPicWhere.Append(" and VD.VisitingTaskDataID='" + pQueryParams["VisitingTaskDataID"].ToString() + "' ");
                outPicWhere.Append(" and VD.VisitingTaskDataID='" + pQueryParams["VisitingTaskDataID"].ToString() + "' ");
                parameterPicWhere.Append(" and VD.VisitingTaskDataID='" + pQueryParams["VisitingTaskDataID"].ToString() + "' ");
            }

            //拜访步骤查询条件
            if (pQueryParams.ContainsKey("VisitingTaskStepID"))
            {
                parameterPicWhere.Append(" and vs.VisitingTaskStepID='" + pQueryParams["VisitingTaskStepID"].ToString() + "' ");
            }

            //开始时间 查询条件
            if (pQueryParams.ContainsKey("DateFrom"))
            {
                //inPicWhere.Append(" and vd.InTime between '" + pQueryParams["DateFrom"].ToString() + "' and '" + pQueryParams["DateTo"].ToString() + "' ");
                //outPicWhere.Append(" and vd.InTime between '" + pQueryParams["DateFrom"].ToString() + "' and '" + pQueryParams["DateTo"].ToString() + "' ");
                //parameterPicWhere.Append(" and vd.InTime between '" + pQueryParams["DateFrom"].ToString() + "' and '" + pQueryParams["DateTo"].ToString() + "' ");
                inPicWhere.Append(" and datediff(DAY,'" + pQueryParams["DateFrom"].ToString() + "',vd.InTime )>=0 ");
                outPicWhere.Append(" and datediff(DAY,'" + pQueryParams["DateFrom"].ToString() + "',vd.InTime )>=0  ");
                parameterPicWhere.Append(" and datediff(DAY,'" + pQueryParams["DateFrom"].ToString() + "',vd.InTime )>=0 ");
            }
            ///结束时间  查询条件
            if (pQueryParams.ContainsKey("DateTo"))
            {
                inPicWhere.Append(" and datediff(DAY,'" + pQueryParams["DateTo"].ToString() + "',vd.InTime )<=0 ");
                outPicWhere.Append("and datediff(DAY,'" + pQueryParams["DateTo"].ToString() + "',vd.InTime )<=0 ");
                parameterPicWhere.Append(" and datediff(DAY,'" + pQueryParams["DateTo"].ToString() + "',vd.InTime )<=0 ");
            }
            #endregion
            #region 主体查询语句

            #region 门头照
            //进店照片
            if (!pQueryParams.ContainsKey("VisitingTaskStepID") || pQueryParams["VisitingTaskStepID"].ToString() == "0")
            {
                queryBody.AppendFormat(@" select CS.ClientStructureID,StructureName=cs.@StructureName,P.ClientPositionID,PositionName=p.@PositionName,VD.POPID, 
                 POPName =  st.StoreName 
                 ,VD.ClientUserID,UserName=U.Name,VD.InTime,VD.InCoordinate,VD.OutCoordinate, VisitingTaskStepID=null,VisitingTaskID=null, TaskName='进店',StepName='进店',PhotoName='进店照片',Value=VD.InPic 
                 from VisitingTaskData VD 
                 inner join  fnGetClientUser({0},1) SubUsers on VD.ClientUserID = SubUsers.ClientUserID 
                 inner join ClientUser U on VD.ClientUserID=U.ClientUserID and U.IsDelete=0
                 inner join ClientPosition P on U.ClientPositionID=P.ClientPositionID and P.IsDelete=0  
                 inner join ClientStructure CS on U.ClientStructureID=CS.ClientStructureID and CS.IsDelete=0 
                 inner join Store ST on VD.POPType=1 and CAST(vd.POPID as uniqueidentifier)=st.StoreID and ST.IsDelete=0 
                 where VD.IsDelete=0  and VD.InPic is not null and rtrim(VD.InPic)!='' and  VD.ClientID='{1}' and isnull(VD.ClientDistributorID,0)={2}", this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID);
                queryBody.Append(inPicWhere.ToString());

                queryBody.Append(" union all  ");

                queryBody.AppendFormat(@" select CS.ClientStructureID,StructureName=cs.@StructureName,P.ClientPositionID,PositionName=p.@PositionName,VD.POPID, 
                 POPName = DT.Distributor   
                 ,VD.ClientUserID,UserName=U.Name,VD.InTime,VD.InCoordinate,VD.OutCoordinate, VisitingTaskStepID=null,VisitingTaskID=null, TaskName='进店',StepName='进店',PhotoName='进店照片',Value=VD.InPic
                 from VisitingTaskData VD 
                 inner join  fnGetClientUser({0},1) SubUsers on VD.ClientUserID = SubUsers.ClientUserID 
                 inner join ClientUser U on VD.ClientUserID=U.ClientUserID and U.IsDelete=0
                 inner join ClientPosition P on U.ClientPositionID=P.ClientPositionID and P.IsDelete=0  
                 inner join ClientStructure CS on U.ClientStructureID=CS.ClientStructureID and CS.IsDelete=0
                 inner join Distributor DT on VD.POPType=2 and vd.POPID=CAST(DT.DistributorID as varchar(50)) and DT.IsDelete=0
                 where VD.IsDelete=0  and VD.InPic is not null and rtrim(VD.InPic)!=''  and  VD.ClientID='{1}' and isnull(VD.ClientDistributorID,0)={2}", this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID);
                queryBody.Append(inPicWhere.ToString());

                queryBody.Append(" union all  ");
                //出店照片
                queryBody.AppendFormat(@" select CS.ClientStructureID,StructureName=cs.@StructureName,P.ClientPositionID,PositionName=p.@PositionName,VD.POPID,POPName = st.StoreName 
                 ,VD.ClientUserID,UserName=U.Name,VD.InTime,VD.InCoordinate,VD.OutCoordinate, VisitingTaskStepID=null,VisitingTaskID=null, TaskName='出店',StepName='出店',PhotoName='出店照片',Value=VD.OutPic 
                 from VisitingTaskData VD
                 inner join  fnGetClientUser({0},1) SubUsers on VD.ClientUserID = SubUsers.ClientUserID
                 inner join ClientUser U on VD.ClientUserID=U.ClientUserID and U.IsDelete=0 
                 inner join ClientPosition P on U.ClientPositionID=P.ClientPositionID and P.IsDelete=0
                 inner join ClientStructure CS on U.ClientStructureID=CS.ClientStructureID and CS.IsDelete=0 
                 inner join Store ST on VD.POPType=1 and CAST(vd.POPID as uniqueidentifier)=st.StoreID and ST.IsDelete=0 
                 where VD.IsDelete=0  and VD.OutPic is not null and rtrim(VD.OutPic)!=''  and  VD.ClientID='{1}' and isnull(VD.ClientDistributorID,0)={2}", this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID);
                queryBody.Append(outPicWhere.ToString());

                queryBody.Append(" union all ");

                queryBody.AppendFormat(@" select CS.ClientStructureID,StructureName=cs.@StructureName,P.ClientPositionID,PositionName=p.@PositionName,VD.POPID,POPName = DT.Distributor 
                 ,VD.ClientUserID,UserName=U.Name,VD.InTime,VD.InCoordinate,VD.OutCoordinate, VisitingTaskStepID=null,VisitingTaskID=null, TaskName='出店',StepName='出店',PhotoName='出店照片',Value=VD.OutPic 
                 from VisitingTaskData VD 
                 inner join  fnGetClientUser({0},1) SubUsers on VD.ClientUserID = SubUsers.ClientUserID 
                 inner join ClientUser U on VD.ClientUserID=U.ClientUserID and U.IsDelete=0 
                 inner join ClientPosition P on U.ClientPositionID=P.ClientPositionID and P.IsDelete=0 
                 inner join ClientStructure CS on U.ClientStructureID=CS.ClientStructureID and CS.IsDelete=0
                 inner join Distributor DT on VD.POPType=2 and CAST(vd.POPID as int)=DT.DistributorID and DT.IsDelete=0 
                 where VD.IsDelete=0  and VD.OutPic is not null and rtrim(VD.OutPic)!=''    and  VD.ClientID='{1}' and isnull(VD.ClientDistributorID,0)={2}", this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID);
                queryBody.Append(outPicWhere.ToString());

                queryBody.Append(" union all ");
            }
            #endregion 门头照
            //拜访参数照片
            queryBody.AppendFormat(@" select CS.ClientStructureID,StructureName=cs.@StructureName,P.ClientPositionID,PositionName=p.@PositionName,VD.POPID,POPName = st.StoreName 
             ,VD.ClientUserID,UserName=U.Name,VD.InTime,VD.InCoordinate,VD.OutCoordinate 
             , VS.VisitingTaskStepID,VT.VisitingTaskID, TaskName=VT.@VisitingTaskName,StepName=vs.@StepName,PhotoName=vp.@ParameterName,VDD.Value 
             from  VisitingTaskDetailData VDD 
             inner join VisitingTaskData VD on VDD.VisitingTaskDataID=VD.VisitingTaskDataID and VD.IsDelete=0 
             inner join VisitingParameter VP on vp.ControlType=12 and VDD.VisitingParameterID=VP.VisitingParameterID
             inner join  fnGetClientUser({0},1) SubUsers on VD.ClientUserID = SubUsers.ClientUserID 
             inner join ClientUser U on VD.ClientUserID=U.ClientUserID
             inner join ClientPosition P on U.ClientPositionID=P.ClientPositionID
             inner join ClientStructure CS on U.ClientStructureID=CS.ClientStructureID
             inner join VisitingTaskStep VS on VDD.VisitingTaskStepID=vs.VisitingTaskStepID
             inner join VisitingTask VT on VS.VisitingTaskID=VT.VisitingTaskID
             inner join Store ST on VD.POPType=1 and CAST(vd.POPID as uniqueidentifier)=st.StoreID 
             where VDD.IsDelete=0   and VDD.Value is not null and rtrim(VDD.Value)!=''  and  VD.ClientID='{1}' and isnull(VD.ClientDistributorID,0)={2}", this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID);
            queryBody.Append(parameterPicWhere.ToString());

            queryBody.Append(" union all ");

            queryBody.AppendFormat(@" select CS.ClientStructureID,StructureName=cs.@StructureName,P.ClientPositionID,PositionName=p.@PositionName,VD.POPID,POPName = DT.Distributor 
             ,VD.ClientUserID,UserName=U.Name,VD.InTime,VD.InCoordinate,VD.OutCoordinate 
             , VS.VisitingTaskStepID,VT.VisitingTaskID, TaskName=VT.@VisitingTaskName,StepName=vs.@StepName,PhotoName=vp.@ParameterName,VDD.Value  
             from  VisitingTaskDetailData VDD  
             inner join VisitingTaskData VD on VDD.VisitingTaskDataID=VD.VisitingTaskDataID and VD.IsDelete=0 
             inner join VisitingParameter VP on vp.ControlType=12 and VDD.VisitingParameterID=VP.VisitingParameterID
             inner join  fnGetClientUser({0},1) SubUsers on VD.ClientUserID = SubUsers.ClientUserID 
             inner join ClientUser U on VD.ClientUserID=U.ClientUserID
             inner join ClientPosition P on U.ClientPositionID=P.ClientPositionID
             inner join ClientStructure CS on U.ClientStructureID=CS.ClientStructureID
             inner join VisitingTaskStep VS on VDD.VisitingTaskStepID=vs.VisitingTaskStepID
             inner join VisitingTask VT on VS.VisitingTaskID=VT.VisitingTaskID
             inner join Distributor DT on VD.POPType=2 and vd.POPID=CAST(DT.DistributorID as varchar(50))
             where VDD.IsDelete=0    and VDD.Value is not null and rtrim(VDD.Value)!=''  and  VD.ClientID='{1}' and isnull(VD.ClientDistributorID,0)={2}", this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID);

            queryBody.Append(parameterPicWhere.ToString());
            #endregion

            #region 分页SQL
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
                pagedSql.AppendFormat(" StructureName,PositionName,POPName,UserName,TaskName,StepName,InTime ,Value  "); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from (" + queryBody.ToString() + ") VisitingTaskDetailViewData  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from (" + queryBody.ToString() + ") VisitingTaskDetailViewData ");

            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            #endregion

            //中英文字段名称替换
            string pagedQueryString = pagedSql.ToString();
            string totalCountQueryString = totalCountSql.ToString();
            foreach (var item in dictColumnNameMapping)
            {
                pagedQueryString = pagedQueryString.Replace(item.Key, item.Value);
                totalCountQueryString = totalCountQueryString.Replace(item.Key, item.Value);
            }
            //执行语句并返回结果
            PagedQueryResult<VisitingTaskPicturesViewEntity> result = new PagedQueryResult<VisitingTaskPicturesViewEntity>();
            List<VisitingTaskPicturesViewEntity> list = new List<VisitingTaskPicturesViewEntity>();
            //Loggers.Debug(new DebugLogInfo() { ClientID = "0", UserID = "0", Message = "查询照片：" + pagedQueryString.ToString() });
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedQueryString))
            {
                while (rdr.Read())
                {
                    VisitingTaskPicturesViewEntity m = new VisitingTaskPicturesViewEntity();
                    //数据转换成ViewEntity
                    if (rdr["ClientPositionID"] != DBNull.Value)
                    {
                        m.ClientPositionID = Convert.ToString(rdr["ClientPositionID"]);
                    }
                    if (rdr["ClientStructureID"] != DBNull.Value)
                    {
                        m.ClientStructureID = Convert.ToString(rdr["ClientStructureID"]);
                    }
                    if (rdr["ClientUserID"] != DBNull.Value)
                    {
                        m.ClientUserID = Convert.ToInt32(rdr["ClientUserID"]);
                    }
                    if (rdr["InCoordinate"] != DBNull.Value)
                    {
                        m.InCoordinate = Convert.ToString(rdr["InCoordinate"]);
                    }
                    if (rdr["InTime"] != DBNull.Value)
                    {
                        m.InTime = Convert.ToDateTime(rdr["InTime"]);
                    }
                    if (rdr["OutCoordinate"] != DBNull.Value)
                    {
                        m.OutCoordinate = Convert.ToString(rdr["OutCoordinate"]);
                    }
                    if (rdr["PhotoName"] != DBNull.Value)
                    {
                        m.PhotoName = Convert.ToString(rdr["PhotoName"]);
                    }
                    if (rdr["POPID"] != DBNull.Value)
                    {
                        m.POPID = Convert.ToString(rdr["POPID"]);
                    }
                    if (rdr["POPName"] != DBNull.Value)
                    {
                        m.POPName = Convert.ToString(rdr["POPName"]);
                    }
                    if (rdr["PositionName"] != DBNull.Value)
                    {
                        m.PositionName = Convert.ToString(rdr["PositionName"]);
                    }
                    if (rdr["StepName"] != DBNull.Value)
                    {
                        m.StepName = Convert.ToString(rdr["StepName"]);
                    }
                    if (rdr["StructureName"] != DBNull.Value)
                    {
                        m.StructureName = Convert.ToString(rdr["StructureName"]);
                    }
                    if (rdr["TaskName"] != DBNull.Value)
                    {
                        m.TaskName = Convert.ToString(rdr["TaskName"]);
                    }
                    if (rdr["UserName"] != DBNull.Value)
                    {
                        m.UserName = Convert.ToString(rdr["UserName"]);
                    }
                    if (rdr["Value"] != DBNull.Value)
                    {
                        m.Value = Convert.ToString(rdr["Value"]);
                    }
                    if (rdr["VisitingTaskID"] != DBNull.Value)
                    {
                        m.VisitingTaskID = Convert.ToString(rdr["VisitingTaskID"]);
                    }
                    if (rdr["VisitingTaskStepID"] != DBNull.Value)
                    {
                        m.VisitingTaskStepID = Convert.ToString(rdr["VisitingTaskStepID"]);
                    }
                    list.Add(m);
                }
            }

            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountQueryString));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }
        #endregion

        #region GetOrderSKUList
        public List<VisitingTaskDataOrderSKUViewEntity> GetOrderSKUList(string pUserID, string pPopID, DateTime pDate)
        {
            //SqlMap.SQL_GETORDERDETAILLIST
            StringBuilder sql = new StringBuilder();

            sql.Append(string.Format(SqlMap.SQL_GETORDERDETAILLIST, pUserID, pPopID, pDate));
            sql.Append(" and  ODD.ClientID='" + this.CurrentUserInfo.ClientID + "' and isnull(ODD.ClientDistributorID,-1)=" + (string.IsNullOrWhiteSpace(this.CurrentUserInfo.ClientDistributorID) ? "-1" : this.CurrentUserInfo.ClientDistributorID) + " ");
            List<VisitingTaskDataOrderSKUViewEntity> result = new List<VisitingTaskDataOrderSKUViewEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskDataOrderSKUViewEntity visitingTaskDataOrderSKUViewEntity = new VisitingTaskDataOrderSKUViewEntity();
                    //数据转换成ViewEntity

                    if (rdr["OrdersNo"] != DBNull.Value)
                    {
                        visitingTaskDataOrderSKUViewEntity.OrdersNo = Convert.ToString(rdr["OrdersNo"]);
                    }
                    if (rdr["OrdersPrice"] != DBNull.Value)
                    {
                        visitingTaskDataOrderSKUViewEntity.OrdersPrice = Convert.ToDecimal(rdr["OrdersPrice"]);
                    }
                    if (rdr["OrdersTypeName"] != DBNull.Value)
                    {
                        visitingTaskDataOrderSKUViewEntity.OrdersTypeName = Convert.ToString(rdr["OrdersTypeName"]);
                    }
                    if (rdr["Quantity"] != DBNull.Value)
                    {
                        visitingTaskDataOrderSKUViewEntity.Quantity = Convert.ToDecimal(rdr["Quantity"]);
                    }
                    if (rdr["SKUName"] != DBNull.Value)
                    {
                        visitingTaskDataOrderSKUViewEntity.SKUName = Convert.ToString(rdr["SKUName"]);
                    }
                    if (rdr["SKUNo"] != DBNull.Value)
                    {
                        visitingTaskDataOrderSKUViewEntity.SKUNo = Convert.ToString(rdr["SKUNo"]);
                    }
                    if (rdr["TotalAmout"] != DBNull.Value)
                    {
                        visitingTaskDataOrderSKUViewEntity.TotalAmout = Convert.ToDecimal(rdr["TotalAmout"]);
                    }
                    result.Add(visitingTaskDataOrderSKUViewEntity);
                }
            }
            return result;
        }
        #endregion

        #region GetPOPInfo_Store
        public DataSet GetPOPInfo_Store(Guid storeid)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
select A.StoreName,A.Addr,B.ChannelName,c.ChainName,A.Manager,A.Tel
from Store A
LEFT JOIN dbo.Channel B ON A.ChannelID=B.ChannelID
LEFT JOIN dbo.Chain C ON a.ChainID=c.ChainID
where A.ClientID='{0}' and isnull(A.ClientDistributorID,0)={1} and A.storeid='{2}' ",
CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID, storeid);
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion

        #region GetPOPInfo_Distributor
        public DataSet GetPOPInfo_Distributor(int disid)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
SELECT A.Distributor,A.Addr,a.Tel,A.Manager
FROM dbo.Distributor A
where A.ClientID='{0}' and isnull(A.ClientDistributorID,0)={1} and A.DistributorID={2} ",
CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID, disid);
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion

        #region GetColumnNameByLang
        private Dictionary<string, string> GetColumnNameByLang(Dictionary<string, string> pColumnNameMapping)
        {
            Dictionary<string, string> dictNewColumnName = new Dictionary<string, string>();
            if (this.CurrentUserInfo.Lang == 2)
            {
                foreach (var item in pColumnNameMapping)
                {
                    dictNewColumnName.Add(item.Key, item.Value + "En");
                }
                return dictNewColumnName;
            }
            else
            {
                return pColumnNameMapping;
            }
        }
        #endregion

        //tiansheng.zhu@jitmarketing.cn
        #region GetVisitingTaskPhoto
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pQueryParams"></param>
        /// <returns></returns>
        public DataSet GetVisitingTaskPhoto(Dictionary<string, object> pQueryParams)
        {
            #region 查询条件
            StringBuilder photoWhere = new StringBuilder();
            bool isInOutPic = false;
            bool isAll = true;
            //判断是否是进店数据
            if (pQueryParams.ContainsKey("IsInOutPic"))
            {
                if (pQueryParams["IsInOutPic"].ToString().Trim() == "1")
                {
                    isInOutPic = true;
                }
            }
            //部门查询条件
            if (pQueryParams.ContainsKey("ClientStructureID"))
            {
                photoWhere.Append(" and ClientStructureID ='" + pQueryParams["ClientStructureID"].ToString() + "' ");
            }

            //执行人员名称查询条件
            if (pQueryParams.ContainsKey("ClientUserName"))
            {
                photoWhere.Append(" and ClientUserName  like '%" + pQueryParams["ClientUserName"].ToString().Replace("'", "") + "%' ");
            }

            //开始时间 查询条件
            if (pQueryParams.ContainsKey("DateFrom"))
            {
                photoWhere.Append(" and datediff(DAY,'" + pQueryParams["DateFrom"].ToString() + "',InTime )>=0 ");
            }

            //结束时间  查询条件
            if (pQueryParams.ContainsKey("DateTo"))
            {
                photoWhere.Append(" and datediff(DAY,'" + pQueryParams["DateTo"].ToString() + "',InTime )<=0 ");
            }

            //拜访任务查询条件
            if (pQueryParams.ContainsKey("VisitingTaskID"))
            {
                photoWhere.Append(" and VisitingTaskID='" + pQueryParams["VisitingTaskID"].ToString() + "' ");
            }

            //终端名称查询条件
            if (pQueryParams.ContainsKey("POPName"))
            {
                photoWhere.Append(" and POPName  like '%" + pQueryParams["POPName"].ToString().Replace("'", "") + "%' ");
            }

            //拜访步骤查询条件
            if (pQueryParams.ContainsKey("VisitingTaskStepID"))
            {
                photoWhere.Append(" and VisitingTaskStepID='" + pQueryParams["VisitingTaskStepID"].ToString() + "' ");
                isAll = false;
            }

            //拜访对象名称查询条件
            if (pQueryParams.ContainsKey("ObjectName"))
            {
                photoWhere.Append(" and ObjectName  like '%" + pQueryParams["ObjectName"].ToString().Replace("'", "") + "%' ");
                isAll = false;
            }

            //职位查询条件
            if (pQueryParams.ContainsKey("ClientPositionID"))
            {
                photoWhere.Append(" and ClientPositionID='" + pQueryParams["ClientPositionID"].ToString() + "' ");
            }
            #endregion

            //主表sql
            StringBuilder strsql = new StringBuilder();
            StringBuilder strsqltwo = new StringBuilder();
            StringBuilder sql = new StringBuilder();

            #region 判断是否是经销商登录
            string strsqlDistributor = string.Format(@"  
            union all
            select convert(nvarchar(50),DistributorID) as POPID,Distributor as StoreName,Coordinate 
            from Distributor where  ClientID='{0}'", this.CurrentUserInfo.ClientID);
            if (this.CurrentUserInfo.ClientDistributorID != "0")
            {
                strsqlDistributor = "";
            }
             strsqlDistributor = "";
            #endregion

            #region 查询主表VisitingTaskData 数据
             strsql.AppendFormat(@"select 
vdd.VisitingTaskDataID,vd.ClientUserID,cu.user_Name  as ClientUserName,'0' as clientpositionid,
                                    '' as ClientStructureID,vd.POPID,'StoreName' as POPName,null as InPic,null as OutPic,vdd.Value as WithinPic,
                                    vd.InTime,vd.InCoordinate,vd.OutCoordinate,vt.VisitingTaskID,vt.VisitingTaskName as TaskName,
                                    vdd.VisitingTaskStepID,vts.StepName as StepName,vp.ParameterName as PhotoName,
                                    '' as ObjectName,'PositonName' as PositionName
                                    from  VisitingTaskDetailData vdd
                                    inner join VisitingParameter as vp
                                    on vp.VisitingParameterID=vdd.VisitingParameterID  and  isnull(vp.ClientDistributorID,0)=0
                                    inner join VisitingTaskData as vd 
                                    on vd.VisitingTaskDataID=vdd.VisitingTaskDataID and isnull(vd.ClientDistributorID,0)=0 and vd.IsDelete=0
                                   
                                    inner join VisitingTaskStep as vts
                                    on vts.VisitingTaskStepID=vdd.VisitingTaskStepID  and vts.VisitingTaskStepID=vdd.VisitingTaskStepID and isnull(vts.ClientDistributorID,0)=0                               
                                    inner join VisitingTask as vt
                                    on vt.VisitingTaskID=vts.VisitingTaskID  and isnull(vt.ClientDistributorID,0)=0 
                                    left join T_User  as cu 
                                    on cu.user_ID=vd.ClientUserID  
                                   ", strsqlDistributor, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID, this.CurrentUserInfo.UserID);
            #endregion
            /*
            #region 查询子表VisitingTaskDetailData 数据
            strsqltwo.AppendFormat(@"select distinct  vdd.VisitingTaskDataID,vd.ClientUserID,cu.Name  as ClientUserName,cu.ClientPositionID,
                                    cu.ClientStructureID,vd.POPID,st.StoreName as POPName,null as InPic,null as OutPic,vdd.Value as WithinPic,
                                    vd.InTime,vd.InCoordinate,vd.OutCoordinate,vt.VisitingTaskID,vt.VisitingTaskName as TaskName,
                                    vdd.VisitingTaskStepID,vts.StepName as StepName,vp.ParameterName as PhotoName,
                                    (obj.Object1Name+'-'+isnull(obj.Object2Name,''))  as ObjectName,cp.PositionName
                                    from  VisitingTaskDetailData vdd
                                    inner join VisitingParameter as vp
                                    on vp.VisitingParameterID=vdd.VisitingParameterID and vp.ControlType=12 and  vp.ClientID='{1}' and isnull(vp.ClientDistributorID,0)={2}
                                    inner join VisitingTaskData as vd 
                                    on vd.VisitingTaskDataID=vdd.VisitingTaskDataID and  vd.ClientID='{1}' and isnull(vd.ClientDistributorID,0)={2} and vd.IsDelete=0
                                    inner join  fnGetClientUser({3},1) SubUsers
                                    on vd.ClientUserID = SubUsers.ClientUserID 
                                    inner join VisitingTaskStep as vts
                                    on vts.VisitingTaskStepID=vdd.VisitingTaskStepID  and vts.VisitingTaskStepID=vdd.VisitingTaskStepID and vts.ClientID='{1}' and isnull(vts.ClientDistributorID,0)={2}                               
                                    inner join VisitingTask as vt
                                    on vt.VisitingTaskID=vts.VisitingTaskID  and vt.ClientID='{1}' and isnull(vt.ClientDistributorID,0)={2} 
                                    inner join ClientUser  as cu 
                                    on cu.ClientUserID=vd.ClientUserID and  cu.ClientID='{1}' and isnull(cu.ClientDistributorID,0)={2}
                                    left join ClientPosition as cp
                                    on cp.ClientPositionID=cu.ClientPositionID and cp.ClientID='{1}' and isnull(cp.ClientDistributorID,0)={2}                                      inner join (select convert(nvarchar(50),StoreID) as POPID,StoreName as StoreName,Coordinate from Store  Where ClientID='{1}' and isnull(ClientDistributorID,0)={2} 
                                     {0}
                                    )as st
                                    on  st.POPID=vd.POPID 
                                    left join  VisitingTaskStepObject  as vtso 
                                    on vtso.ObjectID=vdd.ObjectID and vtso.VisitingTaskStepID=vts.VisitingTaskStepID and vtso.ClientID='{1}' and isnull(vtso.ClientDistributorID,0)={2} 
                                    left join (
                                    select convert(nvarchar(50),SKUID) as Target1ID,SKUName as Object1Name,
                                    null as Target2ID,null as Object2Name,1 as StepType   
                                    from SKU  where ClientID='{1}' and isnull(ClientDistributorID,0)={2}
                                    union all
                                    select  convert(nvarchar(50),BrandID)as Target1ID,BrandName as Object1Name,
                                    null as Target2ID,null as Object2Name,2 as StepType 
                                    from Brand where ClientID='{1}'  and isnull(ClientDistributorID,0)={2}
                                    union all
                                    select distinct  convert(nvarchar(50),A.BrandID) as Target1ID,BrandName as Object1Name,  
                                    convert(nvarchar(50),A.CategoryID) as Target2ID,CategoryName as Object2Name,2 as StepType
                                    from SKU A
                                    inner join Brand B on A.BrandID=B.BrandID  and B.ClientID='{1}' and isnull(B.ClientDistributorID,0)={2}  
                                    inner join Category C on A.CategoryID=C.CategoryID and C.ClientID='{1}'  and isnull(C.ClientDistributorID,0)={2} 
                                    where   A.ClientID='{1}' and isnull(A.ClientDistributorID,0)={2}
                                    union all
                                    select convert(nvarchar(50),CategoryID) as Target1ID,CategoryName as Object1Name,
                                    null as Target2ID,null as Object2Name,3 as StepType 
                                    from Category  where ClientID='{1}' and isnull(ClientDistributorID,0)={2}
                                    union all
                                    select distinct  convert(nvarchar(50),A.CategoryID) as Target1ID,CategoryName as Object1Name,  
                                    convert(nvarchar(50),A.BrandID) as Target2ID,BrandName as Object2Name,3 as StepType
                                    from SKU A
                                    inner join Brand B on A.BrandID=B.BrandID  and B.ClientID='{1}' and isnull(B.ClientDistributorID,0)={2} 
                                    inner join Category C on A.CategoryID=C.CategoryID and C.ClientID='{1}'  and isnull(C.ClientDistributorID,0)={2} 
                                    where   A.ClientID='{1}' and isnull(A.ClientDistributorID,0)={2}
                                    union all
                                    select convert(nvarchar(50),ClientPositionID) as Target1ID,PositionName as Object1Name,
                                    null as Target2ID,null as Object2Name,4 as StepType  from ClientPosition  where ClientID='{1}' and isnull(ClientDistributorID,0)={2} 
                                    union all
                                    select convert(nvarchar(50),OptionValue) as Target1ID,OptionText as Object1Name,
                                    null as Target2ID,null as Object2Name,7 as StepType from options where  OptionName='ObjectGroup' and ClientID='{1}' and isnull(ClientDistributorID,0)={2} 
                                    ) as obj
                                    on obj.Target1ID=vtso.Target1ID and isnull(obj.Target2ID,0)=isnull(vtso.Target2ID,0)  and obj.StepType=vts.StepType
                                    where vdd.ClientID='{1}' and isnull(vdd.ClientDistributorID,0)={2} and vdd.IsDelete=0   and vdd.Value is not null"
                                    , strsqlDistributor, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID, this.CurrentUserInfo.UserID);
            #endregion
            */
            #region 组合的sql查询语句
            sql.Append("select * from (");
            //if (isAll)
            //{
                sql.Append(strsql);
            //}
            //if (!isInOutPic && isAll)
            //{
            //    sql.Append(" union all ");
            //}
            //if (!isInOutPic)
            //{
            //    sql.Append(strsqltwo);
            //}
            sql.Append(") as PD where 1=1 " + photoWhere);
            #endregion
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion


        #region GetVisitingTaskStepSum
        /// <summary>
        /// 查询1．	点击拜访照片查看---〉首先显示的是n个（有多少个拜访步骤+1‘进出店’）类似文件夹的图片，图片上显示3行信息（拜访任务：**拜访任务，拜访步骤：**拜访任务，照片数：1200张），点击进取后再显示目前图片区域内容来查看图片
        /// </summary>   
        /// <returns></returns>
        public DataSet GetVisitingTaskStepSum()
        {
            //主表sql

            StringBuilder sql = new StringBuilder();
            #region 判断是否是经销商登录
            string strsqlDistributor = string.Format(@"  
            union all
            select convert(nvarchar(50),DistributorID) as POPID,Distributor as StoreName,Coordinate 
            from Distributor where  ClientID='{0}' ", this.CurrentUserInfo.ClientID);
            if (this.CurrentUserInfo.ClientDistributorID != "0")
            {
                strsqlDistributor = "";
            }
            #endregion

            #region 查询主表VisitingTaskData 数据
            sql.AppendFormat(@"select vt.VisitingTaskID,vt.VisitingTaskName,vt.CreateTime as TaskCreateTime,
vts.VisitingTaskStepID,vts.StepName,1 as StepPriority,vts.CreateTime as StepCreateTime,
vdd.Value,'' as InPic,'' as OutPic,0 as IsInOutPic
from VisitingTaskDetailData as vdd
inner join VisitingTaskData as vd
on vd.VisitingTaskDataID=vdd.VisitingTaskDataID  and  isnull(vd.ClientDistributorID,0)=0  and vd.IsDelete=0
inner join VisitingParameter as vp
on vp.VisitingParameterID=vdd.VisitingParameterID   and isnull(vp.ClientDistributorID,0)= 0
inner join VisitingTaskStep as vts
on vts.VisitingTaskStepID=vdd.VisitingTaskStepID  and isnull(vts.ClientDistributorID,0)=0
inner join VisitingTask as  vt
on vt.VisitingTaskID=vts.VisitingTaskID and ISNULL(vt.ClientDistributorID,0)=0
order by TaskCreateTime asc,VisitingTaskID asc,StepCreateTime asc ,isinoutpic desc

                ", strsqlDistributor, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.ClientDistributorID, this.CurrentUserInfo.UserID);
            #endregion
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion
    }
}