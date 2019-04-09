namespace AliyunDemo
{
    internal class Config
    {



        //开发环境
        //您的访问密钥ID
        public static string AccessKeyId = "";
        //您的访问密钥
        public static string AccessKeySecret = "";
        //你的端点
        public static string Endpoint = "";
        //bucketName
        public static string BucketName = "";

        ////开发环境
        ////您的访问密钥ID
        //public static string AccessKeyId = "";
        ////您的访问密钥
        //public static string AccessKeySecret = "";
        ////你的端点
        //public static string Endpoint = "";
        ////bucketName
        //public static string BucketName = "";
        


        /// <summary>
        /// 本地文件上传
        /// </summary>
        public static string FileToUpload = @"C:\Users\Administrator\Desktop\12.png";
        //本地大文件上传
        public static string BigFileToUpload = "<your local big file to upload>";
        //要上传的本地图像文件
        public static string ImageFileToUpload = "<your local image file to upload>";
        //您的回调服务器URI
        public static string CallbackServer = "<your callback server uri>";
        //本地DIR下载文件
        public static string DirToDownload = "<your local dir to download file>";
    }
}
