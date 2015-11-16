using Aspose.Cells;
using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CardProduct.MakeVipCard
{
    /// <summary>
    /// 导入卡内码接口
    /// </summary>
    public class ImportVipCardISNAH : BaseActionHandler<ImportVipCardISNRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<ImportVipCardISNRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBatchBLL = new VipCardBatchBLL(loggingSessionInfo);
            try
            {
                if (string.IsNullOrWhiteSpace(para.ExcelPath) && string.IsNullOrWhiteSpace(para.ExcelPath))
                    throw new APIException("参数为NULL！") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };

                Dictionary<string, string> m_VipCardInfoCollection = new Dictionary<string, string>();

                #region 根据Excel地址读取数据并填充给数据字典
                ExcelHelper m_ExcelHelper = new ExcelHelper("D:\\TestImportVipCardISN.xlsx");

                DataTable ds = m_ExcelHelper.ExcelToDataTable("Sheet1", true);
                if (ds != null)
                {
                    foreach (var item in ds.AsEnumerable())
                    {
                        string m_CardCode = item["VipCardCode"].ToString();
                        if (m_CardCode.Length == 10)
                        {
                            m_CardCode = "0" + m_CardCode;
                        }
                        m_VipCardInfoCollection.Add(m_CardCode, item["VipCardISN"].ToString());
                        //m_VipCardInfoCollection.Add(item.Field<string>("VipCardCode"), item.Field<string>("VipCardISN"));
                    }
                }
                #endregion

                //VipCardBatchBLL.ImportVipCardISN(m_VipCardInfoCollection, para.BatchNo);
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }



            return rd;
        }
    }
}