using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.DataAccess;
using System.Data;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    /// QiXinManageLogin的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "GetJobFunc")]
    public class GetJobFuncHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDept(pRequest);
        }

        public string GetDept(string pRequest)
        {
            var rd = new APIResponse<GetJobFuncRD>();
            var rdData = new GetJobFuncRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetJobFuncRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                JobFunctionBLL jobBll = new JobFunctionBLL(loggingSessionInfo);
                //IWhereCondition[] iwhere = { new EqualsCondition { DateTimeAccuracy = null, FieldName = "CustomerID", Value = rp.CustomerID } };
                //if (!string.IsNullOrEmpty(rp.Parameters.JobFunctionID))
                //{
                //    iwhere = new IWhereCondition[]
                //    { 
                //        new EqualsCondition { DateTimeAccuracy = null, FieldName = "CustomerID", Value = rp.CustomerID },
                //        new EqualsCondition { DateTimeAccuracy = null, FieldName = "JobFunctionID", Value = rp.Parameters.JobFunctionID }
                //    };
                //}
                //PagedQueryResult<JobFunctionEntity> jobFunction = jobBll.PagedQuery(iwhere, new OrderBy[] { new OrderBy { FieldName = "Name", Direction = OrderByDirections.Asc } }, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                int totalPage = 0;
                DataTable dTable = jobBll.GetJobFuncList(rp.Parameters.JobFunctionID, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.Keyword, out totalPage);

                //排序
                DataView dv = dTable.DefaultView;
                string sort = string.IsNullOrEmpty(rp.Parameters.sort) ? "Name asc" : rp.Parameters.sort;
                dv.Sort = sort;
                DataTable dt2 = dv.ToTable();
                dTable = dt2;

                if (dTable != null)
                    rdData.JobFuncList = DataTableToObject.ConvertToList<JobFunctionEntity>(dTable);
                rdData.TotalPage = totalPage;
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 获取职衔
    public class GetJobFuncRP : IAPIRequestParameter
    {
        public string JobFunctionID { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public string Keyword { set; get; }
        public string sort { set; get; }
        public void Validate()
        {
            //if (string.IsNullOrEmpty(JobFunctionID))
            //    throw new APIException("【JobFunctionID】不能为空") { ErrorCode = 102 };
            if (PageSize == 0) PageSize = 15;
        }
    }
    public class GetJobFuncRD : IAPIResponseData
    {
        public List<JobFunctionEntity> JobFuncList { set; get; }
        public int TotalPage { set; get; }
    }
    #endregion
}