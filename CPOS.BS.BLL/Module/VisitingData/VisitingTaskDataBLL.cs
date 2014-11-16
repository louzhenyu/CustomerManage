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
    /// ҵ���� �ݷ����ݱ� 
    /// </summary>
    public partial class VisitingTaskDataBLL
    {
        #region GetVisitingTaskDataList
        /// <summary>
        /// ����������ҳ��ѯ��ĳ��ĳ����ߵ���ϸ
        /// </summary>
        /// <param name="pQueryParams">��ѯ����(���ű�ʶ��ClientStructureID��ְλ��ʶ��ClientPositionID��ִ����Ա��ʶ:ClientUserID,ִ������:ExecutionTime)</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="pPageSize">ÿҳ��¼��</param>
        /// <param name="pCurrentPageIndex">��1��ʼ�ĵ�ǰҳ��</param>
        /// <param name="oWorkingHoursIndoor">����ʱ��ϼ�</param>
        /// <param name="oWorkingHoursJourneyTime">·;ʱ��ϼ�</param>
        /// <param name="oWorkingHoursTotal">��ʱ��ϼ�</param>
        /// <returns>һ��ҵ��Աһ����ߵ���ϸ</returns>
        public List<VisitingTaskDataViewEntity> GetVisitingTaskDataList(
            Dictionary<string, object> pQueryParams, 
            OrderBy[] pOrderBys, 
            out int oWorkingHoursIndoor, 
            out int oWorkingHoursJourneyTime, 
            out int oWorkingHoursTotal)
        {
            //BEN��ÿһҳ���涼��ʾ����ҳ�ĺϼƣ���ǰһҳ��������ͬ������Ҫ���¼��㡣��
            //ǰһҳժҪ��ϢΪ������-�״ν���
            //��ǰ�б�ҳΪÿһ�е�ֵ��ӣ�ÿһ�ж��п���4��5�룩
            //���������ϼƾͻ�����Ben˵��������Ը��ͻ����������
            DateTime dtStart = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "����������ҳ��ѯ��ĳ��ĳ����ߵ���ϸ.��ѯ��ʼ��[" + dtStart.ToString() + "]" });

            List<VisitingTaskDataViewEntity> tempResult = this._currentDAO.GetVisitingTaskDataList(pQueryParams, pOrderBys);
            //��ϼ���Ϣ
            oWorkingHoursIndoor = tempResult.Select(m =>
                m.WorkingHoursIndoor.HasValue ? m.WorkingHoursIndoor.Value : 0).Sum();
            oWorkingHoursJourneyTime = tempResult.Select(m =>
                m.WorkingHoursJourneyTime.HasValue ? m.WorkingHoursJourneyTime.Value : 0).Sum();
            oWorkingHoursTotal = tempResult.Select(m =>
                m.WorkingHoursTotal.HasValue ? m.WorkingHoursTotal.Value : 0).Sum();
            
            DateTime dtFinish = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "����������ҳ��ѯ��ĳ��ĳ����ߵ���ϸ.��ѯ��ɣ�[" + dtFinish.ToString() + "].����ʱ��:[" + (dtFinish - dtStart).TotalSeconds.ToString() + "]s" });
            return tempResult;
        }
        #endregion

        #region GetVisitingTaskDetailData
        /// <summary>
        /// ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ
        /// </summary>
        /// <param name="pQueryParams">��ѯ����(ִ����Ա:ClientUserID,�ŵ��ʶ:POPID,ִ������:ExecutionTime)</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="pPageSize">ÿҳ��¼��</param>
        /// <param name="pCurrentPageIndex">��1��ʼ�ĵ�ǰҳ��</param>
        /// <returns>��ϸ��Ϣ</returns>
        public DataSet GetVisitingTaskDetailData(Dictionary<string, object> pQueryParams)
        {
            DateTime dtStart = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ.��ѯ��ʼ��[" + dtStart.ToString() + "]" });
            //��ѡ������Ŀ�ѡֵ(�������ݿ������Զ��ŷָ��ķ�ʽ������ֵ�����Դ˴�Ҫȡ��������)
            Dictionary<string, VisitingParameterOptionsEntity[]> dictParameterOptions = new Dictionary<string, VisitingParameterOptionsEntity[]>();
            DataSet detailData = this._currentDAO.GetVisitingTaskDetailData(pQueryParams);
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ.SQL��ѯ��ɣ�[" + DateTime.Now.ToString() + "]" });
            //DataSet detailData = queryResult.Data;
            #region ����ϼ���Ϣ

            #region �����輰�����б�
            //1.ȡ������Step��Ϊһ��DataTable
            DataSet stepDetail = new DataSet();
            Dictionary<string, DataRow> dictSteps = new Dictionary<string, DataRow>();
            foreach (DataRow item in detailData.Tables[0].Rows)
            {
                string visitingTaskStepID = item["VisitingTaskStepID"].ToString();
                if (dictSteps.ContainsKey(visitingTaskStepID))
                    continue;
                dictSteps.Add(visitingTaskStepID, item);
            }

            //2.����ȡ��ÿ��Step�Ĳ����б���ΪDataTable����
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
            #endregion �����輰�����б�
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ.�����輰�����б���ɣ�[" + DateTime.Now.ToString() + "]" });
            #region 3.DataTable �ݷò�������   ��ֹStepName������������StepID��ΪDataTable�ı�����DataSet�ĵ�һ��DataTable��Ϊһ������
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
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ.DataTable Parameters������Ϣ��[" + DateTime.Now.ToString() + "]" });
            #region 4.DataTable Parameters������Ϣ
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
                        continue;//���ݿ��������������Ч�Ĳ���
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
                    //��ControlTypeת����Jit.grid.column.Column��jitDataType         

                    switch (detailDataItem["ControlType"].ToString())
                    {
                        case "1":  //1	�ַ�-�ı���
                            columnInfo["jitDataType"] = "String";
                            break;
                        case "2":    //2	����-�ı���
                            columnInfo["jitDataType"] = "Int";
                            break;
                        case "3": //3	С��-�ı���
                            columnInfo["jitDataType"] = "Decimal";
                            break;
                        case "4"://4	�����ı���
                            columnInfo["jitDataType"] = "String";
                            break;
                        case "5"://5	��ѡ������
                            columnInfo["jitDataType"] = "String";
                            break;
                        case "6"://6	��ѡ������
                            columnInfo["jitDataType"] = "String";

                            VisitingParameterOptionsDAO visitingParameterOptionsDAO = new VisitingParameterOptionsDAO(this.CurrentUserInfo);
                            VisitingParameterOptionsEntity visitingParameterOptionsQueryEntity = new VisitingParameterOptionsEntity();
                            visitingParameterOptionsQueryEntity.OptionName = detailDataItem["ControlName"].ToString();
                            visitingParameterOptionsQueryEntity.IsDelete = null;
                            dictParameterOptions.Add(detailDataItem["VisitingParameterID"].ToString(), visitingParameterOptionsDAO.QueryByEntityWithOutIsDelete(visitingParameterOptionsQueryEntity, null));
                            break;
                        case "7"://7	��ѡ(checkbox)
                            columnInfo["jitDataType"] = "Boolean";
                            break;
                        case "8"://8	����
                            columnInfo["jitDataType"] = "Date";
                            break;
                        case "9"://9	ʱ��
                            columnInfo["jitDataType"] = "Time";
                            break;
                        case "10"://10	����ʱ��
                            columnInfo["jitDataType"] = "DateTime";
                            break;
                        case "11"://11	��λ
                            columnInfo["jitDataType"] = "Coordinate";
                            break;
                        case "12"://12	����
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
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ.����DataSet��ÿ�������Ľṹ��[" + DateTime.Now.ToString() + "]" });
            #region 6.����DataSet��ÿ�������Ľṹ
            foreach (var item in dictStepParameters)
            {
                DataTable step = new DataTable(item.Key);
                step.Columns.Add("ClientUserID");
                step.Columns["ClientUserID"].Caption = "";
                step.Columns.Add("InTime");
                step.Columns["InTime"].Caption = "����ʱ��";
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
                        step.Columns["ObjectName1"].Caption = "��Ʒ����";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "";
                        break;
                    case 2:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "Ʒ������";

                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "Ʒ������";
                        break;
                    case 3:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "Ʒ������";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "Ʒ������";
                        break;
                    case 4:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "ְλ����";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "��Ա����";
                        break;
                    case 5://�ʾ�����ʾ ��Ŀ�������У�
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "";
                        step.Columns["Target2ID"].Caption = "";
                        step.Columns["ObjectName2"].Caption = "";
                        break;
                    case 7:
                        step.Columns["Target1ID"].Caption = "";
                        step.Columns["ObjectName1"].Caption = "�ݷö���";
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
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ.���DataTable�����ݣ�[" + DateTime.Now.ToString() + "]" });
            #region 7.���DataTable������
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
                        value = "��";
                    else
                        value = "��";
                }
                //�滻��ѡ���ֵ
                if (controlType == "6")
                {
                    if (!dictParameterOptions.ContainsKey(parameterID))
                    {
                        value = "��������(δ�ҵ���Ӧ����ʾֵ)[" + value + "]";
                    }
                    else
                    {
                        string[] selectedItemIds = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //value = "��������(δ�ҵ���Ӧ����ʾֵ)[" + value + "]";
                        string values = string.Empty;
                        foreach (var selectedItemId in selectedItemIds)
                        {
                            //var plm = pEditValue.ToArray().Where(i => i.ControlType != 23).ToList();
                            var optionInfo = dictParameterOptions[parameterID].Where(opt => opt.OptionValue.Value == Convert.ToInt32(selectedItemId)).ToList();
                            if (optionInfo == null || optionInfo.Count == 0)
                            {
                                values += "��Ч����[" + selectedItemId + "],";
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
                    Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "��ȡ�ݷò�����ϸ����:δ�ҵ���Ӧ�Ĳ����ʶ[" + visitingTaskStepID + "]" });
                    continue;
                }
                //���ݷ������ʶ(VisitingTaskDataID)�������ʶ(VisitingTaskStepID)������Target1ID��Target2ID��������֯
                DataRow[] parameterDetail = stepDetail.Tables[visitingTaskStepID].Select("VisitingTaskDataID='" + visitingTaskDataID + "' and VisitingTaskStepID='" + visitingTaskStepID + "' and ObjectID='" + objectID + "' and Target1ID='" + target1ID + "' and Target2ID='" + target2ID + "'");
                if (parameterDetail.Length == 0)
                {//�������
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
                    //�޸��������еĵ�Ԫ������
                    parameterDetail[0][parameterID] = value;
                    //parameterDetail[0][parameterID + "En"] = valueEn;
                }
            }

            #endregion ���DataTable������
            #region 8.���ʾ�(StepType=5)���Զ��岽��(StepType=7)�����ݽ�������ת��

            #endregion  8.���ʾ�(StepType=5)���Զ��岽��(StepType=7)�����ݽ�������ת��
            foreach (DataRow stepItem in stepDetail.Tables["step"].Rows)
            {
                switch (stepItem["StepType"].ToString())
                {
                    case "2":// ����Ʒ�Ʋ�����Ʒ�������е�����
                        DataTable tempStep2 = stepDetail.Tables[stepItem["StepID"].ToString()];
                        DataRow[] drsObject2 = tempStep2.Select("Target2ID is not null and Target2ID<>''");
                        if (drsObject2.Length > 0)
                            tempStep2.Columns["ObjectName2"].Caption = "Ʒ������";
                        else
                            tempStep2.Columns["ObjectName2"].Caption = "";
                        break;
                    case "3"://����Ʒ�ಽ����Ʒ�������е�����                        
                        DataTable tempStep3 = stepDetail.Tables[stepItem["StepID"].ToString()];
                        DataRow[] drsObject3 = tempStep3.Select("Target2ID is not null and Target2ID<>''");
                        if (drsObject3.Length > 0)
                            tempStep3.Columns["ObjectName2"].Caption = "Ʒ������";
                        else
                            tempStep3.Columns["ObjectName2"].Caption = "";
                        break;

                    case "4"://������Ա������������Ա�����е�����                        
                        DataTable tempStep4 = stepDetail.Tables[stepItem["StepID"].ToString()];
                        DataRow[] drsObject4 = tempStep4.Select("Target2ID is not null and Target2ID<>''");
                        if (drsObject4.Length > 0)
                            tempStep4.Columns["ObjectName2"].Caption = "��Ա����";
                        else
                            tempStep4.Columns["ObjectName2"].Caption = "";
                        break;
                    case "5"://�ʾ����͵Ĳ���
                        DataTable tempStep5 = stepDetail.Tables[stepItem["StepID"].ToString()].Clone();
                        while (tempStep5.Columns.Count > 9)
                        {
                            tempStep5.Columns.RemoveAt(9);
                        }
                        tempStep5.Columns.Add("����", typeof(string));
                        tempStep5.Columns.Add("ֵ", typeof(string));
                        tempStep5.Columns.Add("ControlType");
                        tempStep5.Columns["ControlType"].Caption = "";
                        tempStep5.AcceptChanges();
                        foreach (DataRow detailDataRow in stepDetail.Tables[stepItem["StepID"].ToString()].Rows)
                        {
                            //��ת������
                            for (int i = 9; i < stepDetail.Tables[stepItem["StepID"].ToString()].Columns.Count; i++)
                            {
                                DataRow datarowItem = tempStep5.NewRow();
                                //�̶���ǰ9��
                                datarowItem[0] = detailDataRow[0];
                                datarowItem[1] = detailDataRow[1];
                                datarowItem[2] = detailDataRow[2];
                                datarowItem[3] = detailDataRow[3];
                                datarowItem[4] = detailDataRow[4];
                                datarowItem[5] = detailDataRow[5];
                                datarowItem[6] = detailDataRow[6];
                                datarowItem[7] = detailDataRow[7];
                                datarowItem[8] = detailDataRow[8];
                                //��̬����
                                DataRow[] parameterInfo = stepDetail.Tables["ParameterDefinition"].Select("ParameterID='" + stepDetail.Tables[stepItem["StepID"].ToString()].Columns[i].ColumnName + "'");
                                if (parameterInfo != null && parameterInfo.Length > 0)
                                {
                                    //��̬��1.��Ŀ
                                    datarowItem[9] = parameterInfo[0]["ParameterName"].ToString();
                                    //��̬��2.��
                                    datarowItem[10] = detailDataRow[i].ToString();
                                    //��̬��3.�ؼ�����
                                    datarowItem[11] = parameterInfo[0]["ControlType"].ToString();
                                }
                                tempStep5.Rows.Add(datarowItem);
                            }
                        }
                        stepDetail.Tables.Remove(stepItem["StepID"].ToString());
                        stepDetail.Tables.Add(tempStep5);

                        break;

                    case "7"://�Զ������͵Ĳ���
                        DataTable tempStep7 = stepDetail.Tables[stepItem["StepID"].ToString()].Clone();
                        while (tempStep7.Columns.Count > 9)
                        {
                            tempStep7.Columns.RemoveAt(9);
                        }
                        tempStep7.Columns.Add("����", typeof(string));
                        tempStep7.Columns.Add("ֵ", typeof(string));
                        tempStep7.Columns.Add("ControlType");
                        tempStep7.Columns["ControlType"].Caption = "";
                        tempStep7.AcceptChanges();
                        foreach (DataRow detailDataRow in stepDetail.Tables[stepItem["StepID"].ToString()].Rows)
                        {
                            //��ת������
                            for (int i = 9; i < stepDetail.Tables[stepItem["StepID"].ToString()].Columns.Count; i++)
                            {
                                DataRow datarowItem = tempStep7.NewRow();
                                //�̶���ǰ9��
                                datarowItem[0] = detailDataRow[0];
                                datarowItem[1] = detailDataRow[1];
                                datarowItem[2] = detailDataRow[2];
                                datarowItem[3] = detailDataRow[3];
                                datarowItem[4] = detailDataRow[4];
                                datarowItem[5] = detailDataRow[5];
                                datarowItem[6] = detailDataRow[6];
                                datarowItem[7] = detailDataRow[7];
                                datarowItem[8] = detailDataRow[8];
                                //��̬����
                                DataRow[] parameterInfo = stepDetail.Tables["ParameterDefinition"].Select("ParameterID='" + stepDetail.Tables[stepItem["StepID"].ToString()].Columns[i].ColumnName + "'");
                                if (parameterInfo != null && parameterInfo.Length > 0)
                                {
                                    //��̬��1.��Ŀ
                                    datarowItem[9] = parameterInfo[0]["ParameterName"].ToString();
                                    //��̬��2.��
                                    datarowItem[10] = detailDataRow[i].ToString();
                                    //��̬��3.�ؼ�����
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
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����İݷò��輰����ֵ��ϸ.��ѯ��ɣ�[" + dtFinish.ToString() + "].����ʱ��:[" + (dtFinish - dtStart).TotalSeconds.ToString() + "]s" });
            //stepDetail.Tables[4].Rows.Add(stepDetail.Tables[4].NewRow());
            return stepDetail;
        }
        #endregion
        

        
        #region GetVisitingTaskPictures
        /// <summary>
        /// ĳ��ĳ����ĳ����������Ƭ
        /// </summary>
        /// <param name="pQueryParams">��ѯ����(���ű�ʶ��ClientStructureID��ְλ��ʶ��ClientPositionID��ִ����Ա��ʶ:ClientUserID,�ݷ�����ִ�б�ʶ��VisitingTaskDataID���ݷò����ʶ��VisitingTaskStepID����ʼ���ڣ�DateFrom����ֹ���ڣ�DateTo)</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="pPageSize">ÿҳ��¼��</param>
        /// <param name="pCurrentPageIndex">��1��ʼ�ĵ�ǰҳ��</param>
        /// <returns>��ϸ��Ϣ</returns>
        public PagedQueryResult<VisitingTaskPicturesViewEntity> GetVisitingTaskPictures(Dictionary<string, object> pQueryParams, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            DateTime dtStart = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����������Ƭ.��ѯ��ʼ��[" + dtStart.ToString() + "]" });
            var result = this._currentDAO.GetVisitingTaskPictures(pQueryParams, pOrderBys, pPageSize, pCurrentPageIndex);
            DateTime dtFinish = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "ĳ��ĳ����ĳ����������Ƭ.��ѯ��ɣ�[" + dtFinish.ToString() + "].����ʱ��:[" + (dtFinish - dtStart).TotalSeconds.ToString() + "]s" });
            return result;
        }
        #endregion


        #region GetVisitingTaskDataInfo
        /// <summary>
        /// ����������ѯ��ĳ��ĳ����ĳ����ߵ���Ϣ
        /// </summary>
        /// <param name="pClientUserID">ִ����Ա��ʶ</param>
        /// <param name="pPOPID">�ŵ��ʶ</param>
        /// <param name="pExecutionTime">ִ������</param>
        /// <returns>һ���˵�����ߵ�</returns>
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
        /// �����ˡ��ꡢ�� ��ȡ��������Ʒ����
        /// </summary>
        /// <param name="pUserID">�û���ʶ</param>
        /// <param name="pPopID">�ŵ��ʶ</param>
        /// <param name="pDate">��������</param>
        /// <returns>�����е�������Ʒ��Ϣ</returns>
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
        /// ĳ��ĳ����ĳ����������Ƭ
        /// </summary>
        /// <param name="pQueryParams">��ѯ����(���ű�ʶ��ClientStructureID��ְλ��ʶ��ClientPositionID��ִ����Ա��ʶ:ClientUserID,�ݷ�����ִ�б�ʶ��VisitingTaskDataID���ݷò����ʶ��VisitingTaskStepID����ʼ���ڣ�DateFrom����ֹ���ڣ�DateTo)</param>
        /// <returns>��ϸ��Ϣ</returns>
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

                    #region ��ѯ��Ƭ��Ϣ��ӵ�List��
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
            #region ���򣬰����� �ն����� ʱ������
            VisitingTaskPhotoShowViewEntity[] PhotoDatas = photoList.ToArray();
            PhotoDatas = photoList.ToArray().OrderByDescending(c => c.PhotoDate).ThenBy(c => c.POPName).ThenByDescending(c => c.PhotoTime).ToArray();
            //PhotoDatas = PhotoDatas.Skip(5).Take(5).ToArray(); ȡ6-10������
            SumCount = PhotoDatas.Length;   //����������
            PhotoDatas = PhotoDatas.Skip(0).Take(1000).ToArray();         
            #endregion
            return PhotoDatas;
        }

        /// <summary>
        /// ����Ƭ��Ϣ��ӵ�List��
        /// </summary>
        /// <param name="pEntity">��ʾ��Ƭ�Ķ���</param>
        /// <param name="photoList">��ʾ��Ƭ��List</param>
        /// <param name="pPhotoData">��Ƭ�Ķ���</param>
        public void PhotoListInsert(List<VisitingTaskPhotoShowViewEntity> photoList, VisitingTaskPhotoDataViewEntity pPhotoData, int pType, int i, DataSet ds)
        {
            if (!string.IsNullOrEmpty(pPhotoData.FileName))
            {
                #region ��Ƭ��ʾ����
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
                    entity.PhotoName = "������";                 
                    entity.StepName = "����";
                }
                else if (pType == 2)
                {
                    entity.PhotoName = "������";                  
                    entity.StepName = "����";
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
        /// ��ѯ�ͻ�������������Ƭ����
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
                    #region ������Ƭ����
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

                    #region �ж��Ƿ��ǲ�ͬ������Ͳ���
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
                        #region ��� ��������������
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