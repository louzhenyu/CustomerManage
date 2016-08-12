using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.WeiXin.ShortUrl.Request {
	public class ShortUrlRP : IAPIRequestParameter {
		public string Url { get; set; }
		public void Validate() {

		}
	}
}
