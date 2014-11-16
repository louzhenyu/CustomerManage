using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.ZiXun.ZiXunInfo.Request
{
    public class AddZiXunCommentRP : IAPIRequestParameter
    {


        /// <summary>
        /// 咨询ID
        /// </summary>
        public string NewsId { get; set; }
        /// <summary>
        /// 评论人ID
        /// </summary>
        public string VIPId { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }



        public void Validate()
        {
            if (string.IsNullOrEmpty(NewsId))
                throw new Exception("参数NewsId为空");
        }

    }
}
