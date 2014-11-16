using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using System.Collections.Specialized;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess;
using JIT.CPOS.Common;
using System.Data;

namespace JIT.CPOS.BS.Web.Module.LNewsType.Handler
{
    /// <summary>
    /// LNewsTypeHander 的摘要说明
    /// </summary>
    public class LNewsTypeHander : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override LoggingSessionInfo CurrentUserInfo
        {
            get { return new SessionManager().CurrentUserLoginInfo; }
        }

        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "GetLNewsTypeList":
                    content = GetLNewsTypeList(pContext.Request.Form);
                    break;
                case "GetPartentNewsType":
                    content = GetPartentNewsType();
                    break;
                case "LNewsTypeSave":
                    content = LNewsTypeSave(pContext.Request.Form);
                    break;

                case "DelLNewsTypeByID":
                    content = DelLNewsTypeByID(pContext.Request.Form);
                    break;
                case "GetNewsTypeDetail":
                    content = GetNewsTypeDetail(pContext.Request.Form);
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string GetLNewsTypeList(NameValueCollection rParams)
        {

            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            ReqLNewsrTypeEntity entity = rParams["form"].DeserializeJSONTo<ReqLNewsrTypeEntity>();
            //int pageSize = int.Parse(rParams["limit"].ToString());
            //int pageIndex = int.Parse(rParams["page"].ToString());

            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && !string.IsNullOrEmpty(entity.newsTypeName))
            {
                wheres.Add(new LikeCondition() { FieldName = "lt.NewsTypeName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.newsTypeName });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.parentTypeID))
            {
                entity.parentTypeID = rParams["typeid"].ToString();
                wheres.Add(new EqualsCondition() { FieldName = "lt.ParentTypeId", Value = entity.parentTypeID });
            }
            PagedQueryResult<LNewsTypeEntity> prlist = new LNewsTypeBLL(CurrentUserInfo).GetLNewsTypeList(wheres.ToArray(), startRowIndex, PageSize);
            return string.Format("{{\"totalCount\":{0},\"topics\":{1}}}", prlist.RowCount, prlist.Entities.ToJSON());
        }

        #region
        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public string GetPartentNewsType()
        {
            DataSet ds = new LNewsTypeBLL(CurrentUserInfo).GetPartentNewsType();
            if (ds == null || ds.Tables == null || ds.Tables.Count <= 0)
            {
                return "";
            }
            DataRow dr = ds.Tables[0].NewRow();
            dr["NewsTypeId"] = "";
            dr["NewsTypeName"] = "---请选择---";
            dr["ParentTypeId"] = "";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            return string.Format("{0}", ds.Tables[0].ToJSON());
        }
        #endregion

        #region LNewsTypeSave
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string LNewsTypeSave(NameValueCollection rParams)
        {
            try
            {
                string lNewsTypeID = rParams["newstypeid"];
                LNewsTypeBLL newstypeBll = new LNewsTypeBLL(CurrentUserInfo);
                LNewsTypeEntity entity = rParams["form"].DeserializeJSONTo<LNewsTypeEntity>();
                entity.IsVisble = 0;
                // parenttypeId
                if (rParams["parenttypeId"].ToString() != "-2" & (entity.ParentTypeId != null || entity.ParentTypeId != ""))
                {
                    entity.ParentTypeId = rParams["parenttypeId"].ToString();
                }
                bool bl = newstypeBll.IsSameName(lNewsTypeID, entity.NewsTypeName);
                if (bl)
                {
                    return "{success:false,msg:'提示!类别已存在'}";
                }
                if (string.IsNullOrEmpty(lNewsTypeID))
                {
                    entity.NewsTypeId = Guid.NewGuid().ToString();

                    if (!string.IsNullOrEmpty(entity.ParentTypeId))//ParentTypeId不为空
                    {
                        entity.TypeLevel = newstypeBll.GetByID(entity.ParentTypeId).TypeLevel + 1;
                    }
                    else
                    {
                        entity.TypeLevel = 1;//没有父节点就设为一级节点
                        entity.ParentTypeId = "";
                    }

                    entity.CustomerId = CurrentUserInfo.ClientID;
                    entity.CreateBy = CurrentUserInfo.ClientName;
                    newstypeBll.Create(entity);
                    return "{success:true,msg:'保存成功'}";
                }
                else
                {
                    LNewsTypeEntity ckentity = new LNewsTypeEntity();
                    ckentity.NewsTypeName = entity.NewsTypeName;
                    //bool bl = newstypeBll.IsSameName(lNewsTypeID, entity.NewsTypeName);
                    //if (bl)
                    //{
                    //    return "{success:false,msg:'操作失败!类别已存在'}";
                    //}

                    DataSet ds = new LNewsTypeBLL(CurrentUserInfo).GetPartentNewsType();
                    if (lNewsTypeID.Equals(entity.ParentTypeId))
                    {
                        return "{success:false,msg:'提示!上级不能选择自身'}";
                    }
                    if (Searchtype(ds.Tables[0], lNewsTypeID, entity.ParentTypeId))
                    {
                        return "{success:false,msg:'提示!不能层级循环'}";
                    }
                    if (!string.IsNullOrEmpty(entity.ParentTypeId))
                    {
                        entity.NewsTypeId = lNewsTypeID;
                        entity.TypeLevel = newstypeBll.GetByID(entity.ParentTypeId).TypeLevel + 1;
                    }
                    else
                    {
                        entity.NewsTypeId = lNewsTypeID;
                        entity.TypeLevel = 1;
                        entity.ParentTypeId = "";
                    }
                    newstypeBll.Update(entity, false);
                    return "{success:true,msg:'保存成功'}";
                }
            }
            catch (Exception)
            {
                return "{success:true,msg:'操作失败'}";
                throw;
            }


        }
        #endregion
        #region DelLNewsTypeByID
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string DelLNewsTypeByID(NameValueCollection rParams)
        {

            LNewsTypeBLL newstypeBll = new LNewsTypeBLL(CurrentUserInfo);
            string lNewsTypeID = rParams["newstTypeID"];
            int i = newstypeBll.DelLNewsTypeByID(lNewsTypeID);
            if (i > 0)
            {
                return "{success:true,msg:'操作成功'}";
            }
            else
            {
                return "{success:false,msg:'操作失败,有子类或被引用'}";
            }
        }
        #endregion

        #region GetNewsTypeDetail
        /// <summary>
        /// 获取明细信息
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string GetNewsTypeDetail(NameValueCollection rParams)
        {
            LNewsTypeBLL newstypeBll = new LNewsTypeBLL(CurrentUserInfo);
            string lNewsTypeID = rParams["newstTypeID"];
            LNewsTypeEntity entity = newstypeBll.GetByID(lNewsTypeID);
            if (entity.ChannelCode == null)
            {
                entity.ChannelCode = 1;
            }

            LNewsTypeEntity typeentity = new LNewsTypeBLL(CurrentUserInfo).QueryByEntity(new LNewsTypeEntity { NewsTypeId = entity.ParentTypeId }, null).FirstOrDefault();
            if (typeentity != null)
            {
                entity.ParentTypeName = typeentity.NewsTypeName;
            }

            return string.Format("{0}", entity.ToJSON());
        }
        #endregion


        #region
        public bool Searchtype(DataTable dt, string id, string pid)//查看有没他的父节点变成他的子节点的
        {
            //DataRow[] dr = dt.Select(string.Format("NewsTypeId='{0}'", pid));
            //if (dr[0]["ParentTypeId"] == id)
            //{
            //    return true;
            //}
            DataRow[] drs = dt.Select(string.Format("ParentTypeId='{0}'", id));
            if (string.IsNullOrEmpty(pid))
            {
                return false;
            }
            foreach (DataRow item in drs)//遍历子节点
            {
                string NewsTypeId = item["NewsTypeId"].ToString();
                if (NewsTypeId == pid)   //子节点中含有父节点
                {
                    return true;
                }
                bool bl = SonSearchtype(dt, id, NewsTypeId);//查看有没他的子节点变成他的父节点的
                if (bl)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SonSearchtype(DataTable dt, string id, string pid)
        {
            DataRow[] drs = dt.Select(string.Format("ParentTypeId='{0}'", id));
            if (drs != null && drs.Count() > 0)
            {
                if (string.IsNullOrEmpty(pid))
                {
                    return false;
                }
                foreach (DataRow item in drs)
                {
                    string NewsTypeId = item["NewsTypeId"].ToString();
                    if (NewsTypeId == pid)
                    {
                        return true;
                    }
                    SonSearchtype(dt, id, NewsTypeId);
                }
            }
            return false;
        }
        #endregion
        #region 接收参数
        public class ReqLNewsrTypeEntity
        {
            public string newsTypeID { set; get; }
            public string newsTypeName { set; get; }
            public string parentTypeID { set; get; }
            public string typeLevel { set; get; }
        }
        #endregion
    }
}