using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebApiSoft.Class
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiHelper
    {
        /// <summary>
        ///  访问接口信息
        /// </summary>
        /// <param name="jsonString">抛送的字符串</param>
        /// <param name="achieveUrl">访问的路径</param>
        /// <param name="publishKey">密钥Token</param>
        /// <param name="method">访问方法</param>
        /// <returns></returns>
        public static string SendService(string jsonString, string achieveUrl, string publishKey, string method)
        {

            //用于返回信息的记录
            var responseValue = string.Empty;
            if (string.IsNullOrEmpty(achieveUrl)) return responseValue;

            //基于http协议的请求响应
            if (!(WebRequest.Create(achieveUrl) is HttpWebRequest request)) return responseValue;

            request.Method = method;
            //设置Http标头信息
            request.UserAgent = "";
            //设置请求超时时间
            request.Timeout = 1000 * 60 * 30;
            //设置读取/写入超时时间
            request.ReadWriteTimeout = 1000 * 60 * 30;
            //request.Headers.Add("", "");
            request.Headers.Add("Token", publishKey);
            request.ContentType = @"application/json";
            //判断访问方法
            if (method != "GET" && method != "PUT")
            {
                request.ContentLength = Encoding.UTF8.GetByteCount(jsonString);
                if (!string.IsNullOrEmpty(jsonString)) //如果传送的数据不为空，并且方法是put  
                {
                    var bytes = Encoding.GetEncoding("UTF-8").GetBytes(jsonString); //  
                    request.ContentLength = bytes.Length;
                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }

            //http请求的返回状态
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                //获取来自 服务器或接口的响应信息
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null) return responseValue;
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseValue = reader.ReadToEnd();
                    }
                }
            }
            return responseValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encode"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            string result;
            var bytes = encode.GetBytes(source);
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = source;
            }
            return result;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public string GetAddress()
        {
            var addressIp = string.Empty;
            foreach (var ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    addressIp = ipAddress.ToString();
                }
            }
            return addressIp;
        }

        /// <summary>
        /// 获取JSON文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetFileJson(string filepath)
        {
            string json;
            using (var fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }


    }
}
