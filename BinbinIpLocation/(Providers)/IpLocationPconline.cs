using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using Binbin.HttpHelper;

namespace BinbinIpLocation
{
    static class IpLocationPconline
    {
        public static IpLocation.IpLocationResult GetLocation(string ip)
        {
            //http://whois.pconline.com.cn/ipJson.jsp?callback=testJson&ip=221.178.189.237
            //if(window.testJson) {testJson({"ip":"221.178.189.237","pro":"江苏省","city":"连云港市","region":"","addr":"江苏省连云港市 移通","regionNames":""});}
            var request = new SyncHttpRequest();

            var s = HttpGet("http://whois.pconline.com.cn/ipJson.jsp?callback=testJson&ip=" + ip);
            var start = "if(window.testJson) {testJson(";
            var postfix = ");}\n\r\n";
            if (s.StartsWith(start) && s.EndsWith(postfix))
            {
                var json = s.Substring(start.Count(), s.Length - start.Length - postfix.Length);
                var serializer = new JavaScriptSerializer();
                // Produces string value of: 
                // [ 
                //     {"PersonID":1,"Name":"Bryon Hetrick","Registered":true},
                //     {"PersonID":2,"Name":"Nicole Wilcox","Registered":true},
                //     {"PersonID":3,"Name":"Adrian Martinson","Registered":false},
                //     {"PersonID":4,"Name":"Nora Osborn","Registered":false}
                // ] 

                var r = serializer.Deserialize<Result>(json);
                return new IpLocation.IpLocationResult()
                {
                    IP = r.ip,
                    City = r.city,
                    Province = r.pro,
                    Region = r.region,
                };
            }
            else
            {
                throw new Exception("can not find");
            }
        }

        private static string HttpGet(string addr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(addr);

            // Change Username and password.
            //request.Credentials = new NetworkCredential("[username]", "[password]");

            // Downloads the XML file from the specified server.
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                using (var sr = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("gb2312")))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        private class Result
        {
            public string ip { get; set; }
            public string pro { get; set; }
            public string city { get; set; }
            public string region { get; set; }

        }
    }
}