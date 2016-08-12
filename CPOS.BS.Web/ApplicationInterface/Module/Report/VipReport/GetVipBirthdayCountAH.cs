using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Report.VipReport.Request;
using JIT.CPOS.DTO.Module.Report.VipReport.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.VipReport
{
    public class GetVipBirthdayCountAH : BaseActionHandler<VipBirthdayRP, VipBirthdayRD>
    {
        protected override VipBirthdayRD ProcessRequest(DTO.Base.APIRequest<VipBirthdayRP> pRequest)
        {
            var rd = new VipBirthdayRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipBLL = new VipBLL(loggingSessionInfo);
            try
            {
                if (para.Consumption < 0)
                    throw new APIException("最近消费参数异常！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }

            
            string StarDate = null;
            string EndDate = null;
            if (para.Consumption == 0)
            {
                DateTime Now = DateTime.Now;
                StarDate = Now.AddMonths(-para.Consumption).Date.ToString();
                EndDate = Now.Date.ToString();
            }
            //int? CardStatus = null;
            //if (para.VipCardStatusID != null)
            //{
            //    CardStatus = para.VipCardStatusID.Value;
            //}
            DataSet ds = VipBLL.GetVipBirthdayCount(para.Month, para.UnitID, para.Gender, para.VipCardStatusID, StarDate, EndDate, para.PageSize, para.PageIndex);
            //ExcelHelper m_Test = new ExcelHelper("D:\\ExportTest.xlsx");
            //m_Test.DataTableToExcel(ds.Tables[0], "Sheet1", true);
            #region 赋值
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                rd = new VipBirthdayRD();
                rd.VipBirthdayInfoList = new List<VipBirthdayInfo>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    VipBirthdayInfo m = new VipBirthdayInfo();
                    if (ds.Tables[0].Rows[i]["VipCardCode"] != DBNull.Value)
                    {
                        m.VipCardCode = ds.Tables[0].Rows[i]["VipCardCode"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["VipCardTypeName"] != DBNull.Value)
                    {
                        m.VipCardTypeName = ds.Tables[0].Rows[i]["VipCardTypeName"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["VipCardStatusId"] != DBNull.Value)
                    {
                        m.VipCardStatusId = int.Parse(ds.Tables[0].Rows[i]["VipCardStatusId"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["VipName"] != DBNull.Value)
                    {
                        m.VipName = ds.Tables[0].Rows[i]["VipName"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Phone"] != DBNull.Value)
                    {
                        m.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Gender"] != DBNull.Value)
                    {
                        m.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Birthday"] != DBNull.Value)
                    {
                        m.Birthday = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthday"]).ToString("MM-dd");
                    }
                    if (ds.Tables[0].Rows[i]["MembershipUnit"] != DBNull.Value)
                    {
                        m.MembershipUnit = ds.Tables[0].Rows[i]["MembershipUnit"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["unit_name"] != DBNull.Value)
                    {
                        m.MembershipUnitName = ds.Tables[0].Rows[i]["unit_name"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["MembershipTime"] != DBNull.Value)
                    {
                        m.MembershipTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["MembershipTime"]).ToString("yyyy-MM-dd");
                    }
                    //最近消费
                    m.SpendingDateShow ="近月"+para.Consumption.ToString()+"消费";

                    rd.VipBirthdayInfoList.Add(m);
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                    {
                        rd.TotalCount = int.Parse(ds.Tables[1].Rows[j]["totalrow"].ToString());
                        rd.TotalPageCount = int.Parse(ds.Tables[1].Rows[j]["totalpage"].ToString()); ;
                    }
                }

            }
            #endregion

            if (rd == null)
            {
                rd = new VipBirthdayRD();

            }
            return rd;
        }
    }
}