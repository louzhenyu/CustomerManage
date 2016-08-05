using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Request;
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Response;
using JIT.Utility.DataAccess.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.WithdrawDeposit
{
    /// <summary>
    /// Export 的摘要说明
    /// </summary>
    public class Export : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            var action = context.Request["action"];
            switch (action)
            {
                case "VIP.WithdrawDeposit.GetWithdrawDepositApplyList":
                    DownLoadTXExcel(context);
                    break;
                default:
                    break;
            }
        }
        private void DownLoadTXExcel(HttpContext pContext)
        {
            var req = pContext.Request["req"];
            if (string.IsNullOrWhiteSpace(req))
                return;
            var pRequest = JsonConvert.DeserializeObject<DTO.Base.APIRequest<GetWithdrawDepositApplyListRP>>(req);

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                //throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };
                return;

            #region 获取数据
            //排序参数
            List<OrderBy> orderList = new List<OrderBy> { };
            orderList.Add(new OrderBy() { FieldName = "ApplyDate", Direction = OrderByDirections.Desc });
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.WithdrawNo))
                complexCondition.Add(new LikeCondition() { FieldName = "WithdrawNo", Value = "%" + pRequest.Parameters.WithdrawNo + "%" });
            if (pRequest.Parameters.VipType != null && pRequest.Parameters.VipType != -1)
                complexCondition.Add(new EqualsCondition() { FieldName = "VipType", Value = pRequest.Parameters.VipType });

            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.Name))
                complexCondition.Add(new JIT.Utility.DataAccess.Query.LikeCondition() { FieldName = "Name", Value = "%" + pRequest.Parameters.Name + "%" });
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.Phone))
                complexCondition.Add(new JIT.Utility.DataAccess.Query.LikeCondition() { FieldName = "Phone", Value = "%" + pRequest.Parameters.Phone + "%" });

            //if (!string.IsNullOrWhiteSpace(pRequest.Parameters.ApplyStartDate))
            //    complexCondition.Add(new JIT.Utility.DataAccess.Query.MoreThanCondition() { FieldName = "ApplyDate", Value = Convert.ToDateTime(pRequest.Parameters.ApplyStartDate) });
            //if (!string.IsNullOrWhiteSpace(pRequest.Parameters.ApplyEndDate))
            //    complexCondition.Add(new JIT.Utility.DataAccess.Query.LessThanCondition() { FieldName = "ApplyDate", Value = Convert.ToDateTime(pRequest.Parameters.ApplyEndDate) });


            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.ApplyStartDate))
                complexCondition.Add(new DirectCondition() { Expression = "ApplyDate>='" + pRequest.Parameters.ApplyStartDate + "'" });
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.ApplyEndDate))
            {
                if (pRequest.Parameters.ApplyStartDate == pRequest.Parameters.ApplyEndDate)
                {
                    pRequest.Parameters.ApplyEndDate = pRequest.Parameters.ApplyEndDate + " 23:59";
                }
                complexCondition.Add(new DirectCondition { Expression = "ApplyDate<='" + pRequest.Parameters.ApplyEndDate + "'" });
            }

            if (pRequest.Parameters.Status != null && pRequest.Parameters.Status != -1)
                complexCondition.Add(new JIT.Utility.DataAccess.Query.EqualsCondition() { FieldName = "Status", Value = pRequest.Parameters.Status });
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.CompleteStartDate))
                complexCondition.Add(new DirectCondition() { Expression = "CompleteDate >='" + pRequest.Parameters.CompleteStartDate + "'" });

            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.CompleteEndDate))
            {
                if (pRequest.Parameters.CompleteEndDate == pRequest.Parameters.CompleteEndDate)
                {
                    pRequest.Parameters.CompleteEndDate = pRequest.Parameters.CompleteEndDate + " 23:59";
                }
                complexCondition.Add(new DirectCondition { Expression = "CompleteDate<='" + pRequest.Parameters.CompleteEndDate + "'" });
            }

            VipWithdrawDepositApplyBLL bll = new VipWithdrawDepositApplyBLL(loggingSessionInfo);
            int rowCount = 0;
            int pageCount = 0;
            var dbSet = bll.PagedQueryDbSet(complexCondition.ToArray(), orderList.ToArray(), 10000, pRequest.Parameters.PageIndex, out rowCount, out pageCount);
            var dbList = DataTableToObject.ConvertToList<WithdrawDepositApplyInfo>(dbSet.Tables[0]);
            foreach (var m in dbList)
            {
                m.ApplyDate = m.ApplyDate + "" == "" ? "" : Convert.ToDateTime(m.ApplyDate).ToString("yyyy-MM-dd");
                m.CompleteDate = m.CompleteDate + "" == "" ? "" : Convert.ToDateTime(m.CompleteDate).ToString("yyyy-MM-dd");
            }

            #endregion
            //var dt = ToDataTable<WithdrawDepositApplyInfo>(dbList);
            DataTable dt = new DataTable("dt");
            dt.Columns.Add("提现单号");
            dt.Columns.Add("姓名");
            dt.Columns.Add("类别");
            dt.Columns.Add("手机号");
            dt.Columns.Add("提现金额(元)");
            dt.Columns.Add("银行名称");
            dt.Columns.Add("银行卡号");
            dt.Columns.Add("开户人姓名");
            dt.Columns.Add("状态");
            dt.Columns.Add("申请时间");
            dt.Columns.Add("完成时间");
            foreach (var m in dbList)
            {
                DataRow row = dt.NewRow();
                row["提现单号"] = m.WithdrawNo;
                row["姓名"] = m.Name;
                switch (m.VipType)
                {
                    case 1:
                        row["类别"] = "会员";
                        break;
                    case 2:
                        row["类别"] = "员工";
                        break;
                    case 3:
                        row["类别"] = "旧分销商";
                        break;
                    case 4:
                        row["类别"] = "分销商";
                        break;
                    default:
                        row["类别"] = "";
                        break;
                }
                row["手机号"] = m.Phone;
                row["提现金额(元)"] = m.Amount;
                row["银行名称"] = m.BankName;
                row["银行卡号"] = m.CardNo;
                row["开户人姓名"] = m.AccountName;
                switch (m.Status)
                {
                    case 1:
                        row["状态"] = "待审核";
                        break;
                    case 2:
                        row["状态"] = "已审核";
                        break;
                    case 3:
                        row["状态"] = "已完成";
                        break;
                    case 4:
                        row["状态"] = "审核不通过";
                        break;
                    default:
                        break;
                }

                row["申请时间"] = m.ApplyDate;
                row["完成时间"] = m.CompleteDate;

                dt.Rows.Add(row);
            }

            ExcelHelper excel = new ExcelHelper();
            //上传目录  
            string directory = "~/Framework/Upload/";
            //判断目录是否存在  
            if (!System.IO.Directory.Exists(pContext.Server.MapPath(directory)))
            {
                System.IO.Directory.CreateDirectory(pContext.Server.MapPath(directory));
            }
            string fileName = DateTime.Now.Ticks + ".xls";
            string MapUrl = pContext.Server.MapPath(directory + fileName);
            //string MapUrl = pContext.Server.MapPath(DateTime.Now.Ticks + ".xls");
            excel.RenderToExcel(dt, MapUrl);
            Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}