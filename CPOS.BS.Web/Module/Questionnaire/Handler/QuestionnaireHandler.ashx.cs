using System.Collections.Generic;
using System.Web;
using System.Linq;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;
using System;
using System.Text;
using System.Data;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Web.Module.Questionnaire.Handler
{
    /// <summary>
    /// QuestionnaireHandler
    /// </summary>
    public class QuestionnaireHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "Questionnaires_query":      //问卷查询
                    content = GetQuestionnairesData();
                    break;
                case "Questionnaires_delete":     //问卷删除
                    content = QuestionnairesDeleteData();
                    break;
                case "Question_delete":     //试题删除
                    content = QuestionDeleteData();
                    break;
                case "Option_delete":     //选项删除
                    content = OptionDeleteData();
                    break;
                case "get_Questionnaires_by_id":  //根据ID获取问卷信息
                    content = GetQuestionnairesById();
                    break;
                case "get_Question_by_id":  //根据ID获取问题信息
                    content = GetQuestionById();
                    break;
                case "get_Option_by_id":  //根据ID获取问题选项信息
                    content = GetOptionById();
                    break;
                case "Questionnaires_save":       //保存问卷信息
                    content = SaveQuestionnaires();
                    break;
                case "Question_save":       //保存试题信息
                    content = SaveQuestion();
                    break;
                case "Option_save":       //保存试题信息
                    content = SaveOption();
                    break;
                case "QuestionList_query":      //问题查询
                    content = GetQuestionListData();
                    break;
                case "OptionList_query":      //问题选项查询
                    content = GetQuesOptionListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region QuestionnairesDeleteData 问题删除

        /// <summary>
        /// 问卷删除
        /// </summary>
        public string QuestionnairesDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "问题ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new QuestionnaireBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region QuestionDeleteData 

        /// <summary>
        /// 试题删除
        /// </summary>
        public string QuestionDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "试题ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new QuesQuestionsBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region OptionDeleteData

        /// <summary>
        /// 试题选项删除
        /// </summary>
        public string OptionDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "选项ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new QuesOptionBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetQuestionnairesData 查询问卷列表

        /// <summary>
        /// 查询问卷列表
        /// </summary>
        public string GetQuestionnairesData()
        {
            var form = Request("form").DeserializeJSONTo<QuestionnairesQueryEntity>();
            var questionnaireBLL = new QuestionnaireBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string QuestionnaireName = FormatParamValue(form.QuestionnaireName);
            //string Title = FormatParamValue(form.Title);
            //string DateBegin = FormatParamValue(form.DateBegin);
            //string DateEnd = FormatParamValue(form.DateEnd);
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            QuestionnaireEntity queryEntity = new QuestionnaireEntity();
            queryEntity.QuestionnaireName = QuestionnaireName;
            //if (this.CurrentUserInfo.CurrentUser.User_Name.ToLower() == "admin")
            //{
            //    queryEntity.CreateBy = this.CurrentUserInfo.CurrentUser.User_Name.ToLower();
            //}
            //else
            //{
            //    queryEntity.CreateBy = this.CurrentUserInfo.UserID;
            //}

            var data = questionnaireBLL.GetWebQuestionnaires(queryEntity, pageIndex, PageSize);
            var dataTotalCount = questionnaireBLL.GetWebQuestionnairesCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region GetQuestionnairesById 根据ID获取问题信息

        /// <summary>
        /// 根据ID获取问题信息
        /// </summary>
        public string GetQuestionnairesById()
        {
            var questionnairesService = new QuestionnaireBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("QuestionnaireID")) != null && FormatParamValue(Request("QuestionnaireID")) != string.Empty)
            {
                key = FormatParamValue(Request("QuestionnaireID")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "QuestionnaireID", Value = key });
            }

            var data = questionnairesService.Query(condition.ToArray(), null).ToList().FirstOrDefault();
            //if (data != null)
            //{
            //    data.StrPublishTime = data.PublishTime.Value.ToString("yyyy-MM-dd");
            //}

            //data.StartDateText = data.StartTime == null ? string.Empty :
            //    Convert.ToDateTime(data.StartTime).ToString("yyyy-MM-dd");
            //data.EndDateText = data.StartTime == null ? string.Empty :
            //    Convert.ToDateTime(data.StartTime).ToString("yyyy-MM-dd");

            //data.Description =  HttpUtility.HtmlDecode(data.Description);

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region GetQuestionById 根据ID获取问题信息

        /// <summary>
        /// 根据ID获取问题信息
        /// </summary>
        public string GetQuestionById()
        {
            var quesQuestionsBLL = new QuesQuestionsBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("QuestionID")) != null && FormatParamValue(Request("QuestionID")) != string.Empty)
            {
                key = FormatParamValue(Request("QuestionID")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "QuestionID", Value = key });
            }

            var data = quesQuestionsBLL.Query(condition.ToArray(), null).ToList().FirstOrDefault();

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region GetOptionById

        /// <summary>
        /// 根据ID获取问题信息
        /// </summary>
        public string GetOptionById()
        {
            var quesQuestionsBLL = new QuesOptionBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("OptionID")) != null && FormatParamValue(Request("OptionID")) != string.Empty)
            {
                key = FormatParamValue(Request("OptionID")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "OptionID", Value = key });
            }

            var data = quesQuestionsBLL.Query(condition.ToArray(), null).ToList().FirstOrDefault();

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveQuestionnaires 保存问题信息

        /// <summary>
        /// 保存问题信息
        /// </summary>
        public string SaveQuestionnaires()
        {
            var questionnairesService = new QuestionnaireBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string QuestionnaireID = string.Empty;
            var questionnaires = Request("questionnaires");

            if (FormatParamValue(questionnaires) != null && FormatParamValue(questionnaires) != string.Empty)
            {
                key = FormatParamValue(questionnaires).ToString().Trim();
            }
            if (FormatParamValue(Request("QuestionnaireID")) != null && FormatParamValue(Request("QuestionnaireID")) != string.Empty)
            {
                QuestionnaireID = FormatParamValue(Request("QuestionnaireID")).ToString().Trim();
            }

            var questionnairesEntity = key.DeserializeJSONTo<QuestionnaireEntity>();

            //if (questionnairesEntity.QuestionnaireType == null || questionnairesEntity.QuestionnaireType.ToString().Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "问题类型不能为空";
            //    return responseData.ToJSON();
            //}
            //if (questionnairesEntity.Title == null || questionnairesEntity.Title.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "问题标题不能为空";
            //    return responseData.ToJSON();
            //}
            //if (questionnairesEntity.StartTimeText == null || questionnairesEntity.StartTimeText.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "起始时间不能为空";
            //    return responseData.ToJSON();
            //}
            //if (questionnairesEntity.EndTimeText == null || questionnairesEntity.EndTimeText.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "结束时间不能为空";
            //    return responseData.ToJSON();
            //}

            //try
            //{
            //    questionnairesEntity.StartTime = Convert.ToDateTime(questionnairesEntity.StartTimeText);
            //}
            //catch
            //{
            //    responseData.success = false;
            //    responseData.msg = "起始时间格式错误";
            //    return responseData.ToJSON();
            //}

            //try
            //{
            //    questionnairesEntity.EndTime = Convert.ToDateTime(questionnairesEntity.EndTimeText);
            //}
            //catch
            //{
            //    responseData.success = false;
            //    responseData.msg = "结束时间格式错误";
            //    return responseData.ToJSON();
            //}

            //questionnairesEntity.Description = HttpUtility.HtmlEncode(questionnairesEntity.Description);

            if (QuestionnaireID.Trim().Length == 0)
            {
                questionnairesEntity.QuestionnaireID = Utils.NewGuid();
                questionnairesEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                //questionnairesEntity.Status = 0;
                //questionnairesEntity.ApplyCount = 0;
                //questionnairesEntity.CheckInCount = 0;
                //questionnairesEntity.PostCount = 0;
                questionnairesService.Create(questionnairesEntity);
            }
            else
            {
                questionnairesEntity.QuestionnaireID = QuestionnaireID;
                questionnairesService.Update(questionnairesEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveQuestion

        /// <summary>
        /// 保存试题信息
        /// </summary>
        public string SaveQuestion()
        {
            var quesQuestionsBLL = new QuesQuestionsBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string QuestionID = string.Empty;
            var question = Request("Question");

            if (FormatParamValue(question) != null && FormatParamValue(question) != string.Empty)
            {
                key = FormatParamValue(question).ToString().Trim();
            }
            if (FormatParamValue(Request("QuestionID")) != null && FormatParamValue(Request("QuestionID")) != string.Empty)
            {
                QuestionID = FormatParamValue(Request("QuestionID")).ToString().Trim();
            }

            var questionEntity = key.DeserializeJSONTo<QuesQuestionsEntity>();


            if (QuestionID.Trim().Length == 0)
            {
                questionEntity.QuestionID = Utils.NewGuid();
                //questionnairesEntity.Status = 0;
                //questionnairesEntity.ApplyCount = 0;
                //questionnairesEntity.CheckInCount = 0;
                //questionnairesEntity.PostCount = 0;
                quesQuestionsBLL.Create(questionEntity);
            }
            else
            {
                questionEntity.QuestionID = QuestionID;
                quesQuestionsBLL.Update(questionEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveOption

        /// <summary>
        /// 保存试题信息
        /// </summary>
        public string SaveOption()
        {
            var quesOptionsBLL = new QuesOptionBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string OptionID = string.Empty;
            var option = Request("Option");

            if (FormatParamValue(option) != null && FormatParamValue(option) != string.Empty)
            {
                key = FormatParamValue(option).ToString().Trim();
            }
            if (FormatParamValue(Request("OptionID")) != null && FormatParamValue(Request("OptionID")) != string.Empty)
            {
                OptionID = FormatParamValue(Request("OptionID")).ToString().Trim();
            }

            var optionEntity = key.DeserializeJSONTo<QuesOptionEntity>();


            if (OptionID.Trim().Length == 0)
            {
                optionEntity.OptionsID = Utils.NewGuid();
                //questionnairesEntity.Status = 0;
                //questionnairesEntity.ApplyCount = 0;
                //questionnairesEntity.CheckInCount = 0;
                //questionnairesEntity.PostCount = 0;
                quesOptionsBLL.Create(optionEntity);
            }
            else
            {
                optionEntity.OptionsID = OptionID;
                quesOptionsBLL.Update(optionEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region 问题查询
        /// <summary>
        /// 问题查询
        /// </summary>
        public string GetQuestionListData()
        {
            var quesQuestionsBLL = new QuesQuestionsBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string QuestionnaireId = FormatParamValue(Request("QuestionnaireId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var queryEntity = new QuesQuestionsEntity();
            queryEntity.QuestionnaireID = QuestionnaireId;

            var data = quesQuestionsBLL.GetWebQuesQuestions(queryEntity, pageIndex, PageSize);
            int dataTotalCount = data.Count;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region 查询问题选项列表
        /// <summary>
        /// 查询问题选项列表
        /// </summary>
        public string GetQuesOptionListData()
        {
            var quesOptionBLL = new QuesOptionBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string QuestionID = FormatParamValue(Request("QuestionID"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var queryEntity = new QuesOptionEntity();
            queryEntity.QuestionID = QuestionID;

            var data = quesOptionBLL.GetWebQuesOptions(queryEntity, pageIndex, PageSize);
            int dataTotalCount = data.Count;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }
        #endregion
    }

    #region QueryEntity

    public class QuestionnairesQueryEntity
    {
        public string QuestionnaireID;
        public string QuestionnaireName;
        public string Title;
        public string CityID;
        public string DateBegin;
        public string DateEnd;
    }

    #endregion
}