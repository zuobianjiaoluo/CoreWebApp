using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;

namespace AliyunDemo.Services
{
    public static class AliyunHelp
    {
        //static string accessKeyId = Config.AccessKeyId;
        //static string accessKeySecret = Config.AccessKeySecret;
        //static string endpoint = Config.Endpoint;
        //static string bucketName = Config.BucketName;
        //static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        //static string fileToUpload = Config.FileToUpload;


        ///// <summary>
        ///// 本地文件上传
        ///// </summary>
        //public static void PutObjectFromFile()
        //{
        //    const string key = "PutObjectFromFile";
        //    try
        //    {
        //       var response= client.PutObject(bucketName, key, fileToUpload);
                
        //        Console.WriteLine("上传文件:{0} 成功", key);
        //    }
        //    catch (OssException ex)
        //    {
        //        Console.WriteLine("失败错误代码: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
        //            ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("失败错误信息: {0}", ex.Message);
        //    }
        //}


        //阿里云获取私有对象语音文件地址
        static string accessKeyId = "LTAI8ybf3HQPYGi9";
        static string accessKeySecret = "wlJYPIF4VfY5jrKv29TzqYolEo815D";
        static string endpoint = Config.Endpoint;
        static string bucketName = "c21-yinhao-audio";
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        /// <summary>
        /// 获取私有对象
        /// </summary>
        /// <param name="key"></param>
        public static string GetObjectBySignedUrlWithClient(string key)
        {
            string fileUri = "";
            try
            {
                var metadata = client.GetObjectMetadata(bucketName, key);
                var etag = metadata.ETag;

                var req = new GeneratePresignedUriRequest(bucketName, key, SignHttpMethod.Get);
                // 生成用于访问指定对象的URL签名
                var uri = client.GeneratePresignedUri(req);
                fileUri = uri.AbsoluteUri;
                Console.WriteLine("通过签名获得对象成功.");
            }
            catch (OssException ex)
            {
                Console.WriteLine("获取阿里云私有对象失败: {0}; 错误信息: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取阿里云私有对象失败信息: {0}", ex.Message);
            }
            return fileUri;
        }

    }
}
