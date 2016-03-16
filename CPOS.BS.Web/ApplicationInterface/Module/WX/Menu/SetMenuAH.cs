using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Request;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity.WX;
using JIT.CPOS.BS.BLL.WX.Enum;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Menu
{
    public class SetMenuAH : BaseActionHandler<SetMenuRP, SetMenuRD>
    {
        protected override SetMenuRD ProcessRequest(DTO.Base.APIRequest<SetMenuRP> pRequest)
        {
            var rd = new SetMenuRD();

            string menuId = pRequest.Parameters.MenuId;
            string name = pRequest.Parameters.Name;
            string parentId = pRequest.Parameters.ParentId;
            string displayIndex = pRequest.Parameters.DisplayColumn;
            int status = pRequest.Parameters.Status;
            string applicationId = pRequest.Parameters.ApplicationId;//某个公众号在数据库里的标识
            string text = pRequest.Parameters.Text;
            string menuUrl = pRequest.Parameters.MenuUrl;
            string imageUrl = pRequest.Parameters.ImageUrl;
            string messageType = pRequest.Parameters.MessageType;
            int unionTypeId = pRequest.Parameters.UnionTypeId;
            Guid? pageId = pRequest.Parameters.PageId;
            string pageParamJson = pRequest.Parameters.PageParamJson;
            var materialTextIds = pRequest.Parameters.MaterialTextIds;
            string pageUrlJson = pRequest.Parameters.PageUrlJson;

            int level = pRequest.Parameters.Level;
            string type = "";

            var bll = new WMenuBLL(this.CurrentUserInfo);

            #region  一级节点 节点名称长度不能超过3个汉字，二级节点 不能超过7个汉字

            #endregion

            #region type = view时，菜单链接MenuUrl不能为空

            #endregion

            #region 图文消息只能增加10条

            if (unionTypeId == 1 || unionTypeId == 3)
            {
                type = "view";
                if (unionTypeId == 1 && (string.IsNullOrEmpty(menuUrl) || menuUrl == ""))
                {
                    throw new APIException("菜单链接不能为空") { ErrorCode = 123 };
                }

                if (unionTypeId == 1 && (menuUrl.Length > 500))
                {
                    throw new APIException("菜单链接地址超长，请重新填写") { ErrorCode = 140 };
                }
            }
            else if (unionTypeId == 2)
            {
                type = "click";
                if (messageType == "3")
                {
                    if (materialTextIds == null || materialTextIds.Any() == false)
                    {
                        throw new APIException("图文消息不能为空") { ErrorCode = 124 };
                    }
                    if (materialTextIds.Any() == true && materialTextIds.Length > 10)
                    {
                        throw new APIException("图文消息最大不能超过10条数据") { ErrorCode = 125 };
                    }
                }

                if (messageType == "1")
                {
                    if (text == "" || string.IsNullOrEmpty(text))
                    {
                        throw new APIException("文本不能为空") { ErrorCode = 126 };
                    }
                    if (Encoding.Default.GetBytes(text).Length > 2048)
                    {
                        throw new APIException("文本超过了最大限制（2M）") { ErrorCode = 127 };
                    }
                }
                if (messageType == "2")
                {
                    if (imageUrl == "" || string.IsNullOrEmpty(imageUrl))
                    {
                        throw new APIException("图片不能为空") { ErrorCode = 128 };
                    }
                }
            }

            #endregion


            #region 确保每个一级节点下面不能超过五个状态已启用的二级菜单

            if (level == 2)
            {
                int count = bll.GetLevel2CountByMenuId(parentId, applicationId, this.CurrentUserInfo.ClientID);

                if (count >= 5 && status == 1 && (menuId == "" || string.IsNullOrEmpty(menuId)))
                {
                    throw new APIException("二级节点的数量最大为5条启用的菜单") { ErrorCode = 120 };
                }
                if (!string.IsNullOrEmpty(menuId) && count >= 5)
                {
                    var ds = bll.GetMenuDetail(menuId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var oldStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]); 
                        if (oldStatus == 0 && status == 1)   //如果已经目前已经有五个已经启用了，这个之前是没启用的，现在启用了
                        {
                            throw new APIException("二级节点的数量最大为5条启用的菜单") { ErrorCode = 130 };
                        }
                    }
                }
                //取没被删除的，id
                int countDisplayCoulumn = bll.GetLevel2CountByDisplayColumn(parentId, menuId, int.Parse(displayIndex), applicationId, this.CurrentUserInfo.ClientID);
                if (countDisplayCoulumn > 0)
                {
                    throw new APIException("同一个一级菜单下的二级菜单，序号不能相同") { ErrorCode = 130 };
                }


                if (Encoding.Default.GetBytes(name).Length > 16)
                {
                    throw new APIException("二级节点的名称最多不能超过8个汉字") { ErrorCode = 121 };
                }

                //判断一级菜单的类型 如果一级菜单的类型为View改为Click

                var menuEntity = bll.QueryByEntity(new WMenuEntity()
                {
                    ID = parentId
                }, null).FirstOrDefault();

                if (menuEntity != null)
                {
                    if (menuEntity.Type == "view")
                    {
                        menuEntity.Type = "click";
                        bll.Update(menuEntity);
                    }
                }
            }
            else if (level == 1)
            {
                //判断是否有二级菜单

                var b = bll.CheckExistLevel2Menu(menuId);

                //有，type=Click

                if (b)
                {
                    type = "click";
                }

                //没有根据当前选择的点击关联到来判断Click或View
                else
                {
                    if (unionTypeId == 1 || unionTypeId == 3)
                    {
                        type = "view";
                    }
                    else if (unionTypeId == 2)
                    {
                        type = "click";
                    }
                }
                if (Encoding.Default.GetBytes(name).Length > 12)
                {
                    throw new APIException("一级节点的名称最多不能超过6个汉字") { ErrorCode = 122 };
                }
            }
            else
            {

                throw new APIException("菜单级别参数错误【Level】") { ErrorCode = 131 };
            }

            #endregion



            var wappBll = new WApplicationInterfaceBLL(CurrentUserInfo);
            string weixinId = "";
            var wappEntity = wappBll.QueryByEntity(new WApplicationInterfaceEntity()
            {
                CustomerId = CurrentUserInfo.ClientID,
                ApplicationId = applicationId
            }, null);
            if (wappEntity.Length > 0)
            {
                weixinId = wappEntity[0].WeiXinID;
            }

            if (unionTypeId == 3)
            {
                var sysPageBll = new SysPageBLL(CurrentUserInfo);

                var pages = sysPageBll.GetPageByID(pageId);

                if (pages.Length > 0)
                {
                    var page = pages.FirstOrDefault();

                    if (page != null)
                    {
                        //获取生成的URL
                        menuUrl = page.GetUrl(pageParamJson, CurrentUserInfo.ClientID, applicationId, weixinId);
                    }
                    else
                    {
                        throw new APIException("缺少页面参数配置") { ErrorCode = 141 };
                    }
                }
            }

            //上传图文素材  
            //获取access_token/
            /**
            var commonService = new CommonBLL();
            var appService = new WApplicationInterfaceBLL(CurrentUserInfo);
            var appEntity = appService.GetByID(applicationId);//
            //var accessToken = commonService.GetAccessTokenByCache(appEntity.AppID, appEntity.AppSecret, CurrentUserInfo);
            //UploadMediaEntity media = commonService.UploadMediaFileFOREVER(accessToken.access_token, imageUrl, MediaType.Image);
            string filePath = commonService.DownloadFile(imageUrl);        
            ***/
            var wMaterialImageBll = new WMaterialImageBLL(CurrentUserInfo);
            var wMaterialImageEntity = new WMaterialImageEntity();
            var imageId = Utils.NewGuid();

            wMaterialImageEntity.ApplicationId = applicationId;
            wMaterialImageEntity.ImageUrl = imageUrl;
            wMaterialImageEntity.ImageId = imageId;
           // wMaterialImageEntity.ImageName = filePath;//存物理路径，用于在微信端发送图片
            wMaterialImageBll.Create(wMaterialImageEntity);

     

            var entity = new WMenuEntity();
            if (string.IsNullOrEmpty(menuId) || menuId == "")
            {
                var newMenuId = Utils.NewGuid().ToString();
                entity.ID = newMenuId;
                entity.Name = name;
                entity.ParentId = parentId;
                entity.DisplayColumn = displayIndex;
                entity.ImageId = imageId; //和图片做了关联
                entity.Status = status;
                entity.Level = level.ToString();
                entity.MenuURL = menuUrl;
                entity.Key = Utils.NewGuid().Substring(0, 7);
                entity.WeiXinID = weixinId;
                entity.Type = type;
                entity.PageId = pageId;
                entity.BeLinkedType = unionTypeId;
                entity.MaterialTypeId = messageType;
                entity.Text = text;

                bll.Create(entity);

                rd.MenuId = newMenuId;
            }
            else
            {
                if (unionTypeId == 2)
                {
                    menuUrl = null;
                }

                entity.ID = menuId;
                entity.Name = name;
                entity.ParentId = parentId;
                entity.DisplayColumn = displayIndex;
                entity.ImageId = imageId;
                entity.Status = status;
                entity.MaterialTypeId = messageType;
                entity.MenuURL = menuUrl;
                entity.Key = Utils.NewGuid().Substring(0, 7);
                entity.Level = level.ToString();
                entity.WeiXinID = weixinId;
                entity.Type = type;
                entity.PageId = pageId;
                entity.Text = text;
                entity.BeLinkedType = unionTypeId;
                bll.Update(entity);

                rd.MenuId = menuId;
            }

            bll.UpdateMenuData(rd.MenuId, status, pageId, pageParamJson, pageUrlJson, unionTypeId);

            #region unionTypeId 为回复消息的时候，素材类型必须有值，MenuUrl必须为空，反之 清空表中的所有素材类型，MuneUrl必须有值

            var mappingBll = new WMenuMTextMappingBLL(CurrentUserInfo);

            var mappingEntity = mappingBll.QueryByEntity(new WMenuMTextMappingEntity()
            {
                MenuId = rd.MenuId
            }, null);

            if (mappingEntity.Length > 0)
            {
                mappingBll.Delete(mappingEntity);
            }

            if (unionTypeId == 2)
            {
                if (messageType == "3")
                {
                    var textMappingEntity = new WMenuMTextMappingEntity();
                    foreach (var materialTextIdInfo in materialTextIds)
                    {
                        textMappingEntity.MappingId = Guid.NewGuid();
                        textMappingEntity.MenuId = rd.MenuId;
                        textMappingEntity.DisplayIndex = materialTextIdInfo.DisplayIndex;
                        textMappingEntity.TextId = materialTextIdInfo.TestId;
                        textMappingEntity.CustomerId = CurrentUserInfo.ClientID;
                        mappingBll.Create(textMappingEntity);
                    }
                }
            }

            #endregion

            return rd;
        }
    }
}