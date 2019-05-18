using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace AspNetCoreCodeStudy
{
    public class HttpHelper
    {
        public readonly string AppKey = "";

        public static string HttpPost(string url, IDictionary<string, string> parameters)
        {
            HttpWebResponse rsp = null;
            Stream reqStream = null;
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "Post";
                req.KeepAlive = false;
                req.ProtocolVersion = HttpVersion.Version10;
                req.Timeout = 5000;
                req.ContentType = "application/json;charset=UTF-8";
                var postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                req.ContentLength = postData.Length;
                reqStream = req.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);
                rsp = (HttpWebResponse)req.GetResponse();
                if (rsp.CharacterSet == null) return "";
                var encoding = Encoding.GetEncoding(rsp.CharacterSet);
                return GetResponseAsString(rsp, encoding);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                reqStream?.Close();
                rsp?.Close();
            }
        }

        public static string HttpGet(string url, IDictionary<string, string> parameters = null)
        {
            HttpWebRequest request;
            if (parameters != null)
            {
                request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(url);
            }

            request.Method = "Get";
            request.ReadWriteTimeout = 5000;
            request.ContentType = "application/json;charset=UTF-8";
            var response = (HttpWebResponse)request.GetResponse();
            var myResponseStream = response.GetResponseStream();
            if (myResponseStream == null) return "";
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            var retString = myStreamReader.ReadToEnd();
            return retString;
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <param name="encode"></param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters, string encode = "utf-8")
        {
            var postData = new StringBuilder();
            var hasParam = false;
            if (parameters == null) return postData.ToString();
            var dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                var name = dem.Current.Key;
                var value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (string.IsNullOrEmpty(name)) continue;
                if (hasParam)
                {
                    postData.Append("&");
                }
                postData.Append(name);
                postData.Append("=");
                switch (encode)
                {
                    case "gb2312":
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                        break;
                    case "utf8":
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                        break;
                    default:
                        postData.Append(value);
                        break;
                }
                hasParam = true;
            }
            dem.Dispose();
            return postData.ToString();
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        private static string GetResponseAsString(WebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                if (stream != null) reader = new StreamReader(stream, encoding);
                if (reader != null) return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                reader?.Close();
                stream?.Close();
                rsp?.Close();
            }
            return "";
        }
    }
}