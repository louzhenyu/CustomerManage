using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.Utility.ExtensionMethod;
using System.Data;
using System.Text;

namespace JIT.CPOS.BS.Web.Module.VisitingData.Data.Handler
{
    /// <summary>
    /// TaskDataHandler 的摘要说明
    /// </summary>
    public class TaskDataHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.BTNCode)
            {
                case "search":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetTaskUserData":
                                res = GetTaskUserData(pContext.Request.Form);
                                break;
                            case "GetTaskStepParameterList":
                                res = GetTaskStepParameterList(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetTaskUserData
        private string GetTaskUserData(NameValueCollection rParams)
        {
            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(rParams["ClientStructureID"]))
            {
                queryParams.Add("ClientStructureID", rParams["ClientStructureID"]);
            }
            if (!string.IsNullOrEmpty(rParams["ClientPositionID"]))
            {
                queryParams.Add("ClientPositionID", rParams["ClientPositionID"]);
            }
            if (!string.IsNullOrEmpty(rParams["ClientUserName"]))
            {
                queryParams.Add("ClientUserName", rParams["ClientUserName"]);
            }
            if (!string.IsNullOrEmpty(rParams["ExecutionTime"]))
            {
                queryParams.Add("ExecutionTime", rParams["ExecutionTime"]);
            }
            //实际执行
            int oWorkingHoursIndoor, oWorkingHoursJourneyTime, oWorkingHoursTotal;
            List<VisitingTaskDataViewEntity> result = new VisitingTaskDataBLL(CurrentUserInfo).GetVisitingTaskDataList(queryParams, null, out oWorkingHoursIndoor, out oWorkingHoursJourneyTime, out oWorkingHoursTotal);
            
            return string.Format("{{\"data\":{0},\"summary\":[{{\"oWorkingHoursIndoor\":{1},\"oWorkingHoursJourneyTime\":{2},\"oWorkingHoursTotal\":{3}}}] }}",
               result.ToArray().ToJSON(),
                oWorkingHoursIndoor,
                oWorkingHoursJourneyTime,
                oWorkingHoursTotal);
        }
        #endregion

        #region GetTaskStepParameterList
        private string GetTaskStepParameterList(NameValueCollection rParams)
        {

            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(rParams["ClientUserID"]))
            {
                queryParams.Add("ClientUserID", rParams["ClientUserID"]);
            }
            if (!string.IsNullOrEmpty(rParams["POPID"]))
            {
                queryParams.Add("POPID", rParams["POPID"]);
            }
            if (!string.IsNullOrEmpty(rParams["ExecutionTime"]))
            {
                queryParams.Add("ExecutionTime", rParams["ExecutionTime"]);
            }
            DataSet ds = new VisitingTaskDataBLL(CurrentUserInfo).GetVisitingTaskDetailData(queryParams);
            
            StringBuilder json = new StringBuilder();
            json.Append("[{");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //json.Append("stepitems:" + ds.Tables[0].ToJSON().Replace("]", ",{StepName:'拜访照片'}]") + ",");
                json.Append("stepitems:" + ds.Tables[0].ToJSON() + ",");

                #region 基础的步骤(产品、品牌、品类、人员、问卷、自定义对象)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region field
                    json.Append("field" + i + ":[");
                    foreach (DataColumn dc in ds.Tables[dr["StepID"].ToString()].Columns)
                    {
                        json.Append("{name:'" + dc.ColumnName + "',type:'string'},");
                    }
                    json.Remove(json.ToString().Length - 1, 1);
                    json.Append("],");
                    #endregion

                    #region data
                    //store data
                    json.Append("data" + i + ":");
                    json.Append(ds.Tables[dr["StepID"].ToString()].ToJSON());
                    json.Append(",");
                    #endregion

                    #region column
                    //grid column
                    json.Append("column" + i + ":[");

                    switch (int.Parse(dr["StepType"].ToString()))
                    {
                        case 5:
                        //问卷
                        case 7:
                            //自定义对象
                            for (int j = 0; j < ds.Tables[dr["StepID"].ToString()].Columns.Count; j++)
                            {
                                DataColumn dc = ds.Tables[dr["StepID"].ToString()].Columns[j];

                                //前七列是公用信息，第6、7列是objectname1,objectname2
                                if (j > 5 && j <= 7 && !string.IsNullOrEmpty(dc.Caption))
                                {
                                    if (dc.Caption.ToString() == "拜访对象")
                                    {
                                        json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "',width:bfdxwidth},");
                                    }
                                    else
                                    {
                                        json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "'},");
                                    }
                                }
                                else if (j > 7 && !string.IsNullOrEmpty(dc.Caption))
                                {
                                    if (dc.Caption.ToString() == "值")
                                    {
                                        json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "',renderer:fnColumnPhotoByType,width:zhiwidth},");
                                    }
                                    else if (dc.Caption.ToString() == "参数")
                                    {
                                        json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "',width:canshuwidth},");
                                    }
                                    else
                                    {
                                        json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "'},");
                                    }
                                }
                            }
                            json.Remove(json.ToString().Length - 1, 1);
                            break;
                        default:
                            //其它类型步骤
                            for (int j = 0; j < ds.Tables[dr["StepID"].ToString()].Columns.Count; j++)
                            {
                                DataColumn dc = ds.Tables[dr["StepID"].ToString()].Columns[j];

                                //前七列是公用信息，第6、7列是objectname1,objectname2
                                if (j > 5 && j <= 7 && !string.IsNullOrEmpty(dc.Caption))
                                {
                                    json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "'},");
                                }
                                else if (j > 7 && !string.IsNullOrEmpty(dc.Caption))
                                {
                                    //获取控件类型
                                    int controltype = 0;
                                    if (ds.Tables["ParameterDefinition"].Select("ParameterID='" + dc.ColumnName + "'").Length == 1)
                                    {
                                        controltype = ds.Tables["ParameterDefinition"].Select("ParameterID='" + dc.ColumnName + "'")[0]["ControlType"].ToString().ToInt();
                                    }
                                    //type 12(照片列)
                                    if (controltype == 12)
                                    {
                                        json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "',renderer:fnColumnPhoto},");
                                    }
                                    else
                                    {
                                        json.Append("{text:'" + dc.Caption + "',dataIndex:'" + dc.ColumnName + "'},");
                                    }
                                }
                            }
                            json.Remove(json.ToString().Length - 1, 1);
                            break;
                    }
                    json.Append("],");
                    #endregion
                }
                #endregion

                json.Append("stepitemslength:" + (ds.Tables[0].Rows.Count));
            }
            
            #region 照片步骤
            ////照片
            //Dictionary<string, object> queryParams1 = new Dictionary<string, object>();
            //queryParams1.Add("VisitingTaskDataID", rParams["VisitingTaskDataID"]);
            //queryParams1.Add("ClientUserID", rParams["ClientUserID"]);
            //queryParams1.Add("DateFrom", rParams["ExecutionTime"]);
            //queryParams1.Add("DateTo", rParams["ExecutionTime"]);
            //VisitingTaskPicturesViewEntity[] picEntity = null;
            //if (!string.IsNullOrEmpty(rParams["VisitingTaskDataID"]))
            //{
            //    picEntity = new VisitingTaskDataBLL(CurrentUserInfo).GetVisitingTaskPictures(queryParams1, null, 10000, 1).Entities;
            //}
            //#region field
            //json.Append("field" + (ds.Tables[0].Rows.Count ) + ":[");
            //string[] fields = new VisitingTaskPicturesViewEntity().ToJSON().Split(',');
            //foreach (string field in fields)
            //{
            //    json.Append("{name:" + field.Split(':')[0].Replace("{", "").Replace("\"", "'") + ",type:'string'},");
            //}
            //json.Remove(json.ToString().Length - 1, 1);
            //json.Append("],");
            //#endregion

            //#region data
            //json.Append("data" + (ds.Tables[0].Rows.Count) + ":" + (picEntity == null ? "[]" : picEntity.ToJSON()) + ",");
            //#endregion

            //#region column
            //json.Append("column" + (ds.Tables[0].Rows.Count) + ":[");
            //json.Append("{text:'POPID',dataIndex:'POPID'},");
            //json.Append("{text:'PhotoName',dataIndex:'PhotoName'},");
            //json.Append("{text:'部门名称',dataIndex:'StructureName'},");
            //json.Append("{text:'Value',dataIndex:'Value'}");
            //json.Append("]");
            //#endregion
            #endregion

            json.Append("}]");
            return json.ToString();
        }
        #endregion
    }
}