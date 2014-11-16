using System.Collections.Generic;
using System.Web;
using System.Linq;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using System.Configuration;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.Module.CRM.Handler 
{
    /// <summary>
    /// CRMHandler
    /// </summary>
    public class CRMHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "EEnterpriseCustomers_query":
                    content = GetEEnterpriseCustomersData();
                    break;
                case "EEnterpriseCustomers_delete":
                    content = EEnterpriseCustomersDeleteData();
                    break;
                case "get_EEnterpriseCustomers_by_id":
                    content = GetEEnterpriseCustomersById();
                    break;
                case "EEnterpriseCustomers_save":
                    content = SaveEEnterpriseCustomers();
                    break;

                case "VipEnterprise_query":
                    content = GetVipEnterpriseData();
                    break;
                case "VipEnterprise_delete":
                    content = VipEnterpriseDeleteData();
                    break;
                case "get_VipEnterprise_by_id":
                    content = GetVipEnterpriseById();
                    break;
                case "VipEnterprise_save":
                    content = SaveVipEnterprise();
                    break;
                case "ECSearch":
                    content = ECSearchData();
                    break;
                case "ESalesVisit_query":
                    content = GetESalesVisitData();
                    break;
                case "get_ESales_by_id":
                    content = GetESalesById();
                    break;
                case "ESalesVisitVip_query":
                    content = GetESalesVisitVipData();
                    break;
                case "ObjectDownloads_query":
                    content = GetObjectDownloadsData();
                    break;
                case "ESales_save":
                    content = SaveESales();
                    break;
                case "ESalesVisit_save":
                    content = SaveESalesVisit();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region EEnterpriseCustomersDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string EEnterpriseCustomersDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            int? status = int.Parse(Request("status"));
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "活动ID不能为空";
                return responseData.ToJSON();
            }

            //string[] ids = key.Split(',');
            //new EEnterpriseCustomersBLL(this.CurrentUserInfo).Delete(ids);
            new EEnterpriseCustomersBLL(this.CurrentUserInfo).SetStatus(new EEnterpriseCustomersEntity() {
                EnterpriseCustomerId = key,
                Status = status
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetEEnterpriseCustomersData 

        /// <summary>
        /// 查询列表
        /// </summary>
        public string GetEEnterpriseCustomersData()
        {
            var form = Request("form").DeserializeJSONTo<EEnterpriseCustomersQueryEntity>();
            var eEnterpriseCustomersBLL = new EEnterpriseCustomersBLL(this.CurrentUserInfo);

            string content = string.Empty;

            var id = form.EnterpriseCustomerId;
            string Name = FormatParamValue(form.Name);
            string TypeId = FormatParamValue(form.TypeId);
            string IndustryId = FormatParamValue(form.IndustryId);
            string ScaleId = FormatParamValue(form.ScaleId);
            string CityId = FormatParamValue(form.CityId);
            string ECSourceId = FormatParamValue(form.ECSourceId);
            int? Status = form.Status;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            EEnterpriseCustomersEntity queryEntity = new EEnterpriseCustomersEntity();
            queryEntity.EnterpriseCustomerId = id;
            queryEntity.EnterpriseCustomerName = Name;
            queryEntity.TypeId = TypeId;
            queryEntity.IndustryId = IndustryId;
            queryEntity.ScaleId = ScaleId;
            queryEntity.CityId = CityId;
            queryEntity.ECSourceId = ECSourceId;
            queryEntity.Status = Status;

            var data = eEnterpriseCustomersBLL.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = eEnterpriseCustomersBLL.GetListCount(queryEntity);

            //if (data != null && data.Count > 0)
            //{
            //    foreach (var item in data)
            //    {
            //        var mapList = zLargeForumCourseMappingBLL.QueryByEntity(new ZLargeForumCourseMappingEntity() {
            //            ForumId = item.ForumId
            //        }, null);
            //        if (mapList != null && mapList.Length > 0)
            //        {
            //            item.CourseName = "";
            //            foreach (var mapItem in mapList)
            //            {
            //                var tmpCourceObj = zCourseBLL.GetByID(mapItem.CourseId);
            //                if (tmpCourceObj != null)
            //                {
            //                    item.CourseName += tmpCourceObj.CourseName + ", ";
            //                }
            //            }
            //            if (item.CourseName != null && item.CourseName.EndsWith(", "))
            //            {
            //                item.CourseName = item.CourseName.Substring(0, item.CourseName.Length - 2);
            //            }
            //        }
                    
            //    }
            //}

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region GetEEnterpriseCustomersById 
        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        public string GetEEnterpriseCustomersById()
        {
            var eEnterpriseCustomersBLL = new EEnterpriseCustomersBLL(this.CurrentUserInfo);
            var cityBLL = new CityService(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                key = FormatParamValue(Request("id")).ToString().Trim();
            }

            var data = eEnterpriseCustomersBLL.GetByID(key);

            if (data.CityId != null && data.CityId.Length > 0)
            {
                var city = cityBLL.GetCityById(cityBLL.GetCityGUIDByCityCode(data.CityId));
                data.CityName = city.City1_Name + city.City2_Name + city.City3_Name;
            }

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveEEnterpriseCustomers
        /// <summary>
        /// 保存信息
        /// </summary>
        public string SaveEEnterpriseCustomers()
        {
            var eEnterpriseCustomersBLL = new EEnterpriseCustomersBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string id = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                id = FormatParamValue(Request("id")).ToString().Trim();
            }

            var itemEntity = key.DeserializeJSONTo<EEnterpriseCustomersEntity>();

            if (itemEntity.EnterpriseCustomerName == null || itemEntity.EnterpriseCustomerName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }

            if (id.Trim().Length == 0)
            {
                itemEntity.EnterpriseCustomerId = Utils.NewGuid();
                itemEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                eEnterpriseCustomersBLL.Create(itemEntity);
            }
            else
            {
                itemEntity.EnterpriseCustomerId = id;
                eEnterpriseCustomersBLL.Update(itemEntity, false);
            }

            responseData.data = itemEntity.EnterpriseCustomerId;
            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region VipEnterpriseDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string VipEnterpriseDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            int? status = int.Parse(Request("status"));
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "活动ID不能为空";
                return responseData.ToJSON();
            }

            //string[] ids = key.Split(',');
            //new VipEnterpriseBLL(this.CurrentUserInfo).Delete(ids);
            new VipEnterpriseExpandBLL(this.CurrentUserInfo).SetIsDelete(new VipEnterpriseExpandEntity()
            {
                VipId = key,
                IsDelete = status
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetVipEnterpriseData

        /// <summary>
        /// 查询列表
        /// </summary>
        public string GetVipEnterpriseData()
        {
            var form = Request("form").DeserializeJSONTo<VipEnterpriseQueryEntity>();
            var vipEnterpriseExpandBLL = new VipEnterpriseExpandBLL(this.CurrentUserInfo);

            string content = string.Empty;

            var id = form.VipId;
            string VipName = FormatParamValue(form.VipName);
            string EnterpriseCustomerId = FormatParamValue(Request("ECCustomerId"));
            int? Status = form.Status;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            VipEnterpriseExpandEntity queryEntity = new VipEnterpriseExpandEntity();
            queryEntity.VipId = id;
            queryEntity.VipName = VipName;
            queryEntity.EnterpriseCustomerId = EnterpriseCustomerId;
            queryEntity.Status = Status;

            var data = vipEnterpriseExpandBLL.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = vipEnterpriseExpandBLL.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region GetVipEnterpriseById
        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        public string GetVipEnterpriseById()
        {
            var vipEnterpriseExpandBLL = new VipEnterpriseExpandBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                key = FormatParamValue(Request("id")).ToString().Trim();
            }

            VipEnterpriseExpandEntity queryEntity = new VipEnterpriseExpandEntity();
            queryEntity.VipId = key;
            var list = vipEnterpriseExpandBLL.GetList(queryEntity, 1, 1);
            VipEnterpriseExpandEntity data = null;
            if (list != null && list.Count > 0)
            {
                data = list[0];
            }

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveVipEnterprise
        /// <summary>
        /// 保存信息
        /// </summary>
        public string SaveVipEnterprise()
        {
            var vipEnterpriseExpandBLL = new VipEnterpriseExpandBLL(this.CurrentUserInfo);
            var vipBLL = new VipBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string id = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                id = FormatParamValue(Request("id")).ToString().Trim();
            }

            var itemEntity = key.DeserializeJSONTo<VipEnterpriseExpandEntity>();

            if (itemEntity.Vip.VipName == null || itemEntity.Vip.VipName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "姓名不能为空";
                return responseData.ToJSON();
            }

            if (id.Trim().Length == 0)
            {
                itemEntity.VipId = Utils.NewGuid();
                itemEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                itemEntity.Vip.VIPID = itemEntity.VipId;
                vipEnterpriseExpandBLL.Create(itemEntity);
                itemEntity.Vip.ClientID = this.CurrentUserInfo.CurrentUser.customer_id;
                itemEntity.Status = 1;
                vipBLL.Create(itemEntity.Vip);
            }
            else
            {
                itemEntity.VipId = id;
                itemEntity.Vip.VIPID = itemEntity.VipId;
                itemEntity.Vip.ClientID = this.CurrentUserInfo.CurrentUser.customer_id;
                vipEnterpriseExpandBLL.Update(itemEntity, false);

                vipBLL.Update(itemEntity.Vip, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region ECSearchData

        /// <summary>
        /// 查询列表
        /// </summary>
        public string ECSearchData()
        {
            var eEnterpriseCustomersBLL = new EEnterpriseCustomersBLL(this.CurrentUserInfo);
            StringBuilder content = new StringBuilder();
            string Name = FormatParamValue(Request("name"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            EEnterpriseCustomersEntity queryEntity = new EEnterpriseCustomersEntity();
            queryEntity.EnterpriseCustomerName = Name;
            var data = eEnterpriseCustomersBLL.GetTopList(queryEntity);
            if (data != null && data.Count > 0)
            {
                content.AppendFormat("<table style=\"width:100%; border:1px solid #ccc;\">");
                content.AppendFormat("<tr><td class=\"z_cs_td\" style=\"width:50%;\">客户名称</td>");
                content.AppendFormat("<td class=\"z_cs_td\" style=\"width:30%;\">销售负责人</td>");
                content.AppendFormat("<td class=\"z_cs_td\" style=\"width:20%;\">类型</td>");
                content.AppendFormat("</tr>");
                foreach (var item in data)
                {
                    content.AppendFormat("<tr>");
                    content.AppendFormat("<td class=\"z_cs_td2\" onclick=\"fnECSelect('{0}','{1}')\">{1}</td>",
                        item.EnterpriseCustomerId, item.EnterpriseCustomerName);
                    content.AppendFormat("<td class=\"z_cs_td2\" onclick=\"fnECSelect('{0}','{2}')\">{1}</td>",
                        item.EnterpriseCustomerId, "", item.EnterpriseCustomerName);
                    content.AppendFormat("<td class=\"z_cs_td2\" onclick=\"fnECSelect('{0}','{2}')\">{1}</td>",
                        item.EnterpriseCustomerId, item.TypeName, item.EnterpriseCustomerName);
                    content.AppendFormat("</tr>");
                }
                content.AppendFormat("</table>");
            }
            else
            {
                content.AppendFormat("<div>未查询到数据</div>");
            }
            return content.ToString();
        }
        #endregion

        #region GetESalesVisitData

        /// <summary>
        /// 查询列表
        /// </summary>
        public string GetESalesVisitData()
        {
            var eSalesVisitBLL = new ESalesVisitBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string id = FormatParamValue(Request("SalesVisitId"));
            string SalesVisitName = FormatParamValue(Request("SalesVisitName"));
            string SalesId = FormatParamValue(Request("SalesId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            int pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize"))) == 0 ? PageSize : Utils.GetIntVal(FormatParamValue(Request("pageSize")));

            ESalesVisitEntity queryEntity = new ESalesVisitEntity();
            queryEntity.SalesVisitId = id;
            queryEntity.SalesVisitName = SalesVisitName;
            queryEntity.SalesId = SalesId;

            var data = eSalesVisitBLL.GetList(queryEntity, pageIndex, pageSize);
            var dataTotalCount = eSalesVisitBLL.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region GetESalesById
        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        public string GetESalesById()
        {
            var eSalesBLL = new ESalesBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                key = FormatParamValue(Request("id")).ToString().Trim();
            }

            int totalCount = 0;
            ESalesEntity queryEntity = new ESalesEntity();
            queryEntity.SalesId = key;
            var list = eSalesBLL.GetSalesList(queryEntity, 0, 1, out totalCount);
            ESalesEntity data = null;
            if (list != null && list.Count > 0)
            {
                data = list[0];
            }

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region GetESalesVisitVipData
        /// <summary>
        /// 查询列表
        /// </summary>
        public string GetESalesVisitVipData()
        {
            var eSalesVisitBLL = new ESalesVisitVipMappingBLL(this.CurrentUserInfo);
            var vipBLL = new VipBLL(this.CurrentUserInfo);
            var vipEnterpriseExpandBLL = new VipEnterpriseExpandBLL(this.CurrentUserInfo);
            var ePolicyDecisionRoleBLL = new EPolicyDecisionRoleBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string SalesVisitId = FormatParamValue(Request("SalesVisitId"));
            string MappingId = FormatParamValue(Request("MappingId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            int pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize"))) == 0 ? PageSize : Utils.GetIntVal(FormatParamValue(Request("pageSize")));

            ESalesVisitVipMappingEntity queryEntity = new ESalesVisitVipMappingEntity();
            queryEntity.SalesVisitId = SalesVisitId;
            queryEntity.MappingId = MappingId;

            var data = eSalesVisitBLL.GetList(queryEntity, pageIndex, pageSize);
            var dataTotalCount = eSalesVisitBLL.GetListCount(queryEntity);

            if (data != null)
            {
                foreach (var item in data)
                {
                    var vipObj = vipBLL.GetByID(item.VipId);
                    var vipEnterpriseExpandObj = vipEnterpriseExpandBLL.GetByID(item.VipId);
                    if (vipEnterpriseExpandObj == null) vipEnterpriseExpandObj = new VipEnterpriseExpandEntity();
                    var ePolicyDecisionRoleObj = ePolicyDecisionRoleBLL.GetByID(vipEnterpriseExpandObj.PDRoleId);
                    if (ePolicyDecisionRoleObj == null) ePolicyDecisionRoleObj = new EPolicyDecisionRoleEntity();
                    item.VipName = vipObj.VipName;
                    item.Position = vipEnterpriseExpandObj.Position;
                    item.Department = vipEnterpriseExpandObj.Department;
                    item.PDRoleId = vipEnterpriseExpandObj.PDRoleId;
                    item.PDRoleName = ePolicyDecisionRoleObj.PDRoleName;
                }
            }

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region GetObjectDownloadsData
        /// <summary>
        /// 查询列表
        /// </summary>
        public string GetObjectDownloadsData()
        {
            var eSalesVisitBLL = new ObjectDownloadsBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string ObjectId = FormatParamValue(Request("ObjectId"));
            string DownloadId = FormatParamValue(Request("DownloadId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            int pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize"))) == 0 ? PageSize : Utils.GetIntVal(FormatParamValue(Request("pageSize")));

            ObjectDownloadsEntity queryEntity = new ObjectDownloadsEntity();
            queryEntity.ObjectId = ObjectId;
            queryEntity.DownloadId = DownloadId;

            var data = eSalesVisitBLL.GetList(queryEntity, pageIndex, pageSize);
            var dataTotalCount = eSalesVisitBLL.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region SaveESales
        /// <summary>
        /// 保存信息
        /// </summary>
        public string SaveESales()
        {
            var eSalesBLL = new ESalesBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string id = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                id = FormatParamValue(Request("id")).ToString().Trim();
            }

            var itemEntity = key.DeserializeJSONTo<ESalesEntity>();

            if (itemEntity.SalesName == null || itemEntity.SalesName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }

            if (id.Trim().Length == 0)
            {
                itemEntity.SalesId = Utils.NewGuid();
                itemEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                eSalesBLL.Create(itemEntity);
            }
            else
            {
                itemEntity.SalesId = id;
                eSalesBLL.Update(itemEntity, false);
            }

            responseData.data = itemEntity.SalesId;
            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveESalesVisit
        /// <summary>
        /// 保存信息
        /// </summary>
        public string SaveESalesVisit()
        {
            var eSalesVisitBLL = new ESalesVisitBLL(this.CurrentUserInfo);
            var eSalesBLL = new ESalesBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string id = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                id = FormatParamValue(Request("id")).ToString().Trim();
            }

            var itemEntity = key.DeserializeJSONTo<ESalesVisitEntity>();

            if (itemEntity.SalesId == null || itemEntity.SalesId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "业务ID不能为空";
                return responseData.ToJSON();
            }

            if (id.Trim().Length == 0)
            {
                itemEntity.SalesVisitId = Utils.NewGuid();
                eSalesVisitBLL.Create(itemEntity);
            }
            else
            {
                itemEntity.SalesVisitId = id;
                eSalesVisitBLL.Update(itemEntity, false);
            }

            if (itemEntity.StageId != null && itemEntity.StageId.Length > 0)
            {
                eSalesBLL.Update(new ESalesEntity()
                {
                    SalesId = itemEntity.SalesId,
                    StageId = itemEntity.StageId
                }, false);
            }

            var objectDownloadsBLL = new ObjectDownloadsBLL(CurrentUserInfo);
            var tmpImageList = objectDownloadsBLL.QueryByEntity(
                new ObjectDownloadsEntity() { ObjectId = itemEntity.SalesVisitId }, null);
            if (tmpImageList != null && tmpImageList.Length > 0)
            {
                foreach (var tmpImageItem in tmpImageList)
                {
                    objectDownloadsBLL.Delete(tmpImageItem);
                }
            }
            if (itemEntity.ObjectDownloadsList != null)
            {
                foreach (var tmpImageItem in itemEntity.ObjectDownloadsList)
                {
                    tmpImageItem.ObjectId = itemEntity.SalesVisitId;
                    objectDownloadsBLL.Create(tmpImageItem);
                }
            }

            var eSalesVisitVipMappingBLL = new ESalesVisitVipMappingBLL(CurrentUserInfo);
            var eSalesVisitVipMappingList = eSalesVisitVipMappingBLL.QueryByEntity(
                new ESalesVisitVipMappingEntity() { SalesVisitId = itemEntity.SalesVisitId }, null);
            if (eSalesVisitVipMappingList != null && eSalesVisitVipMappingList.Length > 0)
            {
                foreach (var eSalesVisitVipMappingItem in eSalesVisitVipMappingList)
                {
                    eSalesVisitVipMappingBLL.Delete(eSalesVisitVipMappingItem);
                }
            }
            if (itemEntity.ESalesVisitVipMappingIds != null)
            {
                foreach (var ESalesVisitVipMappingId in itemEntity.ESalesVisitVipMappingIds)
                {
                    eSalesVisitVipMappingBLL.Create(new ESalesVisitVipMappingEntity() { 
                        MappingId = Utils.NewGuid(),
                        VipId = ESalesVisitVipMappingId,
                        SalesVisitId = itemEntity.SalesVisitId
                    });
                }
            }

            responseData.success = true;
            responseData.msg = error;
            content = responseData.ToJSON();
            return content;
        }

        #endregion

    }

    #region QueryEntity
    public class EEnterpriseCustomersQueryEntity
    {
        public string EnterpriseCustomerId;
        public string Name;
        public string TypeId;
        public string IndustryId;
        public string ScaleId;
        public string CityId;
        public string Tel;
        public string Address;
        public string ECSourceId;
        public int? Status;
    }
    public class VipEnterpriseQueryEntity
    {
        public string VipId;
        public string VipName;
        public string EnterpriseCustomerId;
        public int? Status;
    }
    #endregion
}