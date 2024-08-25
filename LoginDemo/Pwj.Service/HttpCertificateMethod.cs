using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Service
{
    /// <summary>
    /// 功能描述    ：RestSharpCertificateMethod  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/4 16:37:09 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/4 16:37:09 
    /// </summary>
    public class HttpCertificateMethod
    {

        public async Task<string> Requests(string url,string param,Method method=Method.POST,bool isneedsession=true, bool istreatment = false, bool isJson = true)
        {

            try
            {
                HttpWebRequest https = WebRequest.CreateHttp(url);
                https.Method = method.ToString();
                if (isneedsession)
                {
                    https.Headers.Add("token", GlobalData.Token);
                }
                switch (method)
                {
                    case Method.GET:
                        https.ContentType = "application/json";

                        break;
                    case Method.POST:
                        https.KeepAlive = true;
                        if (isJson)
                        {
                            https.ContentType = "application/json";
                            byte[] bytes = Encoding.UTF8.GetBytes(param);
                            https.ContentLength = bytes.Length;
                            using (Stream reqstream = https.GetRequestStream())
                            {
                                reqstream.Write(bytes, 0, bytes.Length);
                            }

                        }
                        else
                        {
                            https.ContentType = "application/x-www-form-urlencoded";
                            using (StreamWriter sw = new StreamWriter(https.GetRequestStream()))
                            {
                                sw.Write(param);
                            }
                        }
                        break;
                    case Method.PUT:
                        https.Connection = "application/json";

                        break;
                    case Method.DELETE:
                        break;
                    case Method.HEAD:
                        break;
                    case Method.OPTIONS:
                        break;
                    case Method.PATCH:
                        break;
                    case Method.MERGE:
                        break;
                    case Method.COPY:
                        break;
                    default:
                        https.Connection = "application/json";

                        break;
                }
                //获取请求返回的数据 
                using (var response = (HttpWebResponse)await https.GetResponseAsync())
                {

                    ///如果是需要处理头部的
                    if (istreatment)
                    {
                        string cookies = response.Headers["Set-Cookie"];
                        string[] strarray = cookies.Split(';');
                        //数组处理
                        if (strarray != null
                            && strarray.Length > 0)
                        {
                            foreach (string arr in strarray)
                            {
                                if (arr.Contains("SESSION")
                                    || arr.Contains("session"))
                                {
                                    string[] arrString = arr.Split('=');
                                    GlobalData.Token = arrString[1];//赋值SessionID
                                    break;
                                }
                            }
                        }
                    }
                    //读取返回的信息 
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), true))
                    {
                        //获取响应格式信息
                        string result = sr.ReadToEnd();
                        return result;
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }


        }

        //
        // 摘要:
        //     HTTP method to use when making requests
        public enum Method
        {
            GET = 0,
            POST = 1,
            PUT = 2,
            DELETE = 3,
            HEAD = 4,
            OPTIONS = 5,
            PATCH = 6,
            MERGE = 7,
            COPY = 8
        }
    }
}
