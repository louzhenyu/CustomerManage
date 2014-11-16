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
    [ExportMetadata("Action", "GetDept")]
    public class GetDeptHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDept(pRequest);
        }

        public string GetDept(string pRequest)
        {
            var rd = new APIResponse<GetDeptRD>();
            var rdData = new GetDeptRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetDeptRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                int totalPage = 0;
                DataTable dTable = unitBll.GetUnitList(rp.Parameters.UnitID, typeID, rp.Parameters.PageIndex, rp.Parameters.PageSize,rp.Parameters.Keyword, out totalPage);

                //排序
                DataView dv = dTable.DefaultView;
                string sort = string.IsNullOrEmpty(rp.Parameters.sort) ? "UnitName asc" : rp.Parameters.sort;
                dv.Sort = sort;
                DataTable dt2 = dv.ToTable();
                dTable = dt2;
                
                if (dTable != null)
                    rdData.UnitList = DataTableToObject.ConvertToList<UnitInfo>(dTable);
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

    #region 获取部门
    public class GetDeptRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public string Keyword { set; get; }
        public string sort { set; get; }

        public void Validate()
        {
            //if (string.IsNullOrEmpty(UnitID))
            //    throw new APIException("【UnitID】不能为空") { ErrorCode = 102 };
            if (PageSize == 0) PageSize = 15;
        }
    }
    public class GetDeptRD : IAPIResponseData
    {
        public List<UnitInfo> UnitList { set; get; }
        public int TotalPage { set; get; }
    }
    #endregion
}