using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JIT.CPOS.BS.Entity
{

    #region 基本公用类
    /// <summary>
    /// 公用基础类
    /// </summary>
    public class ReqCommonData
    {
        public string locale;  //语言环境
        public string userId;  //用户ID
        public string openId;  //微信openId
        public string customerId; //客户ID
        public string roleid { get; set; } //角色ID
    }

    /// <summary>
    /// 公用详细类
    /// </summary>
    public class SpecialData
    {
        public string Phone { get; set; } //电话
        public string Code { get; set; } //验证码
    }
    #endregion

    #region 前台接受数据类
    //父类
    public class ReqData<T>
    {
        public ReqCommonData common { get; set; }
        public T special { get; set; }
    }
    #endregion


    #region 返回数据类
    /// <summary>
    /// 返回数据实体类
    /// </summary>
    public class RequestEntity<T>
    {
        public string code { get; set; }
        public string description { get; set; }
        public T content { get; set; }
    }

    #endregion

    #region 4.1-4.7功能类
    /// <summary>
    /// 4.1	根据微信OpenID获取用户信息 实体类
    /// </summary>
    public class getUserIDByOpenIDEntity : ReqData<SpecialData> { }

    /// <summary>
    /// 4.2	根据手机获取验证码 实体类
    /// </summary>
    public class getCodeByPhoneEntity : ReqData<SpecialData> { }

    /// <summary>
    /// 4.3	根据手机和验证码判断输入是否正常,获取用户信息 实体类
    /// </summary>
    public class getUserByPhoneAndCodeEntity : ReqData<SpecialData> { }

    /// <summary>
    /// 4.4	根据用户获取字段配置信息
    /// </summary>
    public class getUserDefinedByUserIDEntity : SpecialData {
        public int? TypeID { get; set; }
        public string EventID { get; set; }
    }

    /// <summary>
    /// 4.5	提交用户信息
    /// </summary>
    public class submitUserInfoEntity : ReqData<submitSpecialData> { }

    public class submitSpecialData
    {
        public List<ControlUpdateEntity> Control { get; set; } //上传的内容
    }

    /// <summary>
    /// 需要修改的控件信息 实体类
    /// </summary>
    public class ControlUpdateEntity
    {
        public string ControlID { get; set; }  //控件ID
        public string ColumnDesc { get; set; } //列的描述
        public string ColumnName { get; set; } //字段名称
        public string Value { get; set; }  //数据的值
    }

    /// <summary>
    /// 页的实体类
    /// </summary>
    public class PageEntity
    {
        public string PageName { get; set; }  //页的标题
        public int PageNum { get; set; }  //页码 1,2,3
        public List<BlockEntity> Block { get; set; } //页所包含的块信息
    }

    /// <summary>
    /// 块的实体类
    /// </summary>
    public class BlockEntity
    {
        public string BlockName { get; set; }  //块的标题
        public int BlockSort { get; set; }  //块的排序
        public string Remark { get; set; }//备注
        public List<ControlEntity> Control { get; set; } //块所包含的控件信息
    }

    /// <summary>
    /// 控件的实体类
    /// </summary>
    public class ControlEntity
    {
        public string ControlID { get; set; }  //控件ID
        public bool NeedSaveCookie { get; set; } //是否需要保存Cookie
        public string CookieName { get; set; }  //保存Cookie的名称
        public string ColumnDesc { get; set; } //列的名称
        public string ColumnDescEN { get; set; } //列的英文名称
        public string ColumnName { get; set; } //字段的名称
        public string LinkageItem { get; set; } //联动项
        public string ExampleValue { get; set; }  //例如项
        public string ControlType { get; set; } //控件类型 类型：1:文本；4：日期；5：时间类型；6：下拉框；7 : 单选框 8: 多选框；9:超文本
        public string AuthType { get; set; } //验证的类型1：文本; 2: 整数；3：小数；4:日期；5：时间； 6：邮件 ；7：电话；8：手机；9验证Url网址；10：验证身份证
        public int MinLength { get; set; } //最小长度
        public int MaxLength { get; set; } //最大长度
        public int MinSelected { get; set; } //多选最少选择
        public int MaxSelected { get; set; } //多选最多选择
        public bool IsMustDo { get; set; }  //是否必填
        public string Value { get; set; }   //值
        public List<ConOptionsEntity> Options { get; set; } //单选，多选，下拉的选项
    }

    /// <summary>
    /// 下拉的选项Option 实体类
    /// </summary>
    public class ConOptionsEntity
    {
        public string OptionID { get; set; } //option的值
        public string OptionText { get; set; } //option显示的值
        public bool IsSelected { get; set; } //是否选中
    }
    #endregion


    #region 4.1-4.7返回数据功能
    /// <summary>
    /// 根据微信OpenID获取会员标识
    /// </summary>
    public class reqGetUserIDByOpenIDEntity
    {
        public string userId { get; set; } //会员标识
    }

    public class reqPageListEntity
    {
        public List<PageEntity> pageList; //页集合
    }

    /// <summary>
    /// 查询咨询列表
    /// </summary>
    public class getNewsListEntity
    {
        public int? page { get; set; } //页码
        public int? pageSize { get; set; }  //页面数量
        public string NewsType { get; set; }  //新闻类型
    }

    /// <summary>
    /// 查询详细信息
    /// </summary>
    public class getNewsDetailByNewsIDEntity
    {
        public string NewsID { get; set; } //咨询标识
    }

    /// <summary>
    /// 咨询详细
    /// </summary>
    public class NewsDetailEntity
    {
        public string NewsId { get; set; } //咨询标识
        public string NewsTitle { get; set; } //咨询标题
        public string NewsSubTitle { get; set; } //咨询子标题
        public string Content { get; set; } //咨询内容
        public string Intro { get; set; }  //咨询简介
        public string PublishTime { set; get; } //时间
        public string ContentUrl { get; set; } //咨询连接
        public string ImageUrl { get; set; } //图片地址
        public string ThumbnailImageUrl { get; set; } //图片缩略图
        public string Author { get; set; }  //作者

        public int? BrowseCount { get; set; }//浏览数
        public int? PraiseCount { get; set; }//赞数
        public int CollCount { get; set; }//收藏数
        /// <summary>
        /// 收藏数
        /// </summary>
        public int? BookmarkCount { get; set; }

        public int? ShareCount { set; get; } //分享数
        public int NewsCountID { get; set; }
        public string EventStatsID { set; get; }//主表ID
        public string isPraise { set; get; }
       
    }

    /// <summary>
    /// 返回咨询详细
    /// </summary>
    public class reqNewsEntity
    {
        public NewsDetailEntity News { get; set; } //咨询详细
    }
    #endregion

    #region 返回分页功能类
    /// <summary>
    /// 公用的分页查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchListEntity<T>
    {
        public bool IsNext { get; set; } //是否有下一页 true=是 false=否
        public T ItemList { get; set; } //商品集合
    }
    #endregion

    #region 4.8-4.12 功能类
    /// <summary>
    /// 登陆注册类
    /// </summary>
    public class getUserByLoginEntity
    {
        public string LoginName { get; set; } //登陆名
        public string PassWord { get; set; } //登陆密码
        public string RoleID { get; set; } //角色ID
        public bool IsLogin { get; set; } //是否是登录
    }

    /// <summary>
    /// 查询活动列表
    /// </summary>
    public class getActivityListEntity
    {
        public int? page { get; set; } //页码
        public int? pageSize { get; set; } //页面数量
        public string EventTypeID { get; set; }
        public string type { get; set; }//活动类型
    }

    /// <summary>
    /// 返回活动列表
    /// </summary>
    public class reqActivityListEntity
    {
        public string ActivityID { get; set; } //活动标识
        public string ActivityTitle { get; set; } //咨询标题
        public string ActivityCity { get; set; } //活动城市
        public string Address { get; set; } //活动城市
        public string BeginTime { get; set; } //开始时间
        public string EndTime { get; set; } //结束时间
        public decimal BeginDay { get; set; } //还有几天开始,0当天开始,1还有一天
        public decimal EndDay { get; set; } //还有几天结束,0当天结束,1还有一天结束,-1已结束
        public int UserCount { get; set; } //用户个数
        public int IsSignUpList { get; set; }
        public List<reqActivityUserItem> UserItem { get; set; } //用户集合
    }
        
    /// <summary>
    /// 4.9 用户集合
    /// </summary>
    public class reqActivityUserItem
    {
        public string UserHeadImg { get; set; } //用户头像
        public string UserID { get; set; } //用户ID
    }
    #endregion

    #region 头部新闻列表
    /// <summary>
    /// 咨询头部列表
    /// </summary>
    public class getTopListEntity
    {
        public int? pageSize { get; set; } //页面数量
        public int? Type { get; set; } //页面类别，根据传入的值，跳转不同的页面    
        public string NewsType { get; set; } //咨询类别1：咨询，2：活动
    }
    /// <summary>
    /// 返回咨询头部列表
    /// </summary>
    public class reqTopListEntity
    {
        public string NewsID { get; set; } //咨询标识
        public int Type { get; set; } //页面类别，根据传入的值，跳转不同的页面   
        public int NewsType { get; set; } //咨询类别1：咨询，2：活动
        public string NewsTitle { get; set; } //咨询标题
        public string PublishTime { get; set; } //时间
        public string ImageUrl { get; set; } //图片地址
        public string ThumbnailImageUrl { get; set; } //缩略图地址
    }
    #endregion

    #region 活动类
    /// <summary>
    /// 获取活动标识
    /// </summary>
    public class getActivityByActivityIDEntity
    {
        public string ActivityID { get; set; } //活动标识
    }
    /// <summary>
    /// 获取活动详情
    /// </summary>
    public class retActivityByActivityIDEntity
    {
        public string ActivityID { get; set; } //活动标识
        public string ActivityTitle { get; set; } //活动标题
        public string ActivityIntro { get; set; } //活动简介
        public string ActivityEmail { get; set; } //活动Email
        public string ActivityCompany { get; set; } //活动协会
        public string BeginTime { get; set; } //开始时间
        public string EndTime { get; set; } //结束时间
        public decimal BeginDay { get; set; } //还有几天开始,0当天开始,1还有一天
        public decimal EndDay { get; set; } //还有几天结束,0当天结束,1还有一天结束,-1已结束
        public string ActivityLinker { get; set; } //报名活动人员
        public string ActivityPhone { get; set; } //活动联系电话
        public string ActivityAddr { get; set; } //活动地址
        public string ActivityUp { get; set; } //活动报名人数
        public string ActivityContent { get; set; } //活动内容
        public string TicketID { get; set; } //当前人员的票ID（目前是EventSignUPid）
        public string ActivityTime { get; set; }
        public string Longitude { get; set; }//经度
        public string Latitude { get; set; }//纬度
        public int? TicketSum { get; set; }//活动总人数限制
        public int? VipSum { get; set; }//已报名会员数量
        public List<reqTicketEntity> Ticket { get; set; } //票的实体类
        public int? IsTicketRequired { get; set; }//是否要门票
        /// <summary>
        /// 浏览数
        /// </summary>
        public int? BrowseCount { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int? PraiseCount { get; set; }

        /// <summary>
        /// 收藏数
        /// </summary>
        public int? BookmarkCount { get; set; }

        /// <summary>
        /// 分享数
        /// </summary>
        public int? ShareCount { get; set; }
    }

    /// <summary>
    /// 票的实体类
    /// </summary>
    public class reqTicketEntity
    {
        public string TicketID { get; set; }
        public string TicketName { get; set; }
        public string TicketRemark { get; set; }
        public decimal TicketPrice { get; set; }
        public int? TicketNum { get; set; }
        public int? TicketSort { get; set; }
        public int? TicketMore { get; set; }
    }


    /// <summary>
    /// 返回活动详情
    /// </summary>
    public class reqActivityEntity
    {
        public retActivityByActivityIDEntity Activity { get; set; } //活动详情
    }

    public class submitActivityInfoEntity
    {
        public List<ControlUpdateEntity> Control { get; set; } //上传的内容
        public string ActivityID { get; set; } //活动ID       
        public string TicketID { get; set; }  //票ID
        public decimal? TicketPrice { get; set; }  //票ID
    }

    public class reqActivityInfoEntity
    {
        public string ActivityVipID { get; set; }
    }

    /// <summary>
    /// 活动报名返回的订单ID
    /// </summary>
    public class reqEventInfoEntity
    {
        public string OrderID { get; set; }
        public string VipID { get; set; }
    }
    #endregion

    #region 4.13-
    public class submintNewsCountByIDEntity
    {
        public string NewsID { get; set; }
        public string CountType { get; set; }
        public string NewsType { get; set; }
    }
    #endregion

    #region EventsTypeEntity
    public class EventsTypeEntity
    {
        public string EventTypeID { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public int ChannelCode { get; set; }
    }
    #endregion

    #region VipPriceEntity
    public class VipPriceEntity
    {
        public decimal itemPrice { set; get; }//商品价格
        public string itemCode { set; get; }//商品编号
        public string itemName { set; get; }//商品名称
        public string itemId { set; get; }//商品ID
        public string vipName { set; get; }//用户名
        public string beginTime { set; get; }//开始时间
        public string endTime { set; get; }//结束时间
    }
    #endregion

    #region 2013-05-09 tiansheng 修改

    /// <summary>
    /// 视频详细
    /// </summary>
    public class LEventsAlbumDetail
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        public string AlbumId { get; set; }

        /// <summary>
        /// 视频图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        public string  VideoUrl { get; set; }

        

        /// <summary>
        /// 浏览数
        /// </summary>
        public int? BrowseCount { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int? PraiseCount { get; set; }

        /// <summary>
        /// 收藏数
        /// </summary>
        public int? BookmarkCount { get; set; }

        /// <summary>
        /// 分享数
        /// </summary>
        public int? ShareCount { get; set; }
    }

    /// <summary>
    /// 返回咨询详细
    /// </summary>
    public class reqLEventsAlbumEntity
    {
        public LEventsAlbumDetail Album { get; set; } //视频详细
    }

    /// <summary>
    /// 查询详细信息
    /// </summary>
    public class getLEventsAlbumEntity
    {
        public string AlbumID { get; set; } //视频标识
    }
    #endregion

}