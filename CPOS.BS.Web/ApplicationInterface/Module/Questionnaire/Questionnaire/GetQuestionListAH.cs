using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Questionnaire.Request;
using JIT.CPOS.DTO.Module.Questionnaire.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    public class GetQuestionListAH : BaseActionHandler<GetQuestionnaireIDRP, GetQuestionListRD>
    {

        protected override GetQuestionListRD ProcessRequest(APIRequest<GetQuestionnaireIDRP> pRequest)
        {
            var rd = new GetQuestionListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var QuestionBLL = new T_QN_QuestionBLL(loggingSessionInfo);


            var OptionBLL = new T_QN_OptionBLL(loggingSessionInfo);


           
            #region 获取数据集

            
            T_QN_QuestionEntity[] list_QuestionEntity = QuestionBLL.getList(para.QuestionnaireID);

            rd.QuestionnaireList = new List<Question>();
            for (int i = 0; i < list_QuestionEntity.Length; i++)
            {
                var ques = list_QuestionEntity[i];
                Question _question = new Question();
                
                    _question.CustomerID = ques.CustomerID;
                    _question.DefaultValue = ques.DefaultValue;
                    _question.Isphone = ques.Isphone.Value;
                    _question.IsRequired = ques.IsRequired.Value;
                    _question.IsShowAddress = ques.IsShowAddress.Value;
                    _question.IsShowCity = ques.IsShowCity.Value;
                    _question.IsShowCounty = ques.IsShowCounty.Value;
                    _question.IsShowProvince = ques.IsShowProvince.Value;
                    _question.IsValidateEndDate = ques.IsValidateEndDate.Value;
                    _question.IsValidateMaxChar = ques.IsValidateMaxChar.Value;
                    _question.IsValidateMinChar = ques.IsValidateMinChar.Value;
                    _question.IsValidateStartDate = ques.IsValidateStartDate.Value;
                    _question.MaxChar = ques.MaxChar.Value;
                    _question.MaxScore = ques.MaxScore;
                    _question.MinChar = ques.MinChar.Value;
                    _question.MinScore = ques.MinScore;
                    _question.Name = ques.Name;
                    _question.NoRepeat = ques.NoRepeat.Value;
                    _question.Questionid = ques.Questionid.Value;
                    _question.QuestionidType = ques.QuestionidType.Value;
                    _question.ScoreStyle = ques.ScoreStyle.Value;
                    _question.StartDate = ques.StartDate;
                    _question.EndDate = ques.EndDate;
                    _question.Sort = ques.Sort.Value;
                    _question.Status = ques.Status.Value;




                    #region 获取问卷题目列表

                    #region 条件参数

                    List<IWhereCondition> complexCondition1 = new List<IWhereCondition>();
                    complexCondition1.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
                    if (_question.Questionid!=null)
                        complexCondition1.Add(new EqualsCondition() { FieldName = "QuestionID", Value = _question.Questionid });

                    #endregion

                    #region 排序参数

                    List<OrderBy> lstOrder1 = new List<OrderBy> { };
                    lstOrder1.Add(new OrderBy() { FieldName = "Sort", Direction = OrderByDirections.Asc });

                    #endregion

                    
                    T_QN_OptionEntity[] list_OptionEntity= OptionBLL.Query(complexCondition1.ToArray(), lstOrder1.ToArray());

                    _question.Optionlist = new List<Option>();

                    for (int j = 0; j < list_OptionEntity.Length; j++)
                    {
                        var opt = list_OptionEntity[j];
                        Option _Option = new Option();

                        _Option.CustomerID = loggingSessionInfo.ClientID;
                        _Option.IsRightValue = opt.IsRightValue.Value;
                        _Option.NoOptionScore = opt.NoOptionScore.Value;
                        _Option.OptionContent = opt.OptionContent;
                        _Option.OptionID = opt.OptionID;
                        _Option.OptionPicSrc = opt.OptionPicSrc;
                        _Option.QuestionID = opt.QuestionID;
                        _Option.QuestionidType = opt.QuestionidType.Value;
                        _Option.Sort = opt.Sort.Value;
                        _Option.Status = opt.Status.Value;
                        _Option.YesOptionScore = opt.YesOptionScore.Value;
                        _question.Optionlist.Add(_Option);
                    }




                    var QuestionPicBLL = new T_QN_QuestionPicBLL(loggingSessionInfo);
                    #endregion

                    #region 获取题目标题图片

                    #region 条件参数

                    List<IWhereCondition> complexCondition2 = new List<IWhereCondition>();
                    complexCondition2.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
                    if (_question.Questionid != null)
                        complexCondition2.Add(new EqualsCondition() { FieldName = "QuestionID", Value = _question.Questionid });

                    #endregion

                    T_QN_QuestionPicEntity[] list_QuestionPicEntity = QuestionPicBLL.Query(complexCondition2.ToArray(), null);

                    if (list_QuestionPicEntity.Length > 0)
                    {
                        _question.Src = list_QuestionPicEntity[0].Src;
                        _question.QuestionPicID = list_QuestionPicEntity[0].QuestionPicID.Value.ToString();

                    }
                    else
                    {
                        _question.Src = "";
                        _question.QuestionPicID ="";
                    }

                    #endregion

                    rd.QuestionnaireList.Add(_question);

                    

            }
            #endregion

            return rd;
        }


    }
}