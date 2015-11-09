using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amazon;
using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Amazon.S3;
using Amazon.S3.Model;
using EagleDigital.Web.Areas.Music.Models;

namespace EagleDigital.Web.Areas.Music.Controllers
{
    public class S3UploadController : Controller
    {
        private static string existingBucketName =System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSBucket"].Value;
        private static string accessKey =System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSAccessKey"].Value;
        private static string accessKeySecret =System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSSecretKey"].Value;
        private static string keyName = "";
        private static string filePath = "uploads/";

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult S3MultiUpload()
        {
            AmazonS3Config s3Config = new AmazonS3Config
            {
                ServiceURL = "s3.amazonaws.com",
                UseHttp = true
            };
            IAmazonS3 client = new AmazonS3Client(accessKey, accessKeySecret, Amazon.RegionEndpoint.USEast1);
            //AmazonS3Client client = new AmazonS3Client(accessKey, accessKeySecret);
            string sCommand = Request.Params["command"];
            sCommand = string.IsNullOrEmpty(sCommand) ? "" : sCommand.ToLower();
            switch (sCommand)
            {
                case "createmultipartupload":
                    {
                        InitiateMultipartUploadRequest req = new InitiateMultipartUploadRequest()
                        {
                            BucketName = existingBucketName,
                            Key = Request.Params["fileInfo[name]"],
                            //    ContentType = Request.Params["fileInfo[type]"]
                            ContentType = "application/octet-stream"
                        };
                        InitiateMultipartUploadResponse res = client.InitiateMultipartUpload(req);
                        return Json(new { uploadid = res.UploadId, key = res.Key }, JsonRequestBehavior.AllowGet);
                    }
                case "signuploadpart":
                    {
                        int partNo = 0; int.TryParse(Request.Params["partNumber"], out partNo);
                        long partSize = 0; long.TryParse(Request.Params["contentLength"], out partSize);
                        string uploadId = Request.Params["sendBackData[uploadId]"];

                        GetPreSignedUrlRequest req = new GetPreSignedUrlRequest()
                        {
                            BucketName = existingBucketName,
                            Key = Request.Params["sendBackData[key]"],
                            Verb = HttpVerb.PUT,
                            Expires = DateTime.Now.AddHours(10),
                            ContentType = "application/octet-stream"
                        };

                        var strReq = client.GetPreSignedURL(req);
                        strReq = string.Format("{0}&UploadId={1}&partNumber={2}", strReq, HttpUtility.UrlEncode(uploadId), partNo);

                        var requestUri = new Uri(strReq.Substring(0, strReq.IndexOf("?")));

                        var expiryDate = DateTime.UtcNow.AddHours(12);
                        DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        System.TimeSpan ts = new System.TimeSpan(expiryDate.Ticks - Jan1st1970.Ticks);
                        var expiry = Convert.ToInt64(ts.TotalSeconds);
                        var songModel = new SongModelView();
                        var partUploadUrl = new Uri(string.Format("{0}?uploadId={1}&partNumber={2}&ContentLength={3}", requestUri.AbsoluteUri, HttpUtility.UrlEncode(uploadId), partNo, partSize));
                        var partUploadSignature = songModel.CreateSignature(accessKeySecret, songModel.GetStringToSign(partUploadUrl, "PUT", string.Empty, string.Empty, expiry, null));
                        var partUploadPreSignedUrl = new Uri(string.Format("{0}?uploadId={1}&partNumber={2}&ContentLength={6}&AWSAccessKeyId={3}&Signature={4}&Expires={5}", requestUri.AbsoluteUri,
                            HttpUtility.UrlEncode(uploadId), partNo, accessKey, HttpUtility.UrlEncode(partUploadSignature), expiry, partSize));

                        return Json(new { partNo = partNo, url = partUploadPreSignedUrl.AbsoluteUri, authHeader = string.Format("AWS {0}:{1}", accessKey, HttpUtility.UrlEncode(partUploadSignature)), dateHeader = DateTime.Now.ToString("r") }, JsonRequestBehavior.AllowGet);
                    }
                case "completemultipartupload":
                    {
                        var etags = Request.Params["etags"].Split(',');
                        var partNos = Request.Params["partNos"].Split(',');
                        List<PartETag> lstEtags = new List<PartETag>();
                        for (int i = 0; i < etags.Length; i++)
                        {
                            int partNo = 0; int.TryParse(partNos[i], out partNo);
                            lstEtags.Add(new PartETag(partNo, etags[i]));
                        }
                        CompleteMultipartUploadRequest req = new CompleteMultipartUploadRequest()
                        {
                            BucketName = existingBucketName,
                            Key = Request.Params["sendBackData[key]"],
                            UploadId = Request.Params["sendBackData[uploadId]"],
                            PartETags = lstEtags
                        };

                        client.CompleteMultipartUpload(req);
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                case "abortmultipartupload":
                    {
                        AbortMultipartUploadRequest req = new AbortMultipartUploadRequest()
                        {
                            BucketName = existingBucketName,
                            Key = Request.Params["sendBackData[key]"],
                            UploadId = Request.Params["sendBackData[uploadId]"]
                        };
                        var resp = client.AbortMultipartUpload(req);
                        return Json(new { }, JsonRequestBehavior.AllowGet);
                    }
                default:
                    return Json(new { }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult S3MultiUpload(string command, string etags, string partNos, string fileName)
        {
            AmazonS3Config s3Config = new AmazonS3Config
            {
                ServiceURL = "s3.amazonaws.com",
                UseHttp = true
            };
            IAmazonS3 client = new AmazonS3Client(accessKey, accessKeySecret, Amazon.RegionEndpoint.USEast1);
            //AmazonS3Client client = new AmazonS3Client(accessKey, accessKeySecret);
            var sCommand = string.IsNullOrEmpty(command) ? "" : command.ToLower();
            switch (sCommand)
            {
                case "completemultipartupload":
                    {
                        var listEtags = etags.Split(',');
                        var listPartNos = partNos.Split(',');
                        List<PartETag> lstEtags = new List<PartETag>();
                        for (int i = 0; i < listEtags.Length; i++)
                        {
                            int partNo = 0; int.TryParse(listPartNos[i], out partNo);
                            lstEtags.Add(new PartETag(partNo, listEtags[i]));
                        }
                        CompleteMultipartUploadRequest req = new CompleteMultipartUploadRequest()
                        {
                            BucketName = existingBucketName,
                            Key = Request.Params["sendBackData[key]"],
                            UploadId = Request.Params["sendBackData[uploadId]"],
                            PartETags = lstEtags
                        };

                        client.CompleteMultipartUpload(req);
                        return Json(new { success = true, fileName = fileName }, JsonRequestBehavior.AllowGet);
                    }

                default:
                    return Json(new { }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetInformationFile(string filename)
        {
            try
            {
                var transcoder =
                     new AmazonElasticTranscoderClient(accessKey,
                               accessKeySecret, RegionEndpoint.USEast1);
                var ji = new JobInput
                {
                    AspectRatio = "auto",
                    Container = "mov",
                    FrameRate = "auto",
                    Interlaced = "auto",
                    Resolution = "auto",
                    Key = filename
                };

                //var output = new CreateJobOutput
                //{
                //    ThumbnailPattern = filename + "_{count}",
                //    Rotate = "auto",
                //    PresetId = "1351620000001-000010",
                //    Key = filename + "_enc.mp4"
                //};

                var createJob = new CreateJobRequest
                {
                    Input = ji,
                    //Output = output,

                    // PipelineId = "1413517673900-39qstm"
                };

                transcoder.CreateJob(createJob);
            }
            catch (Exception ex)
            {

                throw;
            }

            return View();
        }

    }
}
