/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:18:56
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// 业务处理： 拜访数据表 
    /// </summary>
    public partial class VisitingTaskDataBLL
    {
        #region GetVisitingTaskDataList
        /// <summary>
        /// 根据条件分页查询出某人某天的走店明细
        /// </summary>
        /// <param name="pQueryParams">查询参数(部门标识：ClientStructureID，职位标识：ClientPositionID，执行人员标识:ClientUserID,执行日期:ExecutionTime)</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <param name="pPageSize">每页记录数</param>
        /// <param name="pCurrentPageIndex">从1开始的当前页码</param>
        /// <param name="oWorkingHoursIndoor">店内时间合计</param>
        /// <param name="oWorkingHoursJourneyTime">路途时间合计</param>
        /// <param name="oWorkingHoursTotal">总时间合计</param>
        /// <returns>一个业务员一天的走店明细</returns>
        public List<VisitingTaskDataViewEntity> GetVisitingTaskDataList(
            Dictionary<string, object> pQueryParams, 
            OrderBy[] pOrderBys, 
            out int oWorkingHoursIndoor, 
            out int oWorkingHoursJourneyTime, 
            out int oWorkingHoursTotal)
        {
            //BEN：每一页下面都显示所有页的合计（与前一页的总数不同，所以要重新计算。）
            //前一页摘要信息为最后出店-首次进店
            //当前列表页为每一行的值相加（每一行都有可能4舍5入）
            //这样两个合计就会有误差，Ben说这个他可以跟客户解释清楚。
            DateTime dtStart = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "根据条件分页查询出某人某天的走店明细.查询开始：[" + dtStart.ToString() + "]" });

            List<VisitingTaskDataViewEntity> tempResult = this._currentDAO.GetVisitingTaskDataList(pQueryParams, pOrderBys);
            //求合计信息
            oWorkingHoursIndoor = tempResult.Select(m =>
                m.WorkingHoursIndoor.HasValue ? m.WorkingHoursIndoor.Value : 0).Sum();
            oWorkingHoursJourneyTime = tempResult.Select(m =>
                m.WorkingHoursJourneyTime.HasValue ? m.WorkingHoursJourneyTime.Value : 0).Sum();
            oWorkingHoursTotal = tempResult.Select(m =>
                m.WorkingHoursTotal.HasValue ? m.WorkingHoursTotal.Value : 0).Sum();
            
            DateTime dtFinish = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "根据条件分页查询出某人某天的走店明细.查询完成：[" + dtFinish.ToString() + "].花费时间:[" + (dtFinish - dtStart).TotalSeconds.ToString() + "]s" });
            return tempResult;
        }
        #endregion

        #region GetVisitingTaskDetailData
        /// <summary>
        /// 某人某天在某个店的拜访步骤及参数值明细
        /// </summary>
        /// <param name="pQueryParams">查询参数(执行人员:ClientUserID,门店标识:POPID,执行日期:ExecutionTime)</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <param name="pPageSize">每页记录数</param>
        /// <param name="pCurrentPageIndex">从1开始的当前页码</param>
        /// <returns>明细信息</returns>
        public DataSet GetVisitingTaskDetailData(Dictionary<string, object> pQueryParams)
        {
            DateTime dtStart = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的拜访步骤及参数值明细.查询开始：[" + dtStart.ToString() + "]" });
            //多选下拉框的可选值(由于数据库中是以逗号分隔的方式保存多个值，所以此处要取出来处理。)
            Dictionary<string, VisitingParameterOptionsEntity[]> dictParameterOptions = new Dictionary<string, VisitingParameterOptionsEntity[]>();
            DataSet detailData = this._currentDAO.GetVisitingTaskDetailData(pQueryParams);
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的拜访步骤及参数值明细.SQL查询完成：[" + DateTime.Now.ToString() + "]" });
            //DataSet detailData = queryResult.Data;
            #region 计算合计信息

            #region 整理步骤及参数列表
            //1.取出所有Step作为一个DataTable
            DataSet stepDetail = new DataSet();
            Dictionary<string, DataRow> dictSteps = new Dictionary<string, DataRow>();
            foreach (DataRow item in detailData.Tables[0].Rows)
            {
                string visitingTaskStepID = item["VisitingTaskStepID"].ToString();
                if (dictSteps.ContainsKey(visitingTaskStepID))
                    continue;
                dictSteps.Add(visitingTaskStepID, item);
            }

            //2.依次取出每个Step的参数列表所为DataTable的列
            Dictionary<string, Dictionary<string, DataRow>> dictStepParameters = new Dictionary<string, Dictionary<string, DataRow>>();
            foreach (DataRow item in detailData.Tables[0].Rows)
            {
                Dictionary<string, DataRow> dictParameters;
                string visitingTaskStepID = item["VisitingTaskStepID"].ToString();
                string visitingParameterID = item["VisitingParameterID"].ToString();
                if (dictStepParameters.ContainsKey(visitingTaskStepID))
                {
                    dictParameters = (Dictionary<string, DataRow>)dictStepParameters[visitingTaskStepID];
                    if (!dictParameters.ContainsKey(visitingParameterID))
                        dictParameters.Add(visitingParameterID, item);
                }
                else
                {
                    dictParameters = new Dictionary<string, DataRow>();
                    dictParameters.Add(visitingParameterID, item);
                    dictStepParameters.Add(visitingTaskStepID, dictParameters);
                }
            }
            #endregion 整理步骤及参数列表
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的拜访步骤及参数值明细.整理步骤及参数列表完成：[" + DateTime.Now.ToString() + "]" });
            #region 3.DataTable 拜访步骤索引   防止StepName重名，所以以StepID所为DataTable的表名，DataSet的第一个DataTable作为一个索引
            DataTable stepInfo = new DataTable("Step");
            stepInfo.Columns.Add("StepID");
            stepInfo.Columns.Add("StepName");
            stepInfo.Columns.Add("StepType");
            stepInfo.Columns.Add("StepNo");
            stepInfo.Columns.Add("StepPriority");
            foreach (var item in dictSteps)
            {
                DataRow detailDataItem = item.Value;
                DataRow stepItem = stepInfo.NewRow();
                stepItem["StepID"] = item.Key;
                stepItem["StepName"] = detailDataItem["StepName"].ToString();
                stepItem["StepType"] = detailDataItem["StepType"].ToString();
                stepItem["StepNo"] = detailDataItem["StepNo"].ToString();
                stepItem["StepPriority"] = detailDataItem["StepPriority"].ToString();
                stepInfo.Rows.Add(stepItem);
            }
            stepDetail.Tables.Add(stepInfo);
            #endregion
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的拜访步骤及参数值明细.DataTable Parameters定义信息：[" + DateTime.Now.ToString() + "]" });
            #region 4.DataTable Parameters定义信息
            DataTable parameterDefinition = new DataTable("ParameterDefinition");
            parameterDefinition.Columns.Add("ParameterID");
            parameterDefinition.Columns.Add("ParameterName");
            //parameterDefinition.Columns.Add("ParameterNameEn");
            parameterDefinition.Columns.Add("ParameterOrder");
            parameterDefinition.Columns.Add("ControlType");
            parameterDefinition.Columns["ControlType"].Caption = "";
            parameterDefinition.Columns.Add("ControlName");
            parameterDefinition.Columns.Add("jitDataType");//Jit.grid.column.Column
            foreach (var stepItem in dictStepParameters)
            {
                foreach (var columnItem in stepItem.Value)
                {
                    if (string.IsNullOrWhiteSpace(columnItem.Key))
                        continue;//数据库表左联产生的无效的参数
                    DataRow[] columnExists = parameterDefinition.Select("ParameterID='" + columnItem.Key + "'");
                    if (columnExists.Length > 0)
                        continue;
                    DataRow detailDataItem = columnItem.Value;
                    DataRow columnInfo = parameterDefinition.NewRow();
                    columnInfo["ParameterID"] = detailDataItem["VisitingParameterID"].ToString();
                    columnInfo["ParameterName"] = detailDataItem["ParameterName"].ToString();
                    //columnInfo["ParameterNameEn"] = detailDataItem["ParameterNameEn"].ToString();
                    columnInfo["ParameterOrder"] = detailDataItem["ParameterOrder"].ToString();
                    columnInfo["ControlType"] = detailDataItem["ControlType"].ToString();
                    columnInfo["ControlName"] = detailDataItem["ControlName"].ToString();
                    //将ControlType转换成Jit.grid.column.Column的jitDataType         

                    switch (detailDataItem["ControlType"].ToString())
                    {
                        case "1":  //1	字符-文本框
                            columnInfo["jitDataType"] = "String";
                            break;
                        case "2":    //2	整数-文本框
                            columnInfo["jitDataType"] = "Int";
                            break;
                        case "3": //3	小数-文本框
                            columnInfo["jitDataType"] = "Decimal";
                            break;
                        case "4"://4	多行文本框
                            columnInfo["jitDataType"] = "String";
                            break;
                        case "5"://5	单选下拉框
                            columnInfo["jitDataType"] = "String";
                            break;
                        case "6"://6	多选下拉框
                            columnInfo["jitDataType"] = "String";

                            VisitingParameterOptionsDAO visitingParameterOptionsDAO = new VisitingParameterOptionsDAO(this.CurrentUserInfo);
                            VisitingParameterOptionsEntity visitingParameterOptionsQueryEntity = new VisitingParameterOptionsEntity();
                            visitingParameterOptionsQueryEntity.OptionName = detailDataItem["ControlName"].ToString();
                            visitingParameterOptionsQueryEntity.IsDelete = null;
                            dictParameterOptions.Add(detailDataItem["VisitingParameterID"].ToString(), visitingParameterOptionsDAO.QueryByEntityWithOutIsDelete(visitingParameterOptionsQueryEntity, null));
                            break;
                        case "7"://7	勾选(checkbox)
                            columnInfo["jitDataType"] = "Boolean";
                            break;
                        case "8"://8	日期
                            columnInfo["jitDataType"] = "Date";
                            break;
                        case "9"://9	时间
                            columnInfo["jitDataType"] = "Time";
                            break;
                        case "10"://10	日期时间
                            columnInfo["jitDataType"] = "DateTime";
                            break;
                        case "11"://11	定位
                            columnInfo["jitDataType"] = "Coordinate";
                            break;
                        case "12"://12	拍照
                            columnInfo["jitDataType"] = "Photo";
                            break;
                        default:
                            break;
                    }

                    parameterDefinition.Rows.Add(columnInfo);
                }
            }
            stepDetail.Tables.Add(parameterDefinition);
            #endregion
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的拜访步骤及参数值明细.创建DataSet中每个步骤表的结构：[" + DateTime.Now.ToString() + "]" });
            #region 6.创建DataSet中每个步骤表的结构
            foreach (var item in dictStepParameters)
            {
                DataTable step = new DataTable(item.Key);
                step.Columns.Add("ClientUserID");
                step.Columns["ClientUserID"].Caption = "";
                step.Columns.Add("InTime");
                step.Columns["InTime"].Caption = "进店时间";
                step.Columns.Add("VisitingTaskDataID");
                step.Columns.Add("VisitingTaskStepID");
                step.Columns.Add("ObjectID");
                step.Columns["ObjectID"].Caption = "";
                step.Columns.Add("Target1ID");
                step.Columns.Add("Target2ID");
                step.Columns.Add("ObjectName1");
                step.Columns.Add("ObjectName2");
                foreach (var param in item.Value)
                {
                    if (string.IsNullOrWhiteSpace(param.Key))
                        continue;
                    DataColumn newColumn = step.Columns.Add(param.Key, typeof(string));
                    newColumn.Caption = param.Value["ParameterName"].ToString();
                    //DataColumn newColumnEn = step.Columns.Add(param.Key + "En", typeof(string));
                    //newColumnEn.Caption = param.Value["ParameterNameEn"].ToString();
                }
                DataRow[] drsStepInfo = stepDetail.Tables["Step"].Select("StepID='" + item.Key + "'");
                int stepType = Convert.ToInt32(drsStepInfo[0]["StepType"]);
                switch (stepType)
                {
                    case 1:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "商品名称";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "";
                        break;
                    case 2:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "品牌名称";

                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "品类名称";
                        break;
                    case 3:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "品类名称";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "品牌名称";
                        break;
                    case 4:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "职位名称";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "人员名称";
                        break;
                    case 5://问卷（仅显示 题目及答案两列）
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "";
                        break;
                    case 7:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "拜访对象";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "";
                        break;
                    default:
                        break;
                }
                stepDetail.Tables.Add(step);
            }
            string colName = string.Empty;
            foreach (DataColumn item in stepDetail.Tables[0].Columns)
            {
                colName += item.Caption + ",";
            }
            #endregion
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的拜访步骤及参数值明细.填充DataTable的数据：[" + DateTime.Now.ToString() + "]" });
            #region 7.填充DataTable的数据
            foreach (DataRow item in detailData.Tables[0].Rows)
            {
                string visitingTaskDetailDataID = item["VisitingTaskDetailDataID"].ToString();
                string controlType = item["ControlType"].ToString();
                int stepType = (int)item["StepType"];
                if (stepType != 5 && stepType != 7 && string.IsNullOrWhiteSpace(visitingTaskDetailDataID))
                    continue;
                string visitingTaskDataID = item["VisitingTaskDataID"].ToString();
                string visitingTaskStepID = item["VisitingTaskStepID"].ToString();
                string clientUserID = item["ClientUserID"].ToString();
                string inTime = item["InTime"].ToString();
                string objectID = item["ObjectID"].ToString();
                string target1ID = item["Target1ID"].ToString();
                string target2ID = item["Target2ID"].ToString();
                string objectName1 = item["ObjectName1"].ToString();
                string objectName2 = item["ObjectName2"].ToString();
                string parameterID = item["VisitingParameterID"].ToString();
                string value = item["Value"].ToString();
                if (controlType == "7")
                {
                    if (value == "1")
                        value = "是";
                    else
                        value = "否";
                }
                //替换复选框的值
                if (controlType == "6")
                {
                    if (!dictParameterOptions.ContainsKey(parameterID))
                    {
                        value = "错误数据(未找到相应的显示值)[" + value + "]";
                    }
                    else
                    {
                        string[] selectedItemIds = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //value = "错误数据(未找到相应的显示值)[" + value + "]";
                        string values = string.Empty;
                        foreach (var selectedItemId in selectedItemIds)
                        {
                            //var plm = pEditValue.ToArray().Where(i => i.ControlType != 23).ToList();
                            var optionInfo = dictParameterOptions[parameterID].Where(opt => opt.OptionValue.Value == Convert.ToInt32(selectedItemId)).ToList();
                            if (optionInfo == null || optionInfo.Count == 0)
                            {
                                values += "无效数据[" + selectedItemId + "],";
                            }
                            else
                            {
                                values += optionInfo[0].OptionText + ",";
                            }
                        }
                        if (values.EndsWith(","))
                            values = values.Substring(0, values.Length - 1);
                        value = values;
                    }
                }
                //string valueEn = item["ValueEn"].ToString();
                if (!stepDetail.Tables.Contains(visitingTaskStepID))
                {
                    Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "获取拜访步骤详细参数:未找到相应的步骤标识[" + visitingTaskStepID + "]" });
                    continue;
                }
                //按拜访任务标识(VisitingTaskDataID)，步骤标识(VisitingTaskStepID)，对象（Target1ID，Target2ID）分行组织
                DataRow[] parameterDetail = stepDetail.Tables[visitingTaskStepID].Select("VisitingTaskDataID='" + visitingTaskDataID + "' and VisitingTaskStepID='" + visitingTaskStepID + "' and ObjectID='" + objectID + "' and Target1ID='" + target1ID + "' and Target2ID='" + target2ID + "'");
                if (parameterDetail.Length == 0)
                {//添加新行
                    DataRow newParameterDetail = stepDetail.Tables[visitingTaskStepID].NewRow();
                    newParameterDetail["ClientUserID"] = clientUserID;
                    newParameterDetail["InTime"] = inTime;
                    newParameterDetail["ObjectID"] = objectID;
                    newParameterDetail["Target1ID"] = target1ID;
                    newParameterDetail["Target2ID"] = target2ID;
                    newParameterDetail["VisitingTaskDataID"] = visitingTaskDataID;
                    newParameterDetail["VisitingTaskStepID"] = visitingTaskStepID;
                    newParameterDetail["ObjectName1"] = objectName1;
                    newParameterDetail["ObjectName2"] = objectName2;
                    newParameterDetail[parameterID] = value;
                    //newParameterDetail[parameterID + "En"] = valueEn;
                    stepDetail.Tables[visitingTaskStepID].Rows.Add(newParameterDetail);
                }
                else
                {
                    //修改现有行中的单元格数据
                    parameterDetail[0][parameterID] = value;
                    //parameterDetail[0][parameterID + "En"] = valueEn;
                }
            }

            #endregion 填充DataTable的数据
            #region 8.将问卷(StepType=5)及自定义步骤(StepType=7)的数据进行行列转换

            #endregion  8.将问卷(StepType=5)及自定义步骤(StepType=7)的数据进行行列转换
            foreach (DataRow stepItem in stepDetail.Tables["step"].Rows)
            {
                switch (stepItem["StepType"].ToString())
                {
                    case "2":// 处理品牌步骤中品类名称列的隐藏
                        DataTable tempStep2 = stepDetail.Tables[stepItem["StepID"].ToString()];
                        DataRow[] drsObject2 = tempStep2.Select("Target2ID is not null and Target2ID<>''");
                        if (drsObject2.Length > 0)
                            tempStep2.Columns["ObjectName2"].Caption = "品类名称";
                        else
                            tempStep2.Columns["ObjectName2"].Caption = "";
                        break;
                    case "3"://处理品类步骤中品牌名称列的隐藏                        
                        DataTable tempStep3 = stepDetail.Tables[stepItem["StepID"].ToString()];
                        DataRow[] drsObject3 = tempStep3.Select("Target2ID is not null and Target2ID<>''");
                        if (drsObject3.Length > 0)
                            tempStep3.Columns["ObjectName2"].Caption = "品牌名称";
                        else
                            tempStep3.Columns["ObjectName2"].Caption = "";
                        break;

                    case "4"://处理人员考评步骤中人员姓名列的隐藏                        
                        DataTable tempStep4 = stepDetail.Tables[stepItem["StepID"].ToString()];
                        DataRow[] drsObject4 = tempStep4.Select("Target2ID is not null and Target2ID<>''");
                        if (drsObject4.Length > 0)
                            tempStep4.Columns["ObjectName2"].Caption = "人员名称";
                        else
                            tempStep4.Columns["ObjectName2"].Caption = "";
                        break;
                    case "5"://问卷类型的步骤
                        DataTable tempStep5 = stepDetail.Tables[stepItem["StepID"].ToString()].Clone();
                        while (tempStep5.Columns.Count > 9)
                        {
                            tempStep5.Columns.RemoveAt(9);
                        }
                        tempStep5.Columns.Add("参数", typeof(string));
                        tempStep5.Columns.Add("值", typeof(string));
                        tempStep5.Columns.Add("ControlType");
                        tempStep5.Columns["ControlType"].Caption = "";
                        tempStep5.AcceptChanges();
                        foreach (DataRow detailDataRow in stepDetail.Tables[stepItem["StepID"].ToString()].Rows)
                        {
                            //列转换成行
                            for (int i = 9; i < stepDetail.Tables[stepItem["StepID"].ToString()].Columns.Count; i++)
                            {
                                DataRow datarowItem = tempStep5.NewRow();
                                //固定的前9列
                                datarowItem[0] = detailDataRow[0];
                                datarowItem[1] = detailDataRow[1];
                                datarowItem[2] = detailDataRow[2];
                                datarowItem[3] = detailDataRow[3];
                                datarowItem[4] = detailDataRow[4];
                                datarowItem[5] = detailDataRow[5];
                                datarowItem[6] = detailDataRow[6];
                                datarowItem[7] = detailDataRow[7];
                                datarowItem[8] = detailDataRow[8];
                                //动态的列
                                DataRow[] parameterInfo = stepDetail.Tables["ParameterDefinition"].Select("ParameterID='" + stepDetail.Tables[stepItem["StepID"].ToString()].Columns[i].ColumnName + "'");
                                if (parameterInfo != null && parameterInfo.Length > 0)
                                {
                                    //动态列1.题目
                                    datarowItem[9] = parameterInfo[0]["ParameterName"].ToString();
                                    //动态列2.答案
                                    datarowItem[10] = detailDataRow[i].ToString();
                                    //动态列3.控件类型
                                    datarowItem[11] = parameterInfo[0]["ControlType"].ToString();
                                }
                                tempStep5.Rows.Add(datarowItem);
                            }
                        }
                        stepDetail.Tables.Remove(stepItem["StepID"].ToString());
                        stepDetail.Tables.Add(tempStep5);

                        break;

                    case "7"://自定义类型的步骤
                        DataTable tempStep7 = stepDetail.Tables[stepItem["StepID"].ToString()].Clone();
                        while (tempStep7.Columns.Count > 9)
                        {
                            tempStep7.Columns.RemoveAt(9);
                        }
                        tempStep7.Columns.Add("参数", typeof(string));
                        tempStep7.Columns.Add("值", typeof(string));
                        tempStep7.Columns.Add("ControlType");
                        tempStep7.Columns["ControlType"].Caption = "";
                        tempStep7.AcceptChanges();
                        foreach (DataRow detailDataRow in stepDetail.Tables[stepItem["StepID"].ToString()].Rows)
                        {
                            //列转换成行
                            for (int i = 9; i < stepDetail.Tables[stepItem["StepID"].ToString()].Columns.Count; i++)
                            {
                                DataRow datarowItem = tempStep7.NewRow();
                                //固定的前9列
                                datarowItem[0] = detailDataRow[0];
                                datarowItem[1] = detailDataRow[1];
                                datarowItem[2] = detailDataRow[2];
                                datarowItem[3] = detailDataRow[3];
                                datarowItem[4] = detailDataRow[4];
                                datarowItem[5] = detailDataRow[5];
                                datarowItem[6] = detailDataRow[6];
                                datarowItem[7] = detailDataRow[7];
                                datarowItem[8] = detailDataRow[8];
                                //动态的列
                                DataRow[] parameterInfo = stepDetail.Tables["ParameterDefinition"].Select("ParameterID='" + stepDetail.Tables[stepItem["StepID"].ToString()].Columns[i].ColumnName + "'");
                                if (parameterInfo != null && parameterInfo.Length > 0)
                                {
                                    //动态列1.题目
                                    datarowItem[9] = parameterInfo[0]["ParameterName"].ToString();
                                    //动态列2.答案
                                    datarowItem[10] = detailDataRow[i].ToString();
                                    //动态列3.控件类型
                                    datarowItem[11] = parameterInfo[0]["ControlType"].ToString();
                                }
                                tempStep7.Rows.Add(datarowItem);
                            }
                        }
                        stepDetail.Tables.Remove(stepItem["StepID"].ToString());
                        stepDetail.Tables.Add(tempStep7);

                        break;
                    default:
                        break;
                }
            }
            #endregion
            //queryResult.Data = stepDetail;
            DateTime dtFinish = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的拜访步骤及参数值明细.查询完成：[" + dtFinish.ToString() + "].花费时间:[" + (dtFinish - dtStart).TotalSeconds.ToString() + "]s" });
            //stepDetail.Tables[4].Rows.Add(stepDetail.Tables[4].NewRow());
            return stepDetail;
        }
        #endregion
        

        
        #region GetVisitingTaskPictures
        /// <summary>
        /// 某人某天在某个店的相关照片
        /// </summary>
        /// <param name="pQueryParams">查询参数(部门标识：ClientStructureID，职位标识：ClientPositionID，执行人员标识:ClientUserID,拜访任务执行标识：VisitingTaskDataID，拜访步骤标识：VisitingTaskStepID，开始日期：DateFrom，截止日期：DateTo)</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <param name="pPageSize">每页记录数</param>
        /// <param name="pCurrentPageIndex">从1开始的当前页码</param>
        /// <returns>明细信息</returns>
        public PagedQueryResult<VisitingTaskPicturesViewEntity> GetVisitingTaskPictures(Dictionary<string, object> pQueryParams, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            DateTime dtStart = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的相关照片.查询开始：[" + dtStart.ToString() + "]" });
            var result = this._currentDAO.GetVisitingTaskPictures(pQueryParams, pOrderBys, pPageSize, pCurrentPageIndex);
            DateTime dtFinish = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "某人某天在某个店的相关照片.查询完成：[" + dtFinish.ToString() + "].花费时间:[" + (dtFinish - dtStart).TotalSeconds.ToString() + "]s" });
            return result;
        }
        #endregion


        #region GetVisitingTaskDataInfo
        /// <summary>
        /// 根据条件查询出某人某天在某店的走店信息
        /// </summary>
        /// <param name="pClientUserID">执行人员标识</param>
        /// <param name="pPOPID">门店标识</param>
        /// <param name="pExecutionTime">执行日期</param>
        /// <returns>一个人店天的走店</returns>
        public VisitingTaskDataViewEntity GetVisitingTaskDataInfo(string pClientUserID, string pPOPID, string pExecutionTime)
        {
            Dictionary<string, object> pQueryParams = new Dictionary<string, object>();
            if (pClientUserID != null)
                pQueryParams.Add("ClientUserID", pClientUserID);
            if (pPOPID != null)
                pQueryParams.Add("POPID", pPOPID);
            if (pExecutionTime != null)
                pQueryParams.Add("ExecutionTime", pExecutionTime);

            List<VisitingTaskDataViewEntity> result = this._currentDAO.GetVisitingTaskDataList(pQueryParams, null);
            if (result.Count == 0)
                return null;
            return result.ToArray()[0];
        }
        #endregion

        #region GetOrderSKUList
        /// <summary>
        /// 根据人、店、天 获取订单的商品详情
        /// </summary>
        /// <param name="pUserID">用户标识</param>
        /// <param name="pPopID">门店标识</param>
        /// <param name="pDate">订单日期</param>
        /// <returns>订单中的所有商品信息</returns>
        public List<VisitingTaskDataOrderSKUViewEntity> GetOrderSKUList(string pUserID, string pPopID, DateTime pDate)
        {
            return this._currentDAO.GetOrderSKUList(pUserID, pPopID, pDate);
        }
        #endregion

        #region GetPOPInfo_Store
        public DataSet GetPOPInfo_Store(Guid storeid)
        {
            return this._currentDAO.GetPOPInfo_Store(storeid);
        }
        #endregion

        #region GetPOPInfo_Distributor
        public DataSet GetPOPInfo_Distributor(int disid)
        {
            return this._currentDAO.GetPOPInfo_Distributor(disid);
        }
        #endregion

        
        #region GetVisitingTaskPictures
        /// <summary>
        /// 某人某天在某个店的相关照片
        /// </summary>
        /// <param name="pQueryParams">查询参数(部门标识：ClientStructureID，职位标识：ClientPositionID，执行人员标识:ClientUserID,拜访任务执行标识：VisitingTaskDataID，拜访步骤标识：VisitingTaskStepID，开始日期：DateFrom，截止日期：DateTo)</param>
        /// <returns>明细信息</returns>
        public VisitingTaskPhotoShowViewEntity[] GetVisitingTaskPhoto(Dictionary<string, object> pQueryParams, int pPageSize, int pPageIndex, out int SumCount)
        {
            SumCount = 0;         
            List<VisitingTaskPhotoShowViewEntity> photoList = new List<VisitingTaskPhotoShowViewEntity>();
            DataSet ds = this._currentDAO.GetVisitingTaskPhoto(pQueryParams);
            VisitingTaskPhotoDataViewEntity[] dataEntity;
            string InPic = "";
            string OutPic = "";
            string WithinPic = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InPic = ds.Tables[0].Rows[i]["InPic"].ToString();
                    OutPic = ds.Tables[0].Rows[i]["OutPic"].ToString();
                    WithinPic = ds.Tables[0].Rows[i]["WithinPic"].ToString();

                    #region 查询照片信息添加到List中
                    if (!string.IsNullOrEmpty(InPic))
                    {
                        if (InPic.IndexOf("[") == 0)
                        {
                            try
                            {
                                dataEntity = InPic.DeserializeJSONTo<VisitingTaskPhotoDataViewEntity[]>();
                            }
                            catch (Exception)
                            {
                                dataEntity = null;
                            }

                            if (dataEntity != null && dataEntity.Length > 0)
                            {
                                for (int j = 0; j < dataEntity.Length; j++)
                                {
                                    PhotoListInsert(photoList, dataEntity[j], 1, i, ds);
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(OutPic))
                    {
                        if (OutPic.IndexOf("[") == 0)
                        {
                            try
                            {
                                dataEntity = OutPic.DeserializeJSONTo<VisitingTaskPhotoDataViewEntity[]>();
                            }
                            catch (Exception)
                            {
                                dataEntity = null;
                            }

                            if (dataEntity != null && dataEntity.Length > 0)
                            {
                                for (int j = 0; j < dataEntity.Length; j++)
                                {
                                    PhotoListInsert(photoList, dataEntity[j], 2, i, ds);
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(WithinPic))
                    {
                        if (WithinPic.IndexOf("[") == 0)
                        {
                            try
                            {
                                dataEntity = WithinPic.DeserializeJSONTo<VisitingTaskPhotoDataViewEntity[]>();
                            }
                            catch (Exception)
                            {
                                dataEntity = null;
                            }

                            if (dataEntity != null && dataEntity.Length > 0)
                            {
                                for (int j = 0; j < dataEntity.Length; j++)
                                {
                                    PhotoListInsert(photoList, dataEntity[j], 3, i, ds);
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            #region 排序，按日期 终端名称 时间排序
            VisitingTaskPhotoShowViewEntity[] PhotoDatas = photoList.ToArray();
            PhotoDatas = photoList.ToArray().OrderByDescending(c => c.PhotoDate).ThenBy(c => c.POPName).ThenByDescending(c => c.PhotoTime).ToArray();
            //PhotoDatas = PhotoDatas.Skip(5).Take(5).ToArray(); 取6-10的数据
            SumCount = PhotoDatas.Length;   //计算总数量
            PhotoDatas = PhotoDatas.Skip(0).Take(1000).ToArray();         
            #endregion
            return PhotoDatas;
        }

        /// <summary>
        /// 将照片信息添加到List中
        /// </summary>
        /// <param name="pEntity">显示照片的对象</param>
        /// <param name="photoList">显示照片的List</param>
        /// <param name="pPhotoData">照片的对象</param>
        public void PhotoListInsert(List<VisitingTaskPhotoShowViewEntity> photoList, VisitingTaskPhotoDataViewEntity pPhotoData, int pType, int i, DataSet ds)
        {
            if (!string.IsNullOrEmpty(pPhotoData.FileName))
            {
                #region 照片显示对象
                VisitingTaskPhotoShowViewEntity entity = new VisitingTaskPhotoShowViewEntity();
                entity.ClientUserName = ds.Tables[0].Rows[i]["ClientUserName"].ToString();
                entity.InCoordinate = ds.Tables[0].Rows[i]["InCoordinate"].ToString();
                entity.ObjectName = ds.Tables[0].Rows[i]["ObjectName"].ToString();
                entity.PositionName = ds.Tables[0].Rows[i]["PositionName"].ToString();
                entity.OutCoordinate = ds.Tables[0].Rows[i]["OutCoordinate"].ToString();
                entity.PhotoName = ds.Tables[0].Rows[i]["PhotoName"].ToString();
                entity.POPName = ds.Tables[0].Rows[i]["POPName"].ToString();
                entity.StepName = ds.Tables[0].Rows[i]["StepName"].ToString();
                entity.TaskName = ds.Tables[0].Rows[i]["TaskName"].ToString();
                entity.ClientUserID =ds.Tables[0].Rows[i]["ClientUserID"].ToString();
                if (pType == 1) {
                    entity.PhotoName = "进店照";                 
                    entity.StepName = "进店";
                }
                else if (pType == 2)
                {
                    entity.PhotoName = "出店照";                  
                    entity.StepName = "出店";
                }
                if (entity.ObjectName.Length > 0 && entity.ObjectName.LastIndexOf("-") >= 0)
                {
                    entity.ObjectName = entity.ObjectName.Substring(0, entity.ObjectName.Length - 1);
                }
                #endregion
                entity.PhotoUrl = pPhotoData.FileName;
                entity.PhotoDate = Convert.ToDateTime(pPhotoData.Date.ToShortDateString());
                entity.PhotoTime = Convert.ToDateTime(pPhotoData.Date.ToLongTimeString());
                entity.PhotoDateTime = pPhotoData.Date;
                photoList.Add(entity);
            }
        }
        #endregion

        #region GetVisitingTaskStepSum
        /// <summary>
        /// 查询客户的任务步骤总照片数量
        /// </summary>
        /// <returns></returns>
        public TaskStepSumViewEntity[] GetVisitingTaskStepSum(out int SumCount)
        {
            SumCount = 0;
            List<TaskStepSumViewEntity> photoList = new List<TaskStepSumViewEntity>();
            DataSet ds = this._currentDAO.GetVisitingTaskStepSum();
            string InPic = "";
            string OutPic = "";
            string WithinPic = "";
            int Sum = 0;
            int Num = 0;
            string VisitingTaskID = "";
            string VisitingTaskStepID = "";
            bool isInsert = false;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    #region 计算照片数量
                    InPic = ds.Tables[0].Rows[i]["InPic"].ToString();
                    if (!string.IsNullOrEmpty(InPic))
                    {
                        if (InPic.IndexOf("[")==0)
                        {
                            try
                            {
                                Num = InPic.DeserializeJSONTo<VisitingTaskPhotoDataViewEntity[]>().Length;
                            }
                            catch (Exception)
                            {
                                Num = 0;
                            }
                           
                            Sum = Sum +Num ;
                        }
                    }
                    OutPic = ds.Tables[0].Rows[i]["OutPic"].ToString();
                    if (!string.IsNullOrEmpty(OutPic))
                    {
                        if (OutPic.IndexOf("[")==0)
                        {
                            try
                            {
                                Num = OutPic.DeserializeJSONTo<VisitingTaskPhotoDataViewEntity[]>().Length;
                            }
                            catch (Exception)
                            {
                                Num = 0;
                            }
                            Sum = Sum + Num;
                        }
                    }
                    WithinPic = ds.Tables[0].Rows[i]["Value"].ToString();
                    if (!string.IsNullOrEmpty(WithinPic))
                    {
                        if (WithinPic.IndexOf("[")==0)
                        {
                            try
                            {
                                Num = WithinPic.DeserializeJSONTo<VisitingTaskPhotoDataViewEntity[]>().Length;
                            }
                            catch (Exception)
                            {
                                Num = 0;
                            }
                            Sum = Sum + Num;
                        }                       
                    }
                    #endregion

                    #region 判断是否是不同的任务和步骤
                    isInsert = false;
                    VisitingTaskID = ds.Tables[0].Rows[i]["VisitingTaskID"].ToString();
                   
                    VisitingTaskStepID = ds.Tables[0].Rows[i]["VisitingTaskStepID"].ToString();  
                    if (i < (ds.Tables[0].Rows.Count - 1))
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["IsInOutPic"].ToString()) == 0)
                        {
                            if (VisitingTaskID != ds.Tables[0].Rows[i + 1]["VisitingTaskID"].ToString() || VisitingTaskStepID != ds.Tables[0].Rows[i + 1]["VisitingTaskStepID"].ToString())
                            {
                                isInsert = true;
                            }
                        }
                        else
                        {
                            if (VisitingTaskID != ds.Tables[0].Rows[i + 1]["VisitingTaskID"].ToString() || Convert.ToInt32(ds.Tables[0].Rows[i + 1]["IsInOutPic"].ToString()) == 0)
                            {
                                isInsert = true;
                            }
                        }
                    }
                    else if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        isInsert = true;
                    }
                    #endregion

                    if (isInsert)
                    {
                        #region 添加 任务步骤总数数据
                        TaskStepSumViewEntity entity = new TaskStepSumViewEntity();
                        entity.VisitingTaskID = (Guid)ds.Tables[0].Rows[i]["VisitingTaskID"];
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["VisitingTaskStepID"].ToString()))
                        {
                            entity.VisitingTaskStepID = (Guid)ds.Tables[0].Rows[i]["VisitingTaskStepID"];
                        }
                        entity.IsInOutPic = Convert.ToInt32(ds.Tables[0].Rows[i]["IsInOutPic"].ToString());
                        entity.VisitingTaskName = ds.Tables[0].Rows[i]["VisitingTaskName"].ToString();
                        entity.StepName = ds.Tables[0].Rows[i]["StepName"].ToString();
                        entity.PicSum = Sum;
                        SumCount = SumCount + Sum;
                        photoList.Add(entity);
                        Sum = 0;
                        #endregion
                    }
                }
            }          
            return photoList.ToArray();
        }
        #endregion
    }
}