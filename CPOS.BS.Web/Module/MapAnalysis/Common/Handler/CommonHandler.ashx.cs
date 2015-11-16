using JIT.CPOS.BS.Web.MapKpiService;
using JIT.CPOS.BS.Web.UnitServiceSoap;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Web.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Module.MapAnalysis.Common.Handler
{
    /// <summary>
    /// CommonHandler 的摘要说明
    /// </summary>
    public class CommonHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        #region 处理Ajax请求
        /// <summary>
        /// 处理Ajax请求
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string op = pContext.Request.QueryString["op"];
            if (!string.IsNullOrWhiteSpace(op))
            {
                switch (op)
                {
                    case "1":
                        #region 获取客户所有的KPI
                        {
                            MapKpiServiceClient service = new MapKpiServiceClient();
                            var kpiCategories = service.GetKpiByClientId(this.GetCurrentClientID());
                            this.ResponseJSON(kpiCategories);
                        }
                        #endregion
                        break;
                    case "2":
                        #region 获取KPI数据
                        {
                            var kpiFilters = this.DeserializeJSONContent<KPIFilter>();
                            MapKpiServiceClient service = new MapKpiServiceClient();
                            //
                            string start = null;
                            string end = null;
                            if (kpiFilters.IsCheckCustomizeDatePeriod.HasValue && kpiFilters.IsCheckCustomizeDatePeriod.Value)
                            {
                                start = kpiFilters.StartDate.HasValue ? kpiFilters.StartDate.Value.ToString("yyyy-MM-dd") : string.Empty;
                                end = kpiFilters.EndDate.HasValue ? kpiFilters.EndDate.Value.ToString("yyyy-MM-dd") : string.Empty;
                            }
                            else
                            {
                                switch (kpiFilters.DatePeriod)
                                {
                                    case "1":   //年
                                        start = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                                        end = DateTime.Now.ToString("yyyy-MM-dd");
                                        break;
                                    case "2":   //季度
                                        start = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                                        end = DateTime.Now.ToString("yyyy-MM-dd");
                                        break;
                                    case "3":   //月
                                        start = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                                        end = DateTime.Now.ToString("yyyy-MM-dd");
                                        break;
                                    case "4":   //周
                                        start = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                                        end = DateTime.Now.ToString("yyyy-MM-dd");
                                        break;
                                }
                            }
                            //
                            string[] channelIDs = kpiFilters.ChannelID;
                            //if (!string.IsNullOrWhiteSpace(kpiFilters.ChannelID))
                            //{
                            //    channelIDs = new string[] { kpiFilters.ChannelID };
                            //}
                            string[] chainIDs = null;
                            if (!string.IsNullOrWhiteSpace(kpiFilters.ChainID))
                            {
                                chainIDs = new string[] { kpiFilters.ChainID };
                            }
                            //
                            var kpiQuery = new KPIQuery();
                            kpiQuery.Level = kpiFilters.Level;
                            kpiQuery.BoundID = kpiFilters.BoundID;
                            kpiQuery.KPIID = kpiFilters.KPIID;
                            kpiQuery.StyleCode = kpiFilters.StyleCode;
                            kpiQuery.StyleSubCode = kpiFilters.StyleSubCode;
                            kpiQuery.DateFrom = start;
                            kpiQuery.DateTo = end;
                            kpiQuery.SKUIDs = kpiFilters.SKUIDs;
                            kpiQuery.SKUBrandIDs = kpiFilters.BrandIDs;
                            kpiQuery.SKUCategoryIDs = kpiFilters.CategoryIDs;
                            kpiQuery.StoreIDs = null;
                            kpiQuery.ChainIDs = chainIDs;
                            kpiQuery.ChannelIDs = channelIDs;
                            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = kpiQuery.ToJSON() });
                            //
                            var datas = service.GetKpiData(kpiFilters.Level, kpiFilters.BoundID, kpiFilters.KPIID, kpiQuery.StyleCode, kpiQuery.StyleSubCode, kpiFilters.SKUIDs, kpiFilters.BrandIDs, kpiFilters.CategoryIDs, null, channelIDs, chainIDs, start, end, this.GetCurrentClientID(), null);
                            if (string.IsNullOrEmpty(datas))
                            {
                                datas = "null";
                            }
                            pContext.Response.ContentEncoding = Encoding.UTF8;
                            this.ResponseContent(datas);
                        }
                        #endregion
                        break;
                    case "3":
                        #region 获取门店收银变化
                        {
                            UnitServiceSoapClient service = new UnitServiceSoapClient();
                            var result = service.GetUnitIdList("B4CEDBEE501443E4BA297B3332304F1B", "0");
                            result = ConvertCPOSStoreIDToQDYStoreID(result);
                            this.ResponseContent(result);
                        }
                        #endregion
                        break;
                    case "4":
                        #region 导出数据
                        {
                            var tempData = GetKpiData();
                            string resultData;
                            if (tempData == null)
                            {
                                resultData = "null";
                            }
                            else
                            {

                                //string path = HttpContext.Current.Server.MapPath(string.Format("UpLoad/{0}", pFilePath));

                                string tempFolder = "/Module/MapAnalysis/Common/Download/" + Guid.NewGuid().ToString();
                                string tempFilePath = tempFolder + "/KPI数据.xlsx";
                                string fullPath = HttpContext.Current.Server.MapPath(tempFolder);
                                if (!System.IO.Directory.Exists(fullPath))
                                    System.IO.Directory.CreateDirectory(fullPath);
                                var xlsFile = JIT.Utility.DataTableExporter.WriteXLS(tempData, 0);
                                xlsFile.Save(HttpContext.Current.Server.MapPath(tempFilePath));
                                resultData = tempFilePath;
                            }
                            this.ResponseContent(resultData);
                        }
                        #endregion
                        break;
                    case "5":
                        #region 导出地图&数据
                        {
                            var tempData = GetKpiData();
                            string resultData;
                            if (tempData == null)
                            {
                                resultData = "null";
                            }
                            else
                            {
                                string tempFolder = "/Module/MapAnalysis/Common/Download/" + Guid.NewGuid().ToString();
                                string tempFilePath = tempFolder + "/KPI数据.xlsx";
                                string fullPath = HttpContext.Current.Server.MapPath(tempFolder);

                                //string imagePath = HttpContext.Current.Server.MapPath(pContext.Request.QueryString["file"]);
                                string imagePath = pContext.Request.QueryString["file"];
                                imagePath = getHttpFile(imagePath);
                                if (!System.IO.Directory.Exists(fullPath))
                                    System.IO.Directory.CreateDirectory(fullPath);
                                //var xlsFile = ExcelUtils.ExportFromDataTableByHeight(tempData, imagePath, 400);
                                var xlsFile = JIT.Utility.DataTableExporter.WriteXLS(tempData, 0);
                                //插入地图图片
                                xlsFile.Worksheets.Insert(0, Aspose.Cells.SheetType.Worksheet, "地图");
                                int imageWidth = 1052;
                                Image imgTemp = new Bitmap(imagePath);
                                double dImageWidth;
                                int imageHeight;
                                dImageWidth = imageWidth / (imgTemp.Width * 1.0);
                                dImageWidth = dImageWidth * imgTemp.Height;
                                imageHeight = Convert.ToInt32(dImageWidth);
                                xlsFile.Worksheets["地图"].Pictures.Add(0, 0, imagePath);
                                xlsFile.Worksheets["地图"].Pictures[0].Width = imageWidth;
                                xlsFile.Worksheets["地图"].Pictures[0].Height = imageHeight;
                                xlsFile.Save(HttpContext.Current.Server.MapPath(tempFilePath));
                                resultData = tempFilePath;
                            }
                            this.ResponseContent(resultData);
                        }
                        #endregion
                        break;
                    case "6":
                        #region 获取终端详情

                        var storeID = this.Request("sid");
                        //
                        //this.StoreName = string.Empty;
                        //SessionManager rSession = new SessionManager();
                        // 
                        //var bll = new StoreBLL(this.GetCurrentUserInfo());
                        var storeInfo = BrandSKUTreeHandler.GetStoreByID(storeID);
                        storeInfo.Col10 = DateTime.Now.ToString("yyyy-MM-dd");//使用Col10作为临时存储字段
                        this.ResponseContent(storeInfo.ToJSON());
                        #endregion
                        break; 
                    
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public class UnitListEntity
        {
            public IList<UnitEntity> UnitList;
            public IList<UnitEntity> StoreList;
        }
        public class UnitEntity
        {
            public string UnitId;
        }

        //将CPOS的终端ID转换为渠道云的终端ID
        protected string ConvertCPOSStoreIDToQDYStoreID(string unitStr)
        {
            var unitListObj = unitStr.DeserializeJSONTo<UnitListEntity>();
            unitListObj.StoreList = new List<UnitEntity>();
            for (var i = 0; i < unitListObj.UnitList.Count; i++)
            {
                unitListObj.StoreList.Add(new UnitEntity() { UnitId = ConfigurationManager.AppSettings["qdy_store1"] });
                unitListObj.StoreList.Add(new UnitEntity() { UnitId = ConfigurationManager.AppSettings["qdy_store2"] });
                unitListObj.StoreList.Add(new UnitEntity() { UnitId = ConfigurationManager.AppSettings["qdy_store3"] });
            }
            return unitListObj.ToJSON();
        }

        protected string GetCurrentClientID()
        {
            return "27";
        }

        //protected TenantPlatform.Utility.TenantPlatformUserInfo GetCurrentUserInfo()
        //{
        //    var userInfo = new TenantPlatform.Utility.TenantPlatformUserInfo();
        //    userInfo.AppModel = this.CurrentUserInfo.AppModel;
        //    userInfo.ClientDistributorID = this.CurrentUserInfo.ClientDistributorID;
        //    userInfo.ClientID = "27";
        //    userInfo.ClientUserRealName = this.CurrentUserInfo.ClientUserRealName;
        //    userInfo.ConnectionString = this.CurrentUserInfo.ConnectionString;
        //    userInfo.ImgPath = this.CurrentUserInfo.ImgPath;
        //    userInfo.Lang = this.CurrentUserInfo.Lang;
        //    userInfo.StructureLevel = this.CurrentUserInfo.StructureLevel;
        //    userInfo.UserID = this.CurrentUserInfo.UserID;
        //    userInfo.UserOPRight = this.CurrentUserInfo.UserOPRight;

        //    return userInfo;
        //}

        private string getHttpFile(string pHttpFileUrl)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(pHttpFileUrl);
            httpRequest.UserAgent = "netclient";
            string fileName = pHttpFileUrl.Substring(pHttpFileUrl.LastIndexOf("/") + 1);
            string tempFolder = "/Module/MapAnalysis/Common/" + Guid.NewGuid().ToString();
            string tempFilePath = tempFolder;
            tempFilePath = HttpContext.Current.Server.MapPath(tempFilePath);
            if (!System.IO.Directory.Exists(tempFilePath))
                System.IO.Directory.CreateDirectory(tempFilePath);
            tempFilePath += "/" + fileName;
            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream stream = myResponse.GetResponseStream();
            //Byte[] imageByte = new Byte[myResponse.ContentLength];
            //myResponse.GetResponseStream().Read(imageByte, 0, imageByte.Length);
            FileStream fs = new FileStream(tempFilePath, FileMode.Create);

            byte[] nbytes = new byte[512];
            int nReadSize = 0;
            nReadSize = stream.Read(nbytes, 0, 512);
            while (nReadSize > 0)
            {
                fs.Write(nbytes, 0, nReadSize);
                nReadSize = stream.Read(nbytes, 0, 512);
            }
            fs.Close();
            stream.Close();
            myResponse.Close();
            return tempFilePath; 
        }

        private DataTable GetKpiData()
        {
            var kpiFilters = this.DeserializeJSONContent<KPIFilter>();
            MapKpiServiceClient service = new MapKpiServiceClient();
            //
            string start = null;
            string end = null;
            if (kpiFilters.IsCheckCustomizeDatePeriod.HasValue && kpiFilters.IsCheckCustomizeDatePeriod.Value)
            {
                start = kpiFilters.StartDate.HasValue ? kpiFilters.StartDate.Value.ToString("yyyy-MM-dd") : string.Empty;
                end = kpiFilters.EndDate.HasValue ? kpiFilters.EndDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            }
            else
            {
                switch (kpiFilters.DatePeriod)
                {
                    case "1":   //年
                        start = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                        end = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    case "2":   //季度
                        start = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                        end = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    case "3":   //月
                        start = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                        end = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    case "4":   //周
                        start = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                        end = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                }
            }
            //
            string[] channelIDs = kpiFilters.ChannelID;
            //if (!string.IsNullOrWhiteSpace(kpiFilters.ChannelID))
            //{
            //    channelIDs = new string[] { kpiFilters.ChannelID };
            //}
            string[] chainIDs = null;
            if (!string.IsNullOrWhiteSpace(kpiFilters.ChainID))
            {
                chainIDs = new string[] { kpiFilters.ChainID };
            }
            //
            var kpiQuery = new KPIQuery();
            kpiQuery.Level = kpiFilters.Level;
            kpiQuery.BoundID = kpiFilters.BoundID;
            kpiQuery.KPIID = kpiFilters.KPIID;
            kpiQuery.StyleCode = kpiFilters.StyleCode;
            kpiQuery.StyleSubCode = kpiFilters.StyleCode;
            kpiQuery.DateFrom = start;
            kpiQuery.DateTo = end;
            kpiQuery.SKUIDs = kpiFilters.SKUIDs;
            kpiQuery.SKUBrandIDs = kpiFilters.BrandIDs;
            kpiQuery.SKUCategoryIDs = kpiFilters.CategoryIDs;
            kpiQuery.StoreIDs = null;
            kpiQuery.ChainIDs = chainIDs;
            kpiQuery.ChannelIDs = channelIDs;
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = kpiQuery.ToJSON() });
            //
            var tempData = service.GetKpiDataExportData(kpiFilters.Level, kpiFilters.BoundID, kpiFilters.KPIID, kpiFilters.StyleCode, kpiFilters.StyleSubCode, kpiFilters.SKUIDs, kpiFilters.BrandIDs, kpiFilters.CategoryIDs, null, channelIDs, chainIDs, start, end, this.GetCurrentClientID(), null);
            return tempData;
        }
        #endregion

        #region 内部类
        class KPIFilter
        {
            public int Level { get; set; }
            public string BoundID { get; set; }
            public string KPIID { get; set; }
            public string StyleCode { get; set; }
            public string StyleSubCode { get; set; }
            public string DatePeriod { get; set; }
            public bool? IsCheckCustomizeDatePeriod { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string[] ChannelID { get; set; }
            public string ChainID { get; set; }
            public string[] BrandIDs { get; set; }
            public string[] CategoryIDs { get; set; }
            public string[] SKUIDs { get; set; }
        }

        class KPIQuery
        {
            public int Level { get; set; }
            public string BoundID { get; set; }
            public string KPIID { get; set; }
            public string StyleCode { get; set; }
            public string StyleSubCode { get; set; }
            public string[] SKUIDs { get; set; }
            public string[] SKUBrandIDs { get; set; }
            public string[] SKUCategoryIDs { get; set; }
            public string[] StoreIDs { get; set; }
            public string[] ChannelIDs { get; set; }
            public string[] ChainIDs { get; set; }
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
        }
        #endregion
    }
}