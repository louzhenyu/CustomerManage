using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;
using JIT.CPOS.BS.Web.Extension;
using System.Text;
using System.Data;

namespace JIT.CPOS.BS.Web.Module.CallSetting.Step.Handler
{
    /// <summary>
    /// StepHandler 的摘要说明
    /// </summary>
    public class StepHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                            case "GetStepByID":
                                res = GetStepByID(pContext.Request.Form["id"]);
                                break;

                            //拜访参数
                            case "GetStepParameterList":
                                res = GetStepParameterList(pContext.Request.Form);
                                break;

                            //品牌、品类
                            case "GetStepLevel":
                                res = GetStepLevel(pContext.Request.Form["stepid"]);
                                break;
                            //品牌
                            case "GetStepBrandType":
                                res = GetStepBrandType();
                                break;
                            case "GetStepBrandList":
                                res = GetStepBrandList(pContext.Request.Form);
                                break;

                            //品类
                            case "GetStepCategoryType":
                                res = GetStepCategoryType();
                                break;
                            case "GetStepCategoryList":
                                res = GetStepCategoryList(pContext.Request.Form);
                                break;

                            //人员考评
                            case "GetStepPositionList":
                                res = GetStepPositionList(pContext.Request.Form);
                                break;


                        }
                    }
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetStepByID":
                                res = GetStepByID(pContext.Request.Form["id"]);
                                break;
                            case "EditStep":
                                res = EditStep(pContext.Request.Form);
                                break;

                            //拜访参数
                            case "GetStepParameterList":
                                res = GetStepParameterList(pContext.Request.Form);
                                break;
                            case "EditStepParameter":
                                res = EditStepParameter(pContext.Request.Form);
                                break;
                            //品牌、品类
                            case "GetStepLevel":
                                res = GetStepLevel(pContext.Request.Form["stepid"]);
                                break;
                            //品牌
                            case "GetStepBrandType":
                                res = GetStepBrandType();
                                break;
                            case "GetStepBrandList":
                                res = GetStepBrandList(pContext.Request.Form);
                                break;
                            case "EditStepObject_Brand":
                                res = EditStepObject_Brand(pContext.Request.Form);
                                break;
                            //品类
                            case "GetStepCategoryType":
                                res = GetStepCategoryType();
                                break;
                            case "GetStepCategoryList":
                                res = GetStepCategoryList(pContext.Request.Form);
                                break;
                            case "EditStepObject_Category":
                                res = EditStepObject_Category(pContext.Request.Form);
                                break;
                            //人员考评
                            case "GetStepPositionList":
                                res = GetStepPositionList(pContext.Request.Form);
                                break;
                            case "EditStepObject_Position":
                                res = EditStepObject_Position(pContext.Request.Form);
                                break;

                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetStepByID":
                                res = GetStepByID(pContext.Request.Form["id"]);
                                break;
                            case "EditStep":
                                res = EditStep(pContext.Request.Form);
                                break;

                            //拜访参数
                            case "GetStepParameterList":
                                res = GetStepParameterList(pContext.Request.Form);
                                break;
                            case "EditStepParameter":
                                res = EditStepParameter(pContext.Request.Form);
                                break;
                            //品牌、品类
                            case "GetStepLevel":
                                res = GetStepLevel(pContext.Request.Form["stepid"]);
                                break;
                            //品牌
                            case "GetStepBrandType":
                                res = GetStepBrandType();
                                break;
                            case "GetStepBrandList":
                                res = GetStepBrandList(pContext.Request.Form);
                                break;
                            case "EditStepObject_Brand":
                                res = EditStepObject_Brand(pContext.Request.Form);
                                break;
                            //品类
                            case "GetStepCategoryType":
                                res = GetStepCategoryType();
                                break;
                            case "GetStepCategoryList":
                                res = GetStepCategoryList(pContext.Request.Form);
                                break;
                            case "EditStepObject_Category":
                                res = EditStepObject_Category(pContext.Request.Form);
                                break;
                            //人员考评
                            case "GetStepPositionList":
                                res = GetStepPositionList(pContext.Request.Form);
                                break;
                            case "EditStepObject_Position":
                                res = EditStepObject_Position(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetStepByID
        private string GetStepByID(string id)
        {
            string res = new VisitingTaskStepBLL(CurrentUserInfo).GetByID(id).ToJSON();
            VisitingTaskStepObjectEntity objectEntity = new VisitingTaskStepObjectBLL(CurrentUserInfo).GetStepObjectList(Guid.Parse(id));
            if (objectEntity != null)
            {
                res = res.Replace("}", ",ObjectGroup:" + objectEntity.Target1ID + "}");
            }
            else
            {
                res = res.Replace("}", ",ObjectGroup:0}");
            }
            return "[" + res + "]";
        }
        #endregion

        #region EditStep
        private string EditStep(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'保存失败'}";

            //组装参数
            VisitingTaskStepEntity entity = new VisitingTaskStepEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new VisitingTaskStepBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<VisitingTaskStepEntity>(rParams, entity);

            entity.ClientID = CurrentUserInfo.ClientID;
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity.VisitingTaskStepID = Guid.Parse(rParams["id"]);
            }
            int result = new VisitingTaskStepBLL(CurrentUserInfo).EditStep(entity, rParams["ObjectGroup"]);
            switch (result)
            {
                case 1:
                    res = "{success:false,msg:'已经存在步骤类型为订单相关的步骤'}";
                    break;
                case 2:
                    res = "{success:true,msg:'保存成功',id:'" + entity.VisitingTaskStepID + "'}";
                    break;
            }
            return res;
        }
        #endregion

        #region GetStepParameterList
        private string GetStepParameterList(NameValueCollection rParams)
        {
            VisitingParameterViewEntity entity = new VisitingParameterViewEntity();
            entity.VisitingTaskStepID = Guid.Parse(rParams["id"]);
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingTaskParameterMappingBLL(CurrentUserInfo).GetStepParameterList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region EditStepParameter
        private string EditStepParameter(NameValueCollection rParams)
        {
            Guid stepid = Guid.Parse(rParams["id"]);
            int allSelectorStatus = rParams["allSelectorStatus"].ToInt();
            string defaultList = rParams["defaultList"];//关联表有的数据
            string includeList = rParams["includeList"];//新加的数据
            string excludeList = rParams["excludeList"];//排除的数据


            VisitingTaskParameterMappingEntity[] updateEntity = rParams["updateData"].ToString().DeserializeJSONTo<VisitingTaskParameterMappingEntity[]>();
            new VisitingTaskParameterMappingBLL(CurrentUserInfo).EditStepParameter(stepid, allSelectorStatus, defaultList, includeList, excludeList, updateEntity);
            return "{success:true,msg:'操作成功'}";
        }
        #endregion

        #region GetStepLevel
        /// <summary>
        /// 获取拜访步骤品牌、品类 等级
        /// </summary>
        /// <returns></returns>
        private string GetStepLevel(string stepid)
        {
            return new VisitingTaskStepObjectBLL(CurrentUserInfo).GetStepObjectLevel(Guid.Parse(stepid));
        }
        #endregion

        #region GetStepBrandType
        private string GetStepBrandType()
        {
            OptionsEntity optionEntity = new OptionsEntity();

            optionEntity.OptionName = "Brandlevel";
            optionEntity.ClientID = CurrentUserInfo.ClientID;
            OptionsEntity[] brandOption = new OptionsBLL(CurrentUserInfo).GetOptionByName(optionEntity);

            optionEntity.OptionName = "CategoryLevel";
            optionEntity.ClientID = CurrentUserInfo.ClientID;
            OptionsEntity[] categoryOption = new OptionsBLL(CurrentUserInfo).GetOptionByName(optionEntity);

            DataSet brandLevel = new BrandBLL(CurrentUserInfo).GetAllLevel();
            //DataSet categoryLevel =new DataSet(); //TODO:new CategoryBLL(CurrentUserInfo).GetAllLevel();

            StringBuilder sbRes = new StringBuilder();
            sbRes.Append("[");
            //品牌
            foreach (DataRow dr in brandLevel.Tables[0].Rows)
            {
                var query = brandOption.Where(m => m.OptionValue == dr["BrandLevel"].ToInt()).ToArray();
                if (query.Length == 1)
                {
                    OptionsEntity oEntity = query[0];
                    sbRes.Append("{name:'" + oEntity.OptionText + "',value:'" + oEntity.OptionValue + ",0'},");
                }
            }
            ////品牌+品类
            //foreach (DataRow dr in brandLevel.Tables[0].Rows)
            //{
            //    var query = brandOption.Where(m => m.OptionValue == dr["BrandLevel"].ToInt()).ToArray();
            //    if (query.Length == 1)
            //    {
            //        OptionsEntity oEntity = query[0];

            //        foreach (DataRow dr1 in categoryLevel.Tables[0].Rows)
            //        {
            //            var query1 = categoryOption.Where(m => m.OptionValue == dr1["CategoryLevel"].ToInt()).ToArray();
            //            if (query1.Length == 1)
            //            {
            //                OptionsEntity oEntity1 = query1[0];

            //                sbRes.Append("{name:'" + oEntity1.OptionText + "+" + oEntity.OptionText + "',value:'" + oEntity.OptionValue + "," + oEntity1.OptionValue + "'},");
            //            }
            //        }
            //    }
            //}
            sbRes.Remove(sbRes.ToString().Length - 1, 1);
            sbRes.Append("]");
            return sbRes.ToString();
        }
        #endregion

        #region GetStepBrandList
        private string GetStepBrandList(NameValueCollection rParams)
        {
            //这里只传递一个值
            if (string.IsNullOrEmpty(rParams["form"]))
            {
                return "{\"totalCount\":[],\"topics\":0}";
            }
            string[] type = rParams["form"].Split(',');
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;

            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingTaskStepObjectBLL(CurrentUserInfo).GetStepBrandList(type[0].ToInt(), type[1].ToInt(), Guid.Parse(rParams["id"]), pageIndex, pageSize, out rowCount).ToJSON(),
               rowCount
                );
        }
        #endregion

        #region EditStepObject_Brand
        private string EditStepObject_Brand(NameValueCollection rParams)
        {
            string[] type = rParams["type"].Split(',');
            Guid stepid = Guid.Parse(rParams["id"]);
            int allSelectorStatus = rParams["allSelectorStatus"].ToInt();
            string defaultList = rParams["defaultList"];//关联表有的数据
            string includeList = rParams["includeList"];//新加的数据
            string excludeList = rParams["excludeList"];//排除的数据

            new VisitingTaskStepObjectBLL(CurrentUserInfo).EditStepObject_Brand(type[0].ToInt(), type[1].ToInt(), stepid, allSelectorStatus, defaultList, includeList, excludeList);
            return "{success:true,msg:'编辑成功'}";
        }
        #endregion

        #region GetStepCategoryType
        private string GetStepCategoryType()
        {
            OptionsEntity optionEntity = new OptionsEntity();

            optionEntity.OptionName = "Brandlevel";
            optionEntity.ClientID = CurrentUserInfo.ClientID;
            OptionsEntity[] brandOption = new OptionsBLL(CurrentUserInfo).GetOptionByName(optionEntity);

            optionEntity.OptionName = "CategoryLevel";
            optionEntity.ClientID =CurrentUserInfo.ClientID;
            OptionsEntity[] categoryOption = new OptionsBLL(CurrentUserInfo).GetOptionByName(optionEntity);

            DataSet brandLevel = new BrandBLL(CurrentUserInfo).GetAllLevel();
            DataSet categoryLevel = new ItemCategoryService(CurrentUserInfo).GetAllLevel();

            StringBuilder sbRes = new StringBuilder();
            sbRes.Append("[");
            //品类
            foreach (DataRow dr in categoryLevel.Tables[0].Rows)
            {
                var query = categoryOption.Where(m => m.OptionValue == dr["CategoryLevel"].ToInt()).ToArray();
                if (query.Length == 1)
                {
                    OptionsEntity oEntity = query[0];
                    sbRes.Append("{name:'" + oEntity.OptionText + "',value:'" + oEntity.OptionValue + ",0'},");
                }
            }
            ////品类+品牌
            //foreach (DataRow dr in categoryLevel.Tables[0].Rows)
            //{
            //    var query = categoryOption.Where(m => m.OptionValue == dr["CategoryLevel"].ToInt()).ToArray();
            //    if (query.Length == 1)
            //    {
            //        OptionsEntity oEntity = query[0];

            //        foreach (DataRow dr1 in brandLevel.Tables[0].Rows)
            //        {
            //            var query1 = brandOption.Where(m => m.OptionValue == dr1["BrandLevel"].ToInt()).ToArray();
            //            if (query1.Length == 1)
            //            {
            //                OptionsEntity oEntity1 = query1[0];

            //                sbRes.Append("{name:'" + oEntity1.OptionText + "+" + oEntity.OptionText + "',value:'" + oEntity.OptionValue + "," + oEntity1.OptionValue + "'},");
            //            }
            //        }
            //    }

            //}
            sbRes.Remove(sbRes.ToString().Length - 1, 1);
            sbRes.Append("]");
            return sbRes.ToString();
        }
        #endregion

        #region GetStepCategoryList
        private string GetStepCategoryList(NameValueCollection rParams)
        {
            //这里只传递一个值
            if (string.IsNullOrEmpty(rParams["form"]))
            {
                return "{\"totalCount\":[],\"topics\":0}";
            }
            string[] type = rParams["form"].Split(',');
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;

            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingTaskStepObjectBLL(CurrentUserInfo).GetStepCategoryList(type[1].ToInt(), type[0].ToInt(), Guid.Parse(rParams["id"]), pageIndex, pageSize, out rowCount).ToJSON(),
               rowCount
                );
        }
        #endregion

        #region EditStepObject_Category
        private string EditStepObject_Category(NameValueCollection rParams)
        {
            string[] type = rParams["type"].Split(',');
            Guid stepid = Guid.Parse(rParams["id"]);
            int allSelectorStatus = rParams["allSelectorStatus"].ToInt();
            string defaultList = rParams["defaultList"];//关联表有的数据
            string includeList = rParams["includeList"];//新加的数据
            string excludeList = rParams["excludeList"];//排除的数据

            new VisitingTaskStepObjectBLL(CurrentUserInfo).EditStepObject_Category(type[1].ToInt(), type[0].ToInt(), stepid, allSelectorStatus, defaultList, includeList, excludeList);
            return "{success:true,msg:'编辑成功'}";
        }
        #endregion

        #region GetStepPositionList
        private string GetStepPositionList(NameValueCollection rParams)
        {
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingTaskStepObjectBLL(CurrentUserInfo).GetStepPositionList(Guid.Parse(rParams["id"]), pageIndex, pageSize, out rowCount).ToJSON(),
               rowCount
                );
        }
        #endregion

        #region EditStepObject_Position
        private string EditStepObject_Position(NameValueCollection rParams)
        {
            Guid stepid = Guid.Parse(rParams["id"]);
            int allSelectorStatus = rParams["allSelectorStatus"].ToInt();
            string defaultList = rParams["defaultList"];//关联表有的数据
            string includeList = rParams["includeList"];//新加的数据
            string excludeList = rParams["excludeList"];//排除的数据

            new VisitingTaskStepObjectBLL(CurrentUserInfo).EditStepObject_Position(stepid, allSelectorStatus, defaultList, includeList, excludeList);
            return "{success:true,msg:'编辑成功'}";
        }
        #endregion
    }
}