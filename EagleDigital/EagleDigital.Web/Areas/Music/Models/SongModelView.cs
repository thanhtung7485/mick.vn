using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using EagleDigital.CodeFirst.TenantMusic.Models;

namespace EagleDigital.Web.Areas.Music.Models
{
    public class SongModelView : Song
    {
        private static string[] subResourcesToConsider = new string[] { "acl", "lifecycle", "location", "logging", "notification", "partNumber", "policy", "requestPayment", "torrent", "uploadId", "uploads", "versionId", "versioning", "versions", "website", };
        private static string[] overrideResponseHeadersToConsider = new string[] { "response-content-type", "response-content-language", "response-expires", "response-cache-control", "response-content-disposition", "response-content-encoding" };

        public S3Config S3Config { get; set; }
        public string Policy { get; set; }
        public string PolicySignature { get; set; }

        public string GetPolicy(S3Config config)
        {
            var policyJson = new JavaScriptSerializer().Serialize(new
            {
                expiration = DateTime.UtcNow.Add(config.ExpirationTime).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                conditions = new object[] {
                    new { bucket = config.Bucket },
                    new [] { "starts-with", "$key", config.KeyPrefix },
                    new { acl = config.Acl },
                    //new [] { "starts-with", "$success_action_redirect", "" },
                    new [] { "starts-with", "$Content-Type", config.ContentTypePrefix },
                    new [] { "starts-with", "$success_action_status", config.SuccessStatus },
                    new [] { "starts-with", "$x-amz-meta-qqfilename", "" },
                    //new [] { "starts-with", "$enclosure-type", "" },
                    // new Dictionary<string, string> {{ "success_action_status", config.SuccessStatus }}
                    //new Dictionary<string, string> {{ "x-amz-meta-uuid", config.Uuid.ToString() }}
                    //
                }
            });

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(policyJson));
        }

