using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.Extension.Question.Request;
using JIT.CPOS.DTO.Module.Extension.Question.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.Question
{
    public class GetQuestionListAH : BaseActionHandler<GetQuestionListRP, GetQuestionListRD>
    {

        protected override GetQuestionListRD ProcessRequest(DTO.Base.APIRequest<GetQuestionListRP> pRequest)
        {
            var rd = new GetQuestionListRD();
            var para = pRequest.Parameters;
            var questionBLL = new X_QuestionBLL(CurrentUserInfo);

            DateTime dtNow = DateTime.Now;//当前时间
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });

            //complexCondition.Add(new MoreThanCondition() { FieldName = "BeginTime", Value = dtNow });
            //complexCondition.Add(new LessThanCondition() { FieldName = "EndTime", Value = dtNow });

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Asc });

            var tempList = questionBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex + 1);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.QuestionList = tempList.Entities.Select(t => new QuestionInfo()
            {
                QuestionID = t.QuestionID,
                Name = t.Name,
                NameUrl = t.NameUrl,
                Option1 = t.Option1,
                Option2 = t.Option2,
                Option3 = t.Option3,
                Option4 = t.Option4,
                Option1ImageUrl = t.Option1ImageUrl,
                Option2ImageUrl = t.Option2ImageUrl,
                Option3ImageUrl = t.Option3ImageUrl,
                Option4ImageUrl = t.Option4ImageUrl,
                Answer = t.Answer,
                IsMultiple = t.IsMultiple,
            }).ToArray();
            return rd;
        }
    }
}