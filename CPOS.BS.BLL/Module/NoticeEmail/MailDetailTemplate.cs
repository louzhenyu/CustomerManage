using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL.NoticeEmail;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL.Module.NoticeEmail
{
    public class MailDetailTemplate : IMailTemplate
    {

        public string Render (object data)
        {
            //var entity = data as VipEntity;
            //if (entity == null)
            //{
            //    throw new ArgumentNullException("data");
            //}
            var html = new StringBuilder();
            html.AppendFormat(@"
<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
<meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport'>
<title>EMBA联盟后台</title>
<style type='text/css'>
@charset 'utf-8';
@charset 'utf-8';
/*------------------ HTML5 Reset ------------------*/
html,body,div,span,object,iframe,h1,h2,h3,h4,h5,h6,p,address,cite,code,del,em,img,strong,sub,sup,b,i,dl,dt,dd,ol,ul,li,fieldset,form,label,legend,table,caption,tbody,tfoot,thead,tr,th,td,article,aside,canvas,details,figcaption,figure,footer,header,hgroup,menu,nav,section,summary,time,mark,audio,video{{margin:0;padding:0;border:0;outline:0;font-size:100%;vertical-align:baseline}}
article,aside,details,figcaption,figure,footer,header,hgroup,menu,nav,section{{display:block}}
img,object,embed{{max-width:100%}}
mark{{background-color:#ff9;color:#000;font-style:normal;font-weight:400}}
del{{text-decoration:line-through}}
caption,cite,em,i,th,b,strong{{font-style:normal;font-weight:400}}
select,input,button,textarea{{font-size:100%;font-style:normal;font-weight:400;vertical-align:middle}}
table{{border-collapse:collapse;border-spacing:0}}fieldset,img{{border:0}}legend{{color:#000}}sup{{vertical-align:text-top}}sub{{vertical-align:text-bottom}}iframe{{display:block}}li{{list-style:none}}img{{vertical-align:middle}}a img{{vertical-align:top}}textarea{{resize:none}}
div,th,td{{word-wrap:break-word;word-break:break-all}}
body,button,input,select,textarea{{font:14px/1.5 Arial,Helvetica,sans-serif}}
body{{text-align:left;color:#333;background:#fff}}
input[type='radio']{{vertical-align:middle}}
input[type='checkbox']{{vertical-align:middle}}
label,input[type='button'],input[type='submit'],button,.button{{cursor:pointer;border:none;outline:none;padding:0;margin:0;-webkit-appearance:button}}
/*input[type='text'],textarea{{background:#fff;color:#aaa;border:1px solid #ddd;
-webkit-transition:border linear 0.2s,box-shadow linear 0.2s;
transition:border linear 0.2s,box-shadow linear 0.2s;
-webkit-box-shadow:inset 0 1px 3px rgba(0,0,0,0.1);
box-shadow:inset 0 1px 3px rgba(0,0,0,0.1);
-webkit-border-radius:3px;
border-radius:3px}}
input[type='text']:focus,textarea:focus{{border-color:#52a8ec;outline:0;color:#333;
-webkit-box-shadow:inset 0 1px 3px rgba(0,0,0,0.1),0 0 8px rgba(82,168,236,0.6);
box-shadow:inset 0 1px 3px rgba(0,0,0,0.1),0 0 8px rgba(82,168,236,0.6);}}*/
/*通用样式*/
a{{color:#333;text-decoration:none}}
a:hover{{color:#none;}}
:focus{{outline:none}}
.al{{text-align:left}}.ac{{text-align:center}}.ar{{text-align:right}}
.fl{{float:left}}.fr{{float:right}}.ti{{text-indent:-9999px;overflow:hidden}}
.hide{{display:none}}
/*通用字体和颜色*/
h1{{font-size:18px;font-weight:400}}h2,h3{{font-size:16px;font-weight:400}}h4,h5,h6{{font-size:14px;font-weight:400}}
.f14{{font-size:14px}}.f16{{font-size:16px}}
.white{{color:#fff}}
.black{{color:#000}}
.red{{color:#ff6b50}}
.pink{{color:#fa719f}}
.blue{{color:#047fe5}}
.yellow{{color:#ffa453}}
.orange{{color:#FF7800}}
.gray{{color:#aaa}}
/*清除浮动*/
.clear{{display:block;clear:both;overflow:hidden;height:0;line-height:0;font-size:0}}
.clearfix:after{{content:' ';visibility:hidden;display:block;clear:both;height:0;font-size:0}}
.clearfix{{*zoom:1}}
/*补丁*/
.blank5{{height:5px;font-size:0;clear:both;visibility:hidden}}
.blank10{{height:10px;font-size:0;clear:both;visibility:hidden}}
.blank15{{height:15px;clear:both;visibility:hidden}}
.blank20{{height:20px;clear:both;visibility:hidden}}
.blank25{{height:25px;clear:both;visibility:hidden}}
.blank30{{height:30px;clear:both;visibility:hidden}}
.blank40{{height:40px;clear:both;visibility:hidden}}
.mt-5{{margin-top:5px}}.mt-10{{margin-top:10px}}.mt-15{{margin-top:15px}}.mt-20{{margin-top:20px}}
.mr-5{{margin-right:5px;display:inline}}.mr-10{{margin-right:10px;display:inline}}.mr-15{{margin-right:15px;display:inline}}.mr-20{{margin-right:20px;display:inline}}
.mb-5{{margin-bottom:5px}}.mb-10{{margin-bottom:10px}}.mb-15{{margin-bottom:15px}}.mb-20{{margin-bottom:20px}}
.ml-5{{margin-left:5px;display:inline}}.ml-10{{margin-left:10px;display:inline}}.ml-15{{margin-left:15px;display:inline}}.ml-20{{margin-left:20px;display:inline}}
.br-2{{-webkit-border-radius:2px;border-radius:2px}}
.br-5{{-webkit-border-radius:5px;border-radius:5px}}
/*public*/
* {{-webkit-box-sizing:border-box;box-sizing:border-box}}
.dis_box {{display:-webkit-box;display:-ms-flexbox}}
.box_flex1 {{-webkit-box-flex:1;flex:1;}}
.box_justify {{-webkit-box-pack:justify;box-pack:justify}}
.box_center {{-webkit-box-pack:center;box-pack:center}}
.box_a_center {{-webkit-box-align:center;box-align:center}}
body{{font:12px/1.2em '宋体';background:#ebebf2;color:#383838;}}
.page-720{{width:720px;min-height:500px;margin:0 auto;}}
.page-900{{width:900px;min-height:500px;margin:0 auto;}}
.header{{height:50px;background:#4d7dad;margin-bottom:44px;}}
.header .logo{{float:left;width:65px;height:65px;margin:0;/*text-indent:-9999em;background:url(http://o2oapi.aladingyidong.com/images/logo/logo.png) no-repeat 0 0;*/}}
.header .tit{{float:left;line-height:32px;padding-bottom:20px;padding-left:10px;font-size:18px;border-left:3px solid #ebebf2;color:#ebebf2;}}
.header table{{width: 100%}}
.content{{padding:40px 15px;}}
.hint{{padding:15px 20px;border-bottom:1px solid #4d7dad;}}
.info{{padding:15px 0;}}
.info span{{float:left;width:37%;line-height:16px;text-align:center;color:#000;}}
.info span i{{color:#f00;}}
.info .wTag{{width:25%;border-left:1px solid #4d7dad;border-right:1px solid #4d7dad;}}
/*table*/
.tabStyle{{height:320px;min-height:320px;border:1px solid #4d7dad;background:#fff;}}
table{{border-collapse:collapse;margin:0 auto;width:100%;text-align:center;}}
tr{{border-bottom:1px solid #4d7dad;}}
th{{line-height:42px;background:#4d7dad;color:#fff;}}
td{{line-height:55px;}}
table a{{text-decoration:underline;color:#4d7dad;}}
.copyright{{text-align:center;margin-top:40px;color:#5c5c5c;}}
/*detail*/
.wrapArea{{width:375px;margin:20px auto;padding:0 15px;border:1px solid #4d7dad;background:#fff;color:#383838;}}
.wrapArea p{{margin:15px 0;line-height: 16px;}}
.wrapArea em{{display:inline-block;width:65px;margin-right:15px;text-align:right;font-weight:bold;}}
.wrapArea .tit{{font-weight:bold;}}
</style>
</head>
<body>
  <div class='page-900'>
    <div class='header reheader'>
      <table>
            <tr>
                <td style='width:65px;'><h1 class='logo'><img src='http://o2oapi.aladingyidong.com/images/logo/logo.png' alt='联盟注册' /></h1></td>
                <td ><h2 class='tit'>会员注册通知</h2></td>
            </tr>
        </table>
    </div>
    <div class='retitle'>亲爱的童鞋，欢迎加入商学院EMBA联盟大家庭！</div>
    <div class='reline'></div>
    <div class='recontent'>
      <p style='margin-top:20px'>商学院EMBA联盟宗旨：</p>
      <p>（1）遵守“平等、互助、开放、共建、共享、共赢”原则；</p>
      <p>
        （2）实现跨界与整合：跨行业、跨学校、跨地域；整合中外主流商学院，及海外商学院的中国学员内外智慧，实践中研讨，成为中国商界的主流联盟，并成为中国传统企业转型升级的策源地，为传统企  业转型升级提供方法论、人才库、技术包和资金池。
      </p>
      <p>（3）远景：中国最知名最大的商学院EMBA同学会联盟，成为中国企业界EMBA联盟的领先品牌。</p>

      <p style='margin-top:20px'>商学院EMBA联盟打造如下四大平台：</p>
      <p>（1）学习平台：为促进本联盟EMBA同学之间的学习提供服务。</p>
      <p>（2）合作交流平台：为本联盟EMBA同学提供各种形式的合作交流服务。</p>
      <p>
        （3）活动组织及发布平台：组织本联盟EMBA同学之间的各种体育运动、户外运动、文艺比赛等各位文体活动，并发布与EMBA有关的各类运动及活动信息。
      </p>
      <p>（4）公益慈善平台：为本联盟EMBA同学或所在企业进行的公益慈善提供服务和指导</p>

      <p style='margin-top:20px' >商学院EMBA联盟官方网站是:<a href='http://WWW.EMBA-Union.com'>WWW.EMBA-Union.com</a>；</p>
      <p>官方微信:EMBA联盟（微信号：Emba-union）；</p>
      <p>官方微博：商学院EMBA联盟（<a href='http://weibo.com/embaorg'>http://weibo.com/embaorg</a>）;</p>
      <p style='margin-bottom:20px'>官方APP：商学院EMBA联盟。</p>

    </div>
    <div class='reline'></div>
    <div class='refoot'style='padding-top: 200px;color:#a0a0a0'>技术提供：协会宝--专注于协会/商会、俱乐部的微信和APP会员平台，技术支持请联系我们</div>
  </div>
</body>
</html>
");
            return html.ToString();
        }
    }
}

