using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWWork_v2.Models
{
	public class ApiResult
	{
        public ApiResult() { }

        /// <summary>
        /// 状态码
        /// </summary>
        public int code { set; get; } = 0;
        /// <summary>
        /// 是否成功
        /// </summary>
       // public bool success { get; set; } = true;
       
        /// <summary>
        /// 总记录数
        /// </summary>
        public int count { set; get; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic data { set; get; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { set; get; }

        /// <summary>
        /// 序列化为字符串
        /// </summary>
        /// <returns></returns>
        // public override string ToString()
        // {
        //     return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        // }
    }
}