        public string GetSign(string text, string key)
        {
            var signer = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(signer.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }

        public  string GetStringToSign(Uri requestUri, string httpVerb, string contentMD5, string contentType, long secondsSince1stJan1970, NameValueCollection requestHeaders)
        {
            var canonicalizedResourceString = GetCanonicalizedResourceString(requestUri);
            var canonicalizedAmzHeadersString = GetCanonicalizedAmzHeadersString(requestHeaders);
            var stringToSign = string.Format("{0}\n{1}\n{2}\n{3}\n{4}{5}", httpVerb, contentMD5, contentType, secondsSince1stJan1970, canonicalizedAmzHeadersString, canonicalizedResourceString);
            return stringToSign;
        }

        public  string CreateSignature(string secretKey, string stringToSign)
        {
            byte[] dataToSign = Encoding.UTF8.GetBytes(stringToSign);
            using (HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey)))
            {
                return Convert.ToBase64String(hmacsha1.ComputeHash(dataToSign));
            }
        }

        public  string GetCanonicalizedResourceString(Uri requestUri)
        {
            var host = requestUri.DnsSafeHost;
            var hostElementsArray = host.Split('.');
            var bucketName = "";
            if (hostElementsArray.Length > 3)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hostElementsArray.Length - 3; i++)
                {
                    sb.AppendFormat("{0}.", hostElementsArray[i]);
                }
                bucketName = sb.ToString();
                if (bucketName.Length > 0)
                {
                    if (bucketName.EndsWith("."))
                    {
                        bucketName = bucketName.Substring(0, bucketName.Length - 1);
                    }
                    bucketName = string.Format("/{0}", bucketName);
                }
            }
            var subResourcesList = subResourcesToConsider.ToList();
            var overrideResponseHeadersList = overrideResponseHeadersToConsider.ToList();
            StringBuilder canonicalizedResourceStringBuilder = new StringBuilder();
            canonicalizedResourceStringBuilder.Append(bucketName);
            canonicalizedResourceStringBuilder.Append(requestUri.AbsolutePath);
            NameValueCollection queryVariables = HttpUtility.ParseQueryString(requestUri.Query);
            SortedDictionary<string, string> queryVariablesToConsider = new SortedDictionary<string, string>();
            SortedDictionary<string, string> overrideResponseHeaders = new SortedDictionary<string, string>();
            if (queryVariables != null && queryVariables.Count > 0)
            {
                var numQueryItems = queryVariables.Count;
                for (int i = 0; i < numQueryItems; i++)
                {
                    var key = queryVariables.GetKey(i);
                    var value = queryVariables[key];
                    if (subResourcesList.Contains(key))
                    {
                        if (queryVariablesToConsider.ContainsKey(key))
                        {
                            var val = queryVariablesToConsider[key];
                            queryVariablesToConsider[key] = string.Format("{0},{1}", value, val);
                        }
                        else
                        {
                            queryVariablesToConsider.Add(key, value);
                        }
                    }
                    if (overrideResponseHeadersList.Contains(key))
                    {
                        overrideResponseHeaders.Add(key, HttpUtility.UrlDecode(value));
                    }
                }
            }
            if (queryVariablesToConsider.Count > 0 || overrideResponseHeaders.Count > 0)
            {
                StringBuilder queryStringInCanonicalizedResourceString = new StringBuilder();
                queryStringInCanonicalizedResourceString.Append("?");
                for (int i = 0; i < queryVariablesToConsider.Count; i++)
                {
                    var key = queryVariablesToConsider.Keys.ElementAt(i);
                    var value = queryVariablesToConsider.Values.ElementAt(i);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        queryStringInCanonicalizedResourceString.AppendFormat("{0}={1}&", key, value);
                    }
                    else
                    {
                        queryStringInCanonicalizedResourceString.AppendFormat("{0}&", key);
                    }
                }
                for (int i = 0; i < overrideResponseHeaders.Count; i++)
                {
                    var key = overrideResponseHeaders.Keys.ElementAt(i);
                    var value = overrideResponseHeaders.Values.ElementAt(i);
                    queryStringInCanonicalizedResourceString.AppendFormat("{0}={1}&", key, value);
                }
                var str = queryStringInCanonicalizedResourceString.ToString();
                if (str.EndsWith("&"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                canonicalizedResourceStringBuilder.Append(str);
            }
            return canonicalizedResourceStringBuilder.ToString();
        }


        public  string GetCanonicalizedAmzHeadersString(NameValueCollection requestHeaders)
        {
            var canonicalizedAmzHeadersString = string.Empty;
            if (requestHeaders != null && requestHeaders.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                SortedDictionary<string, string> sortedRequestHeaders = new SortedDictionary<string, string>();
                var requestHeadersCount = requestHeaders.Count;
                for (int i = 0; i < requestHeadersCount; i++)
                {
                    var key = requestHeaders.Keys.Get(i);
                    var value = requestHeaders[key].Trim();
                    key = key.ToLowerInvariant();
                    if (key.StartsWith("x-amz-", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (sortedRequestHeaders.ContainsKey(key))
                        {
                            var val = sortedRequestHeaders[key];
                            sortedRequestHeaders[key] = string.Format("{0},{1}", val, value);
                        }
                        else
                        {
                            sortedRequestHeaders.Add(key, value);
                        }
                    }
                }
                if (sortedRequestHeaders.Count > 0)
                {
                    foreach (var item in sortedRequestHeaders)
                    {
                        sb.AppendFormat("{0}:{1}\n", item.Key, item.Value);
                    }
                    canonicalizedAmzHeadersString = sb.ToString();
                }
            }
            return canonicalizedAmzHeadersString;
        }

    }

    public class S3Config
    {
        public TimeSpan ExpirationTime { get; set; }
        public string AccessKey { get; set; }
        public string SecretAccessKey { get; set; }
        public string Bucket { get; set; }
        public string BucketUrl { get; set; }
        public string S3AmazonUrl { get; set; }
        public string KeyPrefix { get; set; }
        public string Acl { get; set; }
        public string ContentTypePrefix { get; set; }
        public Guid Uuid { get; set; }
        public string SuccessUrl { get; set; }
        public string SuccessStatus { get; set; }
    }
}