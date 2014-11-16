using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using System.Data.OleDb;
using JIT.Utility.DataAccess.Query;
using System.Linq;
using System.Text;
using JIT.Utility.Log;
using JIT.Utility;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    ///  BatchImportUserList的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "BatchImportUserList")]
    public class BatchImportUserListHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return BatchImportUserList(pRequest);
        }

        public string BatchImportUserList(string pRequest)
        {
            var rd = new APIResponse<BatchImportUserListRD>();
            var rdData = new BatchImportUserListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<BatchImportUserListRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                string[] msg = new ImportStaffManager().Export(loggingSessionInfo, rp.Parameters.Path).Split('|');
                if (msg[0] == "1")
                {
                    rd.ResultCode = 0;
                    rdData.IsSuccess = true;
                }
                else
                {
                    rd.ResultCode = 101;
                    rdData.IsSuccess = false;
                }
                rd.Message = msg[1];
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rdData.IsSuccess = false;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 批量导入用户名单
    public class BatchImportUserListRP : IAPIRequestParameter
    {
        //public string BatchImportUrl { set; get; }
        public string Path { set; get; }

        public void Validate()
        {
            //if (string.IsNullOrEmpty(BatchImportUrl))
            //    throw new APIException("【BatchImportUrl】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Path))
                throw new APIException("【Path】不能为空") { ErrorCode = 102 };
        }
    }
    public class BatchImportUserListRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}