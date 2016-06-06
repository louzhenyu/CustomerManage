using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.VipGoldReport
{
    /// <summary>
    /// 集客内容 报表接口
    /// </summary>
    public class GetAggSetoffForToolListAH : BaseActionHandler<GetAggSetoffForToolListRP, GetAggSetoffForToolListRD>
    {
        protected override GetAggSetoffForToolListRD ProcessRequest(APIRequest<GetAggSetoffForToolListRP> pRequest)
        {

            #region 设置参数
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetAggSetoffForToolListRD();
            int Days = 7;
            int type = pRequest.Parameters.type;
            string customerid = loggingSessionInfo.ClientID;
            if (type == 2) { Days = 30; } //typeId(1=近7天,2=近30天)
            DateTime currentTime = DateTime.Now.AddDays(-6);
            Agg_SetoffForToolBLL SetoffToolService = new Agg_SetoffForToolBLL(loggingSessionInfo);
            List<string> setofftoollst = new List<string>() { "CTW", "Coupon", "SetoffPoster", "Goods" };
            #endregion

            #region 柱状图
            for (int i = 0; i < 5; i++) //获取前五天柱状图 柱状图 永远是5天。
            {
                currentTime = currentTime.AddDays(1); //当前时间
                SetOffToolDate _model = new SetOffToolDate();
                _model.datetime = currentTime.ToString("yyyy-MM-dd");
                // 3个类别所以循环三次分别统计该商户某一天的某一个类别下面的集客人数和分享人数
                foreach (var item in setofftoollst)
                {
                    var basemodel = GetCountByCustomerIdAndDateCode(customerid, _model.datetime, item, SetoffToolService);
                    SetOffToolContent content = new SetOffToolContent();
                    content.PeopleCount = basemodel.PeopleCount;
                    content.ShareCount = basemodel.ShareNumber;
                    content.SetoffRoleId = item;
                    _model.RoleContent.Add(content);
                }
                rd.lst.Add(_model);
            }
            #endregion

            //根据类型 和商户编号 来获取数据 还有开始时间和结束时间
            #region 统计上面方块基础数据
            //4 个角色 
            foreach (var item in setofftoollst)
            {
                #region 获取时间
                string begintime = DateTime.Now.AddDays(-Days).ToString("yyyy-MM-dd");
                string endtime = DateTime.Now.ToString("yyyy-MM-dd");
                string prevbegintime = DateTime.Now.AddDays(-(2 * Days)).ToString("yyyy-MM-dd");
                #endregion

                var modles = SetoffToolService.GetSetofToolListByCustomerId(customerid, null, item, begintime, endtime);
                SetofToolCount currentmodel = ConvertToEntity(modles);
                var oldmodels = SetoffToolService.GetSetofToolListByCustomerId(customerid, null, item, prevbegintime,begintime);
                SetofToolCount oldmodel = ConvertToEntity(oldmodels);
                int? DiffCount = currentmodel.PeopleCount - oldmodel.PeopleCount;
                rd.roletoolsources.Add(new SetOffTool(item, currentmodel.ShareNumber, currentmodel.PeopleCount,DiffCount));
            }
            #endregion

            #region 统计总数据
            //统计总数
            rd.ShareTotal = rd.roletoolsources.Sum(m => m.ShareCount); //分享总数
            rd.TotalSetOff = rd.roletoolsources.Sum(m => m.SetoffCount); //集客总数
            rd.AddTotalSetOff = rd.roletoolsources.Sum(m => m.DiffCount);//新增集客人数
            #endregion

            return rd;
        }
        #region 辅助方法
        /// <summary>
        /// 根据商户编号、时间、集客角色类型 获取 单条信息
        /// </summary>
        /// <param name="customerid">商户编号</param>
        /// <param name="createtime">统计时间 2016-5-5</param>
        /// <param name="tooltype">类型（CTW：创意仓库、Coupon：优惠券、SetoffPoster：集客报、Goods：商品）</param>
        /// <param name="SetoffRoleToolSourceService"></param>
        /// <returns></returns>
        public SetofToolCount GetCountByCustomerIdAndDateCode(string customerid, string createtime, string tooltype, Agg_SetoffForToolBLL SetoffForToolService)
        {
            SetofToolCount result = new SetofToolCount();
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = createtime });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerid });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "SetoffToolType", Value = tooltype });
            List<Agg_SetoffForToolEntity> lst = SetoffForToolService.Query(lstWhereCondition.ToArray(), null).ToList(); //按照条件查询
            result.PeopleCount = lst.Sum(m => m.SetoffCount);
            result.ShareNumber = lst.Sum(m => m.ShareCount);
            return result;
        }
        public class SetofToolCount
        {
            /// <summary>
            /// 集客人数
            /// </summary>
            public int? PeopleCount { get; set; }
            /// <summary>
            /// 分享人数
            /// </summary>
            public int? ShareNumber { get; set; }
            /// <summary>
            /// 和上一个时间节点的集客差
            /// </summary>
            public int? DiffCount { get; set; }
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <returns></returns>
        public SetofToolCount ConvertToEntity(DataSet ds)
        {
            SetofToolCount result = new SetofToolCount();


            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ShareCount = ds.Tables[0].Rows[0]["ShareCount"]+"" == "" ? "0" : ds.Tables[0].Rows[0]["ShareCount"].ToString();
                    string SetoffCount=ds.Tables[0].Rows[0]["SetoffCount"]+"" == "" ? "0" : ds.Tables[0].Rows[0]["SetoffCount"].ToString();
                    result.ShareNumber = Convert.ToInt32(ShareCount);
                    result.PeopleCount = Convert.ToInt32(SetoffCount);
                }
            }
            return result;
        }
        #endregion
    }
}