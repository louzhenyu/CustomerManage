using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Module.Reservation.ReservationStatus.Handler
{
    /// <summary>
    /// StoreItemDailyStatusHandler 的摘要说明
    /// </summary>
    public class StoreItemDailyStatusHandler : PageBase.JITCPOSAjaxHandler
    {

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "BatchUpdate":      //批量修改
                    content = BatchUpdate();
                    break;
                case "GetList":
                    content = GetList();
                    break;
                case "GetById":
                    content = GetById();
                    break;
                case "Add":
                    content = Add();
                    break;
                case "Update":
                    content = Update();
                    break;
                case "getHouseType":
                    content = GetHouseType();
                    break;
                case "GetStoreList":
                    content = GetStoreList();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        private string Update()
        {
            var resp = new ResponseData();
            try
            {
                string requContent = CurrentContext.Request["content"];
                var dailyStatusEntity = requContent.DeserializeJSONTo<StoreItemDailyStatusEntity>();
                StoreItemDailyStatusBLL bll = new StoreItemDailyStatusBLL(CurrentUserInfo);
                bll.Update(dailyStatusEntity);
                return dailyStatusEntity.ToJSON();
            }
            catch (Exception ex)
            {
                resp.status = "101";
                resp.msg = ex.Message;
            }

            return resp.ToJSON();
        }

        private string Add()
        {
            var resp = new ResponseData();
            try
            {
                string requContent = CurrentContext.Request["content"];
                var dailyStatusEntity = requContent.DeserializeJSONTo<StoreItemDailyStatusEntity>();
                StoreItemDailyStatusBLL bll = new StoreItemDailyStatusBLL(CurrentUserInfo);
                int totalCount = 0;
                IList<StoreItemDailyStatusEntity> dailyStatusEntities = bll.GetList(dailyStatusEntity.StatusDate.Value.ToString("yyyy-MM-dd")
                                                                    , dailyStatusEntity.StatusDate.Value.ToString("yyyy-MM-dd")
                                                                    , dailyStatusEntity.StoreID
                                                                    , dailyStatusEntity.SkuID
                                                                    , 1
                                                                    , 10
                                                                    , out totalCount);

                int LowestPrice = 0;
                int StockAmount = 0;
                int.TryParse(CurrentContext.Request["LowestPrice"], out LowestPrice);
                int.TryParse(CurrentContext.Request["StockAmount"], out StockAmount);

                //如果发生并发操作，有则修改，无则创建
                if (dailyStatusEntities != null && dailyStatusEntities.Count > 0)
                {
                    if (CurrentContext.Request["OperateType"] == "price")
                    {
                        dailyStatusEntities[0].LowestPrice = LowestPrice;
                    }
                    else
                    {
                        dailyStatusEntities[0].StockAmount = StockAmount;
                    }

                    bll.Update(dailyStatusEntities[0]);
                }
                else
                {
                    bll.Create(dailyStatusEntity);
                }
                return dailyStatusEntity.ToJSON();
            }
            catch (Exception ex)
            {
                resp.status = "101";
                resp.msg = ex.Message;
            }

            return resp.ToJSON();
        }

        private string GetById()
        {
            string id = CurrentContext.Request["id"];
            StoreItemDailyStatusBLL bll = new StoreItemDailyStatusBLL(CurrentUserInfo);
            StoreItemDailyStatusEntity dailyStatusEntity = bll.GetByID(id);
            return dailyStatusEntity.ToJSON();
        }

        private string GetList()
        {
            string beginDate = CurrentContext.Request["beginDate"];
            string endDate = CurrentContext.Request["endDate"];
            string storeID = CurrentContext.Request["storeID"];
            string skuID = CurrentContext.Request["skuID"];
            int pageIndex = int.Parse(CurrentContext.Request["pageIndex"]);
            int pageSize = int.Parse(CurrentContext.Request["pageSize"]);

            StoreItemDailyStatusBLL bll = new StoreItemDailyStatusBLL(CurrentUserInfo);
            int totalCount;
            IList<StoreItemDailyStatusEntity> dailyStatusEntities = bll.GetList(beginDate, endDate, storeID, skuID,
                                                                                   pageIndex, pageSize, out totalCount);
            string content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                 dailyStatusEntities.ToJSON(),
                 totalCount);
            return content;

        }

        private string BatchUpdate()
        {
            var resp = new ResponseData();
            try
            {
                string requContent = CurrentContext.Request["content"];
                StoreItemDailyStatusBLL bll = new StoreItemDailyStatusBLL(CurrentUserInfo);//这里传的CurrentUserInfo

                DateTime beginDate = Convert.ToDateTime(CurrentContext.Request["beginDate"]);
                DateTime endDate = Convert.ToDateTime(CurrentContext.Request["endDate"]);

                string storeID = CurrentContext.Request["storeID"];
                string skuID = CurrentContext.Request["skuID"];
                string chkWeek = CurrentContext.Request["date"];
                int totalCount = 0;
                //查找列表
                IList<StoreItemDailyStatusEntity> dailyStatusEntities = bll.GetList(CurrentContext.Request["beginDate"]
                                                                                    , CurrentContext.Request["endDate"], storeID, skuID, 1, 100000, out totalCount);

                int LowestPrice = 0;
                int StockAmount = 0;
                int SourcePrice = 0;
                string oLowestPrice = CurrentContext.Request["LowestPrice"];
                string  oSourcePrice = CurrentContext.Request["SourcePrice"];
                int.TryParse(CurrentContext.Request["LowestPrice"], out LowestPrice);//用这种容错方法
                int.TryParse(CurrentContext.Request["SourcePrice"], out SourcePrice);//新增加原价
                int.TryParse(CurrentContext.Request["StockAmount"], out StockAmount);// StockAmount: 100,  //非满房，什么意思？
                for (; beginDate <= endDate; beginDate = beginDate.AddDays(1))
                {
                    if (dailyStatusEntities != null && dailyStatusEntities.Count > 0)
                    {
                        if (dailyStatusEntities.Where(p => p.StatusDate.Value.ToString("yyyy-MM-dd") == beginDate.ToString("yyyy-MM-dd") && p.StoreID == storeID && p.SkuID == skuID).Count() <= 0)   //查不到记录
                        {
                            StoreItemDailyStatusEntity model = new StoreItemDailyStatusEntity();
                            model.LowestPrice = LowestPrice;
                            model.SourcePrice = SourcePrice; //再加一个原价
                            model.StockAmount = StockAmount;
                            model.StatusDate = beginDate;
                            model.StoreID = storeID;
                            model.SkuID = skuID;
                            dailyStatusEntities.Add(model);//添加到列表
                        }
                    }
                    else
                    {
                        StoreItemDailyStatusEntity model = new StoreItemDailyStatusEntity();//如果列表记录是空的话
                        model.LowestPrice = LowestPrice;
                        model.SourcePrice = SourcePrice; //再加一个原价
                        model.StockAmount = StockAmount;
                        model.StatusDate = beginDate;
                        model.StoreID = storeID;
                        model.SkuID = skuID;
                        dailyStatusEntities.Add(model);//添加到列表
                    }
                }
                foreach (var item in dailyStatusEntities)   //遍历
                {
                    //判断是否有星期选择
                    if (chkWeek != null && chkWeek != "")
                    {
                        if (chkWeek.Contains(GetWeekByDate(DateTime.Parse(item.StatusDate.ToString()).DayOfWeek.ToString())))
                        {
                            if (CurrentContext.Request["OperateType"] == "price")
                            {
                                item.LowestPrice = LowestPrice;
                                item.SourcePrice = SourcePrice;//新增加了原价
                            }
                            else
                            {
                                item.StockAmount = StockAmount;
                            }
                        }
                    }
                    else
                    {
                        if (CurrentContext.Request["OperateType"] == "price")
                        {
                            item.LowestPrice = LowestPrice;
                            item.SourcePrice = SourcePrice;//新增加了原价
                        }
                        else
                        {
                            item.StockAmount = StockAmount;
                        }
                    }
                }
                bll.Update(dailyStatusEntities, oLowestPrice, oSourcePrice);//更新
                resp.status = "200";
                resp.msg = "操作成功";
            }
            catch (Exception ex)
            {
                resp.status = "101";
                resp.msg = ex.Message;
            }

            return resp.ToJSON();
        }

        /// <summary>
        /// 获取房型列表
        /// </summary>
        /// <returns></returns>
        private string GetHouseType()
        {
            string storeID = CurrentContext.Request["storeID"];
            return new StoreItemDailyStatusBLL(CurrentUserInfo).GetItemList(storeID).ToJSON();
        }

        /// <summary>
        /// 获取终端列表
        /// </summary>
        /// <returns></returns>
        private string GetStoreList()
        {
            return new StoreItemDailyStatusBLL(CurrentUserInfo).GetStoreList().ToJSON();
        }

        private string GetWeekByDate(string pDate)
        {
            string res = "";
            switch (pDate)
            {
                case "Monday":
                    res = "1";
                    break;
                case "Tuesday":
                    res = "2";
                    break;
                case "Wednesday":
                    res = "3";
                    break;
                case "Thursday":
                    res = "4";
                    break;
                case "Friday":
                    res = "5";
                    break;
                case "Saturday":
                    res = "6";
                    break;
                case "Sunday":
                    res = "0";
                    break;
            }
            return res;
        }
    }
}