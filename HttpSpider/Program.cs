using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpSpider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string url = "https://m.baidu.com/s?word=全国川氏特色洗鼻机构&sa=tb&ts=5884904&t_kt=47&ss=110&pn=20";

            WebProxy proxyObject = new WebProxy("119.101.117.70", 9999);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";

            req.Proxy = proxyObject;
            req.Method = "GET";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Encoding code = Encoding.GetEncoding("UTF-8");
            using (StreamReader sr = new StreamReader(resp.GetResponseStream() ?? throw new InvalidOperationException(), code))
            {

                var html = sr.ReadToEnd();
                Console.WriteLine(html);
                Console.WriteLine("Hello World!");
                Console.ReadLine();

            }
        }
    }
}
