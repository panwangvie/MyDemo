using Newtonsoft.Json;
using Pwj.Shared.HttpContact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pwj.Service.HttpCertificateMethod;

namespace Pwj.Service
{
    /// <summary>
    /// 功能描述    ：BaseServiceRequest  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/5 11:28:51 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/5 11:28:51 
    /// </summary>
    public class BaseServiceRequest
    {
        private readonly string _requestUrl = GlobalData.serverUrl;

        public string requestUrl
        {
            get { return _requestUrl; }
        }
        HttpCertificateMethod httpCertificate = new HttpCertificateMethod();
        /// <summary>
        /// T请求
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="method">方法类型</param>
        /// <returns></returns>
        //public async Task<Response> GetRequest<Response>(BaseRequest request, Method method) where Response : class
        //{
        //    var pms = request?.Parameter;
        //    string url = requestUrl + request.route;
        //    string paraStr = JsonConvert.SerializeObject(pms);

        //    Response result = await httpCertificate.Requests(url, paraStr, method);


        //    return result;
        //}
    }
}
